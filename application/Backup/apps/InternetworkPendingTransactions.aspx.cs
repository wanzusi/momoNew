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
public partial class InternetworkPendingTransactions : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    DataLogin dh = new DataLogin();

    string username = "";
    string fullname = "";
    string userBranch = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            username = Session["UserName"] as string;
            fullname = Session["FullName"] as string;
            userBranch = Session["UserBranch"] as string;

            if (IsPostBack == false)
            {
                string RoleId = Session["RoleCode"].ToString();
                if (isRoleAuthorisedToVisitPage(RoleId))
                {
                    if (userBranch.ToUpper() == "PEGPAY")
                    {
                        MultiView1.ActiveViewIndex = -1;
                        MultiView3.ActiveViewIndex = 0;
                        LoadVendors();
                        DisableBtnsOnClick();
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
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
    private void LoadVendors()
    {
        //cboVendor.Items.Add(new ListItem("All Networks", ""));
        //for now lets do mtn only,
        //airtel is always retried
        cboVendor.Items.Add(new ListItem("ALL", ""));
        cboVendor.Items.Add(new ListItem("MTN", "MTN"));
        cboVendor.Items.Add(new ListItem("AIRTEL", "AIRTEL"));
        cboVendor.Enabled = true;

        ddStatus.Items.Add(new ListItem("ALL", ""));
        ddStatus.Items.Add(new ListItem("PROCESSING", "PROCESSING"));
        ddStatus.Items.Add(new ListItem("SUCCESS", "SUCCESS"));
        ddStatus.Items.Add(new ListItem("FAILED", "FAILED"));
        ddStatus.Items.Add(new ListItem("PENDING", "PENDING"));
        ddStatus.Enabled = true;
        //cboVendor.Items.Add(new ListItem("AIRTEL", "AIRTEL"));
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
        if (txtfromDate.Text.Equals(""))
        {
            dataGridResults.Visible = false;
            ShowMessage("From Date is required", true);
            txtfromDate.Focus();
        }
        else
        {
            SearchDb();
        }
    }

    private void SearchDb()
    {
        //string network = cboVendor.SelectedValue.ToString();
        string status = ddStatus.SelectedValue.ToString();
        //string isRetried = "";
        //string vendorref = txtpartnerRef.Text.Trim();
        //DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        //DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        //string phonenumber = phone.Text.Trim();

        //dataTable = datapay.SearchInterNetworkTransactions(network, vendorref, fromdate, todate, isRetried, phonenumber, status);
        string[] searchParams = GetSearchParameters();
        dataTable = dh.ExecuteDataSet("SearchInterNetworkTransactionsModified", searchParams).Tables[0];
            
        if (dataTable.Rows.Count > 0)
        {
            DataTable dt = dataTable.Clone();
            foreach (DataRow dr in dataTable.Rows)
            {
                dt.Rows.Add(dr.ItemArray);
            }
            string rolecode = Session["RoleCode"].ToString();
            if (rolecode.Equals("004"))
            {
                MultiView1.ActiveViewIndex = -1;
            }
            else
            {
                MultiView1.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = 0;
                CalculateTotal(dt);
            }

            dataGridResults.DataSource = dt;
            dataGridResults.DataBind();
            dataGridResults.Visible = true;
            ShowMessage(".", true);
        }
        else
        {
            lblTotal.Text = ".";
            dataGridResults.Visible = false;
            lblTotal.Visible = false;
            MultiView1.ActiveViewIndex = -1;
            ShowMessage("No Record found", true);
        }
    }

    private string[] GetSearchParameters()
    {
        List<string> searchCriteria = new List<string>();

        string network = cboVendor.SelectedValue.ToString();
        string status = ddStatus.SelectedValue.ToString();
        string isRetried = "";
        string VendorId = txtpartnerRef.Text.Trim();
        string FromDate = txtfromDate.Text.Trim();
        string ToDate = txttoDate.Text.Trim();
        string phonenumber = phone.Text.Trim();

        searchCriteria.Add(network);
        searchCriteria.Add("");//Telecom
        searchCriteria.Add("");//table
        searchCriteria.Add(isRetried);
        searchCriteria.Add(status);
        searchCriteria.Add(phonenumber);
        searchCriteria.Add(VendorId);
        searchCriteria.Add("");//TelecomId
        searchCriteria.Add(FromDate);
        searchCriteria.Add(ToDate);

        return searchCriteria.ToArray();
    }

    private void CalculateTotal(DataTable Table)
    {
        double total = 0;
        foreach (DataRow dr in Table.Rows)
        {
            double amount = double.Parse(dr["Amount"].ToString());
            total += amount;
        }
        lblTotal.Text = "Total Amount of Transactions [" + total.ToString("#,##0") + "]";
    }

    private void LoadUsers()
    {

        cboVendor.DataSource = dataTable;
        cboVendor.DataBind();
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
            dataGridResults.CurrentPageIndex = e.NewPageIndex;
            SearchDb();
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


    private void SelectAllItems()
    {
        foreach (DataGridItem Items in dataGridResults.Items)
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
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }

    protected void btnReverse_Click(object sender, EventArgs e)
    {
        try
        {
            string idList = GetRecordsToAction().TrimEnd(',');

            string[] ids = idList.Split(',');

            foreach (string id in ids)
            {
                ReverseTxn(id);
            }
            
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void ReverseTxn(string transaction)
    {
        if (string.IsNullOrEmpty(transaction))
        {
            ShowMessage("Check atleast one record", true);
        }
        else
        {
            string[] data = transaction.Split(':');
            string table = data[0];
            string vendorid = data[1];
            string telecomid = HttpUtility.HtmlDecode(data[2]);
            string reason = data[3];

            string pegpayId = GetPegPayId(vendorid);

            btnResend.Enabled = false;
            btnResend.Text = "PROCESSING......";
            dh.ExecuteDataSet("TransferFromRecievedToFailedInteropTransaction", pegpayId, reason, telecomid);
            bll.InsertIntoAuditLog(vendorid, "UPDATE", "Reverse Internetwork Transaction", userBranch, username, "InternetworkPendingTransactions.aspx",
                fullname + " moved the interoperability transaction with id: " + vendorid + " from the received table to the failed table " + DateTime.Now.ToString());

            ShowMessage("Transaction reversed. Check status in all transactions", false);
            
            btnReverse.Enabled = true;
            btnReverse.Text = "REVERSE TRANSACTION(S)";
        }
    }

    private string GetPegPayId(string vendorid)
    {
        string network = cboVendor.SelectedValue.ToString();
        string status = ddStatus.SelectedValue.ToString();
        string isRetried = "";
        string vendorref = vendorid;//txtpartnerRef.Text.Trim();
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        string phonenumber = phone.Text.Trim();

        dataTable = datapay.SearchInterNetworkTransactions(network, vendorref, fromdate, todate, isRetried, phonenumber, status);
        return dataTable.Rows[0]["RecordId"].ToString();
    }

    private string GetRecordsToAction()
    {
        int Count = 0;
        string ItemArr = "";
        foreach (DataGridItem Items in dataGridResults.Items)
        {
            CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
            if (chk.Checked)
            {
                Count++;
                string ItemFound = Items.Cells[1].Text + ":" + Items.Cells[2].Text + ":" + Items.Cells[8].Text.Trim() + ":" + Items.Cells[12].Text;
                ItemArr = ItemArr += ItemFound + ",";
            }
        }
        return ItemArr;
    }

    protected void btnResend_Click(object sender, EventArgs e)
    {
        try
        {
            string idList = GetRecordsToAction().TrimEnd(',');

            string[] ids = idList.Split(',');

            foreach (string id in ids)
            {
                ResendTxn(id);
            }
            LoadTransactions();
            CheckBox2.Checked = false;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void ResendTxn(string transaction)
    {
        try
        {
            string[] data = transaction.Split(':');
            string table = data[0];
            string vendorid = data[1];

            btnResend.Enabled = false;
            btnResend.Text = "PROCESSING......";
            dh.ExecuteDataSet("ResendInterworkTxn2", vendorid);
            bll.InsertIntoAuditLog(vendorid, "UPDATE", "Resend Internetwork Transaction", userBranch, username, "InternetworkPendingTransactions.aspx",
                fullname + " resent the interoperability transaction with id: " + vendorid + " at " + DateTime.Now.ToString());
            
            ShowMessage("Transaction resent. Check status in all transactions", false);
            btnResend.Enabled = true;
            btnResend.Text = "RESEND TRANSACTION(S)";
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
   
}
