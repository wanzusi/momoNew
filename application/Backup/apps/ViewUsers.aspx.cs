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
public partial class ViewUsers : System.Web.UI.Page
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
                    LoadAreas();
                    if (!Session["AreaID"].ToString().Equals("1"))
                    {
                        cboAreas.SelectedIndex = cboAreas.Items.IndexOf(new ListItem(Session["AreaCode"].ToString(), Session["AreaID"].ToString()));
                        int AreaID = Convert.ToInt16(cboAreas.SelectedValue.ToString());
                        LoadBranches(AreaID);
                        cboBranches.SelectedIndex = cboBranches.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                        cboAreas.Enabled = false;
                        cboBranches.Enabled = false;
                    }
                    else
                    {
                        int AreaID = Convert.ToInt16(cboAreas.SelectedValue.ToString());
                        LoadBranches(AreaID);
                        cboAreas.Enabled = true;
                        cboBranches.Enabled = true;
                    }
                    LoadRoles();
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
            LoadUsers();            
         }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadUsers()
    {
        string BranchID = "";
        SystemUser user = new SystemUser();
        user.Name = txtSearch.Text.Trim();
        if (cboBranches.Text == "")
        {
            BranchID = "";
        }
        else
        {
            BranchID = cboBranches.SelectedValue.ToString();
        }
        user.Area = int.Parse(cboAreas.SelectedValue.ToString());
        user.Branch = BranchID;
        user.Role = cboAccessLevel.SelectedValue.ToString();
        dataTable = datafile.GetSystemUsers(user);
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
            ShowMessage("No User(s) Found", true);
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
    private void LoadAreas()
    {
        dataTable = datafile.GetAreas();
        cboAreas.DataSource = dataTable;
        cboAreas.DataValueField = "TypeId";
        cboAreas.DataTextField = "UserType";
        cboAreas.DataBind();
    }
    private void LoadBranches(int AreaID)
    {
        //dataTable = datafile.GetBranches(AreaID);
        dataTable = datafile.GetSystemCompanies("", "");
        cboBranches.DataSource = dataTable;
        cboBranches.DataValueField = "CompanyCode";
        cboBranches.DataTextField = "Company";
        cboBranches.DataBind();
        if (Session["AreaID"].ToString().Equals("1"))
        {
            cboBranches.Enabled = true;
        }
        else
        {
            string CompanyCode = Session["Company"].ToString();
            cboBranches.SelectedIndex = cboBranches.Items.IndexOf(cboBranches.Items.FindByValue(CompanyCode));
            cboBranches.Enabled = false;
        }
        //if (dataTable.Rows.Count > 0)
        //{
        //    if (dataTable.Rows.Count == 1)
        //    {
        //        cboBranches.Enabled = false;
        //        string district = dataTable.Rows[0]["CompanyCode"].ToString();
        //        cboBranches.SelectedIndex = cboBranches.Items.IndexOf(cboBranches.Items.FindByValue(district));
        //    }
        //    else
        //    {
        //        cboBranches.Enabled = true;
        //    }

        //}
        //else
        //{
        //    cboBranches.Enabled = false;
        //}
    }
    private void LoadRoles()
    {
        bool CustomerUser = false;
        dataTable = datafile.GetSystemRoles(CustomerUser);
        cboAccessLevel.DataSource = dataTable;
        cboAccessLevel.DataValueField = "RoleCode";
        cboAccessLevel.DataTextField = "RoleName";
        cboAccessLevel.DataBind();
    }
    protected void cboAreas_DataBound(object sender, EventArgs e)
    {
        cboAreas.Items.Insert(0, new ListItem("      All Company Types     ", "0"));
    }
    protected void cboAreas_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int AreaID = Convert.ToInt16(cboAreas.SelectedValue);
            LoadBranches(AreaID);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }
    protected void cboCostCenter_DataBound(object sender, EventArgs e)
    {
        cboBranches.Items.Insert(0, new ListItem("All Companies ", "0"));
    }
   
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            SystemUser user = new SystemUser();
            if (e.CommandName == "btnEdit")
            {
                string UserID = e.Item.Cells[0].Text;
                //Response.Redirect("./AddUser.aspx?transferid=" + UserID, true);
                Server.Transfer("./AddUser.aspx?transferid=" + UserID, true);
                //ShowMessage(UserID, true);
            }
            else if (e.CommandName == "btnSms")
            {
                string UserID = e.Item.Cells[0].Text;
                user.Userid = int.Parse(UserID);
                user.Uname = e.Item.Cells[5].Text;
                string name = e.Item.Cells[6].Text;
                LoadCreditform(user.Uname, name);

            }
            else if (e.CommandName == "btnChange")
            {
                string UserID = e.Item.Cells[0].Text;
                user.Userid = int.Parse(UserID);
                user.Uname = e.Item.Cells[4].Text;
                user.Status = e.Item.Cells[8].Text;
                string returned = Process.ChangeUserAccess(user);
                LoadUsers();
                ShowMessage(returned,false);
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
        cboAccessLevel.Enabled = false;
        cboAreas.Enabled = false;
        cboBranches.Enabled = false;
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            string BranchID ="";
            SystemUser user = new SystemUser();
            user.Name = txtSearch.Text.Trim();
            if (cboBranches.Text == "")
            {
                BranchID ="";
            }
            else
            {
                BranchID = cboBranches.SelectedValue.ToString();
            }
            user.Area = int.Parse(cboAreas.SelectedValue.ToString());
            user.Branch = BranchID;
            user.Role = cboAccessLevel.SelectedValue.ToString();
            dataTable = datafile.GetSystemUsers(user);           
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
        cboAccessLevel.Items.Insert(0, new ListItem("All Roles", "0"));
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        btnOK.Enabled = true;
        cboAccessLevel.Enabled = true;
        cboAreas.Enabled = true;
        cboBranches.Enabled = true;
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
                cboAccessLevel.Enabled = true;
                cboAreas.Enabled = true;
                cboBranches.Enabled = true;

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
