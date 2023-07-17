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
using System.Diagnostics;
using System.Text;
using InterLinkClass.EntityObjects;
using InterLinkClass.PegpayMMoney;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
public partial class MakePayments : System.Web.UI.Page
{
    private DataLogin dac = new DataLogin();
    private BusinessLogin bll = new BusinessLogin();
    private PhoneValidator pv = new PhoneValidator();
    private DataTable dtable = new DataTable();
    private Beneficiary beneficiary = new Beneficiary();
    private SendMail mailer = new SendMail();
    private DataFile df;
    private ArrayList fileContents;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                string RoleId = Session["RoleCode"].ToString();
                if (isRoleAuthorisedToVisitPage(RoleId))
                {
                    if (Session["LoggedIn"] != null || Session["LoggedIn"].ToString().Equals("YES"))
                    {
                        //LoadIntials 
                    }
                    else
                    {
                        Response.Redirect("Default2.aspx");
                    }
                    Toggle4Process();
                    GetTypes();
                    LoadCustomerAccountTypes();
                    ShowAccountBalance();
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
    private void Toggle4Process()
    {
        string strProcessScript = "this.value='Processing...';this.disabled=true;";
        btnSave.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSave, "").ToString());
        Button1.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button1, "").ToString());
        btnYes.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnYes, "").ToString());
    }
    private void GetTypes()
    {
        dtable = dac.GetBeneficiaryTypes();
        cboType2.DataSource = dtable;
        cboType2.DataValueField = "TypeCode";
        cboType2.DataTextField = "TypeName";
        cboType2.DataBind();

        dtable = dac.GetBeneficiaryTypes();
        cboType.DataSource = dtable;
        cboType.DataValueField = "TypeCode";
        cboType.DataTextField = "TypeName";
        cboType.DataBind();
    }
    private void LoadCustomerAccountTypes()
    {
        dtable = dac.GetCustomerAccountsTypes();
        if (dtable.Rows.Count > 0)
        {
            cboFromAccount.DataSource = dtable;
            cboFromAccount.DataValueField = "AccountTypeCode";
            cboFromAccount.DataTextField = "AccountType";
            cboFromAccount.DataBind();

            cboViewAccount.DataSource = dtable;
            cboViewAccount.DataValueField = "AccountTypeCode";
            cboViewAccount.DataTextField = "AccountType";
            cboViewAccount.DataBind();
        }
    }
    private void ShowMessage(string GetMessage, bool ColorRed)
    {
        Label msg = (Label)Master.FindControl("lblmsg");
        msg.Visible = true;
        if (ColorRed == true) { msg.ForeColor = System.Drawing.Color.Red; msg.Font.Bold = false; }
        else { msg.ForeColor = System.Drawing.Color.Blue; msg.Font.Bold = true; }
        if (GetMessage == ".")
        {
            msg.Text = ".";
        }
        else
        {
            msg.Text = "MESSAGE: " + GetMessage;
        }
    }
    private void ShowAccountBalance()
    {
        string PegasusAccount = Session["CustomerPegasusAccount"].ToString();
        Label msg = (Label)Master.FindControl("lblPegasusAccountBal");
        msg.Visible = true;
        dtable = dac.GetAccountBalance(PegasusAccount);
        if (dtable.Rows.Count > 0)
        {
            double AccBal = Convert.ToDouble(dtable.Rows[0]["AccountBalance"].ToString());
            msg.Text = AccBal.ToString("#,##0");
        }
    }
    private void LoadControls(string Code)
    {
        dtable = dac.GetCustomerBeneficiaryDetails(Code);
        //dtable = bll.Decrpyt(dtable);
        if (dtable.Rows.Count > 0)
        {
            lblCode.Text = dtable.Rows[0]["RecordID"].ToString();
            string Name = dtable.Rows[0]["Name"].ToString();
            txtName.Text = Getname(Name, 2);
            txtBenficiaryType.Text = dtable.Rows[0]["TypeCode"].ToString();
            txtPhone.Text = dtable.Rows[0]["AccountNumber"].ToString();
            txtEmail.Text = dtable.Rows[0]["EmailAddress"].ToString();
            string Type = dtable.Rows[0]["TypeCode"].ToString();
            bool IsActive = Convert.ToBoolean(dtable.Rows[0]["Active"].ToString());
            chkActive.Checked = IsActive;
            txtLocation.Text = dtable.Rows[0]["Location"].ToString();
            //cboBeneficiary.SelectedIndex = cboBeneficiary.Items.IndexOf(cboBeneficiary.Items.FindByValue(Corporation));
            MultiView3.ActiveViewIndex = 0;
            //rbnMethod.Enabled = false;
            //cboBeneficiary.Enabled = false;
        }
        else
        {
            ShowMessage(Code + " Details would not load", true);
        }
    }
    private string Getname(string nameString, int position)
    {
        string output = "";
        string[] array = nameString.Split(' ');
        int arrlength = array.Length;
        if (position == 1)
        {
            output = array[0].ToString();
        }
        else
        {
            if (arrlength == 2)
            {
                output = array[1].ToString();
            }
            else if (arrlength > 2)
            {
                output = array[1].ToString() + " " + array[2].ToString();
            }
        }
        return output;
    }

    private void GetBeneficiaries(DataTable dtable)
    {
        cboBeneficiary.DataSource = dtable;
        cboBeneficiary.DataValueField = "RecordID";
        cboBeneficiary.DataTextField = "FullName";
        cboBeneficiary.DataBind();
    }
    //private void GetCustomers2(DataTable dtable)
    //{
    //    cboCustomers2.DataSource = dtable;
    //    cboCustomers2.DataValueField = "ID";
    //    cboCustomers2.DataTextField = "FullName";
    //    cboCustomers2.DataBind();
    //}
    private string ProcessPayment()
    {
        try
        {
            PhoneValidator pv = new PhoneValidator();
            string Paymentstatus = "";
            string CustomerType = Session["CustomerTypeCode"].ToString();
            string CustomerAccount = Session["CustomerPegasusAccount"].ToString();
            string TypeCode = txtBenficiaryType.Text;
            string Reason = txtViewReason.Text;
            string BatchCode = CreatedTransferBatchCode(TypeCode, Reason);
            beneficiary.Name = txtViewName.Text;
            beneficiary.Code = txtviewPhone.Text;
            beneficiary.Mobile = txtviewPhone.Text;
            beneficiary.PaymentReason = txtViewReason.Text;
            beneficiary.PaymentDate = DateTime.Now.ToString();
            beneficiary.TransferType = "2";
            beneficiary.RecordedBy = Session["UserName"].ToString();
            beneficiary.CustomerCode = Session["CustomerCode"].ToString();
            beneficiary.TypeCode = txtBenficiaryType.Text;
            beneficiary.TransferAmount = double.Parse(txtViewAmount.Text);
            beneficiary.BatchCode = BatchCode;
            beneficiary.FromAccount = cboViewAccount.SelectedValue.ToString();
            //added pegasus charge
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
            System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertValidation;
            InterLinkClass.PegpayMMoney.PegPayTelecomsApi telApi = new InterLinkClass.PegpayMMoney.PegPayTelecomsApi();
            int pegasusCharge = telApi.GetPegPayCharge(beneficiary.CustomerCode, ipAddress);

            beneficiary.PegasusCharge = pegasusCharge;
            if (chkviewCashOut.Checked)
            {
                string network = pv.GetNetwork(pv.FormatTelephone(beneficiary.Mobile));
                beneficiary.CashOutCharge = bll.GetCashOutCharge(beneficiary.TransferAmount, network);
            }
            else
            {
                beneficiary.CashOutCharge = 0;
            }
            //save to database to get Reference for vendor transaction.
            string PayId = dac.SaveTransferFileRecord(beneficiary);
            if (!PayId.Equals(""))
            {
                if (CustomerType.ToUpper().Equals("RETAIL"))
                {
                    string schedulestatus = GetSceheduleStatus();
                    string fromAccount = cboFromAccount.SelectedValue.ToString();
                    string Date = txtScheduleDate.Text;
                    string hour = cboHour.SelectedValue.ToString();
                    string min = cbomin.SelectedValue.ToString();
                    string dayState = cboDaystatus.SelectedValue.ToString();
                    if (schedulestatus.Equals("2"))
                    {
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
                                        string Scheduledby = Session["UserName"].ToString();
                                        if (date > now)
                                        {
                                            dac.UpdatePaymentSchedule(BatchCode, date, Scheduledby);
                                            Paymentstatus = "Payment Batch Have been Scheduled Successfully";

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
                    else
                    {
                        Paymentstatus = "Failed To DeductMoney From Account";
                    }
                }
                else
                {
                    //sendmail
                    string status = bll.SendNotification(BatchCode);
                    if (status.Equals("SENT"))
                    {
                        Paymentstatus = "The Payment Has been Successfully Sent for Approval And Notification Sent to Appropriate Users";
                    }
                    else
                    {
                        Paymentstatus = "The Payment Has been Successfully Sent for Approval but " + status;
                    }
                    dac.LogActivity(Session["UserName"].ToString(), "Processed Payment of BatchCode " + BatchCode);
                }
            }
            else
            {
                //rollback batch
            }
            return Paymentstatus;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool RemoteCertValidation(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    private InterLinkClass.PegpayMMoney.Transaction AssginTransactionValues()
    {

        InterLinkClass.PegpayMMoney.Transaction trans = new InterLinkClass.PegpayMMoney.Transaction();
        string Code = lblCode.Text.Trim();
        string Fname = txtName.Text.Trim();
        string Phone = txtPhone.Text.Trim();
        string Email = txtEmail.Text.Trim();
        string Location = txtLocation.Text.Trim();
        string Amount = txtAmount.Text;
        dtable = dac.GetVendorCredentials(Code);
        string encPassword = dtable.Rows[0]["VendorPassword"].ToString();
        string Password = bll.DecryptString(encPassword);
        trans.CustomerName = txtName.Text.Trim();
        //trans.FromAccount = "256776963009";
        trans.ToAccount = txtPhone.Text.Trim(); //"256776963009";
        trans.TranAmount = txtAmount.Text; //"1500";
        trans.TranCharge = "10";
        trans.TranType = "PUSH";
        trans.VendorCode = "PBU";
        trans.Password = "34K61US725";
        trans.PaymentDate = DateTime.Now.ToString();
        Random random = new Random();
        int randomNumber = random.Next(1000, 2000);
        trans.VendorTranId = randomNumber.ToString();
        string StringToSign = "";
        trans.DigitalSignature = bll.SignCertificate(StringToSign, trans.VendorCode);
        return trans;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ValidateDetails();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void ValidateDetails()
    {
        string CustomerType = Session["CustomerTypeCode"].ToString();
        string Code = lblCode.Text.Trim();
        string Fname = txtName.Text.Trim();
        string Phone = txtPhone.Text.Trim();
        string Email = txtEmail.Text.Trim();
        string Location = txtLocation.Text.Trim();
        string Amount = txtAmount.Text;
        string FromAccount = cboFromAccount.SelectedValue.ToString();
        string Reason = txtReason.Text;
        if (cboBeneficiary.SelectedValue.ToString() == "0")
        {
            ShowMessage("Please Select Beneficiary", true);
        }
        else if (CustomerType.Equals("RETAIL") && FromAccount.Equals("0"))
        {
            ShowMessage("Select From Account", true);
        }
        else if (Amount.Equals(""))
        {
            ShowMessage("Please Enter Payment Amount", true);
            txtAmount.Focus();
        }
        else if (Reason.Equals(""))
        {
            ShowMessage("Please Enter Payment Reason", true);
            txtReason.Focus();
        }
        else
        {
            //check Account Balance
            string CustomerCode = Session["CustomerCode"].ToString();
            if (FromAccount.Equals("A001"))
            {
                string CustomerAccount = Session["CustomerPegasusAccount"].ToString();
                double PayAmount = double.Parse(Amount);
                string BalanceStatus = bll.checkAccountBalance(PayAmount, CustomerAccount, CustomerCode, CustomerType);
                if (BalanceStatus.Equals("OK"))
                {
                    //view payment
                    LoadViewPayment();
                }
                else if (BalanceStatus.Equals("LESS"))
                {
                    ShowMessage("There is No enough Credit on your Account. ", true);
                }
                else
                {
                    ShowMessage("Failed To Retrieve Account Balance Details. ", true);
                }
            }
            else
            {
                LoadViewPayment();
            }
        }
    }

    private void LoadViewPayment()
    {
        string FromAccount = cboFromAccount.SelectedValue.ToString();
        string CustomerType = Session["CustomerTypeCode"].ToString();
        MultiView2.ActiveViewIndex = 1;
        txtViewName.Text = txtName.Text;
        txtViewType.Text = txtBenficiaryType.Text;
        txtviewPhone.Text = txtPhone.Text;
        txtViewLocation.Text = txtLocation.Text;
        txtviewEmail.Text = txtEmail.Text;
        txtViewAmount.Text = txtAmount.Text;
        txtViewAccountBalance.Text = txtAccountBalance.Text;
        rbnMethod.Enabled = false;
        txtViewReason.Text = txtReason.Text;
        chkviewCashOut.Checked = ChkCharge.Checked;
        cboViewAccount.SelectedIndex = cboViewAccount.Items.IndexOf(cboViewAccount.Items.FindByValue(FromAccount));
        ShowMessage(".", false);
        if (CustomerType.Equals("RETAIL"))
        {
            MultiView4.ActiveViewIndex = 0;
        }
        else
        {
            MultiView4.ActiveViewIndex = -1;
        }

    }
    private void ClearContrl()
    {
        lblCode.Text = "0";
        txtName.Text = "";
        txtPhone.Text = "";
        txtLocation.Text = "";
    }
    protected void cboCorporation_DataBound(object sender, EventArgs e)
    {
        cboBeneficiary.Items.Insert(0, new ListItem("-- Select Corporation --", "0"));
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            CountRecord();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void CountRecord()
    {
        string TypeCode = cboType2.SelectedValue.ToString();
        if (TypeCode == "0")
        {
            ShowMessage("Please Select Type of Beneficiary to Upload", true);
        }
        else if (FileUpload1.FileName.Trim().Equals(""))
        {
            ShowMessage("Please Select File to Upload", true);
        }
        else
        {
            ReadFile();
        }
    }

    private void ReadFile()
    {

        HttpFileCollection uploads;
        uploads = HttpContext.Current.Request.Files;
        string c = FileUpload1.FileName;
        string file_ext = Path.GetExtension(c);
        string cNoSpace = c.Replace(" ", "-");
        string User = Session["UserName"].ToString().Replace(" ", "-").Replace(".", "");
        string typeCode = cboType2.SelectedValue.ToString();
        string CustomerTypeCode = Session["CustomerTypeCode"].ToString();
        string CustomerAccount = Session["CustomerPegasusAccount"].ToString();
        string Date = DateTime.Now.ToString().Replace("/", "-");
        Date = Date.Replace(":", "-");
        string c1 = User + "_" + Date + "_" + cNoSpace;
        c1 = c1.Replace(" ", "");
        string PathFrom = dac.GetSystemParameter(7, 17);
        bll.CheckPath(PathFrom);
        //string FullPath = (PathFrom + "" + c1);
        string FullPath = ReturnPath();
        FileUpload1.SaveAs(FullPath);

        if (file_ext == ".csv" || file_ext == ".txt")
        {
            int count = 0;
            int failed = 0;
            int position = 0;
            double total = 0;
            df = new DataFile();
            fileContents = df.readFile(FullPath);
            ArrayList failedRecords = new ArrayList();
            ArrayList PassedRecords = new ArrayList();
            //fileContents = df.readFile(FullPath);
            for (int i = 0; i < fileContents.Count; i++)
            {
                position = i + 1;
                string line = fileContents[i].ToString();
                string[] sLine = line.Split(',');
                //line = line.Replace("", "");
                if (sLine.Length == 2)
                {
                    string phone = pv.FormatTelephone(sLine[0].ToString());
                    string amount = sLine[1].ToString();
                    Beneficiary ben = new Beneficiary();
                    ben.Code = bll.formatPhone(phone);
                    ben.Mobile = bll.formatPhone(phone);
                    ben.CustomerCode = Session["CustomerCode"].ToString();
                    ben.TypeCode = cboType2.SelectedValue.ToString();
                    ben.NetworkCode = pv.GetNetwork(phone);
                    double amt;
                    if (Double.TryParse(amount, out amt))
                    {
                        double tran_amount = double.Parse(amount);
                        ben.TransferAmount = tran_amount;
                        total = total + tran_amount;
                        if (!bll.BeneficiaryExists(ben))
                        {
                            failed++;
                            ben.Reason = "Not on file of " + cboType2.SelectedItem.ToString();
                            failedRecords.Add(ben);
                        }
                        else if (!bll.IsBeneficiaryActive(ben))
                        {
                            failed++;
                            ben.Reason = "Beneficiary nolonger active";
                            failedRecords.Add(ben);
                        }
                        else
                        {
                            count = i + 1;
                            PassedRecords.Add(ben);
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid Amount at Line " + position.ToString());
                    }
                }
                else if (sLine.Length == 4)
                {

                    string phone = pv.FormatTelephone(sLine[2].ToString());
                    string amount = sLine[3].ToString();
                    Beneficiary ben = new Beneficiary();
                    ben.Code = bll.formatPhone(phone);
                    ben.Mobile = bll.formatPhone(phone);
                    ben.Name = sLine[0].ToString();
                    ben.Location = sLine[1].ToString();
                    ben.CustomerCode = Session["CustomerCode"].ToString();
                    ben.TypeCode = cboType2.SelectedValue.ToString();
                    ben.NetworkCode = pv.GetNetwork(phone);
                    double amt;
                    if (Double.TryParse(amount, out amt))
                    {
                        double tran_amount = double.Parse(amount);
                        ben.TransferAmount = tran_amount;
                        total = total + tran_amount;

                        
                            count = i + 1;
                            PassedRecords.Add(ben);
                        
                    }
                    else
                    {
                        throw new Exception("Invalid Amount at Line " + position.ToString());
                    }
                }
                else
                {
                    throw new Exception("File Format is not OK, Columns must be 2 (Code and Amount) at line " + position.ToString());
                }

            }
            string CustomerCode = Session["CustomerCode"].ToString();
            string BalanceStatus = bll.checkAccountBalance(total, CustomerAccount, CustomerCode, CustomerTypeCode);
            lblBatchTotal.Text = total.ToString(); ;
            if (BalanceStatus.Equals("OK"))
            {
                if (failed.Equals(0))
                {
                    lblBatchCode.Text = FullPath;
                    Toggle(count, true);
                    ShowMessage(".", true);
                    DisplaySuccessful(PassedRecords);
                }
                else
                {
                    string msg = failed.ToString() + " Record(s) have failed the validation process";
                    ShowMessage(msg, true);
                    DisplayFailed(failedRecords);
                }
            }
            else if (BalanceStatus.Equals("LESS"))
            {
                ShowMessage("There is no enough credit on your account. ", true);
            }
            else
            {
                ShowMessage("Failed to retrieve account balance details. ", true);
            }
        }
        else
        {
            bll.RemoveFile(FullPath);
            ShowMessage("File format " + file_ext + " is not supported", true);
        }
    }
    private string ReturnPath()
    {
        string filename = HttpUtility.HtmlEncode(Path.GetFileName(FileUpload1.FileName));
        string extension = HttpUtility.HtmlEncode(Path.GetExtension(filename)); //Path.GetExtension(FileUpload2.FileName);
        DateTime now = DateTime.Now;
        string dt = now.ToString("ddMMyyyy");
        DataTable returnedPath = new DataTable();
        string folder = dac.GetSystemParameter(7, 17).Trim();
        string User = Session["UserName"].ToString().Replace(" ", "-").Replace(".", "");
        filename = User + filename;
        string filepath = folder + dt + filename; // +"." + extension;
        //string filepath = "E:\\PEGPAY\\BATCH\\IN_FILES\\" + dt + filename;
        //string filepath = "C:\\Batch\\Unprocessed\\" + dt + filename; // +"." + extension;
        if (File.Exists(filepath))
        {
            //File.Delete(filepath);
        }
        FileUpload1.SaveAs(filepath);

        return filepath;
    }
    private void Toggle(int count, bool Check)
    {
        MultiView1.ActiveViewIndex = 2;
        lblQn.Text = "Are you sure you want to upload a file of " + count + " record(s)";
    }
    private void DisplayFailed(ArrayList failedTransactions)
    {
        try
        {
            DataTable dt = GetFailedBsDataTable();
            Beneficiary bene;
            for (int i = 0; i < failedTransactions.Count; i++)
            {
                bene = (Beneficiary)failedTransactions[i];
                DataRow dr = dt.NewRow();
                dr["No."] = i + 1;
                dr["Code"] = bene.Code;
                double amt = Convert.ToDouble(bene.TransferAmount);
                dr["Amount"] = amt.ToString("#,##0");
                dr["Reason"] = bene.Reason;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
            }
            //Session["FailedTran"] = dt;
            ShowFailedGrid(dt);
        }
        catch (Exception ex)
        {
            Label msg = (Label)Master.FindControl("lblmsg");
            msg.Text = "MESSAGE:  " + ex.Message;
        }
    }
    private void DisplaySuccessful(ArrayList successTransactions)
    {
        try
        {
            DataTable dt = GetSuccessfulBsDataTable();
            Beneficiary bene;
            for (int i = 0; i < successTransactions.Count; i++)
            {
                bene = (Beneficiary)successTransactions[i];
                DataRow dr = dt.NewRow();
                dr["No."] = i + 1;
                dr["Contact"] = bene.Code;
                double amt = Convert.ToDouble(bene.TransferAmount);
                dr["Amount"] = amt.ToString("#,##0");
                dr["Network"] = bene.NetworkCode;
                dr["Type"] = bene.TypeCode;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
            }
            DataGrid2.DataSource = dt;
            DataGrid2.DataBind();
            GetTotalAmount(dt);

        }
        catch (Exception ex)
        {
            Label msg = (Label)Master.FindControl("lblmsg");
            msg.Text = "MESSAGE:  " + ex.Message;
        }
    }
    private void ShowFailedGrid(DataTable dt)
    {
        MultiView1.ActiveViewIndex = 3;
        DataGrid6.DataSource = dt;
        DataGrid6.DataBind();
    }
    private DataTable GetFailedBsDataTable()
    {
        DataTable dt = new DataTable("Table2");
        dt.Columns.Add("No.");
        dt.Columns.Add("Code");
        dt.Columns.Add("Amount");
        dt.Columns.Add("Reason");
        return dt;
    }
    private DataTable GetSuccessfulBsDataTable()
    {
        DataTable dt = new DataTable("Success");
        dt.Columns.Add("No.");
        dt.Columns.Add("Contact");
        dt.Columns.Add("Amount");
        dt.Columns.Add("Type");
        dt.Columns.Add("Network");
        dt.Columns.Add("Charge");
        return dt;
    }
    private void RemoveFile(string Path)
    {
        if (File.Exists(Path))
        {
            File.Delete(Path);
        }
    }
    private void CheckPath(string Path)
    {
        if (!Directory.Exists(Path))
        {
            Directory.CreateDirectory(Path);
        }
    }
    protected void rbnMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbnMethod.SelectedValue.ToString() == "0")
            {
                MultiView1.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = 0;
            }
            else
            {
                MultiView1.ActiveViewIndex = 1;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            processBulkPayment();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void processBulkPayment()
    {
        try
        {
            PhoneValidator pv = new PhoneValidator();
            ArrayList failedRecords = new ArrayList();
            string FullPath = lblBatchCode.Text.Trim();
            int count = 0;
            int failed = 0;
            string msg = "";
            df = new DataFile();
            fileContents = df.readFile(FullPath);
            string TypeCode = cboType2.SelectedValue.ToString();
            string Reason = txtBulkpaymentReason.Text;
            string BatchCode = CreatedTransferBatchCode(TypeCode, Reason);
            string CustomerType = Session["CustomerTypeCode"].ToString();
            for (int i = 0; i < fileContents.Count; i++)
            {
                Beneficiary beneficiary = new Beneficiary();
                string line = fileContents[i].ToString();
                string[] sLine = line.Split(',');
                string[] StrArray = line.Split(Convert.ToChar(","));
                string Phone = "";
                double Amount = 0;
                if (sLine.Length == 4)
                {
                    Phone = pv.FormatTelephone(StrArray[2].ToString());
                    Amount = IsEmpty(StrArray[3].ToString());
                    beneficiary.Location = StrArray[1].ToString();
                    beneficiary.Name = StrArray[0].ToString();
                    beneficiary.BatchCode = BatchCode;
                    beneficiary.Code = bll.formatPhone(Phone);
                    beneficiary.Mobile = bll.formatPhone(Phone);
                    beneficiary.PaymentDate = DateTime.Now.ToString();
                    beneficiary.TransferType = "2";
                    beneficiary.RecordedBy = Session["UserName"].ToString();
                    beneficiary.CustomerCode = Session["CustomerCode"].ToString();
                    beneficiary.TypeCode = TypeCode;
                    beneficiary.TransferAmount = Amount;
                    beneficiary.BatchCode = BatchCode;
                    beneficiary.FromAccount = cboViewAccount.SelectedValue.ToString();
                    beneficiary.Active = true;
                    beneficiary.NetworkCode = pv.GetNetwork(pv.FormatTelephone(beneficiary.Mobile));
                    beneficiary.RecordCode = 0;
                    beneficiary.CustomerId = Convert.ToInt32(Session["CustomerId"].ToString());

                    dac.SaveApprovedCustomerBeneficiary(beneficiary);
                }
                else
                {
                    Phone = pv.FormatTelephone(StrArray[0].ToString());
                    Amount = IsEmpty(StrArray[1].ToString());
                    beneficiary.Code = bll.formatPhone(Phone);
                    beneficiary.Mobile = bll.formatPhone(Phone);
                    beneficiary.PaymentDate = DateTime.Now.ToString();
                    beneficiary.TransferType = "2";
                    beneficiary.RecordedBy = Session["UserName"].ToString();
                    beneficiary.CustomerCode = Session["CustomerCode"].ToString();
                    beneficiary.TypeCode = TypeCode;
                    beneficiary.TransferAmount = Amount;
                    beneficiary.BatchCode = BatchCode;
                    beneficiary.FromAccount = cboViewAccount.SelectedValue.ToString();
                    beneficiary.Active = true;
                    beneficiary.NetworkCode = pv.GetNetwork(pv.FormatTelephone(beneficiary.Mobile));
                    beneficiary.RecordCode = 0;
                    beneficiary.CustomerId = Convert.ToInt32(Session["CustomerId"].ToString());

                }


                if (chkCashout2.Checked)
                {
                    string network = pv.GetNetwork(pv.FormatTelephone(beneficiary.Mobile));
                    beneficiary.CashOutCharge = bll.GetCashOutCharge(beneficiary.TransferAmount, network);
                }
                else
                {
                    beneficiary.CashOutCharge = 0;
                }
                if (!bll.BeneficiaryExists(beneficiary))
                {
                    failed++;
                    beneficiary.Reason = "Not on file of " + cboType2.SelectedItem.ToString();
                    failedRecords.Add(beneficiary);
                }
                else if (!bll.IsBeneficiaryActive(beneficiary))
                {
                    failed++;
                    beneficiary.Reason = "Beneficiary nolonger active";
                    failedRecords.Add(beneficiary);
                }
                else
                {
                    //save to database to get Reference for vendor transaction.
                    string PayId = dac.SaveTransferFileRecord(beneficiary);
                    if (!PayId.Equals(""))
                    {
                        if (CustomerType.ToUpper().Equals("RETAIL"))
                        {
                            InterLinkClass.PegpayMMoney.Transaction trans = new InterLinkClass.PegpayMMoney.Transaction();
                            InterLinkClass.PegpayMMoney.Response resp = new InterLinkClass.PegpayMMoney.Response();
                            trans = GetTransactionValues(beneficiary, PayId);
                            resp = bll.SendPaymentRequest(trans);
                            if (resp.StatusCode.Equals("0"))
                            {
                                double Charge = double.Parse(Session["CustomerCharge"].ToString());
                                double Totalamount = double.Parse(trans.TranAmount) + Charge;
                                // string status = dac.DeductCustomerAccount(CustomerAccount, Totalamount);
                                string status = "";
                                if (status.Equals("OK"))
                                {
                                    count++;
                                }
                                else
                                {
                                    failed++;
                                    beneficiary.Reason = status;
                                    failedRecords.Add(beneficiary);
                                }
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            count++;
                        }
                    }
                    else
                    {
                        failed++;
                        beneficiary.Reason = "Failed to save payment record to the system";
                        failedRecords.Add(beneficiary);
                    }

                }
            }
            int TotalRecord = count + failed;
            if (failed == 0)
            {
                //dac.LogBatchTransaction(BatchCode, StatusTo, LevelTo, "");
                string status = bll.SendNotification(BatchCode);
                msg = TotalRecord + " Payments uploaded successfully";
                ShowMessage(msg, false);
                MultiView1.ActiveViewIndex = 1;
            }
            else
            {
                if (count != 0)
                {
                    //dac.LogBatchTransaction(BatchCode, StatusTo, LevelTo, "");
                    string status = bll.SendNotification(BatchCode);
                    msg = TotalRecord + " Payments uploaded, " + count + " were successful and " + failed + " failed";
                    ShowMessage(msg, true);
                    DisplayFailed(failedRecords);
                }
                else
                {
                    msg = TotalRecord + " Payments upload failed";
                    //dac.RollBackBatch(BatchCode);
                    ShowMessage(msg, true);
                    DisplayFailed(failedRecords);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string CreatedTransferBatchCode(string TypeCode, string PaymentReason)
    {
        string CustomerCode = Session["CustomerCode"].ToString();
        string RecordedBy = Session["UserName"].ToString();
        string LevelId = "1";
        double total = double.Parse(lblBatchTotal.Text);
        string StatusCode = "001";
        string BatchCode = dac.CreateTransferBatch(CustomerCode, TypeCode, total, StatusCode, LevelId, RecordedBy, PaymentReason);
        return BatchCode;
    }
    private InterLinkClass.PegpayMMoney.Transaction GetTransactionValues(Beneficiary ben, string PayId)
    {
        try
        {
            DataTable dt = new DataTable();
            InterLinkClass.PegpayMMoney.Transaction trans = new InterLinkClass.PegpayMMoney.Transaction();
            Beneficiary beneficiary = new Beneficiary();
            dt = dac.GetVendorCredentials(ben.CustomerCode);
            string encPassword = dt.Rows[0]["VendorPassword"].ToString();
            string Password = bll.DecryptString(encPassword);
            string ToTelecom = pv.GetNetwork(pv.FormatTelephone(trans.ToAccount));
            string AccountType = "ESCROW";
            string FromAccount = bll.GetFromAccount(ben.CustomerCode, ben.FromAccount, ToTelecom, AccountType);
            dtable = dac.GetBeneficiaryDetails(ben);
            trans.CustomerName = dtable.Rows[0]["Name"].ToString();
            trans.FromAccount = FromAccount;
            trans.ToAccount = ben.Mobile;
            trans.TranAmount = ben.TransferAmount.ToString();
            trans.TranCharge = "10";
            trans.TranType = "PUSH";
            trans.VendorCode = ben.CustomerCode;
            trans.Password = Password;
            trans.PaymentDate = DateTime.Now.ToString();
            trans.VendorTranId = PayId;
            string StringToSign = "";
            trans.DigitalSignature = bll.SignCertificate(StringToSign, trans.VendorCode);
            return trans;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private double IsEmpty(string Value)
    {
        string ToReturn = "0";
        double output = 0;
        if (Value == "")
        {
            ToReturn = "0";
        }
        else
        {
            ToReturn = Value;
        }
        output = Convert.ToDouble(ToReturn);
        return output;
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
    protected void btnNo_Click(object sender, EventArgs e)
    {
        string FullPath = lblPath.Text.Trim();
        RemoveFile(FullPath);
        MultiView1.ActiveViewIndex = 1;

    }
    protected void cboType2_DataBound(object sender, EventArgs e)
    {
        cboType2.Items.Insert(0, new ListItem("-- Select Customer Type --", "0"));
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string BeneficaryName = txtsearchBenName.Text;
            string BeneficaryContact = txtSearchBenPhone.Text;
            string CustomerCode = Session["CustomerCode"].ToString();
            string BeneficiaryType = cboType.SelectedValue.ToString();
            string Location = "";
            dtable = dac.GetCustomerBeneficaries(BeneficaryName, BeneficaryContact, BeneficiaryType, CustomerCode, Location);
            if (dtable.Rows.Count > 0)
            {

                GetBeneficiaries(dtable);
                ShowMessage(".", false);
            }
            else
            {
                ShowMessage("No Beneficiaries Found For the Search", true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    protected void cboBeneficiary_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string BeneficiaryCode = cboBeneficiary.SelectedValue.ToString();
            LoadControls(BeneficiaryCode);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView2.ActiveViewIndex = 0;
            rbnMethod.Enabled = true;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void rbnApproval_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string status = GetSceheduleStatus();
            if (status.Equals("2"))
            {
                MultiView5.ActiveViewIndex = 0;
            }
            else
            {
                MultiView5.ActiveViewIndex = -1;
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            string status = GetSceheduleStatus();
            string CustomerType = Session["CustomerTypeCode"].ToString();
            if (CustomerType.Equals("RETAIL") && status.Equals("0"))
            {
                ShowMessage("Please Select Scheduling Option", true);
            }
            else if (status.Equals("2") && (txtScheduleDate.Text.Equals("")))
            {
                ShowMessage("Please specify Schedule date and time", true);
            }
            else
            {
                string Status = ProcessPayment();
                if (Status.Contains("Successfully"))
                {
                    ShowMessage(Status, false);
                    LoadControl();
                }
                else
                {
                    ShowMessage(Status, false);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
    private void GetTotalAmount(DataTable dataTable)
    {
        double total = 0;
        double charge = 0;
        foreach (DataRow dr in dataTable.Rows)
        {
            double amount = Convert.ToDouble(dr["Amount"].ToString());
            if (dr["Charge"].ToString().Equals(""))
            {
                dr["Charge"] = "0";
            }
            double trancharge = Convert.ToDouble(dr["Charge"].ToString());
            charge += trancharge;
            total += amount;
        }
        lblTotal.Text = "Total Amount(" + total.ToString("#,##0") + ")";
        lblTotalCharge.Text = "Total Charge(" + charge.ToString("#,##0") + ")";
    }
    private void LoadControl()
    {
        MultiView1.ActiveViewIndex = -1;
        rbnMethod.Enabled = true;
        txtAccountBalance.Text = "";
        txtAmount.Text = "";
        txtBenficiaryType.Text = "";
        txtEmail.Text = "";
        txtLocation.Text = "";
        txtName.Text = "";
        txtPhone.Text = "";
        txtScheduleDate.Text = "";
        txtsearchBenName.Text = "";
        txtSearchBenPhone.Text = "";
        txtViewAccountBalance.Text = "";
        txtViewAmount.Text = "";
        txtviewEmail.Text = "";
        txtViewLocation.Text = "";
        txtViewName.Text = "";
        txtviewPhone.Text = "";
        txtViewType.Text = "";
        chkviewCashOut.Checked = false;
        ChkCharge.Checked = false;
        cboBeneficiary.SelectedIndex = cboBeneficiary.Items.IndexOf(cboBeneficiary.Items.FindByValue("0"));
        cboFromAccount.SelectedIndex = cboFromAccount.Items.IndexOf(cboFromAccount.Items.FindByValue("0"));

    }


    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 1;
            ShowMessage(".", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void DataGrid6_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {

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
                txtAccountBalance.Text = "";
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void cboFromAccount_DataBound(object sender, EventArgs e)
    {
        cboFromAccount.Items.Insert(0, new ListItem("-- Select --", "0"));
    }
    protected void cboType_DataBound(object sender, EventArgs e)
    {
        cboType.Items.Insert(0, new ListItem("-- Select Type --", "0"));
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
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
                dtable = dac.GetCustomerAccountInfor(CustomerCode, Account);
                if (dtable.Rows.Count > 0)
                {
                    output = dtable.Rows[0]["AccountBalance"].ToString();
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
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {

    }
}
