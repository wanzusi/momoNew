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

public partial class General : System.Web.UI.MasterPage
{
    ProcessUsers Usersdll = new ProcessUsers();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if ((Session["FullName"] == null))
            //{
            //    Response.Redirect("Default.aspx");
            //}

            //lblAccountDetails.Text = "SYSTEM ACCOUNT DETAILS | Name : Arajab Nabikambali | Role : System Adminstrator | Area : Head Office";
        }
        catch (NullReferenceException exe)
        {
            Response.Redirect("Default.aspx", false);
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
        user.Uname = Session["UserName"].ToString();
        Usersdll.LogActivity(user);
        string usernamesessionid = Session["usernamesessionid"].ToString();
        Cache.Remove(usernamesessionid);
        //string usernamesessionid = Session["usernamesessionid"].ToString();
        //TimeSpan TimeOut = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);
        //HttpContext.Current.Cache.Insert(usernamesessionid,
        //    null,
        //    null,
        //    DateTime.MaxValue,
        //    TimeOut,
        //    System.Web.Caching.CacheItemPriority.Low,
        //    null);
        //Cache[usernamesessionid] = null;
        Session["Accesslevel"] = "";
        Session["UserName"] = "";
       // Session.Clear();
        Session.Abandon();
        Session["FullName"] = null;
        Response.Redirect("Default.aspx");
    }
    private void SwitchBoard()
    {
       
       Session.Remove("StartPage");
       Session["StartPage"] = "General_Welcome.aspx";
       Response.Redirect("SwitchBoard.aspx");
      
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Logout();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Logout();
    }
}
