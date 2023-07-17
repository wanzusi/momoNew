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

public partial class ViewCorporateBatches : System.Web.UI.Page
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

            ShowAccountBalance();
            Button8.Enabled = false;
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
    private void LoadCustomerAccountTypes()
    {
        dataTable = datafile.GetCustomerAccountsTypes();
        if (dataTable.Rows.Count>0)
        {
        cboFromAccount.DataSource = dataTable;
        cboFromAccount.DataValueField = "AccountTypeCode";
        cboFromAccount.DataTextField = "AccountType";
        cboFromAccount.DataBind();
        }
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
            string StatusCode = GetStatus();
            string CustomerCode = Session["CustomerCode"].ToString();
            string TypeCode = cboType.SelectedValue.ToString();
            string LevelCode = Session["LevelID"].ToString();
            DateTime StartDate = bll.Returnstartingdate(txtfromDate.Text.Trim());
            DateTime EndDate = bll.Returnendingdate(txttoDate.Text.Trim());
            dataTable = datafile.GetBatchforApproval(CustomerCode, StatusCode, TypeCode, LevelCode, StartDate, EndDate);
            if (dataTable.Rows.Count > 0)
            {

                DataGrid1.DataSource = dataTable;
                DataGrid1.CurrentPageIndex = 0;
                DataGrid1.DataBind();
                DataGrid1.Visible = true;
                ShowMessage(".", true);
            }
            else
            {
                ShowMessage("No File(s) found for your View", true);
                DataGrid1.Visible = false;
            }
            LoadRejected();
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
        dataTable = datafile.GetRejectedBatches(CustomerCode, StatusCode);
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
    private void ShowAccountBalance()
    {
        string PegasusAccount = Session["CustomerPegasusAccount"].ToString();
        Label msg = (Label)Master.FindControl("lblPegasusAccountBal");
        msg.Visible = true;
        dataTable = datafile.GetAccountBalance(PegasusAccount);
        if (dataTable.Rows.Count > 0)
        {
            double AccBal = Convert.ToDouble(dataTable.Rows[0]["AccountBalance"].ToString());
            msg.Text = AccBal.ToString("#,##0");
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
            txtCommentSent.Text = dataTable.Rows[0]["Comment"].ToString();
            lblUserFrom.Text = dataTable.Rows[0]["RecordedBy"].ToString();
            lblRoleFrom.Text = dataTable.Rows[0]["RoleCode"].ToString();
            lblLevelFrom.Text = dataTable.Rows[0]["FileLevel"].ToString();
            string CustomerCode = Session["CustomerCode"].ToString();
            DataTable dt = datafile.GetBatchRecords(BatchCode, CustomerCode);
            DataGrid4.DataSource = dt;
            DataGrid4.DataBind();
            lblBatchCode.Text = BatchCode;
            MultiView3.ActiveViewIndex = 1;
            MultiView2.ActiveViewIndex = -1;
            MultiView4.ActiveViewIndex = -1;
            MultiView1.ActiveViewIndex = -1;
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
        txtBatchTotal.Text = lblTotal.Text;
        dataTable  = datafile.GetBatchRecords(BatchCode, CustomerCode);
        MultiView3.ActiveViewIndex = -1;
        if (dataTable.Rows.Count > 0)
        {
            if (Session["RoleCode"].ToString() == "015")
            {
                MultiView1.ActiveViewIndex = 1;
                MultiView2.ActiveViewIndex = 0;//approval view.
                DataGrid2.DataSource = dataTable;
                DataGrid2.CurrentPageIndex = 0;
                DataGrid2.DataBind();
               GetTotalAmount(dataTable);
                //GetCustomerAccounts();
                //GetUserAccounts();
                MultiView5.ActiveViewIndex = 0;
                //Button4.Visible = false;
               // LoadLogs(BatchCode);
            }
            else
            {
                MultiView1.ActiveViewIndex = 1;
                MultiView2.ActiveViewIndex = -1;//approval view
                DataGrid2.DataSource = dataTable;
                DataGrid2.DataBind();
                GetTotalAmount(dataTable);

                string Role = Session["RoleCode"].ToString();
                if (Role == "013")
                {
                    MultiView5.ActiveViewIndex = -1;
                    //Button4.Visible = true;
                }
                else
                {
                    MultiView5.ActiveViewIndex = 0;
                    //Button4.Visible = false;
                    LoadLogs(BatchCode);
                }
                ShowMessage(".", true);
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
        lblCashoutFee.Text = "Total Cash Out Fee(" + CashoutTotal.ToString("#,##0") + ")";
        if (Session["RoleCode"].ToString() == "015")
        {
            txtBatchTotal.Text = total.ToString("#,##0");
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
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            string StatusCode = GetStatus();
            string CustomerCode = Session["CustomerCode"].ToString();
            string TypeCode = cboType.SelectedValue.ToString();
            string LevelCode = Session["LevelID"].ToString();
            DateTime StartDate = bll.Returnstartingdate(txtfromDate.Text.Trim());
            DateTime EndDate = bll.Returnendingdate(txttoDate.Text.Trim());
            dataTable = datafile.GetBatchforApproval(CustomerCode, StatusCode, TypeCode, LevelCode, StartDate, EndDate);
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
        //deo1
        string CustomerCode = Session["CustomerCode"].ToString();
        string BatchCode = lblBatchCode.Text;
        txtBatchTotal.Text = lblTotal.Text;
        dataTable = datafile.GetBatchRecords(BatchCode, CustomerCode);
        MultiView3.ActiveViewIndex = -1;
        if (dataTable.Rows.Count > 0)
        {
            if (Session["RoleCode"].ToString() == "015")
            {
                MultiView1.ActiveViewIndex = 1;
                MultiView2.ActiveViewIndex = 0;//approval view.
                DataGrid2.DataSource = dataTable;
                DataGrid2.CurrentPageIndex = e.NewPageIndex;
                DataGrid2.DataBind();
                GetTotalAmount(dataTable);
                //GetCustomerAccounts();
                //GetUserAccounts();
                MultiView5.ActiveViewIndex = 0;
                //Button4.Visible = false;
                // LoadLogs(BatchCode);
            }
            else
            {
                MultiView1.ActiveViewIndex = 1;
                MultiView2.ActiveViewIndex = -1;//approval view
                DataGrid2.DataSource = dataTable;
                DataGrid2.CurrentPageIndex = e.NewPageIndex;
                DataGrid2.DataBind();
                GetTotalAmount(dataTable);

                string Role = Session["RoleCode"].ToString();
                if (Role == "013")
                {
                    MultiView5.ActiveViewIndex = -1;
                    //Button4.Visible = true;
                }
                else
                {
                    MultiView5.ActiveViewIndex = 0;
                    //Button4.Visible = false;
                    LoadLogs(BatchCode);
                }
                ShowMessage(".", true);
            }
        }
        else
        {
            ShowMessage("Batch (" + BatchCode + ") has no record(s)", true);
        }
        

    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {
            ProcessAction();
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }
    private void ProcessAction()
    {
        try
        {
            string CustomerCode = Session["CustomerCode"].ToString();
            string LevelCode = Session["LevelID"].ToString();
            string RoleCode = Session["RoleCode"].ToString();
            //string Account = cboAccount.SelectedValue.ToString();
            string SelectedStatus = GetSelectedStatus();
            if (SelectedStatus == "0")
            {
                ShowMessage("Please select the Approval Option.......", true);
            }
            else if (string.IsNullOrEmpty(cboFromAccount.SelectedValue.ToString()))
            {
                ShowMessage("Please Select a From Account.", true);
                return;
            }
            else if (SelectedStatus != "1" && txtComment.Text.Trim() == "")
            {
                ShowMessage("Please enter your Comment.......", true);
                txtComment.Focus();
            }
            else
            {
                if (SelectedStatus == "1")
                {
                    // Approved
                    string str = GetRecordsToExclude().TrimEnd(',');
                    ExcludePayments(str);
                    ApprovalBatch();

                }
                else
                {
                    // Regected
                    RejectBatch();
                }
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    private void ExcludePayments(string str)
    {
        try
        {
            if (!str.Equals(""))
            {
                string[] arr = str.Split(',');
                int i = 0;
                for (i = 0; i < arr.Length; i++)
                {
                    int PaymentsId = int.Parse(arr[i].ToString());
                    datafile.ExcludeBatchPayment(PaymentsId);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    private string GetRecordsToExclude()
    {
        int Count = 0;
        string ItemArr = "";
        foreach (DataGridItem Items in DataGrid2.Items)
        {
            CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
            if (chk.Checked)
            {
                Count++;
                string ItemFound = Items.Cells[1].Text;
                ItemArr = ItemArr += ItemFound + ",";
            }
        }
        return ItemArr;
    }
    private void RejectBatch()
    {
        string BatchCode = lblBatchCode.Text.Trim();
        string Comment = txtComment.Text.Trim();
        if (BatchCode == "0")
        {
            MultiView1.ActiveViewIndex =0 ;
            MultiView2.ActiveViewIndex = -1;
            MultiView3.ActiveViewIndex = -1;
            MultiView5.ActiveViewIndex = -1;
            LoadBatches();
            lblBatchCode.Text = "0";
            ShowMessage("Sorry Try again", true);
        }
        else
        {
            dataTable = datafile.GetBatchDetailsByCode(BatchCode);
            if (dataTable.Rows.Count > 0)
            {
                string StatusCode = dataTable.Rows[0]["StatusCode"].ToString();
                string LevelCode = dataTable.Rows[0]["LevelAt"].ToString();
                string StatusTo = "003";
                string LevelTo = GetLevelTo(StatusCode, LevelCode, StatusTo);
                datafile.LogBatchTransaction(BatchCode, StatusTo, LevelTo, Comment);
                // Send Email to File Uploader
                MultiView1.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = -1;
                MultiView3.ActiveViewIndex = -1;
                MultiView5.ActiveViewIndex = -1;
                LoadBatches();
                lblBatchCode.Text = "0";
                datafile.LogActivity(Session["UserName"].ToString(), "Processed Payment of BatchCode " + BatchCode);
                ShowMessage("File (" + BatchCode + ") has been rejected Successfully", false);
            }
            else
            {
                MultiView2.ActiveViewIndex = 0;
                MultiView1.ActiveViewIndex = -1;
                MultiView3.ActiveViewIndex = -1;
                MultiView5.ActiveViewIndex = -1;
                LoadBatches();
                lblBatchCode.Text = "0";
                ShowMessage("Sorry Try again", true);
            }
        }
    }

    private string GetSelectedStatus()
    {
        string status = "0";
            foreach (ListItem lst in rbnApproval.Items)
            {
                if (lst.Selected == true)
                {
                    status = lst.Value;
                }
            }
        return status;
    }
    private void ApprovalBatch()
    {
        try
        {
            string BatchCode = lblBatchCode.Text.Trim();
            string Comment = txtComment.Text.Trim();
            if (BatchCode == "0")
            {
                MultiView1.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = -1;
                //MultiView3.ActiveViewIndex = -1;
                MultiView5.ActiveViewIndex = -1;
                LoadBatches();
                lblBatchCode.Text = "0";
                ShowMessage("Sorry Try again", true);
            }
            else
            {
                dataTable = datafile.GetBatchDetailsByCode(BatchCode);
                if (dataTable.Rows.Count > 0)
                {
                    string StatusCode = dataTable.Rows[0]["StatusCode"].ToString();
                    string LevelCode = dataTable.Rows[0]["LevelAt"].ToString();
                    string StatusTo = GetStatusTo(StatusCode);
                    string LevelTo = GetLevelTo(StatusCode, LevelCode, StatusTo);
                    if (LevelTo != "100")
                    {
                        StatusTo = StatusCode;
                    }
                    if (LevelTo == "100")
                    {
                        //LevelTo = "1";
                        LevelTo = Session["LevelID"].ToString();
                    }
                    ProcessTransfer(StatusCode, LevelCode, StatusTo, LevelTo);
                }
                else
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = -1;
                    MultiView3.ActiveViewIndex = -1;
                    MultiView5.ActiveViewIndex = -1;
                    //LoadTransferFiles();
                    LoadBatches();
                    lblBatchCode.Text = "0";
                    ShowMessage("Sorry Try again", true);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void ProcessTransfer(string StatusCode, string LevelCode, string StatusTo, string LevelTo)
    {
        try
        {
            string RoleCode = Session["RoleCode"].ToString();
            string BatchCode = lblBatchCode.Text.Trim();
            string Accountfrom = cboFromAccount.SelectedValue.ToString();
            string Comment = txtComment.Text.Trim();
            string CurrentUserLevel = Session["LevelID"].ToString();
            if (StatusCode == "002" && RoleCode == "015")
            {

                if (LevelTo.Equals(CurrentUserLevel))
                {
                    //secheduling point
                    //authorize the payment;

                    string schedulestatus = GetSceheduleStatus();
                    string fromAccount = cboFromAccount.SelectedValue.ToString();
                    string Date = txtScheduleDate.Text;
                    string hour = cboHour.SelectedValue.ToString();
                    string min = cbomin.SelectedValue.ToString();
                    string dayState = cboDaystatus.SelectedValue.ToString();
                    if (schedulestatus.Equals("2") && fromAccount.Equals("A002"))
                    {
                        ShowMessage("You can Not Schdule Payments Made from Telecom Account", true);
                    }
                    else
                    {
                        if (schedulestatus.Equals("2"))
                        {
                            ProcessSchedule(schedulestatus, StatusTo, LevelTo);

                        }
                        else
                        {
                            DateTime date = DateTime.Now;
                            string status = bll.SendBulkPayment(BatchCode, schedulestatus, date, fromAccount);
                            if (status.Contains("successfully"))
                            {
                                StatusTo = "004";
                                datafile.LogBatchAccount(BatchCode, StatusTo, LevelTo, Accountfrom, Comment);
                                MultiView1.ActiveViewIndex = 0;
                                MultiView2.ActiveViewIndex = -1;
                                MultiView3.ActiveViewIndex = -1;
                                MultiView4.ActiveViewIndex = -1;
                                MultiView5.ActiveViewIndex = -1;
                                LoadBatches();
                                lblBatchCode.Text = "0";
                                string state = bll.SendNotification(BatchCode);
                                datafile.LogActivity(Session["UserName"].ToString(), "Authorized Payment of BatchCode " + BatchCode);
                                ShowMessage("File (" + BatchCode + ") has been Authorized Successfully", false);
                                ShowAccountBalance();
                            }
                            else
                            {
                                ShowMessage(status, true);
                            }
                        }
                    }
                }
                else
                {

                    datafile.LogBatchAccount(BatchCode, StatusTo, LevelTo, Accountfrom, Comment);
                    MultiView1.ActiveViewIndex = 0;
                    MultiView2.ActiveViewIndex = -1;
                    MultiView3.ActiveViewIndex = -1;
                    MultiView4.ActiveViewIndex = -1;
                    MultiView5.ActiveViewIndex = -1;
                    LoadBatches();
                    lblBatchCode.Text = "0";
                    string status = bll.SendNotification(BatchCode);
                    datafile.LogActivity(Session["UserName"].ToString(), "Authorized Payment of Batch Code " + BatchCode);
                    if (status.Equals("SENT"))
                    {
                        ShowMessage("File (" + BatchCode + ") has been sent to the next authorization level and Email notification sent to the Appropriate user level", false);
                    }
                    else
                    {
                        ShowMessage("File (" + BatchCode + ") has been sent to the next authorization level but Email notification Not Sent", false);
                    }
                    //send mail to next authorizers.
                }

            }
            else
            {
                // Send Email
                datafile.LogBatchTransaction(BatchCode, StatusTo, LevelTo, Comment);
                MultiView1.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = -1;
                MultiView3.ActiveViewIndex = -1;
                MultiView5.ActiveViewIndex = -1;
                LoadBatches();
                lblBatchCode.Text = "0";
                string status = bll.SendNotification(BatchCode);
                datafile.LogActivity(Session["UserName"].ToString(), "Verifyied Payment of Batch Code " + BatchCode);
                if (status.Equals("SENT"))
                {
                    ShowMessage("File (" + BatchCode + ") has been approved Successfully and Notification Sent Next User levels", false);
                }
                else
                {
                    ShowMessage("File (" + BatchCode + ") has been approved Successfully  and Notification not Sent", false);
                }

            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    private void ProcessSchedule(string schedulestatus, string StatusTo,string LevelTo)
    {
        try
        {
            string Accountfrom = cboFromAccount.SelectedValue.ToString();
            string Comment = txtComment.Text.Trim();
            string BatchCode = lblBatchCode.Text.Trim();
            string fromAccount = cboFromAccount.SelectedValue.ToString();
            string Date = txtScheduleDate.Text;
            string hour = cboHour.SelectedValue.ToString();
            string min = cbomin.SelectedValue.ToString();
            string dayState = cboDaystatus.SelectedValue.ToString();
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
                                    StatusTo = "004";
                                    datafile.LogBatchAccount(BatchCode, StatusTo, LevelTo, Accountfrom, Comment);
                                    MultiView1.ActiveViewIndex = 0;
                                    MultiView2.ActiveViewIndex = -1;
                                    MultiView3.ActiveViewIndex = -1;
                                    MultiView4.ActiveViewIndex = -1;
                                    MultiView5.ActiveViewIndex = -1;
                                    LoadBatches();
                                    lblBatchCode.Text = "0";
                                    datafile.LogActivity(Session["UserName"].ToString(), "Authorized Payment of Batch Code " + BatchCode);
                                    ShowMessage("File (" + BatchCode + ") has been Authorized Successfully", false);
                                    ShowAccountBalance();
                                }
                                else
                                {
                                    ShowMessage(status, true);
                                }

                            }
                            else
                            {
                                ShowMessage("Schedule Date Should be future date", true);
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

    
    private string GetStatusTo(string StatusCode)
    { //string output = "";
        string output = "002";
        if (StatusCode == "001")
        {
            output = "002";
        }
        else if (StatusCode == "002")
        {
            output = "004";
        }
        else
        {
            output = "002";
        }
        return output;
    }

    private string GetLevelTo(string StatusCode, string LevelCode, string statusTo)
    {
        string output = "0";
        DataTable datatable = new DataTable();
        if (statusTo == "003")
        {
            output = "1";
        }
        else if (statusTo == "002" || statusTo == "004")
        {
            datatable = datafile.CheckOtherLevels(LevelCode);
            if (datatable.Rows.Count > 0)
            {
                output = datatable.Rows[0]["FileLevel"].ToString().Trim();
            }
            else
            {
                output = "100";
            }
        }
        else
        {
            output = "1";
        }
        return output;
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

    protected void rbnApproval_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        // string status = GetSceheduleStatus();
        //    if(status.Equals("2"))
        //    {
        //        MultiView6.ActiveViewIndex=0;
        //    }
        //    else
        //    {
        //        MultiView6.ActiveViewIndex = -1;
        //    }
        //}
        //catch(Exception ex)
        //{
        //}
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

    protected void cboFromAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string accountType = cboFromAccount.SelectedValue.ToString();
            if (accountType.Equals("A001"))
            {
                string PegasusAccount = Session["CustomerPegasusAccount"].ToString();
                string CustomerCode=Session["CustomerCode"].ToString();
                string Balance = AccountBal(CustomerCode, PegasusAccount);
                double Bal = Convert.ToDouble(Balance);
                txtAccountBalance.Text = Bal.ToString("#,##0");
            }
            else
            {
                //retrive Account balance for Telecoms
            }

        }
        catch(Exception ex)
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
               dataTable= datafile.GetCustomerAccountInfor(CustomerCode,Account);
               if (dataTable.Rows.Count > 0)
               {
                   output = dataTable.Rows[0]["AccountBalance"].ToString();
               }
               else
               {
                   ShowMessage("Failed To Account Retrive Account Information ",true);
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

    protected void cboFromAccount_DataBound(object sender, EventArgs e)
    {
        cboFromAccount.Items.Insert(0, new ListItem("-- Select --", "0"));
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 0;
            MultiView5.ActiveViewIndex = -1;
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
       
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        try
        {
            string BatchCode = lblBatchCode.Text.Trim();
            string StatusTo = "005";
            string LevelTo = "1";
            string Comment = txtCommentToSend.Text.Trim();
            datafile.LogBatchTransaction(BatchCode, StatusTo, LevelTo, Comment);
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = -1;
            MultiView3.ActiveViewIndex = -1;
            MultiView5.ActiveViewIndex = -1;
            LoadBatches();
            lblBatchCode.Text = "0";
            //lblAccountfrom.Text = "0";
            ShowMessage(BatchCode + " has been declined Succssfully", false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            string BatchCode = lblBatchCode.Text.Trim();
            string User = lblUserFrom.Text.Trim();
            string Comment = txtCommentToSend.Text.Trim();
            if (Comment != "")
            {
                string LevelID = lblLevelFrom.Text.Trim();
                string Role = lblRoleFrom.Text.Trim();
                string StatusTo = "";
                if (Role == "015")
                {
                    StatusTo = "002";
                }
                else
                {
                    StatusTo = "001";
                }
                datafile.LogBatchTransaction(BatchCode, StatusTo, LevelID, Comment);
                LoadBatches();
                txtCommentSent.Text = "";
                txtCommentToSend.Text = "";
                ShowMessage("Batch (" + BatchCode + ") Resubmitted Successfully", false);
            }
            else
            {
                ShowMessage("Please Enter Comment", true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void DataGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
