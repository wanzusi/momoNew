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
                    LoadTranType();
                    LoadNetworks();
                    if (Session["AreaID"].ToString().Equals("2"))
                    {
                        cboTelecoms.SelectedIndex = cboTelecoms.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                        cboTelecoms.Enabled = false;
                    }
                    ToggleVendor();
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
        else
        {
            dataTable = datapay.GetTransToReconcile(vendorcode, vendorref,telecomId, fromdate, todate, Telecom, TranType);
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
            string ret = Process.Reconcilestr(str);
            if (ret.Contains("reconciled"))
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
