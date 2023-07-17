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

public partial class PushTelecomLineGraph : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    DataTable PullsMinusPushesDataTable = new DataTable();
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
            //MTN PUSHES(DEBITS) FOR PARTICULAR VENDOR.
            dataTable = datapay.GetLineGraphTransactionsMTNDebitsToDisplay(vendorcode, fromdate, todate, "2");
            //AIRTEL PUSHES(DEBITS) FOR PARTICULAR VENDOR.
            dtable = datapay.GetLineGraphTransactionsAIRTELDebitsToDisplay(vendorcode, fromdate, todate, "2");

            //Merge the two tables(Mtn and Airtel DataTable Columns)
            DataTable ALLTelecoms = MergeColumns(dataTable, dtable);

            //Format months in MTN with Months in Airtel.
            DataTable withProperTelecomMonths = FormatTelecomsMonths(ALLTelecoms);

            //Get a DataTable with Proper Months in MTN and Airtel with Amounts
            DataTable FinalDT2 = DoWork(ALLTelecoms, dtable);


            dataTable = formatTable(FinalDT2);
            string appPath, physicalPath, rptName;
            appPath = HttpContext.Current.Request.ApplicationPath;
            physicalPath = HttpContext.Current.Request.MapPath(appPath);

            rptName = physicalPath + "\\Bin\\Reports\\PushTelecomsLineGraphCrystalReport.rpt";

            Rptdoc.Load(rptName);
            Rptdoc.SetDataSource(dataTable);
            CrystalReportViewer1.ReportSource = Rptdoc;

            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "TRANSACTIONS");
           
        }
    }

   
    private void LoadUsers()
    {

        //DataGrid1.DataSource = dataTable;
        //DataGrid1.DataBind();
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
   
    private DataTable DoWork(DataTable dt1, DataTable dt2)
    {
        DataTable dtUpdated = new DataTable();
        //This gives similar schema to the new datatable
        dtUpdated = dt1.Clone();
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            string DebitMTNMonth = dt1.Rows[i]["DebitMTNMonth"].ToString().ToUpper();
            for (int j = 0; j < dt2.Rows.Count; j++)
            {
                string DebitAIRTELMonth2 = dt2.Rows[j]["DebitAIRTELMonth2"].ToString().ToUpper();
                if (DebitMTNMonth == DebitAIRTELMonth2)
                {
                    string member = "0";
                    member = dt2.Rows[j]["DebitAIRTELAmounts2"].ToString();
                    dt1.Rows[i]["DebitAIRTELAmounts"] = member;
                }
                else
                {

                }
            }
            dtUpdated.ImportRow(dt1.Rows[i]);
        }
        return dtUpdated;
    }
    private DataTable FormatTelecomsMonths(DataTable dt)
    {

        DataTable dtUpdated = new DataTable();
        //This gives similar schema to the new datatable
        dtUpdated = dt.Clone();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string DebitMTNMonths = dt.Rows[i]["DebitMTNMonth"].ToString().ToUpper();
            string DebitAIRTELMonths = dt.Rows[i]["DebitAIRTELMonth"].ToString().ToUpper();
            if (DebitMTNMonths == DebitAIRTELMonths)
            {
                //double creditamount1 = double.Parse(dt.Rows[i]["MonthlyStartingBalance"].ToString());
                //double creditamount2 = double.Parse(dt.Rows[i]["CreditAmount"].ToString());

            }

            else
            {
                ////////////////////
                string month = "";
                month = dt.Rows[i]["DebitMTNMonth"].ToString();
                dt.Rows[i]["DebitAIRTELMonth"] = month;
                ///////////////////////
                string amount = "0";
                dt.Rows[i]["DebitAIRTELAmounts"] = amount;
            }
            dtUpdated.ImportRow(dt.Rows[i]);
        }

        return dtUpdated;


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
            Header = agent_name + " DEBIT TELECOM TRANSACTION(S) BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
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
