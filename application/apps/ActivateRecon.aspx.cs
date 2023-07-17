using InterLinkClass.EntityObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ActivateRecon : System.Web.UI.Page
{
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

                        MultiView2.ActiveViewIndex = 0;
                        if (Request.QueryString["id"] != null)
                        {
                            string vendorCode = Request.QueryString["id"].ToString();
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
                    MultiView2.ActiveViewIndex = -1;
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

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try 
        { 
            Process.SaveActivateRecon(chkactivate.Checked, Session["FullName"].ToString());
        }
        catch(Exception ex)
        {
            ShowMessage("Error occured", true);
        } 
    }

    protected void chkResetPassword_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            Process.SaveActivateRecon(chkactivate.Checked, Session["FullName"].ToString());
        }
        catch (Exception ex)
        {
            ShowMessage("Error occured", true);
        }
    }
}