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

public partial class ReverseTelecomTransaction : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                Button MenuReport = (Button)Master.FindControl("btnCalReports");
                Button MenuRecon = (Button)Master.FindControl("btnAccounts");
                Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                MenuTool.Font.Underline = false;
                MenuPayment.Font.Underline = false;
                MenuReport.Font.Underline = false;
                MenuRecon.Font.Underline = true;
                MenuAccount.Font.Underline = false;
                MenuBatching.Font.Underline = false;
                LoadUtilityCompany();   
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadVendors()
    {
        //dtable = datafile.GetAllVendors("0");
        dtable = datafile.GetSystemCompanies("", "");
        dll_vendorCode.DataSource = dtable;
        dll_vendorCode.DataValueField = "CompanyCode";
        dll_vendorCode.DataTextField = "Company";
        dll_vendorCode.DataBind();
    }
    private void LoadUtilityCompany()
    {
        try
        {
            LoadVendors();
        }
        catch (Exception ee)
        {

            ShowMessage(ee.Message, true);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string userId = Session["Username"] as string;
            string fullname = Session["Fullname"] as string;
            string userBranch = Session["CustomerCode"] as string;
            string page = bll.GetCurrentPageName();

            string pegpayId = txt_pegpayId.Text;
            string telecomId = txtm_telecomId.Text;
            string vendorCode =  dll_vendorCode.SelectedValue.ToString();
            string reason = txtReason.Text;
            string amount = txtAmount.Text.Replace(",", "");
            string amount2 = lbl_amount.Text.Replace(",","");
            int amt1, amt2;

            if (!Int32.TryParse(amount2, out amt2))
            {
                ShowMessage("Erroneous transaction information in original amount", true);
            }
            if (string.IsNullOrEmpty(pegpayId))
            {
                ShowMessage("Please provide a pegpay id ", true);
            }
            else if (string.IsNullOrEmpty(telecomId))
            {
                ShowMessage("Please provide a telecom id ", true);
            }
            else if (!Int32.TryParse(amount, out amt1))
            {
                ShowMessage("Invalid transaction amount", true);
            }
            else if (reason.Equals(""))
            {
                ShowMessage("Enter Reason", true);

            }
            else if (amt1 > amt2)
            {
                ShowMessage("Amount being reversed can't be less than the orignal transaction amount", true);
            }
            else
            {
                bool success = datafile.ReverseSuccessFullTelecomTransaction(pegpayId, amount, reason, telecomId);
                if (success)
                {
                    bll.InsertIntoAuditLog(pegpayId, "REVERSE", "ReceivedTransaction", userBranch, userId, page,
   fullname + " successfully reversed the transaction with PegPayId:" + pegpayId + ",TelecomId:" + telecomId + " with the vendorCode [" + vendorCode + "] from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());
                        
                    ShowMessage("Transaction is successfully reversed".ToUpper(), false);
                    txt_pegpayId.Text = "";
                    txtm_telecomId.Text = "";
                    txtPegpayId.Text = "";
                    txtm_telecomId.Text = "";
                    txtReason.Text = "";
                    txtAmount.Text = "";
                    lbl_amount.Text = "";
                }
                else
                {
                    ShowMessage("Operation has failed".ToUpper(), true);
                }
            }
            
        }
        catch (Exception)
        {
            
            throw;
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
    protected void btnfindit_Click(object sender, EventArgs e)
    {
        string pegpayId = txt_pegpayId.Text;
        string telecomId = txtm_telecomId.Text;
        string vendorCode = dll_vendorCode.SelectedValue.ToString();
    
        if (string.IsNullOrEmpty(pegpayId))
        {
            ShowMessage("Please provide a pegpay id ", true);
        }
        else if (string.IsNullOrEmpty(telecomId))
        {
            ShowMessage("Please provide a telecom id ", true);
        }
        else
        {
            //	
            DataTable table = datafile.GetTransactionToReverse(vendorCode, pegpayId, telecomId);
            if (table.Rows.Count > 0)
            {
                DataRow drow = table.Rows[0];
                string PegPayTranId = drow["PegPayTranId"].ToString();
                string TelecomId = drow["TelecomId"].ToString();
                string recorddate = drow["PayDate"].ToString();
                string amount = drow["TranAmount"].ToString();

                txtPegpayId.Text = PegPayTranId;
                txt_telecomId.Text = TelecomId;
                txtAmount.Text = amount;
                lbl_recorddate.Text = recorddate;
                lbl_amount.Text = amount;
                txtPegpayId.Enabled = false;
                txt_telecomId.Enabled = false;

                
            }
            else
            {
                ShowMessage("Transaction not found, please ensusre that transaction was not reversed before", true);
            }
        }
    }
}
