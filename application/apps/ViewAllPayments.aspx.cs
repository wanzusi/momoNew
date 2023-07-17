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
public partial class ViewAllPayments : System.Web.UI.Page
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
                string RoleId = Session["RoleCode"].ToString();
                if (isRoleAuthorisedToVisitPage(RoleId))
                {
                    LoadTranTypes();
                    GetTypes();
                    MultiView1.ActiveViewIndex = -1;
                    lblTotal.Visible = false;
                    DisableBtnsOnClick();
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
   
    private void Page_Unload(object sender, EventArgs e)
    {
        if (Rptdoc != null)
        {
            Rptdoc.Close();
            Rptdoc.Dispose();
            GC.Collect();
        }
    }

    private string GetDistrictCode()
    {
        string ret = "0";
        string role = Session["RoleCode"].ToString();
        if (role.Equals("004") || role.Equals("005"))
        {
            ret = Session["DistrictCode"].ToString();
        }
        return ret;
    }
    private void LoadTranTypes()
    {
        dtable = datafile.GetTranType();
        cboPaymentType.DataSource = dtable;
        cboPaymentType.DataValueField = "TypeId";
        cboPaymentType.DataTextField = "TranType";
        cboPaymentType.DataBind();
        cboPaymentType.SelectedIndex = cboPaymentType.Items.IndexOf(cboPaymentType.Items.FindByValue("2"));
        cboPaymentType.Enabled = false;
    }
    private void GetTypes()
    {
        dtable = datafile.GetBeneficiaryTypes();
        cboBeneficiaryType.DataSource = dtable;
        cboBeneficiaryType.DataValueField = "TypeCode";
        cboBeneficiaryType.DataTextField = "TypeName";
        cboBeneficiaryType.DataBind();
    }
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
           LoadPayments();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadPayments()
    {
        
        if (txtfromDate.Text.Equals("") && txttoDate.Text.Equals(""))
        {
            DataGrid1.Visible = false;
            ShowMessage("From Date is required", true);
            txtfromDate.Focus();
        }
        else
        {
            string benType = cboBeneficiaryType.SelectedValue.ToString();
            string benName = txtBenName.Text;
            string benContact = txtbenConatct.Text;
            string payType = cboPaymentType.SelectedValue.ToString();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);

            List<string> ChargeType = bll.GetChargeType(benName);


            double Charge = Convert.ToDouble(ChargeType[1]);

            dataTable = datafile.GetCustomerPayments(benName, benContact, benType, payType, fromdate, todate, ChargeType[0], Charge);
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
                    GetTotalAmount(dataTable);
                }
                DataGrid1.Visible = true;                
                ShowMessage(".", true);
            }
            else
            {
                lblTotal.Text = ".";
                lblMnoFee.Text = ".";
                lblPegasusTotal.Text = ".";
                lblAllTotal.Text = ".";
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
        lblTotal.Text = "Total Amount of Payments [" + total.ToString("#,##0") + "]";
    }
    private void GetTotalAmount(DataTable dataTable)
    {
        double total = 0;
        double pegasusTotal = 0;
        double MNOTotal = 0;
        double CashoutTotal = 0;
        double overallTotal = 0;
        foreach (DataRow dr in dataTable.Rows)
        {
            double amount = Convert.ToDouble(dr["Amount"].ToString());
            double amount2 = Convert.ToDouble(dr["TranCharge"].ToString());
            double amount3 = Convert.ToDouble(dr["PayOutFee"].ToString());
            double amount4 = Convert.ToDouble(dr["Amount"].ToString());
            double amount5 = Convert.ToDouble(dr["CashOutFee"].ToString().Replace(",",""));
            MNOTotal += amount3;
            pegasusTotal += amount2;
            total += amount;
            CashoutTotal += amount5;
        }
        lblTotal.Text = "Total Amount(" + total.ToString("#,##0") + ")";
        lblMnoFee.Text = "Total MNO Fee(" + MNOTotal.ToString("#,##0") + ")";
        lblPegasusTotal.Text = "Total Pegasus Fee(" + pegasusTotal.ToString("#,##0") + ")";
        overallTotal = MNOTotal + pegasusTotal + total + CashoutTotal;
        lblAllTotal.Text = "Overall Total Amount(" + overallTotal.ToString("#,##0") + ")";
        lblCashoutFee.Text = "Total CashOut Fee (" + CashoutTotal.ToString("#,##0") + ")";
        if (Session["RoleCode"].ToString() == "015")
        {
            // txtTotalAmount.Text = total.ToString("#,##0");
        }
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
        try
        {
            if (e.CommandName.Equals("btnPrint"))
            {
                string receiptno = e.Item.Cells[2].Text;
                string vendorRef = e.Item.Cells[3].Text;
                //LoadReceipt(receiptno, vendorRef);
                Session["frompage"] = "ViewPayments.aspx";
                Response.Redirect("./Receipt.aspx?transfereid=" + receiptno + "&transferecode=" + vendorRef, false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadReceipt(string receiptno, string vendorref)
    {
        Responseobj res = new Responseobj();
        res.VendorRef = vendorref;
        res.Receiptno = receiptno;
        dataTable = datapay.GetPaymentDetails(res);
        dataTable = formatReceiptTable(dataTable);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);

        rptName = physicalPath + "\\Bin\\Reports\\Receipt.rpt";

        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dataTable);
        CrystalReportViewer1.ReportSource = Rptdoc;
        Rptdoc.PrintOptions.PaperSize = PaperSize.PaperEnvelopeDL;
        Rptdoc.PrintToPrinter(1, true, 0, 0);
        Rptdoc.Dispose();
    }
    private DataTable formatReceiptTable(DataTable dataTable)
    {
        DataTable formedTable;
        string Header = "RE-PRINTED PAYMENT RECEIPT";
        DataColumn myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "Title";
        dataTable.Columns.Add(myDataColumn);
        // Add data to the new columns

        dataTable.Rows[0]["Title"] = Header;
        formedTable = dataTable;
        return formedTable;
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            string benType = cboBeneficiaryType.SelectedValue.ToString();
            string benName = txtBenName.Text;
            string benContact = txtbenConatct.Text;
            string payType = cboPaymentType.SelectedValue.ToString();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            List<string> ChargeType = bll.GetChargeType(benName);


            double Charge = Convert.ToDouble(ChargeType[1]);
            dataTable = datafile.GetCustomerPayments(benName, benContact, benType, payType, fromdate, todate, ChargeType[0],Charge);
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }

    protected void cboPaymentType_DataBound(object sender, EventArgs e)
    {
        cboPaymentType.Items.Insert(0, new ListItem("All Payment Types", "0"));
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

    private void ConvertToFile()
    {
        if (rdExcel.Checked.Equals(false) && rdPdf.Checked.Equals(false))
        {
            ShowMessage("Please Check file format to Convert to", true);
        }
        else
        {
            LoadRpt();
        }
    }
    private void LoadRpt()
    {
        string benType = cboBeneficiaryType.SelectedValue.ToString();
        string benName = txtBenName.Text;
        string benContact = txtbenConatct.Text;
        string payType = cboPaymentType.SelectedValue.ToString();
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        List<string> ChargeType = bll.GetChargeType(benName);


        double Charge = Convert.ToDouble(ChargeType[1]);
        dataTable = datafile.GetCustomerPayments(benName, benContact, benType, payType, fromdate, todate, ChargeType[0],Charge);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);
        rptName = physicalPath + "\\bin\\reports\\CustomerPayments.rpt"; 
        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dataTable);
        CrystalReportViewer1.ReportSource = Rptdoc;

        Response.Buffer = false;
        Response.ClearContent();
        Response.ClearHeaders();
        if (rdPdf.Checked.Equals(true))
        {
            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "PAYMENTS");

        }
        else
        {
            Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "PAYMENTS");

        }
    }

    private DataTable formatTable(DataTable dataTable)
    {
        DataTable formedTable;
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);

        string Header = "PAYMENT(S) BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
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
    protected void cboBeneficiaryType_DataBound(object sender, EventArgs e)
    {
        cboBeneficiaryType.Items.Insert(0, new ListItem("-- Select Type --", "0"));
    }
}
