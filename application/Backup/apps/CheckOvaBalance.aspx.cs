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
using InterLinkClass.EntityObjects;
using System.Xml;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

public partial class CheckOvaBalance : System.Web.UI.Page
{
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable table;
    public string ovaUsername = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                string RoleId = Session["RoleCode"].ToString();
                if (isRoleAuthorisedToVisitPage(RoleId))
                {
                    MultiView1.ActiveViewIndex = 0;
                    LoadNetworks();
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
    private void LoadNetworks()
    {
        ddNetwork.Items.Add(new ListItem("Select Telecom ", ""));
        ddNetwork.Items.Add(new ListItem("MTN", "MTN"));
        ddNetwork.Items.Add(new ListItem("AIRTEL", "AIRTEL"));
        ddNetwork.DataBind();
    }

    protected void ddNetwork_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string telecom = ddNetwork.SelectedValue;
            if (string.IsNullOrEmpty(telecom))
            {
                throw new Exception("No telecom selected");
            }
            else if (telecom.Equals("MTN"))
            {
                LoadOvas(telecom);
            }
            else if (telecom.Equals("AIRTEL"))
            {
                LoadOvas(telecom);
            }
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            ShowMessage(msg, true);
        }
    }

    private void LoadOvas(string telecom)
    {
        table = new DataTable();
        table = datapay.GetOvaAccountDetails(telecom);
        cboOvaAccount.DataSource = table;
        cboOvaAccount.DataTextField = "Ova";
        cboOvaAccount.DataValueField = "SenderId";
        cboOvaAccount.DataBind();
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

    private void LoadAllOvas()
    {
        table = new DataTable();
        table = datapay.GetOvaAccounts("");
        cboOvaAccount.DataSource = table;
        cboOvaAccount.DataValueField = "SenderId";
        cboOvaAccount.DataTextField = "SenderId";
        cboOvaAccount.DataBind();
    }

    protected void cboOvaAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("", false);
            ovaUsername = cboOvaAccount.SelectedValue.ToString();

            string telecom = ddNetwork.SelectedValue;

            GetAccountBalance(ovaUsername, telecom);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void GetAccountBalance(string ovaUsername, string telecom)
    {
        table = datapay.GetOvaAccounts(ovaUsername);
        List<OvaAccount> accts = GetAccounts(table);
        DataTable dt = GetTable();
        foreach (OvaAccount acct in accts)
        {
            if (telecom.Equals("AIRTEL"))
            {
                //InterLinkClass.MessengerApi.Messenger svc = new InterLinkClass.MessengerApi.Messenger();
                //InterLinkClass.MessengerApi.OvaAccount req = new InterLinkClass.MessengerApi.OvaAccount();
                //req.SenderId = acct.SenderId;
                //req.SpId = acct.SpId;
                //req.Password = acct.Password;
                //acct.Balance = svc.GetBalance(req);
                //object[] o = { acct.SenderId, acct.SpId, GetMoneyString(acct.Balance), acct.Msisdn };
                //dt.Rows.Add(o);

                InterLinkClass.D2TApi.Service svc = new InterLinkClass.D2TApi.Service();
                InterLinkClass.D2TApi.Transaction req = new InterLinkClass.D2TApi.Transaction();
                //req.VendorCode = acct.SenderId;
                req.TransactingOva = acct.SpId; 
                req.Password = acct.Password;
                
                InterLinkClass.D2TApi.Response Resp = svc.GetAccountBalanceFromMobiquity(req);
                acct.Balance = Resp.TelecomId;
                object[] o = { acct.SenderId, acct.SpId, GetMoneyString(acct.Balance), acct.Msisdn };
                dt.Rows.Add(o);
            }
            else
            {
                OvaAccount a = new OvaAccount();
                string feedback = GetVendorECWBalanceAtTelecom(acct);
                a = ParseVendorBalance(feedback);
                object[] o = { acct.SenderId, acct.SpId, GetMoneyString(a.Balance), acct.Msisdn };
                dt.Rows.Add(o);
            }
        }
        DataGrid1.DataSource = dt;
        DataGrid1.DataBind();
        MultiView2.ActiveViewIndex = 0;

    }

    private string GetMoneyString(string balanceString)
    {
        double balance = Double.Parse(balanceString);
        return balance.ToString("#,##0");
    }

    private DataTable GetTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add("SenderId");
        dt.Columns.Add("SpId");
        dt.Columns.Add("Balance");
        dt.Columns.Add("Msisdn");
        return dt;
    }

    private List<OvaAccount> GetAccounts(DataTable table)
    {
        List<OvaAccount> accounts = new List<OvaAccount>();
        foreach (DataRow drow in table.Rows)
        {
            OvaAccount acct = new OvaAccount();
            acct.SenderId = drow["SenderId"].ToString();
            acct.SpId = drow["SpId"].ToString();
            acct.Password = drow["Password"].ToString();
            acct.Threshold = Double.Parse(drow["Threshold"].ToString());
            acct.Msisdn = drow["Msisdn"].ToString();
            accounts.Add(acct);
        }
        return accounts;
    }

    public string GetVendorECWBalanceAtTelecom(OvaAccount vendor)
    {
        string feedback = "";

        try
        {
            string live = "https://212.88.125.189:6443/bank/getbalance";
            string url = live;

            //Trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidation;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.KeepAlive = false;

            string postData = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>" +
             "<ns2:getbalancerequest xmlns:ns2=\"http://www.ericsson.com/em/emm/financial/v1_0\">" +
             "<fri>FRI:" + vendor.SenderId + "/USER</fri>" +
             "</ns2:getbalancerequest>";


            byte[] data = Encoding.ASCII.GetBytes(postData);

            //credentials are put in authorization header
            string toBeHashed = vendor.SenderId + ":" + vendor.Password;
            string passwordDigest = Base64Encode(toBeHashed);
            request.Method = "POST";
            request.Headers.Add("Authorization:Basic " + passwordDigest);
            request.ContentType = "text/xml";
            request.ContentLength = data.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            feedback = responseString;
        }
        catch (WebException ex)
        {
            using (Stream stream = ex.Response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                feedback = reader.ReadToEnd();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return feedback;
    }

    private static string Base64Encode(string plainText)
    {
        byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    private string GetVendorBalanceAtTelecom(OvaAccount vendor)
    {
        string xmlSent = "";
        string feedback = "";

        try
        {
            //string test = "http://172.25.48.43:8323/mom/mt/queryBalance/";
            string live = "http://172.25.48.36:8323/mom/mt/queryBalance/";
            string url = live;

            //Build Request
            //Trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidation;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.KeepAlive = false;

            string postData = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
                              "<ns2:getbalancerequest xmlns:ns2=\"http://www.ericsson.com/em/emm/financial/v1_0\">" +
                              "<fri>FRI:" + vendor.SenderId + "/USER</fri>" +
                              "</ns2:getbalancerequest>";

            byte[] data = Encoding.ASCII.GetBytes(postData);
            xmlSent = "Sent to MTN at " + DateTime.Now + Environment.NewLine + postData;
            string requestXml = xmlSent + Environment.NewLine + Environment.NewLine;

            string phrase = Guid.NewGuid().ToString();
            string nonce = GetSHA1Hash(phrase);
            DateTime created = DateTime.Now;
            string createdStr = created.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
            string username = vendor.SpId;
            string password = vendor.Password;
            string toBeHashed = nonce + createdStr + password;
            string passwordDigest = GetSHA1Hash(toBeHashed);

            request.Headers["Authorization"] = "WSSE realm=\"SDP\"," +
                                          "profile=\"UsernameToken\"";
            request.Headers["X-WSSE"] = "UsernameToken Username=\"" + username + "\"," +
                                           "PasswordDigest=\"" + passwordDigest + "\"," +
                                           "Nonce=\"" + nonce + "\"," +
                                           "Created=\"" + createdStr + "\"";
            request.Headers["X-RequestHeader"] = "request ServiceId=\"\"," +
                                           "TransId=\"\"";
            request.Headers["Signature"] = "43AD232FD45FF";
            request.Headers["Cookie"] = "";
            request.Headers["Msisdn"] = "256779999508";
            request.Headers["X-HW-Extension"] = "k1=v1;k2=v2";

            request.Accept = "text/xml";
            request.ContentType = "application/xml";
            request.ContentLength = data.Length;
            request.Method = "POST";

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            feedback = responseString;
        }
        catch (WebException ex)
        {
            using (Stream stream = ex.Response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                feedback = reader.ReadToEnd();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return feedback;
    }

    public OvaAccount ParseVendorBalance(string feedback)
    {
        OvaAccount resp = new OvaAccount();
        try
        {
            XmlDocument XmlDeposit = new XmlDocument();
            XmlDeposit.LoadXml(feedback);
            XmlNodeList balance = XmlDeposit.GetElementsByTagName("amount");
            XmlNodeList currency = XmlDeposit.GetElementsByTagName("currency");

            if (feedback.Contains("balance"))
            {
                resp.Balance = balance[0].InnerText.Trim();
                double vendorBalance = Convert.ToDouble(resp.Balance);
                resp.Currency = currency[0].InnerText.Trim();

                if (vendorBalance > 500000)
                {
                    resp.Status = "0";
                }
                else
                {
                    resp.Status = "1";
                }
            }
            else if (feedback.Contains("INTERNAL_ERROR"))
            {
                //resp.Balance = "";
                resp.Balance = "INTERNAL_ERROR";
                resp.Status = "100";
            }
            else
            {
                resp.Status = "100";
            }
        }
        catch (Exception ex)
        {
            resp.Status = "100";
        }
        return resp;
    }

    private static string GetSHA1Hash(string toBeHashed)
    {
        byte[] bytes = System.Text.Encoding.Default.GetBytes(toBeHashed);
        SHA1 sha = new SHA1CryptoServiceProvider();
        byte[] hash = sha.ComputeHash(bytes);
        string authkey = Convert.ToBase64String(hash);

        return authkey;
    }


    private static bool RemoteCertificateValidation(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    protected void cboOvaAccount_DataBound(object sender, EventArgs e)
    {
        cboOvaAccount.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select OVA Account", "0"));
    }
}
