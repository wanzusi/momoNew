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

public partial class AddPosOwnerKYC : System.Web.UI.Page
{
    private DataLogin dac = new DataLogin();
    private BusinessLogin bll = new BusinessLogin();
    private PhoneValidator pv = new PhoneValidator();
    private DataTable dtable = new DataTable();
    private SendMail mailer = new SendMail();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                if (Session["LoggedIn"] != null || Session["LoggedIn"].ToString().Equals("YES"))
                {

                    if (Request.QueryString["transferid"] != null)
                    {
                        string RecordId = Request.QueryString["transferid"].ToString();
                        LoadControls(RecordId);
                    }
                    else if (Request.QueryString["VendorCode"] != null)
                    {
                        string VendorCode = Request.QueryString["VendorCode"].ToString();
                        txtVendorCode.Text = VendorCode;
                        MultiView1.ActiveViewIndex = 0;
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
                Load_page_Initials();

            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
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
                //string CompanyCode = Session["CompanyCode"].ToString();
                //string CompanyName = Session["DistrictName"].ToString();
                //MultiView2.ActiveViewIndex = 0;
                //txtCompanyCode.Text = CompanyCode;
                //txtName.Text = CompanyName;
                //btnCancel.Visible = false;
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
            LoadCustomerType();
            LoadBusinessTypes();
            LoadIdTypes();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   
    private void Toggle4Process()
    {
        string strProcessScript = "this.value='Processing...';this.disabled=true;";
        //btnSave.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSave, "").ToString());
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

            dtable = dac.GetCustomerKycById(RecordId);
            if (dtable.Rows.Count > 0)
            {
                MultiView1.ActiveViewIndex = 0;
                lblVendorCode.Text = dtable.Rows[0]["VendorCode"].ToString();
                txtVendorCode.Text = dtable.Rows[0]["VendorCode"].ToString();
                txtFname.Text = dtable.Rows[0]["FirstName"].ToString();
                txtLname.Text = dtable.Rows[0]["LastName"].ToString();
                txtOtherName.Text = dtable.Rows[0]["OtherName"].ToString();
                txtcontact1.Text = dtable.Rows[0]["Contact1"].ToString();
                txtContact2.Text = dtable.Rows[0]["Contact2"].ToString();
                rbnGender.SelectedValue = dtable.Rows[0]["Sex"].ToString(); ;
                txtNattionality.Text = dtable.Rows[0]["Nationality"].ToString();
                txtAddress.Text = dtable.Rows[0]["Address"].ToString();
                txtEmail.Text = dtable.Rows[0]["Email"].ToString();
                cboCustomerType.SelectedValue = dtable.Rows[0]["OwnerType"].ToString().Trim(); ;
                cboBusinessType.SelectedValue = dtable.Rows[0]["BusinessType"].ToString().Trim(); ;
                txtTradingName.Text = dtable.Rows[0]["TradingName"].ToString();
                txtCompanyReg.Text = dtable.Rows[0]["CompanyRegNo"].ToString();
                txtTin.Text = dtable.Rows[0]["Tin"].ToString();
                txtRegion.Text = dtable.Rows[0]["Region"].ToString();
                txtDistrict.Text = dtable.Rows[0]["District"].ToString();
                txtCustomerIdNo.Text = dtable.Rows[0]["IdNumber"].ToString();
                chkIsActive.Checked = bool.Parse(dtable.Rows[0]["Active"].ToString());
                cboCustomerIdType.SelectedValue = dtable.Rows[0]["IdType"].ToString().Trim(); 
            }
            else
            {
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("Failed to retive Agent KYC", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    
    }
    protected void btnOK_Click(object sender, EventArgs e)
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
        CustomerKYC cusKyc =new CustomerKYC();
        cusKyc.Vendorcode =txtVendorCode.Text.Trim();
        cusKyc.Fname= txtFname.Text.Trim();
        cusKyc.Lname = txtLname.Text.Trim();
        cusKyc.OtherName = txtOtherName.Text.Trim();
        cusKyc.Contact1 = txtcontact1.Text;
        cusKyc.Contact2 = txtContact2.Text;
        cusKyc.Gender = rbnGender.SelectedValue.ToString();
        cusKyc.Nationality = txtNattionality.Text;
        cusKyc.Address = txtAddress.Text.Trim();
        cusKyc.Email = txtEmail.Text;
        cusKyc.CustomerType = cboCustomerType.SelectedValue.ToString();
        cusKyc.BusinessType = cboBusinessType.SelectedValue.ToString();
        cusKyc.TradingName = txtTradingName.Text.Trim();
        cusKyc.CompanyRegNo = txtCompanyReg.Text.Trim();
        cusKyc.CompanyTin = txtTin.Text.Trim();
        cusKyc.Region = txtRegion.Text.Trim();
        cusKyc.District = txtDistrict.Text.Trim();
        cusKyc.CustomerIdNo = txtCustomerIdNo.Text.Trim();
        cusKyc.CustomerIdType = cboCustomerIdType.SelectedValue.ToString();
        if(cusKyc.Vendorcode.Equals(""))
        {
            ShowMessage("Vendor Code not Set",true);
        }else if(cusKyc.Fname.Equals(""))
        {
            ShowMessage("First Name is required",true);
        }
        else if(cusKyc.CustomerType.Equals("1") && cusKyc.Lname.Equals(""))
        {
             ShowMessage("Last Name is required",true);
        }
        else if(cusKyc.CustomerType.Equals("1") && cusKyc.DateofBirth.Equals(""))
        {
            ShowMessage("Date of Birth is required", true);
        }
        else if(cusKyc.Contact1.Equals(""))
        {
            ShowMessage("First Contact is Required", true);
        }
        else if (cusKyc.CustomerType.Equals("1") && cusKyc.Gender.Equals(""))
        {
            ShowMessage("Gender is required",true);
        }
        else if (cusKyc.CustomerType.Equals("1") && cusKyc.Nationality.Equals(""))
        {
            ShowMessage("Nationality is required", true);
        }
        else if(cusKyc.Address.Equals(""))
        {
            ShowMessage("Address is required", true);
        }
        else if (!bll.IsValidEmailAddressOptional(cusKyc.Email))
        {
            ShowMessage("Invalid Email Address", true);
        }
        else if(cusKyc.CustomerType.Equals("0"))
        {
            ShowMessage("Customer Type is required", true);
        }
        else if(cusKyc.BusinessType.Equals("0"))
        {
            ShowMessage("Bussiness Type is required", true);
        }
        else if(cusKyc.CustomerType.Equals("2") && cusKyc.CompanyRegNo.Equals(""))
        {
            ShowMessage("Company Registration Number is required", true);
        }
        else if (cusKyc.CustomerType.Equals("2") && cusKyc.CompanyTin.Equals(""))
        {
            ShowMessage("Company TIN is required", true);
        }
        else if (!cusKyc.CustomerIdType.Equals("0") && cusKyc.CustomerIdNo.Equals(""))
        {
            ShowMessage("Customer Id Number is required", true);
        }
        else
        {
            //validate Date of birth
            cusKyc.Isactive = chkIsActive.Checked;
            if (!txtDateofBirth.Text.Trim().Equals("") && !bll.IsDateValid(txtDateofBirth.Text.Trim()))
            {
                ShowMessage("Invalid Date of Birth", true);
            }
            else
            {
                if (!txtDateofBirth.Text.Trim().Equals(""))
                {
                    cusKyc.DateofBirth = DateTime.Parse(txtDateofBirth.Text.Trim());
                }
                string ok = bll.SaveCustomerKYC(cusKyc);
                if (ok.ToUpper().Equals("OK"))
                {
                    //Save customer Credetials
                  string status=  bll.SaveCustomerCredentails(cusKyc.Vendorcode);
                  if (status.ToUpper().Equals("OK"))
                    {
                        ShowMessage("Customer Details Saved Successfully",false);
                        ClearControls();
                    }
                    else
                    {
                        ShowMessage("Customer Details Saved Successfully but failed to Log Credentails", false);
                        ClearControls();
                    }
                }
                else
                {
                    ShowMessage("Failed to Save Customer Details",true);
                }
            }
        }
            

    }
    private void ClearControls()
    {
        txtVendorCode.Text="";
        txtFname.Text = "";
        txtLname.Text = "";
        txtOtherName.Text = "";
        txtcontact1.Text = "";
        txtContact2.Text = "";
        rbnGender.SelectedValue.ToString();
        txtNattionality.Text = "";
        txtAddress.Text = "";
        txtEmail.Text = "";
        //cboCustomerType.SelectedValue = cboCustomerType.Items.IndexOf(cboCustomerType.Items.FindByValue("0"));
        //cboBusinessType.SelectedValue = cboBusinessType.Items.IndexOf(cboBusinessType.Items.FindByValue("0"));
        txtTradingName.Text = "";
        txtCompanyReg.Text = "";
        txtTin.Text = "";
        txtRegion.Text = "";
        txtDistrict.Text = "";
        txtCustomerIdNo.Text = "";
        lblVendorCode.Text = "0";
        //cboCustomerIdType.SelectedValue = cboCustomerIdType.Items.IndexOf(cboCustomerIdType.Items.FindByValue("0"));
    }
    

    private void LoadCustomerType()
    {
        try
        {
            dtable=dac.GetPosCustomerTypes();
            cboCustomerType.DataSource = dtable;
            cboCustomerType.DataValueField = "TypeId";
            cboCustomerType.DataTextField = "TypeName";
            cboCustomerType.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void LoadBusinessTypes()
    {
        try
        {
            
            dtable = dac.GetBusinessTypes();
            cboBusinessType.DataSource = dtable;
            cboBusinessType.DataValueField = "TypeId";
            cboBusinessType.DataTextField = "TypeName";
            cboBusinessType.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void LoadIdTypes()
    {
        try
        {
            //dtable = dac.GetIdTypes();
            //cboCustomerType.DataSource = dtable;
            //cboCustomerType.DataValueField = "TypeId";
            //cboCustomerType.DataTextField = "TypeName";
            //cboCustomerType.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void cboCustomerType_DataBound(object sender, EventArgs e)
    {
        cboCustomerType.Items.Insert(0, new ListItem("-- Select Customer Type --", "0"));
    }

    protected void cboBusinessType_DataBound(object sender, EventArgs e)
    {
        cboBusinessType.Items.Insert(0, new ListItem("-- Select Business Type --", "0"));
    }

    protected void cboCustomerIdType_DataBound(object sender, EventArgs e)
    {
        cboCustomerIdType.Items.Insert(0, new ListItem("-- Select Id Type --", "0"));
    }
    protected void cboUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //cboAccountType.Items.Insert(0, new ListItem("-- Select Network --", "0"));
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
}
