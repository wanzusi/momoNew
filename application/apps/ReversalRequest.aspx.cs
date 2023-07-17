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

public partial class ReversalRequest : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string RoleId = Session["RoleCode"].ToString();
                if (isRoleAuthorisedToVisitPage(RoleId))
                {
                    LoadVendors();
                }
                else
                {
                    Response.Redirect("UnauthorisedAccess.aspx");
                }
            }
            DataGrid1.Columns[8].Visible = false;
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
    private void LoadVendors()
    {
        //dtable = datafile.GetAllVendors("0");
        dtable = datafile.GetSystemCompanies("", "");
        cboVendor.DataSource = dtable;
        cboVendor.DataValueField = "CompanyCode";
        cboVendor.DataTextField = "Company";
        cboVendor.DataBind();

        cboVendor.SelectedValue = Session["Company"].ToString();

        //if (Session["RoleCode"].ToString() == "003")
        //{
        //    dll_status.Enabled = false;
        //}
    }

    private void LoadTransactions()
    {
        string vendorcode = cboVendor.SelectedValue.ToString();
        string vendorref = txtpartnerRef.Text.Trim();

        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);

        if (vendorcode.Equals("0"))
        {
            ShowMessage("Please provide a vendor transaction reference", true);
        }
      
        else
        {
            string status = dll_status.SelectedValue.ToString();
            dataTable = datafile.getReversalRequest(vendorcode, status, vendorref, fromdate.ToString("yyyy-MM-dd"), todate.ToString("yyyy-MM-dd"));
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();

            if (dataTable.Rows.Count > 0)
            {
                MultiView1.ActiveViewIndex = 0;
                ShowMessage(".", true);
            }
            else
            {
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("No Record found", true);
            }
        }
    }


    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "btnEdit")
            {
                string compCode = cboVendor.SelectedValue.ToString();
                if (Session["RoleCode"].ToString() == "003" || Session["RoleCode"].ToString() == "007")
                {
                    string PegPayId = e.Item.Cells[1].Text;
                    string telecomId = e.Item.Cells[2].Text;
                    string Amount = e.Item.Cells[3].Text;
                    string status = e.Item.Cells[5].Text;
                    txtAmount.Text = Amount;
                    txtTelecomId.Text = telecomId;
                    txtPegyapId.Text = PegPayId;
                    lbl_amount.Text = Amount;
                    txtReason.Text = e.Item.Cells[6].Text;
                    txtAmount.Enabled = true;
                    txtTelecomId.Enabled = false;
                    txtPhone.Text = e.Item.Cells[7].Text;
                    if (status == "FAILED" || status == "SUCCESS")
                    {
                        ShowMessage("REVERSAL ACTION HAS ALREADY BEEN TAKEN", true);
                        MultiView2.ActiveViewIndex = -1;
                    }
                    else
                    {
                        MultiView1.ActiveViewIndex = -1;
                        MultiView2.ActiveViewIndex = 0;
                    }
                }
                else
                {
                    string vendorId = e.Item.Cells[0].Text;
                    string PegPayId = e.Item.Cells[1].Text;
                    string telecomId = e.Item.Cells[2].Text;
                    string Amount = e.Item.Cells[3].Text;
                    string payDate = e.Item.Cells[4].Text;
                    string status = e.Item.Cells[5].Text;
                    string reason = e.Item.Cells[6].Text;
                    string phone = e.Item.Cells[7].Text;
                    PhoneValidator val = new PhoneValidator();
                    string network = val.GetNetwork(val.FormatTelephone(phone));
                    string count = e.Item.Cells[8].Text;
                    count = (string.IsNullOrEmpty(count) || count == "&nbsp") ? "0" : count;
                    int mailCount;
                    Int32.TryParse(count, out mailCount);

                    if (status.ToUpper() == "PENDING" && mailCount <4 )
                    {
                        string userId = Session["UserName"].ToString();
                        bool success = datafile.SaveReversalRequest(compCode, vendorId, "", PegPayId, Amount, reason, "PENDING", "", userId);
                        if (success)
                        {
                            ShowMessage("Reversal request has been RE-Submited".ToUpper(), false);

                            SendMail sendMail = new SendMail();
                            string message = "Kind Reminder!<br/>Please help us reverse the transaction with the following details";
                            message += "<br/>PegpayId = " + PegPayId;
                            message += "<br/>TelecomId = " + telecomId;
                            message += "<br/>Amount = " + Amount;
                            message += "<br/>Payment Date = " + payDate;
                            reason = reason.ToUpper();
                            message += "<br/>Reason: " + reason;
                            string subject = "Request to reverse transaction. TelecomId = "+ telecomId.ToUpper();
                            if (mailCount > 2)
                            {
                                subject = "URGENT!!! Request to reverse transaction. TelecomId = "+ telecomId.ToUpper();
                            }
                            LoadTransactions();
                            sendMail.SendReveralEmail2(compCode, message, network, subject, false);

                        }
                        else
                        {
                            ShowMessage("Operation has failed".ToUpper(), true);
                        }
                    }
                    if (status.ToUpper() == "FAILED")
                    {
                        ShowMessage("Reversal failed: " + reason.ToUpper(), false);
                    }
                    else if (mailCount >= 4)
                    {
                        ShowMessage("Request for reversal has already been made", true);
                    }
                }
            }
            else
            {
                MultiView2.ActiveViewIndex = -1;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {

    }
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string pegpayId = txtPegyapId.Text;
            string telecomId = txtTelecomId.Text;
            string vendorCode = cboVendor.SelectedValue.ToString();
            string reason = txtReason.Text;
            string amount = txtAmount.Text.Replace(",", "");
            string amount2 = lbl_amount.Text.Replace(",", "");
            string phone = txtPhone.Text;
            PhoneValidator val = new PhoneValidator();
            string network = val.GetNetwork(val.FormatTelephone(phone));

            Double amt1, amt2;

            if (!Double.TryParse(amount2, out amt2))
            {
                ShowMessage("Erroneous transaction information in original amount", true);
            }
            if (string.IsNullOrEmpty(pegpayId))
            {
                ShowMessage("Please provide a pegpay id ", true);
            }
            else if (string.IsNullOrEmpty(telecomId))
            {
                ShowMessage("Please provide a telecom id ", true);
            }
            else if (!Double.TryParse(amount, out amt1))
            {
                ShowMessage("Invalid transaction amount", true);
            }
            else if(string.IsNullOrEmpty(txt_ReversalId.Text.Trim()))
            {
                ShowMessage("Please provide telecom reversal id",true);
            }
            else if (!IsValidReversalRequest(txt_ReversalId.Text.Trim(), telecomId))
            {
                ShowMessage("Reversal Id already used", true);
            }
            else if (reason.Equals(""))
            {
                ShowMessage("Enter Reason", true);

            }
            else if (amt1 > amt2)
            {
                ShowMessage("Amount being reversed can't be less than the orignal transaction amount", true);
            }
            else
            {
                reason = txt_ReversalId.Text.Trim();
                bool success = datafile.ReverseSuccessFullTelecomTransaction(pegpayId, (int)amt1 + "", reason, telecomId);
                if (success)
                {
                    ShowMessage("Transaction is successfully reversed".ToUpper(), false);
                    string userId = Session ["UserName"].ToString();
                    datafile.UpdateTransactionReversalStatuus(pegpayId, "SUCCESS", reason, userId);
                    txtReason.Text = "";
                    txtAmount.Text = "";
                    lbl_amount.Text = "";


                    SendMail sendMail = new SendMail();
                    string message = "Reversal has been effected";
                    message += "<br/>PegpayId = " + pegpayId;
                    message += "<br/>TelecomId = " + telecomId;
                    message += "<br/>Amount = " + amount2;
                   // message += "<br/>Payment Date = " + paymentdate;
                    reason = reason.ToUpper();
                    message += "<br/>Reason: " + reason;

                    sendMail.SendReveralEmail2(vendorCode, message, network, "Request to reverse transaction. TelecomId = " + telecomId.ToUpper(), true);
                    MultiView2.ActiveViewIndex =-1;
                    LoadTransactions();
                }
                else
                {
                    ShowMessage("Operation has failed".ToUpper(), true);
                }
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    private bool IsValidReversalRequest(string reversalId, string telecomId)
    {
        bool isValid = false;
        try
        {
            DataTable table = datafile.getReversalsById(reversalId);
            if (table.Rows.Count < 1)
            {
                isValid = true;
            }

        }
        catch (Exception ee)
        {
            
        }

        return isValid;
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            LoadTransactions();
        }
        catch (Exception ee)
        {
            ShowMessage(ee.Message, true);
        }
    }



    protected void RejectTransaction_Click(object sender, EventArgs e)
    {
        try
        {
            string pegpayId = txtPegyapId.Text;
            string telecomId = txtTelecomId.Text;
            string vendorCode = cboVendor.SelectedValue.ToString();
            string reason = txtReason.Text;
            string userId = Session["UserName"].ToString();
            bool success = datafile.UpdateTransactionReversalStatuus(pegpayId, "FAILED", reason, userId);
            if (success)
            {
                ShowMessage("Transaction has been successfully rejected".ToUpper(), false);
                MultiView2.ActiveViewIndex = -1;
            }
            else
            {
                ShowMessage("Operation has failed".ToUpper(), true);
            }
        }
        catch (Exception ee)
        {
            throw;
        }
    }
}
