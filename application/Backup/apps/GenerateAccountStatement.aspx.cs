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

public partial class GenerateAccountStatement : System.Web.UI.Page
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
                    LoadVendorStatus();
                    LoadTranStates();
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
            LoadAccountStatement();
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
        string UsersCompanyCode=Session["UserBranch"].ToString() ;
        if (UsersCompanyCode.ToUpper()!= "PEGPAY") 
        {
            cboVendor.SelectedValue = UsersCompanyCode;
            cboVendor.Enabled = false;
        }
    }

    private void LoadVendorStatus()
    {
           //Session["UserBranch"].ToString().Equals("PegPay");
        if (cboVendor.SelectedValue.ToString().Contains("TUGENDE"))
        {
            cboTranStatus.Visible = true;
            tran_status.Visible = true;
        }
        else
        {
            cboTranStatus.Visible = false;
            tran_status.Visible = false;
        }
    }

    private void LoadTranStates()
    {
        cboTranStatus.Items.Add(new ListItem("All status", "A"));
        cboTranStatus.Items.Add(new ListItem("SUCCESS_AT_TELECOM", "1"));
        cboTranStatus.Items.Add(new ListItem("REVERSED_AT_TELECOM", "2"));
        cboTranStatus.Items.Add(new ListItem("FAILED_AT_TELECOM", "3"));
    }

    protected void cboTranState_DataBound(object sender, EventArgs e)
    {

    }

    public void ExportToExcel(DataTable dtTable, string fullPath, HttpResponse Response)
    {
        if (dtTable == null)
        {
            return;
        }
        if (dtTable.Rows.Count > 0)
        {
            StringBuilder sbldr = new StringBuilder();
            if (dtTable.Columns.Count != 0)
            {
                foreach (DataColumn col in dtTable.Columns)
                {
                    sbldr.Append(col.ColumnName + ',');
                }
                sbldr.Append("\r\n");
                foreach (DataRow row in dtTable.Rows)
                {
                    foreach (DataColumn column in dtTable.Columns)
                    {
                        sbldr.Append(row[column].ToString() + ',');
                    }
                    sbldr.Append("\r\n");
                }
            }

            //File.WriteAllLines(fullPath, sbldr.ToArray());
            Response.ContentType = "Application/x-msexcel";
            Response.AddHeader("content-disposition", "attachment;filename=" + fullPath);
            Response.Write(sbldr.ToString());
            Response.End();
        }
    }

    internal void LoadAccountStatement()
    {
        string VendorCode = cboVendor.SelectedValue;
        string FromDate = txtfromDate.Text;
        string ToDate = txttoDate.Text;
        ShowMessage("", false);

        if (cboVendor.SelectedIndex == 0)
        {
            ShowMessage("PLEASE SELECT A VENDOR", true);
            return;
        }
        if (string.IsNullOrEmpty(FromDate))
        {
            ShowMessage("PLEASE SELECT A START DATE", true);
            return;
        }
        if (string.IsNullOrEmpty(ToDate))
        {
            ToDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (FromDate == ToDate)
        {
            string format = "yyyy-MM-dd";
            ToDate = DateTime.ParseExact(FromDate, format, CultureInfo.InvariantCulture).AddDays(1).ToString(format);
        }
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        string TranState = cboTranStatus.SelectedValue.ToString();
        
        List<DataTable> ds = datafile.GenerateAccountStatement(VendorCode, FromDate, ToDate);
        if (ds.Count > 0)
        {
            MultiView1.ActiveViewIndex = 0;
            string OpeningBal = ds[0].Rows[0][0].ToString().Split('.')[0];
            string ClosingBal = ds[2].Rows[0][0].ToString();
            long lnOpenBal = long.Parse(OpeningBal);
            long lnCloseBal = long.Parse(ClosingBal);
            lblOpeningBal.Text = "OpeningBal: " + lnOpenBal.ToString("#,##0");
            lblTotal.Text = "ClosingBal: " + lnCloseBal.ToString("#,##0");

            DataGrid1.CurrentPageIndex = 0;
            
            if (TranState == "1") 
            {
            DataRow[] drs2 = ds[1].Select("Status = 'SUCCESS'");
            DataTable dt2 = ds[1].Clone();
            //Import the Rows
            foreach (DataRow d in drs2)
            {
                dt2.ImportRow(d);
            }
            ds[1] = dt2;
            }

            if (TranState == "2")
            {
                DataRow[] drs2 = ds[1].Select("Status = 'REVERSED'");
                DataTable dt2 = ds[1].Clone();
                //Import the Rows
                foreach (DataRow d in drs2)
                {
                    dt2.ImportRow(d);
                }
                ds[1] = dt2;
            }

            if (TranState == "3")
            {
                DataRow[] drs2 = ds[1].Select("Status = 'FAILED'");
                DataTable dt2 = ds[1].Clone();
                //Import the Rows
                foreach (DataRow d in drs2)
                {
                    dt2.ImportRow(d);
                }
                ds[1] = dt2;
            }

            dataTable = ds[1];
            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();

        }
        else
        {
            ShowMessage("NO RECORDS FOUND", true);
        }
    }


    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {

    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try 
        {
            string VendorCode = cboVendor.SelectedValue;
            string FromDate = txtfromDate.Text;
            string ToDate = txttoDate.Text;
            ShowMessage("", false);

            if (cboVendor.SelectedIndex == 0)
            {
                ShowMessage("PLEASE SELECT A VENDOR", true);
                return;
            }
            if (string.IsNullOrEmpty(FromDate))
            {
                ShowMessage("PLEASE SELECT A START DATE", true);
                return;
            }
            if (string.IsNullOrEmpty(ToDate))
            {
                ToDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            }
            if (FromDate == ToDate)
            {
                string format = "yyyy-MM-dd";
                ToDate = DateTime.ParseExact(FromDate, format, CultureInfo.InvariantCulture).AddDays(1).ToString(format);
            }
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            string TranState = cboTranStatus.SelectedValue.ToString();
            //dataTable = datapay.GetAllTransactions(VendorCode, "", "", "", "0", "", fromdate, todate, "0", "0", "", "");


            //if (TranState == "1")
            //{
            //    Session["TransCategory"] = "SUCCESS";
            //    DataRow[] drs = dataTable.Select("Status = 'SUCCESS_AT_TELECOM'");
            //    DataTable dt2 = dataTable.Clone();
            //    //Import the Rows
            //    foreach (DataRow d in drs)
            //    {
            //        dt2.ImportRow(d);
            //    }
            //    dataTable = dt2;
            //}
            //if (TranState == "2")
            //{
            //    Session["TransCategory"] = "REVERSED";
            //    DataRow[] drs = dataTable.Select("Status = 'REVERSED_AT_TELECOM'");
            //    DataTable dt2 = dataTable.Clone();
            //    //Import the Rows
            //    foreach (DataRow d in drs)
            //    {
            //        dt2.ImportRow(d);
            //    }
            //    dataTable = dt2;
            //}

            //if (TranState == "3")
            //{
            //    Session["TransCategory"] = "FAILED";
            //    DataRow[] drs = dataTable.Select("Status = 'FAILED_AT_TELECOM'");
            //    DataTable dt2 = dataTable.Clone();
            //    //Import the Rows
            //    foreach (DataRow d in drs)
            //    {
            //        dt2.ImportRow(d);
            //    }
            //    dataTable = dt2;
            //}
            //else
            //{
            //    Session["TransCategory"] = "ALL";

            //}
            List<DataTable> ds = datafile.GenerateAccountStatement(VendorCode, FromDate, ToDate);
            if (ds.Count > 0)
            {
                MultiView1.ActiveViewIndex = 0;
                string OpeningBal = ds[0].Rows[0][0].ToString().Split('.')[0];
                string ClosingBal = ds[2].Rows[0][0].ToString();
                long lnOpenBal = long.Parse(OpeningBal);
                long lnCloseBal = long.Parse(ClosingBal);
                lblOpeningBal.Text = "OpeningBal: " + lnOpenBal.ToString("#,##0");
                lblTotal.Text = "ClosingBal: " + lnCloseBal.ToString("#,##0");

                DataGrid1.CurrentPageIndex = 0;

                if (TranState == "1")
                {
                    DataRow[] drs2 = ds[1].Select("Status = 'SUCCESS'");
                    DataTable dt2 = ds[1].Clone();
                    //Import the Rows
                    foreach (DataRow d in drs2)
                    {
                        dt2.ImportRow(d);
                    }
                    ds[1] = dt2;
                }

                if (TranState == "2")
                {
                    DataRow[] drs2 = ds[1].Select("Status = 'REVERSED'");
                    DataTable dt2 = ds[1].Clone();
                    //Import the Rows
                    foreach (DataRow d in drs2)
                    {
                        dt2.ImportRow(d);
                    }
                    ds[1] = dt2;
                }

                if (TranState == "3")
                {
                    DataRow[] drs2 = ds[1].Select("Status = 'FAILED'");
                    DataTable dt2 = ds[1].Clone();
                    //Import the Rows
                    foreach (DataRow d in drs2)
                    {
                        dt2.ImportRow(d);
                    }
                    ds[1] = dt2;
                }

                dataTable = ds[1];
                DataGrid1.CurrentPageIndex = e.NewPageIndex;
                DataGrid1.DataSource = dataTable;
                DataGrid1.DataBind();

            }
            else
            {
                ShowMessage("NO RECORDS FOUND", true);
            }
        
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
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
            LoadRpt1();

            if (rdPdf.Checked.Equals(true))
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "AccountStatement");

            }
            else
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "AccountStatement");

            }
        }
    }
    private void LoadRpt1()
    {
        string VendorCode = cboVendor.SelectedValue;
        string FromDate = txtfromDate.Text;
        string ToDate = txttoDate.Text;
        ShowMessage("", false);

        if (cboVendor.SelectedIndex == 0)
        {
            ShowMessage("PLEASE SELECT A VENDOR", true);
            return;
        }
        if (string.IsNullOrEmpty(FromDate))
        {
            ShowMessage("PLEASE SELECT A START DATE", true);
            return;
        }
        if (string.IsNullOrEmpty(ToDate))
        {
            ToDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (FromDate == ToDate)
        {
            string format = "yyyy-MM-dd";
            ToDate = DateTime.ParseExact(FromDate, format, CultureInfo.InvariantCulture).AddDays(1).ToString(format);
        }

        ////////post added code
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        string TranState = cboTranStatus.SelectedValue.ToString();
        //dataTable = datapay.GetAllTransactions(VendorCode, "", "", "", "0", "", fromdate, todate, "0", "0", "", "");
        //if (TranState == "1")
        //{
        //    DataRow[] drs = dataTable.Select("Status = 'SUCCESS_AT_TELECOM'");
        //    DataTable dt2 = dataTable.Clone();
        //    //Import the Rows
        //    foreach (DataRow d in drs)
        //    {
        //        dt2.ImportRow(d);
        //    }
        //    dataTable = dt2;
        //}
        //if (TranState == "2")
        //{

        //    DataRow[] drs = dataTable.Select("Status = 'REVERSED_AT_TELECOM'");
        //    DataTable dt2 = dataTable.Clone();
        //    //Import the Rows
        //    foreach (DataRow d in drs)
        //    {
        //        dt2.ImportRow(d);
        //    }
        //    dataTable = dt2;
        //}

        //if (TranState == "3")
        //{

        //    DataRow[] drs = dataTable.Select("Status = 'FAILED_AT_TELECOM'");
        //    DataTable dt2 = dataTable.Clone();
        //    //Import the Rows
        //    foreach (DataRow d in drs)
        //    {
        //        dt2.ImportRow(d);
        //    }
        //    dataTable = dt2;
        //}
        //else
        //{

        //}
        ////////end post added code

        List<DataTable> ds = datafile.GenerateAccountStatement(VendorCode, FromDate, ToDate);
        ParameterField parameterField = new ParameterField();
        ParameterField parameterField2 = new ParameterField();
        ParameterField parameterField3 = new ParameterField();
        parameterField.Name = "@VendorName";
        parameterField2.Name = "@PrintedBy";
        parameterField3.Name = "@DateRange";
        //Create a new Discrete Value
        ParameterDiscreteValue parameterDiscreteValue = new ParameterDiscreteValue();
        ParameterDiscreteValue parameterDiscreteValue2 = new ParameterDiscreteValue();
        ParameterDiscreteValue parameterDiscreteValue3 = new ParameterDiscreteValue();
        parameterDiscreteValue.Value = Session["Company"].ToString();
        parameterDiscreteValue2.Value = Session["FullName"].ToString();
        parameterDiscreteValue3.Value = "ACCOUNT STATEMENT FOR " + FromDate + " TO " + ToDate;
        string Daterange = "ACCOUNT STATEMENT FOR " + FromDate + " TO " + ToDate;
        //Add the value
        parameterField.CurrentValues.Add(parameterDiscreteValue);
        parameterField2.CurrentValues.Add(parameterDiscreteValue2);
        parameterField3.CurrentValues.Add(parameterDiscreteValue3);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);
        rptName = physicalPath + "\\Bin\\Reports\\AccountStatementNew.rpt";
        Rptdoc.Load(rptName);

        //post added code
        if (TranState == "1")
        {
            DataRow[] drs2 = ds[1].Select("Status = 'SUCCESS'");
            DataTable dt2 = ds[1].Clone();
            foreach (DataRow d in drs2)
            {
                dt2.ImportRow(d);
            }
            ds[1] = dt2;
        }

        if (TranState == "2")
        {
            DataRow[] drs2 = ds[1].Select("Status = 'REVERSED'");
            DataTable dt2 = ds[1].Clone();
            foreach (DataRow d in drs2)
            {
                dt2.ImportRow(d);
            }
            ds[1] = dt2;
        }

        if (TranState == "3")
        {
            DataRow[] drs2 = ds[1].Select("Status = 'FAILED'");
            DataTable dt2 = ds[1].Clone();
            foreach (DataRow d in drs2)
            {
                dt2.ImportRow(d);
            }
            ds[1] = dt2;
        }
        dataTable = ds[1];
        ///end post added code

        Rptdoc.SetDataSource(dataTable);
        Rptdoc.SetParameterValue("VendorName", Session["Company"].ToString());
        Rptdoc.SetParameterValue("PrintedBy", Session["FullName"].ToString());
        Rptdoc.SetParameterValue("DateRange", Daterange);
        CrystalReportViewer1.ReportSource = Rptdoc;
        //Rptdoc.PrintToPrinter(1,true, 0,0);
    }
    private void LoadRpt()
    {
        string VendorCode = cboVendor.SelectedValue;
        string FromDate = txtfromDate.Text;
        string ToDate = txttoDate.Text;
        ShowMessage("", false);
        if (cboVendor.SelectedIndex == 0)
        {
            ShowMessage("PLEASE SELECT A VENDOR", true);
            return;
        }
        if (string.IsNullOrEmpty(FromDate))
        {
            ShowMessage("PLEASE SELECT A START DATE", true);
            return;
        }
        if (string.IsNullOrEmpty(ToDate))
        {
            ToDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (FromDate == ToDate)
        {
            ToDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        }

        List<DataTable> ds = datafile.GenerateAccountStatement(VendorCode, FromDate, ToDate);
        DataTable datatable = ds[1];
        string FileName = "PegasusVendorStatement.csv";
        ExportToExcel(datatable, FileName, Response);
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
            Header = "ALL AGENTS TRANSACTION(S) BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
        }
        else
        {
            Header = agent_name + " TRANSACTION(S) BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
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
    protected void cboTranStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
