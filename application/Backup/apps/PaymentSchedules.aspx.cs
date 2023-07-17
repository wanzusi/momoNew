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
public partial class PaymentSchedules : System.Web.UI.Page
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
                     LoadCustomerAccountTypes();
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
    private void LoadCustomerAccountTypes()
    {
        dataTable = datafile.GetCustomerAccountsTypes();
        if (dataTable.Rows.Count > 0)
        {
            cboFromAccount.DataSource = dataTable;
            cboFromAccount.DataValueField = "AccountTypeCode";
            cboFromAccount.DataTextField = "AccountType";
            cboFromAccount.DataBind();
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
            string ScheduleStatus = cboScheduleStatus.SelectedValue.ToString();
            bool paused = false;
            string StatusCode = "";
            if(ScheduleStatus.Equals("1"))
            {
                paused = true;
            }
            else if(ScheduleStatus.Equals("2"))
            {
                 StatusCode = "005";
            }
            string CustomerCode = Session["CustomerCode"].ToString();
            string TypeCode = cboType.SelectedValue.ToString();
            string BatchNo = txtBatchCode.Text;
            DateTime StartDate = bll.Returnstartingdate(txtfromDate.Text.Trim());
            DateTime EndDate = bll.Returnendingdate(txttoDate.Text.Trim());
            dataTable = datafile.GetScheduledBatches(CustomerCode, TypeCode, BatchNo, StatusCode, paused ,StartDate, EndDate);
            if (dataTable.Rows.Count > 0)
            {
                DataGrid1.DataSource = dataTable;
                DataGrid1.DataBind();
                DataGrid1.CurrentPageIndex =0;
                DataGrid1.Visible = true;
                ShowMessage(".", true);
            }
            else
            {
                ShowMessage("No Payment Schedule(s) found for your Search", true);
                DataGrid1.Visible = false;
            }
        }
        catch(Exception ex)
        {
            throw ex;
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
                LoadBatchRecords(BatchCode);
                //rbnApproval.SelectedIndex = rbnApproval.Items.IndexOf(rbnApproval.Items.FindByValue("0"));
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
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
       // lblShowTotal.Text = "Total(" + total.ToString("#,##0") + ")";
    }
    private void LoadBatchRecords(string BatchCode)
    {
        //deo1
        string CustomerCode=Session["CustomerCode"].ToString();
       // txtBatchTotal.Text = lblTotal.Text;
        dataTable  = datafile.GetBatchRecords(BatchCode, CustomerCode);
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
        }
        else
        {
            ShowMessage("Batch (" + BatchCode + ") has no record(s)", true);
        }
        
    }

    private void GetTotalAmount(DataTable dataTable)
    {
        double total = 0;
        foreach (DataRow dr in dataTable.Rows)
        {
            double amount = Convert.ToDouble(dr["Amount"].ToString());
            total += amount;
        }
        lblTotal.Text = "Total(" + total.ToString("#,##0") + ")";
        txtBatchTotal.Text = total.ToString("#,##0");
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
    protected void cboFromAccount_DataBound(object sender, EventArgs e)
    {
        cboFromAccount.Items.Insert(0, new ListItem("-- Select --", "0"));
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            string ScheduleStatus = cboScheduleStatus.SelectedValue.ToString();
            bool paused = false;
            string StatusCode = "";
            if (ScheduleStatus.Equals("1"))
            {
                paused = true;
            }
            else if (ScheduleStatus.Equals("2"))
            {
                StatusCode = "005";
            }
            string CustomerCode = Session["CustomerCode"].ToString();
            string TypeCode = cboType.SelectedValue.ToString();
            string BatchNo = txtBatchCode.Text;
            DateTime StartDate = bll.Returnstartingdate(txtfromDate.Text.Trim());
            DateTime EndDate = bll.Returnendingdate(txttoDate.Text.Trim());
            dataTable = datafile.GetScheduledBatches(CustomerCode, TypeCode, BatchNo, StatusCode, paused, StartDate, EndDate);
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
        try
        {
            string CustomerCode = Session["CustomerCode"].ToString();
            string BatchCode=lblBatchCode.Text;
            dataTable = datafile.GetBatchRecords(BatchCode, CustomerCode);
            DataGrid2.DataSource = dataTable;
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid2.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
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
    protected void cboFromAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string accountType = cboFromAccount.SelectedValue.ToString();
            if (accountType.Equals("A001"))
            {
                string PegasusAccount = Session["CustomerPegasusAccount"].ToString();
                string CustomerCode = Session["CustomerCode"].ToString();
                string Balance = AccountBal(CustomerCode, PegasusAccount);
                double Bal = Convert.ToDouble(Balance);
                txtAccountBalance.Text = Bal.ToString("#,##0");
            }
            else
            {
                //retrive Account balance for Telecoms
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private string AccountBal(string CustomerCode, string Account)
    {
        string output = "";
        try
        {
            if (cboFromAccount.SelectedIndex != 0)
            {
                string accountNumber = Account;
                dataTable = datafile.GetCustomerAccountInfor(CustomerCode, Account);
                if (dataTable.Rows.Count > 0)
                {
                    output = dataTable.Rows[0]["AccountBalance"].ToString();
                }
                else
                {
                    ShowMessage("Failed To Account Retrive Account Information ", true);
                }
            }
            else
            {
                ShowMessage("PLEASE SELECT AN ACCOUNT TO PAY FROM", true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
        return output;
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
    protected void rbnApproval_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string status = GetSceheduleStatus();
            if (status.Equals("2"))
            {
                MultiView2.ActiveViewIndex = 0;
            }
            else
            {
                MultiView2.ActiveViewIndex = -1;
            }
        }
        catch (Exception ex)
        {
        }
    }
    private string GetSceheduleStatus()
    {
        string status = "0";
        foreach (ListItem lst in rbnSchedule.Items)
        {
            if (lst.Selected == true)
            {
                status = lst.Value;
            }
        }
        return status;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            string batchCode = lblBatchCode.Text;
            dataTable = datafile.GetBatchDetailsByCode(batchCode);
            if (dataTable.Rows.Count>0)
            {
                string fromAccount = dataTable.Rows[0]["FromAccount"].ToString();
                cboFromAccount.SelectedIndex = cboFromAccount.Items.IndexOf(cboFromAccount.Items.FindByValue(fromAccount));
                MultiView1.ActiveViewIndex = 2;
                ShowMessage(".",false);
            }
            else
            {
                ShowMessage("Failed to Retrive Batch Details", true);
            }
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message, true); 
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 1;
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            ProcessSchedule();
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }

    private void ProcessSchedule()
    {
        try
        {
            string schedulestatus = GetSceheduleStatus();
            string fromAccount = cboFromAccount.SelectedValue.ToString();
            string BatchCode = lblBatchCode.Text;
            if (schedulestatus.Equals("2"))
            {
                ProcessReschedule(schedulestatus); 
            }
            else if (schedulestatus.Equals("1"))
            {
                //send batch immidiatelly
            }
            else if(schedulestatus.Equals("3"))
            {
                //cancel batch
            }
            else if (schedulestatus.Equals("4"))
            {
                //pause batch
            }
            else
            {
                ShowMessage("Select Action to Perform",true);
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    private void ProcessReschedule(string schedulestatus)
    {
        try
        {
            string fromAccount = cboFromAccount.SelectedValue.ToString();
            string Date = txtScheduleDate.Text;
            string hour = cboHour.SelectedValue.ToString();
            string min = cbomin.SelectedValue.ToString();
            string dayState = cboDaystatus.SelectedValue.ToString();
            string BatchCode = lblBatchCode.Text;
            if (Date.Equals(""))
            {
                ShowMessage("Please Select Date", true);
            }
            else
            {
                if (hour.Equals("HH"))
                {
                    ShowMessage("Please Select Schedule Hour", true);
                }
                else
                {
                    if (min.Equals("MM"))
                    {
                        ShowMessage("Please Select Schedule Minutes", true);
                    }
                    else
                    {
                        if (dayState.Equals("AM/PM"))
                        {

                            ShowMessage("Please Select Date State", true);
                        }
                        else
                        {
                            DateTime now = DateTime.Now;
                            DateTime date = bll.GetScheduledate(Date, hour, min, dayState);
                            if (date > now)
                            {
                                string DayStatus = cboDaystatus.SelectedValue.ToString();
                                string status = bll.SendBulkPayment(BatchCode, schedulestatus, date, fromAccount);
                                if (status.Contains("successfully"))
                                {
                                    //datafile.LogBatchAccount(BatchCode, StatusTo, LevelTo, Accountfrom, Comment);
                                    ShowMessage("File (" + BatchCode + ")" + status, false);
                                }
                                else
                                {
                                    ShowMessage("Failed to Process Payment Batch", false);
                                }

                            }
                            else
                            {
                                ShowMessage("Schedule Date Should be future date", false);
                            }
                        }
                    }
                }
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}
