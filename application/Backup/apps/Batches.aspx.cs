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
public partial class Batches : System.Web.UI.Page
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
                //LoadPaymentNums();
                //if (Session["AreaID"].ToString().Equals("3"))
                //{
                //    paymentnum.SelectedIndex = paymentnum.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                //    paymentnum.Enabled = false;
                //}
                //LoadTranType();
                //LoadALLNetworks();
                LoadStatus();
                if (Session["AreaID"].ToString().Equals("2"))
                {
                    status.SelectedIndex = status.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                    status.Enabled = false;
                }
                //LoadBeneficiaryAccounts();
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
    private void LoadStatus()
    {
        dtable = datafile.GetTransferBatchStatus();

        status.DataSource = dtable;
        status.DataValueField = "Status";
        status.DataTextField = "Status";
        status.DataBind();
    }
    //private void LoadALLNetworks()
    //{
    //    dtable = datafile.GetNetworkInMobileMoneyDB();
    //    cboNetwork.DataSource = dtable;
    //    cboNetwork.DataValueField = "Network";  //Column name of the table.
    //    cboNetwork.DataTextField = "Network";
    //    cboNetwork.DataBind();
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
    //private void LoadPaymentNums()
    //{
    //    //dtable = datafile.GetAllVendors("0");

    //    dtable = datafile.GetPaymentNumbers();
    //    paymentnum.DataSource = dtable;
    //    paymentnum.DataValueField = "PaymentNo";
    //    //paymentnum.DataTextField = "Company";
    //    paymentnum.DataBind();
    //}

    //
    //private void LoadBeneficiaryAccounts()
    //{
    //    dtable = datafile.GetBeneficiaryAccounts();
    //    beneficiaryAc.DataSource = dtable;
    //    beneficiaryAc.DataValueField = "BeneficaryAccount";
    //    //beneficiaryAc.DataTextField = "PaymentType";
    //    beneficiaryAc.DataBind();
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
        if (fromDate.Text.Equals(""))
        {
            DataGrid1.Visible = false;
            ShowMessage("From Date is required", true);
            fromDate.Focus();
        }
        else
        {
            //variables
            DateTime from = bll.ReturnDate(fromDate.Text.Trim(), 1);
            DateTime to = bll.ReturnDate(toDate.Text.Trim(), 2);
            string bacthNum = batchNo.Text.Trim();
            string recorded = recordedBy.Text.Trim();
            string stat = status.SelectedValue.ToString();
            string customerCod = customerCode.Text.ToString();
            dataTable = datapay.GetTransferBatchDetails(bacthNum, recorded, from, to, stat, customerCod);
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
        foreach (DataRow dr in Table.Rows)
        {
            double amount = double.Parse(dr["RecordId"].ToString());
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
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            //Variables
            string batchNum = batchNo.Text.Trim();
            string recorded = recordedBy.Text.Trim();
            DateTime from = bll.ReturnDate(fromDate.Text.Trim(), 1);
            DateTime to = bll.ReturnDate(toDate.Text.Trim(), 2);
            string stat = status.SelectedIndex.ToString();
            string custcode = customerCode.Text.ToString();
            dataTable = datapay.GetTransferBatchDetails(batchNum, recorded, from, to, stat, custcode);
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

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
            try
            {
                if (rdPdf.Checked.Equals(true))
                {
                    Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "TRANSFER BATCH DETAILS");

                }
                else
                {
                    Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "TRANSFER BATCH DETAILS");

                }
            }
            catch (Exception ex)
            {
                //    //do nothing
            }
        }
    }
    private void LoadRpt()
    {
        //variables
        string bactNum = batchNo.Text.Trim();
        string recorded = recordedBy.Text.Trim();
        //variables
        DateTime from = bll.ReturnDate(fromDate.Text.Trim(), 1);
        DateTime to = bll.ReturnDate(toDate.Text.Trim(), 2);
        string stat = status.SelectedIndex.ToString();
        string custcode = customerCode.Text.ToString();
        //end of variables

        dataTable = datapay.GetTransferBatchDetails(bactNum, recorded, from, to, stat, custcode);
        dataTable = formatTable(dataTable);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);


        rptName = physicalPath + "\\Bin\\Reports\\TransferBatchDetails.rpt";

        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dataTable);
        CrystalReportViewer1.ReportSource = Rptdoc;
        //Rptdoc.PrintToPrinter(1,true, 0,0);
    }

    protected void status_DataBound(object sender, EventArgs e)
    {
        status.Items.Insert(0, new ListItem("All Status", "0"));
    }

    private DataTable formatTable(DataTable dataTable)
    {
        DataTable formedTable;

        DateTime fromdate = bll.ReturnDate(fromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(toDate.Text.Trim(), 2);

        string agent_code = "";//paymentnum.SelectedValue.ToString();
        string agent_name = "";//paymentnum.SelectedItem.ToString();
        string Header = "";
        if (agent_code.Equals("0"))
        {
            Header = "ALL ALL TRANSFERBATCHE(S) DETAILS BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
        }
        else
        {
            Header = agent_name + " BATCHE(S) BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
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
