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
public partial class ReverseTransactions : System.Web.UI.Page
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
        cboTranType.SelectedIndex = cboTranType.Items.IndexOf(cboTranType.Items.FindByValue("1"));
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
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        
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
            dataTable = datapay.GetTransToReverse(vendorcode, vendorref, telecomId, fromdate, todate, Telecom, TranType);
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
                MultiView3.ActiveViewIndex = -1;
            }
        }
        catch(Exception ex)
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
    
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string PegPayId = txtVendorId.Text;
            string Reason = txtReason.Text;
        
            if(PegPayId.Equals(""))
            {
                ShowMessage("PegPay Id not Set",true);
            }
            else if (Reason.Equals(""))
            {
                ShowMessage("Enter Reason", true);

            }
           
            else
            {
                string res = Process.ReverseTrans(PegPayId, Reason);
                if (res.Equals("OK"))
                {
                    LoadTransactions();
                }
                else
                {
                    ShowMessage("Failed to Update transaction status",true);
                }
            }

        }
        catch(Exception ex)
        {
        }
    }
}
