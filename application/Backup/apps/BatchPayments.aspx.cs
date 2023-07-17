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
public partial class BatchPayments : System.Web.UI.Page
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
                    LoadVendors();
                    DisableBtnsOnClick();
                    ToggleOptions();
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
    private void ToggleOptions()
    {
        int group_code = 4;
        int value_code = 1;
        string d_option = datafile.GetSystemParameter(group_code,value_code);
        string option = bll.DecryptString(d_option);
        if (option.Equals("A") || option.Equals("P") || option.Equals("N"))
        {
            if (option.Equals("P"))
            {
                cboBatchType.SelectedIndex = cboBatchType.Items.IndexOf(cboBatchType.Items.FindByValue("1"));
                cboBatchType.Enabled = false;
            }
            else if (option.Equals("N"))
            {
                cboBatchType.SelectedIndex = cboBatchType.Items.IndexOf(cboBatchType.Items.FindByValue("2"));
                cboBatchType.Enabled = false;
            }
            else
            {
                btnOK.Enabled = true;
            }
        }
        else
        {
            ShowMessage("Invalid Batching Option Setting Parameter", true);
            btnOK.Enabled = false;
            cboBatchType.Enabled = false;
        }
    }

    private void LoadVendors()
    {
        dtable = datafile.GetAllVendors("0");
        cboVendor.DataSource = dtable;
        cboVendor.DataValueField = "VendorCode";
        cboVendor.DataTextField = "Vendor";
        cboVendor.DataBind();
    }
    private void DisableBtnsOnClick()
    { 
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        btnReconcile.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnReconcile, "").ToString());
        
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
        string vendorref = "";
        string option = cboBatchType.SelectedValue.ToString();
        string Paymentcode = "0";
        string Account = txtAccountno.Text.Trim();
        string CustName = "";
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        string teller = "";
        if (option.Equals("0"))
        {
            ShowMessage("Please Select Batch Type to Create", true);
        }
        else if (vendorcode.Equals("0"))
        {
            ShowMessage("Please Select Collection Partner", true);
        }
        else
        {
            if (option.Equals("P"))
            {
                dataTable = datapay.GetTransforBatching(vendorcode, vendorref, Account, CustName, Paymentcode, teller, fromdate, todate);
            }
            else
            {
                dataTable = datapay.GetRevTransforBatching(vendorcode, vendorref, Account, CustName, Paymentcode, teller, fromdate, todate);
            }
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                MultiView1.ActiveViewIndex = 0;
                CalculateTotal(dataTable);
                ShowMessage(".", true);
            }
            else
            {
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("No Record found", true);
            }
        }
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
        cboVendor.Items.Insert(0, new ListItem("Select Agent", "0"));
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
            ShowMessage(ex.Message,true);
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
            ShowMessage(ex.Message,true);
        }
    }
    protected void btnReconcile_Click(object sender, EventArgs e)
    {
        try
        {
            string str = GetRecordsToReconcile().TrimEnd(',');
            string option = cboBatchType.SelectedValue.ToString();
            string agentcode = cboVendor.SelectedValue.ToString();
            string ret = Process.Batchstr(str,agentcode,option);
            if (ret.Contains("Successfully"))
            {
                LoadTransactions();
                ShowMessage(ret, false);                
            }
            else
            {
                ShowMessage(ret, true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private string GetRecordsToReconcile()
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
}
