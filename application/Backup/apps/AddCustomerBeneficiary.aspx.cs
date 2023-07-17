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
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Collections.Generic;
public partial class AddCustomerBeneficiary : System.Web.UI.Page
{
    private DataLogin dac = new DataLogin();
    private BusinessLogin bll = new BusinessLogin();
    DataTable dtable = new DataTable();
    PhoneValidator pv = new PhoneValidator();
    Beneficiary beneficiary = new Beneficiary();
    private DataFile df;
    private ArrayList fileContents;

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
                        GetTypes();
                        if (Request.QueryString["id"] != null)
                        {
                            btnSave.Text = "update";
                            lblCode.Text = Request.QueryString["id"].ToString();
                            LoadControls(lblCode.Text.Trim());
                        }
                        else
                        {
                            LoadIntialControls();
                        }
                    }
                    else
                    {
                        Session.Clear();
                        Response.Redirect("Default.aspx");
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
    private void LoadIntialControls()
    {
        chkActive.Enabled = false;
        txtCode.Enabled = false;
    }
    private void Toggle4Process()
    {
        string strProcessScript = "this.value='Processing...';this.disabled=true;";
        btnSave.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSave, "").ToString());
        Button1.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button1, "").ToString());
        btnYes.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnYes, "").ToString());
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
    private void LoadControls(string Code)
    {
        dtable = dac.GetCustomerBeneficiaryDetails(Code);
        //dtable = bll.Decrpyt(dtable);
        if (dtable.Rows.Count > 0)
        {
            lblCode.Text = dtable.Rows[0]["RecordID"].ToString();
            txtCode.Text = dtable.Rows[0]["CustomerCode"].ToString();
            txtEmail.Text = dtable.Rows[0]["EmailAddress"].ToString();
            string Name = dtable.Rows[0]["Name"].ToString();
            txtFname.Text = Getname(Name, 2);
            txtLname.Text = Getname(Name, 1);
            txtPhone.Text = dtable.Rows[0]["AccountNumber"].ToString();
            txtTitle.Text = dtable.Rows[0]["Title"].ToString();
            string Type = dtable.Rows[0]["TypeCode"].ToString();
            bool IsActive = Convert.ToBoolean(dtable.Rows[0]["Active"].ToString());
            chkActive.Checked = IsActive;
            txtLocation.Text = dtable.Rows[0]["Location"].ToString();
            TelRegName.Text = dtable.Rows[0]["TelRegisteredNumber"].ToString();
            cboType.SelectedIndex = cboType.Items.IndexOf(cboType.Items.FindByValue(dtable.Rows[0]["TypeCode"].ToString()));
            MultiView3.ActiveViewIndex = 0;
            MultiView1.ActiveViewIndex = 0;
            rbnMethod.Enabled = false;
            chkActive.Enabled = false;
        }
        else
        {
            ShowMessage(Code + " Details would not load", true);
        }
    }
    private string Getname(string nameString, int position)
    {
        string output = "";
        string[] array = nameString.Split(' ');
        int arrlength = array.Length;
        if (position == 1)
        {
            output = array[0].ToString();
        }
        else
        {
            if (arrlength == 2)
            {
                output = array[1].ToString();
            }
            else if (arrlength > 2)
            {
                output = array[1].ToString() + " " + array[2].ToString();
            }
        }
        return output;
    }

    private void GetTypes()
    {
        dtable = dac.GetBeneficiaryTypes();
        cboType.DataSource = dtable;
        cboType.DataValueField = "TypeCode";
        cboType.DataTextField = "TypeName";
        cboType.DataBind();
    }
    private void GetTypes2()
    {
        dtable = dac.GetBeneficiaryTypes();
        cboType2.DataSource = dtable;
        cboType2.DataValueField = "TypeCode";
        cboType2.DataTextField = "TypeName";
        cboType2.DataBind();
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
        //session details
        string userId = Session["Username"] as string;
        string fullname = Session["Fullname"] as string;
        string userBranch = Session["CustomerCode"] as string;
        string page = bll.GetCurrentPageName();

        string Code = lblCode.Text.Trim();
        string CustomerCode = txtCode.Text.Trim();
        string Fname = txtFname.Text.Trim();
        string Lname = txtLname.Text.Trim();
        string Phone = pv.FormatTelephone(txtPhone.Text.Trim());
        string Email = txtEmail.Text.Trim();
        string Location = txtLocation.Text.Trim();
        string TypeCode = cboType.SelectedValue.ToString();
        string Title = txtTitle.Text.Trim();
        if (CustomerCode == "")
        {
            ShowMessage("Please Enter Beneficiary Code", true);
            txtCode.Focus();
        }
        else if (Fname == "")
        {
            ShowMessage("Please Enter Beneficiary First Name", true);
            txtFname.Focus();
        }
        else if (Lname == "")
        {
            ShowMessage("Please Enter Beneficiary Last Name", true);
            txtLname.Focus();
        }
        else if (Phone == "")
        {
            ShowMessage("Please Enter Beneficiary Phone", true);
            txtPhone.Focus();
        }
        else if (Location == "")
        {
            ShowMessage("Please Enter Beneficiary Bank Branch", true);
            txtLocation.Focus();
        }
        else if (TypeCode == "0")
        {
            ShowMessage("Please Select Beneficiary Type", true);
        }
        else
        {
            if (pv.PhoneNumbersOk(bll.formatPhone(Phone)))
            {

                beneficiary.RecordCode = Convert.ToInt32(Code);
                beneficiary.Name = Lname + " " + Fname;
                string formatedPhone = bll.formatPhone(Phone);
                beneficiary.Mobile = formatedPhone;
                beneficiary.TypeCode = TypeCode;
                beneficiary.CustomerCode = Session["CustomerCode"].ToString();
                beneficiary.RecordedBy = Session["UserName"].ToString();
                beneficiary.CustomerId = Convert.ToInt32(Session["CustomerId"].ToString());
                beneficiary.Active = chkActive.Checked;
                beneficiary.Email = Email;
                beneficiary.Location = Location.Replace("'", "");
                beneficiary.Title = Title.Replace("'", "");
                beneficiary.NetworkCode = pv.GetNetwork(pv.FormatTelephone(formatedPhone));
                beneficiary.TelRegisteredNumber = TelRegName.Text.Trim();
                if (bll.BeneficiaryExists(beneficiary) && beneficiary.RecordCode == 0)
                {
                    ShowMessage("Beneficiary Contact " + Phone + " already exists in the system for " + beneficiary.CustomerCode, true);
                }
                else
                {
                    if (btnSave.Text.ToUpper().Equals("UPDATE"))
                    {
                        dac.SaveCustomerBeneficiaryforApproval(beneficiary);
                        dac.LogActivity(Session["UserName"].ToString(), "Single Beneficiaries details sent to Authoriser for Approval");
                        ShowMessage("Beneficiary " + Lname + " " + Fname + " Saved Successfully and Details Sent to Authoriser for Approval", false);
                        bll.InsertIntoAuditLog(beneficiary.Mobile, "UPDATE", "CustomerBeneficiaries", userBranch, userId, page,
fullname + " successfully updated the beneficiary " + beneficiary.Name + ", phone [" + beneficiary.Mobile + "] with the type code of [" + TypeCode + "] from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());
          
                        
                        ClearContrl();
                    }
                    else
                    {
                        dac.SaveCustomerBeneficiary(beneficiary);
                        dac.LogActivity(Session["UserName"].ToString(), "Added single Beneficiary Details");
                        ShowMessage("Beneficiary " + Lname + " " + Fname + " Saved Successfully", false);
                        bll.InsertIntoAuditLog(beneficiary.Mobile, "CREATE", "CustomerBeneficiaries", userBranch, userId, page,
fullname + " successfully created the beneficiary " + beneficiary.Name + ", phone [" + beneficiary.Mobile + "] with the type code of [" + TypeCode + "] from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());
          
                        ClearContrl();
                    }
                    
                }
            }
            else
            {
                ShowMessage("Invalid Phone Number", true);
            }

        }
    }
    private void ClearContrl()
    {
        lblCode.Text = "0";
        txtFname.Text = "";
        txtLname.Text = "";
        txtPhone.Text = "";
        txtEmail.Text = "";
        txtTitle.Text = "";
        cboType.SelectedIndex = cboType.Items.IndexOf(cboType.Items.FindByValue("0"));
        txtLocation.Text = "";
        chkActive.Checked = false;
        chkActive.Enabled = false;
    }
    protected void cboType_DataBound(object sender, EventArgs e)
    {
        cboType.Items.Insert(0, new ListItem("-- Select Type --", "0"));
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            CountRecord();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void CountRecord()
    {
        string TypeCode = cboType2.SelectedValue.ToString();
        string CustomerName = Session["CustomerName"].ToString();
        string CustomerCode = Session["CustomerCode"].ToString();
        if (TypeCode == "0")
        {
            ShowMessage("Please Select Type of Beneficiary to Upload", true);
        }
        else if (CustomerCode == "0")
        {
            ShowMessage("Please Select Corporation to attached Beneficiaries to Upload", true);
        }
        else
        {
            ReadFile(TypeCode);
        }
    }

    private void ReadFile(string TypeCode)
    {
        string c = FileUpload1.FileName;
        string file_ext = Path.GetExtension(c);
        string FullPath = ReturnPath();

        DataTable errorTable = new DataTable();
        DataTable benefactors = new DataTable();

        if (file_ext == ".csv" || file_ext == ".txt")
        {
            int count = 0;
            int i = 0;
            df = new DataFile();
            fileContents = df.readFile(FullPath);
            string customer_code = Session["CustomerCode"].ToString();
            errorTable = GetErrorTable();
            benefactors = GetBeneficiariesTable();

            for (i = 1; i < fileContents.Count; i++)
            {
                string error = "";
                string line = fileContents[i].ToString();
                string[] sLine = line.Split(',');
                string[] StrArray = line.Split(Convert.ToChar(","));
                if (sLine.Length == 3 || sLine.Length == 4)
                {
                    //Validate phone number
                    //deo
                    string Contact = pv.FormatTelephone(StrArray[0].ToString().Trim());
                    string Name = StrArray[1].ToString();
                    string Location = StrArray[2].ToString();
                    string Title = "";
                    string TelregisteredName = bll.VerifyPhoneNumberSingleBeneficiarry(Contact);
                    if (sLine.Length == 4)
                    {
                        Title = StrArray[3].ToString();
                    }
                    if (Contact.Equals(""))
                    {
                        error = error + "Contact is Required, ";

                    }
                    if (Name.Equals(""))
                    {
                        error = error + "Name is required, ";

                    }
                    if (Location.Equals(""))
                    {
                        error = error + "Location is Required, ";
                    }
                    if (!pv.PhoneNumbersOk(Contact))
                    {
                        error = error + "Invalid Phone Format";
                    }

                    //if (!bll.VerifyPhoneNumber(Contact))
                    if (string.IsNullOrEmpty(TelregisteredName))
                    {
                        error = error + "This number is not registered with the telecom";
                    }
                    else
                    {
                        beneficiary.Mobile = bll.formatPhone(StrArray[0].ToString().Trim()); ;
                        beneficiary.CustomerCode = customer_code;
                        beneficiary.TypeCode = TypeCode;
                        beneficiary.TelRegisteredNumber = TelregisteredName;
                        if (bll.BeneficiaryExists(beneficiary))
                        {
                            error = error + "Phone Number already Registered Against a beneficiary";
                        }
                        DataRow dben = benefactors.NewRow();
                        dben["Contact"] = HttpUtility.HtmlEncode(beneficiary.Mobile);
                        dben["Name"] = HttpUtility.HtmlEncode(Name);
                        dben["Location"] = HttpUtility.HtmlEncode(Location);
                        dben["Reason"] = HttpUtility.HtmlEncode(beneficiary.TelRegisteredNumber);
                        benefactors.Rows.Add(dben);
                    }

                    if (!error.Equals(""))
                    {
                        DateTime now = DateTime.Now;
                        DataRow drerror = errorTable.NewRow();
                        drerror["Contact"] = HttpUtility.HtmlEncode("0" + Contact);
                        drerror["Name"] = HttpUtility.HtmlEncode(Name);
                        drerror["Location"] = HttpUtility.HtmlEncode(Location);
                        drerror["Reason"] = error.TrimEnd(',');
                        errorTable.Rows.Add(drerror);
                    }
                    else
                    {
                        Beneficiary ben = new Beneficiary();
                        ben.CustomerCode = customer_code;
                        count = i + 1;
                    }

                }
                else
                {
                    throw new Exception("File Format is not OK, Columns must be 3 or 4..");
                }

            }
            lblPath.Text = FullPath;
            if (errorTable.Rows.Count > 0)
            {
                DataGrid1.DataSource = errorTable;
                DataGrid1.DataBind();
                MultiView3.ActiveViewIndex = 1;
                MultiView2.ActiveViewIndex = 0;
                ShowMessage(" The following Beneficiary(ies) details have Errors", true);
            }
            else
            {
                DataGrid2.DataSource = benefactors;
                DataGrid2.DataBind();
                Toggle(count, true);
            }
        }
        else
        {
            RemoveFile(FullPath);
            ShowMessage("File format " + file_ext + " is not supported", true);
        }
    }

    private string ReturnPath()
    {
        string filename = HttpUtility.HtmlEncode(Path.GetFileName(FileUpload1.FileName));
        string extension = HttpUtility.HtmlEncode(Path.GetExtension(filename)); //Path.GetExtension(FileUpload2.FileName);
        DateTime now = DateTime.Now;
        string dt = now.ToString("ddMMyyyy");
        DataTable returnedPath = new DataTable();
        string folder = dac.GetSystemParameter(7, 16).Trim();
        string User = Session["UserName"].ToString().Replace(" ", "-").Replace(".", "");
        filename = User + filename;
        string filepath = folder + dt + filename;
        if (File.Exists(filepath))
        {
            //File.Delete(filepath);
        }
        FileUpload1.SaveAs(filepath);

        return filepath;
    }

    private void Toggle(int count, bool Check)
    {
        MultiView1.ActiveViewIndex = 2;
        lblQn.Text = "Are you sure you want to upload a file of " + count + " record(s) for " + Session["CustomerCode"].ToString();
    }
    private void RemoveFile(string Path)
    {
        if (File.Exists(Path))
        {
            File.Delete(Path);
        }
    }
    private void CheckPath(string Path)
    {
        if (!Directory.Exists(Path))
        {
            Directory.CreateDirectory(Path);
        }
    }

    private DataTable GetBeneficiariesTable()
    {
        DataTable dterror = new DataTable("Beneficiaries");
        dterror.Columns.Add("Contact");
        dterror.Columns.Add("Name");
        dterror.Columns.Add("Location");
        dterror.Columns.Add("Reason");
        return dterror;
    }

    private DataTable GetErrorTable()
    {
        DataTable dterror = new DataTable("ErrorTable");
        dterror.Columns.Add("Contact");
        dterror.Columns.Add("Name");
        dterror.Columns.Add("Location");
        dterror.Columns.Add("Reason");
        return dterror;
    }
    protected void rbnMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbnMethod.SelectedValue.ToString() == "0")
            {
                GetTypes();
                MultiView3.ActiveViewIndex = 0;
                MultiView1.ActiveViewIndex = 0;
                txtCode.Text = Session["CustomerCode"].ToString();

            }
            else
            {
                GetTypes2();
                MultiView3.ActiveViewIndex = 0;
                MultiView1.ActiveViewIndex = 1;
                txtCustCode2.Text = Session["CustomerCode"].ToString();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            //session details
            string userId = Session["Username"] as string;
            string fullname = Session["Fullname"] as string;
            string userBranch = Session["CustomerCode"] as string;
            string page = bll.GetCurrentPageName();

            DataTable failedTable = new DataTable();
            failedTable = GetErrorTable();
            List<Beneficiary> existing = new List<Beneficiary>();

            string FullPath = lblPath.Text.Trim();
            int count = 0;
            int failed = 0;
            string msg = "";
            df = new DataFile();
            fileContents = df.readFile(FullPath);
            string CustomerCode = Session["CustomerCode"].ToString();
            string UserName = Session["UserName"].ToString();
            beneficiary.CustomerId = Convert.ToInt32(Session["CustomerId"].ToString());
            beneficiary.RecordedBy = Session["UserName"].ToString();
            beneficiary.TypeCode = cboType2.SelectedValue.ToString();
            beneficiary.CustomerCode = CustomerCode;

            for (int i = 0; i < fileContents.Count; i++)
            {
                string line = fileContents[i].ToString();
                string[] sLine = line.Split(',');
                string[] StrArray = line.Split(Convert.ToChar(","));
                string formatedphone = bll.formatPhone(StrArray[0].ToString().Trim());
                string Phone = pv.FormatTelephone(formatedphone); ;
                beneficiary.Mobile = formatedphone;
                beneficiary.Name = StrArray[1].ToString();
                beneficiary.Location = StrArray[2].ToString().Replace("'", "");
                beneficiary.RecordCode = 0;
                beneficiary.NetworkCode = pv.GetNetwork(pv.FormatTelephone(beneficiary.Mobile));
                beneficiary.TelRegisteredNumber = bll.VerifyPhoneNumberSingleBeneficiarry(Phone);
                beneficiary.Active = false;
                if (sLine.Length == 4)
                {
                    beneficiary.Title = StrArray[3].ToString().Replace("'", "");
                }
                else
                {
                    beneficiary.Title = "";
                }

                if (bll.BeneficiaryExists(beneficiary))
                {
                    failed++;
                    Beneficiary exists = new Beneficiary();
                    exists.Mobile = beneficiary.Mobile;
                    exists.Location = beneficiary.Location;
                    exists.Name = beneficiary.Name;
                    existing.Add(exists);
                }
                else
                {
                    dac.SaveCustomerBeneficiary(beneficiary);
                    bll.InsertIntoAuditLog(beneficiary.Mobile, "CREATE", "CustomerBeneficiaries", userBranch, userId, page,
fullname + " successfully created the beneficiary" + beneficiary.Name + ", phone [" + beneficiary.Mobile + "] with the type code of [" + beneficiary.TypeCode + "] from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());
          
                    count = i + 1;
                }

            }

            int TotalRecord = count + failed;
            if (failed == 0)
            {
                msg = TotalRecord + " records processed successfully";
                dac.LogActivity(UserName, "Uploaded Beneficiary Details");
                ShowMessage(msg, false);
            }
            else
            {
                if (count != 0)
                {
                    msg = TotalRecord + " records processed, " + count + " were successful and " + failed + " failed";
                    ShowMessage(msg, true);
                }
                else
                {
                    msg = TotalRecord + " records process failed";
                    ShowMessage(msg, true);
                }

                foreach (Beneficiary benExists in existing)
                {
                    DataRow dfailed = failedTable.NewRow();
                    dfailed["Contact"] = benExists.Mobile;
                    dfailed["Name"] = benExists.Name;
                    dfailed["Location"] = benExists.Location;
                    dfailed["Reason"] = "This number is already registered in the system";
                    failedTable.Rows.Add(dfailed);
                }
            }
            if (failedTable.Rows.Count > 0)
            {
                DataGrid1.DataSource = failedTable;
                DataGrid1.DataBind();
                MultiView1.ActiveViewIndex = 1;
                MultiView3.ActiveViewIndex = 1;
                MultiView2.ActiveViewIndex = 0;
            }
            else
            {
                //do nothing
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        string FullPath = lblPath.Text.Trim();
        RemoveFile(FullPath);
        MultiView1.ActiveViewIndex = 1;

    }
    protected void cboType2_DataBound(object sender, EventArgs e)
    {
        cboType2.Items.Insert(0, new ListItem("-- Select Type --", "0"));
    }
    protected void btnPrintFile_Click(object sender, EventArgs e)
    {

    }
    protected void btnConvert_Click(object sender, EventArgs e)
    {

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView3.ActiveViewIndex = 0;
            MultiView1.ActiveViewIndex = 1;
            ShowMessage(" ", true);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    protected void Button2_Click1(object sender, EventArgs e)
    {
        string Phone = pv.FormatTelephone(txtPhone.Text.Trim()); 
        string TelregisteredName = bll.VerifyPhoneNumberSingleBeneficiarry(Phone);
        if (!string.IsNullOrEmpty(TelregisteredName))
        {
            TelRegName.Text = TelregisteredName;
            btnSave.Visible = true;
        }
        else
        {
            ShowMessage("Number not registered to any customer", true);
        }
    }
}
