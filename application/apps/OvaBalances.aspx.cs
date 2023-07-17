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
using InterLinkClass.EntityObjects;
using System.Xml;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;


public partial class OvaBalances : System.Web.UI.Page
{
   
    Datapay datapay = new Datapay();
    DataTable table;
    BusinessLogin bll = new BusinessLogin();
    public string ovaUsername = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                 string RoleId = Session["RoleCode"].ToString();
                 if (isRoleAuthorisedToVisitPage(RoleId))
                 {
                     MultiView1.ActiveViewIndex = 0;
                     LoadNetworks();
                 }
                 else
                 {
                     Response.Redirect("UnauthorisedAccess.aspx");
                 }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
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
    private void LoadNetworks()
    {
        ddNetwork.Items.Add(new ListItem("Select All Telecom ", ""));
        ddNetwork.Items.Add(new ListItem("MTN", "MTN"));
        ddNetwork.Items.Add(new ListItem("AIRTEL", "AIRTEL"));
        ddNetwork.DataBind();
    }

    protected void ddNetwork_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string telecom = ddNetwork.SelectedValue;
            if (string.IsNullOrEmpty(telecom))
            {
                //throw new Exception("No telecom selected");
            }
            else if (telecom.Equals("MTN"))
            {
                LoadAllOvas(telecom);
               // LoadOvas(telecom);
            }
            else if (telecom.Equals("AIRTEL"))
            {
                LoadAllOvas(telecom);
            }
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            ShowMessage(msg, true);
        }
    }

    //private void LoadOvas(string telecom)
    //{
        
    //    table = datapay.GetOvaAccountDetails(telecom);
    //    cboOvaAccount.DataSource = table;
    //    cboOvaAccount.DataTextField = "Ova";
    //    cboOvaAccount.DataValueField = "SenderId";
    //    cboOvaAccount.DataBind();
    //}

    private void ShowMessage(string GetMessage, bool ColorRed)
    {
        Label msg = (Label)Master.FindControl("lblmsg");
        msg.Visible = true;
        if (ColorRed == true) { msg.ForeColor = System.Drawing.Color.Red; msg.Font.Bold = false; }
        else { msg.ForeColor = System.Drawing.Color.Blue; msg.Font.Bold = true; }
        if (GetMessage == ".")
        {
            msg.Text = ".";
        }
        else
        {
            msg.Text = "MESSAGE: " + GetMessage;
        }
    }


    private void LoadAllOvas(string network)
    {
        table = new DataTable();
        DataLogin Dl = new DataLogin();
        table = Dl.GetOvaAccounts(network);
        cboOvaAccount.DataSource = table;
        cboOvaAccount.DataValueField = "Username";
        cboOvaAccount.DataTextField = "Username";
        cboOvaAccount.DataBind();
    }

    protected void cboOvaAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("", false);
            //ovaUsername = cboOvaAccount.SelectedValue.ToString();

            //string telecom = ddNetwork.SelectedValue;

            //GetAccountBalance(ovaUsername, telecom);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        try
        {
            DataTable dt = SearchDb();
            Gvuploadedreading.Visible = true;
            Gvuploadedreading.DataSource = dt;
            Gvuploadedreading.DataBind();
            MultiView2.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            ShowMessage(msg, true);
        }
    }
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
                        ShowMessage( "CHECK ONE EXPORT OPTION", true);
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
                ShowMessage( msg, true);
            }
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            ShowMessage( msg, true);
        }
    }

    private DataTable SearchDb()
    {
        string Network = ddNetwork.SelectedValue.ToString();
        string OvaName = cboOvaAccount.SelectedValue.ToString();
        if (txtFromDate.Text.Equals(""))
        {
            Gvuploadedreading.Visible = false;
            ShowMessage("From Date is required", true);
            txtFromDate.Focus();
            return new DataTable();
        }
        DateTime FromDate = bll.ReturnDate(txtFromDate.Text.Trim(), 1);
        DateTime ToDate = bll.ReturnDate(txtToDate.Text.Trim(), 2);
        //if (String.IsNullOrEmpty(txtFromDate.Text))
        //{
        //     FromDate = DateTime.Now.ToString();
        //}
        //if (String.IsNullOrEmpty(txtToDate.Text))
        //{
        //     ToDate = DateTime.Now.ToString();
        //}
        //DateTime Fromdate = Convert.ToDateTime(FromDate);
        //DateTime Todate = Convert.ToDateTime(ToDate);
        DataTable dt = new DataTable();
        DataLogin Df = new DataLogin();
        dt = Df.GetEODOVABalance(Network, OvaName, FromDate, ToDate);
        return dt;

    }
    protected void Gvuploadedreading_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gvuploadedreading.PageIndex = e.NewPageIndex;
        //Gvuploadedreading.DataBind();
        //loadreading();
    }
    protected void cboOvaAccount_DataBound(object sender, EventArgs e)
    {
        cboOvaAccount.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select All OVA Account", ""));
    }
}
