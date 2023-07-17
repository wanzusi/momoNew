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
public partial class ViewCustomers : System.Web.UI.Page
{
    ProcessUsers Process = new ProcessUsers();
    DataLogin datafile = new DataLogin();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();  
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
                    //LoadUsers();
                    DisableBtnsOnClick();
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
    private void DisableBtnsOnClick()
    { 
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            LoadCustomers();            
         }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadCustomers()
    {
        PegPayCustomer cust = new PegPayCustomer();
        cust.Fullname = txtSearch.Text;
        cust.PegpayAccountNumber = txtpegpayAccount.Text;
        cust.MoMoAccountNumber = txtMomoAccount.Text;
        cust.CustomerType = cboCustomerType.SelectedValue.ToString();
        dataTable = datafile.GetSystemCustomers(cust);
        if (dataTable.Rows.Count > 0)
        {
            ShowMessage(".", true);
            MultiView1.ActiveViewIndex = 0;
            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        else
        {
            ShowMessage("No Customer(s) Found", true);
            MultiView1.ActiveViewIndex = -1;
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
    private void LoadCustomerType()
    {
        try
        {
            dataTable = datafile.GetCustomerTypes();
            if (dataTable.Rows.Count > 0)
            {
                cboCustomerType.DataSource = dataTable;
                cboCustomerType.DataValueField = "CustomerTypeCode";
                cboCustomerType.DataTextField = "CustomerTypeCode";
                cboCustomerType.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void cboCustomerType_DataBound(object sender, EventArgs e)
    {
        cboCustomerType.Items.Insert(0, new ListItem("      All Customer Types     ", "ALL"));
    }
   
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "btnEdit")
            {
                string CustomerId = e.Item.Cells[0].Text;
                Server.Transfer("./RegisterCustomer.aspx?transferid=" + CustomerId);
            }


        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }

    private void LoadCreditform(string username,string name)
    {
        ShowMessage(".", true);
        MultiView1.ActiveViewIndex = 1;
        lblUserName.Text = username;
        string header = "ADDING CREDIT TO "+name;
        lblHeader.Text = header;
        btnOK.Enabled = false;
        cboCustomerType.Enabled = false;
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            PegPayCustomer cust = new PegPayCustomer();
            cust.Fullname = txtSearch.Text;
            cust.PegpayAccountNumber = txtpegpayAccount.Text;
            cust.MoMoAccountNumber = txtMomoAccount.Text;
            cust.CustomerType = cboCustomerType.SelectedValue.ToString();
            dataTable = datafile.GetSystemCustomers(cust);        
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
    protected void cboAccessLevel_DataBound(object sender, EventArgs e)
    {
        cboCustomerType.Items.Insert(0, new ListItem("All Roles", "0"));
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        btnOK.Enabled = true;
        cboCustomerType.Enabled = true;
        ShowMessage(".", true);
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string credit = txtCredit.Text.Trim();
            string username = lblUserName.Text.Trim();
            string res = Process.SaveSMSCredit(credit, username);
            if (res.Contains("Successfully"))
            {
                ShowMessage(res, false);
                txtCredit.Text = "";
                lblUserName.Text = "0";
                MultiView1.ActiveViewIndex = 0;
                btnOK.Enabled = true;
                cboCustomerType.Enabled = true;

            }
            else
            {
                ShowMessage(res, true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
}
