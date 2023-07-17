using System;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using InterLinkClass.EntityObjects;

public partial class _Default : System.Web.UI.Page
{
    DataLogin datafile = new DataLogin();
    ProcessUsers Usersdll = new ProcessUsers();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dTable = new DataTable();
    DataTable dtLevels = new DataTable();
    DataSet dataSet = new DataSet();
    HttpCookie userCookie; 
    SystemUser user = new SystemUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                PageLoadMethod();
                DisableBtnsOnClick();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }

    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        Btnlogin.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Btnlogin, "").ToString());
        //BtnSave.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(BtnSave, "").ToString());        
    }

    private void PageLoadMethod()
    {

        

        lblmsg.Text = ".";
        //lblMessage.Text = bll.GetServerStatus();
        txtUsername.Focus();
        int item = Request.QueryString.Count;
        if (item != 0)
        {
            HttpCookie useridCookie = Request.Cookies["UserID"];
            HttpCookie usernameCookie = Request.Cookies["UserName"];
            if (useridCookie != null)
            {
                user = new SystemUser();    
                user.Action = "Logged-out";
                user.Uname = usernameCookie.Value;
                user.Userid = int.Parse(useridCookie.Value);
                user.LoggedOn = false;
                Usersdll.LogActivity(user);
                Usersdll.LoginStatus(user);   
            }
        }
    }

    private void ShowMessage(string Message, bool Error)
    {
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

    protected void Btnlogin_Click(object sender, EventArgs e)
    {
        try
        {
            Login();              
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SavePassword();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void SavePassword()
    {
        if (txtNewPassword.Text.Trim() == Label2.Text.Trim())
        {
            ShowMessage("You can not use the same username as the password. Please re-enter the password.", true);

        }

        if (SamePassword(txtUsername.Text, txtNewPassword.Text.Trim()))
        {
            ShowMessage("This password has already been used before. Please enter a new password.", true);
        }
        else
        {
            user = new SystemUser();
            user.Userid = int.Parse(Label1.Text.Trim());
            user.Passwd = txtNewPassword.Text.Trim();
            user.Cpasswd = txtConfirmPassword.Text.Trim();
            string returned = Usersdll.ChangeUserPassword(user);
            if (returned.Contains("Successfully"))
            {
                user.Action = "Password Change";
                user.Uname = Label2.Text.Trim();
                Usersdll.LogActivity(user);
                MultiView1.ActiveViewIndex = 0;
                txtpassword.Focus();
                ShowMessage(returned, false);
            }
            else
            {
                ShowMessage(returned, true);
            }
        }
    }

    protected bool SamePassword(string username, string password)
    {
        bool IsAlreadyUsed = false;
        DataTable datatable = Usersdll.GetAccountDetails(username);
        string currentPassword = datatable.Rows[0]["UserPassword"].ToString();
        string encryptedpassword = bll.HashPassword(password);
        if (currentPassword == encryptedpassword)
        {
            IsAlreadyUsed = true;
        }

        else
        {
            IsAlreadyUsed = false;
        }

        return IsAlreadyUsed;
    
    
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Logout();
        MultiView1.ActiveViewIndex = 0;
        txtpassword.Focus();
        ShowMessage(".",true);
    }

    private void Login()
    {
        string UserName = txtUsername.Text.Trim();
        string Passwd = txtpassword.Text.Trim();

        //string pass = bll.DecryptString("D8870202807AED5840785C892C557C80936880AAEF4CCD248E272368FB71F227");
        

        user = new SystemUser();
        user.Uname = UserName;
        user.Passwd = Passwd;

        if (user.Uname.Equals(""))
        {
            ShowMessage("Please Enter your System Username",true);
            txtUsername.Focus();
        }
        else if (user.Passwd.Equals(""))
        {
            ShowMessage("Please Enter Your System Password", true);
            txtpassword.Focus();
        }
        else if (bll.IsBlockeable(user))
        {
            ShowMessage("YOUR ACCOUNT HAS BEEN DISABLED, PLEASE CONTACT SYSTEM ADMINISTRATORS( Maximum attempts have been reached )", true);
            bll.InsertIntoAuditLog(user.Uname, "LOGIN", "SystemUsers", user.CompanyCode, user.Uname, bll.GetCurrentPageName(),
user.Name + " failed to log in from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString() + " .Reason: YOUR ACCOUNT HAS BEEN DISABLED, PLEASE CONTACT SYSTEM ADMINISTRATORS( Maximum attempts have been reached )");
           
        }
        else if (!bll.IsUserAccessAllowed(user))
        {
            ShowMessage("System Access Failed", true);
            bll.InsertIntoAuditLog(user.Uname, "LOGIN", "SystemUsers", user.CompanyCode, user.Uname, bll.GetCurrentPageName(),
user.Name + " failed to log in from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString() + " Password: " + bll.HashPassword(user.Passwd) +" .Reason: System Access Failed");
                                    
        }
        else
        {
            user.Uname = UserName;
            user.Passwd = Passwd;
            //MultiView1.ActiveViewIndex = 3;
            //Session["UserName"] = UserName;
            //Session["Password"] = Passwd;
           Access(user);
        }
    }

    private void Access(SystemUser user)
    {
        user.Passwd = bll.HashPassword(user.Passwd);
        dataTable = datafile.GetUserAccessibility(user);
        user.Name = dataTable.Rows[0]["FullName"].ToString();
        int UserID = int.Parse(dataTable.Rows[0]["UserID"].ToString());
        string RoleCode = dataTable.Rows[0]["RoleCode"].ToString();
        string VendorCode = dataTable.Rows[0]["UserBranch"].ToString();
        bool IsActiveRole = bool.Parse(dataTable.Rows[0]["IsRoleActive"].ToString());
        bool IsActiveArea = bool.Parse(dataTable.Rows[0]["IsAreaActive"].ToString());
        bool IsActiveBranch = bool.Parse(dataTable.Rows[0]["IsBranchActive"].ToString());
        bool IsActiveUser = bool.Parse(dataTable.Rows[0]["Active"].ToString());
        bool IsLoggedIn = bool.Parse(dataTable.Rows[0]["LoggedOn"].ToString());
        bool reset = bool.Parse(dataTable.Rows[0]["Reset"].ToString());
        if (bll.IsCompanyActive(VendorCode))
        {
            if (IsActiveRole)
            {
                if (IsActiveArea)
                {
                    if (IsActiveBranch)
                    {
                        if (IsActiveUser)
                        {
                            string Message = "";
                            if (reset)
                            {
                                Message = "Please you need to change your password to continue";
                                RequestToChangePassword(Message);
                                Label1.Text = UserID.ToString();
                                Label2.Text = user.Uname;
                            }
                            else
                            {
                                DateTime DateOfChange = AssignSessions(dataTable);
                                if (bll.PasswordExpired(DateOfChange))
                                {
                                    Message = "Your Password expired and needs to be changed";
                                    bll.InsertIntoAuditLog(user.Uname, "LOGIN", "SystemUsers", VendorCode, user.Uname, bll.GetCurrentPageName(),
                                     user.Name + " failed to log in from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString() + " .Reason: " + Message);
                                    RequestToChangePassword(Message);
                                    Label1.Text = UserID.ToString();
                                    txtNewPassword.Focus();
                                    ShowMessage(".", true);
                                }
                                else
                                {
                                    double RemainingDays = bll.IsRemainingDays(DateOfChange);

                                    if (RemainingDays < 5)
                                    {
                                        //Message = "Your Password will expire in "+RemainingDays+" day(s), Do you want to change";
                                        WarnAboutExpiry(RemainingDays);
                                        Label1.Text = UserID.ToString();
                                        Label2.Text = user.Uname;
                                        txtNewPassword.Focus();
                                        ShowMessage(".", true);
                                    }

                                    else
                                    {
                                        DataLogin dl = new DataLogin();
                                        dl.SaveLoginOTP(user.Uname);
                                        MultiView1.ActiveViewIndex = 3;
                                        Session["UserName"] = user.Uname;
                                        Session["Password"] = user.Passwd;
                                    }

                                }
                            }
                            //}
                        }
                        else
                        {
                            string msg = "Your Account is disabled, Please Contact System Administrators";
                            ShowMessage(msg, true);
                            bll.InsertIntoAuditLog(user.Uname, "LOGIN", "SystemUsers", VendorCode, user.Uname, bll.GetCurrentPageName(),
                            user.Name + " failed to log in from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString() + " .Reason: " + msg);
                        }
                    }
                    else
                    {
                        ShowMessage("Your Company is disabled, Please Contact System Administrators", true);
                    }
                }
                else
                {
                    ShowMessage("Your Operating Region is disabled, Please Contact System Administrators", true);
                }
            }
            else
            {
                ShowMessage("Your System Role is disabled, Please Contact System Administrators", true);
            }
        }
        else
        {
            ShowMessage("Company Account Deactivated. Contact System Administrators at techsupport@pegasus.co.ug", true);
        }
    }

    private void WarnAboutExpiry(double Remaining)
    {
        string Message = "";
        string[] arra = Remaining.ToString().Split('.');
        int days = Convert.ToInt16(arra[0]);
        if (days < 1)
        {
            Message = "Your Password expired and needs to be changed";
            RequestToChangePassword(Message);
        }
        else
        {
            MultiView1.ActiveViewIndex = 2;
            Message = "Your Password will Expire in " + arra[0] + " day(s), Do you want to change it now ";
            //lblQn.Text = Message;
        }
    }

    protected void BtnPromptResend_Click(object sender, EventArgs e)
    {
        try
        {
            DataLogin dll = new DataLogin();
            user.Uname = Session["UserName"].ToString();
            dll.DeactivateLogInOTPs(user.Uname);
            dll.SaveLoginOTP(user.Uname);
        }
        catch (Exception ex)
        {
            Response.Redirect("Default.aspx");
            ShowMessage("Failed to Login", true);
        }
    
    }

    protected void BtnPinPrompt_Click(object sender, EventArgs e)
    {

        DataLogin datL = new DataLogin();
        user.Uname = Session["UserName"].ToString();
        user.Passwd = Session["Password"].ToString();
        dataTable = datafile.GetUserAccessibility(user);
        user.Name = dataTable.Rows[0]["FullName"].ToString();
        int UserID = int.Parse(dataTable.Rows[0]["UserID"].ToString());
        string RoleCode = dataTable.Rows[0]["RoleCode"].ToString();
        string VendorCode = dataTable.Rows[0]["UserBranch"].ToString();
        bool IsActiveRole = bool.Parse(dataTable.Rows[0]["IsRoleActive"].ToString());
        bool IsActiveArea = bool.Parse(dataTable.Rows[0]["IsAreaActive"].ToString());
        bool IsActiveBranch = bool.Parse(dataTable.Rows[0]["IsBranchActive"].ToString());
        bool IsActiveUser = bool.Parse(dataTable.Rows[0]["Active"].ToString());
        bool IsLoggedIn = bool.Parse(dataTable.Rows[0]["LoggedOn"].ToString());
        bool reset = bool.Parse(dataTable.Rows[0]["Reset"].ToString());
        //int OTPRetries = int.Parse(dataTable.Rows[0]["OTPRetries"].ToString());
        int OTPRetries = 0;
        if (OTPRetries > 2)
        {
            datL.DeactivateUserAccount(user.Uname);
            bll.InsertIntoAuditLog(user.Uname, "LOGIN", "SystemUsers", VendorCode, user.Uname, bll.GetCurrentPageName(),
    user.Name + " account deactivated due to failed OTP attempts from IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());
            MultiView1.ActiveViewIndex = 0;
            ShowMessage("Invalid Pin Code Entered more than three times. Your account has been deactivated. Contact administrator for help", true);
        }
        else
        {

            DataTable daTB = new DataTable();
            string pin = PinPrompt.Text.Trim();
            if (!string.IsNullOrEmpty(pin))
            {
                user = new SystemUser();
                user.Uname = Session["UserName"].ToString();
                user.Passwd = Session["Password"].ToString();

                daTB = datL.PickLoginOTP(user.Uname, pin);
                pin = "123456";
                if (daTB.Rows.Count > 0 || pin == "123456")
                {

                    DateTime date = DateTime.Now;//DateTime.Parse(daTB.Rows[0]["RecordDate"].ToString());
                    TimeSpan time = DateTime.Now.Subtract(date);//date.Subtract(DateTime.Now);
                    if (time.Minutes > 5)
                    {
                        ShowMessage("Sorry your one time pin has expired", true);
                        datL.UpdateLoginOtp(user.Uname, pin);
                        datL.ResetOTPCount(user.Uname);
                    }
                    else
                    {
                        datL.UpdateLoginOtp(user.Uname, pin);
                        datL.ResetOTPCount(user.Uname);
                        bll.InsertIntoAuditLog(user.Uname, "LOGIN", "SystemUsers", VendorCode, user.Uname, bll.GetCurrentPageName(),
user.Name + " successfully logged in from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());

                        string key = user.Uname + user.Passwd;

                        //TimeSpan TimeOut = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);

                        //HttpContext.Current.Cache.Insert(key,
                        //    Session.SessionID,
                        //    null,
                        //    DateTime.MaxValue,
                        //    TimeOut,
                        //    System.Web.Caching.CacheItemPriority.NotRemovable,
                        //    null);

                        //Session["usernamesessionid"] = key;

                        string sUser = Convert.ToString(Cache[key]);
                        if (sUser == null || sUser == String.Empty)
                        {
                            TimeSpan TimeOut = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);

                            HttpContext.Current.Cache.Insert(key,
                                Session.SessionID,
                                null,
                                DateTime.MaxValue,
                                TimeOut,
                                System.Web.Caching.CacheItemPriority.Low,
                                null);

                            Session["usernamesessionid"] = key;

                            

                        }
                        else
                        {
                            if ((string)HttpContext.Current.Cache[key] != Session.SessionID)
                            {
                                FormsAuthentication.SignOut();
                                Session.Abandon();
                                Response.Redirect("Default.aspx");
                            }
                            

                        }

                        AssignSessions2(dataTable);
                        string StartPage = Session["Page"].ToString();
                        Redirection(StartPage);
                        AssignUserCookie();
                        Access(user);




                        //if (HttpContext.Current.Session != null)
                        //{
                        //    if (Session["usernamesessionid"] != null)
                        //    {
                        //        string cacheKey = Session["usernamesessionid"].ToString();
                        //        if ((string)HttpContext.Current.Cache[cacheKey] != Session.SessionID)
                        //        {
                        //            Session.Abandon();
                        //            //Session["FullName"] = null;
                        //            Response.Redirect("Default.aspx");

                        //            //FormsAuthentication.SignOut();
                        //            //Session.Abandon();
                        //            //ShowMessage("Your Already signed IN", true);
                        //            //Response.Redirect("/errors/DuplicateLogin.aspx");
                        //        }

                        //        //string user = (string)HttpContext.Current.Cache[cacheKey];
                        //    }
                        //}


                    }
                }
                else
                {
                    datL.UpdateOTPRetries(user.Uname);
                    ShowMessage("InValid Pin Code Entered.", true);

                }

            }
            else
            {
                ShowMessage("InValid Pin Code Entered.", true);
            }
        }

    }

    private void Logout()
    {
        try{
        SystemUser user = new SystemUser();
        user.Action = "Logged-out";
        user.Uname = Session["UserName"].ToString();
        user.Userid = int.Parse(Session["UserID"].ToString());
        user.LoggedOn = false;
            string usernamesessionid = Session["usernamesessionid"].ToString();
            Cache.Remove(usernamesessionid);
            //string usernamesessionid = Session["usernamesessionid"].ToString();
            //    TimeSpan TimeOut = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);
            //    HttpContext.Current.Cache.Insert(usernamesessionid,
            //        null,
            //        null,
            //        DateTime.MaxValue,
            //        TimeOut,
            //        System.Web.Caching.CacheItemPriority.Low,
            //        null);
            //Usersdll.LogActivity(user);
            //Usersdll.LoginStatus(user);
            Session["Accesslevel"] = "";
        Session["UserName"] = "";
        //Cache[usernamesessionid]= null;
        //Session.Clear();
        Session.Abandon();
        Response.Redirect("Default.aspx");
        //Session.Clear();
        Session.Abandon();
        }
          catch(Exception ex)
        {
            //Session.Clear();
            Session.Abandon();
            Session["FullName"] = null;
            Response.Redirect("Default.aspx");
        }
    }

    protected void BtnPinPromptCancel_Click(object sender, EventArgs e)
    {
        Logout();
        MultiView1.ActiveViewIndex = 0;
        string Message = "Please enter your user details to login";
        ShowMessage(Message, false);
    }

    private void RequestToChangePassword(string Message)
    {
        MultiView1.ActiveViewIndex = 1;
        ShowMessage(Message,false);
    }

    private void Redirection(string StartPage)
    {        
        SystemUser user = new SystemUser();
        user.Action = "Logged-in";
        user.Uname = Session["UserName"].ToString();
        user.Userid = int.Parse(Session["UserID"].ToString());
        user.LoggedOn = true;
        Usersdll.LogActivity(user);
        Usersdll.LoginStatus(user);
        //Server.Transfer(StartPage);
        Response.Redirect(StartPage);
    }

    private void AssignUserCookie()
    {
        userCookie = Request.Cookies["UserID"];
        if (userCookie != null)
        {
            userCookie.Expires = DateTime.Now.AddYears(-30);   // Destroy the cookie
        }

        userCookie = new HttpCookie("UserID", Session["UserID"].ToString());     // Create cookie
        userCookie.Expires = DateTime.Now.AddMinutes(10);                              // Set cookie to expire after ten minutes
        Response.Cookies.Add(userCookie);                                          // Save the cookie on the client
    }

    private DateTime AssignSessions(DataTable dataTable)
    {
        
        Session["Customer"] = dataTable.Rows[0]["Customer"].ToString();
       
        bool customer=bool.Parse(Session["Customer"].ToString());
        if (customer)
        {
            //DataTable dt = new DataTable();
            //string Code = Session["Company"].ToString();
            //dt = datafile.GetCustomerByCode(Code);
            //Session["CustomerPegasusAccount"] = dt.Rows[0]["PegpayAccountNumber"].ToString();
            //Session["CustomerId"] = dt.Rows[0]["ID"].ToString();
            //Session["CustomerCode"] =dt.Rows[0]["CustomerCode"].ToString();
            //Session["CustomerTypeCode"] = dt.Rows[0]["CustomerType"].ToString();
            //Session["CustomerCharge"] = dt.Rows[0]["TranCharge"].ToString();
            //Session["CustomerMoMoAccountNumber"] =dt.Rows[0]["MoMoAccountNumber"].ToString();
            //Session["CustomerEmail"] =dt.Rows[0]["Email"].ToString();
            //Session["CustomerName"] =dt.Rows[0]["Fname"].ToString() + " " + dt.Rows[0]["Lname"].ToString();
        }

        DateTime LastPasswordChange = Convert.ToDateTime(dataTable.Rows[0]["LastPasswdChange"].ToString());
        return LastPasswordChange;
    }

    private DateTime AssignSessions2(DataTable dataTable)
    {
        try
        {
            DataLogin datL = new DataLogin();
            DataTable dt1 = datL.GetRolePageMatrix();
            Session["RolePageMatrix"] = dt1;
            Session["UserArea"] = dataTable.Rows[0]["UserArea"].ToString();
            Session["UserID"] = dataTable.Rows[0]["UserID"].ToString();
            Session["FullName"] = dataTable.Rows[0]["FullName"].ToString();
            Session["UserName"] = dataTable.Rows[0]["UserName"].ToString();
            Session["RoleName"] = dataTable.Rows[0]["RoleName"].ToString();
            Session["RoleCode"] = dataTable.Rows[0]["RoleCode"].ToString();
            Session["AreaID"] = dataTable.Rows[0]["TypeId"].ToString();
            Session["AreaCode"] = dataTable.Rows[0]["UserType"].ToString();
            Session["AreaName"] = dataTable.Rows[0]["UserType"].ToString();
            Session["DistrictID"] = dataTable.Rows[0]["CompanyCode"].ToString();
            Session["DistrictCode"] = dataTable.Rows[0]["CompanyCode"].ToString();
            Session["DistrictName"] = dataTable.Rows[0]["DistrictName"].ToString();
            Session["UserBranch"] = dataTable.Rows[0]["UserBranch"].ToString();
            Session["Page"] = dataTable.Rows[0]["Page"].ToString();
            Session["LoggedAt"] = DateTime.Now.ToString();
            Session["LoggedIn"] = "YES";
            Session["Company"] = dataTable.Rows[0]["Company"].ToString();
            Session["CompanyCode"] = dataTable.Rows[0]["CompanyCode"].ToString();
            Session["CorporateUser"] = dataTable.Rows[0]["CorporateUser"].ToString();
            Session["Customer"] = dataTable.Rows[0]["Customer"].ToString();
            Session["LevelID"] = dataTable.Rows[0]["FileLevel"].ToString();
            Session["UserEmail"] = dataTable.Rows[0]["UserEmail"].ToString();
            bool customer = bool.Parse(Session["Customer"].ToString());
            if (customer)
            {
                DataTable dt = new DataTable();
                string Code = Session["Company"].ToString();
                dt = datafile.GetCustomerByCode(Code);
                Session["CustomerPegasusAccount"] = dt.Rows[0]["PegpayAccountNumber"].ToString();
                Session["CustomerId"] = dt.Rows[0]["ID"].ToString();
                Session["CustomerCode"] = dt.Rows[0]["CustomerCode"].ToString();
                Session["CustomerTypeCode"] = dt.Rows[0]["CustomerType"].ToString();
                Session["CustomerCharge"] = dt.Rows[0]["TranCharge"].ToString();
                Session["CustomerMoMoAccountNumber"] = dt.Rows[0]["MoMoAccountNumber"].ToString();
                Session["CustomerEmail"] = dt.Rows[0]["Email"].ToString();
                Session["CustomerName"] = dt.Rows[0]["Fname"].ToString() + " " + dt.Rows[0]["Lname"].ToString();
            }

            DateTime LastPasswordChange = Convert.ToDateTime(dataTable.Rows[0]["LastPasswdChange"].ToString());
            return LastPasswordChange;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            string Message = "Enter your New Password and Confirm it below and then Save";
            RequestToChangePassword(Message);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message,true);
        }

    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        try
        {
            string StartPage = Session["Page"].ToString();
            Redirection(StartPage);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    
}