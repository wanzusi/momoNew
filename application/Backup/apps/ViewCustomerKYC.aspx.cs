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
public partial class ViewVendors : System.Web.UI.Page
{
    ProcessUsers Process = new ProcessUsers();
    DataLogin datafile = new DataLogin();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["AreaID"].ToString().Equals("1"))
            {
                if (IsPostBack == false)
                {
                    chkActive.Checked = true;
                    Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                    Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                    Button MenuReport = (Button)Master.FindControl("btnCalReports");
                    Button MenuRecon = (Button)Master.FindControl("btnAccounts");
                    Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                    Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                    MenuTool.Font.Underline = true;
                    MenuPayment.Font.Underline = false;
                    MenuReport.Font.Underline = false;
                    MenuRecon.Font.Underline = false;
                    MenuAccount.Font.Underline = false;
                    MenuBatching.Font.Underline = false;
                    DisableBtnsOnClick();
                    LoadVendors();
                }
            }
            else
            {
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("YOU DO NOT HAVE RIGHTS TO VIEW THIS PAGE", true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
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
            LoadCustomerKYC();            
         }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadVendors()
    {
        //dtable = datafile.GetAllVendors("0");
        dataTable = datafile.GetSystemCompanies("", "");
        cboVendorCode.DataSource = dataTable;
        cboVendorCode.DataValueField = "CompanyCode";
        cboVendorCode.DataTextField = "Company";
        cboVendorCode.DataBind();
    }
    private void LoadCustomerKYC()
    {
        Vendor vendor = new Vendor();
        vendor.VendorName = txtSearch.Text.Trim();
        vendor.Active = chkActive.Checked;
        dataTable = datafile.GetCustomerKYC(vendor);
        DataGrid1.CurrentPageIndex = 0;
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
            //SystemUser user = new SystemUser();
            if (e.CommandName == "btnAddDevice")
            {
                string VendorCode = e.Item.Cells[2].Text;
                cboVendorCode.SelectedValue = VendorCode.Trim();
                MultiView1.ActiveViewIndex = -1;
                MultiView3.ActiveViewIndex = 0;
                dataTable = datafile.GetCustomerDevices(VendorCode);
                DataGrid2.CurrentPageIndex = 0;
                DataGrid2.DataSource = dataTable;
                DataGrid2.DataBind();
            }
            else if (e.CommandName == "btnEdit")
            {
                string RecordId = e.Item.Cells[0].Text;
                Response.Redirect("AddPosOwnerKYC.aspx?transferId=" + RecordId,true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
    protected void DataGrid2_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "btnEdit")
            {
                string RecordId = e.Item.Cells[0].Text;
                dataTable = datafile.GetCustomerDeviceById(RecordId);
                if (dataTable.Rows.Count>0)
                {
                    MultiView1.ActiveViewIndex = -1;
                    MultiView2.ActiveViewIndex = 0;
                    MultiView3.ActiveViewIndex = 0;
                    lblDeviceId.Text = dataTable.Rows[0]["RecordId"].ToString();
                    txtDeviceId.Text = dataTable.Rows[0]["AgentId"].ToString();
                    txtAgentName.Text = dataTable.Rows[0]["AgentName"].ToString();
                    txtAgentAddress.Text = dataTable.Rows[0]["AgentAddress"].ToString();
                    cboVendorCode.SelectedValue = dataTable.Rows[0]["OwnerId"].ToString();
                    chkIsActive.Checked = bool.Parse(dataTable.Rows[0]["Active"].ToString());
                    txtAgentContact.Text = dataTable.Rows[0]["AgentTelephone"].ToString();
                    txtDeviceSerial.Text = dataTable.Rows[0]["DeviceSerial"].ToString();
                    txtDataSim.Text = dataTable.Rows[0]["DeviceDataSim"].ToString();
                    cboDeviceType.SelectedValue = dataTable.Rows[0]["DeviceType"].ToString();

                }
                else
                {
                }
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
            
            Vendor vendor = new Vendor();
            vendor.VendorName = txtSearch.Text.Trim();
            vendor.Active = chkActive.Checked;
            dataTable = datafile.GetCustomerKYC(vendor);
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView2.ActiveViewIndex = 0;
            MultiView1.ActiveViewIndex = -1;
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void cboVendorCode_DataBound(object sender, EventArgs e)
    {
        cboVendorCode.Items.Insert(0, new ListItem("--Select Agents--", "0"));
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string AgentId = txtDeviceId.Text.Trim();
            string AgentName = txtAgentName.Text.Trim();
            string AgentAddress = txtAgentAddress.Text.Trim();
            string OwnerId = cboVendorCode.SelectedValue.Trim();
            bool Active = chkIsActive.Checked;
            string AgentTel = txtAgentContact.Text.Trim();
            string DeviceSerial = txtDeviceSerial.Text.Trim();
            string DeviceDataSim = txtDataSim.Text.Trim();
            string DeviceType = cboDeviceType.SelectedValue.ToString();
            string RecordId = lblDeviceId.Text;
            if(OwnerId.Equals("0"))
            {
                ShowMessage("Device Owner Not Set", true);
            }
            //else if (AgentName.Equals(""))
            //{
            //    ShowMessage("Agent Name not Set", true);
            //}
            else if (AgentAddress.Equals(""))
            {
                ShowMessage("Agent Address not Set", true);
            }
            else if (AgentTel.Equals(""))
            {
                ShowMessage("Agent Tel Not Set", true);
            }
            else if (DeviceSerial.Equals(""))
            {
                ShowMessage("Device Serial  Not Set", true);
            }
            else if (DeviceDataSim.Equals(""))
            {
                ShowMessage("Device Data Sim Not Set", true);
            }
            else if (DeviceType.Equals("0"))
            {
                ShowMessage("Select Device Type", true);
            }
            else
            {
                if (!bll.DeviceExists(AgentId) && lblDeviceId.Text.Trim().Equals("0"))
                {
                    string ok = bll.SaveDeviceDetails(RecordId,AgentId, AgentName, AgentAddress, OwnerId, Active, DeviceSerial, DeviceDataSim, DeviceType, AgentTel);
                    if (ok.Equals("OK"))
                    {
                        ShowMessage("Device has Been registered successfully", false);
                        ClearControls();
                    }
                    else
                    {
                        ShowMessage("Failed to register Device", true);
                    }
                }
                else if (bll.DeviceExists(AgentId) && !lblDeviceId.Text.Trim().Equals("0"))
                {
                    string ok = bll.SaveDeviceDetails(RecordId, AgentId, AgentName, AgentAddress, OwnerId, Active, DeviceSerial, DeviceDataSim, DeviceType, AgentTel);
                    if (ok.Equals("OK"))
                    {
                        ShowMessage("Device has Been Updated successfully", false);
                        ClearControls();
                    }
                    else
                    {
                        ShowMessage("Failed to Update Device Details", true);
                    }
                }
                else
                {
                    ShowMessage("DeviceId Already registered against an Agent", true);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void ClearControls()
    {
        txtDeviceId.Text = "";
        txtAgentName.Text = "";
        txtAgentAddress.Text = "";
        cboVendorCode.Text = "";
        chkIsActive.Checked = false;
        txtAgentContact.Text = "";
        txtDeviceSerial.Text = "";
        txtDataSim.Text = "";
        txtDataSim.Text = "";
        lblDeviceId.Text = "0";
        MultiView2.ActiveViewIndex = -1;
        MultiView3.ActiveViewIndex = 0;
        MultiView1.ActiveViewIndex = -1;
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = -1;
            MultiView3.ActiveViewIndex = -1;
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }
}
