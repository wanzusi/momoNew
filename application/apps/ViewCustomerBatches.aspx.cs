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
using InterLinkClass.PegpayMMoney;
using System.Collections.Generic;
public partial class ViewCustomerBatches : System.Web.UI.Page
{
    ProcessUsers Process = new ProcessUsers();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    PhoneValidator pv = new PhoneValidator();
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
                    DisableBtnsOnClick();
                    MultiView1.ActiveViewIndex = 0;
                    GetTypes();
                    GetBatchStatus();
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
    private void GetBatchStatus()
    {
        dataTable = datafile.GetBatchStatuses();
        cboBatchStatus.DataSource = dataTable;
        cboBatchStatus.DataValueField = "StatusCode";
        cboBatchStatus.DataTextField = "Status";
        cboBatchStatus.DataBind();
    }

    
    private void DisableBtnsOnClick()
    { 
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            LoadBatches();            
         }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void GetTypes()
    {
        dataTable = datafile.GetBeneficiaryTypes();
        cboType.DataSource = dataTable;
        cboType.DataValueField = "TypeCode";
        cboType.DataTextField = "TypeName";
        cboType.DataBind();
    }
    private void LoadBatches()
    {
        try
        {
            string Batchstatus = cboBatchStatus.SelectedValue.ToString();
            string CustomerCode = Session["CustomerCode"].ToString();
            string TypeCode = cboType.SelectedValue.ToString();
            string BatchNo = txtBatchCode.Text;
            DateTime StartDate = bll.Returnstartingdate(txtfromDate.Text.Trim());
            DateTime EndDate = bll.Returnendingdate(txttoDate.Text.Trim());
            //dataTable = datafile.GetBatchforApproval(CustomerCode, StatusCode, TypeCode, LevelCode, StartDate, EndDate);
            dataTable = datafile.GetCustomerBatches(CustomerCode, TypeCode, BatchNo, Batchstatus, StartDate, EndDate);
            if (dataTable.Rows.Count > 0)
            {
                MultiView2.ActiveViewIndex = 0;
                DataGrid1.DataSource = dataTable;
                DataGrid1.CurrentPageIndex = 0;
                DataGrid1.DataBind();
                DataGrid1.Visible = true;
                ShowMessage(".", true);
            }
            else
            {
                MultiView2.ActiveViewIndex = -1;
                ShowMessage("No File(s) found for your View", true);
                DataGrid1.Visible = false;
            }
           // LoadRejected();
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    private void LoadRejected()
    {
        string CustomerCode = Session["CustomerCode"].ToString();
        string StatusCode = "003";
        string verifier = Session["Username"].ToString();
        dataTable = datafile.GetRejectedBatches(CustomerCode, StatusCode, verifier);
        string RoleCode = Session["RoleCode"].ToString();
        if (dataTable.Rows.Count > 0 && RoleCode == "013")
        {
            MultiView3.ActiveViewIndex = 0;//rejected batches
            MultiView4.ActiveViewIndex = -1;//audit trail not allowed
            DataGrid3.DataSource = dataTable;
            DataGrid3.DataBind();
           ShowMessage(".", true);
            DataGrid3.Visible = true;
        }
        else
        {
            MultiView3.ActiveViewIndex = -1;
            MultiView4.ActiveViewIndex = -1;
            DataGrid3.Visible = false;
        }

    }
    private string GetStatus()
    {
       string output = "001";
       if (Session["RoleCode"].ToString() == "014")
        {
            output = "001";
        }
        else if (Session["RoleCode"].ToString() == "015")
        {
            output = "002";
        }
        return output;
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
                string BatchCode = e.Item.Cells[2].Text;
                string FromAccount = e.Item.Cells[3].Text;
                string Mode = e.Item.Cells[4].Text;
                lblBatchCode.Text = BatchCode;
                string status = "SUCCESS";
                tranStatusLbl.Text = status;
                //LoadBatchRecords(BatchCode);
                LoadBatchRecords1(BatchCode, status);
                //rbnApproval.SelectedIndex = rbnApproval.Items.IndexOf(rbnApproval.Items.FindByValue("0"));
            }
            else if (e.CommandName == "btnView1")
            {
                string BatchCode = e.Item.Cells[2].Text;
                string FromAccount = e.Item.Cells[3].Text;
                string Mode = e.Item.Cells[4].Text;
                lblBatchCode.Text = BatchCode;
                string status = "FAILED";
                tranStatusLbl.Text = status;
               // LoadBatchRecords(BatchCode);
                LoadBatchRecords1(BatchCode, status);
                //rbnApproval.SelectedIndex = rbnApproval.Items.IndexOf(rbnApproval.Items.FindByValue("0"));
            }
            else if (e.CommandName == "btnView2")
            {
                string BatchCode = e.Item.Cells[2].Text;
                string FromAccount = e.Item.Cells[3].Text;
                string Mode = e.Item.Cells[4].Text;
                lblBatchCode.Text = BatchCode;
                string status = "PENDING";
                tranStatusLbl.Text = status;
                //LoadBatchRecords(BatchCode);
                LoadBatchRecords1(BatchCode, status);
                //rbnApproval.SelectedIndex = rbnApproval.Items.IndexOf(rbnApproval.Items.FindByValue("0"));
            }
            else if (e.CommandName == "btnView3")
            {
                string BatchCode = e.Item.Cells[2].Text;
                string FromAccount = e.Item.Cells[3].Text;
                string Mode = e.Item.Cells[4].Text;
                lblBatchCode.Text = BatchCode;
                string status = "EXCLUDED";
                tranStatusLbl.Text = status;
                //LoadBatchRecords(BatchCode);
                LoadBatchRecords1(BatchCode, status);
                //rbnApproval.SelectedIndex = rbnApproval.Items.IndexOf(rbnApproval.Items.FindByValue("0"));
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void DataGrid3_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "btnView")
            {
                string BatchCode = e.Item.Cells[0].Text;
                LoadRejectedfrom(BatchCode);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadRejectedfrom(string BatchCode)
    {
        string Status = "003";
        dataTable = datafile.RejectedBatchComment(BatchCode, Status);
        if (dataTable.Rows.Count > 0)
        {
            string CustomerCode = Session["CustomerCode"].ToString();
            DataTable dt = datafile.GetBatchRecords(BatchCode, CustomerCode);
            DataGrid4.DataSource = dt;
            DataGrid4.DataBind();
            lblBatchCode.Text = BatchCode;
            MultiView3.ActiveViewIndex = 1;
            MultiView4.ActiveViewIndex = -1;
            ShowTotalAmount(dt);
        }
    }
    private void ShowTotalAmount(DataTable dataTable)
    {
        double total = 0;
        foreach (DataRow dr in dataTable.Rows)
        {
            double amount = Convert.ToDouble(dr["Amount"].ToString());
            total += amount;
        }
        lblShowTotal.Text = "Total(" + total.ToString("#,##0") + ")";
    }
    private void LoadBatchRecords(string BatchCode)
    {
        //deo1
        string CustomerCode=Session["CustomerCode"].ToString();
       // txtBatchTotal.Text = lblTotal.Text;
        dataTable  = datafile.GetBatchRecords(BatchCode, CustomerCode);
        MultiView3.ActiveViewIndex = -1;
        if (dataTable.Rows.Count > 0)
        {
            if (Session["RoleCode"].ToString() == "015")
            {
                MultiView1.ActiveViewIndex = 1;
                DataGrid2.DataSource = dataTable;
                DataGrid2.DataBind();
               GetTotalAmount(dataTable);
            }
            else
            {
                MultiView1.ActiveViewIndex = 1;
                DataGrid2.DataSource = dataTable;
                DataGrid2.DataBind();
                GetTotalAmount(dataTable);
                LoadLogs(BatchCode);
            }
            MultiView5.ActiveViewIndex = 0;
        }
        else
        {
            ShowMessage("Batch (" + BatchCode + ") has no record(s)", true);
        }
        
    }


   private void LoadBatchRecords1(string BatchCode, string status)
    {
        //herbert
        string CustomerCode = Session["CustomerCode"].ToString();

        List<string> ChargeType = bll.GetChargeType(CustomerCode);


        double Charge = Convert.ToDouble(ChargeType[1]);

        dataTable = datafile.GetBatchRecords1(BatchCode, CustomerCode, status,ChargeType[0],Charge);
        MultiView3.ActiveViewIndex = -1;
        if (dataTable.Rows.Count > 0)
        {
            if (Session["RoleCode"].ToString() == "015")
            {
                MultiView1.ActiveViewIndex = 1;
                DataGrid2.DataSource = dataTable;
                DataGrid2.DataBind();
                GetTotalAmount(dataTable);
            }
            else
            {
                MultiView1.ActiveViewIndex = 1;
                DataGrid2.DataSource = dataTable;
                DataGrid2.DataBind();
                GetTotalAmount(dataTable);
                LoadLogs(BatchCode);
            }
            MultiView5.ActiveViewIndex = 0;
            ShowMessage("Batch Code ( " + BatchCode + " )", false);
        }
        else
        {
            ShowMessage("Batch ( " + BatchCode + " ) has no record(s)", true);
            

        }

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
            double amount5 = Convert.ToDouble(dr["CashOutFee"].ToString().Replace(",", ""));
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

    private double CalculateTotalAmount(DataTable dataTable)
    {
        double total = 0;
        foreach (DataRow dr in dataTable.Rows)
        {
            double amount = Convert.ToDouble(dr["Amount"].ToString());
            total += amount;
        }
        return total;
    }
    private double CalculateTotalCharge(DataTable dataTable)
    {
        double TotalCharge = 0;
        foreach (DataRow dr in dataTable.Rows)
        {
            double amount = Convert.ToDouble(dr["TranCharge"].ToString());
            TotalCharge += amount;
        }
        return TotalCharge;
    }

    private void LoadLogs(string BatchCode)
    {
        DataTable dt = datafile.GetBatchLogs(BatchCode);
        if (dt.Rows.Count > 0)
        {
            MultiView4.ActiveViewIndex = 0;
           DataGrid5.DataSource = dt;
           DataGrid5.DataBind();
        }
        else
        {
            MultiView4.ActiveViewIndex = -1;
        }
    }

   
    private void DownloadBatch(string BatchCode)
    {
        dataTable = datapay.GetRevBatchDetails(BatchCode);
        if (dataTable.Rows.Count > 0)
        {
            string appPath, physicalPath, rptName;
            appPath = HttpContext.Current.Request.ApplicationPath;
            physicalPath = HttpContext.Current.Request.MapPath(appPath);

            rptName = physicalPath + "\\Bin\\Reports\\NegativeBatch.rpt";

            Rptdoc.Load(rptName);
            Rptdoc.SetDataSource(dataTable);
            CrystalReportViewer1.ReportSource = Rptdoc;
            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "REVERSALS");
            Rptdoc.Dispose();
        }
    }
    private void DownloadFile(string path, bool forceDownload)
    {
        string name = Path.GetFileName(path);
        string type = "text/plain";
        if (forceDownload)
        {
            Response.AppendHeader("content-disposition",
                "attachment; filename=" + name);
        }
        if (type != "")
            Response.ContentType = type;
        Response.WriteFile(path);
        Response.End();
        bll.RemoveFile(path);
    }
    protected void cboType_DataBound(object sender, EventArgs e)
    {
        cboType.Items.Insert(0, new ListItem("-- Select Type --", "0"));
    }
    protected void cboBatchStatus_DataBound(object sender, EventArgs e)
    {
        cboBatchStatus.Items.Insert(0, new ListItem("-- Select Status --", "0"));
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            string Batchstatus = cboBatchStatus.SelectedValue.ToString();
            string CustomerCode = Session["CustomerCode"].ToString();
            string TypeCode = cboType.SelectedValue.ToString();
            string BatchNo = txtBatchCode.Text;
            DateTime StartDate = bll.Returnstartingdate(txtfromDate.Text.Trim());
            DateTime EndDate = bll.Returnendingdate(txttoDate.Text.Trim());
            //dataTable = datafile.GetBatchforApproval(CustomerCode, StatusCode, TypeCode, LevelCode, StartDate, EndDate);
            dataTable = datafile.GetCustomerBatches(CustomerCode, TypeCode, BatchNo, Batchstatus, StartDate, EndDate);
            DataGrid1.DataSource = dataTable;
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
    protected void DataGrid2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        //herbert
        string status=tranStatusLbl.Text;
        string CustomerCode = Session["CustomerCode"].ToString();
        // txtBatchTotal.Text = lblTotal.Text;
        string BatchCode = lblBatchCode.Text;
       //dataTable = datafile.GetBatchRecords(BatchCode, CustomerCode);
        List<string> ChargeType = bll.GetChargeType(CustomerCode);


        double Charge = Convert.ToDouble(ChargeType[1]);

        dataTable = datafile.GetBatchRecords1(BatchCode, CustomerCode, status, ChargeType[0],Charge);
        MultiView3.ActiveViewIndex = -1;
        if (dataTable.Rows.Count > 0)
        {
            if (Session["RoleCode"].ToString() == "015")
            {
                MultiView1.ActiveViewIndex = 1;
                DataGrid2.DataSource = dataTable;
                DataGrid2.CurrentPageIndex = e.NewPageIndex;
                DataGrid2.DataBind();
                GetTotalAmount(dataTable);
            }
            else
            {
                MultiView1.ActiveViewIndex = 1;
                DataGrid2.DataSource = dataTable;
                DataGrid2.CurrentPageIndex = e.NewPageIndex;
                DataGrid2.DataBind();
                GetTotalAmount(dataTable);
                LoadLogs(BatchCode);
            }
            MultiView5.ActiveViewIndex = 0;
        }
        else
        {
            ShowMessage("Batch (" + BatchCode + ") has no record(s)", true);
        }
    }
    protected void btnApprovalReturn_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 0;
        }
        catch(Exception ex)
        {
            throw ex;
        }

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 0;
            ShowMessage(".", true);
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try 
        {
            ConvertToFile();
        }
        catch(Exception ex)
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
        string Batchstatus = cboBatchStatus.SelectedValue.ToString();
        string CustomerCode = Session["CustomerCode"].ToString();
        string TypeCode = cboType.SelectedValue.ToString();
        string BatchNo = txtBatchCode.Text;
        DateTime StartDate = bll.Returnstartingdate(txtfromDate.Text.Trim());
        DateTime EndDate = bll.Returnendingdate(txttoDate.Text.Trim());
        dataTable = datafile.GetCustomerBatches(CustomerCode, TypeCode, BatchNo, Batchstatus, StartDate, EndDate);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);
        rptName = physicalPath + "\\Bin\\Reports\\CustomerBatches.rpt";
        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dataTable);
        CrystalReportViewer1.ReportSource = Rptdoc;
    }
    private void ConvertToFile2()
    {
        if (rdExcel2.Checked.Equals(false) && rdPdf2.Checked.Equals(false))
        {
            ShowMessage("Please Check file format to Convert to", true);
        }
        else
        {
            LoadRpt2();
        }
    }
    private void LoadRpt2()
    {
        string status=tranStatusLbl.Text;
        string CustomerCode = Session["CustomerCode"].ToString();
        string BatchCode = lblBatchCode.Text;
        // txtBatchTotal.Text = lblTotal.Text;
       // dataTable = datafile.GetBatchRecords(BatchCode, CustomerCode);
        List<string> ChargeType = bll.GetChargeType(CustomerCode);


        double Charge = Convert.ToDouble(ChargeType[1]);

        dataTable = datafile.GetBatchRecords1(BatchCode, CustomerCode, status,ChargeType[0],Charge);
        dataTable = AddTotal(dataTable);
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
        if (rdPdf2.Checked.Equals(true))
        {
            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "PAYMENTS");

        }
        else
        {
            Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "PAYMENTS");

        }
    }

    public DataTable AddTotal(DataTable dataTable)
    {
        DataTable formedTable = new DataTable();

        DataRow dr = dataTable.NewRow();
        DataRow dr2 = dataTable.NewRow();
DataRow dr3 = dataTable.NewRow();
        List<int> List = new List<int>();
        List = GetTotal(dataTable);
        dr["BeneficaryAccount"] = "SUB TOTAL";
        dr2["BeneficaryAccount"] = "GRAND TOTAL";
        dr["Amount"] = List[0].ToString("#,##0");
        dr["TranCharge"] = List[1].ToString("#,##0");
        dr["PayOutFee"] = List[2].ToString("#,##0");
        dr["CashOutFee"] = List[3].ToString("#,##0");
        dr["TotalCharge"] = List[4].ToString("#,##0");

        dr2["TotalCharge"] = Convert.ToDouble((List[0] + List[4])).ToString("#,##0");

        dataTable.Rows.Add(dr3);
        dataTable.Rows.Add(dr);
        dataTable.Rows.Add(dr2);

        return dataTable;
    }

    private List<int> GetTotal(DataTable dataTable)
    {
        int totalAmount = 0;
        int pegasusCharge = 0;
        int MnoCharge = 0;
        int cashOutCharge = 0;
        int TotalCharge = 0;
        List<int> list = new List<int>();
        try
        {
            foreach (DataRow dr in dataTable.Rows)
            {
                totalAmount = totalAmount + Convert.ToInt32(dr["Amount"].ToString().Replace(",", ""));
                pegasusCharge = pegasusCharge + Convert.ToInt32(dr["TranCharge"].ToString().Replace(",", ""));
                MnoCharge = MnoCharge + Convert.ToInt32(dr["PayOutFee"].ToString().Replace(",", ""));
                cashOutCharge = cashOutCharge + Convert.ToInt32(dr["CashOutFee"].ToString().Replace(",", ""));
                TotalCharge = TotalCharge + Convert.ToInt32(dr["TotalCharge"].ToString().Replace(",", ""));
            }

            list.Add(totalAmount);
            list.Add(pegasusCharge);
            list.Add(MnoCharge);
            list.Add(cashOutCharge);
            list.Add(TotalCharge);
        }
        catch (Exception ex)
        {

        }
        return list;
    }
    protected void btnConvertDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ConvertToFile2();
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
}
