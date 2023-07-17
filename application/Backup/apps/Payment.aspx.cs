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
using System.Data;

public partial class Payment : System.Web.UI.Page
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
            //Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
            //Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
            //Button MenuReport = (Button)Master.FindControl("btnCalReports");
            //Button MenuRecon = (Button)Master.FindControl("btnAccounts");
            //Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
            //Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
            //MenuTool.Font.Underline = false;
            //MenuPayment.Font.Underline = true;
            //MenuReport.Font.Underline = false;
            //MenuRecon.Font.Underline = false;
            //MenuAccount.Font.Underline = false;
            //MenuBatching.Font.Underline = false;
            ShowAccountBalance();
        }
    }
    private void ShowAccountBalance()
    {
        string PegasusAccount = Session["CustomerPegasusAccount"].ToString();
        Label msg = (Label)Master.FindControl("lblPegasusAccountBal");
        msg.Visible = true;
        dataTable = datafile.GetAccountBalance(PegasusAccount);
        if (dataTable.Rows.Count > 0)
        {
            string bal = dataTable.Rows[0]["AccountBalance"].ToString();

            double AccBal = 0;// Convert.ToDouble();
            if (Double.TryParse(bal, out AccBal))
            {
                msg.Text = AccBal.ToString("#,##0");
            }
            else
            {
                msg.Text = bal;
            }
        }
    }
}
