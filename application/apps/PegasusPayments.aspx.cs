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
using System.Text;

public partial class PegasusPayments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["IsError"] = null;
            string Msg = Request.QueryString["Status"];
            if (!string.IsNullOrEmpty(Msg))
            {


                if (Msg.ToUpper().Contains("SUCCESS"))
                {
                    Msg = "Transaction Status: " + Msg + "! TransactionID: " + Request.QueryString["TranID"];
                    ShowMessage(Msg, false);
                }
                else
                {
                    string Reason = Request.QueryString["Reason"];
                    if (string.IsNullOrEmpty(Reason))
                    {
                        Msg = "Transaction Status: " + Msg + "! Reason: Unable to Complete Payment";
                    }
                    else
                    {
                        Msg = "Transaction Status: " + Msg + "! Reason: " + Reason;
                    }
                    ShowMessage(Msg, true);
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            ShowMessage(msg, true);
        }
    }

    private void ShowMessage(string Message, bool Error)
    {
        Label lblmsg = (Label)Master.FindControl("lblmsg");
        if (Error) { lblmsg.ForeColor = System.Drawing.Color.Red; lblmsg.Font.Bold = false; }
        else { lblmsg.ForeColor = System.Drawing.Color.Green; lblmsg.Font.Bold = true; }
        if (Message == ".")
        {
            lblmsg.Text = ".";
        }
        else
        {
            lblmsg.Text = "MESSAGE: " + Message.ToUpper();
        }
    }

    public string DecryptString(string Encrypted)
    {
        string ret = "";
        ret = Encryption.encrypt.DecryptString(Encrypted, "Umeme2501PegPay");
        return ret;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            string Pwd = DecryptString("cionzvPpzLCIObEXp97B+g==");
            Response.Clear();
            string DigitalSignature = "TEST";
            string VendorCode = "P00029";
            string Password = "34K61US725";
            string TranID = DateTime.Now.Ticks.ToString();
            string ItemTotal = txtName.Text;
            string ItemDesc = txtCode.Text;

            Uri url = new Uri(this.Request.Url.ToString());
            string path = String.Format("{0}{1}{2}{3}", url.Scheme,
                                                        Uri.SchemeDelimiter,
                                                        url.Authority, 
                                                        url.AbsolutePath);

            string ReturnUrl =path ;


            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
            sb.AppendFormat("<form name='form' action='{0}' method='post'>", "http://192.168.0.7/TestPegasusPaymentsGateway/Default.aspx");

            sb.AppendFormat("<input type='hidden' name='{0}' value='{1}'>", "VENDOR_CODE", VendorCode);
            sb.AppendFormat("<input type='hidden' name='{0}' value='{1}'>", "PASSWORD", Password);
            sb.AppendFormat("<input type='hidden' name='{0}' value='{1}'>", "VENDOR_TRANID", TranID);
            sb.AppendFormat("<input type='hidden' name='{0}' value='{1}'>", "ITEM_TOTAL", ItemTotal);
            sb.AppendFormat("<input type='hidden' name='{0}' value='{1}'>", "ITEM_DESCRIPTION", ItemDesc);
            sb.AppendFormat("<input type='hidden' name='{0}' value='{1}'>", "RETURN_URL", ReturnUrl);
            sb.AppendFormat("<input type='hidden' name='{0}' value='{1}'>", "DIGITAL_SIGNATURE", DigitalSignature);

            // Other params go here
            sb.Append("</form>");
            sb.Append("</body>");
            sb.Append("</html>");
            string html = sb.ToString();
            Response.Write(html);
            Response.End();
        }
        catch (Exception)
        {

            throw;
        }
    }
}
