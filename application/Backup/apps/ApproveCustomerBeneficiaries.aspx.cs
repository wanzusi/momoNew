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
public partial class ApproveCustomerBeneficiaries : System.Web.UI.Page
{
    ProcessUsers Process = new ProcessUsers();
    DataLogin datafile = new DataLogin();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    Beneficiary beneficiary = new Beneficiary();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                string RoleId = Session["RoleCode"].ToString();
                if (isRoleAuthorisedToVisitPage(RoleId))
                {
                    LoadBeneficaryType();
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
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            LoadBeneficaries();            
         }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadBeneficaries()
    {
        string name = txtSearch.Text.Trim();
        string phone = txtSearchPhone.Text.Trim();
        string benType = cboBeneficiaryType.SelectedValue.ToString();
        string CustomerCode = Session["CustomerCode"].ToString();
        string Location = txtdistrict.Text;
        dataTable = datafile.GetCustomerBeneficariesToApprove(name, phone, benType, CustomerCode, Location);
        if (dataTable.Rows.Count > 0)
        {
            ShowMessage(".", true);
            MultiView1.ActiveViewIndex = 0;
            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        else
        {
            ShowMessage("No Beneficiary(ies) Found", true);
            MultiView1.ActiveViewIndex = -1;
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
    private void LoadBeneficaryType()
    {
        dataTable = datafile.GetBeneficaryType();
        cboBeneficiaryType.DataSource = dataTable;
        cboBeneficiaryType.DataValueField = "TypeCode";
        cboBeneficiaryType.DataTextField = "TypeName";
        cboBeneficiaryType.DataBind();

    }
    protected void cboBeneficiaryType_DataBound(object sender, EventArgs e)
    {
        cboBeneficiaryType.Items.Insert(0, new ListItem("All Beneficary Types ", "0"));
    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "btnEdit")
            {
                string RecordId = e.Item.Cells[0].Text;
                Server.Transfer("AddCustomerBeneficiary.aspx?transfereid=" + RecordId);
                //AssginControls(RecordId);
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

            string name = txtSearch.Text.Trim();
            string phone = txtSearchPhone.Text.Trim();
            string benType = cboBeneficiaryType.SelectedValue.ToString();
            string CustomerCode = Session["CustomerCode"].ToString();
            string Location = txtdistrict.Text;
            dataTable = datafile.GetCustomerBeneficariesToApprove(name, phone, benType, CustomerCode, Location);          
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
    
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        btnOK.Enabled = true;
        cboBeneficiaryType.Enabled = true;
        ShowMessage(".", true);
    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string str = GetRecordsToApprove().TrimEnd(',');
            if (str.Equals(""))
            {
                ShowMessage("Please Select Beneficiary(ies) to Approve", true);
            }
            else
            {
                ProcessApprovals(str);
                LoadBeneficaries();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }

    private void ProcessApprovals(string str)
    {
        try
        {
            int suc = 0;
            int failed = 0;
            int count = 0;
            int UserId = 0;
            string[] arr = str.Split(',');
            int i = 0;
            for (i = 0; i < arr.Length; i++)
            {
                int BeneficairyId = int.Parse(arr[i].ToString());
                string Status = datafile.ApproveCustomerBeneficiary(BeneficairyId);
                if (Status.ToUpper().Equals("SUCCESS"))
                {
                    count++;
                }
                else
                {
                    failed++;
                }

            }
            string msg = suc + "Changes Have Been Approved and " + failed + "Failed";
            datafile.LogActivity(Session["UserName"].ToString(), "Approved Beneficiary(ies) Details");
            ShowMessage(msg, false);
        }
        catch(Exception ex)
        {
            throw ex;
        }

    }
    private string GetRecordsToApprove()
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
    protected void chkTransactions_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            chkAll.Visible = true;
            //chkTransactions2.Visible = true;
            SelectAllItems();
            if (chkAll.Checked == true)
            {
                //chkTransactions2.Checked = true;
            }
            else
            {
                //chkTransactions2.Checked = false;
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
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {

            string str = GetRecordsToApprove().TrimEnd(',');
            if (str.Equals(""))
            {
                ShowMessage("Please Select Beneficiaries to Reject", true);
            }
            else
            {
                ProcessRejected(str);
                LoadBeneficaries();
            }
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true); 
        }

    }

    private void ProcessRejected(string str)
    {
        try
        {
            int suc = 0;
            int failed = 0;
            int count = 0;
            int UserId = 0;
            string[] arr = str.Split(',');
            int i = 0;
            for (i = 0; i < arr.Length; i++)
            {
                int BeneficairyId = int.Parse(arr[i].ToString());
                string Status = datafile.RejectCustomerBeneficiary(BeneficairyId);
                if (Status.ToUpper().Equals("SUCCESS"))
                {
                    count++;
                }
                else
                {
                    failed++;
                }

            }
            string msg = suc + "Beneficiaries Have Been Rejected and " + failed + "Failed";
            ShowMessage(msg, false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
