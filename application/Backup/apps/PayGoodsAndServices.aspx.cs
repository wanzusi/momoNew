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
public partial class PayGoodsAndServices : System.Web.UI.Page
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
                string RoleId = Session["RoleCode"].ToString();
                if (isRoleAuthorisedToVisitPage(RoleId))
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
                else
                {
                    Response.Redirect("UnauthorisedAccess.aspx");
                }
            }
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
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
    private void Load_intialControls()
    {
        try
        {
            string CustomerCode = Session["CustomerId"].ToString();
            string PegpayAccount = Session["CustomerPegasusAccount"].ToString();
            PegPayCustomer cust = new PegPayCustomer();
            //cust = bll.GetCustomerDetails(CustomerCode);
            dataTable = datafile.GetAccountBalance(PegpayAccount);
            //if (dataTable.Rows.Count>0)
            //{
            //    txtPegpayAccount.Text = dataTable.Rows[0]["AccountName"].ToString();
            //    txtPegPayBalance.Text = dataTable.Rows[0]["AccountBalance"].ToString();
            //    //lblCustName.Text = cust.Fullname;
            //}
            LoadCustomerAccountTypes();
            
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    private void LoadCustomerAccountTypes()
    {
        string CustomerCode=Session["CustomerCode"].ToString();
        dataTable = datafile.GetCustomerAccounts(CustomerCode);
        if (dataTable.Rows.Count > 0)
        {
            cboCustomerAccount.DataSource = dataTable;
            cboCustomerAccount.DataValueField = "AccountNumber";
            cboCustomerAccount.DataTextField = "AccountName";
            cboCustomerAccount.DataBind();

            cboviewAccount.DataSource = dataTable;
            cboviewAccount.DataValueField = "AccountNumber";
            cboviewAccount.DataTextField = "AccountName";
            cboviewAccount.DataBind();
        }
    }

    private void LoadSubServices(string service)
    {
        dataTable = datafile.GetSubServices(service);
        cboSubservice.DataSource = dataTable;
        cboSubservice.DataValueField = "RecordId";
        cboSubservice.DataTextField = "SubserviceName";
        cboSubservice.DataBind();

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
            string Service=cboService.SelectedValue.ToString();
            string subservice=cboSubservice.SelectedValue.ToString();
            string AccountFrom = cboCustomerAccount.SelectedValue.ToString();
            
            if (Service.Equals("0"))
            {
                ShowMessage("Please Select Service", true);
            }
            else if (subservice.Equals("0"))
            {
                ShowMessage("Please Select Service Options", true);
            }
            else if (Amount.Equals(""))
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
        txtViewService.Text = cboService.SelectedItem.Text;
        txtViewSubservice.Text = cboSubservice.SelectedItem.Text;
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
            string Amount = txtViewAmount.Text.Replace(",","");
            resp = bll.RequestPayment(Account,Amount);
            if (resp.StatusCode.Equals("0"))
            {
                //deoooo
                string name = Session["FullName"].ToString();
                string message = "Dear Customer" + name + "," +
                                       "payment of" + Amount + "for " + cboService.SelectedItem.Text + " received by Pegasus. Payment Reference is:" + resp.PegPayId;
                string Mask = "Pegasus";
                string sender = "Pegasus";
                datafile.logSMS(Account, message, Mask, sender);
                ShowMessage("Payment Has been Successfully Processed You will receive a confirmation SMS Shortly", false);
                MultiView2.ActiveViewIndex = 0;
                cboCustomerAccount.SelectedValue = "0";
                cboService.SelectedValue = "0";
                cboCustomerAccount.SelectedIndex = cboCustomerAccount.Items.IndexOf(cboCustomerAccount.Items.FindByValue("0"));
                cboService.SelectedIndex = cboService.Items.IndexOf(cboService.Items.FindByValue("0"));
                cboSubservice.SelectedIndex = cboSubservice.Items.IndexOf(cboSubservice.Items.FindByValue("0"));
                txtAmount.Text = "";
                
            }
            else
            {
                ShowMessage(resp.StatusDescription, true);
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
    protected void cboSubservice_OnDataBound(object sender, EventArgs e)
    {
        cboSubservice.Items.Insert(0, new ListItem("-- Select --", "0"));
    }
    protected void cboCustomerAccount_OnDataBound(object sender, EventArgs e)
    {
        cboCustomerAccount.Items.Insert(0, new ListItem("-- Select --", "0"));
    }
    protected void cboService_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string service = cboService.SelectedValue.ToString();
            LoadSubServices(service);
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }
    protected void cboSubservice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int recordId = int.Parse(cboSubservice.SelectedValue.ToString().Trim());
            dataTable = datafile.GetSubServicesById(recordId);
            double amount = double.Parse(dataTable.Rows[0]["Amount"].ToString());
             txtAmount.Text=amount.ToString("#,##0");
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }
}
