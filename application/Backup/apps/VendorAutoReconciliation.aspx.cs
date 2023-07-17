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

public partial class VendorAutoReconciliation : System.Web.UI.Page
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
                string RoleId = Session["RoleCode"].ToString();
                if (isRoleAuthorisedToVisitPage(RoleId))
                {
                    LoadNetworks();
                    ToggleVendor();
                    LoadVendors();
                    MultiView1.ActiveViewIndex = 0;
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
    private void LoadNetworks()
    {
        dtable = datafile.GetNetworks();
        cboVendor.DataSource = dtable;
        cboVendor.DataValueField = "Network";
        cboVendor.DataTextField = "Network";
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
    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        Button1.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button1, "").ToString());
        Button2.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button2, "").ToString());
    }
    protected void cboVendor_DataBound(object sender, EventArgs e)
    {
        cboVendor.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            string vendorcode = cboVendor.SelectedValue.ToString();
            if (vendorcode.Equals("0"))
            {
                ShowMessage("Please Select Vendor for Reconciliation", true);
            }
            else if (FileUpload1.FileName.Trim().Equals(""))
            {
                ShowMessage("Please Select Browser file to reconcile", true);
            }
            else if (vendorcode.Equals("STANBIC_VAS"))
            {
                ReadVASFileToRecon();
            } 
            else
            {
                ReadFileToRecon(vendorcode);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void ReadVASFileToRecon()
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
            string filePath = bll.ReconFilePath("STANBIC_VAS", filename);
            FileUpload1.SaveAs(filePath);
            ArrayList failedBankTransactions = new ArrayList();
            ArrayList successBankTransactions = new ArrayList();
            bool Status;
            int count = 0;
            int failedRecon = 0;
            int Reconciled = 0;
            string user = Session["Username"].ToString();
            dfile = new DataFileProcess();
            fileContents = dfile.readFile(filePath);
            int Reconcode = CreateReconCode();
            Recontran tran;

            for(int i = 0; i < fileContents.Count; i++)
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
                        DataRow dr = FileVendorRef.NewRow();
                        dr["VendorRef"] = vendorref;
                        dr["Amount"] = Amount;
                        dr["RecordDate"] = RecordDate;
                        FileVendorRef.Rows.Add(dr);
                        FileVendorRef.AcceptChanges();

                        string FoundStatus = bll.CheckIfVasTranExistAtPegasus(vendorref);

                        if (FoundStatus.ToUpper().Equals("PENDING"))
                        {
                            dr["PegasusStatus"] = "FOUND";
                            dr["Deleted"] = "NOT FOUND";
                            dr["Recieved"] = "NOT FOUND";
                            Reconciled++;
                        }
                        else if (FoundStatus.ToUpper().Equals("SUCCESS"))
                        {
                            dr["PegasusStatus"] = "FOUND";
                            dr["Deleted"] = "NOT FOUND";
                            dr["Recieved"] = "FOUND";
                            Reconciled++;
                        }
                        else if (FoundStatus.ToUpper().Equals("COMPLETED"))
                        {
                            dr["PegasusStatus"] = "FOUND";
                            dr["Recieved"] = "NOT FOUND";
                            dr["Deleted"] = "NOT FOUND";
                            Reconciled++;
                        }
                        else if (FoundStatus.ToUpper().Equals("FAILED"))
                        {
                            dr["PegasusStatus"] = "NOT FOUND";
                            dr["Recieved"] = "NOT FOUND";
                            dr["Deleted"] = "FOUND";
                            failedRecon++;
                        }
                        else
                        {
                            dr["PegasusStatus"] = "NOT FOUND";
                            dr["Deleted"] = "NOT FOUND";
                            dr["Recieved"] = "NOT FOUND";
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
                ShowMessage("File of " + Total + " VAS transactions record(s) Reconciliation failed", false);
                DisplayFailed(failedBankTransactions);
            }
            else if (Reconciled == 0)
            {
                ShowMessage("File of " + Total + " VAS transactions record(s) Reconciled Successfully", true);
                DisplayFailed(failedBankTransactions);
            }
            else
            {
                ShowMessage("File of " + Total + " VAS transactions record(s) Processed( Found -" + Reconciled + " Not Found - " + failedRecon + ")", true);
                DisplayFailed(failedBankTransactions);
            }
        }
        else
        {
            ShowMessage("Please Browser CSV File, " + extension + " file not supported", true);
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
                        DataRow dr = FileVendorRef.NewRow();
                        dr["VendorRef"] = vendorref;
                        dr["Amount"] = Amount;
                        dr["RecordDate"] = RecordDate;
                        FileVendorRef.Rows.Add(dr);
                        FileVendorRef.AcceptChanges();
                        string status = "";

                        string FoundStatus = bll.CheckIfExistAtPegasus(vendorref, out status);

                        if (FoundStatus == "Reconciled")
                        {
                            dr["PegasusStatus"] = "FOUND";
                            dr["Deleted"] = "NOT FOUND";
                            dr["Recieved"] = "NOT FOUND";
                            Reconciled++;
                        }
                        else if (FoundStatus == "Recieved")
                        {
                            dr["PegasusStatus"] = "NOT FOUND";
                            dr["Deleted"] = "NOT FOUND";
                            dr["Recieved"] = "FOUND : " + status;
                            failedRecon++;
                        }
                        else if (FoundStatus == "Deleted")
                        {
                            dr["PegasusStatus"] = "NOT FOUND";
                            dr["Recieved"] = "NOT FOUND";
                            dr["Deleted"] = "FOUND : " + status;
                            failedRecon++;
                        }
                        else
                        {
                            dr["PegasusStatus"] = "NOT FOUND";
                            dr["Deleted"] = "NOT FOUND";
                            dr["Recieved"] = "NOT FOUND";
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
        string vendor = cboVendor.SelectedItem.ToString();
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
            string bank = vendor;
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
        rptName = physicalPath + "\\Bin\\reports\\VendorRecon.rpt";
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
