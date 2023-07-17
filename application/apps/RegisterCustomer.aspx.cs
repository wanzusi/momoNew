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
public partial class RegisterCustomer : System.Web.UI.Page
{
  private DataLogin dac = new DataLogin();
  private BusinessLogin bll = new BusinessLogin();
   //private Customer customer;
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
                    lblun.Visible = false;
                    LoadCustomerType();
                    LoadChargeTypes();
                    if (Session["LoggedIn"] != null || Session["LoggedIn"].ToString().Equals("YES"))
                    {
                        if (Request.QueryString["transferid"] != null)
                        {
                            lblCode.Text = Request.QueryString["transferid"].ToString();
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

    // Business Logic Methods
    private void LoadCustomerType()
    {
        try
        {
            dataTable = dac.GetCustomerTypes();
            if (dataTable.Rows.Count > 0)
            {
                rbAccountType.DataSource = dataTable;
                rbAccountType.DataValueField = "CustomerTypeCode";
                rbAccountType.DataTextField = "CustomerType";
                rbAccountType.DataBind();
            }
        }
        catch(Exception ex)
        {
            throw ex;  
        }

    }
    private void LoadChargeTypes()
    {
        try
        {
            dataTable = dac.GetChargeTypes();
            cboChargeType.DataSource = dataTable;
            cboChargeType.DataValueField = "TypeId";
            cboChargeType.DataTextField = "ChargeName";
            cboChargeType.DataBind();

            cboChargeType2.DataSource = dataTable;
            cboChargeType2.DataValueField = "TypeId";
            cboChargeType2.DataTextField = "ChargeName";
            cboChargeType2.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CallValidations()
    {
        pv = new PhoneValidator();
        double res=0;
        if (txtFname.Text.Trim() == "")
        {
            ShowMessage("First Name Required", true);
        }
        if (txtLName.Text.Trim() == "")
        {
            ShowMessage("Last Name Required", true);
        }
        else if ((txtEmail.Text.Trim() == ""))
        {
            ShowMessage("Email Required for IB Subscription", true);
        }
        else if (!txtEmail.Text.Trim().Equals(txtEmailReconf.Text.Trim()))
        {
               
            ShowMessage("Email and Email Reconfirmation are not the same", true);
            
        }
        else if (cboChargeType.SelectedValue.EndsWith("0"))
        {
            ShowMessage("Select Pegasus Charge Type", true);
        }
        else if ((txtPegpayCharge.Text == ""))
        {
            ShowMessage("Pegasus Charge is required", true);
        }
        else if (!Double.TryParse(txtPegpayCharge.Text, out res))
        {
            ShowMessage("Invalid Pegasus Charge", true);
        }
        else if (!txtphone.Text.Trim().Equals("") && !pv.PhoneNumbersOk(txtphone.Text.Trim()))
        {
            ShowMessage("Phone Number Format not valid. Make sure number is not landline and is of a valid format".ToUpper(), true);
        }
        else if (rbnGender.SelectedValue == "")
        {
            ShowMessage("Gender Required", true);
        }

        else if (bll.IsDuplicateNumber(txtphone.Text.Trim()) && lblCode.Text.Equals("0"))
        {
            ShowMessage("Phone Number " + txtphone.Text.Trim() + " is already registered against Another Customer", true);
        }
        else if (txtDrivingPermit.Text.Trim() == "" && txtPassport.Text.Trim() == "")
        {
            ShowMessage("Driving Permit or Passport or Work ID Number is required", true);
        }
        else
        {
            bool validator = bll.IsValidEmailAddress(txtEmail.Text.Trim()); /// 
            if (validator)
            {
                //string email_exists = bll.EmailExists(txtEmail.Text.Trim());
                //if (email_exists.Equals("OK"))
                //{
                bool corporate = false;
                SaveData(corporate);
                //}
                //else
                //{
                //    ShowMessage(email_exists, true);
                //}
            }
            else
            {
                ShowMessage("Invalid Email Address", true);
                txtEmail.Focus();
            }
        }
    }
    private void CallCorporateValidations()
    {
        pv = new PhoneValidator();
        double res = 0;
        if (txtFname2.Text.Trim() == "")
        {
            ShowMessage("First Name Required", true);
        }
        else if (txtContactPerson.Text.Trim() == "")
        {
            ShowMessage("User Last Name Required", true);
            txtContactPerson.Focus();
        }
        else if (txtEmail2.Text.Trim() == "" && chkActive.Checked)
        {
            ShowMessage("Email Required for IB Subscription", true);
        }
        else if (!txtEmail2.Text.Trim().Equals(txtEmailReconf2.Text.Trim()))
        {
            ShowMessage("Email and Email Reconfirmation are not the same", true);
        }
        else if (cboChargeType2.SelectedValue.EndsWith("0"))
        {
            ShowMessage("Select Pegasus Charge Type", true);
        }
        else if ((txtPegPayCharge2.Text == ""))
        {
            ShowMessage("Pegasus Charge is required", true);
        }
        else if (!Double.TryParse(txtPegPayCharge2.Text, out res))
        {
            ShowMessage("Invalid Pegasus Charge", true);
        }
        else if (txtPhone2.Text.Trim() == "")
        {
            ShowMessage("Mobile Required", true);
        }
        else if (!txtPhone2.Text.Trim().Equals("") && !pv.PhoneNumbersOk(txtPhone2.Text.Trim()))
        {
            ShowMessage("Phone Number Format not valid. Make sure number is not landline and is of a valid format".ToUpper(), true);
        }
        else
        {
            //string email_exists = bll.EmailExists(txtEmail2.Text.Trim());
            //if (email_exists.Equals("OK"))
            //{
                bool Corporate = true;
                SaveData(Corporate);
            //}
            //else
            //{
            //    ShowMessage(email_exists, true);
            //}
        }
    }  
    private void ClearControls()
    {
        txtAddress.Text = "";
        txtEmail.Text = "";
        txtFname.Text = "";
        txtphone.Text = "";
        lblpassword.Text = "";
        lblusername.Text = "";
        chkActive.Checked = true;
        txtplaceofwork.Text = "";
        txtPassport.Text = "";
        txtOtherID.Text = "";
        txtDrivingPermit.Text = "";
        rbnGender.SelectedIndex = -1;
        txtEmailReconf.Text = "";
        lblun.Text = "";
        lblCode.Text = "0";
        chkActive.Checked = false;
        string Type = rbAccountType.SelectedValue.ToString();
        //DisplayCustomerAccounts(Type);


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
        txtContactPerson.Text = "";
    }
    

    private void LoadControls(string code)
    {
        try
        {
            DataTable dtGetCustomersByID = dac.GetCustomerByID(code);
            if (dtGetCustomersByID.Rows.Count == 1)
            {
                string CustomerType = dtGetCustomersByID.Rows[0]["CustomerType"].ToString();
                bool Corporate = bool.Parse(dtGetCustomersByID.Rows[0]["Corporate"].ToString());
                if (Corporate)
                {
                    rbAccountType.SelectedIndex = rbAccountType.Items.IndexOf(rbAccountType.Items.FindByValue("CORPORATE"));
                    MultiView1.ActiveViewIndex = 1;
                    txtEmail2.Text = dtGetCustomersByID.Rows[0]["Email"].ToString();
                    txtEmailReconf2.Text = dtGetCustomersByID.Rows[0]["Email"].ToString();
                    txtFname2.Text = dtGetCustomersByID.Rows[0]["Fname"].ToString();
                    txtContactPerson.Text = dtGetCustomersByID.Rows[0]["ContactPerson"].ToString();
                    txtPhone2.Text = dtGetCustomersByID.Rows[0]["Phone"].ToString();
                    txtAddress2.Text = dtGetCustomersByID.Rows[0]["Address"].ToString();
                    chkActive2.Checked = bool.Parse(dtGetCustomersByID.Rows[0]["Active"].ToString());
                    lblCode.Text = code;
                }
                else
                {
                    rbAccountType.SelectedIndex = rbAccountType.Items.IndexOf(rbAccountType.Items.FindByValue("RETAIL"));
                    MultiView1.ActiveViewIndex = 0;
                    txtEmail.Text = dtGetCustomersByID.Rows[0]["Email"].ToString();
                    txtEmailReconf.Text = dtGetCustomersByID.Rows[0]["Email"].ToString();
                    txtFname.Text = dtGetCustomersByID.Rows[0]["Fname"].ToString();
                    txtContactPerson.Text = dtGetCustomersByID.Rows[0]["Lname"].ToString();
                    txtphone.Text = dtGetCustomersByID.Rows[0]["Phone"].ToString();
                    txtAddress.Text = dtGetCustomersByID.Rows[0]["Address"].ToString();
                    txtplaceofwork.Text = dtGetCustomersByID.Rows[0]["PlaceofWork"].ToString();
                    //txtOtherID.Text = dtGetCustomersByID.Rows[0]["OtherID"].ToString();
                    txtDrivingPermit.Text = dtGetCustomersByID.Rows[0]["DrivingPermit"].ToString();
                    rbnGender.SelectedValue = dtGetCustomersByID.Rows[0]["Gender"].ToString();
                    lblCode.Text = code;
                }
                rbAccountType.Enabled = false;
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

    private void UpdateAccounts(string Type) 
    {
    //    if (Type == "0")
    //    {
    //        string un = Encryption.encrypt.EncryptString(lblusername.Text.Trim(), "10987654321ipegpay12345678910");
    //        DataTable dtGetUserAccountsALL = dac.GetUserAccountsALL(un);
    //        dtGetUserAccountsALL = bll.DecryptDatatable(dtGetUserAccountsALL);

    //        foreach (ListItem lst in chkAccounts.Items)
    //        {
    //            if (lst.Selected == true)
    //            {
    //                foreach (DataRow dr in dtGetUserAccountsALL.Rows)
    //                {
    //                    if ((lst.Value == dr["AccountNumber"].ToString()) && (dr["Active"].ToString().ToLower() == "true"))
    //                    {
                            
    //                    }
    //                    else if ((lst.Value == dr["AccountNumber"].ToString()) && (dr["Active"].ToString().ToLower() == "false"))
    //                    {
    //                        // Selected and In-active
    //                        // Update to Active
    //                        string acc = Encryption.encrypt.EncryptString(lst.Value, "10987654321ipegpay12345678910");
    //                        dac.EditCustomerAccounts(customer.Username, acc, true);
    //                    }
    //                    else
    //                    {
    //                        // Selected and Not In Table
    //                        // Save in Table
    //                        string acc = Encryption.encrypt.EncryptString(lst.Value, "10987654321ipegpay12345678910");
    //                        if (!dac.AccountExists(acc,customer.Username))
    //                        {
    //                            dac.SaveCustomerAccounts(customer.Username, acc, true);
    //                        }
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                foreach (DataRow dr in dtGetUserAccountsALL.Rows)
    //                {
    //                    if ((lst.Value == dr["AccountNumber"].ToString()) && (dr["Active"].ToString().ToLower() == "true"))
    //                    {
    //                        // Not Selected and active
    //                        //   Update to In-Active
    //                        string acc = Encryption.encrypt.EncryptString(lst.Value, "10987654321ipegpay12345678910");
    //                        dac.EditCustomerAccounts(customer.Username, acc, false);
    //                    }
    //                    else if ((lst.Value == dr["AccountNumber"].ToString()) && (dr["Active"].ToString().ToLower() == "false"))
    //                    {
    //                        // Not Selected and In-active
    //                        //  ******* Do nothing ********
    //                    }
    //                    else
    //                    {
    //                        // Not Selected and Not In Table
    //                        //  ******* Do nothing ********
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        string un = Encryption.encrypt.EncryptString(lblusername.Text.Trim(), "10987654321ipegpay12345678910");
    //        DataTable dtGetUserAccountsALL = dac.GetUserAccountsALL(un);
    //        dtGetUserAccountsALL = bll.DecryptDatatable(dtGetUserAccountsALL);

    //        foreach (ListItem lst in chkAccounts2.Items)
    //        {
    //            if (lst.Selected == true)
    //            {
    //                foreach (DataRow dr in dtGetUserAccountsALL.Rows)
    //                {
    //                    if ((lst.Value == dr["AccountNumber"].ToString()) && (dr["Active"].ToString().ToLower() == "true"))
    //                    {
    //                        string acc = Encryption.encrypt.EncryptString(lst.Value, "10987654321ipegpay12345678910");
    //                        dac.SaveCustomerAccounts(customer.Username, acc, true);
    //                    }                      
    //                }
    //            }
    //            else
    //            {
    //                foreach (DataRow dr in dtGetUserAccountsALL.Rows)
    //                {
    //                    if ((lst.Value == dr["AccountNumber"].ToString()) && (dr["Active"].ToString().ToLower() == "true"))
    //                    {
    //                        string acc = Encryption.encrypt.EncryptString(lst.Value, "10987654321ipegpay12345678910");
    //                        //dac.SaveCustomerAccounts(customer.Username, acc, true);
    //                        dac.RemoveAccount(customer.Username, acc);
    //                    }  
    //                }
    //            }
    //        }

    //    }
    }

    private void SaveAccounts(string Type,PegPayCustomer customer) 
    {
    //    if (Type == "0")
    //    {
    //        foreach (ListItem lst in chkAccounts.Items)
    //        {
    //            if (lst.Selected == true)
    //            {
    //                string acc = Encryption.encrypt.EncryptString(lst.Value, "10987654321ipegpay12345678910");
    //                if (!dac.AccountExists(acc,customer.Username))
    //                {
    //                    dac.SaveCustomerAccounts(customer.Username, acc, true);
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        foreach (ListItem lst in chkAccounts2.Items)
    //        {
    //            if (lst.Selected == true)
    //            {
    //                string acc = Encryption.encrypt.EncryptString(lst.Value, "10987654321ipegpay12345678910");
    //                if (!dac.AccountExists(acc,customer.Username))
    //                {
    //                    dac.SaveCustomerAccounts(customer.Username, acc, true);
    //                }
    //            }
    //        }
    //    }

    }

    private string IsAccountSubscribed(string Type)
    {
        string output = "OK";
        //string acc = "";
        //bool status = false;
        //if (Type == "0")
        //{            
        //    foreach (ListItem lst in chkAccounts.Items)
        //    {
        //        if (lst.Selected == true)
        //        {
        //            acc = Encryption.encrypt.EncryptString(lst.Value, "10987654321ipegpay12345678910");
        //            if (dac.AccountExists(acc,customer.Username))
        //            {
        //                status = true;
        //                output = acc+" is already subscribed";
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    foreach (ListItem lst in chkAccounts2.Items)
        //    {
        //        if (lst.Selected == true)
        //        {
        //            acc = Encryption.encrypt.EncryptString(lst.Value, "10987654321ipegpay12345678910");
        //            if (lst.Selected == true)
        //            {
        //                acc = Encryption.encrypt.EncryptString(lst.Value, "10987654321ipegpay12345678910");
        //                if (dac.AccountExists(acc,customer.Username))
        //                {
        //                    status = true;
        //                    output = acc + " is already subscribed";
        //                }
        //            }
        //        }
        //    }
        //}
        return output;

    }

    private void SaveData(bool Corporate)
    {
        try 
        {
            //CreateChildControls /Update Customer
            PegPayCustomer customer = new PegPayCustomer();
            if (Corporate)
            {
                customer =SetNewCorporateCustomer();
            }
            else
            {
                customer = SetNewRetailCustomer();
            }
            string type = rbAccountType.SelectedValue.ToString();
            string Status = dac.InsertintoCustomersHolding(customer, Corporate, type);
            if (Status.Equals("OK"))
            {

                ClearControls();
                ShowMessage("Customer Registered Successfully and Pending Approval",false);
            }
            else 
            {
                ClearControls();
                ShowMessage("Failed To Register Customer", true);
            }
        }
        catch(Exception ex)
        {
            throw ex;

        }

    }


    private PegPayCustomer SetNewRetailCustomer()
    {
        PegPayCustomer customer = new PegPayCustomer();
        string email = txtEmail.Text.Trim();
        string pass = bll.GetPasswordString();
        customer.ID = int.Parse(lblCode.Text);
        customer.Address = txtAddress.Text.Trim();
        customer.CustomerType = rbAccountType.SelectedValue.ToString();
        customer.DrivingPermit = txtDrivingPermit.Text.Trim();
        customer.Email = email;
        customer.FirstName = txtFname.Text.Trim();
        customer.LastName = txtLName.Text.Trim();
        customer.PassportNo = txtPassport.Text.Trim();
        customer.Phone = bll.formatPhone(txtphone.Text.Trim());
        customer.PlaceOfWork = txtplaceofwork.Text.Trim();
        customer.RecordedBy = Session["Username"].ToString();
        customer.RecordDate = DateTime.Now;
        //customer.WorkId = txtWorkID.Text.Trim();
        customer.Gender = rbnGender.SelectedItem.Text;
        customer.Active = chkActive.Checked;
        customer.Password = bll.HashPassword(pass);
        customer.PegasusCharge = Convert.ToDouble(txtPegpayCharge.Text.Trim());
        customer.ChargeType = int.Parse(cboChargeType.SelectedValue.ToString().Trim());
        return customer;
  
    }

    private PegPayCustomer SetNewCorporateCustomer()
    {
        PegPayCustomer customer = new PegPayCustomer();
        string email = txtEmail2.Text.Trim();
        string pass= bll.GetPasswordString();
        customer.ID = int.Parse(lblCode.Text);
        customer.CustomerType = rbAccountType.SelectedValue.ToString();
        customer.Address = txtAddress2.Text.Trim();
        customer.Email = email;
        customer.FirstName = txtFname2.Text.Trim();
        customer.Phone = bll.formatPhone(txtPhone2.Text.Trim());
        customer.RecordedBy = Session["Username"].ToString();
        customer.RecordDate = DateTime.Now;
        customer.Active = chkActive2.Checked;
        customer.Password = bll.HashPassword(pass);
        customer.PegasusCharge = Convert.ToDouble(txtPegPayCharge2.Text.Trim());
        customer.ChargeType = int.Parse(cboChargeType2.SelectedValue.ToString().Trim());
        return customer;

    }

    

    private void SaveCorporation(PegPayCustomer customer)
    {
        //dac.SaveCorporation(customer);
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
            CallValidations();
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
            CallCorporateValidations();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void cboChargeType_DataBound(object sender, EventArgs e)
    {
        cboChargeType.Items.Insert(0, new ListItem("      Select Charge Type     ", "0"));
    }
    protected void cboChargeType2_DataBound(object sender, EventArgs e)
    {
        cboChargeType2.Items.Insert(0, new ListItem("      Select Charge Type     ", "0"));
    }
    protected void rbAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ShowMessage(".", true);
            if (rbAccountType.SelectedValue.ToString() == "RETAIL")
            {
                MultiView1.ActiveViewIndex = 0;
            }
            else
            {
                MultiView1.ActiveViewIndex = 1;               
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }


    protected void btnAcctNo_Click(object sender, EventArgs e)
    {
        try
        {


        }
        catch(Exception ex)
        {
        }
    }
}
