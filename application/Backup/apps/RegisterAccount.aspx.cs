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
public partial class RegisterAccount : System.Web.UI.Page
{
    private DataLogin dac = new DataLogin();
    private BusinessLogin bll = new BusinessLogin();
    private PhoneValidator pv = new PhoneValidator();
    private DataTable dtable = new DataTable();
    private Beneficiary beneficiary = new Beneficiary();
    private SendMail mailer = new SendMail();
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

                        if (Request.QueryString["transferid"] != null)
                        {
                            string RecordId = Request.QueryString["transferid"].ToString();
                            LoadControls(RecordId);
                        }
                        else
                        {
                            LoadPageInitials();
                        }
                    }
                    else
                    {
                        Response.Redirect("Default2.aspx");
                    }
                    Toggle4Process();
                    LoadNetworkCodes();
                    Load_page_Initials();
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
            if (RoleCode.Equals("001"))
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

    private void Load_page_Initials()
    {
        try
        {
            string Rolecode = Session["RoleCode"].ToString();
            if (Rolecode.Equals("001"))
            {
                cboAccountType.Enabled = true;
            }
            else
            {
                cboAccountType.SelectedIndex = cboAccountType.Items.IndexOf(cboAccountType.Items.FindByValue("ESCROW"));
                cboAccountType.Enabled = false;
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    private void LoadNetworkCodes()
    {
        dtable = dac.GetDistinctNetwork();
        cboNetwork.DataSource = dtable;
        cboNetwork.DataTextField = "Network";
        cboNetwork.DataValueField = "Network";
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
        try
        {
            int Id = Int32.Parse(RecordId);
            dtable = dac.GetAccountDetailsById(Id);
            if (dtable.Rows.Count>0)
            {
                txtAccountName.Text = dtable.Rows[0]["AccountName"].ToString();
                txtAccountNumber.Text = dtable.Rows[0]["AccountNumber"].ToString();
                cboAccountType.SelectedValue = dtable.Rows[0]["Type"].ToString();
                txtCompanyCode.Text = dtable.Rows[0]["CustomerCode"].ToString();
                txtName.Text = dtable.Rows[0]["Company"].ToString();
                cboNetwork.SelectedValue = dtable.Rows[0]["Network"].ToString();
                chkActive.Checked = bool.Parse(dtable.Rows[0]["Active"].ToString());
                MultiView1.ActiveViewIndex = -1;
                MultiView2.ActiveViewIndex = 0;
                
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
        string AccountName = txtAccountName.Text;
        string AccountNumber = pv.FormatTelephone(txtAccountNumber.Text.Trim());
        string AccountType = cboAccountType.SelectedValue.ToString();
        string CompanyCode=txtCompanyCode.Text;
        string CompanyName=txtName.Text;
        string Network = cboNetwork.SelectedValue.ToString();
        bool Active=chkActive.Checked;
        string recordId = lblCode.Text;
        if (CompanyCode.Equals(""))
        {
            ShowMessage("Please Select Company", true);
        }
        else if (AccountName.Equals(""))
        {
            ShowMessage("Enter Account Name", true);
        }
        else if (AccountNumber.Equals(""))
        {
            ShowMessage("Please Enter Account Number", true);
            txtAccountNumber.Focus();
        }
        else if (AccountType.Equals(""))
        {
            ShowMessage("Please Select the Account Type",true);
        }
        else if (Network.Equals("") && !AccountType.Equals("AIRTIMESUSP"))
        {
            ShowMessage("Please Select the Network", true);
        }
        else
        {
            if (pv.PhoneNumbersOk(AccountNumber) || AccountType.Equals("AIRTIMESUSP"))
            {
                string AccountNetwork = pv.GetNetwork(AccountNumber);
                AccountNumber = bll.formatPhone(AccountNumber);
                //if (AccountNetwork.Equals(Network))
                //{
                    bool exists = bll.AccountExists(AccountNumber);
                    if (exists && recordId.Equals("0"))
                    {
                        ShowMessage("Account Already Registered against a Company", true);
                    }
                    else
                    {
                        //deo
                        string status = bll.SaveAccountDetails(recordId, CompanyCode, AccountName, AccountNumber, AccountType, Network, Active);
                        if (status.Equals("OK"))
                        {
                            string message = "Details of Account Number " + AccountNumber + "Have been saved successfully";
                            ShowMessage(message, false);
                            ClearControls();
                        }
                        else
                        {
                            ShowMessage("Failed to Save Account Details", true);
                        }
                    }
                //}
                //else
                //{
                //    ShowMessage("Invalid Network Selected For the Account", true);
                //}
            }
            else
            {
                ShowMessage("Invalid Phone Format", true);
            }
        }
    }
    private void ClearControls()
    {
         txtAccountName.Text="";
         txtAccountNumber.Text="";
         cboAccountType.SelectedIndex = cboAccountType.Items.IndexOf(cboAccountType.Items.FindByValue("0"));
         txtCompanyCode.Text="";
         txtName.Text="";
         cboNetwork.SelectedIndex = cboNetwork.Items.IndexOf(cboNetwork.Items.FindByValue("0"));
         chkActive.Checked=false;
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
            txtName.Text = CompanyName;
            txtCompanyCode.Text = companyCode;
            MultiView1.ActiveViewIndex = -1;
            MultiView2.ActiveViewIndex = 0;
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }

    }

    protected void cboAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string AcctType = cboAccountType.SelectedValue.ToString();
            if (AcctType.Equals("AIRTIMESUSP"))
            {
                cboNetwork.SelectedValue = "";
                cboNetwork.Enabled = false;
            }
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
        cboNetwork.Items.Insert(0, new ListItem("-- Select Network --", ""));
    }

    protected void cboAccountType_DataBound(object sender, EventArgs e)
    {
        //cboAccountType.Items.Insert(0, new ListItem("-- Select Network --", "0"));
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = -1;
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }
}
