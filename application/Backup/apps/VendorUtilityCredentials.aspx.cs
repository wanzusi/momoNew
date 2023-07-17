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
using System.Drawing;

public partial class VendorUtilityCredentials : System.Web.UI.Page
{
    BusinessLogin bll = new BusinessLogin();

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
                else
                {
                    LoadData();
                    MultiView1.ActiveViewIndex = 0;
                    Multiview2.ActiveViewIndex = 1;
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
    private void LoadData()
    {
        bll.LoadSystemCompanies(userBranch, ddVendor);
        bll.LoadTelecoms(ddUtility);
    }

    //protected void ddUtility_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //disable switch until user searches first
    //        btnSwitch.Visible = false;
    //        SearchDb();
    //    }
    //    catch (Exception ex)
    //    {
    //        bll.LoadTelecoms(ddUtility);
    //    }
    //}

    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = SearchDb();
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (!rdPdf.Checked && !rdExcel.Checked)
                    {
                        bll.ShowMessage(lblmsg, "CHECK ONE EXPORT OPTION", true, Session);
                    }
                    else if (rdExcel.Checked)
                    {
                        bll.ExportToExcel(dt, "", Response);
                    }
                    else if (rdPdf.Checked)
                    {
                        bll.ExportToPdf(dt, "", Response);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                string msg = "No Records Found Matching Search Criteria";
                bll.ShowMessage(lblmsg, msg, true, Session);
            }
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SearchDb();
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    private DataTable SearchDb()
    {
        DataTable dt = new DataTable();
        try
        {
            if (ddUtility.SelectedValue == "")
            {
                bll.ShowMessage(lblmsg, "Please select a telecom", true, Session);
                ddUtility.Focus();
            }
            else
            {
                string[] searchParams = GetSearchParameters();
                dt = bll.ExecuteProcedure("SearchVendorCredentials", searchParams).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    btnSwitch.Visible = true;
                    dataGridResults.DataSource = dt;
                    dataGridResults.DataBind();
                    string msg = "Found " + dt.Rows.Count + " Records Matching Search Criteria";
                    Multiview2.ActiveViewIndex = 0;
                    bll.ShowMessage(lblmsg, msg, false, Session);
                }
                else
                {
                    btnSwitch.Visible = false;
                    dataGridResults.DataSource = dt;
                    dataGridResults.DataBind();
                    string msg = "No Records Found Matching Search Criteria";
                    bll.ShowMessage(lblmsg, msg, true, Session);
                }

            }

        }
        catch (Exception ex)
        {
            dataGridResults.DataSource = dt;
            dataGridResults.DataBind();
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
        return dt;
    }

    private string[] GetSearchParameters()
    {
        List<string> searchCriteria = new List<string>();
        string Vendor = ddVendor.SelectedValue;
        string utility = ddUtility.SelectedValue;
        string state = "";// ddState.SelectedValue;

        searchCriteria.Add(Vendor);
        searchCriteria.Add(utility);
        searchCriteria.Add(state);

        return searchCriteria.ToArray();
    }

    protected void btnSwitch_Click(object sender, EventArgs e)
    {
        try
        {
            bll.ExecuteProcedure("SwitchUtilityValidation", new string[] { ddVendor.SelectedValue, ddUtility.SelectedValue });
            bll.InsertIntoAuditLog(ddUtility.SelectedValue, "UPDATE", "Switch Utility Validation", userBranch, username, bll.GetCurrentPageName(), fullname + " changed the utility validation for " + ddUtility.SelectedValue);

            SearchDb();
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = 0;
        GridViewRow row;
        GridView grid = sender as GridView;

        index = Convert.ToInt32(e.CommandArgument);
        row = grid.Rows[index];
        string vendor = row.Cells[1].Text;
        string utility = row.Cells[2].Text;

        if (e.CommandName.Equals("EditRow"))
        {
            Server.Transfer("./AddUtilityCredentials.aspx?transferid=" + vendor + "&utility=" + utility, true);
        }
    }
}

