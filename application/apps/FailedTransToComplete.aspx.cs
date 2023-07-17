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
public partial class FailedTransToComplete : System.Web.UI.Page
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
            string RoleId = Session["RoleCode"].ToString();
            if (isRoleAuthorisedToVisitPage(RoleId))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbxConfirm", "doConfirm();", true);
                if (Session["AreaID"].ToString().Equals("1"))//3
                {
                    if (IsPostBack == false)
                    {
                        MultiView1.ActiveViewIndex = -1;
                        MultiView3.ActiveViewIndex = 0;
                        DisableBtnsOnClick();
                    }
                }
                else
                {
                    MultiView1.ActiveViewIndex = -1;
                    ShowMessage("YOU DO NOT HAVE RIGHTS TO USE THIS PAGE", true);
                }
            }
            else
            {
                Response.Redirect("UnauthorisedAccess.aspx");
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




    //private void LoadTransactions()
    //{
    //    if (fromDate.Text.Equals(""))
    //    {
    //        DataGrid1.Visible = false;
    //        ShowMessage("From Date is required", true);
    //        fromDate.Focus();
    //    }
    //    else
    //    {
    //        //variables
    //        DateTime from = bll.ReturnDate(fromDate.Text.Trim(), 1);
    //        DateTime to = bll.ReturnDate(toDate.Text.Trim(), 2);
    //        string bacthNum = batchNo.Text.Trim();
    //        string recorded = recordedBy.Text.Trim();
    //        string stat = status.SelectedValue.ToString();
    //        string customerCod = customerCode.Text.ToString();
    //        dataTable = datapay.GetTransferBatchDetails(bacthNum, recorded, from, to, stat, customerCod);
    //        DataGrid1.CurrentPageIndex = 0;
    //        DataGrid1.DataSource = dataTable;
    //        DataGrid1.DataBind();
    //        if (dataTable.Rows.Count > 0)
    //        {
    //            string rolecode = Session["RoleCode"].ToString();
    //            if (rolecode.Equals("004"))
    //            {
    //                MultiView1.ActiveViewIndex = -1;
    //            }
    //            else
    //            {
    //                MultiView1.ActiveViewIndex = 0;
    //                CalculateTotal(dataTable);
    //            }
    //            DataGrid1.Visible = true;
    //            ShowMessage(".", true);
    //        }
    //        else
    //        {
    //            lblTotal.Text = ".";
    //            DataGrid1.Visible = false;
    //            lblTotal.Visible = false;
    //            MultiView1.ActiveViewIndex = -1;
    //            ShowMessage("No Record found", true);
    //        }
    //    }

    //    MultiView3.ActiveViewIndex = 0;
    //    chkSelect.Checked = false;
    //    CheckBox2.Checked = false;

    //}



    private void LoadTransactions()
    {
        if (fromDate.Text.Equals(""))
        {
            DataGrid1.Visible = false;
            ShowMessage("From Date is required", true);
            fromDate.Focus();
        }
        else
        {
            //variables
            DateTime from = bll.ReturnDate(fromDate.Text.Trim(), 1);
            DateTime to = bll.ReturnDate(toDate.Text.Trim(), 2);
            string bacthNum = batchNo.Text.Trim();
            string recorded = recordedBy.Text.Trim();
            string stat = "FAILED";
            string customerCod = customerCode.Text.ToString();
            dataTable = datapay.GetTransferBatchDetails(bacthNum, recorded, from, to, stat, customerCod);
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
            double amount = double.Parse(dr["Amount"].ToString());
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
                string batchnum = e.Item.Cells[4].Text;
                string paymentnum = e.Item.Cells[10].Text;
                string currentstat = e.Item.Cells[8].Text;
                batchNum.Text = batchnum;
                paymentNum.Text = paymentnum;
                currentStatus.Text = currentstat;
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

    //protected void cboVendor_DataBound(object sender, EventArgs e)
    //{
    //    cboVendor.Items.Insert(0, new ListItem("ALL Agents", "0"));
    //}
    //protected void cboTelecoms_DataBound(object sender, EventArgs e)
    //{
    //    cboTelecoms.Items.Insert(0, new ListItem("Select Telecom", "0"));
    //}
    //protected void cboTranType_DataBound(object sender, EventArgs e)
    //{
    //    cboTranType.Items.Insert(0, new ListItem("All Tran Types", "0"));
    //}
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
            string ret = Process.ResendTransactions(str, Session);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbxConfirm", "doConfirm();", true);
            string batchnum = batchNum.Text;
            string paymentnum = paymentNum.Text;
            if (batchnum.Equals(""))
            {
                ShowMessage("batch number not Set", true);
            }
            else if (paymentnum.Equals(""))
            {
                ShowMessage("payment number not set", true);

            }
            else
            {
                string res = Process.UpdateFailedBatchTran(paymentnum, batchnum);
                if (res.Equals("OK"))
                {
                    ShowMessage("batch transaction completed successfully, you can leave this page", true);
                    txtMark.Enabled = false;
                    txtMark.Text = "Successfully completed transaction";
                    //LoadTransactions();
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
    protected void txtVendorId_TextChanged(object sender, EventArgs e)
    {

    }
}
