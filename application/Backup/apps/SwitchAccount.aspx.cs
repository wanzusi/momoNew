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

public partial class SwitchAccount : System.Web.UI.Page
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
            if (IsPostBack == false)
            {
                 string RoleId = Session["RoleCode"].ToString();
                 if (isRoleAuthorisedToVisitPage(RoleId))
                 {
                     MultiView1.ActiveViewIndex = -1;
                     LoadTransactions();
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
    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        //btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        // btnActivate.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnActivate, "").ToString());

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

        dataTable = datapay.GetAccountsToSwitch();
        DataGrid1.DataSource = dataTable;
        DataGrid1.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = 0;

            ShowMessage(".", true);
        }
        else
        {
            MultiView1.ActiveViewIndex = -1;
            ShowMessage("No Record found", true);
        }

        //MultiView3.ActiveViewIndex = 0;

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
                txtAmount.Text = Amount;
                txtVendorId.Text = PegPayId;
                MultiView2.ActiveViewIndex = 1;
                MultiView1.ActiveViewIndex = -1;

            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }



    protected void btnActivate_Click(object sender, EventArgs e)
    {
        try
        {
            //weird naming : get checklist
            List<string> str = GetRecordsToReverse();
            //string ret = Process.Reconcilestr(str);

            if (str.Count > 2)
            {
                ShowMessage("PLEASE SELECT ONLY ONE ACCOUNT", true);
            }
            else if (str.Count < 2)
            {
                ShowMessage("PLEASE SELECT ACCOUNT TO ACTIVATE", true);
            }
            else
            {
                btnActivate.Enabled = false;
                btnActivate.Text = "PROCESSING......";
                string ret = Process.AcivateAccount(str);
                if (ret.Equals("Activated"))
                {

                    LoadTransactions();
                    ShowMessage(ret, false);
                    btnActivate.Enabled = true;
                    btnActivate.Text = "ACTIVATE";
                }
                else
                {
                    ShowMessage(ret, true);
                    btnActivate.Enabled = true;
                    btnActivate.Text = "ACTIVATE";
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private List<string> GetRecordsToReverse()
    {
        List<string> list = new List<string>();
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
                list.Add(ItemFound);
                list.Add(Items.Cells[1].Text);
            }
        }
        return list;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string PegPayId = txtVendorId.Text;
            string TelecomId = txtTelecomRef.Text;
            if (PegPayId.Equals(""))
            {
                ShowMessage("PegPay Id not Set", true);
            }
            else if (TelecomId.Equals(""))
            {
                ShowMessage("Enter Telecom Id", true);

            }
            else
            {
                string res = Process.UpdateTranStatus(PegPayId, TelecomId);
                if (res.Equals("OK"))
                {
                    LoadTransactions();
                }
                else
                {
                    ShowMessage("Failed to Update transaction status", true);
                }
            }

        }
        catch (Exception ex)
        {
        }
    }
}
