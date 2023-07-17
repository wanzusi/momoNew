
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
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using InterLinkClass.MessengerApi;
using System.Collections.Generic;

public partial class ResendCreditReceipts : System.Web.UI.Page
{
    BusinessLogin bll = new BusinessLogin();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        string RoleId = Session["RoleCode"].ToString();
        if (isRoleAuthorisedToVisitPage(RoleId))
        {
           
        }
        else
        {
            Response.Redirect("UnauthorisedAccess.aspx");
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
    protected void chkTransactions_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            chkAll.Visible = true;
            //chkTransactions2.Visible = true;
            SelectAllItems();
            if (chkAll.Checked == true)
            {
                //chkTransactions2.Checked = true;
            }
            else
            {
                //chkTransactions2.Checked = false;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void SelectAllItems()
    {
        foreach (DataGridItem Items in DataGrid1.Items)
        {
            CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
            if (chk.Checked)
            {
                chk.Checked = false;
            }
            else
            {
                chk.Checked = true;
            }
        }
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
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string str = GetRecordsToApprove().TrimEnd(',');
            if (str.Equals(""))
            {
                ShowMessage("Please Select Credits to Approve", true);
            }
            else
            {
                ProcessApprovals(str);
                LoadCredits();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void ProcessApprovals(string str)
    {
        try
        {
            int suc = 0;
            int failed = 0;
            int count = 0;
            string[] arr = str.Split(',');
            int i = 0;
            string User = Session["UserName"].ToString();
            for (i = 0; i < arr.Length; i++)
            {
                int RecordId = int.Parse(arr[i].ToString());
                count++;
                CustomerReceiptCreditDetails cust = datafile.GetCustomerReceiptDetails(RecordId);
                if (cust.StatusCode.Equals("0"))
                {
                    SendReceiptToUsernameEmail(cust);
                    ShowMessage("An Email has been sent to" + cust.CustomerCode + "\t\t Vendor", true);
                }
                else
                {
                    cust.StatusCode = "100";
                    cust.StatusDescription = " NO CUSTOMER ";
                    ShowMessage("No Email has been sent to" + cust.CustomerCode + "\t\t Vendor", true);
                }

            }
            string msg = suc + " Credits Have Been Approved and " + failed + "Failed";
            datafile.LogActivity(Session["UserName"].ToString(), "Approved Account Credits Details");
            ShowMessage(msg, false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SendReceiptToUsernameEmail(CustomerReceiptCreditDetails cust)
    {

        // string custName = txtCustName.Text;
        //string CustAccount = txtCustAccount.Text;
        string AccountNumber = "";
        string AccountBalance = "";
        double AccountBalanceNo = 0;
        double CreditAmount = 0;
        DateTime todaydate = DateTime.Today.Date;
        //string bbd = todaydate.ToShortDateString();  //mm/dd/yy
        string datetoday = todaydate.ToString("dd/MM/yyyy");
        try
        {
            DataTable dtable = datafile.GetPegPayAccount(cust.CustomerCode);
            if (dtable.Rows.Count > 0)
            {
                AccountNumber = dtable.Rows[0]["AccountNumber"].ToString();
                AccountBalance = dtable.Rows[0]["AccountBalance"].ToString();
            }

            AccountBalanceNo = Convert.ToDouble(AccountBalance.Split('.')[0]);
            CreditAmount = Convert.ToDouble(cust.CustomerCreditAmount.Split('.')[0]);
            //string nn = Num2Wrd(CreditAmount);
            //string companyName = "PEGASUS CREIT RECEIPT";

            string CreditAmountInWords = ToWords(CreditAmount);
            string AccountBalanceInWords = ToWords(AccountBalanceNo);


            string AccountBalanceNo_WithCommas = AccountBalanceNo.ToString("#,##0");
            string CreditAmountWithCommas = CreditAmount.ToString("#,##0");


            StringBuilder sb = new StringBuilder();
            sb.Append("<table width='100%' cellspacing='0'  cellpadding='2' frame='box'");
            string imageFile = Server.MapPath(".") + "/Images/Receipt.png";
            sb.Append(imageFile);
            // sb.Append("<img src='E:\\PePay\\MoMo\\Production\\application\\apps\\Images\\Receipt.png' width='125' height='101' hspace='20' vspace='3'>");
            //E:\PePay\MoMo\Production\application\apps\Images\Receipt.png
            sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>PEGASUS CREDIT RECEIPT</b></td></tr>");

            sb.Append("<tr><td colspan = '4' ></td></tr>");


            sb.Append("<tr><td colspan = '5'  align='right'><b>Receipt No.</b> ");
            sb.Append(cust.ReceiptNumber);
            sb.Append("</td></tr>");



            sb.Append("<tr><td colspan = '5'   align='right'><b>Date:</b> ");
            sb.Append(datetoday);
            sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  height='4'>&nbsp;</td> </tr>");

            //sb.Append("<tr><td colspan = '5' ><b>Customer Name:</b>");
            //sb.Append(cust.CustomerCode);
            //sb.Append("</td></tr>");
            BatchCustomerDetails batchCustomer = datafile.GetCustomerBatchDetails(cust.CustomerCode);
            if (batchCustomer.StatusCode.Equals("0"))
            {
                sb.Append("<tr><td colspan = '5' ><b>Customer Name:</b>");
                sb.Append(batchCustomer.CustomerName);
                sb.Append("</td></tr>");
            }
            else
            {
                sb.Append("<tr><td colspan = '5' ><b>Customer Name:</b>");
                sb.Append(cust.CustomerCode);
                sb.Append("</td></tr>");

            }

            //sb.Append("<tr><td colspan = '4'><b>CustomerAccount :</b> ");
            //sb.Append(cust.CustomerAccount);
            //sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td colspan = '5'><b>Credited Amount :</b> ");
            sb.Append(CreditAmountWithCommas + "/" + "=");
            sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td colspan = '5' ><b>Credited Amount In Words :</b> ");
            sb.Append(CreditAmountInWords + "\t  Uganda Shillings Only");
            sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td colspan = '5' ><b>Account Balance :</b> ");
            sb.Append(AccountBalanceNo_WithCommas + "/" + "=");
            sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td colspan = '5' ><b>Account Balance in Words :</b> ");
            sb.Append(AccountBalanceInWords + "\t  Uganda Shillings Only");
            sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");
            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '7'><b>KIND REGARDS <br>PEGASUS TECHNOLOGIES LIMITED</br></b></td></tr>");

            sb.Append("<tr><td colspan = '4' ></td></tr>");
            sb.Append("</table>");



            StringReader sr = new StringReader(sb.ToString());


            Document myDoc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 50, 50);
            HTMLWorker htmlparser = new HTMLWorker(myDoc);


            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(myDoc, memoryStream);
                myDoc.Open();
                //Put the image
                iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(imageFile);

                //Image alignment
                myImage.ScaleToFit(600f, 350f);
                myImage.SpacingBefore = 50f;
                myImage.SpacingAfter = 10f;
                //myImage.IndentationLeft = 9f;
                // myImage.BorderWidthTop = 36f;
                myImage.Alignment = Element.ALIGN_CENTER;
                //myDoc.Add(para);
                myDoc.Add(myImage);


                //Add the content
                htmlparser.Parse(sr);
                //================
                FileStream filer = new FileStream(@"E:\Logs\MobileMoneyApiLogs\Receipts\" + cust.ReceiptNumber + ".pdf", FileMode.Create, FileAccess.Write);
                memoryStream.WriteTo(filer);
                filer.Close();
                //========================
                myDoc.Close();

                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                MailMessage mm = new MailMessage();

                mm.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(bytes), cust.ReceiptNumber + "\t Receipt.pdf"));

                string sendEmailsFrom = bll.DecryptString(datafile.GetSystemParameter(2, 19));
                string sendEmailToVendor = datafile.GetVendorCodeEmail(cust.CustomerCode);

                foreach (string line in sendEmailToVendor.Split(','))
                {
                     mm.To.Add(new MailAddress(line));
                }
                mm.CC.Add(new MailAddress("techsupport@pegasus.co.ug"));

                mm.Subject = "Pegasus Receipt " + cust.ReceiptNumber;
                mm.Body = "Hi, find attached";
                mm.From = new MailAddress(sendEmailsFrom);
                mm.Subject = "Pegasus Receipt";
                if (batchCustomer.StatusCode.Equals("0"))
                {
                    mm.Body = "Hello\t" + batchCustomer.CustomerName + "<br></br><br></br>" + "Your account has been credited with\t" + CreditAmountWithCommas + "/" + "="
                      + "<br></br><br></br>" + "See Receipt Attached" + "<br></br><br></br><br></br>" + "Thank You";
                    //Receipt Attachment";
                    //mm.Attachments.Add(new Attachment(new MemoryStream(bytes), batchCustomer.CustomerName + "\t Receipt.pdf"));
                }
                else
                {
                    mm.Body = "Hello\t" + cust.CustomerCode + "<br></br><br></br>" + "Your account has been credited with\t" + CreditAmountWithCommas + "/" + "="
                     + "<br></br><br></br>" + "See Receipt Attached" + "<br></br><br></br><br></br>" + "Thank You";
                    //Receipt Attachment";
                    //mm.Attachments.Add(new Attachment(new MemoryStream(bytes), cust.CustomerCode + "\t Receipt.pdf"));
                }

                //SEND EMAIL
                if (cust.CustomerCode.Equals("P00029"))
                { }
                else
                {
                    bll.SendMailViaPegasus(mm);
                }
                
            }

        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    public bool RemoteCertificateValidation(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    public string ToWords(double num)
    {
        // Return a word representation of the whole number value.
        // Remove any fractional part.
        num = Math.Truncate(num);

        // If the number is 0, return zero.
        if (num == 0) return "zero";

        string[] groups = {"", "Thousands", "Million", "Billion",
        "Trillion", "Quadrillion", "?", "??", "???", "????"};

        string result = "";

        // Process the groups, smallest first.
        int group_num = 0;
        while (num > 0)
        {
            // Get the next group of three digits.
            double quotient = Math.Truncate(num / 1000);
            int remainder = (int)Math.Round(num - quotient * 1000);
            num = quotient;

            // Convert the group into words.
            if (remainder != 0)

                result = GroupToWords(remainder) + " " + groups[group_num] + ", " + result;

            // Get ready for the next group.
            group_num++;
        }

        // Remove the trailing ", ".
        if (result.EndsWith(", "))
            result = result.Substring(0, result.Length - 2);

        return result.Trim();
    }
    // Convert a number between 0 and 999 into words.
    private string GroupToWords(int num)
    {
        string[] one_to_nineteen = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Ninteen" };
        string[] multiples_of_ten = { "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninty" };

        // If the number is 0, return an empty string.
        if (num == 0) return "";

        // Handle the hundreds digit.
        int digit;
        string result = "";
        if (num > 99)
        {
            digit = (int)(num / 100);
            num = num % 100;
            result = one_to_nineteen[digit] + " " + "Hundred";
        }

        // If num = 0, we have hundreds only.
        if (num == 0) return result.Trim();

        // See if the rest is less than 20.
        if (num < 20)
        {
            // Look up the correct name.
            result += " " + one_to_nineteen[num];
        }
        else
        {
            // Handle the tens digit.
            digit = (int)(num / 10);
            num = num % 10;
            result += " " + multiples_of_ten[digit - 2];

            // Handle the final digit.
            if (num > 0)
                result += " " + one_to_nineteen[num];
        }

        return result.Trim();
    }

    private string GetRecordsToApprove()
    {
        int Count = 0;
        string ItemArr = "";
        foreach (DataGridItem Items in DataGrid1.Items)
        {
            CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
            if (chk.Checked)
            {
                Count++;
                string ItemFound = Items.Cells[0].Text;
                ItemArr = ItemArr += ItemFound + ",";
            }
        }
        return ItemArr;
    }
    private void LoadCredits()
    {

        try
        {
            string custName = txtCustName.Text;
            string CustAccount = txtCustAccount.Text;
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            dataTable = datafile.GetApprovedCredits(custName, CustAccount, fromdate, todate);
            if (dataTable.Rows.Count > 0)
            {
                MultiView1.ActiveViewIndex = 0;
                DataGrid1.Visible = true;
                DataGrid1.DataSource = dataTable;
                DataGrid1.CurrentPageIndex = 0;
                DataGrid1.DataBind();
                //ShowMessage(".", false);
            }
            else
            {
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("No Credits To Approve", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            string str = GetRecordsToApprove().TrimEnd(',');
            if (str.Equals(""))
            {
                ShowMessage("Please Select Credits for which to resend the reciepts", true);
            }
            else
            {
                UpdateRejectedCredits(str);
                LoadCredits();
            }

        }
        catch (Exception ex)
        {
            throw ex;

        }
    }
    private void UpdateRejectedCredits(string str)
    {
        int suc = 0;
        int failed = 0;
        string[] arr = str.Split(',');
        int i = 0;
        string User = Session["UserName"].ToString();
        for (i = 0; i < arr.Length; i++)
        {
            int RecordId = int.Parse(arr[i].ToString());


            //update Rejected credits
            string updatesuccess = datafile.UpdateRejectedCredit(RecordId, User);
            if (updatesuccess.Equals("SUCCESSFUL"))
            {
                //count++;
                suc++;
            }
            else
            {
                failed++;
            }

        }
        string msg = suc + "  Credits Have Been Rejected and " + failed + "  Failed";
        datafile.LogActivity(Session["UserName"].ToString(), "Rejected Account Credits Details");
        ShowMessage(msg, false);
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            LoadCredits();
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
            string custName = txtCustName.Text;
            string CustAccount = txtCustAccount.Text;
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            dataTable = datafile.GetCreditsToApprove(custName, CustAccount, fromdate, todate);
            MultiView1.ActiveViewIndex = 0;
            DataGrid1.DataSource = dataTable;
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataBind();
            ShowMessage(".", false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
}
