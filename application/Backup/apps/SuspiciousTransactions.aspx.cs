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
public partial class SuspiciousTransactions : System.Web.UI.Page
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
                    MultiView3.ActiveViewIndex = 0;
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
        string userBranch = Session["UserBranch"] as string;
        bll.LoadSystemCompanies(userBranch, cboVendor);
        bll.LoadTelecoms(cboTelecoms);
    }

    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        btnReverse.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnReverse, "").ToString());

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
        string Vendor = cboVendor.SelectedValue.ToString();
        string Telecom = cboTelecoms.SelectedValue.ToString();
        string Value = txtpartnerRef.Text.Trim();
        string FromDate = txtfromDate.Text.Trim();
        string ToDate = txttoDate.Text.Trim();

        string[] searchParams = { Vendor, Telecom, Value, FromDate, ToDate };


        if (string.IsNullOrEmpty(FromDate))
        {
            ShowMessage("Please Select A from Date", true);
        }
        else
        {
            dataTable = datafile.ExecuteDataSet("SearchSuspiciousTransactions", searchParams).Tables[0];
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
        chkSelect.Checked = false;
        CheckBox2.Checked = false;
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
                string PegPayId = e.Item.Cells[1].Text;
                string Amount = e.Item.Cells[9].Text;
                //txtAmount.Text = Amount;
                //txtVendorId.Text = PegPayId;
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
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            SelectAllItems();
            if (chkSelect.Checked == true)
            {
                CheckBox2.Checked = true;
            }
            else
            {
                CheckBox2.Checked = false;
            }
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
            SelectAllItems();
            if (CheckBox2.Checked == true)
            {
                chkSelect.Checked = true;
            }
            else
            {
                chkSelect.Checked = false;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void btnReverse_Click(object sender, EventArgs e)
    {
        try
        {
            string str = GetRecordsToReverse().TrimEnd(',');
            //string ret = Process.Reconcilestr(str);
            btnReverse.Enabled = false;
            btnReverse.Text = "PROCESSING......";
            string ret = Process.ReverseTransactions(str, Session);
            if (ret.Contains("reconciled"))
            {
                LoadTransactions();
                ShowMessage(ret, false);
                btnReverse.Enabled = true;
                btnReverse.Text = "REVERSE TRANSACTION(S)";
            }
            else
            {
                ShowMessage(ret, true);
                btnReverse.Enabled = true;
                btnReverse.Text = "REVERSE TRANSACTION(S)";
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
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
                string ItemFound = Items.Cells[1].Text;
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
            btnResend.Enabled = false;
            btnResend.Text = "PROCESSING......";
            string ret = Process.MarkTransactionAsInserted(str, Session);
            if (ret.Contains("Resent"))
            {
                LoadTransactions();
                ShowMessage(ret, false);
                btnResend.Enabled = true;
                btnResend.Text = "RESEND TRANSACTION(S)";
            }
            else
            {
                ShowMessage(ret, true);
                btnResend.Enabled = true;
                btnResend.Text = "RESEND TRANSACTION(S)";
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
}
