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
public partial class CreditAccount : System.Web.UI.Page
{
    private DataLogin dac = new DataLogin();
    private BusinessLogin bll = new BusinessLogin();
    private PhoneValidator pv = new PhoneValidator();
    private DataTable dtable = new DataTable();
    private Beneficiary beneficiary = new Beneficiary();
    private SendMail mailer = new SendMail();

    string username = "";
    string fullname = "";
    string userBranch = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            username = Session["UserName"] as string;
            fullname = Session["FullName"] as string;
            userBranch = Session["UserBranch"] as string;

            if (IsPostBack == false)
            {
                string RoleId = Session["RoleCode"].ToString();
                if (isRoleAuthorisedToVisitPage(RoleId))
                {
                    if (Session["LoggedIn"] != null || Session["LoggedIn"].ToString().Equals("YES"))
                    {

                        LoadPageInitials();
                    }
                    else
                    {
                        Response.Redirect("Default2.aspx");
                    }
                    Toggle4Process();
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
    private void LoadPageInitials()
    {
        try
        {
            string RoleCode = Session["RoleCode"].ToString();
            string AreaId = Session["AreaID"].ToString();
            LoadNetworks();
            if ((RoleCode.Equals("001") || RoleCode.Equals("003") || RoleCode.Equals("007")) && AreaId.Equals("1"))
            {
                MultiView1.ActiveViewIndex = 0;
            }
            else
            {

                string CompanyCode = Session["CompanyCode"].ToString();
                string CompanyName = Session["DistrictName"].ToString();
                MultiView2.ActiveViewIndex = 0;
                txtCompanyCode.Text = CompanyCode;
                txtName.Text = CompanyName;
                btnCancel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void LoadNetworks()
    {
        dtable = dac.GetNetworks();
        cboNetwork.DataSource = dtable;
        cboNetwork.DataValueField = "Network";
        cboNetwork.DataTextField = "Network";
        cboNetwork.DataBind();
    }

    private void Toggle4Process()
    {
        string strProcessScript = "this.value='Processing...';this.disabled=true;";
        btnSave.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSave, "").ToString());
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

    private void LoadControls(string RecordId)
    {
        //try
        //{
        //    int Id = Int32.Parse(RecordId);
        //    dtable = dac.GetAccountDetailsById(Id);
        //    if (dtable.Rows.Count>0)
        //    {
        //        txtAccountName.Text = dtable.Rows[0]["AccountName"].ToString();
        //        txtAccountNumber.Text = dtable.Rows[0]["AccountNumber"].ToString();
        //        cboAccountType.SelectedValue = dtable.Rows[0]["Type"].ToString();
        //        txtCompanyCode.Text = dtable.Rows[0]["CustomerCode"].ToString();
        //        txtName.Text = dtable.Rows[0]["Company"].ToString();
        //        cboNetwork.SelectedValue = dtable.Rows[0]["Network"].ToString();
        //        chkActive.Checked = bool.Parse(dtable.Rows[0]["Active"].ToString());
        //        MultiView1.ActiveViewIndex = -1;
        //        MultiView2.ActiveViewIndex = 0;
                
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
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
        string CompanyCode = txtCompanyCode.Text;
        string CompanyName = txtName.Text;
        string AccountNumber = txtAccNumber.Text;
        string Network = cboNetwork.SelectedValue.ToString();
        string stringAmt = txtCreditAmount.Text.Trim().Replace(",","");
        string TelecomId = TextBox1.Text;
        string Reason = txt_reason.Text;
        double amt = 0;
        bool validAmount = double.TryParse(stringAmt, out amt);
        string recordId = lblCode.Text;
        if (CompanyCode.Equals(""))
        {
            ShowMessage("Please Select Company", true);
        }
        else if (AccountNumber.Equals(""))
        {
            ShowMessage("Account Number not retrieved", true);
        }
        else if (!validAmount)
        {
            ShowMessage("Please Enter Correct Amount", true);
        }
        else if (Network.Equals("0"))
        {
            ShowMessage("Please Select Network", true);
        }
        else if (Reason.Equals("0"))
        {
            ShowMessage("Please Select Network", true);
        }
        else if (PastCreditTime())
        {
            ShowMessage("Account Crediting Disabled at this time.", true);
        }
        else
        {
            string userId = Session["Username"] as string;
            string page = bll.GetCurrentPageName();

            double Amount = double.Parse(stringAmt);
            string status = bll.CreditAccountWithTelecomID(CompanyCode, AccountNumber, Amount, Network, TelecomId, Reason);
            if (status.Equals("OK"))
            {
                bll.InsertIntoAuditLog(AccountNumber, "CREATE", "Initiate Credit", CompanyCode, userId, page,
  fullname + " successfully initiated a credit of [" + Amount.ToString() + "]  for the vendorCode [" + CompanyCode + "] from the IP:" + bll.GetIPAddress() + "at " + DateTime.Now.ToString());

                string message = "The Account Number " + AccountNumber + " Have been Credited with " + Amount.ToString() + " and is Pending Approval. With a Telecom ID of : " + TelecomId;
                ShowMessage(message, false);
                ClearControls();
            }
        }
    }

    private bool PastCreditTime()
    {
        ArrayList badHours = new ArrayList();
        ArrayList badDays = new ArrayList();
        badHours = dac.GetHoursNotToCredit();
        badDays = dac.GetDaysNotToCredit();
        bool pastCreditTime = true;
        DateTime current = DateTime.Now;
        if (badDays.Contains(current.DayOfWeek.ToString().ToUpper()))
        {
            pastCreditTime = true;
        }
        else
        {
            int hour = current.Hour;
            if (badHours.Contains(hour))
            {
                pastCreditTime = true;
            }
            else
            {
                pastCreditTime = false;
            }
        }
        return pastCreditTime;
    }
    private void ClearControls()
    {
         txtCompanyCode.Text="";
         txtName.Text="";
         txtCreditAmount.Text = "";
         txtCompanyCode.Text = "";
         txtAccountBalance.Text = "";
         txtAccNumber.Text = "";
         txtSearchCode.Text = "";
         txtsearchName.Text = "";
         cboCompanyCode.SelectedValue = "0";
         lblCode.Text="0";
         btnSave.Enabled = false;

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string CompanyName = txtsearchName.Text;
            string CompanyCode = txtSearchCode.Text;
            dtable = dac.GetSystemCompanies(CompanyName, CompanyCode);
            if (dtable.Rows.Count>0)
            {
                ShowMessage(".", false);
                LoadCompanies(dtable);
            }
            else
            {
                ShowMessage("No Companies Found For the Search",true);
            }
        }
        catch(Exception ex)
        {
           ShowMessage(ex.Message,true);
        }
    }
   
    private void LoadCompanies(DataTable dtable)
    {
        try
        {
            cboCompanyCode.DataSource = dtable;
            cboCompanyCode.DataValueField = "CompanyCode";
            cboCompanyCode.DataTextField = "Company";
            cboCompanyCode.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void cboCompanyCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string companyCode = cboCompanyCode.SelectedValue.ToString();
            string CompanyName = cboCompanyCode.SelectedItem.Text;
            string AccountNumber = "";
            string AccountBalance = "";
            dtable = dac.GetPegPayAccount(companyCode);
            if (dtable.Rows.Count>0)
            {
                AccountNumber = dtable.Rows[0]["AccountNumber"].ToString();
                AccountBalance = dtable.Rows[0]["AccountBalance"].ToString();
            }
            txtName.Text = CompanyName;
            txtAccNumber.Text = AccountNumber;
            txtAccountBalance.Text = AccountBalance;
            txtCompanyCode.Text = companyCode;
            MultiView1.ActiveViewIndex = -1;
            MultiView2.ActiveViewIndex = 0;
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }

    }
    protected void cboCompanyCode_DataBound(object sender, EventArgs e)
    {
        cboCompanyCode.Items.Insert(0, new ListItem("-- Select Company --", "0"));
    }
    protected void cboNetwork_DataBound(object sender, EventArgs e)
    {
        cboCompanyCode.Items.Insert(0, new ListItem("-- Select Network --", "0"));
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = -1;
            cboCompanyCode.SelectedValue = "0";
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }
}
