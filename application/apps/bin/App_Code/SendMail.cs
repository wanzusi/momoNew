using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for SendMail
/// </summary>
public class SendMail
{
    DataLogin datafile = new DataLogin();
    BusinessLogin bll = new BusinessLogin();
	public SendMail()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void Alert(string Message,int Who)
    {


    }
    public string SendeMail(string MailTo, string Subject, string Body)
    {
        string ret = "";
        try
        {
            int GlobalCode = 1;
            int ValueCode = 2;
            SmtpClient client = new SmtpClient();
            string smtpIP = datafile.GetSystemParameter(GlobalCode, ValueCode);
            if (smtpIP.Equals(""))
            {
                smtpIP = "10.0.0.6";
            }
            client.Host = smtpIP;
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("inteface@umeme.co.ug", "UMEME INTERFACE");
            msg.ReplyTo = new MailAddress("interface@umeme.co.ug");
            msg.To.Add(new MailAddress(MailTo));
            msg.Subject = Subject;
            msg.IsBodyHtml = true;
            msg.Body = Body;
            client.Send(msg);
            ret = "SENT";
        }
        catch (Exception ex)
        {
            ret = ex.Message;
        }
        return ret;
    }

    internal string GoogleMail(string mailto, string subject, string message, string name)
    {
        string ret = "";
        try
        {           
            MailMessage mailMessage = new MailMessage();
            // Email to send to
            MailAddress toEmail = new MailAddress(mailto, name);
            mailMessage.To.Add(toEmail);
            // set subject
            mailMessage.Subject = subject;
            // set body
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;
            // set from email
            mailMessage.From = new MailAddress("info@pegasustechnologies.co.ug", "PEGPAY PAYMENTS INTERFACE");
            // Identify the credentials to login to the gmail account 
            string sendEmailsFrom = bll.DecryptString(datafile.GetSystemParameter(2, 19));//"jab.arajab@gmail.com";
            string sendEmailsFromPassword = bll.DecryptString(datafile.GetSystemParameter(2, 20));//"jabqas@2012";
            string host = bll.DecryptString(datafile.GetSystemParameter(2, 18));
            NetworkCredential cred = new NetworkCredential(sendEmailsFrom, sendEmailsFromPassword);
            SmtpClient mailClient = new SmtpClient(host, 587);
            mailClient.EnableSsl = true;
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.UseDefaultCredentials = false;
            mailClient.Timeout = 20000;
            mailClient.Credentials = cred;
            mailClient.Send(mailMessage);
            ret = "SENT";
        }
        catch (Exception ex)
        {
            ret = ex.Message;
        }
        return ret;
    }
    public void SendReveralEmail1(string vendorCode, string msg, string network, string subject)
    {
        string ret = "";
        try
        {
            DataSet dset = datafile.GetMailingList(vendorCode, network, "TO");
            MailMessage message = new MailMessage();
           
            string toMsg ="Hello ";
            if (dset.Tables.Count > 1)
            {
                DataTable mailingTable = dset.Tables[0];

                int count;
                foreach (DataRow drow in mailingTable.Rows)
                {
                    string toMail = drow["EmailAddress"].ToString();
                    string level = drow["ReceiveLevel"].ToString();
                    string name = drow["Name"].ToString();
                    MailAddress mytoAddress = new MailAddress(toMail);
                    message.To.Add(mytoAddress);
                    if (level == "1")
                    {
                        toMsg += name + ",";
                    }
                    
                }

                msg = toMsg + "<br/><br/>" + msg;
                DataTable senderTable = dset.Tables[1];
                DataRow drow2 = senderTable.Rows[0];
                
                string senderName = drow2["Name"].ToString();
                msg += "<br/><br/>Thanks<br/>" + senderName + "<br/>Pegasus Technologies";
                DataSet dset2 = datafile.GetMailingList(vendorCode, network, "COPIED");
                if (dset2.Tables.Count > 0)
                {
                    foreach (DataRow drows in dset2.Tables[0].Rows)
                    {
                        string copyMail = drows["EmailAddress"].ToString();
                        MailAddress mytoAddress = new MailAddress(copyMail);
                        message.CC.Add(mytoAddress);

                    }
                }

                string sendEmailsFrom = bll.DecryptString(datafile.GetSystemParameter(2, 21));//"jab.arajab@gmail.com";
                string sendEmailsFromPassword = bll.DecryptString(datafile.GetSystemParameter(2, 22));//"jabqas@2012";
                string host = datafile.GetSystemParameter(2, 23);// bll.DecryptString(datafile.GetSystemParameter(2, 23));

                message.From = new MailAddress(sendEmailsFrom, "PEGASUS REVERSAL REQUESTS");
                message.Subject = subject;
                //set body
                message.Body = msg;
                message.IsBodyHtml = true;

                NetworkCredential cred = new NetworkCredential(sendEmailsFrom, sendEmailsFromPassword);
                SmtpClient mailClient = new SmtpClient(host);
                mailClient.EnableSsl = false;
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailClient.UseDefaultCredentials = false;
                mailClient.Timeout = 20000;
                mailClient.Credentials = cred;
                mailClient.Send(message);
                ret = "SENT";
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            Console.WriteLine(ex.Message);
        }
    }
    public void SendReveralEmail2(string vendorCode, string msg, string network, string subject, bool confirmation)
    {
        string ret = "";
        try
        {

            DataSet dset = datafile.GetMailingList(vendorCode,network, "TO");
            MailMessage mailMessage = new MailMessage();

            string toMsg = "Hello ";
            if (dset.Tables.Count > 1)
            {
                DataTable mailingTable = dset.Tables[0];

                int count;
                foreach (DataRow drow in mailingTable.Rows)
                {
                    string toMail = drow["EmailAddress"].ToString();
                    string level = drow["ReceiveLevel"].ToString();
                    string name = drow["Name"].ToString();

                    MailAddress mytoAddress = new MailAddress(toMail, name);

                    mailMessage.To.Add(mytoAddress);

                    if (level == "1")
                    {
                        toMsg += name + ",";
                    }

                }

                msg = toMsg + "<br/><br/>" + msg;
                DataTable senderTable = dset.Tables[1];
                DataRow drow2 = senderTable.Rows[0];

                string senderName = drow2["Name"].ToString();
                msg += "<br/><br/>Thanks<br/>" + senderName + "<br/>Pegasus Technologies";
                DataSet dset2 = datafile.GetMailingList(vendorCode,network, "COPIED");
                if (dset2.Tables.Count > 0)
                {
                    foreach (DataRow drows in dset2.Tables[0].Rows)
                    {
                        string copyMail = drows["EmailAddress"].ToString();
                        //MailAddress mytoAddress = new MailAddress(copyMail);
                        mailMessage.CC.Add(copyMail);

                    }
                }

                string sendEmailsFrom = bll.DecryptString(datafile.GetSystemParameter(2, 21));//"jab.arajab@gmail.com";
                string sendEmailsFromPassword = bll.DecryptString(datafile.GetSystemParameter(2, 22));//"jabqas@2012";
                string host = datafile.GetSystemParameter(2, 23);// bll.DecryptString(datafile.GetSystemParameter(2, 23));

                mailMessage.From = new MailAddress(sendEmailsFrom, "PEGASUS REVERSAL REQUESTS");
                mailMessage.Subject = subject;

                mailMessage.Body = msg;
                mailMessage.IsBodyHtml = true;

                //ServicePointManager.ServerCertificateValidationCallback =  RemoteCertificateValidation;

                NetworkCredential cred = new NetworkCredential(sendEmailsFrom, sendEmailsFromPassword);
                SmtpClient mailClient = new SmtpClient("mail.pegasustechnologies.co.ug", 25);
                mailClient.EnableSsl = false;
                // mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                // mailClient.UseDefaultCredentials = false;
                // mailClient.Timeout = 20000;
                mailClient.Credentials = cred;
                mailClient.Send(mailMessage);
                ret = "SENT";
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            Console.WriteLine(ex.Message);
        }

    }
}
