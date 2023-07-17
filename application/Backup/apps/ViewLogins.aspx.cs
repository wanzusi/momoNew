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
public partial class ViewLogins : System.Web.UI.Page
{
    ProcessUsers Process = new ProcessUsers();
    DataLogin datafile = new DataLogin();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();

    SystemUser user = new SystemUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                string RoleId = Session["RoleCode"].ToString();
                if (isRoleAuthorisedToVisitPage(RoleId))
                {
                    LoadRoles();
                    DateTime now = DateTime.Now;
                    txtfromDate.Text = now.ToString("dd MMMM yyyy");
                    txttoDate.Text = now.ToString("dd MMMM yyyy");
                    if ((Session["RoleCode"].ToString().Equals("001") && Session["AreaID"].ToString().Equals("1")) || Session["RoleCode"].ToString().Equals("016"))
                    {
                        txtSearch.Text = "";
                        txtSearch.Enabled = true;
                        cboAccessLevel.Enabled = true;
                    }
                    else
                    {
                        txtSearch.Text = Session["FullName"].ToString();
                        txtSearch.Enabled = false;
                        string role = Session["RoleCode"].ToString();
                        cboAccessLevel.SelectedIndex = cboAccessLevel.Items.IndexOf(cboAccessLevel.Items.FindByValue(role));
                        cboAccessLevel.Enabled = false;
                    }
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
            LoadLogs();            
         }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadLogs()
    {      
        
        user.Name = txtSearch.Text.Trim();
        user.Role = cboAccessLevel.SelectedValue.ToString();
        user.FromDate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        user.ToDate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        dataTable = datafile.GetLogs(user);    
        DataGrid1.DataSource = dataTable;
        DataGrid1.DataBind();
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
    private void LoadRoles()
    {
        bool CustomerUsers = true;
        if (Session["RoleCode"].ToString().Equals("016"))
        {
            
            CustomerUsers = true;
        }
        else
        {
            CustomerUsers = false;
        }
        dataTable = datafile.GetSystemRoles(CustomerUsers);
        cboAccessLevel.DataSource = dataTable;
        cboAccessLevel.DataValueField = "RoleCode";
        cboAccessLevel.DataTextField = "RoleName";
        cboAccessLevel.DataBind();
    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {

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
            user.Name = txtSearch.Text.Trim();
            user.Role = cboAccessLevel.SelectedValue.ToString();
            user.FromDate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            user.ToDate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            dataTable = datafile.GetLogs(user);
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
}
