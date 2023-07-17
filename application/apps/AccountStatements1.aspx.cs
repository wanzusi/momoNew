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
                if (Session["AreaID"].ToString().Equals("1"))
                {
                    cboVendor.SelectedIndex = cboVendor.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                    cboVendor.Enabled = true;
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
            if (txtfromDate.Text.Equals(""))
            {
                //DataGrid1.Visible = false;
                ShowMessage("From Date is required", true);
                txtfromDate.Focus();
            }
            else if(txttoDate.Text.Equals(""))
            {
                ShowMessage("End Date is required", true);
                txttoDate.Focus();
            }
            else
            {
                DataTable dtable = new DataTable();
                dtable = LoadBatchStatement();
                if (dtable.Rows.Count>0)
                {
                    lblAccountNo.Text = Session["CustomerPegasusAccount"].ToString();
                    LblContact.Text = Session["CustomerEmail"].ToString();
                    lblName.Text = Session["CustomerName"].ToString();
                    MultiView1.ActiveViewIndex = 0;
                    DataGrid1.DataSource = dtable;
                    DataGrid1.CurrentPageIndex = 0;
                    DataGrid1.DataBind();
                    ShowMessage("", false);
                }
                else
                {
                    ShowMessage("No Statement Details Found", true);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private DataTable LoadBatchStatement()
    {
        try
        {
            DataTable statment = GetBatchSatementTable();
            string vendorcode = cboVendor.SelectedValue.ToString();
            ArrayList content = new ArrayList();
            ArrayList succesMainTran = new ArrayList();
            ArrayList FailedTran = new ArrayList();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            string accNo = datapay.GetAccountNo(vendorcode);
            lblCloseBal.Text = "";
            DataTable CLTable = new DataTable();
            dataTable = datapay.GetBatchStatement(vendorcode, accNo, fromdate, todate);
            if (dataTable.Rows.Count > 0)
            {
                CLTable = datapay.PeriodOpeningandClosingBalance(accNo, fromdate, todate);
                Statement balancerecord = getBalanceRecord(CLTable);
                double Bal =Convert.ToDouble(balancerecord.Balance.Trim());
                lblOpenBal.Text = Bal.ToString("#,##0");
                content.Add(balancerecord);
                foreach (DataRow dr in dataTable.Rows)
                {
                    string type = dr["TranType"].ToString();
                    string Description = dr["Description"].ToString();
                    string BatchCode = dr["BatchCode"].ToString();
                    string Amount = dr["Amount"].ToString();
                     string BalAfter = dr["BalanceAfter"].ToString();
                    DateTime Date = DateTime.Parse(dr["Date"].ToString());
                    if (type.Trim().Equals("DEBITENTRY"))
                    {
                        succesMainTran = GetBatchTotal(BatchCode, Description, Date.ToString("dd/MM/yyyy"), Bal);
                        Bal = Convert.ToDouble(lblRunningBal.Text.Trim());
                        for (int i = 0; i < succesMainTran.Count; i++)
                        {
                            content.Add(succesMainTran[i]);
                        }
                    }
                    else
                    {
                        Statement accountCredit = new Statement();
                        double amt = Convert.ToDouble(Amount.Trim());
                        //double balance = Convert.ToDouble(BalAfter.Trim());
                        //double balance = Convert.ToDouble(lblRunningBal.Text.Trim()) + amt;
                        double balance = Bal + amt;
                        Bal = balance;
                        lblRunningBal.Text = Bal.ToString();
                        accountCredit.Balance = balance.ToString("#,##0");
                        accountCredit.BatchNo = BatchCode;
                        accountCredit.Credit = amt.ToString("#,##0");
                        accountCredit.Debit = "";
                        accountCredit.Description = Description;
                        accountCredit.No = "";
                        accountCredit.Type = type;
                        accountCredit.ValueDate = Date.ToString("dd/MM/yyyy");
                        content.Add(accountCredit);
                    }                    
                }

                if (content.Count > 0)
                {
                    Statement stm = new Statement();
                    int no = content.Count;
                    for (int i = 0; i < content.Count; i++)
                    {
                        stm = (Statement)content[i];
                        DataRow dr = statment.NewRow();
                        dr["No."] = i + 1;
                        dr["BatchNo"] = stm.BatchNo;
                        dr["ValueDate"] = stm.ValueDate;
                        dr["Description"] = stm.Description;
                        dr["Credit"] = stm.Credit;
                        dr["Debit"] = stm.Debit;
                        dr["Balance"] = stm.Balance;
                        dr["Type"] = stm.Type;
                        statment.Rows.Add(dr);
                        statment.AcceptChanges();
                        if (i == (no - 1))
                        {
                            lblCloseBal.Text = stm.Balance;
                        }
                    }
                }
                else
                {
                    return statment;
                }
            }
            else
            {
                return statment;
            }
            return statment;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private ArrayList GetBatchTotal(string BatchCode, string Description, string Date,double bal)
    {
        ArrayList content=new ArrayList();
        try
        {            
            DataTable dtmaincontent = new DataTable();
            DataTable dtfailedmaincontent = new DataTable();
            DataTable dtFailedcharge=new DataTable();
            DataTable dtmaincharge =new DataTable();
            DataTable dtCashoutcharge =new DataTable();
            dtmaincontent = datapay.GetBatchAmountTotal(BatchCode, "ALL","MAIN");
            dtfailedmaincontent = datapay.GetBatchAmountTotal(BatchCode, "FAILED", "MAIN");
            dtmaincharge = datapay.GetBatchAmountTotal(BatchCode, "ALL", "CHARGE");
            dtFailedcharge = datapay.GetBatchAmountTotal(BatchCode, "FAILED", "CHARGE");
            string Amount = "";
            if (dtmaincontent.Rows.Count>0)
            {
                Statement line = new Statement();
                Amount = dtmaincontent.Rows[0]["Amount"].ToString();
                double amt = Convert.ToDouble(Amount);
                double bl = bal - amt;
                if (!amt.Equals(0))
                {                   
                    line.Balance = bl.ToString("#,##0");
                    line.BatchNo = BatchCode;
                    line.Credit = "";
                    line.Debit = amt.ToString("#,##0");
                    line.Description = Description;
                    line.No = "";
                    line.Type = "DEBIT";
                    line.ValueDate = Date;
                    content.Add(line);
                    bal = bl;
                }
            }

            if (dtmaincharge.Rows.Count > 0)
            {                
                Statement line2 = new Statement();
                Amount = dtmaincharge.Rows[0]["Amount"].ToString();
                double amt = Convert.ToDouble(Amount);
                double bl = bal - amt;
                if (!amt.Equals(0))
                {
                    line2.Balance = bl.ToString("#,##0");
                    line2.BatchNo = BatchCode;
                    line2.Credit = "";
                    line2.Debit = amt.ToString("#,##0");
                    line2.Description = Description + " Charges";
                    line2.No = "";
                    line2.Type = "CHARGEDEBIT";
                    line2.ValueDate = Date;
                    content.Add(line2);
                    bal = bl;
               }
                
            }

            if (dtfailedmaincontent.Rows.Count > 0)
            {              
                Statement line3 = new Statement();
                Amount = dtfailedmaincontent.Rows[0]["Amount"].ToString();
                double amt = Convert.ToDouble(Amount);
                double bl = bal + amt;
                if (!amt.Equals(0))
                {
                    line3.Balance = bl.ToString("#,##0");
                    line3.BatchNo = BatchCode;
                    line3.Credit = amt.ToString("#,##0");
                    line3.Debit = "";
                    line3.Description = Description + " Reversal";
                    line3.No = "";
                    line3.Type = "CREDIT";
                    line3.ValueDate = Date;
                    content.Add(line3);
                    bal = bl;
                }
            }

            if (dtFailedcharge.Rows.Count>0)
            {
                Statement line4 = new Statement();
                Amount = dtFailedcharge.Rows[0]["Amount"].ToString();
                double amt = Convert.ToDouble(Amount);
                double bl = bal + amt;
                if (!amt.Equals(0))
                {
                    line4.Balance = bl.ToString("#,##0");
                    line4.BatchNo = BatchCode;
                    line4.Credit = amt.ToString("#,##0");
                    line4.Debit = "";
                    line4.Description = Description + " Charges Reversals";
                    line4.No = "";
                    line4.Type = "CHARGECREDIT";
                    line4.ValueDate = Date;
                    content.Add(line4);
                    bal = bl;
                }
            }
            lblRunningBal.Text = bal.ToString();
            return content;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    private Statement getBalanceRecord(DataTable CLTable)
    {
        Statement Bal=new Statement();
        try
        {
            if (CLTable.Rows.Count>0)
            {
                string Amount=CLTable.Rows[0]["BalanceBefore"].ToString();
                double amt = Convert.ToDouble(Amount.Trim());
                Bal.Balance = amt.ToString("#,##0") ;
                Bal.BatchNo ="" ;
                Bal.Credit="";
                Bal.Debit="";
                Bal.Description="Balance as of beginning of period";
                Bal.No="";
                Bal.Type="OPENING";
                Bal.ValueDate="";
            }
            return Bal;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetBatchSatementTable()
    {
        DataTable dt = new DataTable("SatementTable");
        dt.Columns.Add("No.");
        dt.Columns.Add("BatchNo");
        dt.Columns.Add("ValueDate");
        dt.Columns.Add("Description");
        dt.Columns.Add("Credit");
        dt.Columns.Add("Debit");
        dt.Columns.Add("Balance");
        dt.Columns.Add("Type");

        return dt;
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
        try
        {
            if (e.CommandName == "btnView")
            {
                
                string Type = e.Item.Cells[0].Text;
                if (!Type.Equals("CREDITENTRY"))
                {
                    string BatchNo = e.Item.Cells[1].Text;
                    lblBatchCode.Text = BatchNo;
                    lblType.Text = Type;
                    dataTable = datapay.GetBatchTransactions(BatchNo, Type);
                    MultiView1.ActiveViewIndex = 1;
                    DataGrid2.DataSource = dataTable;
                    DataGrid2.CurrentPageIndex = 0;
                    DataGrid2.DataBind();
                    DataGrid2.Visible = true;
                }
                else
                {
                    ShowMessage("Account Credit does not have Batch Details",true);
                }
            }
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            dtable = LoadBatchStatement();
            MultiView1.ActiveViewIndex = 0;
            DataGrid1.DataSource = dtable;
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataBind();
            ShowMessage("", false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
    protected void DataGrid2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            dataTable = datapay.GetBatchTransactions(lblBatchCode.Text, lblType.Text);
            MultiView1.ActiveViewIndex = 1;
            DataGrid2.DataSource = dtable;
            DataGrid2.CurrentPageIndex = e.NewPageIndex;
            DataGrid2.DataBind();
            DataGrid2.Visible = true;
            ShowMessage("", false);
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
    //protected void btnConvert_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ConvertToFile();
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, true);
    //    }
    //}

    //private void ConvertToFile()
    //{
    //    if (rdExcel.Checked.Equals(false) && rdPdf.Checked.Equals(false))
    //    {
    //        ShowMessage("Please Check file format to Convert to", true);
    //    }
    //    else
    //    {
    //        LoadRpt();
    //        if (rdPdf.Checked.Equals(true))
    //        {
    //            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "STATEMENTS");

    //        }
    //        else
    //        {
    //            Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "STATEMENTS");

    //        }
    //    }
    //}
  

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
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtable = new DataTable();
            dtable = LoadBatchStatement();
            DataTable dtExport = GetExportTable();
            dtExport = GetExportData(dtable);
            string appPath, physicalPath, rptName;
            appPath = HttpContext.Current.Request.ApplicationPath;
            physicalPath = HttpContext.Current.Request.MapPath(appPath);

            rptName = physicalPath + "\\Bin\\Reports\\AccountStatement2.rpt";

            Rptdoc.Load(rptName);
            Rptdoc.SetDataSource(dtExport);
            CrystalReportViewer1.ReportSource = Rptdoc;
            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "STATEMENT");
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private DataTable GetExportData(DataTable dtable)
    {
        try
        {
            DataTable dtExport = GetExportTable();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            string Range = "Period: " + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "";
            string printedBy = Session["FullName"].ToString();
            string VendorCode = "VendorCode: " + Session["CompanyCode"].ToString();
            string CompanyName = Session["CustomerName"].ToString();
            string Openbal = "OpeningBalance: " + lblOpenBal.Text;
            string ClosingBal = "ClosingBalance: " + lblCloseBal.Text;
            string Account = "FOR ACCOUNT NUMBER: " + Session["CustomerPegasusAccount"].ToString();

            foreach (DataRow dr in dtable.Rows)
            {
                DataRow newdr = dtExport.NewRow();
                newdr["Number1"] = dr["No."].ToString();
                newdr["VendorTranId"] = dr["BatchNo"].ToString();
                newdr["Date1"] = dr["ValueDate"].ToString();
                newdr["Str1"] = dr["Description"].ToString();
                newdr["Amount1"] = dr["Credit"].ToString();
                newdr["Amount2"] = dr["Debit"].ToString();
                newdr["Str2"] = dr["Balance"].ToString();
                newdr["DateRange"] = Range;
                newdr["PrintedBy"] = printedBy;
                newdr["CustomerRef"] = VendorCode;
                newdr["CustomerName"] = CompanyName;
                newdr["UtilityCode"] = Account;
                newdr["Teller"] = Openbal;
                newdr["Number2"] = ClosingBal;
                dtExport.Rows.Add(newdr);
                dtExport.AcceptChanges();
            }
            return dtExport;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable GetExportTable()
    {
            DataTable dt = new DataTable("SatementTable");
            dt.Columns.Add("VendorTranId");
            dt.Columns.Add("CustomerRef");
            dt.Columns.Add("CustomerName");
            dt.Columns.Add("Amount");
            dt.Columns.Add("PayDate");
            dt.Columns.Add("PostDate");
            dt.Columns.Add("TranType");
            dt.Columns.Add("PaymentType");
            dt.Columns.Add("Teller");
            dt.Columns.Add("Vendor");
            dt.Columns.Add("DateRange");
            dt.Columns.Add("PrintedBy");
            dt.Columns.Add("TranId");
            dt.Columns.Add("ReconciledBy");
            dt.Columns.Add("Date1");
            dt.Columns.Add("Date2");
            dt.Columns.Add("Str1");
            dt.Columns.Add("Str2");
            dt.Columns.Add("Amount1");
            dt.Columns.Add("Amount2");
            dt.Columns.Add("Number1");
            dt.Columns.Add("Number2");
            dt.Columns.Add("UtilityCode");

            return dt;
        
    }
    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
        }
        catch(Exception ex)
        {
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 0;
            
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }
}
