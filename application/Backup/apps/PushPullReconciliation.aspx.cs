using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;

public partial class AutoReconciliation : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    DataTable FileVendorRef = new DataTable();
    private ReportDocument Rptdoc = new ReportDocument();

    DataFileProcess dfile = new DataFileProcess();
    private ArrayList fileContents;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                LoadNetworks();
                ToggleVendor();
                LoadVendors();
                MultiView1.ActiveViewIndex = 0;
                Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                Button MenuReport = (Button)Master.FindControl("btnCalReports");
                Button MenuRecon = (Button)Master.FindControl("btnAccounts");
                Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                MenuTool.Font.Underline = false;
                MenuPayment.Font.Underline = false;
                MenuReport.Font.Underline = false;
                MenuRecon.Font.Underline = true;
                MenuAccount.Font.Underline = false;
                MenuBatching.Font.Underline = false;
                DisableBtnsOnClick();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void ToggleVendor()
    {
        string districtcode = Session["DistrictCode"].ToString();
        string role = Session["RoleCode"].ToString();
        if (role.Equals("005"))
        {
            //cboVendor.Enabled = false;
            //cboVendor.SelectedIndex = cboVendor.Items.IndexOf(cboVendor.Items.FindByValue(districtcode));
        }
        else
        {
            //cboVendor.Enabled = true;
        }
    }

    private void LoadVendors()
    {
        //dtable = datafile.GetAllVendors("0");
        dtable = datafile.GetSystemCompanies("", "");
        //cboVendor.DataSource = dtable;
        //cboVendor.DataValueField = "CompanyCode";
        //cboVendor.DataTextField = "Company";
        //cboVendor.DataBind();
    }
    private void LoadNetworks()
    {
        dtable = datafile.GetNetworks();
        ///cboVendor.DataSource = dtable;
        //cboVendor.DataValueField = "Network";
        //cboVendor.DataTextField = "Network";
        //cboVendor.DataBind();
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
    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        Button1.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button1, "").ToString());
        Button2.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button2, "").ToString());
    }
    protected void cboVendor_DataBound(object sender, EventArgs e)
    {
        // cboVendor.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.FileName.Trim().Equals(""))
            {
                ShowMessage("Please Browser file to reconcile", true);
            }
            else
            {
                ReadFileToRecon("");
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private int CreateReconCode()
    {
        int output = 0;
        string createdby = Session["Username"].ToString();
        output = datapay.SaveReconBatch(0, 0, 0, 0, createdby);
        return output;
    }
    private void ReadFileToRecon(string vendorcode)
    {
        string filename = Path.GetFileName(FileUpload1.FileName);
        string extension = Path.GetExtension(filename);


        FileVendorRef.Columns.Add("VendorRef", typeof(String));
        FileVendorRef.Columns.Add("Amount", typeof(String));
        FileVendorRef.Columns.Add("RecordDate", typeof(String));
        FileVendorRef.Columns.Add("PegasusStatus", typeof(String));
        FileVendorRef.Columns.Add("Recieved", typeof(String));
        FileVendorRef.Columns.Add("Deleted", typeof(String));
        //FileVendorRef.Columns.Add("Purchases", typeof(Double));

        if (extension.ToUpper().Equals(".CSV") || extension.ToUpper().Equals(".TXT"))
        {
            string filePath = bll.ReconFilePath(vendorcode, filename);
            FileUpload1.SaveAs(filePath);
            ArrayList failedBankTransactions = new ArrayList();
            bool Status;
            int count = 0;
            int failedRecon = 0;
            int Reconciled = 0;
            string user = Session["Username"].ToString();
            dfile = new DataFileProcess();
            fileContents = dfile.readFile(filePath);
            int Reconcode = CreateReconCode();
            Recontran tran;
            for (int i = 0; i < fileContents.Count; i++)
            {
                count++;
                string line = fileContents[i].ToString();
                string[] sLine = line.Split(',');
                {
                    tran = new Recontran();
                    if (sLine.Length == 3)
                    {
                        string vendorref = sLine[0].Trim();
                        string Amount = sLine[1].Trim();
                        string RecordDate = sLine[2].Trim();
                        string[] splitId = null;

                        DataRow dr = FileVendorRef.NewRow();
                        if (vendorref.Contains("USSD"))
                        {
                            splitId = vendorref.Split('_');
                            dr["VendorRef"] = splitId[2];
                            vendorref = splitId[2];
                        }
                        else
                        {
                            dr["VendorRef"] = vendorref;
                        }
                        dr["Amount"] = Amount;
                        dr["RecordDate"] = RecordDate;

                        FileVendorRef.Rows.Add(dr);
                        FileVendorRef.AcceptChanges();

                        string FoundStatus = bll.CheckIfExistAtPegasusPushPull(vendorref);

                        if (FoundStatus == "COMPLETED" || FoundStatus == "SUCCESS")
                        {
                            dr["PegasusStatus"] = "SUCCESS";
                            dr["Deleted"] = "YES";
                            dr["Recieved"] = "YES";
                            Reconciled++;
                        }
                        else if (FoundStatus == "FAILED")
                        {
                            dr["PegasusStatus"] = "FAILED";
                            dr["Deleted"] = "YES";
                            dr["Recieved"] = "YES";
                            Reconciled++;
                            //failedRecon++;
                        }
                        else
                        {
                            dr["PegasusStatus"] = "NO";
                            dr["Deleted"] = "NO";
                            dr["Recieved"] = "NO";
                            failedRecon++;
                        }

                    }
                    else
                    {
                        CancelRecnBatch(Reconcode);
                        throw new Exception("File Format is not OK, Columns must be 1... and not " + sLine.Length.ToString());
                    }
                }

            }
            int Total = Reconciled + failedRecon;
            if (failedRecon == 0)
            {
                ShowMessage("File of " + Total + " record(s) Reconciled Successfully", false);
                DisplayFailed(failedBankTransactions);
            }
            else if (Reconciled == 0)
            {
                ShowMessage("File of " + Total + " record(s) Reconciliation failed", true);
                DisplayFailed(failedBankTransactions);
            }
            else
            {
                ShowMessage("File of " + Total + " record(s) Processed( Found -" + Reconciled + " Not Found - " + failedRecon + ")", true);
                DisplayFailed(failedBankTransactions);
            }
        }
        else
        {
            ShowMessage("Please Browser CSV File, " + extension + " file not supported", true);
        }
    }

    private string GetPayDate(string pay_date)
    {
        string res = "";
        try
        {
            string[] str_date = pay_date.Split('/');
            if (str_date.Length.Equals(3))
            {
                string dd = str_date[0].ToString();
                string mm = str_date[1].ToString();
                string yy = str_date[2].ToString();
                int dd_len = dd.Length;
                int mm_len = mm.Length;
                int yy_len = yy.Length;
                if (dd_len.Equals(1))
                {
                    dd = "0" + dd;
                    dd_len = dd.Length;
                }

                if (dd_len.Equals(2) && mm_len.Equals(2) && yy_len.Equals(4))
                {
                    res = dd + "/" + mm + "/" + yy;
                }
                else
                {
                    throw new Exception("Wrong Payment Date");
                }
            }
            else
            {
                throw new Exception("Wrong Payment Date");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Wrong Payment Date");
        }
        return res;
    }
    private void DisplayFailed(ArrayList failedTransactions)
    {
        try
        {
            //    DataTable dt = FileVendorRef; //bll.GetFailedReconTable();
            //    Recontran recontran = new Recontran();
            //    for (int i = 0; i < failedTransactions.Count; i++)
            //    {
            //        recontran = (Recontran)failedTransactions[i];
            //        DataRow dr = dt.NewRow();
            //        //dr["No."] = i + 1;
            //        dr["VendorRef"] = recontran.VendorRef;
            //        dr["PayDate"] = recontran.PayDate;
            //        dr["TransactionAmount"] = recontran.TransAmount.ToString("#,##0");
            //        dr["Reason"] = recontran.Reason;
            //        dt.Rows.Add(dr);

            //        dt.AcceptChanges();
            //    }
            LoadFailedGrid(FileVendorRef);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadFailedGrid(DataTable FileVendorRefs)
    {
        MultiView1.ActiveViewIndex = 1;
        DataGrid1.DataSource = FileVendorRefs;
        DataGrid1.DataBind();
        //string filePath = CallFilling(dt);
        //foreach (DataRow dr in dac.GetEmailSubscribers().Rows)
        //{
        //    string Email = dr["Email"].ToString();
        //    string FName = dr["FullName"].ToString();
        //    string Msg = "Hello " + " " + FName + "\nPlease find attached, \n Failed Reconciliations for Vendor : " + cboBank.SelectedItem.ToString();
        //    string Subject = "Failed Reconciliations for Vendor: " + cboBank.SelectedItem.ToString();
        //    string Sender = Session["UserName"].ToString();
        //    bll.SendMail(Email, Msg, Sender, Subject, filePath);
        //}     
    }

    private void CancelRecnBatch(int Reconcode)
    {
        datapay.CancelReconBatch(Reconcode);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        ShowMessage(".", true);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            string fileFormat = GetFileformat();
            if (fileFormat.Equals("NONE"))
            {
                ShowMessage("Please Select File Format", true);
            }
            else
            {
                ShowMessage(".", true);
                ConvertToPdf(fileFormat);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
        finally
        {
            Rptdoc.Dispose();
        }
    }

    private string GetFileformat()
    {
        string res = "NONE";
        if (rdPdf.Checked == false && rdExcel.Checked == false)
        {
            res = "NONE";
        }
        else
        {
            if (rdPdf.Checked == true)
            {
                res = "1";
            }
            else
            {
                res = "2";
            }
        }
        return res;
    }

    private void ConvertToPdf(string fileformat)
    {
        dataTable = GetGridRptTable();
        LoadReport(dataTable, fileformat);
    }
    private DataTable GetGridRptTable()
    {
        DataTable dtble = GetFailedRptDataTable();
        //string vendor = cboVendor.SelectedItem.ToString();
        foreach (DataGridItem Items in DataGrid1.Items)
        {
            DataRow dr = dtble.NewRow();
            //string No = Items.Cells[0].Text;
            string VendorRef = Items.Cells[0].Text;
            string RecordDate = Items.Cells[1].Text;
            string Amount = Items.Cells[2].Text;
            string Status = Items.Cells[3].Text;
            string Recived = Items.Cells[4].Text;
            string Deleted = Items.Cells[5].Text;
            string bank = "";
            string PrintedBy = Session["FullName"].ToString();
            ///////
            //dr["No."] = No;
            dr["VendorRef"] = VendorRef;
            dr["Amount"] = Amount;
            dr["PayDate"] = RecordDate;
            dr["Reason"] = Status;
            dr["Bank"] = "";
            dr["PrintedBy"] = PrintedBy;
            dr["Str1"] = Recived;
            dr["Str2"] = Deleted;

            dtble.Rows.Add(dr);
            dtble.AcceptChanges();
        }
        return dtble;
    }
    private DataTable GetFailedRptDataTable()
    {
        DataTable dt = new DataTable("Table2");
        // dt.Columns.Add("No.");
        dt.Columns.Add("VendorRef");
        dt.Columns.Add("PayDate");
        dt.Columns.Add("Amount");
        dt.Columns.Add("Reason");
        dt.Columns.Add("Bank");
        dt.Columns.Add("PrintedBy");
        dt.Columns.Add("Str1");
        dt.Columns.Add("Str2");
        return dt;
    }
    private void LoadReport(DataTable dataTable, string fileformat)
    {
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);
        rptName = physicalPath + "\\Bin\\reports\\PushPullRecon.rpt";
        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dataTable);
        CrystalReportViewer1.ReportSource = Rptdoc;
        Response.Buffer = false;
        Response.ClearContent();
        Response.ClearHeaders();
        if (fileformat.Equals("1"))
        {
            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "VENDOR RECONCILIATION REPORT");
        }
        else
        {
            Rptdoc.ExportToHttpResponse(ExportFormatType.Excel, Response, true, "VENDOR RECONCILIATION REPORT");
        }
        ShowMessage(".", true);
    }
}
