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
public partial class AccountStatements : System.Web.UI.Page
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
            if (IsPostBack == false)
            {
                LoadVendors();
                if (Session["AreaID"].ToString().Equals("3"))
                {
                    cboVendor.SelectedIndex = cboVendor.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                    cboVendor.Enabled = false;
                   
                }
               /* LoadTranType();
                if (Session["AreaID"].ToString().Equals("2"))
                {
                    cboTranType.SelectedIndex = cboTranType.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                    cboTranType.Enabled = false;
                }*/
               // LoadPayTypes();
                MultiView1.ActiveViewIndex = -1;
                Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                Button MenuReport = (Button)Master.FindControl("btnCalReports");
                Button MenuRecon = (Button)Master.FindControl("btnAccounts");
                Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                MenuTool.Font.Underline = false;
                MenuPayment.Font.Underline = false;
                MenuReport.Font.Underline = true;
                //MenuRecon.Font.Underline = false;
                //MenuAccount.Font.Underline = false;
                //MenuBatching.Font.Underline = false;
                lblTotal.Visible = false;
                DisableBtnsOnClick();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    //private void LoadUtilities()
    //{
    //    dtable = datafile.GetAllUtilities("0");
    //    cboTranType.DataSource = dtable;
    //    cboTranType.DataValueField = "UtilityCode";
    //    cboTranType.DataTextField = "Utility";
    //    cboTranType.DataBind();
    //}
    //private void LoadTranType()
    //{
    //    dtable = datafile.GetTranType();
    //    cboTranType.DataSource = dtable;
    //    cboTranType.DataValueField = "TypeId";
    //    cboTranType.DataTextField = "TranType";
    //    cboTranType.DataBind();
    //}
    private void Page_Unload(object sender, EventArgs e)
    {
        if (Rptdoc != null)
        {
            Rptdoc.Close();
            Rptdoc.Dispose();
            GC.Collect();
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
        cboVendor.SelectedValue = Session["CompanyCode"].ToString().Trim();
        cboVendor.Enabled=false;
    }
    //private void LoadPayTypes()
    //{
    //    dtable = datafile.GetPayTypes();
    //    cboPaymentType.DataSource = dtable;
    //    cboPaymentType.DataValueField = "PaymentCode";
    //    cboPaymentType.DataTextField = "PaymentType";
    //    cboPaymentType.DataBind();
    //}
    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        //btnConvert.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnConvert, "").ToString());

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            LoadTransactions();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadTransactions()
    {
        if (txtfromDate.Text.Equals(""))
        {
            DataGrid1.Visible = false;
            ShowMessage("From Date is required", true);
            txtfromDate.Focus();
        }
        else
        {
            string vendorcode = cboVendor.SelectedValue.ToString();
           // string vendorcode = "P00033";
            //string vendorref = txtpartnerRef.Text.Trim();
            //string Paymentcode = cboPaymentType.SelectedValue.ToString();
           // string FromAccount = txtAccount.Text.Trim();
            string CustName = txtCustName.Text.Trim();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
           // string ToAccount = txtSearch.Text.Trim();
            //string TranType = cboTranType.SelectedValue.ToString();
            
            //pick acc no.
            string accNo = datapay.GetAccountNo(vendorcode);

            dataTable = datapay.GetAccountStatements(vendorcode, CustName, fromdate, todate, accNo);
            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
            
            if (dataTable.Rows.Count > 0)
            {
                string rolecode = Session["RoleCode"].ToString();
                if (rolecode.Equals("004"))
                {
                    MultiView1.ActiveViewIndex = -1;
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;
                    CalculateTotal(dataTable);
                }
                DataGrid1.Visible = true;
                ShowMessage(".", true);
            }
            else
            {
                lblTotal.Text = ".";
                DataGrid1.Visible = false;
                lblTotal.Visible = false;
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("No Record found", true);
            }
        }
    }

    private void CalculateTotal(DataTable Table)
    {
        double total = 0;
        int t = 0;
        double bal = 0;
        t = Table.Rows.Count;
        foreach (DataRow dr in Table.Rows)
        {
            double amount = double.Parse(dr["TranAmount"].ToString());
            double balance = double.Parse(dr["AccountBalance"].ToString());
            bal = balance;
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
       // lblTotal.Text = "Total Amount of Transactions [" + total.ToString("#,##0") + "]" + "Total no. of transactions  [" + t.ToString("#,##0") + "]";
        lblTotal.Text = "Total No. of transactions  [" + t.ToString("#,##0") + "]" +" | "+ "Account Balance  [" + bal.ToString("#,##0") + "]";
    }
    private void LoadUsers()
    {

        DataGrid1.DataSource = dataTable;
        DataGrid1.DataBind();
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

    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            string vendorcode = cboVendor.SelectedValue.ToString();
            //string vendorcode = "P00033";
           // string vendorref = txtpartnerRef.Text.Trim();
          //  string Paymentcode = cboPaymentType.SelectedValue.ToString();
          //  string Account = txtAccount.Text.Trim();
            string CustName = txtCustName.Text.Trim();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
           // string teller = txtSearch.Text.Trim();
            //string utility = cboTranType.SelectedValue.ToString();

            //pick acc no.
            string accNo = datapay.GetAccountNo(vendorcode);

            dataTable = datapay.GetAccountStatements(vendorcode, CustName, fromdate, todate, accNo);
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }

    protected void cboVendor_DataBound(object sender, EventArgs e)
    {
        cboVendor.Items.Insert(0, new ListItem("All Agents", "0"));
       //cboVendor.Items.Insert(0, new ListItem("SDS", "0"));
       //cboVendor.Enabled = false;
        
    }
    //protected void cboTranType_DataBound(object sender, EventArgs e)
    //{
    //    cboTranType.Items.Insert(0, new ListItem("All Tran Types", "0"));
    //}
    //protected void cboPaymentType_DataBound(object sender, EventArgs e)
    //{
    //    cboPaymentType.Items.Insert(0, new ListItem("All Payment Types", "0"));
    //}
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
                Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "STATEMENTS");

            }
            else
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "STATEMENTS");

            }
        }
    }
    private void LoadRpt()
    {
        string vendorcode = cboVendor.SelectedValue.ToString();
       // string vendorcode = "P00033";
       // string vendorref = txtpartnerRef.Text.Trim();
        //string Paymentcode = cboPaymentType.SelectedValue.ToString();
       // string Account = txtAccount.Text.Trim();
        string CustName = txtCustName.Text.Trim();
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        //string teller = txtSearch.Text.Trim();
       // string utility = cboTranType.SelectedValue.ToString();


        //pick acc no.
        string accNo = datapay.GetAccountNo(vendorcode);

        dataTable = datapay.GetAccountStatements(vendorcode, CustName, fromdate, todate, accNo);
        dataTable = formatTable(dataTable);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);

        rptName = physicalPath + "\\Bin\\Reports\\AccountStatement.rpt";

        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dataTable);
        CrystalReportViewer1.ReportSource = Rptdoc;
        //Rptdoc.PrintToPrinter(1,true, 0,0);
    }

    private DataTable formatTable(DataTable dataTable)
    {
        DataTable formedTable;

        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);

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
        DataColumn myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "DateRange";
        dataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "PrintedBy";
        dataTable.Columns.Add(myDataColumn);

        // Add data to the new columns

        dataTable.Rows[0]["DateRange"] = Header;
        dataTable.Rows[0]["PrintedBy"] = Printedby;
        formedTable = dataTable;
        return formedTable;
    }
}
