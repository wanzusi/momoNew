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
using System.Text;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Mail;
using iTextSharp.text.html.simpleparser;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
public partial class DebitClientAccountBalance : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    private DataTable dtable = new DataTable();
    //DataTable dtable = new DataTable();
    private DataLogin dac = new DataLogin();
    public string message = "";
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
                    LoadVendors();

                    if (Session["AreaID"].ToString().Equals("3"))
                    {
                        cboVendor.SelectedIndex = cboVendor.Items.IndexOf(new System.Web.UI.WebControls.ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                        cboVendor.Enabled = false;
                    }

                    LoadDifferentPegasusAccountNames();

                    //Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                    //Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                    //Button MenuReport = (Button)Master.FindControl("btnCalReports");
                    //Button MenuRecon = (Button)Master.FindControl("btnAccounts");
                    //Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                    //Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                    //MenuTool.Font.Underline = false;
                    //MenuPayment.Font.Underline = false;
                    //MenuReport.Font.Underline = true;
                    //MenuRecon.Font.Underline = false;
                    //MenuAccount.Font.Underline = false;
                    //MenuBatching.Font.Underline = false;
                    //lblTotal.Visible = false;
                    DisableBtnsOnClick();
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
    private void LoadVendors()
    {
        //dtable = datafile.GetAllVendors("0");
        dtable = datafile.GetSystemCompanies("", "");
        cboVendor.DataSource = dtable;
        cboVendor.DataValueField = "CompanyCode";
        cboVendor.DataTextField = "Company";
        cboVendor.DataBind();
    }

    private void LoadDifferentPegasusAccountNames()
    {
        //dtable = datafile.GetAllVendors("0");
        dtable = datafile.GetAllPegasusAccountNames("0");
        cboPegasusAccountName.DataSource = dtable;
        cboPegasusAccountName.DataValueField = "AccountName";
        cboPegasusAccountName.DataTextField = "AccountName";
        cboPegasusAccountName.DataBind();
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

    

    private void DisableBtnsOnClick()
    {
        //string strProcessScript = "this.value='Working...';this.disabled=true;";
        //btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        //btnConvert.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnConvert, "").ToString());

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

    protected void btnDebit_Click(object sender, EventArgs e)
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
        //session details
        string userId = Session["Username"] as string;
        string fullname = Session["Fullname"] as string;
        string userBranch = Session["CustomerCode"] as string;
        string page = bll.GetCurrentPageName();


        //Client Account Details
        string VendorCode = cboVendor.SelectedValue.ToString().Trim();  //Text.Trim()
        string AgentAccountNumber = txtAgentAccountNumber.Text.Trim();
        string AgentAccountBalance = txtAgentAccountBalance.Text.Trim();
        string AgentdebitAmount = txtAgentDebitAmount.Text.Trim().Replace(",", "");
        double amt = 0;
        bool CheckIfAgentdebitAmountIsValid = double.TryParse(AgentdebitAmount, out amt);
        double validAgentdebitAmount = 0.0;

        double validAgentAccountBalance = 0.0;
        double validAgentAccountNumber = 0.0;

        dtable = dac.GetPegPayAccount(VendorCode);
        string AccountNumber = "";
        string AccountBalance = "";
        if (dtable.Rows.Count > 0)
        {
            AccountNumber = dtable.Rows[0]["AccountNumber"].ToString();
            AccountBalance = dtable.Rows[0]["AccountBalance"].ToString();
        }

        //Pegaus Account Details
        string PegasusAccountName = cboPegasusAccountName.SelectedValue.ToString().Trim();
        string PegasusAccountNumber = txtPegasusAccountNo.Text.Trim();
        string PegasusAccountBalance = txtPegasusAccountBalance.Text.Trim();
        string PegasusAccountNetwork = cboNetwork.Text.Trim();

        double validPegasusAccountBalance = 0.0;
        double validPegasusAccountNumber = 0.0;

        //Validation  for Amounts Here
        if (AgentdebitAmount.Equals(""))
        {
            ShowMessage("Please Enter An Amount to Be Debited", true);
            txtAgentDebitAmount.Focus();
        }
        else{

            validAgentdebitAmount = double.Parse(AgentdebitAmount); 
            }


        if (AgentAccountBalance.Equals(""))
        {
            ShowMessage("Agent Account Balance not retrieved", true);
            txtAgentAccountBalance.Focus();
           
        } 
        else{

             validAgentAccountBalance = Convert.ToDouble(AgentAccountBalance.Split('.')[0]);
            }

       if (AgentAccountNumber.Equals(""))
       {
                ShowMessage("Agent Account Number not retrieved", true);
                txtAgentAccountBalance.Focus();
      }
      else
       {

                //validAgentAccountNumber = Convert.ToDouble(AgentAccountNumber.Split('.')[0]);
       }

       if (PegasusAccountBalance.Equals(""))
        {
            ShowMessage("Pegasus Account Balance not retrieved", true);
            txtPegasusAccountBalance.Focus();
        }
        else
        {

            validPegasusAccountBalance = Convert.ToDouble(PegasusAccountBalance.Split('.')[0]);
        }

        if (PegasusAccountNumber.Equals(""))
        {
            ShowMessage("Pegasus Account Number not retrieved", true);
            txtPegasusAccountBalance.Focus();
        }

        else
        {

            validPegasusAccountNumber = Convert.ToDouble(PegasusAccountNumber.Split('.')[0]);
        }


        //Validation for other fields
       if (VendorCode.Equals("0"))
        {
            ShowMessage("Please Select an Agent from the List", true);
            cboVendor.Focus();

        } 
        else if (!CheckIfAgentdebitAmountIsValid)
        {
            ShowMessage("Please Enter Correct Debit Amount", true);
            txtAgentDebitAmount.Focus();
        }
        else if (PegasusAccountName.Equals("0"))
        {
            ShowMessage("Please Select a Pegasus Account from the List", true);
            cboPegasusAccountName.Focus();
        }       
        else if (PegasusAccountNetwork.Equals(""))
        {
            ShowMessage("Pegasus Account Network not retrieved", true);
            cboNetwork.Focus();
        }
        else if (validAgentdebitAmount == 0.0)
        {
            ShowMessage("Please Enter a Number greater than zero", true);
            txtAgentDebitAmount.Focus();
       
       }

       else if (validAgentdebitAmount > validAgentAccountBalance)
        {
            ShowMessage("The Amount to be Debited is Greater than the Client's Account balance", true);
            txtAgentDebitAmount.Focus();

        }
        else if (IsduplicateDebit(VendorCode, validAgentdebitAmount, validAgentAccountNumber, validPegasusAccountNumber, PegasusAccountNetwork))
        {

            ShowMessage("The Amount to be Debited has already been debited try again after 20 MINUTES Please", true);
            txtAgentDebitAmount.Focus();
        }
        else {

            DateTime todaydate = DateTime.Now;
            string datetoday = todaydate.ToString("yyyy-MM-dd hh:mm:ss").Replace("/", "").Replace(":", "").Replace(" ", "").Replace("-", "");
            string VendorTranId = "D-"+datetoday;
            string debitstatus = bll.DebitClientAccountBalance(VendorTranId, VendorCode, AgentAccountNumber, validAgentdebitAmount, validPegasusAccountNumber, PegasusAccountNetwork);
            if (debitstatus.Equals("OK"))
            {

                try
                {
                    CustomerReceiptDebitDetails debitdetails = new CustomerReceiptDebitDetails();
                    debitdetails = datafile.GetCustomerReceiptDebitDetails(VendorTranId, VendorCode, AgentAccountNumber, validAgentdebitAmount, validPegasusAccountNumber, PegasusAccountNetwork);
                    SendProofOfDebitReceiptToEmail(debitdetails);


                }
                catch (Exception ex)
                {
                    ShowMessage(ex.Message, true);
                }

                bll.InsertIntoAuditLog(AgentAccountNumber, "CREATE", "ReceivedTransaction", userBranch, userId, page,
fullname + " successfully debited the vendor" + VendorCode + ", account [" + validPegasusAccountNumber + "] with the amount [" + bll.PutCommas(validAgentdebitAmount.ToString()) + "] from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());
          

                string message = "The Pegasus Account Number " + validPegasusAccountNumber + " Has been Credited with " + validAgentdebitAmount.ToString();
                ShowMessage(message, false);
                btnDebit.Enabled = false;
                ClearControls();


                

            }
        
        }

    }

    private void SendProofOfDebitReceiptToEmail(CustomerReceiptDebitDetails debitdetails)
    {
        string AccountNumber = "";
        string AccountBalance = "";
        double AgentAccountBalance= 0;
        double DebitAmount = 0;
        DateTime todaydate = DateTime.Today.Date;
        string datetoday = todaydate.ToString("dd/MM/yyyy");
        try
        {
            DataTable dtable = datafile.GetPegPayAccount(debitdetails.CustomerCode);
            if (dtable.Rows.Count > 0)
            {
                AccountNumber = dtable.Rows[0]["AccountNumber"].ToString();
                AccountBalance = dtable.Rows[0]["AccountBalance"].ToString();
            }

            AgentAccountBalance = Convert.ToDouble(AccountBalance.Split('.')[0]);
            DebitAmount = Convert.ToDouble(debitdetails.CustomerDebitAmount.Split('.')[0]);
         

            string DebitAmountInWords = ToWords(DebitAmount);
            string AgentAccountBalanceInWords = ToWords(AgentAccountBalance);


            string AgentAccountBalance_WithCommas = AgentAccountBalance.ToString("#,##0");
            string DebitAmountWithCommas = DebitAmount.ToString("#,##0");


            double PegasusAccountBalance=Convert.ToDouble(debitdetails.CurrentPegasusAccountBalance);

            string PegasusAccountBalance_WithCommas=PegasusAccountBalance.ToString("#,##0");
            string PegasusAccountBalanceInWords = ToWords(PegasusAccountBalance);



            StringBuilder sb = new StringBuilder();
            sb.Append("<table width='100%' cellspacing='0'  cellpadding='2' frame='box'");
            string imageFile = Server.MapPath(".") + "/Images/Receipt.png";
            sb.Append(imageFile);
            sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>PEGASUS CREDIT RECEIPT</b></td></tr>");

            sb.Append("<tr><td colspan = '4' ></td></tr>");


            sb.Append("<tr><td colspan = '5'  align='right'><b>Receipt No.</b> ");
            sb.Append(debitdetails.ReceiptNumber);
            sb.Append("</td></tr>");



            sb.Append("<tr><td colspan = '5'   align='right'><b>Date:</b> ");
            sb.Append(datetoday);
            sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  height='4'>&nbsp;</td> </tr>");
            sb.Append("<tr><td colspan = '5' ><b>Customer Name:</b>");
            sb.Append(debitdetails.CustomerCode);
            sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td colspan = '5'><b>Debited Amount :</b> ");
            sb.Append(DebitAmountWithCommas + "/" + "=");
            sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td colspan = '5' ><b>Debited Amount In Words :</b> ");
            sb.Append(DebitAmountInWords + "\t  Uganda Shillings Only");
            sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td colspan = '5' ><b> Customer Account Balance :</b> ");
            sb.Append(AgentAccountBalance_WithCommas + "/" + "=");
            sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td colspan = '5' ><b>Customer Account Balance in Words :</b> ");
            sb.Append(AgentAccountBalanceInWords + "\t  Uganda Shillings Only");
            sb.Append("</td></tr>");


            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td colspan = '5' ><b>Credited Pegasus Account Number :</b> ");
            sb.Append(debitdetails.PegasusAccountNumber);
            sb.Append("</td></tr>");

             sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td colspan = '5' ><b>Pegasus Account Balance :</b> ");
            sb.Append(PegasusAccountBalance_WithCommas + "/"+"=");
            sb.Append("</td></tr>");

              sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td colspan = '5' ><b>Pegasus Account Balance In Words :</b> ");
            sb.Append(PegasusAccountBalanceInWords + "\t  Uganda Shillings Only");
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
                myDoc.Close();

                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                string sendEmailsFrom = bll.DecryptString(datafile.GetSystemParameter(2, 19));
                System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
                mm.To.Clear();
                MailAddress toEmail = new MailAddress("techsupport@pegasustechnologies.co.ug");
                mm.To.Add(toEmail);
                mm.From = new MailAddress(sendEmailsFrom, "Pegasus Technologies");
                mm.Subject = "Pegasus Debit/Credit Receipt";
                mm.Body = "Hello Pegasus \t <br></br><br></br>" + debitdetails.CustomerCode + " 's Account has been debited with\t" + DebitAmountWithCommas + "/" + "="
                     + "<br></br><br></br>" + "See Receipt Attached" + "<br></br><br></br><br></br>" + "Thank You" + "<br></br><b></br><b></br>Pegasus Technologies";
                    //Receipt Attachment";
                mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "PEGASUS CREDIT" + "\t Receipt.pdf"));    
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "64.233.167.108"; 
                smtp.EnableSsl = true;
                smtp.Timeout = 8000000;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = bll.DecryptString(datafile.GetSystemParameter(2, 19)); //"antheamarthy@gmail.com";
                NetworkCred.Password = bll.DecryptString(datafile.GetSystemParameter(2, 20));//"PasswordForEmail";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;

                System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidation;
                smtp.Send(mm);              
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

    private bool IsduplicateDebit(string VendorCode, double AgentdebitAmount, double AgentAccountNumber, double PegasusAccountNumber, string PegasusAccountNetwork)
    {
        bool ret = false;
        //DataLogin dp = new DataLogin();
        DateTime postDate = DateTime.Now;
        DataTable dt = datafile.GetDuplicateDebitClientAmount(VendorCode, AgentdebitAmount, AgentAccountNumber, PegasusAccountNumber, PegasusAccountNetwork, postDate);
        if (dt.Rows.Count > 0)
        {
            DateTime Postdate = DateTime.Parse(dt.Rows[0]["RecordDate"].ToString());
            TimeSpan t = postDate.Subtract(Postdate);
            int tmdiff = t.Minutes;
            if (tmdiff < 10)
            {
                ret = true;
            }
            else
            {
                ret = false;
            }
        }
        else
        {
            ret = false;
        }
        return ret;
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

    private void ClearControls()
    {
        cboVendor.SelectedValue = "0";
        txtAgentAccountNumber.Text = "";
        txtAgentAccountBalance.Text = "";
        txtAgentDebitAmount.Text = "";
        txtPegasusAccountBalance.Text = "";
        cboPegasusAccountName.SelectedValue = "0";
        txtPegasusAccountNo.Text = "";
        cboNetwork.Text = "";
        //txtPegasusAccountBalance.SelectedValue = "0";
        //lblCode.Text = "0";
        //btnDebit.Enabled = false;

    }


    protected void cboVendor_DataBound(object sender, EventArgs e)
    {
        cboVendor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Agents", "0"));
    }
    protected void cboPegasusAccountName_DataBound(object sender, EventArgs e)
    {
        cboPegasusAccountName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Pegasus Account Name", "0"));
    }

    
    protected void cboPegasusAccountName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string PegasusAccountName = cboPegasusAccountName.SelectedValue.ToString();
        dtable = dac.GetPegasusAccountNameDetails(PegasusAccountName);
        string AccountNumber = "";
        string AccountBalance = "";
        string Network = "";
        if (dtable.Rows.Count > 0)
        {
            AccountNumber = dtable.Rows[0]["AccountNumber"].ToString();
            AccountBalance = dtable.Rows[0]["AccountBalance"].ToString();
            Network = dtable.Rows[0]["Network"].ToString();
        }
        txtPegasusAccountNo.Text=AccountNumber;
        txtPegasusAccountBalance.Text=AccountBalance;
        cboNetwork.Text = Network;
        //cboNetwork.Enabled = false;

    }
    protected void cboVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
       string VendorCode = cboVendor.SelectedValue.ToString();
       dtable = dac.GetPegPayAccount(VendorCode);
       string AccountNumber = "";
       string AccountBalance = "";
       if (dtable.Rows.Count > 0)
       {
           AccountNumber = dtable.Rows[0]["AccountNumber"].ToString();
           AccountBalance = dtable.Rows[0]["AccountBalance"].ToString();
       }
       txtAgentAccountNumber.Text = AccountNumber;
       txtAgentAccountBalance.Text = AccountBalance;
       //txtAccountBalance.Text = AccountBalance;
    }
}
