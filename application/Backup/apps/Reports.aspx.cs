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

public partial class Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FullName"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            string FullName = Session["FullName"].ToString();
            string Area = Session["AreaName"].ToString();
            string Branch = Session["DistrictName"].ToString();
            string Role = Session["RoleName"].ToString();
            string WelcomeMessage = "Welcome " + FullName;
            lblWelcome.Text = WelcomeMessage;
            lblUsage.Text = "Use the Buttons on your Left and Links above to Navigation System Activities and System forms respectively";
        }
    }
}
