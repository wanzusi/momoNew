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
public partial class ViewVendors : System.Web.UI.Page
{
    ProcessUsers Process = new ProcessUsers();
    DataLogin datafile = new DataLogin();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["AreaID"].ToString().Equals("1"))
            {
                if (IsPostBack == false)
                {
                    chkActive.Checked = true;
                    Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                    Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                    Button MenuReport = (Button)Master.FindControl("btnCalReports");
                    Button MenuRecon = (Button)Master.FindControl("btnAccounts");
                    Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                    Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                    MenuTool.Font.Underline = true;
                    MenuPayment.Font.Underline = false;
                    MenuReport.Font.Underline = false;
                    MenuRecon.Font.Underline = false;
                    MenuAccount.Font.Underline = false;
                    MenuBatching.Font.Underline = false;
                    DisableBtnsOnClick();
                }
            }
            else
            {
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("YOU DO NOT HAVE RIGHTS TO VIEW THIS PAGE", true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
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
            LoadVendors();            
         }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadVendors()
    {
        Vendor vendor = new Vendor();
        vendor.VendorName = txtSearch.Text.Trim();
        vendor.Active = chkActive.Checked;
        dataTable = datafile.GetVendors(vendor);
        DataGrid1.CurrentPageIndex = 0;
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
   
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            SystemUser user = new SystemUser();
            if (e.CommandName == "btnEdit")
            {
                string VendorID = e.Item.Cells[0].Text;     
                Server.Transfer("./AddVendor.aspx?transferid=" + VendorID, true);
            }
            else if (e.CommandName == "btnAddKYC")
            {
                string VendorCode = e.Item.Cells[2].Text;
                Server.Transfer("./AddPosOwnerKYC.aspx?VendorCode=" + VendorCode, true);
            }
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
            Vendor vendor = new Vendor();
            vendor.VendorName = txtSearch.Text.Trim();
            vendor.Active = chkActive.Checked;
            dataTable = datafile.GetVendors(vendor);        
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
}
