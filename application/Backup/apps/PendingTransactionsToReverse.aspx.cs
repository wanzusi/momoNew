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
public partial class Reconciliation : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    Transaction2 Trans = new Transaction2();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                MultiView1.ActiveViewIndex = -1;
                MultiView3.ActiveViewIndex = 0;
                LoadVendors();
                LoadTranType();
                LoadNetworks();
                if (Session["AreaID"].ToString().Equals("2"))
                {
                    cboTelecoms.SelectedIndex = cboTelecoms.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                    cboTelecoms.Enabled = false;
                }
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
                DisableBtnsOnClick();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadNetworks()
    {
        dtable = datafile.GetNetworks();
        cboTelecoms.DataSource = dtable;
        cboTelecoms.DataValueField = "Network";
        cboTelecoms.DataTextField = "Network";
        cboTelecoms.DataBind();
    }

    private void LoadTranType()
    {
        dtable = datafile.GetTranType();
        cboTranType.DataSource = dtable;
        cboTranType.DataValueField = "TypeId";
        cboTranType.DataTextField = "TranType";
        cboTranType.DataBind();
        cboTranType.SelectedIndex = cboTranType.Items.IndexOf(cboTranType.Items.FindByValue("2"));
        cboTranType.Enabled = false;
    }
    private void ToggleVendor()
    {
        string districtcode = Session["DistrictCode"].ToString();
        string role = Session["RoleCode"].ToString();
        if (role.Equals("005"))
        {
            cboVendor.Enabled = false;
            cboVendor.SelectedIndex = cboVendor.Items.IndexOf(cboVendor.Items.FindByValue(districtcode));
        }
        else
        {
            cboVendor.Enabled = true;
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
    }
    private void DisableBtnsOnClick()
    {
        //string strProcessScript = "this.value='Working...';this.disabled=true;";
        //btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        //btnReverse.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnReverse, "").ToString());

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            LoadTransactions();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadTransactions()
    {
        string vendorcode = cboVendor.SelectedValue.ToString();
        string vendorref = txtpartnerRef.Text.Trim();
        string Paymentcode = "0";
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        string telecomId = txtSearch.Text.Trim();
        string Telecom = cboTelecoms.SelectedValue.ToString();
        string TranType = cboTranType.SelectedValue.ToString();
        //if (vendorcode.Equals("0"))
        //{
        //    ShowMessage("Please Select Collection Partner", true);
        //}
        // else 
        if (Telecom.Equals("0"))
        {
            ShowMessage("Please Select a Telecom", true);
        }
        else if (txtfromDate.Text.Trim().Equals(""))
        {
            ShowMessage("Please Select a start date", true);
        }
        else
        {
            dataTable = datapay.GetPendingTransToRetry(vendorcode, vendorref, "", fromdate, todate, "", TranType);


            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                MultiView1.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = 0;
                CalculateTotal(dataTable);
                ShowMessage(".", true);
            }
            else
            {
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("No Record found", true);
            }
        }
        MultiView3.ActiveViewIndex = 0;
        //  chkSelect.Checked = false;
        // CheckBox2.Checked = false;
    }

    private void CalculateTotal(DataTable Table)
    {
        double total = 0;
        foreach (DataRow dr in Table.Rows)
        {
            double amount = double.Parse(dr["TranAmount"].ToString());
            total += amount;
        }
        lblTotal.Text = "Total Amount of Transactions [" + total.ToString("#,##0") + "]";
    }
    private void LoadUsers()
    {

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
            if (e.CommandName == "btnEdit")
            {
                string PegPayId = e.Item.Cells[0].Text;
                string Amount = e.Item.Cells[9].Text;
                //txtAmount.Text = Amount;
               // txtVendorId.Text = PegPayId;
                MultiView2.ActiveViewIndex = 1;
                MultiView1.ActiveViewIndex = -1;
                MultiView3.ActiveViewIndex = -1;
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

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }

    protected void cboVendor_DataBound(object sender, EventArgs e)
    {
        cboVendor.Items.Insert(0, new ListItem("ALL Agents", "0"));
    }
    protected void cboTelecoms_DataBound(object sender, EventArgs e)
    {
        cboTelecoms.Items.Insert(0, new ListItem("Select Telecom", "0"));
    }
    protected void cboTranType_DataBound(object sender, EventArgs e)
    {
        cboTranType.Items.Insert(0, new ListItem("All Tran Types", "0"));
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //SelectAllItems();
            //if (chkSelect.Checked == true)
            //{
            //    CheckBox2.Checked = true;
            //}
            //else
            //{
            //    CheckBox2.Checked = false;
            //}
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void SelectAllItems()
    {
        foreach (DataGridItem Items in DataGrid1.Items)
        {
            CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
            if (chk.Checked)
            {
                chk.Checked = false;
            }
            else
            {
                chk.Checked = true;
            }
        }
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //SelectAllItems();
            //if (CheckBox2.Checked == true)
            //{
            //    chkSelect.Checked = true;
            //}
            //else
            //{
            //    chkSelect.Checked = false;
            //}
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    //protected void btnReverse_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string str = GetRecordsToReverse().TrimEnd(',');
    //        //string ret = Process.Reconcilestr(str);
    //        btnReverse.Enabled = false;
    //        btnReverse.Text = "PROCESSING......";
    //        string ret = Process.ReverseTransactions(str);
    //        if (ret.Contains("reconciled"))
    //        {
    //            LoadTransactions();
    //            ShowMessage(ret, false);
    //            btnReverse.Enabled = true;
    //            btnReverse.Text = "REVERSE TRANSACTION(S)";
    //        }
    //        else
    //        {
    //            ShowMessage(ret, true);
    //            btnReverse.Enabled = true;
    //            btnReverse.Text = "REVERSE TRANSACTION(S)";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, true);
    //    }
    //}
    private string GetRecordsToReverse()
    {
        int Count = 0;
        string ItemArr = "";
        foreach (DataGridItem Items in DataGrid1.Items)
        {
            CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
            if (chk.Checked)
            {
                Count++;
                string ItemFound = Items.Cells[0].Text;
                ItemArr = ItemArr += ItemFound + ",";
            }
        }
        return ItemArr;
    }
    protected void btnResend_Click(object sender, EventArgs e)
    {
        try
        {
            string str = GetRecordsToReverse().TrimEnd(',');
            //string ret = Process.Reconcilestr(str);
            btnResend.Enabled = false;
            btnResend.Text = "PROCESSING......";
            string ret = Process.ReverseTranToRecieved(str);
            if (ret.Contains("SUCCESSFUL"))
            {
                LoadTransactions();
                ShowMessage(ret, false);
                btnResend.Enabled = true;
                btnResend.Text = "Reverse Txn(s) to Received";
            }
            else
            {
                //LoadTransactions();
                ShowMessage(ret, true);
                btnResend.Enabled = true;
                btnResend.Text = "Reverse Txn(s) to Received";
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    //string PegPayId = txtVendorId.Text;
        //    //string TelecomId = txtTelecomRef.Text;
        //    if (PegPayId.Equals(""))
        //    {
        //        ShowMessage("PegPay Id not Set", true);
        //    }
        //    else if (TelecomId.Equals(""))
        //    {
        //        ShowMessage("Enter Telecom Id", true);

        //    }
        //    else
        //    {
        //        string res = Process.UpdateTranStatus(PegPayId, TelecomId);
        //        if (res.Equals("OK"))
        //        {
        //            LoadTransactions();
        //        }
        //        else
        //        {
        //            ShowMessage("Failed to Update transaction status", true);
        //        }
        //    }

        //}
        //catch (Exception ex)
        //{
        //}
    }
}
