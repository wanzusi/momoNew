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
public partial class AddCorporateBeneficiary : System.Web.UI.Page
{
    private DataLogin dac = new DataLogin();
    private BusinessLogin bll = new BusinessLogin();
    DataTable dtable = new DataTable();
    PhoneValidator pv = new PhoneValidator();
    Beneficiary beneficiary = new Beneficiary();
    private DataFile df;
    private ArrayList fileContents;

    //session details
    string userId = "";
    string fullname = "";
    string userBranch = "";
    string page = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //session details
            userId = Session["Username"] as string;
            fullname = Session["Fullname"] as string;
            userBranch = Session["UserBranch"] as string;
            page = bll.GetCurrentPageName();

            if (IsPostBack == false)
            {
                  string RoleId = Session["RoleCode"].ToString();
                  if (isRoleAuthorisedToVisitPage(RoleId))
                  {
                      Toggle4Process();

                      LoadData();
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

    private void LoadData()
    {
        try
        {
             if (Request.QueryString["customerId"].ToString() != null)
             {
                 string customerId = Request.QueryString["customerId"].ToString();
                 LoadCustomerData(customerId);
                 MultiView3.ActiveViewIndex = 0;
                 MultiView1.ActiveViewIndex = 0;
             }
        }
        catch (Exception ex)
        {
            
        }
    }

    private void LoadCustomerData(string customerId)
    {
        DataTable dt = bll.ExecuteDataAccess("LiveMerchantDB", "GetCustomer", Int32.Parse(customerId)).Tables[0];
        lblCode.Text = dt.Rows[0]["RecordId"].ToString();
        txtCode.Text = dt.Rows[0]["CustomerId"].ToString();
        txtFname.Text = dt.Rows[0]["FullName"].ToString();
        txtType.Text = dt.Rows[0]["CustomerType"].ToString();
        chkActive.Checked = bool.Parse(dt.Rows[0]["Active"].ToString());
        Session["Edit"] = "1";
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
        string Code = lblCode.Text.Trim();
        string customerId = txtCode.Text.Trim();
        string Fname = txtFname.Text.Trim();
        string type = txtType.Text;
        bool active = chkActive.Checked;

        if (customerId == "")
        {
            ShowMessage("Please provide customer Id", true);
            txtCode.Focus();
        }
        else if (Fname == "")
        {
            ShowMessage("Please provide the customer name", true);
            txtFname.Focus();
        }
        else if (type == "")
        {
            ShowMessage("Please provide the customer type", true);
            txtType.Focus();
        }
        else
        {

            string edit = Session["Edit"] as string;
            if (edit == "1")
            {
                Int32 Id = Int32.Parse(lblCode.Text);
                InterLinkClass.DbApi.Result res = bll.ExecuteDataQuery("LiveMerchantDB", "UpdateCustomer", Id,customerId, Fname, type, active);
                ShowMessage("Customer " + Fname + " updated successfully", false);
                bll.InsertIntoAuditLog(customerId, "UPDATE", "BillerCustomers", userBranch, userId, page,
    fullname + " successfully updated the customer " + Fname + ", customer id [" + customerId + "]  from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());
                
            }
            else
            {
                InterLinkClass.DbApi.Result res = bll.ExecuteDataQuery("LiveMerchantDB", "Customers_Update", customerId, Fname, type, userBranch, active);
                ShowMessage("Customer " + Fname + " saved successfully", false);
                bll.InsertIntoAuditLog(customerId, "CREATE", "BillerCustomers", userBranch, userId, page,
    fullname + " successfully created the customer " + Fname + ", customer id [" + customerId + "]  from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());

            }
            ClearContrl();
        }
    }
    private void ClearContrl()
    {
        lblCode.Text = "0";
        txtCode.Text = "";
        txtFname.Text = "";
        txtType.Text = "";
        chkActive.Checked = false;
        Session["Edit"] = "0";
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
        ReadFile();
    }

    private void ReadFile()
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

            errorTable = GetErrorTable();
            benefactors = GetBeneficiariesTable();

            for (i = 1; i < fileContents.Count; i++)
            {
                string error = "";
                string line = fileContents[i].ToString();
                string[] sLine = line.Split(',');
                string[] StrArray = line.Split(Convert.ToChar(","));
                if (sLine.Length == 2 || sLine.Length == 3)
                {
                    string CustomerId = StrArray[0].ToString().Trim();
                    string FirstName = StrArray[1].ToString();
                    string custType = "";

                    if (sLine.Length == 3)
                    {
                        custType = StrArray[3].ToString();
                    }
                    if (CustomerId.Equals(""))
                    {
                        error = error + "CustomerId is Required, ";

                    }
                    if (FirstName.Equals(""))
                    {
                        error = error + "Name is required, ";

                    }


                    beneficiary.Mobile = bll.formatPhone(StrArray[0].ToString().Trim()); ;
                    beneficiary.CustomerCode = userBranch;
                    beneficiary.TypeCode = custType;

                    DataRow dben = benefactors.NewRow();
                    dben["CustomerCode"] = HttpUtility.HtmlEncode(beneficiary.Mobile);
                    dben["Name"] = HttpUtility.HtmlEncode(FirstName);
                    dben["TypeCode"] = HttpUtility.HtmlEncode(custType);
                    dben["Reason"] = HttpUtility.HtmlEncode(count.ToString());
                    benefactors.Rows.Add(dben);

                    if (!error.Equals(""))
                    {
                        DateTime now = DateTime.Now;
                        DataRow drerror = errorTable.NewRow();
                        drerror["CustomerCode"] = HttpUtility.HtmlEncode(CustomerId);
                        drerror["Name"] = HttpUtility.HtmlEncode(FirstName);
                        drerror["TypeCode"] = HttpUtility.HtmlEncode(count.ToString());
                        drerror["Reason"] = error.TrimEnd(',');
                        errorTable.Rows.Add(drerror);
                    }
                    else
                    {
                        Beneficiary ben = new Beneficiary();
                        ben.CustomerCode = userBranch;
                        count = i + 1;
                    }

                }
                else
                {
                    DataRow drerror = errorTable.NewRow();
                    drerror["CustomerCode"] = HttpUtility.HtmlEncode(sLine[0]);
                    drerror["Name"] = HttpUtility.HtmlEncode(sLine[1]);
                    drerror["TypeCode"] = HttpUtility.HtmlEncode(count.ToString());
                    drerror["Reason"] = "Columns must be 2 or 3 but found " + sLine.Length;
                    errorTable.Rows.Add(drerror);
                    //throw new Exception("File Format is not OK, Columns must be 2 or 3..");
                }

            }
            lblPath.Text = FullPath;
            if (errorTable.Rows.Count > 0)
            {
                DataGrid1.DataSource = errorTable;
                DataGrid1.DataBind();
                MultiView3.ActiveViewIndex = 1;
                MultiView2.ActiveViewIndex = 0;
                ShowMessage(" The following customer(s) details have Errors", true);
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
        string folder = @"E:\Logs\BillerCustomers\";
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
        dterror.Columns.Add("CustomerCode");
        dterror.Columns.Add("Name");
        dterror.Columns.Add("TypeCode");
        dterror.Columns.Add("Reason");
        return dterror;
    }

    private DataTable GetErrorTable()
    {
        DataTable dterror = new DataTable("ErrorTable");
        dterror.Columns.Add("CustomerCode");
        dterror.Columns.Add("Name");
        dterror.Columns.Add("TypeCode");
        dterror.Columns.Add("Reason");
        return dterror;
    }

    protected void rbnMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbnMethod.SelectedValue.ToString() == "0")
            {
                MultiView3.ActiveViewIndex = 0;
                MultiView1.ActiveViewIndex = 0;
            }
            else
            {
                MultiView3.ActiveViewIndex = 0;
                MultiView1.ActiveViewIndex = 1;
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
            string userBranch = Session["UserBranch"] as string;
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

            for (int i = 0; i < fileContents.Count; i++)
            {
                string line = fileContents[i].ToString();
                string[] sLine = line.Split(',');
                string[] StrArray = line.Split(Convert.ToChar(","));

                beneficiary.BatchCode = userBranch;
                beneficiary.CustomerCode = StrArray[0].ToString().Trim();
                beneficiary.Name = StrArray[1].ToString();

                if (sLine.Length == 3)
                {
                    beneficiary.TypeCode = StrArray[2].ToString().Trim();
                }

                InterLinkClass.DbApi.Result res = bll.ExecuteDataQuery("LiveMerchantDB", "Customers_Update", beneficiary.CustomerCode, beneficiary.Name, beneficiary.TypeCode, userBranch, true);
                ShowMessage("Customer " + beneficiary.Name + " saved successfully", false);
                bll.InsertIntoAuditLog(beneficiary.CustomerCode, "CREATE", "BillerCustomers", userBranch, userId, page,
    fullname + " successfully created the customer " + beneficiary.Name + ", customer id [" + beneficiary.CustomerCode + "]  from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());

                count = i + 1;

            }

            int TotalRecord = count + failed;
            if (failed > 0)
            {
                foreach (Beneficiary benExists in existing)
                {
                    DataRow dfailed = failedTable.NewRow();
                    dfailed["CustomerCode"] = benExists.CustomerCode;
                    dfailed["Name"] = benExists.Name;
                    dfailed["TypeCode"] = benExists.TypeCode;
                    dfailed["Reason"] = "This number is already registered in the system";
                    failedTable.Rows.Add(dfailed);
                }

                DataGrid1.DataSource = failedTable;
                DataGrid1.DataBind();
                MultiView1.ActiveViewIndex = 1;
                MultiView3.ActiveViewIndex = 1;
                MultiView2.ActiveViewIndex = 0;
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
}
