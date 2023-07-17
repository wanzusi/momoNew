using System;
using System.Data;
using System.Data.SqlClient;
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

public partial class AddCustomerUser : System.Web.UI.Page
{
    //SystemUsers dac = new SystemUsers();
    ProcessUsers Process = new ProcessUsers();
    DataLogin datafile = new DataLogin();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    private HttpFileCollection uploads2 = HttpContext.Current.Request.Files;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                string RoleId = Session["RoleCode"].ToString();
                if (isRoleAuthorisedToVisitPage(RoleId))
                {
                    string Coporate = Session["CorporateUser"].ToString();
                    bool IsCoporate = bool.Parse(Coporate);
                    if (IsCoporate || Session["AreaID"].ToString().Equals("1"))
                    {
                        LoadRoles();
                        LoadUserType();
                        LoadLevels();
                        chkResetPassword.Enabled = false;
                        chkIsLoggedon.Enabled = false;
                        chkIsLoggedon.Checked = false;
                        chkIsActive.Checked = true;
                        cboFileLevel.Enabled = true;
                        if (Request.QueryString["transferid"] != null)
                        {
                            string UserCode = Request.QueryString["transferid"].ToString();
                            LoadControls(UserCode);
                        }
                        else
                        {
                            LoadPageInitials();
                        }

                        MultiView1.ActiveViewIndex = 0;
                        MultiView2.ActiveViewIndex = -1;
                        string strProcessScript = "this.value='Working...';this.disabled=true;";
                        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
                    }
                    else
                    {
                        ShowMessage("You do not have Right to Perform this operation", true);
                        MultiView1.ActiveViewIndex = -1;
                        MultiView2.ActiveViewIndex = -1;
                    }
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
            string Rolcode = Session["RoleCode"].ToString();
            if (Rolcode.Equals("001") && Session["AreaID"].ToString().Equals("1"))
            {
                MultiView4.ActiveViewIndex = 0;
            }
            else
            {
                string CustomerCode = Session["CustomerCode"].ToString();
                string CustomerName = Session["CustomerName"].ToString();
                txtCustomerCode.Text = CustomerCode;
                txtCustName.Text = CustomerName;
                MultiView1.ActiveViewIndex = 0;
                MultiView3.ActiveViewIndex = 0;
                MultiView4.ActiveViewIndex = -1;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void LoadLevels()
    {
        DataTable dtLevels = datafile.GetFileLevels();
        cboFileLevel.DataSource = dtLevels;
        cboFileLevel.DataValueField = "LevelRank";
        cboFileLevel.DataTextField = "LevelName";
        cboFileLevel.DataBind();
    }


    private void LoadControls(string UserCode)
    {
        int UserID = Convert.ToInt32(UserCode);
        dataTable = datafile.GetUserDetailsByID(UserID);
        lblCode.Text = dataTable.Rows[0]["Userid"].ToString();
        TxtFname.Text = dataTable.Rows[0]["FirstName"].ToString();
        txtLname.Text = dataTable.Rows[0]["SurName"].ToString();
        txtMiddleName.Text = dataTable.Rows[0]["OtherName"].ToString();
        txtemail.Text = dataTable.Rows[0]["UserEmail"].ToString();
        txtDesignation.Text = dataTable.Rows[0]["Title"].ToString();
        txtphone.Text = dataTable.Rows[0]["UserPhone"].ToString();
        lblusername.Text = dataTable.Rows[0]["Username"].ToString();
        txtCustCode.Text = dataTable.Rows[0]["UserBranch"].ToString();
        txtCustomerCode.Text = dataTable.Rows[0]["UserBranch"].ToString();
        txtConfirmEmail.Text = dataTable.Rows[0]["UserEmail"].ToString();
        string BranchID = dataTable.Rows[0]["UserBranch"].ToString();
        string RoleID = dataTable.Rows[0]["RoleCode"].ToString();
        string AreaID = dataTable.Rows[0]["UserArea"].ToString();
        string FileLevel = dataTable.Rows[0]["FileLevel"].ToString();
        bool IsActive = bool.Parse(dataTable.Rows[0]["Active"].ToString());
        bool IsLoggedOn = bool.Parse(dataTable.Rows[0]["LoggedOn"].ToString());
        cboUserType.SelectedIndex = cboUserType.Items.IndexOf(cboUserType.Items.FindByValue(AreaID));
        cboAccessLevel.SelectedIndex = cboAccessLevel.Items.IndexOf(cboAccessLevel.Items.FindByValue(RoleID));
        cboFileLevel.SelectedIndex = cboFileLevel.Items.IndexOf(cboFileLevel.Items.FindByValue(FileLevel));
        if (Session["CustomerName"]!=null)
        {
            txtCustName.Text = Session["CustomerName"].ToString();
        }
        chkIsLoggedon.Checked = IsLoggedOn;
        chkIsActive.Checked = IsActive;
        chkResetPassword.Enabled = true;
        chkIsLoggedon.Enabled = true;
        MultiView1.ActiveViewIndex = 0;
        MultiView3.ActiveViewIndex = 0;
        MultiView4.ActiveViewIndex = -1;
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
    private void LoadRoles()
    {
        bool CustomerUser = true;
        dataTable = datafile.GetSystemRoles(CustomerUser);
        cboAccessLevel.DataSource = dataTable;
        cboAccessLevel.DataValueField = "RoleCode";
        cboAccessLevel.DataTextField = "RoleName";
        cboAccessLevel.DataBind();
    }
    private void LoadUserType()
    {
        dataTable = datafile.GetUserTypes();
        cboUserType.DataSource = dataTable;
        cboUserType.DataValueField = "TypeId";
        cboUserType.DataTextField = "UserType";
        cboUserType.DataBind();
        cboUserType.SelectedIndex = cboUserType.Items.IndexOf(cboUserType.Items.FindByValue("2"));
    }
    private void GetCustomers(DataTable dtable)
    {
        cboCustomers.DataSource = dtable;
        cboCustomers.DataValueField = "CustomerCode";
        cboCustomers.DataTextField = "FullName";
        cboCustomers.DataBind();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            ValidateInputs();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void ValidateInputs()
    {

        SystemUser user = new SystemUser();
        user = GetUserDetails();
        if (user.UserType.Equals(2))
        {
            user.IsCoporate = true;
            user.IsCustomer = true;
        }
        else
        {
            user.IsCoporate = false;
            user.IsCustomer = false;
        }

        if (user.Fname.Equals(""))
        {
            ShowMessage("Please Enter User First Name", true);
            TxtFname.Focus();
        }
        else if (user.Sname.Equals(""))
        {
            ShowMessage("Please Enter User SurName", true);
            txtLname.Focus();
        }
        else if (!bll.IsValidEmailAddress(user.Email))
        {
            ShowMessage("Please Enter User Valid Email Address", true);
            txtemail.Focus();
        }
        else if (!txtemail.Text.Equals(txtConfirmEmail.Text))
        {
            ShowMessage("Email address donot Match", true);
        }
        else if (user.UserType.Equals(0))
        {
            ShowMessage("Please Select UserType Type", true);
        }
        else if (user.Role.Equals("0"))
        {
            ShowMessage("Please Select User System role", true);
        }
        else if (!bll.IsBelowIn(cboFileLevel.SelectedValue.ToString(), cboAccessLevel.SelectedValue.ToString(), user.CompanyCode))
        {
            if (cboFileLevel.SelectedValue.ToString() == "2")
            {
                ShowMessage("You need to first put " + cboAccessLevel.SelectedItem.ToString() + " at first level", true);
            }
            else
            {
                ShowMessage("You need to first put " + cboAccessLevel.SelectedItem.ToString() + " at Second level", true);
            }
        }
        else
        {
            if (user.CompanyCode.Equals(0))
            {
                ShowMessage("Please Select Company", true);
            }
            else
            {
                string returned = Process.SaveSystemUser(user);
                if (returned.Contains("Successfully"))
                {
                    ShowMessage(returned, false);
                    MultiView2.ActiveViewIndex = -1;
                    ClearControls();
                }
                else if (returned.Contains("System generated username"))
                {
                    ShowMessage(returned, true);
                    MultiView2.ActiveViewIndex = 0;
                    txtUserName.Focus(); // USERNAME PROVIDED ALREADY EXISTS
                }
                else if (returned.Contains("UserName Provided already Exists"))
                {
                    ShowMessage(returned, true);
                    MultiView2.ActiveViewIndex = 0;
                    txtUserName.Focus(); // USERNAME PROVIDED ALREADY EXISTS
                }
                else
                {
                    ShowMessage(returned, true);
                    MultiView2.ActiveViewIndex = -1;
                }
            }
        }
    }

    private SystemUser GetUserDetails()
    {
        SystemUser user = new SystemUser();
        user.Userid = int.Parse(lblCode.Text.Trim());
        user.Fname = TxtFname.Text.Trim();
        user.Sname = txtLname.Text.Trim();
        user.Oname = txtMiddleName.Text.Trim();
        user.Uname = lblusername.Text.Trim();
        user.Phone = txtphone.Text.Trim();
        user.Email = txtemail.Text.Trim();
        user.UserType = int.Parse(cboUserType.SelectedValue.ToString());
        user.CompanyCode = txtCustomerCode.Text;
        user.Title = txtDesignation.Text.Trim();
        user.Role = cboAccessLevel.SelectedValue.ToString();
        user.Active = chkIsActive.Checked;
        user.LoggedOn = chkIsLoggedon.Checked;
        user.Reset = chkResetPassword.Checked;
        user.UserName = txtUserName.Text.Trim();
        user.FileLevel = cboFileLevel.SelectedValue.ToString();
        return user;

    }

    private void CallCofirmation(string FirstName, string MiddleName, string LastName)
    {
        MultiView1.ActiveViewIndex = 1;
        string Message = "User with Names( " + FirstName + " " + MiddleName + " " + LastName + " ) is already in the System";
        lblQn.Text = Message;
    }
    protected void cboAccessLevel_DataBound(object sender, EventArgs e)
    {
        cboAccessLevel.Items.Insert(0, new ListItem("Select User Type", "0"));
    }

    protected void cboUserType_DataBound(object sender, EventArgs e)
    {
        cboUserType.Items.Insert(0, new ListItem("Select Category", "0"));
    }
    protected void cboUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int UserType = Convert.ToInt16(cboUserType.SelectedValue.ToString());
            // LoadCompanies(UserType);

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void ClearControls()
    {
        lblCode.Text = "0";
        txtDesignation.Text = "";
        txtemail.Text = "";
        TxtFname.Text = "";
        txtLname.Text = "";
        txtMiddleName.Text = "";
        txtphone.Text = "";
        txtConfirmEmail.Text = "";
        //cboUserType.SelectedIndex = cboUserType.Items.IndexOf(cboUserType.Items.FindByValue("0"));
        // cboCompany.SelectedIndex = cboCompany.Items.IndexOf(cboCompany.Items.FindByValue("0"));
        cboAccessLevel.SelectedIndex = cboAccessLevel.Items.IndexOf(cboAccessLevel.Items.FindByValue("0"));

    }
    protected void cboCorporation_DataBound(object sender, EventArgs e)
    {
        cboCustomers.Items.Insert(0, new ListItem("-- Select Corporation --", "0"));
    }
    protected void cboFileLevel_DataBound(object sender, EventArgs e)
    {
        cboFileLevel.Items.Insert(0, new ListItem("- - Select Level - -", "0"));
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {

    }
    protected void btnNo_Click(object sender, EventArgs e)
    {

    }
    protected void cboAccessLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string AccessLevel = cboAccessLevel.SelectedValue.ToString();
            if (AccessLevel.Equals("013") || AccessLevel.Equals("014") || AccessLevel.Equals("015"))
            {
                cboFileLevel.Enabled = true;
            }
            else
            {
                cboFileLevel.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string customerName = txtCustSearch.Text;
            string CustomerCode = txtCustCode.Text;
            string CustomerType = "CORPORATE";
            dataTable = datafile.GetCustomersByName(CustomerCode, customerName, CustomerType);
            GetCustomers(dataTable);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void cboCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string CustomerCode = cboCustomers.SelectedValue.ToString();
            dataTable = datafile.GetCustomerByCode(CustomerCode);
            if (dataTable.Rows.Count > 0)
            {
                txtCustomerCode.Text = CustomerCode;
                txtCustName.Text = dataTable.Rows[0]["Fname"].ToString() + " " + dataTable.Rows[0]["Lname"].ToString();
                cboUserType.SelectedIndex = cboUserType.Items.IndexOf(cboUserType.Items.FindByValue("2"));
                MultiView3.ActiveViewIndex = 0;
            }
            else
            {
                ShowMessage("Failed To Load Customer Details", true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
}