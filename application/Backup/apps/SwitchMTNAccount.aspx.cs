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
using System.Collections.Generic;

public partial class SwitchMTNAccount : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    public static int go;
    public string MTNUserName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                LoadAllMTNAccounts2();
                //MultiView1.ActiveViewIndex = 0;
                //LoadMTNAccounts();
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

            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadAllMTNAccounts2()
    {

        dataTable = datapay.GetMTNAccountsToSwitch();
        cboMtnAccountUsername.DataSource = dataTable;
        cboMtnAccountUsername.DataValueField = "MTNAccountUsername";
        cboMtnAccountUsername.DataTextField = "MTNAccountUsername";
        cboMtnAccountUsername.DataBind();
    }

    private void LoadMTNAccounts()
    {

        dataTable = datapay.GetMTNAccountsToSwitch();
        DataGrid1.DataSource = dataTable;
        DataGrid1.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            //MultiView1.ActiveViewIndex = 0;  //Shows the view with button
            //MultiView2.ActiveViewIndex = 0;  //Shows the view with datagrid


        }
        else
        {
            //MultiView1.ActiveViewIndex = -1;
            // MultiView2.ActiveViewIndex = -1;
            ShowMessage("No Record found", true);
        }

        //MultiView3.ActiveViewIndex = 0;

    }
    private void LoadAllVendorsMTNAccountsToSwitch(string MtnAccountUsername)
    {
        if (MtnAccountUsername.Equals("0"))
        {
            MultiView1.ActiveViewIndex = -1;
            btnSwitch.Visible = false;
            ShowMessage("No Record found", true);

        }
        else
        {

            dataTable = datapay.GetAllVendorMTNAccountsToSwitch(MtnAccountUsername);
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                MultiView1.ActiveViewIndex = 0;  //Shows the view with button
                btnSwitch.Visible = true;
            }
            else
            {
                MultiView1.ActiveViewIndex = -1;
                btnSwitch.Visible = false;
                ShowMessage("No Record found", true);
            }


        }
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
    protected void cboMtnAccountUsername_SelectedIndexChanged(object sender, EventArgs e)
    {
        MTNUserName = cboMtnAccountUsername.SelectedValue.ToString();

        Session["MTNName"] = MTNUserName;

        LoadAllVendorsMTNAccountsToSwitch(MTNUserName);

    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
    }


    protected void cboMtnAccountUsername_DataBound(object sender, EventArgs e)
    {
        cboMtnAccountUsername.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select MTN Accounts Names", "0"));
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSwitch_Click(object sender, EventArgs e)
    {
        try
        {
            string userId = Session["Username"] as string;
            string fullname = Session["Fullname"] as string;
            string userBranch = Session["CustomerCode"] as string;
            string page = bll.GetCurrentPageName();

            int count = 0;
            GetData();

            string acc = Session["MTNName"].ToString();

            dataTable = datapay.GetAllVendorMTNAccountsToSwitch(acc);
            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
            DataGrid1.Visible = true;


            DataTable dtable = datapay.GetMTNAccountDetails(acc);

            string UserName = "";
            string Password = "";
            if (dtable.Rows.Count > 0)
            {
                UserName = dtable.Rows[0]["MTNAccountUsername"].ToString();
                Password = dtable.Rows[0]["MTNAccountPassword"].ToString();
            }


            ArrayList arr = (ArrayList)ViewState["SelectedRecords"];

            count = arr.Count;

            // Iterate through the Items.Rows property
            for (int i = 0; i < DataGrid1.Items.Count; i++)
            {
                DataGridItem item = DataGrid1.Items[i];

                if (arr.Contains(item.Cells[1].Text))
                {
                    UpdateActiveOVA(item.Cells[1].Text.ToString(), "MTN");
                    bll.InsertIntoAuditLog(item.Cells[1].Text.ToString(), "UPDATE", "Switch Account", userBranch, userId, page,
fullname + " successfully switched " + item.Cells[1].Text.ToString() + "'s MTN transacting OVA from  [" + item.Cells[2].Text.ToString() + "] to [" + UserName + "] from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());
          
                    arr.Remove(item.Cells[1].Text);

                }
            }
            ViewState["SelectedRecords"] = arr;


            hfCount.Value = "0";
            MultiView1.ActiveViewIndex = -1;
            btnSwitch.Visible = false;

        }
        catch (Exception Ex)
        {
            ShowMessage("Sorry Something went Wrong : " + Ex.Message, true);
        }
    }

    internal void UpdateActiveOVA(string ova, string telecom)
    {
        try
        {
            string newActiveAccount = "";
            if (telecom.ToUpper() == "MTN")
            {
                newActiveAccount = ova;
            }
            else if (telecom.ToUpper() == "AIRTEL")
            {
                newActiveAccount = ova;
            }
            else
            {
                throw new Exception("UNSUPPORTED TELECOM");
            }

            datapay.ExecuteNonQuery("UpdateActiveOVA", telecom, newActiveAccount);
            return;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void UpdateMTNAccount(string VendorCode, string MtnAccUserName, string MtnAccPassword)
    {
        try
        {
            datapay.UpdateVendorMTNAccount(VendorCode, MtnAccUserName, MtnAccPassword); 
            ShowMessage("Update Successfully", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //The GetData function simply retrieves the records for which the user has checked the checkbox, 
    //adds them to an ArrayList and then saves the ArrayList to ViewState 
    private void GetData()
    {
        ArrayList arr;
        if (ViewState["SelectedRecords"] != null)
        {
            arr = (ArrayList)ViewState["SelectedRecords"];
        }
        else
        {
            arr = new ArrayList();
        }
        go = DataGrid1.Items.Count;
        for (int i = 0; i < DataGrid1.Items.Count; i++)
        {
            DataGridItem item = DataGrid1.Items[i];
            CheckBox chkAll = ((CheckBox)(item.FindControl("checkAll")));
            if (chkAll != null && chkAll.Checked)
            {
                if (!arr.Contains(item.Cells[1].Text))
                {
                    arr.Add(item.Cells[1].Text);
                }
            }
            else
            {
                HtmlInputCheckBox chk = ((HtmlInputCheckBox)(item.FindControl("SelectCheckBox")));
                if (chk != null && chk.Checked)
                {
                    if (!arr.Contains(item.Cells[1].Text))
                    {
                        arr.Add(item.Cells[1].Text);

                    }
                }
                else
                {
                    if (arr.Contains(item.Cells[1].Text))
                    {
                        arr.Remove(item.Cells[1].Text);

                    }
                }
            }
        }
        ViewState["SelectedRecords"] = arr;
    }//End of GetData
}
