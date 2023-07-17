using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Net;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Encryption;
using InterLinkClass.EntityObjects;
using InterLinkClass.PegpayMMoney;
using InterLinkClass.AirTelMoney;
using System.Collections.Generic;
using System.Drawing;
using OfficeOpenXml;
using System.Messaging;


public class BusinessLogin
{
    DataLogin datafile = new DataLogin();
    Datapay dalpay = new Datapay();
    DataTable datatable = new DataTable();
    DataTable dtable = new DataTable();
    DataSet dataSet = new DataSet();
    public string BulkPaymentsQueue = @".\private$\BulkPaymentsBatchesQueue";
    public string FortebetQueue = @".\private$\FortebetMomoUpdatedTxnsQueue";

    //public static string smtpServer = "mail.pegasus.co.ug";//"192.185.83.129";//"64.233.167.108";
    //public static string smtpPassword = "notifications@123";//"Tingate710";
    //public static string smtpUsername = "notifications@pegasus.co.ug";//"pegpay247pegasus@gmail.com";
    //public static int smtpPort = 587;
    public BusinessLogin()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string HashPassword(string input)
    {
        // Use input string to calculate MD5 hash
        SHA256 md5 = System.Security.Cryptography.SHA256.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        // Convert the byte array to hexadecimal string
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("X2"));
        }
        return sb.ToString();
    }
    public bool SaveMomoFile(string filename, string uploadedBy)
    {
        bool exists = false;
        try
        {
            ExecuteNonQuery("SaveMomoFile", filename, "MOBILEMONEY", "RECON", uploadedBy);
            exists = true;
            return exists;
        }
        catch (Exception ee)
        {
            throw ee;
        }
    }
    public void ExecuteNonQuery(string procedure, params object[] data)
    {
        datafile.ExecuteNonQuery(procedure, data);
    }

    public string EncryptString(string ClearText)
    {
        string ret = "";
        ret = Encryption.encrypt.EncryptString(ClearText, "Umeme2501PegPay");
        return ret;
    }
    public string DecryptString(string Encrypted)
    {
        string ret = "";
        ret = Encryption.encrypt.DecryptString(Encrypted, "Umeme2501PegPay");
        return ret;
    }

    public void LoadAccountsToReconcile(string telecom, DropDownList ddlst)
    {
        Datapay datapay = new Datapay();
        DataTable table = new DataTable();
        table = datapay.GetAccountsToReconcile(telecom);

        ddlst.Items.Clear();
        foreach (DataRow dr in table.Rows)
        {
            string Ova = dr["AccountCode"].ToString();
            string SenderId = dr["AccountNumber"].ToString();
            ddlst.Items.Add(new ListItem(Ova + "-" + SenderId, SenderId));
        }
    }

    public void SendOTPSms(string username, string pin)
    {
        try
        {
            DataLogin dataLog = new DataLogin();
            DataTable dt = dataLog.GetCustomerDetails(username);
            string tel = dt.Rows[0]["UserPhone"].ToString();
            string Message = "Your PegPay One Time Pin is :" + pin + ". Thank You.";
            string phone = formatPhone(tel);
            string email = dt.Rows[0]["UserEmail"].ToString();
            dataLog.LogOTPSMS(phone, Message);
         
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public bool IsUserAccessAllowed(SystemUser user)
    {
        user.Passwd = HashPassword(user.Passwd);
        datatable = datafile.GetUserAccessibility(user);
        bool isValid = false;
        int foundRows = datatable.Rows.Count;
        if (foundRows > 0)
        {
            user.CompanyCode = datatable.Rows[0]["UserBranch"].ToString();
            isValid = true;
        }
        else
        {
            isValid = false;
        }
        string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FOR_WARDED_FOR"];
        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] data = ipAddress.Split(new string[] { "," }, StringSplitOptions.None);
            if (data.Length != 0)
            {
                ipAddress = data[0];
            }
        }
        else
        {
            ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        datafile.LogLoginRequest(user.Uname, user.Passwd, isValid, ipAddress);
        return isValid;
    }

    public bool IsBlockeable(SystemUser user)
    {
        bool block = false;
        try
        {
            datatable = datafile.GetLoginAttempts(user.Uname);

            if (datatable.Rows.Count > 0)
            {
                int val = 0;
                string count = datatable.Rows[0]["LoginCount"].ToString();

                if (Int32.TryParse(count, out val))
                {
                    if (val >= 4)
                    {
                        block = true;
                    }
                }
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        return block;
    }

    public bool IsValidEmailAddress(string sEmail)
    {
        if (sEmail == null)
        {
            return false;
        }
        else if (sEmail.Contains("ug.worlded.org"))
        {
            return true;
        }
        else
        {
            return Regex.IsMatch(sEmail, @"
               ^
               [-a-zA-Z0-9][-.a-zA-Z0-9]*
               @
               [-.a-zA-Z0-9]+
               (\.[-.a-zA-Z0-9]+)*
               \.
               (
               com|edu|info|gov|int|mil|net|org|biz|
               name|museum|coop|aero|pro|worlded
               |
               [a-zA-Z]{2}
               )
               $",
            RegexOptions.IgnorePatternWhitespace);
        }
    }
    public bool IsValidEmailAddressOptional(string sEmail)
    {
        if (sEmail == null)
        {
            return true;
        }
        else if (sEmail.Equals(""))
        {
            return true;
        }
        else
        {
            return Regex.IsMatch(sEmail, @"
               ^
               [-a-zA-Z0-9][-.a-zA-Z0-9]*
               @
               [-.a-zA-Z0-9]+
               (\.[-.a-zA-Z0-9]+)*
               \.
               (
               com|edu|info|gov|int|mil|net|org|biz|
               name|museum|coop|aero|pro
               |
               [a-zA-Z]{2}
               )
               $",
            RegexOptions.IgnorePatternWhitespace);
        }
    }
    public bool UserNameExists(string userName)
    {
        datatable = datafile.CheckUsername(userName);
        int foundRows = datatable.Rows.Count;
        if (foundRows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsCompanyActive(string companyCode)
    {
        datatable = datafile.CheckCompanyCode(companyCode);
        int foundRows = datatable.Rows.Count;
        if (foundRows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PasswordExpired(DateTime DateOfChange)
    {
        int Duration = 30;
        int GroupCode = 5;
        int valueCode = 8;
        string duration = DecryptString(datafile.GetSystemParameter(GroupCode, valueCode));
        Duration = Convert.ToInt16(duration);
        DateTime Today = DateTime.Now;
        TimeSpan t = Today.Subtract(DateOfChange);
        double dateDiff = t.TotalDays;
        if (dateDiff > Duration)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public double IsRemainingDays(DateTime DateOfChange)
    {
        int WarningAt = 5;
        int Duration = 30;
        int GroupCode = 5;
        int valueCode = 8;
        string duration = DecryptString(datafile.GetSystemParameter(GroupCode, valueCode));
        Duration = Convert.ToInt16(duration);
        DateTime Today = DateTime.Now;
        TimeSpan t = Today.Subtract(DateOfChange);
        double dateDiff = t.TotalDays;
        double Remaining = Duration - dateDiff;
        return Remaining;
    }
    public bool NameExists(string AreaName)
    {
        datatable = datafile.GetAreaByName(AreaName);
        int foundRows = datatable.Rows.Count;
        if (foundRows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public DateTime ReturnDate(string date, int type)
    {
        DateTime dates;

        if (type == 1)
        {

            if (date == "")
            {
                dates = DateTime.Parse("January 1, 2012");
            }
            else
            {
                dates = DateTime.Parse(date);
            }
        }
        else
        {
            if (date == "")
            {
                dates = DateTime.Now;
            }
            else
            {
                dates = DateTime.Parse(date);
            }
        }

        return dates;
    }
    public bool AreaHasBranches(int AreaID)
    {
        int foundRows = datafile.GetNumberOfBranches(AreaID);
        if (foundRows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    internal bool NamesExist(SystemUser user)
    {
        datatable = datafile.GetUserByNames(user);
        int foundRows = datatable.Rows.Count;
        if (foundRows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    internal bool IsRightOldPassword(SystemUser user)
    {
        user.Passwd = HashPassword(user.Opasswd);
        datatable = datafile.GetUserAccessibility(user);
        int foundRows = datatable.Rows.Count;
        if (foundRows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool ManualReceiptExists(string ReceiptNo)
    {
        datatable = dalpay.GetReceiptNumber(ReceiptNo);
        int foundRows = datatable.Rows.Count;
        if (foundRows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public string GetFileApplicationPath(string VendorCode)
    {
        int GlobalCode = 7;
        int ValueCode = 12;
        string strPath = datafile.GetSystemParameter(GlobalCode, ValueCode);
        if (strPath.Equals(""))
        {
            strPath = "C:\\Certificates\\";
        }
        strPath = strPath + "\\" + VendorCode;
        CheckPath(strPath);
        return strPath;
    }
    public void CheckPath(string Path)
    {
        if (!Directory.Exists(Path))
        {
            Directory.CreateDirectory(Path);
        }
    }
    public string GetPasswordString()
    {
        int intMin = 0;
        int intMax = 9;
        int strMin = 0;
        int strMax = 25;
        string[] alphabet = new string[26];
        alphabet[0] = "A";
        alphabet[1] = "B";
        alphabet[2] = "C";
        alphabet[3] = "D";
        alphabet[4] = "E";
        alphabet[5] = "F";
        alphabet[6] = "G";
        alphabet[7] = "H";
        alphabet[8] = "I";
        alphabet[9] = "J";
        alphabet[10] = "K";
        alphabet[11] = "L";
        alphabet[12] = "M";
        alphabet[13] = "N";
        alphabet[14] = "O";
        alphabet[15] = "P";
        alphabet[16] = "Q";
        alphabet[17] = "R";
        alphabet[18] = "S";
        alphabet[19] = "T";
        alphabet[12] = "U";
        alphabet[21] = "V";
        alphabet[22] = "W";
        alphabet[23] = "X";
        alphabet[24] = "Y";
        alphabet[25] = "Z";
        string pass = "";
        Random random1 = new Random();
        Random random = new Random();
        while (pass.Length < 10)
        {
            if (pass.Length == 2 || pass.Length == 5 || pass.Length == 6)
            {
                int rand = random1.Next(strMin, strMax);
                string letter = alphabet[rand];
                pass = pass + letter;
            }
            else
            {
                int randomno = random.Next(intMin, intMax);
                pass = pass + randomno.ToString();
            }
        }
        return pass;
    }
    public string ReconFilePath(string VendorCode, string fileName)
    {
        string filename = Path.GetFileName(fileName);
        string filePath = "E:\\MmoneyReconFiles\\";

        //add a salt, since same file name may be uploaded more than once
        string salt = DateTime.Now.ToString("hhmmssfff");
        string filepath = filePath + salt + "_" + filename;

        CheckPath(filePath);
        return filepath;
    }
    public string BackReconFilePath(string VendorCode, string fileName)
    {
        string filename = Path.GetFileName(fileName);
        string filePath = "E:\\MmoneyBackReconFiles\\";

        //add a salt, since same file name may be uploaded more than once
        string salt = DateTime.Now.ToString("hhmmssfff");
        string filepath = filePath + salt + "_" + filename;

        CheckPath(filePath);
        return filepath;
    }
    public string UploadFilePath(string UtilityCode, string fileName)
    {
        string filename = Path.GetFileName(fileName);
        string filePath = "E:\\MmoneyReconFiles\\";

        string salt = DateTime.Now.ToString("hhmmssfff");
        string filepath = filePath + salt + "_" + filename;

        CheckPath(filePath);
        return filepath;
    }
    public void SendMailViaGoogle(string mailTo, string message)
    {
        SmtpClient client = new SmtpClient();
        client.Credentials = new NetworkCredential("jab.arajab@gmail.com", "jabqas2012");
        client.Port = 587;
        client.Host = "smtp.gmail.com";
        client.EnableSsl = true;

        try
        {
            MailAddress
                maFrom = new MailAddress("sender_email@domain.tld", "COLLECTION", Encoding.UTF8),
                maTo = new MailAddress("recipient_email@domain2.tld", "Recipient's Name", Encoding.UTF8);
            MailMessage mmsg = new MailMessage(maFrom, maTo);
            mmsg.Body = "<html><body><h1>" + message + "</h1></body></html>";
            mmsg.BodyEncoding = Encoding.UTF8;
            mmsg.IsBodyHtml = true;
            mmsg.Subject = "ACCESS CREDENTIALS";
            mmsg.SubjectEncoding = Encoding.UTF8;
            client.Send(mmsg);

        }
        catch (Exception ex)
        {
            throw ex;
            //throw;
        }
    }
    public DataTable GetFailedReconTable()
    {
        DataTable dt = new DataTable("Table2");
        dt.Columns.Add("No.");
        dt.Columns.Add("VendorRef");
        dt.Columns.Add("PayDate");
        dt.Columns.Add("TransactionAmount");
        dt.Columns.Add("Reason");
        return dt;
    }
    public DataTable GetAgentTotalTable()
    {
        DataTable dt = new DataTable("Table2");
        dt.Columns.Add("No.");
        dt.Columns.Add("VendorCode");
        dt.Columns.Add("Vendor");
        dt.Columns.Add("Amount");
        return dt;
    }
    public string BuildBatchfile(string BatchCode, string BillCode, int trans, double total)
    {
        int new_trans = trans + 1;
        DateTime now = DateTime.Now;
        string strdt = now.ToString("dMMyy");
        string name = "C" + BillCode + "" + strdt;
        string filename = name + ".txt";
        int GroupCode = 6;
        int ValueCode = 2;
        string reportFolder = datafile.GetSystemParameter(GroupCode, ValueCode);
        if (reportFolder.Equals(""))
        {
            reportFolder = "C:\\Interface\\BatchFiles\\";
        }
        CheckPath(reportFolder);
        TextWriter tw = new StreamWriter(reportFolder + filename);
        try
        {
            tw.WriteLine("000CSU" + DateTime.Now.ToString("yyyyMMdd") + "000000" + BillCode.PadLeft(3, ' ') + "1".PadLeft(4, ' ') + "" + new_trans.ToString().PadLeft(6, ' ') + "" + total.ToString().PadLeft(12, ' '));//File Header
            /// Details
            DataTable dtpayments = datafile.GetTransByBatch(BatchCode);
            string[] batchArray = BatchCode.Split('-');
            string batchno = batchArray[1].ToString();
            foreach (DataRow dr in dtpayments.Rows)
            {
                string RecieptNo = dr["Receiptno"].ToString();
                string ChequeNo = "";
                string custaccount = dr["CustomerRef"].ToString();
                DateTime payDate = DateTime.Parse(dr["PayDate"].ToString());
                string payDateStr = payDate.ToString("yyyyMMdd");
                DateTime postDate = DateTime.Parse(dr["PostDate"].ToString());
                string postDateStr = payDate.ToString("yyyyMMdd");
                double amount = double.Parse(dr["TranAmount"].ToString());
                //string PaymentTypeCode = dr["PaymentType"].ToString();
                string PaymentTypeCode = "2";
                string line = batchno.PadLeft(8, ' ') + "" + RecieptNo.PadLeft(10, '0') + "" + BillCode + "" + custaccount.PadLeft(10, ' ') + " " + PaymentTypeCode + "N" + ChequeNo.PadLeft(16, ' ') + "" + payDateStr + "" + postDateStr + "" + amount.ToString().PadLeft(12, ' ') + "" + "".PadLeft(4, ' ');
                tw.WriteLine(line);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            tw.Close();
        }
        string fullPath = reportFolder + "\\" + filename;
        return fullPath;
    }
    private string SpaceCreator(int x)
    {
        string s = "";
        for (int i = 0; i < x; i++)
        {
            s = s + " ";
        }
        return s;
    }

    public void RemoveFile(string Filepath)
    {
        File.Delete(Filepath);
    }
    public void EmptyFolder(DirectoryInfo directoryInfo)
    {
        foreach (FileInfo file in directoryInfo.GetFiles())
        {

            file.Delete();
        }
    }
    private bool RemoteCertValidation(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    public string GetServerStatus()
    {
        //PegPay ep = new PegPay();
        //System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertValidation;
        //string status = ep.GetServerStatus();
        string status = dalpay.GetServerStatus();
        return status;
    }
    //public Responseobj PostInternalPayment(InterLinkClass.Epayment.Transaction tr,bool sms)
    //{
    //    Responseobj ret = new Responseobj();
    //    if (!IsduplicateVendorRef(tr))
    //    {
    //        string recieptNo = dalpay.PostUmemeTransaction(tr);
    //        if (!recieptNo.Equals(""))
    //        {
    //            string res_log = dalpay.LogInternaltran(recieptNo, tr.Teller);
    //            if (res_log.Equals("LOGGED"))
    //            {
    //                // Now Reconcile the transaction
    //                string res_reconcile = Reconcile_InternalTrans(recieptNo, tr.Teller);
    //                ret.Errorcode = "0";
    //                if (res_reconcile.Equals("RECONCILED"))
    //                {
    //                    ret.Message = "Transaction Posted Successfully [" + recieptNo + "]";
    //                }
    //                else
    //                {
    //                    ret.Message = "Transaction Posted Successfully [" + tr.VendorTranId + "] but Reconciled failed, Please reconciled it";
    //                }
    //                SendSms(tr, recieptNo,sms);
    //            }
    //        }
    //        else
    //        {
    //            ret.Errorcode = "100";
    //            ret.Message = "Failure To Process Transaction Receipt number";
    //        }
    //    }
    //    else
    //    {
    //        ret.Errorcode = "20";
    //        ret.Message = dalpay.GetStatusDescr(ret.Errorcode);

    //    }
    //    return ret;
    //}
    public string Reconcile_InternalTrans(string recieptNo, string createdby)
    {
        string res = "";
        //int BatchNo = dalpay.SaveReconBatch(0, 0, 0, 0, createdby);
        //int recordid = GetRecordIDByReceipt(recieptNo);
        //if (!recordid.Equals(0))
        //{
        //    string ReconType = "MR";
        //    string source = "RECEIVED";
        //    dalpay.ReconcileTransaction(recordid, BatchNo, source, ReconType, createdby);
        //    dalpay.SaveReconBatch(BatchNo, 1, 0, 1, createdby);
        //    res = "RECONCILED";
        //}
        //else
        //{
        //    res = "NOT RECONCILED";
        //}
        return res;
    }
    //public bool IsduplicateVendorRef(InterLinkClass.Epayment.Transaction trans)
    //{
    //    bool ret = false;
    //    DataTable dt = dalpay.GetDuplicateVendorRef(trans);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ret = true;
    //    }
    //    else
    //    {
    //        ret = false;
    //    }
    //    return ret;
    //}
    private int GetRecordIDByReceipt(string recieptNo)
    {
        int trans_Id = 0;
        datatable = dalpay.GetTransDetailsByReceipt(recieptNo);
        if (datatable.Rows.Count > 0)
        {
            string transno = datatable.Rows[0]["TranId"].ToString();
            trans_Id = int.Parse(transno);
        }
        return trans_Id;
    }
    //public static string SignCertificate(string text, string districtcode)
    //{
    //    // Open certificate store of current user
    //    //X509Store my = new X509Store(StoreName.My, StoreLocation.CurrentUser);
    //    X509Store my = new X509Store(districtcode, StoreLocation.LocalMachine);
    //    my.Open(OpenFlags.ReadOnly);

    //    // Look for the certificate with specific subject 
    //    RSACryptoServiceProvider csp = null;
    //    foreach (X509Certificate2 cert in my.Certificates)
    //    {
    //        //if (cert.Subject.Contains("CN=WINGROUP\\micwein"))
    //        if (cert.Subject.Contains(districtcode))
    //        {
    //            // retrieve private key
    //            csp = (RSACryptoServiceProvider)cert.PrivateKey;
    //        }
    //    }
    //    if (csp == null)
    //    {
    //        throw new Exception("Valid certificate was not found");
    //    }

    //    // Hash the data
    //    SHA1Managed sha1 = new SHA1Managed();
    //    //UnicodeEncoding encoding = new UnicodeEncoding();
    //    ASCIIEncoding encoding = new ASCIIEncoding();
    //    byte[] data = encoding.GetBytes(text);
    //    byte[] hash = sha1.ComputeHash(data);
    //    byte[] sig = csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
    //    string strSig = Convert.ToBase64String(sig);
    //    // Sign the hash
    //    return strSig;
    //}


    internal bool IsCurrentDate(DateTime date)
    {
        DateTime now = DateTime.Now;
        string str_now = now.ToString("ddMMyyyy");
        string str_date = date.ToString("ddMMyyyy");
        if (str_date.Equals(str_now))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public string isValidManualReceipt(string receiptNo, string Amt)
    {
        string ret = "";
        int receipt_no = int.Parse(receiptNo);
        double amount = double.Parse(Amt);
        string teller = HttpContext.Current.Session["UserID"].ToString();
        string districtcode = HttpContext.Current.Session["DistrictCode"].ToString();
        string district = HttpContext.Current.Session["DistrictName"].ToString();
        int tellerID = int.Parse(teller);
        dtable = dalpay.CheckReceiptRange(districtcode, tellerID);
        if (dtable.Rows.Count > 0)
        {
            int start_point = int.Parse(dtable.Rows[0]["StartOn"].ToString());
            int end_point = int.Parse(dtable.Rows[0]["EndAt"].ToString());
            int level_point = int.Parse(dtable.Rows[0]["LevelAt"].ToString());
            double bal = double.Parse(dtable.Rows[0]["Balance"].ToString());
            int new_point = level_point + 1;
            if (receipt_no < start_point)
            {
                ret = "Receipt Number[" + receiptNo + "] is less than " + district + " range";
            }
            else if (receipt_no > end_point)
            {
                ret = "Receipt Number[" + receiptNo + "] is out of " + district + " range";
            }
            else
            {
                if (receipt_no < level_point)
                {
                    ret = "Duplicate Receipt Number[" + receiptNo + "]";
                }
                else if (receipt_no != new_point)
                {
                    ret = "Unexpected Receipt Number[" + receiptNo + "], Expecting " + new_point;
                }
                else if (amount > bal)
                {
                    ret = amount.ToString("#,##0") + " is greater than the balance on the receipt range";
                }
                else
                {
                    ret = "YES";
                }
            }
        }
        else
        {
            ret = "No Active Manual Receipt Range in System for " + district + " Please Contact Your Supervisor";
        }
        return ret;
    }
    internal string ReceiptRangeExists(int recordId, int startpoint, int endpoint, string districtcode)
    {
        string ret = "";
        datatable = dalpay.CheckReceiptRange(districtcode, 0);
        if (datatable.Rows.Count > 0)
        {
            int record = int.Parse(datatable.Rows[0]["RecordID"].ToString());
            int start = int.Parse(datatable.Rows[0]["StartOn"].ToString());
            int end = int.Parse(datatable.Rows[0]["EndAt"].ToString());
            if (recordId == record)
            {
                dtable = dalpay.GetFormerReceipt(districtcode, recordId);
                if (dtable.Rows.Count > 0)
                {
                    int formerStart = int.Parse(dtable.Rows[0]["StartOn"].ToString());
                    int formerEnd = int.Parse(dtable.Rows[0]["EndAt"].ToString());
                    if (startpoint <= formerEnd)
                    {
                        ret = startpoint.ToString() + " Starting Point is already in another range";
                    }
                    else if (endpoint <= formerEnd)
                    {
                        ret = endpoint.ToString() + " Ending Point is already in the System";
                    }
                    else
                    {
                        ret = "YES";
                    }
                }
                else
                {
                    ret = "YES";
                }
            }
            else
            {
                if (startpoint <= start)
                {
                    ret = startpoint.ToString() + " Starting Point is already in the System";
                }
                else if (endpoint <= end)
                {
                    ret = endpoint.ToString() + " Ending Point is already in the System";
                }
                else if (startpoint <= end)
                {
                    ret = startpoint.ToString() + " Start Point is already in another range";
                }
                else
                {
                    ret = "YES";
                }
            }
        }
        else
        {
            ret = "YES";
        }
        return ret;
    }

    internal string RangeIn(string districtcode, int cashier, int record_id)
    {
        string ret = "";
        dtable = dalpay.CheckReceiptRange(districtcode, cashier);
        if (dtable.Rows.Count > 0)
        {
            bool is_active = bool.Parse(dtable.Rows[0]["Active"].ToString());
            int recordId = int.Parse(dtable.Rows[0]["RecordID"].ToString());
            if (record_id.Equals(recordId))
            {
                ret = "NO";
            }
            else if (is_active)
            {
                ret = "THERE IS AN ACTIVE RECEIPT RANGE IN THE SYSTEM";
            }
            else
            {
                ret = "NO";
            }
        }
        else
        {
            ret = "NO";
        }
        return ret;
    }
    public string PasswdString(int size)
    {
        StringBuilder builder = new StringBuilder();
        Random random = new Random();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }

        return builder.ToString();
    }


    internal bool IsSubEmail(string email)
    {
        datatable = datafile.GetErrorSubByEmail(email);
        if (datatable.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    internal bool IsSubPhone(string phone)
    {
        datatable = datafile.GetErrorSubByPhone(phone);
        if (datatable.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public string FormatPhoneNumber(string number)
    {
        if (number.Length == 9)
        {
            number = "256" + number;
        }
        else if (number.Length == 10 && number.StartsWith("0"))
        {
            number = number.Remove(0, 1);
            number = "256" + number;
        }
        else if (number.StartsWith("+") && number.Length == 13)
        {
            number = number.Remove(0, 1);
        }
        return number;
    }

    public string IsPerpaymentVendorDetails(Merchant merchant)
    {
        string res = "";
        if (!merchant.Active)
        {
            res = "OK";
        }
        else
        {
            if (merchant.ClientId.Equals(""))
            {
                res = "Please Provide Prepayment Vending Client ID";
            }
            else if (merchant.TerminalId.Equals(""))
            {
                res = "Please Provide Prepayment Vending Terminal ID";
            }
            else if (merchant.OperatorId.Equals(""))
            {
                res = "Please Provide Prepayment Vending Operator ID";
            }
            else if (merchant.Password.Equals(""))
            {
                res = "Please Provide Prepayment Vending Password";
            }
            else
            {
                res = "OK";
            }
        }
        return res;
    }

    public bool IsDuplicateNumber(string Phone)
    {
        if (Phone.Equals(""))
        {
            return false;
        }
        else
        {
            //string ePhone = Encryption.encrypt.EncryptString(formatPhone(Phone), "10987654321ipegpay12345678910");
            dtable = datafile.GetDuplicateNumber(Phone);
            if (dtable.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                string Name = dtable.Rows[0]["FullName"].ToString();
                return true;

            }
        }
    }

    public string formatPhone(string phoneString)
    {
        string output = "";
        phoneString = Regex.Replace(phoneString, @"\s", "");
        int len = phoneString.Length;
        if (len.Equals(13) && phoneString.StartsWith("+256"))
        {
            // 0772020124
            string Sub = phoneString.Substring(4, 9);
            output = "256" + Sub;
        }
        else if (len.Equals(12) && phoneString.StartsWith("256"))
        {
            output =  phoneString;
        }
        else if (len.Equals(10) && phoneString.StartsWith("0"))
        {
            // 0772020124
            string Sub = phoneString.Substring(1, 9);
            output = "256" + Sub;
        }
        else if (len.Equals(9))
        {
            output = "256" + phoneString;
        }
        else
        {
            output = "";
        }
        return output;
    }

    public string EmailExists(string Email)
    {
        string output = "";
        try
        {
            dtable = datafile.GetCustomerByEmail(Email);
            if (dtable.Rows.Count > 0)
            {
                string fullName = dtable.Rows[0]["fullname"].ToString();
                output = "Email already registered aganist " + fullName;
            }
            else
            {
                output = "OK";
            }
            return output;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    public string ApproveInternetCustomer(PegPayCustomer cust, string ApprovedBy, SystemUser user)
    {
        string status = "";
        try
        {
            string status2 = datafile.ApproveCustomer(cust.ID, ApprovedBy);
            if (status2.Equals("SUCCESS"))
            {
                ProcessUsers Process = new ProcessUsers();
                string returned = Process.SaveSystemUser(user);
                if (returned.Contains("Successfully"))
                {
                    status = "SUCCESS";
                }

            }
            else
            {
                status = "FAILED";
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return status;
    }
    public string checkAccountBalance(double PayAmount, string CustomerAccount, string CustomerCode, string CustomerTypeCode)
    {

        try
        {
            //deo
            string ret = "";
            dtable = datafile.GetCustomerAccountInfor(CustomerCode, CustomerAccount);
            DataTable dt = new DataTable();
            dt = datafile.GetMinimumBalance(CustomerTypeCode);
            if (dtable.Rows.Count != 0)
            {
                double balance = double.Parse(dtable.Rows[0]["AccountBalance"].ToString());
                double MinimumBalance = double.Parse(dt.Rows[0]["MinimumAccountBalance"].ToString());
                if (PayAmount > (balance - MinimumBalance))
                {
                    ret = "LESS";
                }
                else
                {
                    ret = "OK";
                }
            }
            else
            {
                ret = "FAILED";
            }
            return ret;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string SignCertificate(string text, string VendorCode)
    {
        try
        {
            string strDigCert = "";
            string certificate = "";
            // retrieve private key
            if (VendorCode.Equals("TEST"))
            {
                certificate = @"C:\AirtelMoneyCerts\Pegpay-AirtelMoney.pfx";
            }
            else
            {
                certificate = @"E:\Certificates\" + VendorCode + @"\" + VendorCode + ".pfx";
            }
            //string certificate = @"C:\POSCerts\CORSU\corsu.pfx";
            X509Certificate2 cert = new X509Certificate2(certificate, "Tingate710", X509KeyStorageFlags.UserKeySet);
            RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PrivateKey;

            // Hash the data
            SHA1Managed sha1 = new SHA1Managed();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(text);
            byte[] hash = sha1.ComputeHash(data);

            // Sign the hash
            byte[] digitalCert = rsa.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
            return strDigCert = Convert.ToBase64String(digitalCert);
        }
        catch (Exception ex)
        {
            return "Signature required";
        }

    }

    public InterLinkClass.PegpayMMoney.Response SendPaymentRequest(InterLinkClass.PegpayMMoney.Transaction trans)
    {
        InterLinkClass.PegpayMMoney.Response resp = new InterLinkClass.PegpayMMoney.Response();
        try
        {

            PegPayTelecomsApi Mmoney = new PegPayTelecomsApi();
            System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertValidation;
            Mmoney.Timeout = 200000;
            resp = Mmoney.PostTransaction(trans);

        }
        catch (Exception ex)
        {
            resp.StatusCode = "000";
            resp.StatusDescription = ex.Message;
        }
        return resp;
    }
    public bool IsBeneficiaryActive(Beneficiary beneficiary)
    {
        try
        {
            dtable = datafile.CheckActiveBeneficiary(beneficiary);
            int foundRows = dtable.Rows.Count;
            if (foundRows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public bool BeneficiaryExists(Beneficiary beneficiary)
    {
        dtable = datafile.CheckBeneficiary(beneficiary);
        int foundRows = dtable.Rows.Count;
        if (foundRows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public PegPayCustomer GetCustomerDetails(string CustomerId)
    {
        try
        {
            PegPayCustomer cust = new PegPayCustomer();
            datatable = datafile.GetCustomerByID(CustomerId);
            if (datatable.Rows.Count > 0)
            {
                cust.PegpayAccountNumber = datatable.Rows[0]["PegpayAccountNumber"].ToString();
                cust.PegpayAccountBalance = datatable.Rows[0]["PegpayAccountBalance"].ToString();
                cust.Fullname = datatable.Rows[0]["Fname"].ToString() + " " + datatable.Rows[0]["Lname"].ToString();
            }
            else
            {
            }
            return cust;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public DateTime Returnstartingdate(string date)
    {
        DateTime startingdate;

        if (date == "")
        {
            startingdate = DateTime.Parse("November 1, 2012");
        }
        else
        {
            startingdate = DateTime.Parse(date);
        }

        return startingdate;
    }

    public DateTime Returnendingdate(string date)
    {
        DateTime endingdate;

        if (date == "")
        {
            endingdate = DateTime.Now;
        }
        else
        {
            endingdate = DateTime.Parse(date);
        }

        return endingdate;
    }

    public bool IsBelowIn(string LevelCode, string RoleCode, string CustomerCode)
    {
        if (RoleCode.Equals("013") || RoleCode.Equals("014") || RoleCode.Equals("015"))
        {
            if (LevelCode == "1")
            {
                return true;
            }
            else
            {
                dtable = datafile.CheckLevelBelow(LevelCode, RoleCode, CustomerCode);
                int foundRows = dtable.Rows.Count;
                if (foundRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            return true;
        }
    }
    public string SendBulkPayment(string batchCode, string schedulestatus, DateTime date, string fromAccount)
    {
        string status = "";
        try
        {
            //create a batch request for Processing
            BatchRequest batchRequest = CreateBatchRequest(batchCode, schedulestatus, date, fromAccount);

            //save batch request to queue
            OpResult saveResult = LogBatchRequestInQueue(batchRequest);


            //we cant log the batch request to the Queue
            if (saveResult.StatusCode != "0")
            {
                datafile.LogError2(batchCode, fromAccount, fromAccount, batchCode, saveResult.StatusDesc, saveResult.StatusCode);
                return "UNABLE TO SAVE APPROVED BATCH REQUEST. PLEASE TRY AGAIN LATER";
            }

            //do the other stuff we used to do in Deos days...I dont know what he wanted to archieve with all this
            string Scheduledby = HttpContext.Current.Session["UserName"].ToString();
            string CustomerCode = HttpContext.Current.Session["CustomerCode"].ToString();
            string CustomerType = HttpContext.Current.Session["CustomerTypeCode"].ToString();
            datatable = datafile.GetBatchRecords(batchCode, CustomerCode);
            double totalAmount = CalculateTotalAmount(datatable);
            double totalCharge = CalculateTotalCharge(datatable);
            double PayAmount = totalAmount + totalCharge;
            string CustomerAccount = HttpContext.Current.Session["CustomerPegasusAccount"].ToString();
            string BalanceStatus = checkAccountBalance(PayAmount, CustomerAccount, CustomerCode, CustomerType);
            if (schedulestatus.Equals("2"))
            {
                if (BalanceStatus.Equals("OK"))
                {
                    datafile.UpdatePaymentSchedule(batchCode, date, Scheduledby);
                    status = deductAccountBalance(CustomerAccount, PayAmount);
                    status = "Payment Batch Have been Scheduled successfully";
                }
                else
                {
                    status = "There is less credit on your account please recharge account";
                }
            }
            else if (schedulestatus.Equals("1"))
            {

                if (datatable.Rows.Count > 0)
                {
                    if (fromAccount.Equals("A001"))
                    {
                        if (BalanceStatus.Equals("OK"))
                        {
                            //deo
                            // string response = MakePayment(datatable,"",false,fromAccount);
                            status = deductAccountBalance(CustomerAccount, PayAmount);//"successfully Approved the Payment Batch";

                        }
                        else
                        {
                            status = "There is less credit on your account please recharge account";
                        }
                    }
                    else if (fromAccount.Equals("A002"))
                    {
                        //send using the mobilemoney account
                        InterLinkClass.PegpayMMoney.Response resp2 = new InterLinkClass.PegpayMMoney.Response();
                        ArrayList TelecomCodes = new ArrayList();
                        TelecomCodes = GetTelecomCodes(datatable);
                        foreach (string value in TelecomCodes)
                        {
                            //check if there is an account corresponding to the telecom code
                            dtable = datafile.GetAccountDetailsByName(value, CustomerCode);
                            if (dtable.Rows.Count > 0)
                            {
                                //request payment from account
                                string AccountNumber = dtable.Rows[0]["AccountNumber"].ToString();
                                //double totalAmount = CalculateTotalTelcomAmount(datatable, value);
                                //double totalCharge = CalculateTotalTelcomCharge(datatable, value);
                                totalAmount = totalAmount + totalCharge;
                                resp2 = RequestPayment(AccountNumber, totalAmount.ToString());
                                if (resp2.StatusCode.Equals("0"))
                                {
                                    bool FromTelecomAccount = true;
                                    string response = MakePayment(datatable, value, FromTelecomAccount, fromAccount);
                                }
                                else
                                {
                                    //update all payments of a particular network to not sent
                                    UpdateAllTelecomTransactions(datatable, value, resp2.StatusDescription);
                                }
                            }
                            else
                            {
                                //update all payments of a particular network
                                status = "Failed to Determine Account from which payments are to be made";
                                UpdateAllTelecomTransactions(datatable, value, status);
                            }
                        }
                    }
                    else
                    {
                        status = "Failed to Determine Account from which payment is to be made";
                    }
                }
                else
                {
                    //ShowMessage("Failed to retrive batch payment details",false);
                    status = "Failed to retrive batch payment details";
                }
            }
            else
            {
                status = "Failed to Determine schedule status";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return status;
    }

    private BatchRequest CreateBatchRequest(string batchCode, string schedulestatus, DateTime date, string fromAccount)
    {
        BatchRequest request = new BatchRequest();
        request.BatchCode = batchCode;
        request.Schedulestatus = schedulestatus;
        request.Date = date;
        request.FromAccount = fromAccount;
        return request;
    }

    private OpResult LogBatchRequestInQueue(BatchRequest batchRequest)
    {
        OpResult result = new OpResult();
        try
        {
            //log in BulkPayments queue
            MessageQueue queue;

            //log in MSMQ
            if (MessageQueue.Exists(BulkPaymentsQueue))
            {
                queue = new MessageQueue(BulkPaymentsQueue);
            }
            else
            {
                queue = MessageQueue.Create(BulkPaymentsQueue);
            }

            Message msg = new Message();
            msg.Body = batchRequest;
            msg.Label = batchRequest.BatchCode;
            msg.Recoverable = true;
            queue.Send(msg);

            //return SUCCESS
            result.StatusCode = "0";
            result.StatusDesc = "SUCCESS";
        }
        catch (Exception ex)
        {
            result.StatusCode = "100";
            result.StatusDesc = "EXCEPTION: " + ex.Message;
        }
        return result;
    }

    public OpResult LogFortebetTxntInQueue(FortuneBetRequest betRequest)
    {
        OpResult result = new OpResult();
        try
        {
            //log in BulkPayments queue
            MessageQueue queue;

            //log in MSMQ
            if (MessageQueue.Exists(FortebetQueue))
            {
                queue = new MessageQueue(FortebetQueue);
            }
            else
            {
                queue = MessageQueue.Create(FortebetQueue);
            }

            Message msg = new Message();
            msg.Body = betRequest;
            msg.Label = betRequest.PegpayId;
            msg.Recoverable = true;
            queue.Send(msg);

            //return SUCCESS
            result.StatusCode = "0";
            result.StatusDesc = "SUCCESS";
        }
        catch (Exception ex)
        {
            result.StatusCode = "100";
            result.StatusDesc = "EXCEPTION: " + ex.Message;
        }
        return result;
    }

    private string deductAccountBalance(string CustomerAccount, double PayAmount)
    {
        string response = "";
        //dtable = datafile.GetAccountBalance(CustomerAccount);
        //double AccountBalance = double.Parse(dtable.Rows[0]["AccountBalance"].ToString().Trim());
        //double totalbalance = AccountBalance - PayAmount;
        //string Status = datafile.UpdateCustomerAccountBalance(CustomerAccount, totalbalance);
        response = "successfully Approved the Payment Batch";
        return response;
    }

    private void UpdateAllTelecomTransactions(DataTable datatable, string fromTelecom, string reason)
    {
        try
        {
            PhoneValidator pv = new PhoneValidator();
            foreach (DataRow dr in datatable.Rows)
            {
                string PaymentNo = dr["PaymentNo"].ToString();
                string TelecomNumber = dr["BeneficaryAccount"].ToString();
                string TelecomCode = pv.GetNetwork(pv.FormatTelephone(TelecomNumber));
                if (TelecomCode.Equals(fromTelecom))
                {
                    datafile.UpdateTranferfileStatus(PaymentNo, "FAILED", reason);

                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string MakePayment(DataTable datatable, string TelecomCode, bool FromTelecomAccount, string fromAccount)
    {
        PhoneValidator pv = new PhoneValidator();
        string status = "";
        string Reason = "";
        try
        {
            InterLinkClass.PegpayMMoney.Transaction trans = new InterLinkClass.PegpayMMoney.Transaction();
            InterLinkClass.PegpayMMoney.Response resp = new InterLinkClass.PegpayMMoney.Response();
            int count = 0;
            int failed = 0;
            foreach (DataRow dr in datatable.Rows)
            {
                string CustomerCode = dr["CustomerCode"].ToString();
                string AccountType = "ESCROW";
                trans.CustomerName = dr["BeneficiaryName"].ToString();
                trans.ToAccount = dr["BeneficaryAccount"].ToString();
                trans.TranAmount = dr["Amount"].ToString().Replace(",", ""); //"1500";
                trans.TranCharge = dr["TranCharge"].ToString();
                trans.TranType = "PUSH";
                DataTable dt = new DataTable();
                dt = datafile.GetVendorCredentials(CustomerCode);
               // string encPassword = dt.Rows[0]["VendorPassword"].ToString();
                string encPassword = dt.Rows[0]["EncryptedPassword"].ToString();
                //encPassword = "DT0fKFXG0a98Wptgh3W7Lg==";
                string Password = DecryptString(encPassword);
                trans.VendorCode = CustomerCode;
                trans.Password = Password;
                trans.PaymentDate = dr["PaymentDate"].ToString();
                trans.VendorTranId = dr["PaymentNo"].ToString();
                string ToTelecom = pv.GetNetwork(pv.FormatTelephone(trans.ToAccount));
                string StringToSign = "PalceHolder";
                string FromAccount = GetFromAccount(CustomerCode, fromAccount, ToTelecom, AccountType);
                trans.FromAccount = FromAccount;
                trans.DigitalSignature = SignCertificate(StringToSign, trans.VendorCode);
                if (FromTelecomAccount)
                {
                    if (TelecomCode.Equals(ToTelecom))
                    {
                        resp = SendPaymentRequest(trans);
                        if (resp.StatusCode.Equals("0"))
                        {
                            count++;
                            //update payment record to sent
                            datafile.UpdateTranferfileStatus(trans.VendorTranId, "SUCCESS", Reason);
                            //deduct money off 
                        }
                        else if (resp.StatusCode.Equals("000"))
                        {
                            datafile.UpdateTranferfileStatus(trans.VendorTranId, "PENDING", Reason);
                        }
                        else
                        {
                            failed++;
                            //updatepayment record to failed
                            datafile.UpdateTranferfileStatus(trans.VendorTranId, "FAILED", resp.StatusDescription);
                        }
                    }
                }
                else
                {
                    resp = SendPaymentRequest(trans);
                    if (resp.StatusCode.Equals("0"))
                    {
                        count++;
                        //update payment record to sent
                        datafile.UpdateTranferfileStatus(trans.VendorTranId, "SUCCESS", Reason);
                        //deduct money off 
                    }
                    else
                    {
                        failed++;
                        //updatepayment record to failed
                        datafile.UpdateTranferfileStatus(trans.VendorTranId, "FAILED", resp.StatusDescription);
                    }
                }
            }
            if (failed > 0)
            {
                status = count + " Payment(s) Have been Send successfully and " + failed + " Failed";
            }
            else
            {
                status = count + " Payment(s) Have been Send successfully and";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return status;
    }

    public InterLinkClass.PegpayMMoney.Response RequestPayment(string AccountNumber, string totalAmount)
    {
        InterLinkClass.PegpayMMoney.Response resp = new InterLinkClass.PegpayMMoney.Response();
        PhoneValidator pv = new PhoneValidator();
        try
        {
            //log transaction.
            string paymentdate = DateTime.Now.ToString();
            Beneficiary beneficiary = new Beneficiary();
            string Custname = HttpContext.Current.Session["Company"].ToString();
            string CustomerCode = HttpContext.Current.Session["CompanyCode"].ToString();
            beneficiary.Code = AccountNumber;
            beneficiary.Name = Custname;
            beneficiary.Mobile = AccountNumber;//AccountNumber;
            beneficiary.PaymentDate = paymentdate;
            beneficiary.TransferType = "2";
            beneficiary.RecordedBy = HttpContext.Current.Session["UserName"].ToString();
            beneficiary.CustomerCode = HttpContext.Current.Session["CompanyCode"].ToString();
            beneficiary.TypeCode = "";
            totalAmount = totalAmount;
            beneficiary.TransferAmount = double.Parse(totalAmount);
            beneficiary.BatchCode = "";
            //save to database to get Reference for vendor transaction.
            string PayId = datafile.SaveTransferFileRecord(beneficiary);
            if (!PayId.Equals(""))
            {
                string CustomerPegasus = "PEGASUS";
                string AccountType = "ESCROW";
                string ToTelecom = pv.GetNetwork(pv.FormatTelephone(AccountNumber));
                string ToAccount = "";
                datatable = datafile.GetFromAccount(CustomerPegasus, ToTelecom, AccountType);
                if (datatable.Rows.Count > 0)
                {
                    ToAccount = datatable.Rows[0]["AccountNumber"].ToString();
                }
                InterLinkClass.PegpayMMoney.Transaction trans = new InterLinkClass.PegpayMMoney.Transaction();
                trans.CustomerName = Custname;
                trans.FromAccount = AccountNumber;
                trans.ToAccount = beneficiary.Mobile;//ToAccount;
                trans.TranAmount = totalAmount; //"1500";
                trans.TranCharge = "0";
                trans.TranType = "PUSH";
                string FromTelecom = pv.GetNetwork(pv.FormatTelephone(trans.FromAccount));
                DataTable dt = new DataTable();
                dt = datafile.GetVendorCredentials(CustomerCode);
                string encPassword = dt.Rows[0]["EncryptedPassword"].ToString();
                string Password = DecryptString(encPassword);
                trans.VendorCode = CustomerCode;
                trans.Password = Password;
                trans.PaymentDate = paymentdate;
                trans.VendorTranId = PayId;
                trans.FromTelecom = FromTelecom;
                trans.ToTelecom = FromTelecom;
                string StringToSign = trans.CustomerRef + trans.CustomerName + trans.FromTelecom + trans.ToTelecom + trans.VendorTranId + trans.VendorCode + trans.Password + trans.PaymentDate + trans.TranType + trans.PaymentCode + trans.TranAmount + trans.FromAccount + trans.ToAccount;
                trans.DigitalSignature = SignCertificate(StringToSign, trans.VendorCode);
                resp = SendPaymentRequest(trans);
                if (resp.StatusCode.Equals("0"))
                {
                    //update request to successfull
                    datafile.UpdateTranferfileStatus(PayId, "SUCCESS", "");
                }
                else if (resp.StatusCode.Equals("000"))
                {
                    datafile.UpdateTranferfileStatus(PayId, "PENDING", "");
                }
                else
                {
                    //Rollback request
                    datafile.UpdateTranferfileStatus(PayId, "FAILED", resp.StatusDescription);
                }
            }
            else
            {
                resp.StatusCode = "00";
                resp.StatusDescription = "Falied To Log Payment into the sytem";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return resp;
    }

    private ArrayList GetTelecomCodes(DataTable datatable)
    {
        ArrayList array = new ArrayList();
        PhoneValidator pv = new PhoneValidator();
        try
        {
            string telecomCode = "";
            string account = "";
            int count = 0;
            foreach (DataRow dr in datatable.Rows)
            {
                account = dr["BeneficaryAccount"].ToString();
                string checker = pv.GetNetwork(pv.FormatTelephone(account));
                if (!telecomCode.Equals(checker))
                {
                    if (count.Equals(0))
                    {
                        telecomCode = checker;
                        array.Add(checker);
                    }
                    else
                    {
                        if (!telecomCode.Equals(""))
                        {
                            telecomCode = checker;
                            array.Add(checker);
                        }
                    }
                }
                count++;
            }
            return array;
        }
        catch (Exception ex)
        {
            throw ex;
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

    private double CalculateTotalTelcomAmount(DataTable dataTable, string TelecomCode)
    {
        PhoneValidator pv = new PhoneValidator();
        double total = 0;
        string ToTelecom = "";
        foreach (DataRow dr in dataTable.Rows)
        {
            ToTelecom = pv.GetNetwork(pv.FormatTelephone(dr["BeneficaryAccount"].ToString()));
            if (ToTelecom.Equals(TelecomCode))
            {
                double amount = Convert.ToDouble(dr["Amount"].ToString());
                total += amount;
            }
        }
        return total;
    }

    private double CalculateTotalTelcomCharge(DataTable dataTable, string TelecomCode)
    {
        PhoneValidator pv = new PhoneValidator();
        string ToTelecom = "";
        double TotalCharge = 0;
        foreach (DataRow dr in dataTable.Rows)
        {
            ToTelecom = pv.GetNetwork(pv.FormatTelephone(dr["BeneficaryAccount"].ToString()));
            if (ToTelecom.Equals(ToTelecom))
            {
                double amount = Convert.ToDouble(dr["TranCharge"].ToString());
                TotalCharge += amount;
            }
        }
        return TotalCharge;
    }

    public DateTime GetScheduledate(string Date, string hour, string min, string dayState)
    {
        DateTime ScheduleDate;
        try
        {
            string newDate = DateTime.Parse(Date).ToString();
            String[] dateparts = newDate.Split(' ');
            string formated = dateparts[0] + " " + hour + ":" + min + ":" + "00" + " " + dayState;
            formated = formated.Trim();
            ScheduleDate = DateTime.Parse(formated);
            return ScheduleDate;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string SendNotification(string BatchCode)
    {
        string status = "";
        try
        {
            string Level = HttpContext.Current.Session["LevelID"].ToString();
            string RoleCode = HttpContext.Current.Session["RoleCode"].ToString();
            string CustomerCode = HttpContext.Current.Session["CustomerCode"].ToString();
            datatable = datafile.CheckOtherLevels(Level);
            if (datatable.Rows.Count > 0)
            {
                status = GetmessageToSend(datatable, BatchCode);
            }
            else
            {
                string NextRole = "";
                string LevelId = "1";
                if (RoleCode.Equals("013"))
                {
                    NextRole = "014";
                }
                else
                {
                    NextRole = "015";
                }
                datatable = datafile.GetCustomerUserByRole(CustomerCode, NextRole, LevelId);
                if (datatable.Rows.Count > 0)
                {
                    status = GetmessageToSend(datatable, BatchCode);
                }
                else
                {
                    status = "Failed To send Email Notification";
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return status;

    }

    public string GetmessageToSend(DataTable dt, string BatchCode)
    {
        SendMail mailer = new SendMail();
        string status = "SENT";
        try
        {
            string processorName = HttpContext.Current.Session["FullName"].ToString();
            foreach (DataRow dr in dt.Rows)
            {
                string name = dr["SurName"].ToString() + " " + dr["FirstName"].ToString();
                string mailto = dr["UserEmail"].ToString();
                string RoleCode = dr["RoleCode"].ToString();
                string CurrentCode = HttpContext.Current.Session["RoleCode"].ToString();
                string message = "Hello " + name + ", <br/>";
                string subject = "PEGPAY INTERFACE PAYMENT PROCESSED";
                string state = "";
                if (RoleCode.Equals("013"))
                {
                    state = "and is pending Verification";
                }
                else if (RoleCode.Equals("014"))
                {
                    state = "and is pending Approval";
                }
                else if (RoleCode.Equals("015"))
                {
                    state = "and has been Approved";
                }
                else
                {
                    state = "";
                }
                string link = datafile.GetSystemParameter(10, 24);
                message += "A payment has been processed by " + processorName + " " + state + " <br/>" + Environment.NewLine + Environment.NewLine;
                message += " Payment BatchNo: <a href=\"" + link + "\">" + BatchCode + "</a><br/>" + Environment.NewLine + Environment.NewLine;
                string res = "";
                if (!CurrentCode.Equals("015"))
                {
                    //res = mailer.GoogleMail(mailto, subject, message, name);
                    res = "SENT";
                    
                }
                else
                {
                    res = "SENT";
                }
                status = res;
            }
        }
        catch (Exception ex)
        {
            // throw ex;
        }
        return status;
    }

    public string GetFromAccount(string CustomerCode, string fromCode, string ToTelecom, string AccountType)
    {
        string fromAccount = "";
        try
        {
            //string fromCode = beneficiary.FromAccount;
            // string CustomerCode = beneficiary.CustomerCode;
            string Type = "";
            if (fromCode.Equals("A002"))
            {
                datatable = datafile.GetFromAccount(CustomerCode, ToTelecom, AccountType);
                if (datatable.Rows.Count > 0)
                {
                    fromAccount = datatable.Rows[0]["AccountNumber"].ToString();
                }
            }
            else
            {
                CustomerCode = "PEGASUS";
                datatable = datafile.GetFromAccount(CustomerCode, ToTelecom, AccountType);
                if (datatable.Rows.Count > 0)
                {
                    fromAccount = datatable.Rows[0]["AccountNumber"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
        }
        return fromAccount;
    }

    public bool AccountExists(string AccountNumber)
    {
        bool exists = false;
        try
        {
            datatable = datafile.CheckAccountDetails(AccountNumber);
            if (datatable.Rows.Count > 0)
            {
                exists = true;
            }
            else
            {
                exists = false;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return exists;
    }

    public string SaveAccountDetails(string recordId, string CompanyCode, string AccountName, string AccountNumber, string AccountType, string Network, bool Active)
    {
        try
        {
            string status = "";
            int Id = Int32.Parse(recordId);
            string recordedBy = HttpContext.Current.Session["UserName"].ToString();
            datafile.SaveAccountDetails(Id, CompanyCode, AccountName, AccountNumber, AccountType, Network, Active, recordedBy);
            status = "OK";
            return status;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string CreditAccountWithTelecomID(string CompanyCode, string Account, double Amount, string Network, string TelecomID, string reason)
    {
        try
        {
            string status = "";
            string recordedBy = HttpContext.Current.Session["UserName"].ToString();
            datafile.CreditAccountWithTelecomId(CompanyCode, Account, Amount, recordedBy, Network, TelecomID, reason);
            status = "OK";
            return status;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string CreditAccount(string CompanyCode, string Account, double Amount, string Network)
    {
        try
        {
            string status = "";
            string recordedBy = HttpContext.Current.Session["UserName"].ToString();
            datafile.CreditAccount(CompanyCode, Account, Amount, recordedBy, Network);
            status = "OK";
            return status;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public double GetCashOutCharge(double Amount, string Network)
    {
        double cashoutCharge = 0;
        try
        {
            datatable = datafile.GetCashOutCharge(Amount, Network);
            if (datatable.Rows.Count > 0)
            {
                cashoutCharge = double.Parse(datatable.Rows[0]["Charge"].ToString());
            }
            else
            {
                cashoutCharge = 0;
            }
            return cashoutCharge;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public InterLinkClass.PegpayMMoney.Response ApproveAccountCredit(int recordId)
    {
        InterLinkClass.PegpayMMoney.Response resp = new InterLinkClass.PegpayMMoney.Response();
        try
        {
            datatable = datafile.GetCreditToApproveById(recordId);
            if (datatable.Rows.Count > 0)
            {
                string CustomerAccount = datatable.Rows[0]["CustomerAccount"].ToString();
                double amount = Convert.ToDouble(datatable.Rows[0]["CreditAmount"].ToString());
                string CreditAmount = Math.Round(amount, 0).ToString();
                string Network = datatable.Rows[0]["Network"].ToString();
                string CustomerCode = datatable.Rows[0]["CustomerCode"].ToString();
                dtable = datafile.GetVendorCredentials(CustomerCode);
                //string encPassword = dtable.Rows[0]["EncryptedPassword"].ToString();
                string Password = "";// DecryptString(encPassword);
                string vendorTranId = recordId.ToString();
                resp = SendCredit(CustomerCode, Password, CustomerAccount, CreditAmount, Network, vendorTranId);

            }
            return resp;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private InterLinkClass.PegpayMMoney.Response SendCredit(string CustomerCode, string Password, string CustomerAccount, string CreditAmount, string Network, string VendorTranId)
    {
        InterLinkClass.PegpayMMoney.Response resp = new InterLinkClass.PegpayMMoney.Response();
        try
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
            System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertValidation;
            InterLinkClass.PegpayMMoney.PegPayTelecomsApi PegPay = new InterLinkClass.PegpayMMoney.PegPayTelecomsApi();
            resp = PegPay.CreditAccount(CustomerCode, Password, CustomerAccount, CreditAmount, Network, VendorTranId, ipAddress);

        }
        catch (Exception ex)
        {
            resp.StatusCode = "000";
            resp.StatusDescription = ex.Message;
        }
        return resp;
    }

    internal void SendTransaction(DataTable datatable, int cashoutfee)
    {
        try
        {
            string Network = datatable.Rows[0]["FromNetwork"].ToString();
            string VendorTranId = datatable.Rows[0]["VendorTranId"].ToString();
            string VendorCode = datatable.Rows[0]["VendorCode"].ToString();
            string PegPayId = datatable.Rows[0]["PegPayTranId"].ToString();
            if (Network.Equals("MTN"))
            {
                MOMOResponse MOMORes = new MOMOResponse();
                string message = GetMessage(VendorCode, VendorTranId);
                MOMORes = SendRequestToMOMO(datatable, message, cashoutfee);
                if (MOMORes.StatusCode.Equals("01"))
                {
                    datafile.UpdateTransactionStatus(PegPayId, MOMORes.MOMTransactionID, "SUCCESS", "", MOMORes.StatusCode);
                }
                else if (MOMORes.StatusCode.Equals("000"))
                {
                    datafile.UpdateTransactionStatus(PegPayId, "", "PENDING", MOMORes.StatusDesc, MOMORes.StatusCode);
                    datafile.LogError(VendorCode, " ", " ", VendorTranId, MOMORes.StatusDesc, MOMORes.StatusCode);
                }
                else if (MOMORes.StatusCode.Equals("513"))
                {
                    datafile.UpdateTransactionStatus(PegPayId, "", "PENDING", MOMORes.StatusDesc, MOMORes.StatusCode);
                    datafile.LogError(VendorCode, " ", " ", VendorTranId, MOMORes.StatusDesc, MOMORes.StatusCode);
                }
                else if (MOMORes.StatusCode.Equals("100"))
                {
                    datafile.UpdateTransactionStatus(PegPayId, "", "PENDING", MOMORes.StatusDesc, MOMORes.StatusCode);
                    datafile.LogError(VendorCode, "", "", VendorTranId, MOMORes.StatusDesc + " " + MOMORes.MOMTransactionID, MOMORes.StatusCode);
                }
                else
                {
                    datafile.UpdateTransactionStatus(PegPayId, "", "FAILED", MOMORes.StatusDesc, MOMORes.StatusCode);
                    datafile.reverseTransaction(PegPayId);
                    datafile.LogError(VendorCode, "", "", VendorTranId, MOMORes.StatusDesc + " " + MOMORes.MOMTransactionID, MOMORes.StatusCode);
                }
            }
            else if (Network.ToUpper().Equals("AIRTEL") || Network.Equals("WARID"))
            {
                //Process Warid /Aritel Payments
                payoutResponse AirtelPayout = new payoutResponse();
                AirtelPayout = ProcesssAirtelPayment(datatable, cashoutfee);
                if (AirtelPayout.returnCode.Equals(0))
                {
                    datafile.UpdateTransactionStatus(PegPayId, AirtelPayout.transactionID, "SUCCESS", "", "0");
                }
                else if (AirtelPayout.returnCode.Equals(100))
                {
                    datafile.UpdateTransactionStatus(PegPayId, "", "PENDING", AirtelPayout.returnMessage, AirtelPayout.returnCode.ToString());
                    datafile.LogError(VendorCode, "", "", VendorTranId, AirtelPayout.returnMessage, AirtelPayout.returnCode.ToString());
                }
                else
                {
                    datafile.UpdateTransactionStatus(PegPayId, "", "FAILED", AirtelPayout.returnMessage, AirtelPayout.returnCode.ToString());
                    datafile.reverseTransaction(PegPayId);
                    datafile.LogError(VendorCode, "", "", VendorTranId, AirtelPayout.returnMessage, AirtelPayout.returnCode.ToString());
                }
            }

            else if (Network.ToUpper().Equals("UTL"))
            {
                //Process UTL Payments
                MOMOResponse MOMORes = new MOMOResponse();
                string message = GetMessage(VendorCode, VendorTranId);
                MOMORes = SendRequestToUTL(datatable, message, cashoutfee);

                string[] res1 = MOMORes.StatusDesc.Split(new string[] { "Dial " }, StringSplitOptions.None);
                string messageRequest = res1[0];
                string[] res2 = messageRequest.Split(new string[] { "ID: " }, StringSplitOptions.None);
                string vendorTrandId = res2[1];

                if (MOMORes.StatusCode.ToString().Equals("0"))
                {
                    datafile.UpdateTransactionStatus(PegPayId, vendorTrandId, "SUCCESS", "", MOMORes.StatusCode);
                }

                else if (MOMORes.StatusCode.Equals("100"))
                {
                    datafile.UpdateTransactionStatus(PegPayId, "", "PENDING", MOMORes.StatusDesc, MOMORes.StatusCode);
                    datafile.LogError(VendorCode, "", "", VendorTranId, MOMORes.StatusDesc, MOMORes.StatusCode);
                }
                else
                {
                    datafile.UpdateTransactionStatus(PegPayId, "", "FAILED", MOMORes.StatusDesc, MOMORes.StatusCode);
                    datafile.reverseTransaction(PegPayId);
                    datafile.LogError(VendorCode, "", "", VendorTranId, MOMORes.StatusDesc, MOMORes.StatusCode);
                }
            }
            else
            {
                datafile.UpdateTransactionStatus(PegPayId, "", "FAILED", "CANNOT SEND MONEY TO TELECOM: " + Network.ToUpper(), "22");
                datafile.reverseTransaction(PegPayId);

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private payoutResponse ProcesssAirtelPayment(DataTable dt, int CashoutFee)
    {
        payoutResponse PayoutResp = new payoutResponse();
        try
        {
            string Network = dt.Rows[0]["FromNetwork"].ToString().ToUpper();
            string VendorCode = dt.Rows[0]["VendorCode"].ToString();

            UtilityCreds creds = new UtilityCreds();
            ThirdpartyWebServices service = new ThirdpartyWebServices();
            System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertValidation;
            string billerCode = "";//datafile.GetSystemParameter(9, 21); //"PEGASUS";
            string payoutPhone = ""; //datafile.GetSystemParameter(9, 22); //"256704008823";
            string payoutPhonepin = "";//datafile.GetSystemParameter(9, 23);//"1234";

            creds = GetVendorSenderId(VendorCode, Network);

            billerCode = creds.SenderId;
            payoutPhone = creds.SpId;
            payoutPhonepin = creds.Password;

            string Recipient = dt.Rows[0]["Phone"].ToString();
            double total = Convert.ToDouble(dt.Rows[0]["TranAmount"].ToString()) + CashoutFee;
            string Amount = Convert.ToInt64(total.ToString()).ToString();

            string RequestId = dt.Rows[0]["PegPayTranId"].ToString();
            string PaymentMemo = RequestId;
            string Tosign = billerCode + payoutPhone + Recipient + Amount + PaymentMemo + payoutPhonepin + RequestId;
            string transignature = GetAirtelMoneySignature(Tosign);
            PayoutResp = service.PayOut(billerCode, payoutPhone, payoutPhonepin, transignature, Recipient, Amount, PaymentMemo, RequestId);
        }
        catch (Exception ex)
        {
            PayoutResp.returnCode = 100;
            PayoutResp.returnMessage = ex.Message;
            PayoutResp.transactionID = "";
        }
        return PayoutResp;
    }

    private string GetAirtelMoneySignature(string Tosign)
    {
        string certificate = @"C:\AirtelMoneyCerts\Pegpay-AirtelMoney.pfx";
        X509Certificate2 cert = new X509Certificate2(certificate, "Tingate710", X509KeyStorageFlags.UserKeySet);
        RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PrivateKey;

        // Hash the data
        SHA1Managed sha1 = new SHA1Managed();
        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] data = encoding.GetBytes(Tosign);
        byte[] hash = sha1.ComputeHash(data);

        // Sign the hash
        byte[] digitalCert = rsa.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
        string strDigCert = Convert.ToBase64String(digitalCert);
        return strDigCert;
    }

    private string GetMessage(string VendorCode, object VendorTranId)
    {
        try
        {
            string Message = "";
            DataTable dt = new DataTable();
            dt = datafile.GetCustomerName(VendorCode);
            if (dt.Rows.Count == 1)
            {
                string name = dt.Rows[0]["Company"].ToString();
                if (name.ToUpper().Contains("WAVE"))
                {
                    Message = "Pegasus Payment: " + VendorTranId;
                }
                else
                {
                    Message = name + " Payment: " + VendorTranId;
                }
            }
            else
            {
                Message = "Pegasus Payment: " + VendorTranId;

            }
            return Message;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal MOMOResponse SendRequestToMOMO(DataTable dt, string message, int CashoutFee)
    {
        MOMOResponse MoMores = new MOMOResponse();
        string TranType = dt.Rows[0]["TranType"].ToString();
        try
        {
            string myUrl = "";
            string RequestXML = "";
            if (TranType.Equals("2"))
            {
                RequestXML = GetRechargeXml(dt, message, CashoutFee);
                //myUrl = "http://193.108.214.105:8310/ThirdPartyServiceUMMImpl/UMMServiceService/DepositMobileMoney/v15/";
                myUrl = "http://172.25.48.36:8310/ThirdPartyServiceUMMImpl/UMMServiceService/DepositMobileMoney/v15/";
            }
            else if (TranType.Equals("1"))
            {
                //RequestXML = GetRequestPaymentXml(trans, PegPayId);
                //myUrl = "http://193.108.214.105:8310/ThirdPartyServiceUMMImpl/UMMServiceService/RequestPayment/v15/";
                myUrl = "http://172.25.48.36:8310/ThirdPartyServiceUMMImpl/UMMServiceService/RequestPayment/v15";
            }
            System.Net.ServicePointManager.Expect100Continue = false;
            WebRequest r = WebRequest.Create(myUrl);
            r.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(RequestXML);
            r.ContentType = "application/x-www-form-urlencoded";
            r.ContentLength = byteArray.Length;
            r.Timeout = 180000;
            Stream dataStream = r.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            WebResponse response = r.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader rdr = new StreamReader(dataStream);
            string feedback = rdr.ReadToEnd();
            if (TranType.Equals("2"))
            {
                MoMores = ParseDepositXmlResponse(feedback);
            }
            else if (TranType.Equals("1"))
            {
                MoMores = ParseRequestXmlResponse(feedback);
            }



        }
        catch (Exception ex)
        {
            MoMores.StatusCode = "000";
            MoMores.StatusDesc = ex.Message;
            //throw ex;
        }
        return MoMores;
    }

    internal MOMOResponse SendRequestToUTL(DataTable dt, string message, int CashoutFee)
    {
        MOMOResponse MoMores = new MOMOResponse();
        string TranType = dt.Rows[0]["TranType"].ToString();
        try
        {
            string username = "pegasus_live";
            string password = "w4LaASeE";
            string billProvider = "PEGASUS";
            string account = "", token = "";
            string[] accNumber = GetPegasusUtlOvaAccountNumberAndToken();

            account = accNumber[0].ToString();
            token = accNumber[1].ToString();


            string url = "http://apimsente.utl.co.ug:9081/MSENTEwebService/MsenteTransactionEndpoint?wsdl";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.KeepAlive = false;

            string postData = "<x:Envelope xmlns:x='http://schemas.xmlsoap.org/soap/envelope/' xmlns:int='http://interfac.msente.tlc.ph.com/'>" +
                                 "<x:Header/>" +
                                 "<x:Body>" +
                                     "<int:doTransferMoney>" +
                                         "<MsenteTransaction>" +
                                             "<token>" + token + "</token>" +
                                             "<customerData>" + dt.Rows[0]["Phone"].ToString() + "</customerData>" +
                                             "<amount>" + Decimal.Parse(dt.Rows[0]["TranAmount"].ToString() + CashoutFee.ToString()) + "</amount>" +
                                             "<operationId>" + dt.Rows[0]["PegPayTranId"].ToString() + "</operationId>" +
                                             "<operationDesc>" + message + "</operationDesc>" +
                                             "<billDetails>" +
                                                 "<billProvider>PEGASUS</billProvider>" +
                                                 "<acctNumber>" + account + "</acctNumber>" +
                                                 "<data1></data1>" +
                                                 "<data2></data2>" +
                                             "</billDetails>" +
                                             "<refTransID></refTransID>" +
                                         "</MsenteTransaction>" +
                                     "</int:doTransferMoney>" +
                                 "</x:Body>" +
                                 "</x:Envelope>";

            string toBeHashed = username + ":" + password;
            string passwordDigest = Base64Encode(toBeHashed);

            request.Method = "POST";
            request.Headers.Add("Authorization:Basic " + passwordDigest);
            request.Headers["username"] = username;
            request.Headers["password"] = password;
            request.ContentType = "text/xml; charset=utf-8";

            StreamWriter sw = new StreamWriter(request.GetRequestStream()); // Wrap the request stream with a text-based writer                   
            byte[] data = Encoding.ASCII.GetBytes(postData);

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            string xmlResponse = "Response from UTL at " + DateTime.Now + Environment.NewLine + responseString;

            string xmlString = responseString;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            try
            {
                XmlNodeList _status_description = doc.GetElementsByTagName("message");
                XmlNodeList _status_code = doc.GetElementsByTagName("result");

                MoMores.StatusCode = _status_code[0].InnerText.ToString();
                MoMores.StatusDesc = _status_description[0].InnerText.ToString();

            }
            catch (Exception ex)
            {
                MoMores.StatusCode = "100";
                MoMores.StatusDesc = ex.Message.ToString();
            }

        }
        catch (Exception ex)
        {
            MoMores.StatusCode = "100";
            MoMores.StatusDesc = ex.Message.ToString();
        }

        return MoMores;
    }

    public static string Base64Encode(string plainText)
    {
        byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    private static string[] GetPegasusUtlOvaAccountNumberAndToken()
    {
        string[] rows = File.ReadAllLines(@"E:\PePay\MoMo\Mr ronalds Momo\New Telecoms API\TelecomsApi\UtlAccountNumber.txt");
        List<String> list = new List<String>();

        foreach (string row in rows)
        {
            string[] rowlist = row.Split(',');
            string AccNumber = rowlist[0].ToString();
            string token = @rowlist[1].ToString();
            list.Add(AccNumber);
            list.Add(token);
        }

        return list.ToArray();
    }

    private MOMOResponse ParseRequestXmlResponse(string feedback)
    {
        try
        {
            MOMOResponse resp = new MOMOResponse();
            XmlDocument XmlDeposit = new XmlDocument();
            XmlDeposit.LoadXml(feedback);
            XmlNodeList returns = XmlDeposit.GetElementsByTagName("return");
            XmlNodeList returnname = XmlDeposit.GetElementsByTagName("name");
            XmlNodeList returnvalue = XmlDeposit.GetElementsByTagName("value");
            int i = 0;
            foreach (XmlNode returnnode in returns)
            {
                string returnName = returnname[i].InnerText.Trim();
                string returnValue = returnvalue[i].InnerText.Trim();
                switch (returnName)
                {
                    case "ProcessingNumber":
                        resp.ProcessingNumber = returnValue;
                        break;

                    case "StatusCode":
                        resp.StatusCode = returnValue;
                        break;

                    case "StatusDesc":
                        resp.StatusDesc = returnValue;
                        break;

                    case "MOMTransactionID":
                        resp.MOMTransactionID = returnValue;
                        break;

                    case "ThirdPartyAcctRef":
                        resp.ThirdPartyAcctRef = returnValue;
                        break;

                    case "SenderID":
                        resp.SenderID = returnValue;
                        break;

                }
                i++;
            }
            return resp;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private MOMOResponse ParseDepositXmlResponse(string feedback)
    {
        try
        {
            MOMOResponse resp = new MOMOResponse();
            XmlDocument XmlDeposit = new XmlDocument();
            XmlDeposit.LoadXml(feedback);
            XmlNodeList returns = XmlDeposit.GetElementsByTagName("return");
            XmlNodeList returnname = XmlDeposit.GetElementsByTagName("name");
            XmlNodeList returnvalue = XmlDeposit.GetElementsByTagName("value");
            int i = 0;
            foreach (XmlNode returnnode in returns)
            {
                string returnName = returnname[i].InnerText.Trim();
                string returnValue = returnvalue[i].InnerText.Trim();
                switch (returnName)
                {
                    case "ProcessingNumber":
                        resp.ProcessingNumber = returnValue;
                        break;

                    case "StatusCode":
                        resp.StatusCode = returnValue;
                        break;

                    case "StatusDesc":
                        resp.StatusDesc = returnValue;
                        break;

                    case "MOMTransactionID":
                        resp.MOMTransactionID = returnValue;
                        break;
                }
                i++;
            }
            return resp;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private string GetRechargeXml(DataTable dt, string message, int CashoutCharge)
    {
        DataLogin dl = new DataLogin();

        UtilityCreds creds = new UtilityCreds();
        string VendorCode = dt.Rows[0]["VendorCode"].ToString();
        string Network = dt.Rows[0]["FromNetwork"].ToString();

        creds = GetVendorSenderId(VendorCode, Network);

        //string spId = dh.GetSystemParameter(1, 1);
        string spId = creds.SpId.Trim();

        // string password = dh.GetSystemParameter(1, 2);
        string password = creds.Password.Trim();

        string serviceId = dl.GetSystemParameters(2, 13);

        // string SenderId = dh.GetSystemParameter(3, 14);

        string SenderId = creds.SenderId.Trim();



        //string spId = datafile.GetSystemParameter(1, 1);
        //string password = datafile.GetSystemParameter(1, 2);
        //string serviceId = datafile.GetSystemParameter(2, 13);
        //string SenderId = datafile.GetSystemParameter(3, 14);
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        string spPassword = CalculateMD5Hash(spId + password + timestamp);
        string OrderDateTime = DateTime.Now.ToString();
        string DueAmount = dt.Rows[0]["TranType"].ToString();
        string MSISDNNum = dt.Rows[0]["Phone"].ToString();
        string ProcessingNumber = dt.Rows[0]["PegPayTranId"].ToString();
        string AcctRef = "";
        string AcctBalance = dt.Rows[0]["TranAmount"].ToString();
        double total = Convert.ToDouble(dt.Rows[0]["TranAmount"].ToString().Trim()) + CashoutCharge;
        string Amount = Convert.ToInt64(total.ToString()).ToString();
        string Narration = message;
        string PrefLang = "EN";
        string OpCoID = "25601";
        string CurrCode = "UGX";
        string rechargeXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\""
+ " xmlns:b2b=\"http://b2b.mobilemoney.mtn.zm_v1.0/\">"
+ "<soapenv:Header>"
+ "<RequestSOAPHeader xmlns=\"http://www.huawei.com.cn/schema/common/v2_1\">"
+ "<spId>" + spId + "</spId>"
 + "<spPassword>" + spPassword + "</spPassword>"
 + "<serviceId></serviceId>"
 + "<timeStamp>" + timestamp + "</timeStamp>"
+ "</RequestSOAPHeader>"
+ "</soapenv:Header>"
+ "<soapenv:Body>"
+ "<b2b:processRequest>"
 + "<serviceId>102" + "</serviceId>"
 + "<parameter>"
    + "<name>ProcessingNumber</name>"
    + "<value>" + ProcessingNumber + "</value>"
 + "</parameter>"
 + "<parameter>"
    + "<name>serviceId</name>"
    + "<value>" + SenderId + "</value>"
 + "</parameter>"
 + "<parameter>"
    + "<name>SenderID</name>"
    + "<value>" + "0" + "</value>"
 + "</parameter>"
 + "<parameter>"
    + "<name>PrefLang</name>"
    + "<value>" + PrefLang + "</value>"
 + "</parameter>"
+ "<parameter>"
    + "<name>OpCoID</name>"
    + "<value>" + OpCoID + "</value>"
 + "</parameter>"
 + "<parameter>"
    + "<name>MSISDNNum</name>"
    + "<value>" + MSISDNNum + "</value>"
 + "</parameter>"
 + "<parameter>"
    + "<name>Amount</name>"
    + "<value>" + Amount + "</value>"
 + "</parameter>"
 + "<parameter>"
    + "<name>Narration</name>"
    + "<value>" + Narration + "</value>"
 + "</parameter>"
 + "<parameter>"
    + "<name>CurrCode</name>"
    + "<value>" + CurrCode + "</value>"
 + "</parameter>"
 + "<parameter>"
    + "<name>IMSINum</name>"
    + "<value></value>"
 + "</parameter>"
 + "<parameter>"
    + "<name>OrderDateTime</name>"
    + "<value>" + OrderDateTime + "</value>"
 + "</parameter>"
+ "</b2b:processRequest>"
+ "</soapenv:Body>"
+ "</soapenv:Envelope>";
        return rechargeXml;
    }

    private static string CalculateMD5Hash(string input)
    {
        // step 1, calculate MD5 hash from input
        MD5 md5 = MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);

        // step 2, convert byte array to hex string
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }


    public string GetVendorPegPayAccount(string VendorCode)
    {
        //DataTable datatable = new DataTable();
        string Account = "";
        try
        {
            datatable = datafile.GetVendorPegPayAccount(VendorCode);
            if (datatable.Rows.Count > 0)
            {
                Account = datatable.Rows[0]["AccountNumber"].ToString();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Account;
    }


    public bool IsDateValid(string dateTime)
    {
        bool vaild = false;
        DateTime Out;
        try
        {
            if (dateTime.Equals(""))
            {
                vaild = true;
            }
            else
            {
                vaild = DateTime.TryParse(dateTime, out Out);
            }
            return vaild;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string SaveCustomerKYC(CustomerKYC cusKyc)
    {
        string status = "";
        try
        {
            cusKyc.CreatedBy = HttpContext.Current.Session["UserName"].ToString();
            status = datafile.SaveCustomerKYC(cusKyc);

        }
        catch (Exception ex)
        {
            status = "FAILED";
        }
        return status;
    }

    public string SaveCustomerCredentails(string VendorCode)
    {
        string status = "";
        try
        {
            string Password = GetVendorCredentials(VendorCode);
            if (!Password.Equals(""))
            {
                datafile.SaveCustomerCredentials(VendorCode, Password);
                status = "OK";
            }
            else
            {
                status = "FAILED";

            }
            return status;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string SavePOSAccountDetails(CustomerKYC kyc)
    {
        string status = "";
        try
        {
            datafile.SavePOSAccountDetails(kyc);
            status = "OK";

            return status;
        }
        catch (Exception ex)
        {
            status = "FAILED";
            //throw ex;
        }
        return status;
    }
    private string GetVendorCredentials(string vendorCode)
    {
        string vendorPassword = "";
        try
        {
            DataTable dt = new DataTable();
            dt = datafile.GetVendorCredentials(vendorCode);
            if (dt.Rows.Count != 0)
            {
                string encVendorPassword = dt.Rows[0]["VendorPassword"].ToString();
                vendorPassword = DecryptString(encVendorPassword);
            }
            else
            {
                vendorPassword = "";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return vendorPassword;
    }

    public bool DeviceExists(string AgentId)
    {
        bool exists = true;
        try
        {
            datatable = datafile.GetDeviceById(AgentId);
            if (datatable.Rows.Count > 0)
            {
                exists = true;
            }
            else
            {
                exists = false;
            }
            return exists;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string SaveDeviceDetails(string RecordId, string AgentId, string AgentName, string AgentAddress, string OwnerId, bool Active, string DeviceSerial, string DeviceDataSim, string DeviceType, string AgentTel)
    {
        string status = "";
        try
        {
            string createdBY = HttpContext.Current.Session["UserName"].ToString();
            datafile.SaveDeviceDetails(RecordId, AgentId, AgentName, AgentAddress, OwnerId, Active, DeviceSerial, DeviceDataSim, DeviceType, createdBY, AgentTel);
            status = "OK";
            return status;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string CheckIfExistAtPegasusPushPull(string vendorref)
    {
        string tableFoundIn = "";
        string FindStatus = "";
        try
        {
            datatable = datafile.CheckAtPagasusPushPull(vendorref);

            if (datatable.Rows.Count > 0)
            {

                foreach (DataRow dr in datatable.Rows)
                {
                    tableFoundIn = dr["Status"].ToString();
                }

                FindStatus = tableFoundIn;
            }
            else
            {

                FindStatus = "";
            }

        }
        catch (Exception ex)
        {

        }

        return FindStatus;
    }

    public string CheckIfExistAtPegasus(string vendorref, out string status)
    {
        string tranStatus = "";
        string tableFoundIn = "";
        string FindStatus = "";
        try
        {
            datatable = datafile.CheckAtPagasus(vendorref);

            if (datatable.Rows.Count > 0)
            {

                foreach (DataRow dr in datatable.Rows)
                {
                    tableFoundIn = dr["PegasusStatus"].ToString();
                    tranStatus = dr["Status"].ToString();
                }

                FindStatus = tableFoundIn;
            }
            else
            {

                FindStatus = "";
            }

        }
        catch (Exception ex)
        {

        }
        status = tranStatus;
        return FindStatus;
    }

    private static UtilityCreds GetVendorSenderId(string VendorCode, string Network)
    {
        UtilityCreds creds = new UtilityCreds();
        try
        {
            DataLogin dh = new DataLogin();
            DataTable dtable = new DataTable();

            dtable = dh.GetVendorSenderId(VendorCode, Network);
            if (dtable.Rows.Count > 0)
            {
                //SenderId = dtable.Rows[0]["SenderId"].ToString();
                creds.SenderId = dtable.Rows[0]["SenderId"].ToString();
                creds.Password = dtable.Rows[0]["Password"].ToString();
                creds.SpId = dtable.Rows[0]["SpId"].ToString();
            }
            else
            {
                if (Network.ToUpper().Equals("MTN"))
                {

                    creds.SenderId = dh.GetSystemParameter(3, 14);
                    creds.Password = dh.GetSystemParameter(1, 2);
                    creds.SpId = dh.GetSystemParameter(1, 1);

                }
                else if (Network.ToUpper().Equals("AIRTEL"))
                {

                    creds.SenderId = dh.GetSystemParameter(9, 21); //"PEGASUS";
                    creds.SpId = dh.GetSystemParameter(9, 22); //"256704008823";
                    creds.Password = dh.GetSystemParameter(9, 23);//"1234";           
                }
                else
                {

                    creds.SenderId = ""; //"PEGASUS";
                    creds.SpId = "";//"256704008823";
                    creds.Password = "";//"1234";   
                }

                //SenderId = dh.GetSystemParameter(3, 14);

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return creds;
    }

    public List<string> GetChargeType(string customerCode)
    {
        DataLogin dh = new DataLogin();
        List<string> result = new List<string>();
        string chargeType = "";
        string charge = "0";
        try
        {
            dtable = dh.GetChargeType(customerCode);

            chargeType = dtable.Rows[0]["ChargeType"].ToString();
            charge = dtable.Rows[0]["PegasusCharge"].ToString();

            result.Add(chargeType);
            result.Add(charge);

        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public string DebitClientAccountBalance(string VendorTranld, string VendorCode, string AgentAccountNumber, double Amount, double PegasusAccountNumber, string PegasusAccountNetwork)
    {
        try
        {
            string status = "";
            string recordedBy = HttpContext.Current.Session["UserName"].ToString();
            datafile.DebitClientAccountBalanceWithAmount(VendorTranld, VendorCode, AgentAccountNumber, Amount, PegasusAccountNumber, PegasusAccountNetwork);
            status = "OK";
            return status;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string CheckIfVasTranExistAtPegasus(string vendorref)
    {
        string tableFoundIn = "";
        string FindStatus = "";
        try
        {
            datatable = datafile.CheckVasTranAtPagasus(vendorref);

            if (datatable.Rows.Count > 0)
            {

                foreach (DataRow dr in datatable.Rows)
                {
                    tableFoundIn = dr["PegasusStatus"].ToString();
                }

                FindStatus = tableFoundIn;
            }
            else
            {

                FindStatus = "";
            }

        }
        catch (Exception ex)
        {

        }

        return FindStatus;
    }

    public string VerifyPhoneNumberSingleBeneficiarry(string number)
    {
        string valid = "Ismaeal Kigozi";

        try
        {
            PhoneValidator pv = new PhoneValidator();
            string network = pv.GetNetwork(pv.FormatTelephone(number));
            number = "0" + number;

            if (network.Equals("AIRTEL") || network.Equals("MTN") || network.Equals("UTL"))
            {
                InterLinkClass.LevelOne.PegPayTelecomsApi momo = new InterLinkClass.LevelOne.PegPayTelecomsApi();
                InterLinkClass.LevelOne.TelephoneNumberDetails telResp = new InterLinkClass.LevelOne.TelephoneNumberDetails();

                string vendorCode = "TEST";
                string password = "47N73TI461";

                string dataToSign = number + vendorCode + password;
                string signature = GetSignature(dataToSign);


                System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertValidation;
                telResp = momo.ValidatePhoneNumber(number, vendorCode, password, signature);

                if (telResp.Status.Equals("0"))
                {
                    valid = telResp.Name;
                }
                else
                {
                    valid = "";
                }
            }
            else
            {
                valid = "";
            }
        }
        catch (Exception ex)
        {

        }
        return valid;
    }

    public bool VerifyPhoneNumber(string number)
    {
        bool valid = false;

        try
        {
            InterLinkClass.LevelOne.TelephoneNumberDetails telResp = ValidatePhoneNumber(number);

            if (telResp.Status.Equals("0"))
            {
                valid = true;
            }
            else
            {
                valid = false;
            }
        }
        catch (Exception ex)
        {

        }
        return valid;
    }

    public InterLinkClass.LevelOne.TelephoneNumberDetails ValidatePhoneNumber(string number)
    {
        PhoneValidator pv = new PhoneValidator();
        string network = pv.GetNetwork(pv.FormatTelephone(number));

        InterLinkClass.LevelOne.PegPayTelecomsApi momo = new InterLinkClass.LevelOne.PegPayTelecomsApi();
        InterLinkClass.LevelOne.TelephoneNumberDetails telResp = new InterLinkClass.LevelOne.TelephoneNumberDetails();

        string vendorCode = "TEST";
        string password = "47N73TI461";

        string dataToSign = number + vendorCode + password;
        string signature = GetSignature(dataToSign);


        System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertValidation;
        return momo.ValidatePhoneNumber(number, vendorCode, password, signature);
    }

    private static string GetSignature(string Tosign)
    {
        string certificate = @"C:\AirtelMoneyCerts\Pegpay-AirtelMoney.pfx";
        X509Certificate2 cert = new X509Certificate2(certificate, "Tingate710", X509KeyStorageFlags.UserKeySet);
        RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PrivateKey;

        // Hash the data
        SHA1Managed sha1 = new SHA1Managed();
        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] data = encoding.GetBytes(Tosign);
        byte[] hash = sha1.ComputeHash(data);

        // Sign the hash
        byte[] digitalCert = rsa.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
        string strDigCert = Convert.ToBase64String(digitalCert);
        return strDigCert;
    }

    public void UpdateStanbicTransaction(string VendorTranId, string UtilityTranRef, string Status)
    {
        DataLogin dh = new DataLogin();
        try
        {

            dh.UpdateStanbicTransaction(VendorTranId, UtilityTranRef, Status);
        }
        catch (Exception ee)
        {

        }
    }

    public void LoadSystemCompanies(string userBranch, DropDownList ddlst)
    {
        datatable = dtable = datafile.GetSystemCompanies("", "");

        ddlst.Items.Clear();
        ddlst.Items.Add(new ListItem("ALL", ""));
        foreach (DataRow dr in datatable.Rows)
        {
            string company = dr["Company"].ToString();
            string companyCode = dr["CompanyCode"].ToString();
            ddlst.Items.Add(new ListItem(company, companyCode));
        }

        if (userBranch.ToUpper() != "PEGPAY")
        {
            ddlst.SelectedValue = userBranch;
            ddlst.Enabled = false;
        }
    }

    public void LoadSystemCompanyAccount(string userBranch, DropDownList ddlst)
    {
        datatable = dtable = datafile.GetSystemCompanyAccounts("", "");

        ddlst.Items.Clear();
        ddlst.Items.Add(new ListItem("ALL", ""));
        foreach (DataRow dr in datatable.Rows)
        {
            string company = dr["AccountName"].ToString();
            string companyCode = dr["AccountNumber"].ToString();
            ddlst.Items.Add(new ListItem(company, companyCode));
        }

        if (userBranch.ToUpper() != "PEGPAY")
        {
            ddlst.SelectedValue = userBranch;
            ddlst.Enabled = false;
        }
    }

    public void LoadTelecoms(DropDownList ddlst)
    {
        datatable = datafile.GetNetworkInMobileMoneyDB();

        ddlst.Items.Clear();
        ddlst.Items.Add(new ListItem("ALL", ""));
        foreach (DataRow dr in datatable.Rows)
        {
            string telecom = dr["Network"].ToString();
            ddlst.Items.Add(new ListItem(telecom, telecom));
        }
    }
    public void LoadVendorsToReconcile(DropDownList ddlst)
    {
        datatable = datafile.GetVendorsToReconcile();

        ddlst.Items.Clear();
        ddlst.Items.Add(new ListItem("ALL", ""));
        foreach (DataRow dr in datatable.Rows)
        {
            string vendor = dr["Vendor"].ToString();
            string vendorCode = dr["VendorCode"].ToString();
            ddlst.Items.Add(new ListItem(vendor, vendorCode));
        }
    }

    public void LoadVendors(DropDownList ddlst)
    {
        datatable = datafile.GetVendorsInDB();

        ddlst.Items.Clear();
        ddlst.Items.Add(new ListItem("All ", ""));
        foreach (DataRow dr in datatable.Rows)
        {
            string vendor = dr["Vendor"].ToString();
            string vendorCode = dr["VendorCode"].ToString();
            ddlst.Items.Add(new ListItem(vendor, vendorCode));
        }
    }

    public void LoadTransactionCategories(DropDownList ddlst)
    {
        datatable = datafile.ExecuteDataSet("GetTranCategories").Tables[0];

        ddlst.Items.Clear();
        ddlst.Items.Add(new ListItem("ALL", ""));
        foreach (DataRow dr in datatable.Rows)
        {
            string category = dr["Category"].ToString();
            string categoryCode = dr["CategoryCode"].ToString();
            ddlst.Items.Add(new ListItem(category, categoryCode));
        }
    }

    public void LoadTranTypes(DropDownList ddlst)
    {
        datatable = datafile.ExecuteDataSet("GetTranType").Tables[0];

        ddlst.Items.Clear();
        ddlst.Items.Add(new ListItem("ALL", ""));
        foreach (DataRow dr in datatable.Rows)
        {
            string typeid = dr["TypeId"].ToString();
            string tranType = dr["TranType"].ToString();
            ddlst.Items.Add(new ListItem(tranType, typeid));
        }
    }

    public void LoadTranStatus(DropDownList ddlst)
    {
        datatable = datafile.ExecuteDataSet("GetTranStatuses").Tables[0];

        ddlst.Items.Clear();
        ddlst.Items.Add(new ListItem("ALL", ""));
        foreach (DataRow dr in datatable.Rows)
        {
            string status = dr["Status"].ToString();
            ddlst.Items.Add(new ListItem(status, status));
        }
    }

    public void ShowMessage(Label lblmsg, string msg, bool IsError, System.Web.SessionState.HttpSessionState Session)
    {
        lblmsg.Text = msg;
        if (IsError)
        {
            Session["IsError"] = "True";
            lblmsg.ForeColor = Color.Red;
        }
        else
        {
            Session["IsError"] = "False";
            lblmsg.ForeColor = Color.Green;
        }
    }

    public DataTable SearchVwTransactionsTable(string[] searchParams)
    {
        datatable = datafile.ExecuteDataSet("SearchVw_Transactions", searchParams).Tables[0];
        return datatable;
    }

    public DataTable AccountSearchVwTransactionsTable(string[] searchParams)
    {
        datatable = datafile.ExecuteDataSet("AccountSearchVw_Transactions", searchParams).Tables[0];
        return datatable;
    }

    public void ExportToExcel(DataTable dt, HttpResponse Response)
    {
        ExcelPackage package = new ExcelPackage();
        ExcelWorksheet ws = package.Workbook.Worksheets.Add("sheet1");

        //set heading
        int excelColumn = 1;
        foreach (DataColumn dc in dt.Columns)
        {
            ws.Cells[1, excelColumn].Value = dc.ColumnName;
            excelColumn++;
        }

        ws.Cells[1, 1, 1, dt.Columns.Count].Style.Font.Bold = true;

        int i = 2;//row position in excel sheet

        foreach (DataRow dr in dt.Rows)
        {
            int dataColumn = 1;
            int tableColumnNumber = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                ws.Cells[i, dataColumn].Value = dr[tableColumnNumber].ToString();
                dataColumn++;
                tableColumnNumber++;
            }
            i++;
        }

        package.Workbook.Properties.Title = "Attempts";
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader(
                  "content-disposition",
                  string.Format("attachment;  filename={0}", "ExcellData.xlsx"));
        Response.BinaryWrite(package.GetAsByteArray());
    }

    public void ExportToExcel(DataTable dt, string filename, HttpResponse Response)
    {
        //FileInfo newFile = new FileInfo(path);

        ExcelPackage package = new ExcelPackage();
        ExcelWorksheet ws = package.Workbook.Worksheets.Add("sheet1");

        //set heading
        int excelColumn = 1;
        foreach (DataColumn dc in dt.Columns)
        {
            ws.Cells[1, excelColumn].Value = dc.ColumnName;
            excelColumn++;
        }

        ws.Cells[1, 1, 1, dt.Columns.Count].Style.Font.Bold = true;

        int i = 2;//row position in excel sheet

        foreach (DataRow dr in dt.Rows)
        {
            int dataColumn = 1;
            int tableColumnNumber = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                ws.Cells[i, dataColumn].Value = dr[tableColumnNumber].ToString();
                dataColumn++;
                tableColumnNumber++;
            }
            i++;
        }

        package.Workbook.Properties.Title = "Attempts";
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader(
                  "content-disposition",
                  string.Format("attachment;  filename={0}", "Report.xlsx"));
        Response.BinaryWrite(package.GetAsByteArray());
    }


    public void ExportToPdf(DataTable table, string filename, HttpResponse Response)
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                //To Export all pages

                GridView gridView = new GridView();
                gridView.DataSource = table;
                gridView.DataBind();
                gridView.AllowPaging = false;

                gridView.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A2, 10f, 10f, 10f, 0f);
                iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
                iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Report.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }
        }
    }

    public string InsertIntoAuditLog(string ActionId, string ActionType, string TableName, string Company, string ModifiedBy, string Screen, string Action)
    {
        try
        {
            DataTable datatable = datafile.ExecuteDataSet("InsertIntoAuditTrail",
                                                           new string[]
                                                           {
                                                             ActionId,
                                                             ActionType,
                                                             TableName,
                                                             Company,
                                                             Screen,
                                                             ModifiedBy,
                                                             Action
                                                           }).Tables[0];
            return datatable.Rows[0][0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadReportTypesIntoDropDown(string company, DropDownList ddlst, string userRole)
    {
        string[] parameters = { company };
        DataSet ds = datafile.ExecuteDataSet("Reports_SelectAll", parameters);
        DataTable dt = ds.Tables[0];

        ddlst.Items.Clear();
        ddlst.Items.Add(new ListItem("-- Select a Report To View --", ""));
        foreach (DataRow dr in dt.Rows)
        {
            string Code = dr["ReportType"].ToString();
            string Name = dr["ReportName"].ToString();
            string roleList = dr["ReportCategory"].ToString();

            List<String> roles = new List<String>();
            roles.AddRange(roleList.Split(','));

            if (roles.Contains(userRole) || roles.Contains("ALL"))
            {
                ddlst.Items.Add(new ListItem(Name, Code));
            }
        }
    }

    public SystemReport GetReportById(string reportType, string company)
    {
        SystemReport report = new SystemReport();
        string[] parameters = { reportType, company };
        DataTable dt = datafile.ExecuteDataSet("Reports_SelectRow", parameters).Tables[0];
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            report.Company = company;
            report.CanAccess = dr["ReportCategory"].ToString();
            report.Database = dr["DbLocation"].ToString();
            report.ReportName = dr["ReportName"].ToString();
            report.ReportType = dr["ReportType"].ToString();
            report.StoredProcedure = dr["StoredProcedure"].ToString();
            report.IsDateDelimited = Boolean.Parse(dr["IsDateDelimited"].ToString());
            report.StatusCode = "0";
            report.StatusDesc = "SUCCESS";
        }
        else
        {
            report.StatusCode = "100";
            report.StatusDesc = "FAILED: REPORT TYPE NOT FOUND";
        }
        return report;
    }

    public DataTable GenerateReport(string[] searchParams, SystemReport report)
    {
        DataSet ds = datafile.ExecuteDataSet(report.StoredProcedure, searchParams);
        DataTable dt = ds.Tables[0];
        return dt;
    }

    public void LoadOvas(string telecom, DropDownList ddlst)
    {
        Datapay datapay = new Datapay();
        DataTable table = new DataTable();
        table = datapay.GetOvaAccountDetails(telecom);

        ddlst.Items.Clear();
        foreach (DataRow dr in table.Rows)
        {
            string Ova = dr["Ova"].ToString();
            string SenderId = dr["SenderId"].ToString();
            ddlst.Items.Add(new ListItem(Ova, SenderId));
        }
    }

    public object GetCurrentPageName(string path)
    {
        System.IO.FileInfo oInfo = new System.IO.FileInfo(path);
        string sRet = oInfo.Name;
        return sRet;
    }

    public string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }

    public void LoadHours(DropDownList ddlst)
    {
        ddlst.Items.Clear();
        for (int i = 0; i < 24; i++)
        {
            ddlst.Items.Add(new ListItem(i.ToString()));
        }
    }

    public void LoadBeneficiaryTypes(DropDownList ddlst)
    {
        DataTable table = datafile.GetBeneficaryType();

        ddlst.Items.Clear();
        ddlst.Items.Add(new ListItem("-- Select a type --", ""));
        foreach (DataRow dr in table.Rows)
        {
            string typeCode = dr["TypeCode"].ToString();
            string typeName = dr["TypeName"].ToString();
            ddlst.Items.Add(new ListItem(typeName, typeCode));
        }
    }

    public string GetArrayElement(string[] array, int index)
    {
        try
        {
            return array[index].ToString();
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public bool DeleteBeneficiaryById(string beneficiaryId, string reason, string deletedBy)
    {
        string[] parameters = { beneficiaryId, reason, deletedBy };
        int rowsAffected = datafile.ExecuteNonQuery("DeleteBeneficiaryById", parameters);
        if (rowsAffected > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string GetCurrentPageName()
    {
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string sRet = oInfo.Name;
        return sRet;
    }

    public string PutCommas(string p)
    {
        try
        {
            double value = Double.Parse(p);
            return value.ToString("#,##0");
        }
        catch (Exception ex)
        {
            return p;
        }
    }

    public bool IsValidOption(string value, string validList)
    {
        List<String> values = new List<String>();
        values.AddRange(validList.Split('|'));
        return values.Contains(value);
    }

    public DataTable CreateDataTable(string name, params string[] columns)
    {
        DataTable dt = new DataTable(name);
        foreach (string column in columns)
        {
            dt.Columns.Add(column);
        }
        return dt;
    }

    public DataSet ExecuteProcedure(string procedure, params object[] parameters)
    {
        try
        {
            return datafile.ExecuteDataSet(procedure, parameters);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void LoadReportTypes(string company, DropDownList ddlst, string userRole)
    {
        string[] parameters = { company };
        DataSet ds = datafile.ExecuteDataSet("Reports_SelectAll", parameters);
        DataTable dt = ds.Tables[0];

        ddlst.Items.Clear();
        ddlst.Items.Add(new ListItem("-- Select a Report To View --", ""));
        foreach (DataRow dr in dt.Rows)
        {
            string Code = dr["ReportType"].ToString();
            string Name = dr["ReportName"].ToString();
            string roleList = dr["ReportCategory"].ToString();

            List<String> roles = new List<String>();
            roles.AddRange(roleList.Split(','));

            if (roles.Contains(userRole) || roles.Contains("ALL"))
            {
                ddlst.Items.Add(new ListItem(Name, Code));
            }
        }
    }

    public void LoadUtilities(DropDownList ddlst, SystemReport report)
    {
        datatable = ExecuteDataAccess(report.Database, "GetItemsToChooseFrom", report.ReportType).Tables[0];

        ddlst.Items.Clear();
        ddlst.Items.Add(new ListItem("ALL", ""));
        foreach (DataRow dr in datatable.Rows)
        {
            string item = dr["Item"].ToString();
            string itemCode = dr["ItemCode"].ToString();
            ddlst.Items.Add(new ListItem(item, itemCode));
        }
    }

    public void LoadVendorsDynamic(DropDownList ddlst, SystemReport report)
    {
        datatable = ExecuteDataAccess(report.Database, "GetVendorsToChooseFrom", report.ReportType).Tables[0];

        ddlst.Items.Clear();
        ddlst.Items.Add(new ListItem("ALL", ""));
        foreach (DataRow dr in datatable.Rows)
        {
            string item = dr["Item"].ToString();
            string itemCode = dr["ItemCode"].ToString();
            ddlst.Items.Add(new ListItem(item, itemCode));
        }
    }

    public DataSet ExecuteDataAccess(string database, string procedure, params object[] searchParams)
    {
        InterLinkClass.DbApi.DataAccess db = new InterLinkClass.DbApi.DataAccess();
        return db.ExecuteDataSet("", database, procedure, searchParams);
    }

    public InterLinkClass.DbApi.Result ExecuteDataQuery(string database, string procedure, params object[] searchParams)
    {
        InterLinkClass.DbApi.DataAccess db = new InterLinkClass.DbApi.DataAccess();
        return db.ExecuteNonQuery("", database, procedure, searchParams);
    }

    public bool TableContainsColumn(string columnName, DataTable dt)
    {
        DataColumnCollection columns = dt.Columns;
        return columns.Contains(columnName);
    }

    public void SendMailViaPegasus(MailMessage mm)
    {
        string smtpServer = "mail.pegasus.co.ug";//192.185.83.129";//"64.233.167.108";
        int smtpPort = 587;
        string smtpPassword = "notifications@123";//"Tingate710";
        string smtpUsername = "notifications@pegasus.co.ug";//"pegpay247pegasus@gmail.com";

        mm.IsBodyHtml = true;
        mm.From = new MailAddress(smtpUsername);

        ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidation;
        //I USE GMAIL AS THE SMTP SERVER..for more info google
        NetworkCredential cred = new NetworkCredential(smtpUsername, smtpPassword);
        SmtpClient mailClient = new SmtpClient(smtpServer, smtpPort);
        mailClient.EnableSsl = true;
        mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        mailClient.UseDefaultCredentials = false;
        mailClient.Timeout = 450000;
        mailClient.Credentials = cred;
        mailClient.Send(mm);
        //SendMailNew(mm.To, mm.CC, mm.Body, mm.Subject, mm.Attachments, "notifications@pegasus.co.ug");
    }

    public bool RemoteCertificateValidation(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    public DataTable LoadDataForEdit(string RecordId)
    {
        datatable = datafile.GetVendorDataById(RecordId);
        return datatable;
    }

    public void LoadVendorsNew(DropDownList ddlst)
    {
        datatable = datafile.GetNewVendorsFromDB();

        ddlst.Items.Clear();
        ddlst.Items.Add(new ListItem("All ", ""));
        foreach (DataRow dr in datatable.Rows)
        {
            string telecom = dr["Vendor"].ToString();
            string telecom2 = dr["VendorCode"].ToString();
            ddlst.Items.Add(new ListItem(telecom, telecom2));
        }
    }
}
