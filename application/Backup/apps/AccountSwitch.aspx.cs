using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using InterLinkClass.EntityObjects;
using System.Collections.Generic;

public partial class AccountSwitch : System.Web.UI.Page
{
    BusinessLogin bll = new BusinessLogin();
    Datapay datapay = new Datapay();
    //
    string username = "";
    string fullname = "";
    string userBranch = "";
    string userRole = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
             string RoleId = Session["RoleCode"].ToString();
             if (isRoleAuthorisedToVisitPage(RoleId))
             {
                 //Check If this is an Edit Request
                 string Id = Request.QueryString["Id"];

                 username = Session["UserName"] as string;
                 fullname = Session["FullName"] as string;
                 userBranch = Session["UserBranch"] as string;
                 userRole = Session["RoleCode"] as string;

                 Session["IsError"] = null;

                 //Session is invalid
                 if (username == null)
                 {
                     Response.Redirect("Default.aspx");
                 }
                 else if (IsPostBack)
                 {

                 }
                 //this is an edit request
                 else if (Id != null)
                 {
                     LoadData();
                     MultiView1.ActiveViewIndex = 0;
                 }
                 else
                 {
                     LoadData();
                     MultiView1.ActiveViewIndex = 0;
                     Multiview2.ActiveViewIndex = 0;
                 }
             }
             else
             {
                 Response.Redirect("UnauthorisedAccess.aspx");
             }
        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
    }

    private void LoadData()
    {
        SearchDb();

    }
    private bool isRoleAuthorisedToVisitPage(string RoleId)
    {
        bool authorised = false;
        DataTable dt = (DataTable)Session["RolePageMatrix"];
        ArrayList roleIds = new ArrayList();
        string currenePage = GetCurrentPage();
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string page = dr["OtherPages"].ToString();
                if (page.Trim().ToUpper().Equals(currenePage.Trim().ToUpper()))
                {
                    roleIds.Add(dr["RoleId"].ToString().Trim());
                }
            }
            if (roleIds.Contains(RoleId.Trim()))
            {
                authorised = true;
            }
        }
        return authorised;
    }
    private string GetCurrentPage()
    {
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string sRet = oInfo.Name;
        return sRet;
    }
    private DataTable SearchDb()
    {
        DataTable dt = new DataTable();
        dt = datapay.GetMTNAccountsToSwitch();
        if (dt.Rows.Count > 0)
        {
            dataGridResults.DataSource = dt;
            dataGridResults.DataBind();
            string msg = "SUCCESS: Found " + dt.Rows.Count + " Records Matching Search Criteria";
            Multiview2.ActiveViewIndex = 0;
            bll.ShowMessage(lblmsg, msg, false, Session);
        }
        else
        {
            dataGridResults.DataSource = null;
            dataGridResults.DataBind();
            Multiview2.ActiveViewIndex = -1;
            string msg = "FAILED: No Records Found Matching Search Criteria";
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
        return dt;
    }

    protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            DataLogin dh = new DataLogin();
            ProcessUsers process = new ProcessUsers();

            //make this better later, caters only for merchant requests report
            if (e.CommandName == "Action")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                // Retrieve the row that contains the button clicked by the user from the Rows collection.

                GridViewRow row = dataGridResults.Rows[index];

                string newSenderId = row.Cells[3].Text;
                string newPasswd = row.Cells[4].Text;
                string newSpId = row.Cells[6].Text;

                string active = row.Cells[5].Text;

                //first get current account
                string senderid = dh.GetSystemParameter(3, 14);

                if (string.IsNullOrEmpty(senderid))
                {
                    throw new Exception("No default account found");
                }

                UpdateActiveOVA(newSenderId, "MTN");
                bll.InsertIntoAuditLog(senderid, "UPDATE", "SystemSettings", userBranch, username, bll.GetCurrentPageName(),
              fullname + " switched the transacting ova from [" + senderid + "] to " + newSenderId + " from IP: " + bll.GetIPAddress());

                SearchDb();

                bll.ShowMessage(lblmsg, "The ova has been switched from  [" + senderid + "] to " + newSenderId, false, Session);

            }
        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
    }

    internal void UpdateActiveOVA(string ova, string telecom)
    {
        try
        {
            string newActiveAccount = "";
            if (telecom.ToUpper() == "MTN")
            {
                newActiveAccount = ova;
            }
            else if (telecom.ToUpper() == "AIRTEL")
            {
                newActiveAccount = ova;
            }
            else
            {
                throw new Exception("UNSUPPORTED TELECOM");
            }

            datapay.ExecuteNonQuery("UpdateActiveOVA", telecom, newActiveAccount);
            return;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataGridResults.PageIndex = e.NewPageIndex;
        SearchDb();
    }
}
