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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using System.Text;

public partial class UpdateTelecomId : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    public string message = "";

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if ((Session["UserName"] == null))
    //    {
    //        Response.Redirect("Default.aspx");
    //    }
    //    if (IsPostBack == false)
    //    {
    //        rbnMethod.SelectedValue = "0";
    //        MultiView1.ActiveViewIndex = 0;
    //        MultiView2.ActiveViewIndex = 0;
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (IsPostBack == false)
            {
                rbnMethod.SelectedValue = "0";
                MultiView1.ActiveViewIndex = 1;
                //MultiView1.ActiveViewIndex = 2;
                string RoleId = Session["RoleCode"].ToString();
                if (isRoleAuthorisedToVisitPage(RoleId))
                {
                    LoadData();
                    DisableBtnsOnClick();
                }
                else
                {
                    Response.Redirect("UnauthorisedAccess.aspx");
                }
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
    private void LoadData()
    {
        bll.LoadTelecoms(cboNetwork);
    }

    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
    }
    public void Upload_Click(object sender, EventArgs e)
    {
        try
        {
            FileInfo file = new FileInfo(FileUpload1.FileName);
            string filename = "";
            string uploadedby = Session["UserName"] as string;
            if (!FileUpload1.HasFile)
            {
                message = "UPLOAD A FILE";
                FileUpload1.Focus();
                ShowMessage(message, true);
            }
            else if (FileUpload1.PostedFile.ContentLength >= 30000000)
            {
                FileUpload1.Focus();
                ShowMessage("This file is too big for upload", false);
            }
            else if ((String.IsNullOrEmpty(FileUpload1.FileName)) || !(file.Extension == ".csv"))
            {
                FileUpload1.Focus();
                ShowMessage("Please upload a csv document", true);
            }
            else
            {
                filename = Path.GetFileName(FileUpload1.FileName);
                filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + filename;
                string extension = Path.GetExtension(filename);
                string filePath = @"D:\Files\UnprocessedMomoUpdates";
                string fileDirectoryy = filePath + filename;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                FileUpload1.SaveAs(fileDirectoryy);
                if (!IsvalidFile(fileDirectoryy, out message))
                {
                    FileUpload1.Focus();
                    ShowMessage(message, true);
                }
                else
                {
                    bool save = bll.SaveMomoFile(fileDirectoryy, uploadedby);
                    if (save)
                    {
                        message = "SUCCESSFULLY UPLOADED THE FILE FOR PROCESSING";
                        ShowMessage(message, false);
                        Loadfiles();
                    }
                }
            }
        }
        catch (Exception ee)
        {
            message = ee.Message;
            ShowMessage(message, true);
        }
    }
    private bool IsvalidFile(string filepath, out string error)
    {
        bool valid = false;
        error = "";
        try
        {
            string[] trans = File.ReadAllLines(filepath);
            int x = 0;
            foreach (string fileTran in trans)
            {
                string[] data = fileTran.Split(new string[] { "," }, StringSplitOptions.None);

                if (!(data.Length == 4))
                {
                    valid = false;
                    error = "Invalid Column length. At line " + x + ". File should have 4 columns (Telecom id, Date, Phone Number and Amount)";
                    return valid;
                }

                string network = data[0];
                string pegpayId = data[1];
                string telecomId = data[2];


                if (telecomId.Contains("Transaction ID"))
                {
                    x++;
                    continue;
                }
    
            }
        }
        catch (Exception ee)
        {
            error = ee.Message;
        }
        return valid;
    }
    private void Loadfiles()
    {
        DataTable files = datafile.ExecuteDataSet("GetMomoFiles").Tables[0];
        if (files.Rows.Count > 0)
        {
            //DataGrid2.DataSource = files;
            //DataGrid2.DataBind();
        }
    }
    protected void rbnMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbnMethod.SelectedValue.ToString() == "0")
            {
                MultiView1.ActiveViewIndex = 1;
            }
            else
            {
                MultiView1.ActiveViewIndex = 2;
                Loadfiles();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            UpdateTransaction();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void UpdateTransaction()
    {
        string pegpayId = txtpegpayid.Text.Trim();
        string telecomId = TelecomId.Text.Trim();
        string network = cboNetwork.SelectedValue.ToString().Trim();

        if (string.IsNullOrEmpty(pegpayId))
        {
            message = "Please supply the pegpay Id";
            ShowMissingFieldMessage(message);
            txtpegpayid.Focus();
            return;
        }
        if (string.IsNullOrEmpty(telecomId))
        {
            message = "Please supply a telecom Id";
            ShowMissingFieldMessage(message);
            TelecomId.Focus();
            return;
        }

        MakeTransactionUpdate(network, pegpayId, telecomId);

    }

    private void MakeTransactionUpdate(string network, string pegpayTranId, string telecomid)
    {
        string userId = Session["Username"] as string;
        string fullname = Session["Fullname"] as string;
        string userBranch = Session["UserBranch"] as string;
        string page = bll.GetCurrentPageName();
        DataTable dt = datafile.ExecuteDataSet("GetTransactionByPegPayId", pegpayTranId).Tables[0];
        string vendorId = dt.Rows[0]["VendorTranId"].ToString();
        string telecomId = dt.Rows[0]["TelecomId"].ToString();
        string vendor = dt.Rows[0]["VendorCode"].ToString();
        string tranAmount = dt.Rows[0]["TranAmount"].ToString().Split(new string[] { "." }, StringSplitOptions.None)[0];

        if (vendor.ToUpper() == "FLEXIPAY")
        {
            DataTable table = datafile.GetOnlinePaymentTransaction(vendorId, tranAmount);
            if (table.Rows.Count > 0)
            {
                string sent2telecom = table.Rows[0]["SentToTelecom"].ToString();
                string sent2Pegpay = table.Rows[0]["SentToPegpay"].ToString();
                string sent2school = table.Rows[0]["SentToSchool"].ToString();
                string onlineAmount = table.Rows[0]["TranAmount"].ToString().Split(new string[] { "." }, StringSplitOptions.None)[0];
                string onlineId = table.Rows[0]["RecordId"].ToString();
                string onlineStatus = table.Rows[0]["Status"].ToString();
                string status = dt.Rows[0]["Status"].ToString();

                if (onlineStatus.ToUpper() == "SUCCESS")
                {
                    string orignTelecomId = table.Rows[0]["TelecomId"].ToString();
                    if (telecomid != orignTelecomId)
                    {
                        ShowMessage("Transaction is already success full in school fees " + orignTelecomId + ". Retry with the correct id", true);
                     
                    }
                }
                if (status.ToUpper() == "SUCCESS" ||onlineStatus.ToUpper() == "SUCCESS")
                {
                    string orignTelecomId = dt.Rows[0]["TelecomId"].ToString();
                    if (telecomid != orignTelecomId)
                    {
                        ShowMessage("Transaction is already success full at mobile money with id " + orignTelecomId + ". Retry with the correct id", true);
                        return;
                    }
                }
                if (onlineAmount != tranAmount)
                {
                    ShowMessage("Transaction Amounts don't match original amount " + onlineAmount + ". Verify and try again", true);
                    return;
                }
                CbxSentToMomo.Checked = (sent2telecom.ToUpper() == "TRUE" || sent2telecom.ToUpper() == "1") ? true : false;
                CbxSentToPegpay.Checked = (sent2Pegpay.ToUpper() == "TRUE" || sent2Pegpay.ToUpper() == "1") ? true : false;
                CbxSentToSchool.Checked = (sent2school.ToUpper() == "TRUE" || sent2school.ToUpper() == "1") ? true : false;

                txtPaymentId.Text = onlineId;
                txtPaymentAmount.Text = onlineAmount;

                txt_momoId.Text = pegpayTranId;
                txtMomoAmount.Text = tranAmount;
                txt_TelecomIds.Text = telecomid;

                MultiView1.ActiveViewIndex = 0;

            }
        }
        else
        {
            int updatesuccesscount = datafile.UpdateTransactionWithTelecomId(network, pegpayTranId, telecomid);

            if (updatesuccesscount > 0)
            {
                ShowUpdateMessage(pegpayTranId);
                bll.InsertIntoAuditLog(pegpayTranId.ToString(), "UPDATE", "Update Telecom Id", userBranch, userId, page,
    fullname + " successfully updated the transaction [" + pegpayTranId.ToString() + "]  with the telecom Id [" + telecomid.ToString() + "] from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());


                if (vendor == "STANBIC_MERCHANT")
                {

                    //update in gateway requests table too
                    datafile.ExecuteDataSetInUtilitiesDB("MarkTxnAsSuccessful", vendorId, telecomid);
                }
                else if (vendor == "Fortebet Pulls")
                {
                    FortuneBetRequest request = new FortuneBetRequest();
                    request.VendorCode = vendor;
                    request.PegpayId = pegpayTranId;
                    request.TelecomID = telecomId;

                    bll.LogFortebetTxntInQueue(request);
                }
            }
            else
            {
                ShowNonUpdateMessage(pegpayTranId);
            }
        }
    }

    private void ShowUpdateMessage(string pegpayTranId)
    {

        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("alert('");

        sb.Append("Transaction with PegPayTran Id ");

        sb.Append(pegpayTranId);

        sb.Append(" has been Updated Successfully.');");

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(),

                        "script", sb.ToString());

        ClearControls();

    }

    private void ShowMissingFieldMessage(string errormsg)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("alert('");

        sb.Append(errormsg.ToString());

        sb.Append(".');");

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(),

                        "script", sb.ToString());

    }

    private void ShowNonUpdateMessage(string pegpayTranId)
    {

        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("alert('");

        sb.Append("Transaction with PegPayTran Id ");

        sb.Append(pegpayTranId);

        sb.Append(" has not been Updated. Kindly cross-check the PegPayTranld you have Entered.');");

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(),

                        "script", sb.ToString());

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

    protected void cboNetwork_DataBound(object sender, EventArgs e)
    {
        cboNetwork.Items.Insert(0, new ListItem("ALL Networks Types", "0"));
    }

    private void ClearControls()
    {
        txtpegpayid.Text = "";
        TelecomId.Text = "";
    }

    protected void btnFinish_Click(object sender, EventArgs e)
    {
        try
        {
            string sendToPegpay = CbxSentToPegpay.Checked ? "1" : "0";
            string sendToSchool = CbxSentToSchool.Checked ? "1" : "0";
            string network = "";
            string pegpaytranid = txtpegpayid.Text;
            string telecomid = txt_TelecomIds.Text;
            string pegpayTranId = txt_momoId.Text;
            string vendorId = txtPaymentId.Text;

            int updatesuccesscount = datafile.UpdateTransactionWithTelecomId(network, pegpayTranId, telecomid);
            if (updatesuccesscount > 0)
            {
                datafile.UpdateSchoolFeesOnlinePayments(vendorId, telecomid, sendToPegpay.ToString(), sendToSchool.ToString(), "PENDING");
                ShowMessage("Operation has completed successfully", true);

                MultiView1.ActiveViewIndex = -1;
            }
        }
        catch (Exception ee)
        {

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txt_TelecomIds.Text = "";
        txtPaymentAmount.Text = "";
        txt_momoId.Text = "";
        txtMomoAmount.Text = "";
        CbxSentToSchool.Checked = false;
        CbxSentToPegpay.Checked = false;
        CbxSentToMomo.Checked = false;

        MultiView1.ActiveViewIndex = -1;
    }

    protected void DataGrid2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}