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

public partial class AddVendor : System.Web.UI.Page
{
    //SystemUsers dac = new SystemUsers();
    ProcessUsers Process = new ProcessUsers();
    DataLogin datafile = new DataLogin();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();

    Vendor vendor = new Vendor();

    Merchant merchant = new Merchant();
    private HttpFileCollection uploads2 = HttpContext.Current.Request.Files;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string RoleId = Session["RoleCode"].ToString();
            if (isRoleAuthorisedToVisitPage(RoleId))
            {
                if (Session["AreaID"].ToString().Equals("1"))
                {
                    if (IsPostBack == false)
                    {
                        LoadChargeTypes();

                        MultiView1.ActiveViewIndex = 0;
                        txtUser.Text = Session["FullName"].ToString();
                        if (Request.QueryString["id"] != null)
                        {
                            string vendorCode = Request.QueryString["id"].ToString();
                            LoadControls(vendorCode);
                        }
                        else
                        {
                            MultiView2.ActiveViewIndex = -1;
                        }
                        string strProcessScript = "this.value='Working...';this.disabled=true;";
                        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
                    }
                }
                else
                {
                    MultiView1.ActiveViewIndex = -1;
                    ShowMessage("YOU DO NOT HAVE RIGHTS TO VIEW THIS PAGE", true);

                }
            }
            else
            {
                Response.Redirect("UnauthorisedAccess.aspx");
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
    private void LoadControls(string VendorCode)
    {
        vendor.Vendorid = int.Parse(VendorCode);
        dataTable = datafile.GetVendorById(vendor);
        if (dataTable.Rows.Count > 0)
        {
            lblCode.Text = dataTable.Rows[0]["Vendorid"].ToString();
            txtCode.Text = dataTable.Rows[0]["VendorCode"].ToString();
            txtName.Text = dataTable.Rows[0]["Vendor"].ToString();
            txtBillSystemCode.Text = dataTable.Rows[0]["BillSystemCode"].ToString();
            txtcontact.Text = dataTable.Rows[0]["ContactPerson"].ToString();
            txtemail.Text = dataTable.Rows[0]["VendorEmail"].ToString();
            txtconfirmemail.Text = dataTable.Rows[0]["VendorEmail"].ToString();
            txtAccountManager.Text = dataTable.Rows[0]["AccountManager"].ToString();
            txtAccountRep.Text = dataTable.Rows[0]["AccountRep"].ToString(); 
            bool isActive = bool.Parse(dataTable.Rows[0]["Active"].ToString());
            bool isMActive = bool.Parse(dataTable.Rows[0]["MActive"].ToString());

            DataTable chargeTable = datafile.ExecuteDataSet("GenerateChargesReport", txtCode.Text, "", "", "", "").Tables[0];
            cboChargeType.SelectedValue = chargeTable.Rows[0]["TypeId"].ToString();
            txtPegPayCharge.Text = chargeTable.Rows[0]["PegasusCharge"].ToString();
            txtCode.Enabled = false;
            MultiView2.ActiveViewIndex = 0;

        }
    }

    private void LoadChargeTypes()
    {
        try
        {
            dataTable = datafile.GetChargeTypes();
            cboChargeType.DataSource = dataTable;
            cboChargeType.DataValueField = "TypeId";
            cboChargeType.DataTextField = "ChargeName";
            cboChargeType.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
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
        double amountt = 0;
        Double.TryParse(minBalance.Text.Trim(), out amountt);
        vendor.MinBal = amountt;
        vendor.Vendorid = int.Parse(lblCode.Text.Trim());
        vendor.VendorCode = txtCode.Text.Trim();
        vendor.BillSysCode = txtBillSystemCode.Text.Trim();
        vendor.VendorName = txtName.Text.Trim().ToUpper();      
        vendor.Email = txtemail.Text.Trim();      
        vendor.Active = false;
        vendor.Sendemail = chkResend.Checked;
        vendor.Reset = chkResetPassword.Checked;
        vendor.Contract = txtcontact.Text.Trim();
        vendor.ChargeType = int.Parse(cboChargeType.SelectedValue.ToString());
        vendor.AccountManager = txtAccountManager.Text.Trim();
        vendor.AccountRep = txtAccountRep.Text;
        vendor.IsRequiredCert = chkcert.Checked;
        double res=0;
        //////

        //merchant.Active = true;
        //merchant.ClientId = "";
        //merchant.TerminalId = "";
        //merchant.OperatorId = "";
        //merchant.Password = "";
        if (vendor.VendorCode.Equals(""))
        {
            ShowMessage("Please Enter Vendor Code", true);
            txtCode.Focus();
        }
        else if (vendor.BillSysCode.Equals(""))
        {
            ShowMessage("Please Enter Vendor Billing System Code", true);
            txtBillSystemCode.Focus();
        }
        else if (vendor.VendorName.Equals(""))
        {
            ShowMessage("Please Enter Vendor Name", true);
            txtName.Focus();
        }
        else if (vendor.Email.Equals(""))
        {
            ShowMessage("Please Enter Vendor Email", true);
            txtemail.Focus();
        }
        else if (txtconfirmemail.Text.Equals(""))
        {
            ShowMessage("Please Confirm Email", true);
            txtconfirmemail.Focus();
        }
        else if (txtAccountManager.Text.Equals(""))
        {
            ShowMessage("Please Assign the Relationship Manager", true);
            txtAccountManager.Focus();
        }
        else if (txtAccountRep.Text.Equals(""))
        {
            ShowMessage("Please Assign the Relationship Representative", true);
            txtAccountManager.Focus();
        }
        else if(!vendor.Email.Equals(txtconfirmemail.Text.Trim()))
        {
            ShowMessage("Please Emails Provided do not match", true);            
        }
        else if (!bll.IsValidEmailAddress(vendor.Email))
        {
            ShowMessage("Please Provide valid Emails ", true);
            txtemail.Focus();
        }
        else if (cboChargeType.SelectedValue.Equals("0"))
        {
            ShowMessage("Select Pegasus Charge type", true);
            txtconfirmemail.Focus();
        }
        else if (txtPegPayCharge.Text.Equals(""))
        {
            ShowMessage("Please Pegasus Charge is required", true);
            txtconfirmemail.Focus();
        }
        else if (!Double.TryParse(txtPegPayCharge.Text.Trim(), out res))
        {
            ShowMessage("Invalid Pegasus Charge Entered", true);
            txtconfirmemail.Focus();
        }
        else if (vendor.IsRequiredCert && !FileUpload1.HasFile)
        {
            ShowMessage("Certificate is Required", true);
        }
        else
        {
            vendor.PegpayCharge = Convert.ToDouble(txtPegPayCharge.Text.Trim());
            string ret = Process.SaveVendor(vendor, merchant);
            if (vendor.IsRequiredCert)
            {
                UploadCert(vendor.VendorCode);
            }
            ShowMessage(ret, false);
            ClearControls();
        }
        
   }
    private void UploadCert(string VendorCode)
    {
        HttpFileCollection uploads;
        uploads = HttpContext.Current.Request.Files;
        int countfiles = 0;
        for (int i = 0; i <= (uploads.Count - 1); i++)
        {
            if (uploads[i].ContentLength > 0)
            {
                string c = Path.GetFileName(uploads[i].FileName);
                string cNoSpace = c.Replace(" ", "-");
                string c1 = cNoSpace;
                string strPath = bll.GetFileApplicationPath(VendorCode);
                DirectoryInfo dic = new DirectoryInfo(strPath);
                bll.EmptyFolder(dic);
                string fullPath = strPath + "\\" + c1;
                FileUpload1.PostedFile.SaveAs(fullPath);                
            }
        }
    }

    private void ClearControls()
    {
        lblCode.Text = "0";
        txtName.Text = "";
        txtemail.Text = "";
        txtCode.Text = "";
        txtBillSystemCode.Text = "";
        txtconfirmemail.Text = "";
        txtcontact.Text = "";
        txtAccountManager.Text = "";
        txtAccountRep.Text = "";
        //txtVPassword.Text = "";
        //txtTerminalId.Text = "";
        //txtOperatorId.Text = "";
        //txtClientId.Text = "";
        //chkIsActive.Checked = false;
        chkResend.Checked = false;
        chkResetPassword.Checked = false;
        //chkPrepayment.Checked = false;
        MultiView2.ActiveViewIndex = -1;
        //MultiView3.ActiveViewIndex = -1;
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
    protected void cboChargeType_DataBound(object sender, EventArgs e)
    {
        cboChargeType.Items.Insert(0, new ListItem(" -----Select Charge Type----- ", "0"));
    }
    protected void chkResend_CheckedChanged(object sender, EventArgs e)
    {
        if (chkResend.Checked.Equals(true))
        {
            chkResetPassword.Checked = false;
        }
    }
    protected void chkResetPassword_CheckedChanged(object sender, EventArgs e)
    {
        if (chkResetPassword.Checked.Equals(true))
        {
            chkResend.Checked = false;
        }
    }
    //protected void chkPrepayment_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        bool isPayment = chkPrepayment.Checked;
    //        if (isPayment)
    //        {
    //            MultiView3.ActiveViewIndex = 0;
    //        }
    //        else
    //        {
    //            MultiView3.ActiveViewIndex = -1;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, true);
    //    }
    //}
}