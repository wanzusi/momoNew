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

public partial class AddUser : System.Web.UI.Page
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
                    LoadRoles();
                    LoadUserType();
                    LoadNotificationTypes();
                    if (Session["AreaID"].ToString().Equals("1"))
                    {
                        chkResetPassword.Enabled = false;
                        chkIsLoggedon.Enabled = false;
                        chkIsLoggedon.Checked = false;
                        chkIsActive.Checked = true;
                        if (Request.QueryString["transferid"] != null)
                        {
                            string UserCode = Request.QueryString["transferid"].ToString();
                            LoadControls(UserCode);
                        }

                        MultiView1.ActiveViewIndex = 0;
                        MultiView2.ActiveViewIndex = -1;
                        string strProcessScript = "this.value='Working...';this.disabled=true;";
                        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
                    }
                    else
                    {
                        ShowMessage("You Do not Have rights To Make this Operation", true);
                        MultiView1.ActiveViewIndex = -1;
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
        string BranchID = dataTable.Rows[0]["UserBranch"].ToString();
        string RoleID = dataTable.Rows[0]["RoleCode"].ToString();
        string AreaID = dataTable.Rows[0]["UserArea"].ToString();
        string OTPDeliveryType = dataTable.Rows[0]["OTPDeliveryType"].ToString();
        bool IsActive = bool.Parse(dataTable.Rows[0]["Active"].ToString());
        bool IsLoggedOn = bool.Parse(dataTable.Rows[0]["LoggedOn"].ToString());
        cboUserType.SelectedIndex = cboUserType.Items.IndexOf(cboUserType.Items.FindByValue(AreaID));
        LoadCompanies(int.Parse(AreaID));
        cboCompany.SelectedIndex = cboCompany.Items.IndexOf(cboCompany.Items.FindByValue(BranchID));
        cboAccessLevel.SelectedIndex = cboAccessLevel.Items.IndexOf(cboAccessLevel.Items.FindByValue(RoleID));
        ddNotificationType.SelectedIndex = ddNotificationType.Items.IndexOf(ddNotificationType.Items.FindByValue(OTPDeliveryType));
        chkIsLoggedon.Checked = IsLoggedOn;
        chkIsActive.Checked = IsActive;
        chkResetPassword.Enabled = true;
        chkIsLoggedon.Enabled = true;     
        //if(
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
        bool CustomerUser = false;
        dataTable = datafile.GetSystemRoles(CustomerUser); 
        cboAccessLevel.DataSource = dataTable;
        cboAccessLevel.DataValueField = "RoleCode";
        cboAccessLevel.DataTextField = "RoleName";
        cboAccessLevel.DataBind();
    }
    private void LoadNotificationTypes()
    {
        string category = "OTP";
        dataTable = datafile.GetNotificationTypes(category);
        ddNotificationType.DataSource = dataTable;
        ddNotificationType.DataValueField = "NotificationTypeCode";
        ddNotificationType.DataTextField = "NotificationType";
        ddNotificationType.DataBind();
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

    private void LoadCompanies(int UserType)
    {
        dataTable = datafile.GetBranches(UserType);
        dataTable = datafile.GetCompanies(UserType);
        cboCompany.DataSource = dataTable;
        cboCompany.DataValueField = "CompanyCode";
        cboCompany.DataTextField = "Company";
        cboCompany.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            if (dataTable.Rows.Count == 1)
            {
                cboCompany.Enabled = false;
                string CompanyCode = dataTable.Rows[0]["CompanyCode"].ToString();
                cboCompany.SelectedIndex = cboCompany.Items.IndexOf(cboCompany.Items.FindByValue(CompanyCode));
            }
            else
            {
                cboCompany.Enabled = true;
            }

        }
        else
        {
            cboCompany.Enabled = false;
        }
        //MultiView3.ActiveViewIndex = 0;

        //if (UserType.Equals(2)||UserType.Equals(3))
        //{
            
        //}
        //else
        //{
        //    MultiView3.ActiveViewIndex = -1;
        //    cboCompany.SelectedIndex = cboCompany.Items.IndexOf(cboCompany.Items.FindByValue("0"));
        //    if(UserType.Equals(4))
        //    {
        //        cboAccessLevel.SelectedIndex = cboAccessLevel.Items.IndexOf(cboAccessLevel.Items.FindByValue("0"));
        //        cboAccessLevel.Enabled = false;
        //    }
        //    else
        //    {
        //        cboAccessLevel.Enabled = true;
        //    }
        //}
        
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
            ShowMessage("Please Enter User First Name",true);
            TxtFname.Focus();
        }
        else if (user.Sname.Equals(""))
        {
            ShowMessage("Please Enter User SurName",true);
            txtLname.Focus();
        }
        else if (!bll.IsValidEmailAddress(user.Email))
        {
            ShowMessage("Please Enter User Valid Email Address",true);
            txtemail.Focus();
        }
        else if (user.UserType.Equals(0))
        {
            ShowMessage("Please Select UserType Type",true);
        }
        else if (user.Role.Equals("0"))
        {
            ShowMessage("Please Select User System role",true);
        }
        else
        {
            if (user.CompanyCode.Equals(0))
            {
                ShowMessage("Please Select Company",true);
            }
            else
            {
                string returned = Process.SaveSystemUser(user);
                if (returned.Contains("Successfully"))
                {
                    ShowMessage(returned,false);
                    MultiView2.ActiveViewIndex = -1;
                    ClearControls();
                }  
                else if(returned.Contains("System generated username"))
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
                    ShowMessage(returned,true);
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
        user.CompanyCode = cboCompany.SelectedValue.ToString();
        user.Title = txtDesignation.Text.Trim();
        user.Role = cboAccessLevel.SelectedValue.ToString();
        user.Active = chkIsActive.Checked;
        user.LoggedOn = chkIsLoggedon.Checked;
        user.Reset = chkResetPassword.Checked;
        user.UserName = txtUserName.Text.Trim();
        user.NotificationType = ddNotificationType.SelectedValue.ToString();
        user.FileLevel = "";
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
            LoadCompanies(UserType);
 
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);  
        }
    }

    protected void ddlNotification_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    protected void ddlNotificaction_DataBound(object sender, EventArgs e)
    {



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
        cboUserType.SelectedIndex = cboUserType.Items.IndexOf(cboUserType.Items.FindByValue("0"));
        cboCompany.SelectedIndex = cboCompany.Items.IndexOf(cboCompany.Items.FindByValue("0"));
        cboAccessLevel.SelectedIndex = cboAccessLevel.Items.IndexOf(cboAccessLevel.Items.FindByValue("0"));
        ddNotificationType.SelectedIndex = 0;
        chkIsActive.Checked = false;
        chkIsLoggedon.Checked = false;
        chkResetPassword.Checked = false;
    }
    protected void cboCompany_DataBound(object sender, EventArgs e)
    {
        cboCompany.Items.Insert(0, new ListItem("Select Company", "0"));
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {

    }
    protected void btnNo_Click(object sender, EventArgs e)
    {

    }
    protected void cboAccessLevel_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}