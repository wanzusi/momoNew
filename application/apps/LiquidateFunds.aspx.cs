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
public partial class TransferFunds : System.Web.UI.Page
{
    private BusinessLogin bll = new BusinessLogin();
    DataLogin datafile = new DataLogin();
    DataTable dataTable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                if (Session["LoggedIn"] != null || Session["LoggedIn"].ToString().Equals("YES"))
                {
                    MultiView1.ActiveViewIndex = 0;
                    MultiView2.ActiveViewIndex = 0;
                    Load_intialControls();
                }
                else
                {
                    Response.Redirect("Default2.aspx");
                }

            }
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }

    private void Load_intialControls()
    {
        try
        {
            //string CustomerCode = Session["CustomerId"].ToString();
            //string PegpayAccount = Session["CustomerPegasusAccount"].ToString();
            string CustomerCode = Session["CompanyCode"].ToString();
            string PegpayAccount = bll.GetVendorPegPayAccount(CustomerCode);
            PegPayCustomer cust = new PegPayCustomer();
            //cust = bll.GetCustomerDetails(CustomerCode);
            dataTable = datafile.GetAccountBalance(PegpayAccount);
            if (dataTable.Rows.Count>0)
            {
                txtPegpayAccount.Text = dataTable.Rows[0]["AccountName"].ToString();
                txtPegPayBalance.Text = dataTable.Rows[0]["AccountBalance"].ToString();
                //lblCustName.Text = cust.Fullname;
            }
            LoadCustomerAccountTypes();
            
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    private void LoadCustomerAccountTypes()
    {
        string CustomerCode = Session["CompanyCode"].ToString();
        dataTable = datafile.GetCustomerAccounts(CustomerCode);
        if (dataTable.Rows.Count > 0)
        {
            cboCustomerAccount.DataSource = dataTable;
            cboCustomerAccount.DataValueField = "AccountNumber";
            cboCustomerAccount.DataTextField = "FullName";
            cboCustomerAccount.DataBind();

            cboviewAccount.DataSource = dataTable;
            cboviewAccount.DataValueField = "AccountNumber";
            cboviewAccount.DataTextField = "FullName";
            cboviewAccount.DataBind();
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


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string Amount = txtAmount.Text;
            string AccountFrom = cboCustomerAccount.SelectedValue.ToString();
            if (Amount.Equals(""))
            {
                ShowMessage("Transfer Amount is required", true);
            }
            else
            {
                AssignViewControls();
            }
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }

    private void AssignViewControls()
    {
        string account = cboCustomerAccount.SelectedValue.ToString();
        txtViewAmount.Text=txtAmount.Text;
        txtViewPegPayAccount.Text=txtPegpayAccount.Text;
        txtViewPegpayBalance.Text=txtPegPayBalance.Text;
        cboviewAccount.SelectedIndex = cboCustomerAccount.Items.IndexOf(cboCustomerAccount.Items.FindByValue(account));
        MultiView2.ActiveViewIndex = 1;
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            ProcessPayment();
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }

    private void ProcessPayment()
    {
        try
        {
            //InterLinkClass.PegpayMMoney.Transaction trans = new InterLinkClass.PegpayMMoney.Transaction();
            InterLinkClass.PegpayMMoney.Response resp = new InterLinkClass.PegpayMMoney.Response();
           // trans=AssginTransactionValues();
            //resp = bll.SendPaymentRequest(trans);
            string Account =cboCustomerAccount.SelectedValue;
            string Amount = txtViewAmount.Text;
            resp = bll.RequestPayment(Account,Amount);
            if(resp.StatusCode.Equals("0"))
            {
                //update Bank Account;
                string CustomerCode = Session["CompanyCode"].ToString();
                string CustomerAccount = bll.GetVendorPegPayAccount(CustomerCode);
                double TranAmount = double.Parse(Amount);
                dataTable = datafile.GetAccountBalance(CustomerAccount);
                double AccountBalance = double.Parse(dataTable.Rows[0]["AccountBalance"].ToString().Trim());
                double totalbalance = TranAmount + AccountBalance;
                //string Status = datafile.UpdateCustomerAccountBalance(CustomerAccount, totalbalance);
                //if (Status.ToUpper().Equals("SUCCESS"))
                //{
                    string name = Session["FullName"].ToString();
                    string message = "Dear Customer" + name + "," +
                                           "Amount of" + Amount + "Has been transfered to Your account. Reference is:" + resp.PegPayId;
                    string Mask = "Pegasus";
                    string sender = "Pegasus";
                    datafile.logSMS(Account, message, Mask, sender);
                    ShowMessage("Fund Transfer to Your Account Has been Successful", false);
                    MultiView2.ActiveViewIndex = 0;
                //}
                //else 
                //{
                    //ShowMessage("Failed to Credit Pegasus Account",true);
                //}
            
            }
            else
            {
                ShowMessage(resp.StatusDescription,true);
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    private InterLinkClass.PegpayMMoney.Transaction AssginTransactionValues()
    {
        InterLinkClass.PegpayMMoney.Transaction trans = new InterLinkClass.PegpayMMoney.Transaction();
        string name =lblCustName.Text;
        string Phone =cboCustomerAccount.SelectedValue;
        string Amount = txtViewAmount.Text;
        trans.CustomerName = name;
        trans.FromAccount = Phone;
        //trans.ToAccount = Phone; //"256776963009";
        trans.TranAmount = Amount; //"1500";
        trans.TranCharge = "10";
        trans.TranType = "PULL";
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = 0;
            txtAmount.Text = "";
            cboCustomerAccount.SelectedIndex = cboCustomerAccount.Items.IndexOf(cboCustomerAccount.Items.FindByValue("0"));
            cboviewAccount.SelectedIndex = cboviewAccount.Items.IndexOf(cboviewAccount.Items.FindByValue("0"));
            txtViewAmount.Text = "";
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }
    protected void cboviewAccount_OnDataBound(object sender, EventArgs e)
    {
        cboviewAccount.Items.Insert(0, new ListItem("-- Select --", "0"));
    }

    protected void cboCustomerAccount_OnDataBound(object sender, EventArgs e)
    {
        cboCustomerAccount.Items.Insert(0, new ListItem("-- Select --", "0"));
    }
}
