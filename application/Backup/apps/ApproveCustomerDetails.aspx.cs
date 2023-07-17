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


public partial class ApproveCustomerDetails : System.Web.UI.Page
{
    private DataLogin dac = new DataLogin();
    private ProcessUsers Process = new ProcessUsers();
    private BusinessLogin bll = new BusinessLogin();
    private PegPayCustomer customer;
    private PhoneValidator pv;
    private DataTable dataTable = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                string RoleId = Session["RoleCode"].ToString();
                if (isRoleAuthorisedToVisitPage(RoleId))
                {
                    LoadCustomerType();
                    LoadUserType();
                    lblun.Visible = false;
                    MultiView2.ActiveViewIndex = 0;
                    if (Session["LoggedIn"] != null || Session["LoggedIn"].ToString().Equals("YES"))
                    {
                        if (Request.QueryString["transfereid"] != null)
                        {
                            lblCode.Text = Request.QueryString["transfereid"].ToString();
                            LoadControls(lblCode.Text.Trim());
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
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


    // ************************* REGION NUMBER ONE *************************

    #region CLASS METHODS
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
            msg.Text = "MESSAGE: " + GetMessage.ToUpper();
        }
    }

    private void LoadUserType()
    {
        dataTable = dac.GetUserTypes();
        cboUserType.DataSource = dataTable;
        cboUserType.DataValueField = "TypeId";
        cboUserType.DataTextField = "UserType";
        cboUserType.DataBind();
    }
    private void LoadCustomerType()
    {
        try
        {
            dataTable = dac.GetCustomerTypes();
            if (dataTable.Rows.Count > 0)
            {
                cboCustomerType.DataSource = dataTable;
                cboCustomerType.DataValueField = "CustomerTypeCode";
                cboCustomerType.DataTextField = "CustomerType";
                cboCustomerType.DataBind();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ClearControls()
    {
        txtAddress2.Text = "";
        txtEmail2.Text = "";
        txtFname2.Text = "";
        txtPhone2.Text = "";
        lblpassword.Text = "";
        lblusername.Text = "";
        chkActive2.Checked = true;       
        txtEmailReconf2.Text = "";
        lblun2.Text = "";
        chkActive2.Checked = false;
        lblCode.Text = "0";
        txtLastName.Text = "";
    }
    // Database Get Methods
    private void LoadCustomersToApprove()
    {
        PegPayCustomer cust = new PegPayCustomer();
        cust.Fullname = txtSearch.Text;
        cust.CustomerType = cboCustomerType.SelectedValue.ToString();
        dataTable = dac.GetCustomersToApprove(cust);
        if (dataTable.Rows.Count > 0)
        {
            ShowMessage(".", true);
            MultiView3.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = 0;
            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        else
        {
            ShowMessage("No Customer(s) Found", true);
            MultiView3.ActiveViewIndex = -1;
        }
    }
    private void LoadControls(string code)
    {
        try
        {
            DataTable dtGetCustomersByID = dac.GetCustomerByID(code);
            if (dtGetCustomersByID.Rows.Count == 1)
            {
                MultiView3.ActiveViewIndex = -1;
                MultiView2.ActiveViewIndex = -1;
                string CustomerType = dtGetCustomersByID.Rows[0]["CustomerType"].ToString();
                bool Corporate=bool.Parse(dtGetCustomersByID.Rows[0]["Corporate"].ToString());
                if (Corporate)
                {
                    MultiView1.ActiveViewIndex = 1;
                    txtEmail2.Text = dtGetCustomersByID.Rows[0]["Email"].ToString();
                    txtEmailReconf2.Text = dtGetCustomersByID.Rows[0]["Email"].ToString();
                    txtFname2.Text = dtGetCustomersByID.Rows[0]["Fname"].ToString();
                    txtPhone2.Text = dtGetCustomersByID.Rows[0]["Phone"].ToString();
                    txtAddress2.Text = dtGetCustomersByID.Rows[0]["Address"].ToString();
                    txtLastName.Text = dtGetCustomersByID.Rows[0]["ContactPerson"].ToString();
                    chkActive2.Checked = bool.Parse(dtGetCustomersByID.Rows[0]["Active"].ToString());
                    lblCode.Text = code;
                    txtCustType.Text = dtGetCustomersByID.Rows[0]["CustomerType"].ToString();
                    lblusername.Text = dtGetCustomersByID.Rows[0]["RecordedBy"].ToString();
                }
                else
                { 
                    MultiView1.ActiveViewIndex = 0;
                    txtEmail.Text = dtGetCustomersByID.Rows[0]["Email"].ToString();
                    txtEmailReconf.Text = dtGetCustomersByID.Rows[0]["Email"].ToString();
                    txtFname.Text = dtGetCustomersByID.Rows[0]["Fname"].ToString();
                    txtLastName.Text = dtGetCustomersByID.Rows[0]["Lname"].ToString();
                    txtphone.Text = dtGetCustomersByID.Rows[0]["Phone"].ToString();
                    txtAddress.Text = dtGetCustomersByID.Rows[0]["Address"].ToString();
                    txtplaceofwork.Text = dtGetCustomersByID.Rows[0]["PlaceofWork"].ToString();
                    txtDrivingPermit.Text = dtGetCustomersByID.Rows[0]["DrivingPermit"].ToString();
                    rbnGender.SelectedValue = dtGetCustomersByID.Rows[0]["Gender"].ToString();
                    lblusername.Text = dtGetCustomersByID.Rows[0]["RecordedBy"].ToString();
                    lblCode.Text = code;
                    lblCustomerCode.Text = dtGetCustomersByID.Rows[0]["CustomerCode"].ToString();
                    cboUserType.SelectedIndex = cboUserType.Items.IndexOf(cboUserType.Items.FindByValue("4"));
                    chkActive.Checked = bool.Parse(dtGetCustomersByID.Rows[0]["Active"].ToString());
                }
            }
            else 
            {
                ShowMessage("Failed to Load Customers",true);
            }
            

            
        }
        catch(Exception ex)
        {
            throw ex;
        }
        
    }

    // Database Save Methods
    private PegPayCustomer SetRetailCustomer()
    {
        PegPayCustomer customer = new PegPayCustomer();
        string email = txtEmail.Text.Trim();
        customer.ID = int.Parse(lblCode.Text);
        customer.Address = txtAddress.Text.Trim();
        customer.DrivingPermit = txtDrivingPermit.Text.Trim();
        customer.Email = Encryption.encrypt.EncryptString(email, "10987654321ipegpay12345678910");
        customer.FirstName = txtFname.Text.Trim();
        customer.LastName = "";
        customer.PassportNo = txtPassport.Text.Trim();
        customer.Phone = bll.formatPhone(txtphone.Text.Trim());
        customer.PlaceOfWork = txtplaceofwork.Text.Trim();
        customer.RecordedBy = Session["Username"].ToString();
        customer.RecordDate = DateTime.Now;
        //customer.WorkId = txtWorkID.Text.Trim();
        customer.Gender = rbnGender.SelectedItem.Text;
        customer.Active = chkActive.Checked;
        return customer;
  
    }

    private PegPayCustomer SetNewCorporateCustomer()
    {
        PegPayCustomer customer = new PegPayCustomer();
        string email = txtEmail2.Text.Trim();
        customer.ID = int.Parse(lblCode.Text);
        customer.Address = txtAddress2.Text.Trim();
        customer.Email = Encryption.encrypt.EncryptString(email, "10987654321ipegpay12345678910");
        customer.FirstName = txtFname2.Text.Trim();
        customer.Phone = bll.formatPhone(txtPhone2.Text.Trim());
        customer.RecordedBy = Session["Username"].ToString();
        customer.RecordDate = DateTime.Now;
        customer.Active = chkActive2.Checked;
        return customer;

    }

    private void SaveCorporation(PegPayCustomer customer)
    {
        //dac.SaveCorporation(customer);
    }

    private void ApproveRetailCustomer()
    {
        try
        {
            string ApprovedBy = Session["Username"].ToString();
            string RecordedBy = lblusername.Text;
            int CustomerId = int.Parse(lblCode.Text);
            if (CustomerId.Equals(""))
            {
            //if (ApprovedBy.Equals(RecordedBy))
            //{
                ShowMessage("You Have No Rights to Activate The Customer", true);
            }
            else
            {
                PegPayCustomer cust = new PegPayCustomer();
                SystemUser user = new SystemUser();
                user = GetUserDetails();
                cust = SetRetailCustomer();
                string status = bll.ApproveInternetCustomer(cust, ApprovedBy, user);
                if (status.Equals("SUCCESS"))
                {
                    ShowMessage("Customer Approved Successfully", true);
                    dac.LogActivity(Session["UserName"].ToString(), "Approved Customer( "+ cust.FirstName+cust.LastName +" ) Details");
                    LoadCustomersToApprove();
                    ClearControls();
                    MultiView1.ActiveViewIndex = -1;
                }
                else
                {
                    ShowMessage("Failed To Approve Customer", true);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private SystemUser GetUserDetails()
    {
        SystemUser user = new SystemUser();
        user.Userid = int.Parse(lblCode.Text.Trim());
        user.Fname = txtFname.Text.Trim();
        user.Sname = txtLname.Text.Trim();
        user.Uname = txtEmail.Text.Trim();
        user.Phone = txtphone.Text.Trim();
        user.Email = txtEmail.Text.Trim();
        user.UserType = int.Parse(cboUserType.SelectedValue.ToString());
        user.CompanyCode = lblCustomerCode.Text;
        user.Title = "";
        user.Role = "009";
        user.Active = true;
        user.LoggedOn = false;
        user.UserName = txtEmail.Text.Trim();
        string passwd = bll.PasswdString(8);
        user.Passwd = bll.EncryptString(passwd);
        user.IsCoporate = false;
        user.IsCustomer=true;
        return user;

    }
    private void ApproveCoporate()
    {
        try
        {
            string ApprovedBy = Session["Username"].ToString();
            string RecordedBy = lblusername.Text;
            int CustomerId = int.Parse(lblCode.Text);
            if (CustomerId.Equals(0))
            {
           // if (ApprovedBy.Equals(RecordedBy))
           // {
                ShowMessage("You Have No Rights to Activate The Customer", true);
            }
            else
            {
                string status = dac.ApproveCustomer(CustomerId, ApprovedBy);
                if (status.Equals("SUCCESS"))
                {
                    //send mail notification to customer
                    LoadCustomersToApprove();
                    MultiView1.ActiveViewIndex = -1;
                    ClearContols();
                }
                else
                {
                    ShowMessage("Failed To Approve Customer", true);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void ClearContols()
    {
       
    }
    
    #endregion

    // *************************   END   *************************



    // ************************* REGION NUMBER TWO *************************

    #region CLASS EVENTS (ASP.NET CONTROL EVENTS)

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
            ShowMessage("", true);
            ApproveRetailCustomer();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    

    protected void rbAccountType_DataBound(object sender, EventArgs e)
    {
      
    }

    #endregion

    // *************************   END   *************************


  
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            ApproveCoporate();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    protected void cboUserType_DataBound(object sender, EventArgs e)
    {
        cboUserType.Items.Insert(0, new ListItem("Select Company Type", "0"));
    }
    protected void cboAccessLevel_DataBound(object sender, EventArgs e)
    {
        cboAccessLevel.Items.Insert(0, new ListItem("Select User Type", "0"));
    }
    protected void cboCompany_DataBound(object sender, EventArgs e)
    {
        cboCompany.Items.Insert(0, new ListItem("Select Company", "0"));
    }
    protected void cboCustomerType_DataBound(object sender, EventArgs e)
    {
        cboCustomerType.Items.Insert(0, new ListItem("ALL", "ALL"));
    }
   

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            LoadCustomersToApprove();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "btnapprove")
            {
                string CustomerId = e.Item.Cells[0].Text;
                LoadControls(CustomerId);
            }
            

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            PegPayCustomer cust = new PegPayCustomer();
            cust.Fullname = txtSearch.Text;
            cust.CustomerType = cboCustomerType.SelectedValue.ToString();
            dataTable = dac.GetCustomersToApprove(cust);
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
}
