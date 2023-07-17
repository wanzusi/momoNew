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

public partial class GraphsBulkPayments : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    DataTable PullsMinusPushesDataTable = new DataTable();
    DataTable MonthlyPullsTotalDataTable = new DataTable();
    DataTable AddPullsDataTable = new DataTable();
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


                LoadALLNetworks();
                //MultiView1.ActiveViewIndex = -1;
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
                //lblTotal.Visible = false;
                DisableBtnsOnClick();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
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
    //private void LoadVendors()
    //{
    //    //dtable = datafile.GetAllVendors("0");
    //    dtable = datafile.GetSystemCompanies("", "");
    //    cboVendor.DataSource = dtable;
    //    cboVendor.DataValueField = "CompanyCode";
    //    cboVendor.DataTextField = "Company";
    //    cboVendor.DataBind();
    //}
    private void LoadVendors()
    {
        //dtable = datafile.GetAllVendors("0");
        dtable = datafile.GetSystemCompanies("", "");
        cboVendor.DataSource = dtable;
        cboVendor.DataValueField = "CompanyCode";
        cboVendor.DataTextField = "Company";
        cboVendor.DataBind();
        if (Session["AreaID"].ToString().Equals("1"))
        {
            cboVendor.Enabled = true;
        }
        else
        {
            string CompanyCode = Session["Company"].ToString();
            cboVendor.SelectedIndex = cboVendor.Items.IndexOf(cboVendor.Items.FindByValue(CompanyCode));
            cboVendor.Enabled = false;
        }
    }
    private void LoadALLNetworks()
    {
        dtable = datafile.GetNetworkInMobileMoneyDBNew("0");
        cboNetwork.DataSource = dtable;
        cboNetwork.DataValueField = "Network";  //Column name of the table.
        cboNetwork.DataTextField = "Network";
        cboNetwork.DataBind();
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

    private void LoadTransactions()
    {
        if (txtfromDate.Text.Equals(""))
        {
            //DataGrid1.Visible = false;
            ShowMessage("From Date is required", true);
            txtfromDate.Focus();
        }
        else if (cboNetwork.SelectedValue.ToString().Trim().Equals("0"))
        {
            string errormessage = "Pliz Select a Network from the List";
            //ShowMissingFieldMessage(errormessage);
            ShowMessage(errormessage, true);
            cboNetwork.Focus();

        }
        else if (cboVendor.SelectedValue.ToString().Trim().Equals("0"))
        {
            string errormessage = "Pliz Select an Agent from the List";
            //ShowMissingFieldMessage(errormessage);
            ShowMessage(errormessage, true);
            cboVendor.Focus();

        }

        else
        {
            string vendorcode = cboVendor.SelectedValue.ToString();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            string network = cboNetwork.SelectedValue.ToString();

            //Display Debits(PUSHES)
            dtable = datapay.GetLineGraphTransactionsDebitsToDisplay(vendorcode, network, fromdate, todate, "2");


            //Display Credits(PULLES)
            //Get The Starting/Bring Forward Balance for the Month
            string AccountDetails = datapay.GetAccountNumberDetails(vendorcode);
            dataTable = datapay.GetPullsLineGraphTransactions(vendorcode, AccountDetails, fromdate, todate);
            DataTable MonthlyStartingBalanceForVendors = GetAllPulles(dataTable);
            DataTable FinalStartingMonthlyBalanceForVendorPullsDT = MergeColumns(dataTable, MonthlyStartingBalanceForVendors);


            //Get  all the Credits for that Month
            DataTable creditdtable = datapay.GetLineGraphTransactionsCreditsToDisplay(vendorcode, network, fromdate, todate, "1");
            //Merge the two DTs
            DataTable VendorsPullsFinalDT = MergeColumns(FinalStartingMonthlyBalanceForVendorPullsDT, creditdtable);

            //DataTable to have Credit Months  in the first table Corresponding with the other table
            DataTable FinalCreditMonths = FormatPullMonths(VendorsPullsFinalDT);

            //DataTable that 
            //How do you take two datatables and match values, 
            //then add something to a column in the matched row values? 
            DataTable WithProperMonthsAndAmontsToBeAdded = DoWorkProperMonthsToBeAdded(FinalCreditMonths, creditdtable);

            DataTable AdditionDT = CalutateAdditionOfPulls(WithProperMonthsAndAmontsToBeAdded);

            DataTable FinalPullesDataTable = MergeColumns(WithProperMonthsAndAmontsToBeAdded, AdditionDT);


            //Merge Credits(PULLES) and Debits(PUSHES) DataTable
            DataTable FinalPullesAndPushesDT = MergeColumns(FinalPullesDataTable, dtable);


            //DataTable to have Credit Months Correspond with Debit Months
            DataTable Final33 = FormatDebitMonthsWithCreditMonths(FinalPullesAndPushesDT);

            //DataTable that 
            //How do you take two datatables and match values, 
            //then add something to a column in the matched row values? 
            DataTable WithProperMonthsAndAmountsToBeSubtracted = DoWork(Final33, dtable);

            DataTable SubtractionDT = CalutatePullsAndPushes(WithProperMonthsAndAmountsToBeSubtracted);

            DataTable FinalDT = MergeColumns(WithProperMonthsAndAmountsToBeSubtracted, SubtractionDT);



            DataTable dataTableReal = formatTable(FinalDT);
            string appPath, physicalPath, rptName;
            appPath = HttpContext.Current.Request.ApplicationPath;
            physicalPath = HttpContext.Current.Request.MapPath(appPath);

            rptName = physicalPath + "\\Bin\\Reports\\PullAndPushLineGraph.rpt";

            Rptdoc.Load(rptName);
            Rptdoc.SetDataSource(dataTableReal);
            CrystalReportViewer1.ReportSource = Rptdoc;
            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "TRANSACTIONS");


        }
    }


    private void ShowMessage(string Message, bool Error)
    {
        System.Web.UI.WebControls.Label lblmsg = (System.Web.UI.WebControls.Label)Master.FindControl("lblmsg");
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


    protected void cboVendor_DataBound(object sender, EventArgs e)
    {
        cboVendor.Items.Insert(0, new ListItem("All Agents", "0"));
    }

    protected void cboNetwork_DataBound(object sender, EventArgs e)
    {
        cboNetwork.Items.Insert(0, new ListItem("All Network Types", "0"));
    }

    //private void LoadRpt()
    //{
    //    string vendorcode = cboVendor.SelectedValue.ToString();
    //    string network = cboNetwork.SelectedValue.ToString();
    //    DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
    //    DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
    //    string TranType = cboTranType.SelectedValue.ToString();
    //    //dataTable = datapay.GetLineGraphTransactions(vendorcode, network, fromdate, todate, TranType);
    //   //dataTable = datapay.GetLineGraphTransactionsCreditsToDisplay(vendorcode, network, fromdate, todate, "1");
    //    //Display Debits(PUSHES)
    //   dtable = datapay.GetLineGraphTransactionsDebitsToDisplay(vendorcode, network, fromdate, todate, "2");
    //    //DataTable mergedTables = new DataTable();
    //    //mergedTables.Merge(dataTable);
    //    //mergedTables.Merge(dtable);

    //    //Display Credits(PULLES)
    //   //Get The Starting/Bring Forward Balance for the Month
    //   string AccountDetails = datapay.GetAccountNumberDetails(vendorcode);
    //   dataTable = datapay.GetPullsLineGraphTransactions(vendorcode, AccountDetails, fromdate, todate);
    //   DataTable MonthlyStartingBalanceForVendors = GetAllPulles(dataTable);
    //   DataTable FinalStartingMonthlyBalanceForVendorPullsDT = MergeColumns(dataTable, MonthlyStartingBalanceForVendors);


    //   //Get  all the Credits for that Month
    //  DataTable creditdtable = datapay.GetLineGraphTransactionsCreditsToDisplay(vendorcode, network, fromdate, todate, "1");
    //   //Merge the two DTs
    //  DataTable VendorsPullsFinalDT = MergeColumns(FinalStartingMonthlyBalanceForVendorPullsDT, creditdtable);

    //   //DataTable to have Credit Months  in the first table Corresponding with the other table
    //   DataTable FinalCreditMonths = FormatPullMonths(VendorsPullsFinalDT);

    //   //DataTable that 
    //   //How do you take two datatables and match values, 
    //   //then add something to a column in the matched row values? 
    //   DataTable WithProperMonthsToBeAdded = DoWorkProperMonthsToBeAdded(FinalCreditMonths, creditdtable);

    //   DataTable AdditionDT = CalutateAdditionOfPulls(WithProperMonthsToBeAdded);

    //   DataTable FinalPullesDataTable = MergeColumns(WithProperMonthsToBeAdded, AdditionDT);


    //   //Merge Credits(PULLES) and Debits(PUSHES) DataTable
    //   DataTable FinalPullesAndPushesDT = MergeColumns(FinalPullesDataTable, dtable);


    //    //DataTable to have Credit Months Correspond with Debit Months
    //   DataTable Final33 = FormatDebitMonthsWithCreditMonths(FinalPullesAndPushesDT);

    //   //DataTable that 
    //    //How do you take two datatables and match values, 
    //    //then add something to a column in the matched row values? 
    //   DataTable WithProperMonthsToBeSubtracted = DoWork(Final33, dtable);

    //   DataTable SubtractionDT = CalutatePullsAndPushes(WithProperMonthsToBeSubtracted);

    //   DataTable FinalDT = MergeColumns(WithProperMonthsToBeSubtracted, SubtractionDT);



    //   dataTable = formatTable(FinalDT);
    //    string appPath, physicalPath, rptName;
    //    appPath = HttpContext.Current.Request.ApplicationPath;
    //    physicalPath = HttpContext.Current.Request.MapPath(appPath);

    //    rptName = physicalPath + "\\Bin\\Reports\\PullAndPushLineGraph.rpt";

    //    Rptdoc.Load(rptName);
    //    Rptdoc.SetDataSource(dataTable);
    //    CrystalReportViewer1.ReportSource = Rptdoc;
    //    //Rptdoc.PrintToPrinter(1,true, 0,0);
    //}
    private DataTable FormatPullMonths(DataTable dt)
    {

        DataTable dtUpdated = new DataTable();
        //This gives similar schema to the new datatable
        dtUpdated = dt.Clone();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string CreditMonthsYears = dt.Rows[i]["MonthsYears"].ToString().ToUpper();
            string CreditMonth = dt.Rows[i]["CreditMonth"].ToString().ToUpper();
            if (CreditMonthsYears == CreditMonth)
            {
                double creditamount1 = double.Parse(dt.Rows[i]["MonthlyStartingBalance"].ToString());
                double creditamount2 = double.Parse(dt.Rows[i]["CreditAmount"].ToString());

            }

            else
            {
                ////////////////////
                string month = "";
                month = dt.Rows[i]["MonthsYears"].ToString();
                dt.Rows[i]["CreditMonth"] = month;
                ///////////////////////
                string amount = "0";
                dt.Rows[i]["CreditAmount"] = amount;
            }
            dtUpdated.ImportRow(dt.Rows[i]);
        }

        return dtUpdated;


    }
    private DataTable DoWorkProperMonthsToBeAdded(DataTable dt1, DataTable dt2)
    {
        DataTable dtUpdated = new DataTable();
        //This gives similar schema to the new datatable
        dtUpdated = dt1.Clone();
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            string CreditMonth = dt1.Rows[i]["CreditMonth"].ToString().ToUpper();
            for (int j = 0; j < dt2.Rows.Count; j++)
            {
                string CreditMonth2 = dt2.Rows[j]["CreditMonth2"].ToString().ToUpper();
                if (CreditMonth == CreditMonth2)
                {
                    string member = "0";
                    member = dt2.Rows[j]["CreditAmount2"].ToString();
                    dt1.Rows[i]["CreditAmount"] = member;
                }
                else
                {

                }
            }
            dtUpdated.ImportRow(dt1.Rows[i]);
        }
        return dtUpdated;
    }
    public DataTable CalutateAdditionOfPulls(DataTable dt)
    {
        AddPullsDataTable.Columns.Add("CREDITS", typeof(long));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            double MonthlyStartingBalance = double.Parse(dt.Rows[i]["MonthlyStartingBalance"].ToString());
            double creditamount = double.Parse(dt.Rows[i]["CreditAmount"].ToString());

            double finaladded = MonthlyStartingBalance + creditamount;
            DataRow dr1 = AddPullsDataTable.NewRow();
            dr1["CREDITS"] = finaladded;  //.ToString("#,##0");
            AddPullsDataTable.Rows.Add(dr1);
            AddPullsDataTable.AcceptChanges();
            //}
        }

        return AddPullsDataTable;

    }
    public DataTable GetAllPulles(DataTable dt)
    {
        MonthlyPullsTotalDataTable.Columns.Add("MonthlyStartingBalance", typeof(Int64));
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            Int64 id = int.Parse(dt.Rows[i]["RecordId"].ToString());


            Int64 Balance = datapay.GetMonthlyStartingBalance(id);
            DataRow dr1 = MonthlyPullsTotalDataTable.NewRow();
            dr1["MonthlyStartingBalance"] = Balance;  //.ToString("#,##0");
            MonthlyPullsTotalDataTable.Rows.Add(dr1);
            MonthlyPullsTotalDataTable.AcceptChanges();
            //}
        }

        return MonthlyPullsTotalDataTable;

    }
    private DataTable MergeColumns(DataTable dt1, DataTable dt2)
    {
        DataTable result = new DataTable();
        foreach (DataColumn dc in dt1.Columns)
        {
            result.Columns.Add(new DataColumn(dc.ColumnName, dc.DataType));
        }
        foreach (DataColumn dc in dt2.Columns)
        {
            result.Columns.Add(new DataColumn(dc.ColumnName, dc.DataType));
        }
        for (int i = 0; i < Math.Max(dt1.Rows.Count, dt2.Rows.Count); i++)
        {
            DataRow dr = result.NewRow();
            if (i < dt1.Rows.Count)
            {
                for (int c = 0; c < dt1.Columns.Count; c++)
                {
                    dr[c] = dt1.Rows[i][c];
                }
            }
            if (i < dt2.Rows.Count)
            {
                for (int c = 0; c < dt2.Columns.Count; c++)
                {
                    dr[dt1.Columns.Count + c] = dt2.Rows[i][c];
                }
            }
            result.Rows.Add(dr);
        }
        return result;
    }
    private DataTable DoWork(DataTable dt1, DataTable dt2)
    {
        DataTable dtUpdated = new DataTable();
        //This gives similar schema to the new datatable
        dtUpdated = dt1.Clone();
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            string MonthsYearsDebit = dt1.Rows[i]["DebitMonth"].ToString().ToUpper();
            for (int j = 0; j < dt2.Rows.Count; j++)
            {
                string MonthsYearsDebit2 = dt2.Rows[j]["DebitMonth2"].ToString().ToUpper();
                if (MonthsYearsDebit == MonthsYearsDebit2)
                {
                    string member = "0";
                    member = dt2.Rows[j]["DEBITS2"].ToString();
                    dt1.Rows[i]["DEBITS"] = member;
                }
                else
                {

                }
            }
            dtUpdated.ImportRow(dt1.Rows[i]);
        }
        return dtUpdated;
    }
    private DataTable FormatDebitMonthsWithCreditMonths(DataTable dt)
    {
        //PullsMinusPushesDataTable.Columns.Add("Amount", typeof(String));

        DataTable dtUpdated = new DataTable();
        //This gives similar schema to the new datatable
        dtUpdated = dt.Clone();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string CreditMonthsYears = dt.Rows[i]["MonthsYears"].ToString().ToUpper();
            string DebitMonthsYears = dt.Rows[i]["DebitMonth"].ToString().ToUpper();
            if (CreditMonthsYears == DebitMonthsYears)
            {
                double creditamount = double.Parse(dt.Rows[i]["MonthlyStartingBalance"].ToString());
                double debitamount = double.Parse(dt.Rows[i]["DEBITS"].ToString());

            }

            else
            {
                ////////////////////
                string month = "";
                month = dt.Rows[i]["MonthsYears"].ToString();
                dt.Rows[i]["DebitMonth"] = month;
                ///////////////////////
                string member = "0";
                //member = Table.Rows[i]["MonthlyStartingBalance"].ToString();
                //double newVal = oldVal;
                dt.Rows[i]["DEBITS"] = member;
            }
            dtUpdated.ImportRow(dt.Rows[i]);
        }

        return dtUpdated;


    }
    //private DataTable RemoveGaps(DataTable Table)
    //{
    //    //PullsMinusPushesDataTable.Columns.Add("Amount", typeof(String));

    //    DataTable dtUpdated = new DataTable();
    //    //This gives similar schema to the new datatable
    //    dtUpdated = Table.Clone();

    //    for (int i = 0; i < Table.Rows.Count; i++)
    //    {
    //    string CreditAmount = Table.Rows[i]["CreditAmount"].ToString();
    //         if (string.IsNullOrEmpty(CreditAmount)) 
    //         {
    //             ////////////////////
    //             string month = "";
    //             month = Table.Rows[i]["DebitMonth"].ToString();
    //             Table.Rows[i]["CreditMonth"] = month;
    //             ///////////////////////
    //             string member = "";
    //             member=Table.Rows[i - 1]["CreditAmount"].ToString();
    //             //double newVal = oldVal;
    //             Table.Rows[i]["CreditAmount"] = member;

    //         }

    //         else
    //         {
    //             double creditamount = double.Parse(Table.Rows[i]["CreditAmount"].ToString());
    //             double debitamount = double.Parse(Table.Rows[i]["DebitAmount"].ToString());

    //                 // double finalsubtracted = creditamount - debitamount;
    //                 // DataRow dr1 = PullsMinusPushesDataTable.NewRow();
    //                 //dr1["Amount"] = finalsubtracted.ToString("#,##0");
    //                 //PullsMinusPushesDataTable.Rows.Add(dr1);
    //                 //PullsMinusPushesDataTable.AcceptChanges();
    //             //}
    //         }
    //         dtUpdated.ImportRow(Table.Rows[i]); 
    //         }

    //    return dtUpdated;
    //}

    public DataTable CalutatePullsAndPushes(DataTable dt)
    {
        PullsMinusPushesDataTable.Columns.Add("BALANCES", typeof(long));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            double creditamount = double.Parse(dt.Rows[i]["CREDITS"].ToString());
            double debitamount = double.Parse(dt.Rows[i]["DEBITS"].ToString());

            double finalsubtracted = creditamount - debitamount;
            DataRow dr1 = PullsMinusPushesDataTable.NewRow();
            dr1["BALANCES"] = finalsubtracted;  //.ToString("#,##0");
            PullsMinusPushesDataTable.Rows.Add(dr1);
            PullsMinusPushesDataTable.AcceptChanges();
            //}
        }

        return PullsMinusPushesDataTable;

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
            Header = agent_name + " RUNNING BALANCES BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
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
