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

public partial class ReconcileVASTransactions : System.Web.UI.Page
{
    DataTable dtable = new DataTable();
    DataLogin datafile = new DataLogin();
    BusinessLogin bll = new BusinessLogin();
    string telecomreconfile = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                LoadVASUtilities();
                ToggleVendor();
                Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                Button MenuReport = (Button)Master.FindControl("btnCalReports");
                Button MenuRecon = (Button)Master.FindControl("btnAccounts");
                Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                MenuTool.Font.Underline = false;
                MenuPayment.Font.Underline = false;
                MenuReport.Font.Underline = false;
                MenuRecon.Font.Underline = true;
                MenuAccount.Font.Underline = false;
                MenuBatching.Font.Underline = false;
                //DisableBtnsOnClick();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadVASUtilities()
    {
        dtable = datafile.GetAllStanbicVasUtilities();
        // Session["VendorCode"] = dtable.Rows[0]["VendorCode"].ToString();
        cboVASTxn.DataSource = dtable;
        cboVASTxn.DataValueField = "Utility";
        cboVASTxn.DataTextField = "Utility";
        cboVASTxn.DataBind();
    }

    private void ToggleVendor()
    {
        string districtcode = Session["DistrictCode"].ToString();
        string role = Session["RoleCode"].ToString();
        if (role.Equals("005"))
        {
            cboVASTxn.Enabled = false;
            cboVASTxn.SelectedIndex = cboVASTxn.Items.IndexOf(cboVASTxn.Items.FindByValue(districtcode));
        }
        else
        {
            cboVASTxn.Enabled = true;
        }
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            string vendorcode = cboVASTxn.SelectedValue.ToString();
            if (vendorcode.Equals("0"))
            {
                ShowMessage("Please Vendor for Reconciliation", true);
            }
            else if (FileUpload1.FileName.Trim().Equals(""))
            {
                ShowMessage("Please Browser file to reconcile", true);
            }
            else
            {
                ReadFileToRecon(vendorcode);
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    private void ReadFileToRecon(string vendorcode)
    {
        string filename = Path.GetFileName(FileUpload1.FileName);
        string extension = Path.GetExtension(filename);
        if (extension.ToUpper().Equals(".CSV") || extension.ToUpper().Equals(".TXT"))
        {
            string filePath = bll.ReconFilePath(vendorcode, filename);
            FileUpload1.SaveAs(filePath);
            telecomreconfile = filePath;
            string sessionEmail = Session["UserEmail"].ToString();
            string name = Session["FullName"].ToString();
            string user = Session["Username"].ToString();

            datafile.SaveUploadedVASReconFileDetail(telecomreconfile, user, sessionEmail, 0, vendorcode);

            ShowMessage("Hello\t" + name + "\t\tVAS Transactions File has been Uploaded Successfully Reconciliation has started and the report will be sent to your Email Shortly", true);
        }
        else
        {
            ShowMessage("Please Browser CSV File, " + extension + " file not supported", true);
        }

    }
    protected void cboVASTxn_DataBound(object sender, EventArgs e)
    {
        cboVASTxn.Items.Insert(0, new ListItem("Select", "0"));
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
}
