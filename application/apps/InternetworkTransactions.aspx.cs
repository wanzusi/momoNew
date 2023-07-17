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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using System.Collections.Generic;

public partial class InternetworkTransactions : System.Web.UI.Page
{
    BusinessLogin bll = new BusinessLogin();
    string username = "";
    string fullname = "";
    string userBranch = "";

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
        bll.LoadTelecoms(ddVendor);
        bll.LoadTelecoms(ddTelecom);
        LoadStatus();
    }

    private void LoadStatus()
    {
        ddTranType.Items.Clear();
        ddTranType.Items.Add(new ListItem("All ", ""));
        ddTranType.Items.Add(new ListItem("Received", "ReceivedTransactions"));
        ddTranType.Items.Add(new ListItem("Pending", "PendingTransactions"));
        ddTranType.Items.Add(new ListItem("Failed", "FailedTransactions"));
        ddTranType.Items.Add(new ListItem("Reversed", "DeletedTransactions"));

        ddStatus.Items.Clear();
        ddStatus.Items.Add(new ListItem("All ", ""));
        ddStatus.Items.Add(new ListItem("SUCCESS", "SUCCESS"));
        ddStatus.Items.Add(new ListItem("PENDING", "PENDING"));
        ddStatus.Items.Add(new ListItem("PROCESSING", "PROCESSING"));

        ddTranCategory.Items.Clear();
        ddTranCategory.Items.Add(new ListItem("All ", ""));
        ddTranCategory.Items.Add(new ListItem("Yes", "true"));
        ddTranCategory.Items.Add(new ListItem("No", "false"));
    }

    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            string[] searchParams = GetSearchParameters();
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
                    bll.ShowMessage(lblmsg, ex.Message, true, Session);
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

    private void SetSessionVariables(DataTable dt)
    {
        Session["StatementDataTable"] = dt;
        if (string.IsNullOrEmpty(txtFromDate.Text) || string.IsNullOrEmpty(txtToDate.Text))
        {
            Session["fromDate"] = "THE START";
            Session["toDate"] = "TO TODAY";
        }
        else
        {
            Session["fromDate"] = txtFromDate.Text;
            Session["toDate"] = txtToDate.Text;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text.Equals(""))
            {
                bll.ShowMessage(lblmsg, "From Date is required", true, Session);
                txtFromDate.Focus();
            }
            else
            {
                SearchDb();
            }
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    private DataTable SearchDb()
    {
        DataLogin dh = new DataLogin();
        string[] searchParams = GetSearchParameters();
        DataTable dt = dh.ExecuteDataSet("SearchInterNetworkTransactionsModified", searchParams).Tables[0];
        if (dt.Rows.Count > 0)
        {
            CalculateTotal(dt);
            dataGridResults.DataSource = dt;
            dataGridResults.DataBind();
            string msg = "Found " + dt.Rows.Count + " Records Matching Search Criteria";
            Multiview2.ActiveViewIndex = 0;
            bll.ShowMessage(lblmsg, msg, false, Session);
        }
        else
        {
            CalculateTotal(dt);
            dataGridResults.DataSource = null;
            dataGridResults.DataBind();
            string msg = "No Records Found Matching Search Criteria";
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
        return dt;
    }

    private void CalculateTotal(DataTable Table)
    {
        double total = 0;
        foreach (DataRow dr in Table.Rows)
        {
            double amount = double.Parse(dr["Amount"].ToString());
            total += amount;
        }
        string rolecode = Session["RoleCode"].ToString();
        if (rolecode.Equals("004"))
        {
            lblTotal.Visible = false;
        }
        else
        {
            lblTotal.Visible = true;
        }
        lblTotal.Text = "Total Amount [" + total.ToString("#,##0") + "]";
    }

    private string[] GetSearchParameters()
    {
        List<string> searchCriteria = new List<string>();
        string Vendor = ddVendor.SelectedValue;
        string Telecom = ddTelecom.SelectedValue;
        string IsRetried = ddTranType.SelectedValue;
        string table = ddTranCategory.SelectedValue;
        string Status = ddStatus.SelectedValue;
        string Phone = txtPhone.Text;
        string VendorId = txtVendorTranId.Text;
        string TelecomId = txtTelecomId.Text;
        string FromDate = txtFromDate.Text;
        string ToDate = txtToDate.Text;

        searchCriteria.Add(Vendor);
        searchCriteria.Add(Telecom);
        searchCriteria.Add(table);
        searchCriteria.Add(IsRetried);
        searchCriteria.Add(Status);
        searchCriteria.Add(Phone);
        searchCriteria.Add(VendorId);
        searchCriteria.Add(TelecomId);
        searchCriteria.Add(FromDate);
        searchCriteria.Add(ToDate);

        return searchCriteria.ToArray();
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataGridResults.PageIndex = e.NewPageIndex;
        SearchDb();
    }
}
