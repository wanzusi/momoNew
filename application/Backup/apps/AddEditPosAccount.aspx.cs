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
                //Load_page_Initials();

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
        DataTable dt = new DataTable();
        try
        {

            dtable = dac.GetCustomerKycById(RecordId);

            if (dtable.Rows.Count > 0)
            {
                MultiView1.ActiveViewIndex = 0;
                lblVendorCode.Text = dtable.Rows[0]["VendorCode"].ToString();
                txtVendorCode.Text = dtable.Rows[0]["VendorCode"].ToString();
                txtFname.Text = dtable.Rows[0]["TradingName"].ToString();
                txtLname.Text = dtable.Rows[0]["Address"].ToString();

                dt = dac.getPosAccountIfExist(txtVendorCode.Text.Trim());

                if (dt.Rows.Count > 0)
                {
                    txtSpid.Text = dt.Rows[0]["spid"].ToString();
                    txtUsername.Text = dt.Rows[0]["SenderId"].ToString();
                    txtPassword.Text = dt.Rows[0]["Password"].ToString();
                }
                txtFname.Enabled = false;
                txtLname.Enabled = false;
            }
            else
            {
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("Failed to retive POS ACCOUNT", true);
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
        CustomerKYC cusKyc = new CustomerKYC();
        cusKyc.Vendorcode = txtVendorCode.Text.Trim();
        cusKyc.Fname = txtFname.Text.Trim();
        cusKyc.Lname = txtLname.Text.Trim();
        cusKyc.Username = txtUsername.Text.Trim();
        cusKyc.Password = txtPassword.Text.Trim();
        cusKyc.Spid = txtSpid.Text.Trim();

        if (cusKyc.Vendorcode.Equals(""))
        {
            ShowMessage("Vendor Code not Set", true);
        }

        else if (cusKyc.Username.Equals(""))
        {
            ShowMessage("User name is required", true);
        }
        else if (cusKyc.Password.Equals(""))
        {
            ShowMessage("Password is required", true);
        }
        else if (cusKyc.Spid.Equals(""))
        {
            ShowMessage("SPID is required", true);
        }
        else
        {
            string ok = bll.SavePOSAccountDetails(cusKyc);

            if (ok.ToUpper().Equals("OK"))
            {
                ShowMessage("POS Account Saved Successfully", false);
                ClearControls();
            }
            else
            {
                ShowMessage("POS Account failed to Log ", false);
                ClearControls();
            }
            // }
        }

    }
    private void ClearControls()
    {
        txtVendorCode.Text = "";
        txtFname.Text = "";
        txtLname.Text = "";
        txtPassword.Text = "";
        txtSpid.Text = "";
        txtUsername.Text = "";
    }

    protected void cboCustomerType_DataBound(object sender, EventArgs e)
    {//
        //.Items.Insert(0, new ListItem("-- Select Customer Type --", "0"));
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
