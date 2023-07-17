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

public partial class NewPayments : System.Web.UI.MasterPage
{
    ProcessUsers Usersdll = new ProcessUsers();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (Session["LoginId"] == null)
            //    Response.Redirect("Default.aspx");
            if ((Session["FullName"] == null))
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                DataLogin dll = new DataLogin();
                SystemUser user = new SystemUser();
                user.Uname = Session["UserName"].ToString();
                dll.DeactivateLogInOTPs(user.Uname);
                Response.ClearHeaders();
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
            }
            lblUserId.Text = Session["FullName"].ToString();
            string AreaDesc = "";
            string Area = Session["AreaName"].ToString();
            string Branch = Session["DistrictName"].ToString();
            if (Branch.Equals("NONE"))
            {
                AreaDesc = Area;
            }
            else
            {
                AreaDesc = Area + " - " + Branch;
            }
            lblArea.Text = AreaDesc;
            lblRole.Text = Session["RoleName"].ToString();
        }
        catch (NullReferenceException exe)
        {
            Response.Redirect("Default.aspx?login=1", false);

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    private void Logout()
    {
        SystemUser user = new SystemUser();
        user.Action = "Logged-out";
        user.Name = Session["FullName"].ToString();
        user.Uname = Session["UserName"].ToString();
        user.CompanyCode = Session["UserBranch"].ToString();
        user.Userid = int.Parse(Session["UserID"].ToString());
        user.LoggedOn = false;
        Usersdll.LogActivity(user);
        Usersdll.LoginStatus(user);
        BusinessLogin bll = new BusinessLogin();
        bll.InsertIntoAuditLog(user.Uname, "LOGOUT", "Logout", user.CompanyCode, user.Uname, bll.GetCurrentPageName(),
user.Name + " was successfully logged out from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString() + " .Reason: The user logged out");
       
        Session["Accesslevel"] = "";
        Session["UserName"] = "";
        Session.Clear();
        Session.Abandon();
        Session["FullName"] = null;
        Response.Redirect("Default.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Logout();
    }
    protected void btnCallSystemTool_Click(object sender, EventArgs e)
    {       
        Response.Redirect("Admin.aspx");
    }
    protected void btnCallAccountDetails_Click(object sender, EventArgs e)
    {
        Response.Redirect("SystemPassword.aspx");
    }
    protected void btnAccounts_Click(object sender, EventArgs e)
    {
        Response.Redirect("Accounts.aspx");
    }
    protected void btnCalReports_Click(object sender, EventArgs e)
    {
        Response.Redirect("Reports.aspx");
    }
    protected void btnCallBatching_Click(object sender, EventArgs e)
    {
        Response.Redirect("Batching.aspx");
    }
    protected void btnCallPayments_Click(object sender, EventArgs e)
    {
        Response.Redirect("Payment.aspx");
    }
    protected void btnCallBatching_Click1(object sender, EventArgs e)
    {
        Response.Redirect("Billing.aspx");
    }
    protected void btnCustomers_Click(object sender, EventArgs e)
    {
        Response.Redirect("Customer.aspx");
    }
    protected void btnBeneficiaries_Click(object sender, EventArgs e)
    {
        Response.Redirect("Beneficiaries.aspx");
    }
    protected void btnCalRecon_Click(object sender, EventArgs e)
    {
        Response.Redirect("Recon.aspx");
    }
    protected void btncalReports2_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminReports.aspx");
    }

    protected void btnCallInter_Click(object sender, EventArgs e)
    {
        Response.Redirect("InternetworkTransactions.aspx");
    }
}
