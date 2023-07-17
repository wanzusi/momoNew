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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;


public partial class PendTransactions : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    private ReportDocument Rptdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string RoleId = Session["RoleCode"].ToString();
            if (isRoleAuthorisedToVisitPage(RoleId))
            {
                if (Session["AreaID"].ToString().Equals("1"))//3
                {
                    if (IsPostBack == false)
                    {
                        MultiView3.ActiveViewIndex = 0;
                        LoadVendors();
                        if (Session["AreaID"].ToString().Equals("3"))//3
                        {
                            cboVendor.SelectedIndex = cboVendor.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                            cboVendor.Enabled = false;
                        }
                        LoadTranType();
                        LoadALLNetworks();
                        if (Session["AreaID"].ToString().Equals("2"))//2
                        {
                            cboTranType.SelectedIndex = cboTranType.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                            cboTranType.Enabled = false;
                        }
                        LoadPayTypes();
                        MultiView1.ActiveViewIndex = -1;
                        lblTotal.Visible = false;
                        DisableBtnsOnClick();
                    }
                }
                else
                {
                    MultiView1.ActiveViewIndex = -1;
                    ShowMessage("YOU DO NOT HAVE RIGHTS TO VIEW THIS PAGE", true);
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
    public string GetRecordsToPend()
    {
        //System.Windows.Forms.MessageBox.Show("Dot Net Perls is awesome.");
        // MessageBox.Show("Dot Net Perls is awesome.");
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





    private void LoadTranType()
    {
        dtable = datafile.GetTranType();
        cboTranType.DataSource = dtable;
        cboTranType.DataValueField = "TypeId";
        cboTranType.DataTextField = "TranType";
        cboTranType.DataBind();
    }
    private void LoadALLNetworks()
    {
        dtable = datafile.GetNetworkInMobileMoneyDB();
        cboNetwork.DataSource = dtable;
        cboNetwork.DataValueField = "Network";  //Column name of the table.
        cboNetwork.DataTextField = "Network";
        cboNetwork.DataBind();
    }
    private void Page_Unload(object sender, EventArgs e)
    {
        if (Rptdoc != null)
        {
            Rptdoc.Close();
            Rptdoc.Dispose();
            GC.Collect();
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
    private void LoadPayTypes()
    {
        dtable = datafile.GetPayTypes();
        cboPaymentType.DataSource = dtable;
        cboPaymentType.DataValueField = "PaymentCode";
        cboPaymentType.DataTextField = "PaymentType";
        cboPaymentType.DataBind();
    }
    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        //btnConvert.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnConvert, "").ToString());

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
            DataGrid1.Visible = false;
            ShowMessage("From Date is required", true);
            txtfromDate.Focus();
        }
        else
        {
            /// string Phone = cboVendor.SelectedValue.ToString();
            string vendorcode = cboVendor.SelectedValue.ToString();
            string vendorref = txtpartnerRef.Text.Trim();
            string Paymentcode = cboPaymentType.SelectedValue.ToString();
            string FromAccount = txtAccount.Text.Trim();
            string CustName = txtCustName.Text.Trim();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            string network = cboNetwork.SelectedValue.ToString();
            string ToAccount = txtSearch.Text.Trim();
            string TranType = cboTranType.SelectedValue.ToString();
            string telecom = telecomId.Text.ToString();
            // string CustomerTel = phoneNum.Text.Trim();
            if (network.ToUpper() == "AIRTEL" && TranType.ToUpper() == "1")
            {
                TranType = "PULL";
                dataTable = datapay.GetAirtelB2WTransactions(vendorcode, FromAccount, fromdate, todate, TranType, network, "", vendorref);

            }
            else
            {
                dataTable = datapay.GetPendAllTransactions(vendorcode, vendorref, FromAccount, CustName, Paymentcode, ToAccount, fromdate, todate, TranType, network, telecom);

            }
            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                string rolecode = Session["RoleCode"].ToString();
                if (rolecode.Equals("004"))
                {
                    MultiView1.ActiveViewIndex = -1;
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;
                    CalculateTotal(dataTable);
                }
                DataGrid1.Visible = true;
                ShowMessage(".", true);
            }
            else
            {
                lblTotal.Text = ".";
                DataGrid1.Visible = false;
                lblTotal.Visible = false;
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
        string rolecode = Session["RoleCode"].ToString();
        if (rolecode.Equals("004"))
        {
            lblTotal.Visible = false;
        }
        else
        {
            lblTotal.Visible = true;
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

            if (e.CommandName != "Page")
            {
                // int intIndex = Convert.ToInt32(e.CommandArgument);
                //  string id = GridData.Rows[intIndex].Cells[1].Text.ToString();
                string id = e.Item.Cells[1].Text.ToString();
                Response.Redirect("./updateTransaction.aspx?Id=" + id, false);
            }
            //if (e.CommandName == "btnEdit")
            //{
            //    string Status = e.Item.Cells[0].Text;
            //    string Amount = e.Item.Cells[9].Text;
            //    //txtAmount.Text = Amount;
            //    // txtVendorId.Text = PegPayId;
            //    MultiView2.ActiveViewIndex = 1;
            //    MultiView1.ActiveViewIndex = -1;
            //    MultiView3.ActiveViewIndex = -1;
            //}
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
            string vendorcode = cboVendor.SelectedValue.ToString();
            string vendorref = txtpartnerRef.Text.Trim();
            string Paymentcode = cboPaymentType.SelectedValue.ToString();
            string Account = txtAccount.Text.Trim();
            string CustName = txtCustName.Text.Trim();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            string network = cboNetwork.SelectedValue.ToString();
            string teller = txtSearch.Text.Trim();
            string utility = cboTranType.SelectedValue.ToString();
            dataTable = datapay.GetTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, teller, fromdate, todate, utility, network);
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }

    protected void cboVendor_DataBound(object sender, EventArgs e)
    {
        cboVendor.Items.Insert(0, new ListItem("All Agents", "0"));
    }
    protected void cboTranType_DataBound(object sender, EventArgs e)
    {
        cboTranType.Items.Insert(0, new ListItem("All Tran Types", "0"));
    }
    protected void cboPaymentType_DataBound(object sender, EventArgs e)
    {
        cboPaymentType.Items.Insert(0, new ListItem("All Payment Types", "0"));
    }
    protected void cboNetwork_DataBound(object sender, EventArgs e)
    {
        cboNetwork.Items.Insert(0, new ListItem("All Network Types", "0"));
    }
    protected void btnConvert_Click(object sender, EventArgs e)
    {
        //try
        //{
        ConvertToFile();
        //}
        //catch (Exception ex)
        //{
        //    ShowMessage(ex.Message, true);
        //}
    }

    private void ConvertToFile()
    {
        //if (rdExcel.Checked.Equals(false) && rdPdf.Checked.Equals(false))
        //{
        //    ShowMessage("Please Check file format to Convert to", true);
        //}
        //else
        //{
        //    LoadRpt();
        //   // try{
        //    if (rdPdf.Checked.Equals(true))
        //    {
        //        Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "TRANSACTIONS");

        //    }
        //    else
        //    {
        //        Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "TRANSACTIONS");

        //    }
        ////}
        ////catch (Exception ex)
        ////{
        ////    //do nothing
        ////} 
        //}
    }
    private void LoadRpt()
    {
        string vendorcode = cboVendor.SelectedValue.ToString();
        string vendorref = txtpartnerRef.Text.Trim();
        string Paymentcode = cboPaymentType.SelectedValue.ToString();
        string Account = txtAccount.Text.Trim();
        string CustName = txtCustName.Text.Trim();
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        string network = cboNetwork.SelectedValue.ToString();
        string teller = txtSearch.Text.Trim();
        string utility = cboTranType.SelectedValue.ToString();
        dataTable = datapay.GetTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, teller, fromdate, todate, utility, network);
        dataTable = formatTable(dataTable);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);

        rptName = physicalPath + "\\Bin\\Reports\\TransReport.rpt";

        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dataTable);
        CrystalReportViewer1.ReportSource = Rptdoc;
        //Rptdoc.PrintToPrinter(1,true, 0,0);
    }

    private DataTable formatTable(DataTable dataTable)
    {
        DataTable formedTable;

        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);

        string agent_code = cboVendor.SelectedValue.ToString();
        string agent_name = cboVendor.SelectedItem.ToString();
        string Header = "";
        if (agent_code.Equals("0"))
        {
            Header = "ALL AGENTS TRANSACTION(S) BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
        }
        else
        {
            Header = agent_name + " TRANSACTION(S) BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
        }
        string Printedby = "Printed By : " + Session["FullName"].ToString();
        DataColumn myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "DateRange";
        dataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "PrintedBy";
        dataTable.Columns.Add(myDataColumn);

        // Add data to the new columns

        dataTable.Rows[0]["DateRange"] = Header;
        dataTable.Rows[0]["PrintedBy"] = Printedby;
        formedTable = dataTable;
        return formedTable;
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


    public int CountRecordsToPend()
    {
        //System.Windows.Forms.MessageBox.Show("Dot Net Perls is awesome.");
        // MessageBox.Show("Dot Net Perls is awesome.");
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
        return Count;
    }



    protected void pendTransaction_Click(object sender, EventArgs e)
    {


        try
        {
            string str = GetRecordsToPend().TrimEnd(',');
            int count = CountRecordsToPend();

            pendTransaction.Enabled = true;
            // pendTransaction.Text = "PROCESSING......";
            string ret = Process.PendTransaction(str);
            if (ret.Contains("SUCCESSFUL"))
            {
                LoadTransactions();
                ShowMessage(ret, false);
                pendTransaction.Enabled = true;
                pendTransaction.Text = "Pend transactions";
            }
            else
            {
                //LoadTransactions();
                ShowMessage(ret, true);
                pendTransaction.Enabled = true;
                pendTransaction.Text = "Pend transactions";
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            chkAll.Visible = true;
            //chkTransactions2.Visible = true;
            SelectAllItems();
            if (chkAll.Checked == true)
            {
                chkAll.Checked = true;
            }
            else
            {
                chkAll.Checked = false;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void DataGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
