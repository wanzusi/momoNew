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
using System.Collections.Generic;

public partial class ManageVendor : System.Web.UI.Page
{
    BusinessLogin bll = new BusinessLogin();
    string username = "";
    string fullname = "";
    string userBranch = "";
    string Id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string RoleId = Session["RoleCode"].ToString();
            //isRoleAuthorisedToVisitPage(RoleId)
            if (true)
            {
                //Check If this is an Edit Request
                Id = Request.QueryString["Id"];

                username = Session["UserName"] as string;
                fullname = Session["FullName"] as string;
                userBranch = Session["UserBranch"] as string;

                Session["IsError"] = null;

                if (string.IsNullOrEmpty(Id) && !Session["RoleName"].ToString().Equals("Manager") && !Session["RoleName"].ToString().Equals("Super Administrator"))
                {
                    MultiView1.ActiveViewIndex = -1;
                    //    //ShowMessage("YOU DO NOT HAVE RIGHTS TO VIEW THIS PAGE", true);
                    Server.Transfer("Accountant.aspx");
                    //    //ShowMessage("YOU DO NOT HAVE RIGHTS TO VIEW THIS PAGE", true);

                }

                //Session is invalid
                if (username == null)
                {
                    Response.Redirect("Default.aspx");
                }
                else if (IsPostBack)
                {

                }
                //this is an edit request
                else if (Id != null)
                {
                    LoadDataEdit(Id);
                    MultiView1.ActiveViewIndex = 0;
                }
                else
                {
                    LoadData();
                    MultiView1.ActiveViewIndex = 0;
                    Multiview2.ActiveViewIndex = 1;
                }
            }
            else
            {
                Response.Redirect("UnauthorisedAccess.aspx");
            }
        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
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
        //bll.LoadTelecoms(ddVendor);
        bll.LoadTelecoms(ddTelecom);
        LoadStatus();
    }

    private void LoadDataEdit(string id)
    {
        //bll.LoadTelecoms(ddVendor);
        DataTable dt = bll.LoadDataForEdit(id);
        foreach (DataRow dr in dt.Rows)
        {
            ddVendor.Text = dr["Vendor"].ToString();
            vendorCode.Text = dr["VendorCode"].ToString();
            ddTelecom.SelectedItem.Text = dr["Network"].ToString();
            ddTranType.SelectedItem.Text = dr["TranType"].ToString();


        }

        vendorCode.Enabled = false;
        ddVendor.Enabled = true;
        ddTelecom.Enabled = false;
        ddTranType.Enabled = false;
        txtPin.Enabled = true;
        txtPin.Visible = true;
        txtSenderId.Enabled = true;
        txtSenderId.Visible = true;
        txtSpId.Enabled = true;
        txtSpId.Visible = true;
        if (ddTranType.SelectedItem.Text == "PULL")
        {
            enabledPulls.Visible = true;
            pushenabledlable.Visible = true;
            enabledPushes.Visible = true;
            enabledPushes.Text = "";
            enabledPushes.Enabled = false;
        }
        if (ddTranType.SelectedItem.Text == "PUSH")
        {
            enabledPushes.Visible = true;
            pullenabledlable.Visible = true;
            enabledPulls.Visible = true;
            enabledPulls.Text = "";
            enabledPulls.Enabled = false;

        }

        ddOvaChoice.Items.Clear();
        ddOvaChoice.Items.Add(new ListItem("All ", ""));
        ddOvaChoice.Items.Add(new ListItem("Pegasus", "Pegasus"));
        ddOvaChoice.Items.Add(new ListItem("Client", "Client"));
        ddOvaChoice.Enabled = true;
        btnSubmit.Text = "UPDATE";
        //bll.LoadTelecoms(ddTelecom);
        //LoadStatus();
    }


    private void LoadStatus()
    {
        ddTranType.Items.Clear();
        ddTranType.Items.Add(new ListItem("All ", ""));
        ddTranType.Items.Add(new ListItem("Pull", "PULL"));
        ddTranType.Items.Add(new ListItem("Push", "PUSH"));
        //ddTranType.Items.Add(new ListItem("Failed", "FailedTransactions"));
        //ddTranType.Items.Add(new ListItem("Reversed", "DeletedTransactions"));

        //ddStatus.Items.Clear();
        //ddStatus.Items.Add(new ListItem("All ", ""));
        //ddStatus.Items.Add(new ListItem("SUCCESS", "SUCCESS"));
        //ddStatus.Items.Add(new ListItem("PENDING", "PENDING"));
        //ddStatus.Items.Add(new ListItem("PROCESSING", "PROCESSING"));

        ddOvaChoice.Items.Clear();
        ddOvaChoice.Items.Add(new ListItem("All ", ""));
        ddOvaChoice.Items.Add(new ListItem("Pegasus", "Pegasus"));
        ddOvaChoice.Items.Add(new ListItem("Client", "Client"));

        SpIdlabel.Visible = false;
        passwordlabel.Visible = false;
        SenderIdlabel.Visible = false;
        pullenabledlable.Visible = false;
        pushenabledlable.Visible = false;
        enabledPulls.Visible = false;
        enabledPushes.Visible = false;
    }

    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            string[] searchParams = GetSearchParameters();
            DataTable dt = SearchDb();
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (!rdPdf.Checked && !rdExcel.Checked)
                    {
                        bll.ShowMessage(lblmsg, "CHECK ONE EXPORT OPTION", true, Session);
                    }
                    else if (rdExcel.Checked)
                    {
                        bll.ExportToExcel(dt, "", Response);
                    }
                    else if (rdPdf.Checked)
                    {
                        bll.ExportToPdf(dt, "", Response);
                    }
                }
                catch (Exception ex)
                {
                    bll.ShowMessage(lblmsg, ex.Message, true, Session);
                }
            }
            else
            {
                string msg = "No Records Found Matching Search Criteria";
                bll.ShowMessage(lblmsg, msg, true, Session);
            }
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    private void SetSessionVariables(DataTable dt)
    {
        //Session["StatementDataTable"] = dt;
        //if (string.IsNullOrEmpty(txtFromDate.Text) || string.IsNullOrEmpty(txtToDate.Text))
        //{
        //    Session["fromDate"] = "THE START";
        //    Session["toDate"] = "TO TODAY";
        //}
        //else
        //{
        //    Session["fromDate"] = txtFromDate.Text;
        //    Session["toDate"] = txtToDate.Text;
        //}
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (vendorCode.Text.Equals(""))
            {
                bll.ShowMessage(lblmsg, "Please Enter the Vendor Code to identfy this vendor", true, Session);
                vendorCode.Focus();
            }
            if (ddVendor.Text.Equals(""))
            {
                bll.ShowMessage(lblmsg, "Please Enter the Vendor Name for this vendor", true, Session);
                ddVendor.Focus();
            }
            if (txtSpId.Text.Equals(""))
            {
                bll.ShowMessage(lblmsg, "Please Enter the SpId or BillerCode for this vendor", true, Session);
                txtSpId.Focus();
            }
            if (txtSenderId.Text.Equals(""))
            {
                bll.ShowMessage(lblmsg, "Please Enter the Sender Id or Merchant Id for this vendor", true, Session);
                txtSenderId.Focus();
            }
            if (txtPin.Text.Equals(""))
            {
                bll.ShowMessage(lblmsg, "Please Enter the Password or Pin for the vendor SpId or Biller Code", true, Session);
                txtPin.Focus();
            }
            else
            {
                if (btnSubmit.Text == "SAVE")
                {
                    SaveVendorCredentials();
                }

                else
                {
                    UpdateVendorCredentials();
                }
            }
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    private DataTable SearchDb()
    {
        DataLogin dh = new DataLogin();
        string[] searchParams = GetSearchParameters();
        DataTable dt = dh.ExecuteDataSet("SearchInterNetworkTransactionsModified", searchParams).Tables[0];
        if (dt.Rows.Count > 0)
        {
            CalculateTotal(dt);
            dataGridResults.DataSource = dt;
            dataGridResults.DataBind();
            string msg = "Found " + dt.Rows.Count + " Records Matching Search Criteria";
            Multiview2.ActiveViewIndex = 0;
            bll.ShowMessage(lblmsg, msg, false, Session);
        }
        else
        {
            CalculateTotal(dt);
            dataGridResults.DataSource = null;
            dataGridResults.DataBind();
            string msg = "No Records Found Matching Search Criteria";
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
        return dt;
    }

    private DataTable SaveVendorCredentials()
    {
        DataLogin dh = new DataLogin();
        string[] searchParams = GetSearchParameters();
        DataTable dt = dh.ExecuteDataSet("SaveVendorCredentials", searchParams).Tables[0];
        if (dt.Rows.Count > 0)
        {
            dataGridResults.DataSource = dt;
            dataGridResults.DataBind();
            string msg = "Vendor details successfully created";
            Multiview2.ActiveViewIndex = 0;
            bll.ShowMessage(lblmsg, msg, false, Session);
        }
        else
        {
            dataGridResults.DataSource = null;
            dataGridResults.DataBind();
            string msg = "Vendor Was not Created";
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
        return dt;
    }


    private DataTable UpdateVendorCredentials()
    {
        DataLogin dh = new DataLogin();
        string[] searchParams = GetUpdateParameters();
        DataTable dt = dh.ExecuteDataSet("UpdateVendorCredentials2", searchParams).Tables[0];
        if (dt.Rows.Count > 0)
        {
            dataGridResults.DataSource = dt;
            dataGridResults.DataBind();
            string msg = "Vendor Details successfully updated";
            Multiview2.ActiveViewIndex = 0;
            bll.ShowMessage(lblmsg, msg, false, Session);
        }
        else
        {
            dataGridResults.DataSource = null;
            dataGridResults.DataBind();
            string msg = "Vendor Details not Updated";
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
        return dt;
    }

    private void CalculateTotal(DataTable Table)
    {
        double total = 0;
        foreach (DataRow dr in Table.Rows)
        {
            double amount = double.Parse(dr["Amount"].ToString());
            total += amount;
        }
        string rolecode = Session["RoleCode"].ToString();
        if (rolecode.Equals("004"))
        {
            lblTotal.Visible = false;
        }
        else
        {
            lblTotal.Visible = true;
        }
        lblTotal.Text = "Total Amount [" + total.ToString("#,##0") + "]";
    }

    private string[] GetSearchParameters()
    {
        List<string> searchCriteria = new List<string>();
        string VendorName = ddVendor.Text;
        string VendorCode = vendorCode.Text;
        string BankCode = ddTelecom.SelectedValue;
        string TranType = ddTranType.SelectedValue;
        string OVAChoice = ddOvaChoice.SelectedValue;
        string SpId = txtSpId.Text;
        string senderId = txtSenderId.Text;
        string password = txtPin.Text;
        string createdBy = username;

        searchCriteria.Add(BankCode);
        searchCriteria.Add(VendorCode);
        searchCriteria.Add(VendorName);
        searchCriteria.Add(TranType);
        searchCriteria.Add(OVAChoice);
        searchCriteria.Add(senderId);
        searchCriteria.Add(SpId);
        searchCriteria.Add(password);
        searchCriteria.Add(createdBy);

        return searchCriteria.ToArray();
    }

    private string[] GetUpdateParameters()
    {
        List<string> searchCriteria = new List<string>();
        string VendorName = ddVendor.Text;
        string VendorCode = vendorCode.Text;
        string BankCode = ddTelecom.SelectedValue;
        string TranType = ddTranType.SelectedValue;
        string OVAChoice = ddOvaChoice.SelectedValue;
        string SpId = txtSpId.Text;
        string senderId = txtSenderId.Text;
        string password = txtPin.Text;
        string createdBy = username;
        string enabledforPulls = enabledPulls.SelectedValue;
        string enabledforPushes = enabledPushes.SelectedValue;
        string RecordId = Id;

        searchCriteria.Add(BankCode);
        searchCriteria.Add(VendorCode);
        searchCriteria.Add(VendorName);
        searchCriteria.Add(TranType);
        searchCriteria.Add(OVAChoice);
        searchCriteria.Add(senderId);
        searchCriteria.Add(SpId);
        searchCriteria.Add(password);
        searchCriteria.Add(createdBy);
        searchCriteria.Add(enabledforPulls);
        searchCriteria.Add(enabledforPushes);
        searchCriteria.Add(RecordId);

        return searchCriteria.ToArray();
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataGridResults.PageIndex = e.NewPageIndex;
        SearchDb();
    }

    protected void ddTranCtegory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string bank = ddTelecom.SelectedValue;
        string payType = ddTranType.SelectedValue;
        if (string.IsNullOrEmpty(bank))
        {
            string msg = "Please Select Bank or Telecom for the vendor";
            bll.ShowMessage(lblmsg, msg, true, Session);
            ddOvaChoice.AutoPostBack = true;
        }
        else if (string.IsNullOrEmpty(payType))
        {
            string msg = "Please Select Account Type";
            bll.ShowMessage(lblmsg, msg, true, Session);
            ddOvaChoice.AutoPostBack = true;
        }
        else
        {
            if (ddOvaChoice.SelectedValue.ToString().ToUpper().Equals("CLIENT"))
            {
                SpIdlabel.Visible = true;
                SenderIdlabel.Visible = true;
                passwordlabel.Visible = true;
                txtSenderId.Visible = true;
                txtSpId.Visible = true;
                txtPin.Visible = true;

            }

            if (ddOvaChoice.SelectedValue.ToString().ToUpper().Equals("PEGASUS"))
            {

                DataLogin dh = new DataLogin();
                string PegasusSpId = "";
                string PegasusSenderId = "";
                string PegasusPassword = "";

                if (ddTelecom.SelectedValue == "AIRTEL")
                {
                    if (payType == "PUSH")
                    {
                        PegasusSpId = dh.GetSystemParameters(9, 21);
                        PegasusSenderId = dh.GetSystemParameters(9, 22);
                        PegasusPassword = dh.GetSystemParameters(9, 23);
                    }
                    else 
                    {
                        DataTable dt = dh.GetAirtelPullAccountDetails("PEGASUS");
                        PegasusSpId = dt.Rows[0]["MobiquityMerchantCode"].ToString();
                        PegasusPassword = dt.Rows[0]["ClientID"].ToString();
                        PegasusSenderId = dt.Rows[0]["OvaName"].ToString();
                    }
                }
                if (ddTelecom.SelectedValue == "MTN")
                {
                    if (payType == "PULL")
                    {
                        PegasusSenderId = dh.GetSystemParameters(35, 14);
                        PegasusPassword = dh.GetSystemParameters(35, 16);
                        PegasusSpId = dh.GetSystemParameters(35, 15);
                    }

                    else
                    {
                        PegasusSenderId = dh.GetSystemParameters(3, 14);
                        PegasusPassword = dh.GetSystemParameters(1, 2);
                        PegasusSpId = dh.GetSystemParameters(1, 1);
                        
                    }
                }
                SpIdlabel.Visible = true;
                SenderIdlabel.Visible = true;
                passwordlabel.Visible = true;
                txtSenderId.Visible = true;
                txtSpId.Visible = true;
                txtPin.Visible = true;
                txtSpId.Enabled = false;
                txtSenderId.Enabled = false;
                txtPin.Enabled = false;

                txtSpId.Text = PegasusSpId;
                txtSenderId.Text = PegasusSenderId;
                txtPin.Text = PegasusPassword;



            }
        }
    }
}
