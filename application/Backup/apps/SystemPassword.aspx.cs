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

public partial class SystemPassword : System.Web.UI.Page
{
    DataLogin dac = new DataLogin();
    ProcessUsers Usersdll = new ProcessUsers();
    BusinessLogin bll = new BusinessLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
             string RoleId = Session["RoleCode"].ToString();
             if (isRoleAuthorisedToVisitPage(RoleId))
             {
                 lblUserCode.Text = Session["UserID"].ToString();

             }
             else
             {
                 Response.Redirect("UnauthorisedAccess.aspx");
             }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
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
        BtnSave.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(BtnSave, "").ToString());

    }
    private void ShowMessage(string Message)
    {
        Label msg = (Label)Master.FindControl("lblmsg");
        if (Message == ".")
        {
            msg.Text = ".";
        }
        else
        {
            msg.Text = "MESSAGE: " + Message.ToUpper();
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SavePassword();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }
    private void SavePassword()
    {
        SystemUser user = new SystemUser();
        user.Passwd = txtnewpw.Text.Trim();
        user.Opasswd = txtoldpw.Text.Trim();
        user.Cpasswd = txtconfirmpw.Text.Trim();
        if (user.Opasswd.Equals(""))
        {
            ShowMessage("Please Enter Your Old Password");
            txtoldpw.Focus();
        }
        else if (user.Passwd.Equals(""))
        {
            ShowMessage("Please Enter Your New Password");
            txtnewpw.Focus();
        }
        else if (user.Cpasswd.Equals(""))
        {
            ShowMessage("Please Confirm Your Password");
            txtconfirmpw.Focus();
        }
        else if (user.Cpasswd != user.Passwd)
        {
            ShowMessage("Your Passwords donot match");
            txtconfirmpw.Focus();
        }
        else
        {
            user.Userid = int.Parse(lblUserCode.Text.ToString());
            user.Passwd = txtnewpw.Text.Trim();
            user.Opasswd = txtoldpw.Text.Trim();
            user.Cpasswd = txtconfirmpw.Text.Trim();
            string returned = Usersdll.ChangeMyPassword(user);
            ShowMessage(returned);
            if (returned.Contains("Successfully"))
            {
                user.Uname = Session["UserName"].ToString();
                user.Action = "Password Change";
                Usersdll.LogActivity(user);
                ClearContrl();
            }           
        }
    }

    private void ClearContrl()
    {
        txtconfirmpw.Text = "";
        txtnewpw.Text = "";
        txtoldpw.Text = "";
    }
}
