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

public partial class Customer : System.Web.UI.Page
{
    private DataTable dataTable = new DataTable();
    private DataLogin datafile = new DataLogin();
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
            lblRole.Text = Session["RoleName"].ToString().ToUpper();
            if (Session["CustomerPegasusAccount"]!=null)
            {
            //ShowAccountBalance();
            }
        }
    }
    private void ShowAccountBalance()
    {
        string PegasusAccount = Session["CustomerPegasusAccount"].ToString();
        Label msg = (Label)Master.FindControl("lblPegasusAccountBal");
       // msg.Visible = true;
        dataTable = datafile.GetAccountBalance(PegasusAccount);
        if (dataTable.Rows.Count > 0)
        {
            double AccBal = Convert.ToDouble(dataTable.Rows[0]["AccountBalance"].ToString());
            msg.Text = AccBal.ToString("#,##0");
        }
    }
}
