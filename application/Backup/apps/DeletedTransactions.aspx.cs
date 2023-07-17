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
using System.Threading;
public partial class Transactions : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    Transaction2 Trans = new Transaction2();

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
                LoadTranType();
                if (Session["AreaID"].ToString().Equals("2"))
                {
                    cboTranType.SelectedIndex = cboTranType.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                    cboTranType.Enabled = false;
                }
                LoadPayTypes();
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

                if (Session["UserArea"].ToString().Equals("1") && Session["RoleCode"].ToString() == "007")
                {
                    DataGrid1.Columns[14].Visible = true;
                }
                else {
                    DataGrid1.Columns[14].Visible = false;
                }

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
    private void LoadTranType()
    {
        dtable = datafile.GetTranType();
        cboTranType.DataSource = dtable;
        cboTranType.DataValueField = "TypeId";
        cboTranType.DataTextField = "TranType";
        cboTranType.DataBind();
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
    private void LoadVendors()
    {
        //dtable = datafile.GetAllVendors("0");
        dtable = datafile.GetSystemCompanies("", "");
        cboVendor.DataSource = dtable;
        cboVendor.DataValueField = "CompanyCode";
        cboVendor.DataTextField = "Company";
        cboVendor.DataBind();
    }
    private void LoadPayTypes()
    {
        dtable = datafile.GetPayTypes();
        cboPaymentType.DataSource = dtable;
        cboPaymentType.DataValueField = "PaymentCode";
        cboPaymentType.DataTextField = "PaymentType";
        cboPaymentType.DataBind();
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
            LoadTransactions();
           
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    protected void back_Clik(object sender, EventArgs e)
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
            string vendorref = txtpartnerRef.Text.Trim();
            string Paymentcode = cboPaymentType.SelectedValue.ToString();
            string FromAccount = txtAccount.Text.Trim();
            string CustName = txtCustName.Text.Trim();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            string ToAccount = txtSearch.Text.Trim();
            string TranType = cboTranType.SelectedValue.ToString();

            dataTable = datapay.GetDeletedTransactions(vendorcode, vendorref, FromAccount, CustName, Paymentcode, ToAccount, fromdate, todate, TranType);

          

          //  dataTable.Columns.;

            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                string rolecode = Session["RoleCode"].ToString();

                //  ButtonColumn bt = FindControl();

                if (rolecode.Equals("004"))
                {
                    MultiView1.ActiveViewIndex = -1;


                    Form_Multiview.ActiveViewIndex = -1;
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;
                    CalculateTotal(dataTable);
                }
                Form_Multiview.ActiveViewIndex = -1;
                DataGrid1.Visible = true;
                ShowMessage(".", true);

                if (Session["UserArea"].ToString().Equals("1") && Session["RoleCode"].ToString() == "007")
                {
                    DataGrid1.Columns[14].Visible = true;
                }
                else
                {
                    DataGrid1.Columns[14].Visible = false;

                }
            }
            else
            {
                lblTotal.Text = ".";
                DataGrid1.Visible = false;
                lblTotal.Visible = false;
                MultiView1.ActiveViewIndex = -1;
                Form_Multiview.ActiveViewIndex = -1;
                ShowMessage("No Record found", true);
            }
        }
    }

    private void CalculateTotal(DataTable Table)
    {
        double total = 0;
        foreach (DataRow dr in Table.Rows)
        {
            double amount = double.Parse(dr["TranAmount"].ToString());
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
        lblTotal.Text = "Total Amount of Transactions [" + total.ToString("#,##0") + "]";
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
            if (e.CommandName == "btnEdit")
            {
                //string PegPayId = e.Item.Cells[13].Text;
                // string Amount = e.Item.Cells[9].Text;
                string st1 = e.Item.Cells[0].Text;
                string st2 = e.Item.Cells[1].Text;
                string FromAccount = e.Item.Cells[2].Text;
                string st4 = e.Item.Cells[4].Text;
                string Phone = e.Item.Cells[5].Text;//ph
                string CustName = e.Item.Cells[6].Text;//cust name
                string st7 = e.Item.Cells[7].Text;//transaction typ
                string st8 = e.Item.Cells[8].Text;
                string PaymentDate = e.Item.Cells[9].Text;//date
                string Amount = e.Item.Cells[10].Text;//amount
                string st11 = e.Item.Cells[11].Text;//reason
                string ToAccount = e.Item.Cells[12].Text;//
                string pegPayId = e.Item.Cells[13].Text;//pegpay
                string st14 = e.Item.Cells[3].Text;


                txtTelecomRef.Text = "";
                txtAmount.Text = Amount;
                txtPegPayID.Text = pegPayId;
                // txtCustName.Text = CustName;
                txtPaymentDate.Text = PaymentDate;
                txtPhone.Text = Phone;
                txtFromAccount.Text = ToAccount;
                txtToAccount.Text = st4;
                txtCustomerName.Text = CustName;

                Form_Multiview.ActiveViewIndex = 0;
                MultiView1.ActiveViewIndex = -1;
                DataGrid1.Visible = false;
                lblTotal.Visible = false;
                txtMark.Enabled = true;
                // MultiView3.ActiveViewIndex = -1;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            string vendorcode = cboVendor.SelectedValue.ToString();
            string vendorref = txtpartnerRef.Text.Trim();
            string Paymentcode = cboPaymentType.SelectedValue.ToString();
            string Account = txtAccount.Text.Trim();
            string CustName = txtCustName.Text.Trim();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            string teller = txtSearch.Text.Trim();
            string utility = cboTranType.SelectedValue.ToString();
            dataTable = datapay.GetDeletedTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, teller, fromdate, todate, utility);
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
    }
    protected void cboTranType_DataBound(object sender, EventArgs e)
    {
        cboTranType.Items.Insert(0, new ListItem("All Tran Types", "0"));
    }
    protected void cboPaymentType_DataBound(object sender, EventArgs e)
    {
        cboPaymentType.Items.Insert(0, new ListItem("All Payment Types", "0"));
    }
    protected void btnConvert_Click(object sender, EventArgs e)
    {
        //try
        //{
            ConvertToFile();
        //}
        //catch (Exception ex)
        //{
        //    ShowMessage(ex.Message, true);
        //}
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
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
                if (rdPdf.Checked.Equals(true))
                {
                    Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "TRANSACTIONS");

                }
                else
                {
                    Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "TRANSACTIONS");

                }
                        
        }
    }
    private void LoadRpt()
    {
        string vendorcode = cboVendor.SelectedValue.ToString();
        string vendorref = txtpartnerRef.Text.Trim();
        string Paymentcode = cboPaymentType.SelectedValue.ToString();
        string Account = txtAccount.Text.Trim();
        string CustName = txtCustName.Text.Trim();
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        string teller = txtSearch.Text.Trim();
        string utility = cboTranType.SelectedValue.ToString();
        dataTable = datapay.GetDeletedTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, teller, fromdate, todate, utility);
        dataTable = formatTable(dataTable);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);

        rptName = physicalPath + "\\Bin\\Reports\\TransReport.rpt";

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

    protected void DataGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = -1;
    }
    protected void txtMark_Click(object sender, EventArgs e)
    {
        DataTable DeletedRecord = new DataTable();

        try
        {
            DeletedRecord = datafile.GetDeletedTransactionToReverse(txtPegPayID.Text);

            string telecomId = txtTelecomRef.Text.Trim();
            Trans.getTelecomID = telecomId;

            if (telecomId != "")
            {

                Trans.getTranStatus = "SUCCESS";
                Trans.getSentToVendorStatus = "1";
                
            }
            else
            {
                //Trans.getTranStatus = "PENDING";
                Trans.getSentToVendorStatus = "0";
                Trans.getTranStatus = "INSERTED PUSH";
            }

            if (DeletedRecord.Rows.Count > 0)
            {


                foreach (DataRow dr in DeletedRecord.Rows)
                {
                    string TransactionCategory = dr["TransCategory"].ToString();
                    string Trantype = dr["TranType"].ToString();

                    if (Trantype == "1")
                    {
                        Trans.getSentToVendorStatus = "1";
                        if (Trantype == "1" && telecomId == "")
                        {
                            ShowMessage("PLEASE ENTER TELECOM REFERENCE", true);

                            return;
                        }
                    }

                    if (TransactionCategory == "01")
                    {
                        Trans.getFromAccount = dr["FromAccount"].ToString();
                        Trans.getToAccount = dr["ToAccount"].ToString();
                        Trans.getCustName = dr["CustName"].ToString();
                        Trans.getVendorTranId = dr["VendorTranId"].ToString();
                        Trans.getTranAmount = dr["TranAmount"].ToString();
                        Trans.getFromNetwork = dr["FromNetwork"].ToString();
                        Trans.getToNetwork = dr["ToNetwork"].ToString();
                        Trans.getPaymentDate = dr["PaymentDate"].ToString();
                        Trans.getRecordDate =DateTime.Now.ToString(); //dr["RecordDate"].ToString();
                        Trans.getPaymentType = dr["PaymentType"].ToString();
                        Trans.GetTranType = dr["TranType"].ToString();
                        Trans.getVendorCode = dr["VendorCode"].ToString();
                        Trans.getPhone = dr["Phone"].ToString();
                        Trans.getPegPayId = dr["PegPayTranId"].ToString();

                        //Trans.getSentToVendorStatus = "INSERTED PUSH";
                        Trans.getPegasusCommisionAccount = "";
                        Trans.getTranCharge = dr["TranCharge"].ToString();
                        Trans.getTelecomCommissionAccount = "";
                        Trans.getMNOCharge = dr["MNOCharge"].ToString();
                        Trans.getCashoutCharge = "0";
                        Trans.getCashoutAccount = "0";
                      

                    }
                    else if (TransactionCategory == "02")
                    {
                        Trans.getPegasusCommisionAccount = dr["ToAccount"].ToString();
                        Trans.getTranCharge = dr["TranAmount"].ToString();
                    }
                    else if (TransactionCategory == "03")
                    {
                        Trans.getTelecomCommissionAccount = dr["ToAccount"].ToString();
                        Trans.getMNOCharge = dr["TranAmount"].ToString();
                        Trans.getCashoutCharge = "0";
                        Trans.getCashoutAccount = "0";
                    }
                    else if (TransactionCategory == "04")
                    {
                        Trans.getCashoutCharge = dr["TranAmount"].ToString();
                        Trans.getCashoutAccount = dr["ToAccount"].ToString();
                    }
                    else
                    {
                    }
                }
                string Response = datapay.ReverseDeletedTransaction(Trans);
                if (Response == "1")
                {
                    ShowMessage("Transaction successfully Reversed to Recieved ", false);
                    ClearControls();
                }
                else
                {
                    ShowMessage("FAILED:   DUPLICATE RECORD", true);
                }
            }
        }
        catch (Exception exx)
        {
            ShowMessage(exx.Message, true);
        }
        //}
    }

    public void ClearControls()
    {

        txtTelecomRef.Text = "";
        txtToAccount.Text = "";
        txtPegPayID.Text = "";
        //txtCustName.Text = "";
        txtPaymentDate.Text = "";
        txtMark.Enabled = false;
        txtPhone.Text = "";
        txtAmount.Text = "";
        txtFromAccount.Text = "";
        txtCustomerName.Text = "";

    }
}
