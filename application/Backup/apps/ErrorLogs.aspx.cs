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
using CrystalDecisions.CrystalReports.Engine;
using System.Collections.Generic;
using CrystalDecisions.Shared;
using System.Text;
using System.Globalization;

public partial class ErrorLogs : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    private ReportDocument Rptdoc = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string RoleId = Session["RoleCode"].ToString();
            if (isRoleAuthorisedToVisitPage(RoleId))
            {
                if (IsPostBack == false)
                {
                    LoadVendors();
                    MultiView1.ActiveViewIndex = -1;
                }
            }
            else
            {
                Response.Redirect("UnauthorisedAccess.aspx");
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
    private void ShowMessage(string Message, bool Error)
    {
        Label lblmsg = (Label)Master.FindControl("lblmsg");
        if (Error) { lblmsg.ForeColor = System.Drawing.Color.Red; lblmsg.Font.Bold = false; }
        else { lblmsg.ForeColor = System.Drawing.Color.Black; lblmsg.Font.Bold = true; }
        if (Message == ".")
        {
            lblmsg.Text = ".";
        }
        else
        {
            lblmsg.Text = "MESSAGE: " + Message.ToUpper();
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            LoadErrors();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadVendors()
    {
        //dtable = datafile.GetAllVendors("0");
        dtable = datafile.GetSystemCompanies("", "");
        cboVendor.DataSource = dtable;
        cboVendor.DataValueField = "CompanyCode";
        cboVendor.DataTextField = "Company";
        cboVendor.DataBind();

        //disbale drop down if user is not from Pegasus
        string UsersCompanyCode = Session["UserBranch"].ToString();
        if (UsersCompanyCode.ToUpper() != "PEGPAY")
        {
            cboVendor.SelectedValue = UsersCompanyCode;
            cboVendor.Enabled = false;
        }
    }

    public void LoadErrors()
    {
        string vendorCode = cboVendor.SelectedValue;
        string vendorRef = txtVendorId.Text;
        string phone = txtPhone.Text;
        string error = txtError.Text;
        string fromDate = txtfromDate.Text;
        string toDate = txttoDate.Text;
        ShowMessage("", false);
        DataGrid1.Visible = false;

        //if (cboVendor.SelectedIndex == 0)
        //{
            
        //    ShowMessage("PLEASE SELECT A VENDOR", true);
        //    return;
        //}
        if (string.IsNullOrEmpty(fromDate))
        {
            ShowMessage("PLEASE SELECT A START DATE", true);
            return;
        }
        if (string.IsNullOrEmpty(toDate))
        {
            toDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (fromDate == toDate)
        {
            string format = "yyyy-MM-dd";
            toDate = DateTime.ParseExact(fromDate, format, CultureInfo.InvariantCulture).AddDays(1).ToString(format);
        }


        DataTable dt = datapay.SearchErrorLogs(vendorCode, vendorRef.Trim(), phone.Trim(), error.Trim(), fromDate, toDate);
        if (dt.Rows.Count > 0)
        {
            MultiView1.ActiveViewIndex = 0;
            DataGrid1.DataSource = dt;
            DataGrid1.DataBind();
            DataGrid1.Visible = true;
        }
        else
        {
            DataGrid1.Visible = false;
            DataGrid1.DataSource = dt;
            DataGrid1.DataBind();
            ShowMessage("NO RECORDS FOUND", true);
        }
    }


    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {

    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        LoadErrors();
        DataGrid1.CurrentPageIndex = e.NewPageIndex;
    }

    protected void cboVendor_DataBound(object sender, EventArgs e)
    {
        cboVendor.Items.Insert(0, new ListItem("All Agents", "0"));
    }

    private void ConvertToFile()
    {
        if (rdExcel.Checked.Equals(false) && rdPdf.Checked.Equals(false))
        {
            ShowMessage("Please Check file format to Convert to", true);
        }
        else
        {
            LoadRpt();

            if (rdPdf.Checked.Equals(true))
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "ErrorReport");

            }
            else
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ErrorReport");

            }
        }
    }

    private void LoadRpt()
    {
        string vendorCode = cboVendor.SelectedValue;
        string vendorRef = txtVendorId.Text;
        string phone = txtPhone.Text;
        string error = txtError.Text;
        string fromDate = txtfromDate.Text;
        string toDate = txttoDate.Text;
        //DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        //DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);

        dataTable = datapay.SearchErrorLogs(vendorCode, vendorRef.Trim(), phone.Trim(), error.Trim(), fromDate, toDate);

        dataTable = formatTable(dataTable);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);

        rptName = physicalPath + "\\bin\\reports\\InternetworkTransReport.rpt";

        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dataTable);
        CrystalReportViewer1.ReportSource = Rptdoc;
    }

    private DataTable formatTable(DataTable dataTable)
    {
        DataTable formedTable = new DataTable();
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        formedTable.Columns.Add("DateRange");
        formedTable.Columns.Add("PrintedBy");

        string agent_code = cboVendor.SelectedValue.ToString();
        string agent_name = cboVendor.SelectedItem.ToString();
        string Header = "";
        if (agent_code.Equals("0"))
        {
            Header = "ERROR(S)LOGGED BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
        }
        else
        {
            Header = agent_name + " ERROR(S) LOGGED BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
        }
        string Printedby = "Printed By : " + Session["FullName"].ToString();
        foreach (DataColumn dc in dataTable.Columns)
        {
            formedTable.Columns.Add(dc.ColumnName);
        }

        foreach (DataRow dr in dataTable.Rows)
        {
            formedTable.Rows.Add(dr);
        }

        // Add data to the new columns
        formedTable.Rows[0]["DateRange"] = Header;
        formedTable.Rows[0]["PrintedBy"] = Printedby;
        formedTable = dataTable;
        return formedTable;
    }

    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            ConvertToFile();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
}
