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

public partial class AccountStatments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                Page_Initials();
            }
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }

    private void Page_Initials()
    {
        //string 
        //if()
        //{

        //}
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

        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    protected void cboPaymentType_DataBound(object sender, EventArgs e)
    {

        cboCustomerAccount.Items.Insert(0, new ListItem("All Payment Types", "0"));
    }
}
