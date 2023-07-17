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
using InterLinkClass.EntityObjects;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
public partial class ViewAllCustomerBeneficiaries : System.Web.UI.Page
{
    ProcessUsers Process = new ProcessUsers();
    DataLogin datafile = new DataLogin();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    Beneficiary beneficiary = new Beneficiary();
    private ReportDocument Rptdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string RoleId = Session["RoleCode"].ToString();
            if (isRoleAuthorisedToVisitPage(RoleId))
            {
                if (IsPostBack == false)
                {
                    LoadBeneficaryType();
                    LoadBeneficaryType2();
                    DisableBtnsOnClick();
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
    private void DisableBtnsOnClick()
    { 
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            LoadBeneficaries();            
         }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadBeneficaries()
    {
        string name = txtSearch.Text.Trim();
        string phone = txtSearchPhone.Text.Trim();
        string benType = cboBeneficiaryType.SelectedValue.ToString();
        string CustomerCode = Session["CustomerCode"].ToString();
        string Location = "";
        dataTable = datafile.GetCustomerBeneficaries(name, phone, benType, CustomerCode, Location);
        if (dataTable.Rows.Count > 0)
        {
            ShowMessage(".", true);
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = 0;
            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        else
        {
            ShowMessage("No User(s) Found", true);
            MultiView1.ActiveViewIndex = -1;
            MultiView2.ActiveViewIndex = -1;
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
    private void LoadBeneficaryType()
    {
        dataTable = datafile.GetBeneficaryType();
        cboBeneficiaryType.DataSource = dataTable;
        cboBeneficiaryType.DataValueField = "TypeCode";
        cboBeneficiaryType.DataTextField = "TypeName";
        cboBeneficiaryType.DataBind();

    }
    private void LoadBeneficaryType2()
    {
        dataTable = datafile.GetBeneficaryType();
        cboType.DataSource = dataTable;
        cboType.DataValueField = "TypeCode";
        cboType.DataTextField = "TypeName";
        cboType.DataBind();
    }
    private void ClearContrl()
    {
        lblCode.Text = "0";
        txtCode.Text = "";
        txtFname.Text = "";
        txtlname.Text = "";
        txtPhone.Text = "";
        txtLocation.Text = "";
        lblCustomerCode.Text = "0";
    }
    private void ValidateDetails()
    {
        string Code = lblCode.Text.Trim();
        string BCode = txtCode.Text.Trim();
        string Fname = txtFname.Text.Trim();
        string Lname = txtlname.Text.Trim();
        string Phone = txtPhone.Text.Trim();
        string Email = txtEmail.Text.Trim();
        string Location = txtLocation.Text.Trim();
        string TypeCode = cboType.SelectedValue.ToString();
        if (lblCustomerCode.Text == "0")
        {
            ShowMessage("Please Enter Beneficiary Corporation", true);
        }
        else if (BCode == "")
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
            txtlname.Focus();
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
            beneficiary.RecordCode = Convert.ToInt32(Code);
            beneficiary.Name = Lname.ToUpper() + " " + Fname.ToUpper();
            beneficiary.Code = BCode.ToUpper();
            beneficiary.TypeCode = TypeCode;
            beneficiary.CustomerId = Convert.ToInt32(lblCustomerCode.Text);
            beneficiary.RecordedBy = Session["username"].ToString();
            beneficiary.Active = chkActive.Checked;
            beneficiary.Email = Email;
            if (bll.BeneficiaryExists(beneficiary) && beneficiary.RecordCode == 0)
            {
                ShowMessage("Beneficiary Code " + BCode + " already exists in the system", true);
            }
            else
            {
                datafile.SaveCustomerBeneficiary(beneficiary);
                ShowMessage("Beneficiary " + Lname + " " + Fname + " saved Successfully", false);
                ClearContrl();

            }

        }
    }
    protected void cboBeneficiaryType_DataBound(object sender, EventArgs e)
    {
        cboBeneficiaryType.Items.Insert(0, new ListItem("All Beneficary Types ", "0"));
    }

    protected void cboType_DataBound(object sender, EventArgs e)
    {
        cboType.Items.Insert(0, new ListItem("-- Select Type --", "0"));
    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "btnEdit")
            {
                string RecordId = e.Item.Cells[0].Text;
                Server.Transfer("AddCustomerBeneficiary.aspx?transfereid=" + RecordId);
                //AssginControls(RecordId);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }

    private void AssginControls(string RecordId)
    {
        try
        {
            dataTable = datafile.GetCustomerBeneficiaryDetails(RecordId);
            if (dataTable.Rows.Count == 1)
            {
                txtCode.Text=dataTable.Rows[0]["CustomerCode"].ToString();
                txtEmail.Text = dataTable.Rows[0]["EmailAddress"].ToString();
                txtFname.Text = dataTable.Rows[0]["Name"].ToString();
                txtLocation.Text = dataTable.Rows[0]["Location"].ToString();
                txtPhone.Text = dataTable.Rows[0]["Phone"].ToString();
                chkActive.Checked = bool.Parse(dataTable.Rows[0]["Active"].ToString());
                //cboType.SelectedIndex =cboType.SelectedItem. dataTable.Rows[0]["TypeCode"].ToString();
                lblCode.Text = dataTable.Rows[0]["RecordID"].ToString();
                lblCustomerCode.Text = dataTable.Rows[0]["CustomerCode"].ToString();
                MultiView1.ActiveViewIndex = 1;
                
            }
            else
            {
                ShowMessage("Falied to Load Beneficiary Details",true);
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {

            string name = txtSearch.Text.Trim();
            string phone = txtSearchPhone.Text.Trim();
            string benType = cboBeneficiaryType.SelectedValue.ToString();
            string CustomerCode = Session["CustomerCode"].ToString();
            string Location = "";
            dataTable = datafile.GetCustomerBeneficaries(name, phone, benType, CustomerCode, Location);          
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
    
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        btnOK.Enabled = true;
        cboBeneficiaryType.Enabled = true;
        ShowMessage(".", true);
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


    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            ConvertToFile();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void ConvertToFile()
    {
        if (rdExcel.Checked.Equals(false) && rdPdf.Checked.Equals(false))
        {
            ShowMessage("Please Check file format to Convert to", true);
        }
        else
        {
            LoadRpt();
            if (rdPdf.Checked.Equals(true))
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "TRANSACTIONS");

            }
            else
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "TRANSACTIONS");

            }
        }
    }

    private void LoadRpt()
    {
        string name = txtSearch.Text.Trim();
        string phone = txtSearchPhone.Text.Trim();
        string benType = cboBeneficiaryType.SelectedValue.ToString();
        string CustomerCode = Session["CustomerCode"].ToString();
        string Location = "";
        dataTable = datafile.GetCustomerBeneficaries(name, phone, benType, CustomerCode, Location);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);

        rptName = physicalPath + "\\Bin\\Reports\\CustomerBeneficiaries.rpt";

        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dataTable);
        CrystalReportViewer1.ReportSource = Rptdoc;
    }
    protected void txtCode_TextChanged(object sender, EventArgs e)
    {

    }
}
