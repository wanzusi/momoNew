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

public partial class DisonPushPull : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    DataTable FileVendorRef = new DataTable();
    DataTable MTNFileDataTable = new DataTable();
    DataTable BANKFileDataTable = new DataTable();
    DataTable MTNBANKFileDataTable = new DataTable();
    private ReportDocument Rptdoc = new ReportDocument();

    DataFileProcess dfile = new DataFileProcess();
    //private ArrayList fileContents;
    private ArrayList mtnfileContents;
    private ArrayList bankfileContents;
    protected void Page_Load(object sender, EventArgs e)
    {
        
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
                ShowMessage("Please Browser MTN file to reconcile", true);
            }
            else if (FileUpload2.FileName.Trim().Equals(""))
            {
                ShowMessage("Please Browser BANK file to reconcile", true);
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
        string mtnfilename = Path.GetFileName(FileUpload1.FileName);
        string mtnfilenameextension = Path.GetExtension(mtnfilename);

        string bankfilename = Path.GetFileName(FileUpload2.FileName);
        string bankfilenameextension = Path.GetExtension(bankfilename);


        MTNFileDataTable.Columns.Add("VendorRef", typeof(String));
        MTNFileDataTable.Columns.Add("Amount", typeof(String));
        MTNFileDataTable.Columns.Add("RecordDate", typeof(String));
        

        /////////////////////////////////////////////////////////////
        BANKFileDataTable.Columns.Add("VendorRef", typeof(String));
        BANKFileDataTable.Columns.Add("Amount", typeof(String));
        BANKFileDataTable.Columns.Add("RecordDate", typeof(String));
       



        if (mtnfilenameextension.ToUpper().Equals(".CSV") || mtnfilenameextension.ToUpper().Equals(".TXT"))
        {
            string mtnfilePath = bll.ReconFilePath(vendorcode, mtnfilename);
            FileUpload1.SaveAs(mtnfilePath);
            ArrayList failedBankTransactions = new ArrayList();

          
            int count = 0;
           

            string user = Session["Username"].ToString();

            //Read CSV MTN FILE PATH
            dfile = new DataFileProcess();

            mtnfileContents = dfile.readFile(mtnfilePath);

            int Reconcode = CreateReconCode();
            Recontran tran;
            for (int i = 0; i < mtnfileContents.Count; i++)
            {
                count++;
                string line = mtnfileContents[i].ToString();
                string[] sLine = line.Split(',');
                {
                    tran = new Recontran();
                    if (sLine.Length == 3)
                    {
                        string vendorref = sLine[0].Trim();
                        string Amount = sLine[1].Trim();
                        string RecordDate = sLine[2].Trim();
                        string[] splitId = null;

                        DataRow dr = MTNFileDataTable.NewRow();
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

                        MTNFileDataTable.Rows.Add(dr);
                        MTNFileDataTable.AcceptChanges();



                    }
                }
            }
        }
        else
        {
            ShowMessage("Please Browser CSV File, " + mtnfilenameextension + " file not supported", true);
        }

        if (bankfilenameextension.ToUpper().Equals(".CSV") || bankfilenameextension.ToUpper().Equals(".TXT"))
        {
            string filePath = bll.ReconFilePath(vendorcode, bankfilename);
            FileUpload2.SaveAs(filePath);
            ArrayList failedBankTransactions = new ArrayList();

            
            int count = 0;
            

            string user = Session["Username"].ToString();

            //Read CSV MTN FILE PATH
            dfile = new DataFileProcess();

            bankfileContents = dfile.readFile(filePath);

            int Reconcode = CreateReconCode();
            Recontran tran;
            for (int i = 0; i < bankfileContents.Count; i++)
            {
                count++;
                string line = bankfileContents[i].ToString();
                string[] sLine = line.Split(',');
                {
                    tran = new Recontran();
                    if (sLine.Length == 3)
                    {
                        string vendorref = sLine[0].Trim();
                        string Amount = sLine[1].Trim();
                        string RecordDate = sLine[2].Trim();
                        string[] splitId = null;

                        DataRow dr = BANKFileDataTable.NewRow();
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

                        BANKFileDataTable.Rows.Add(dr);
                        BANKFileDataTable.AcceptChanges();



                    }
                }
            }
        }
        else
        {
            ShowMessage("Please Browser CSV File, " + bankfilenameextension + " file not supported", true);
        }

        GetMatches(MTNFileDataTable,BANKFileDataTable);
    }
    public void GetMatches(DataTable mtntable1, DataTable banktable2)
         {
             int failedRecon = 0;
             int Reconciled = 0;
             
             MTNBANKFileDataTable.Columns.Add("MTNVendorRef", typeof(String));
             MTNBANKFileDataTable.Columns.Add("BANKVendorRef", typeof(String));
             MTNBANKFileDataTable.Columns.Add("MTNBANKVendorRefStatus", typeof(String));
             
               DataSet set = new DataSet();

    //wrap the tables in a DataSet.
    set.Tables.Add(mtntable1);
    set.Tables.Add(banktable2);

    //Creates a ForeignKey like Join between two tables.
    //Table1 will be the parent. Table2 will be the child.
    DataRelation relation = new DataRelation("IdJoin", mtntable1.Columns[0], banktable2.Columns[0], false);

    //Have the DataSet perform the join.
    set.Relations.Add(relation);

    int found = 0;

    //Loop through table1 without using LINQ.
    for(int i = 0; i < mtntable1.Rows.Count; i++)
    {
        //If any rows in Table2 have the same Id as the current row in Table1
        if (mtntable1.Rows[i].GetChildRows(relation).Length > 0)
        {
            //Add a counter
            found++;

            //For debugging, proof of match:
            //Get the id's that matched.
            string mtnfileVendorRef = mtntable1.Rows[i][0].ToString();

            string bankfileVendorRef = mtntable1.Rows[i].GetChildRows(relation)[0][0].ToString();

            
                
                
            
            DataRow dr = MTNBANKFileDataTable.NewRow();
            dr["MTNVendorRef"] = mtnfileVendorRef;
            dr["BANKVendorRef"] = bankfileVendorRef;
            dr["MTNBANKVendorRefStatus"]="SUCCESS";
            MTNBANKFileDataTable.Rows.Add(dr);
            MTNBANKFileDataTable.AcceptChanges();
            Reconciled++;

        }
        else {

            string mtnfileVendorRefFailed = mtntable1.Rows[i][0].ToString();
                DataRow dr = MTNBANKFileDataTable.NewRow();
                dr["MTNVendorRef"] = mtnfileVendorRefFailed;
                dr["BANKVendorRef"] = "------";
               dr["MTNBANKVendorRefStatus"]="FAILED";
              MTNBANKFileDataTable.Rows.Add(dr);
            MTNBANKFileDataTable.AcceptChanges();
                failedRecon++;
                found++;
            
        
        
        }
    }

    ArrayList failedBankTransactions = new ArrayList();
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

    //return found;
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
            
            LoadFailedGrid(MTNBANKFileDataTable);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadFailedGrid(DataTable MTNBANKFileDataTable)
    {
        MultiView1.ActiveViewIndex = 1;
        DataGrid1.DataSource = MTNBANKFileDataTable;
        DataGrid1.DataBind();
            
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
        MTNBANKFileDataTable = GetGridRptTable();
        LoadReport(MTNBANKFileDataTable, fileformat);
    }
    private DataTable GetGridRptTable()
    {
        DataTable dtble = GetFailedRptDataTable();
        //string vendor = cboVendor.SelectedItem.ToString();
        foreach (DataGridItem Items in DataGrid1.Items)
        {
            DataRow dr = dtble.NewRow();
            //string No = Items.Cells[0].Text;
            string MTNVendorRef = Items.Cells[0].Text;
            string BANKVendorRef = Items.Cells[1].Text;
            string MTNBANKVendorRefStatus = Items.Cells[2].Text;
            //string Status = Items.Cells[3].Text;
            //string Recived = Items.Cells[4].Text;
            //string Deleted = Items.Cells[5].Text;
            //string bank = "";
            //string PrintedBy = Session["FullName"].ToString();
            ///////
            //dr["No."] = No;
            //dr["MTNVendorRef"] = MTNVendorRef;
            //dr["BANKVendorRef"] = BANKVendorRef;
            //dr["MTNBANKVendorRefStatus"] = MTNBANKVendorRefStatus;
            dr["VendorRef"] = MTNVendorRef;
            dr["Amount"] = BANKVendorRef;
            //dr["PayDate"] = RecordDate;
            dr["Reason"] = MTNBANKVendorRefStatus;
            //dr["Reason"] = Status;
            //dr["Bank"] = "";
            //dr["PrintedBy"] = PrintedBy;
            //dr["Str1"] = Recived;
            //dr["Str2"] = Deleted;

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
        dt.Columns.Add("Amount");
        dt.Columns.Add("Reason");
        //dt.Columns.Add("Reason");
        //dt.Columns.Add("Bank");
        //dt.Columns.Add("PrintedBy");
        //dt.Columns.Add("Str1");
        //dt.Columns.Add("Str2");
        return dt;
    }
    private void LoadReport(DataTable dataTable, string fileformat)
    {
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);
        rptName = physicalPath + "\\Bin\\reports\\MtnBankRecon.rpt";
        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dataTable);
        CrystalReportViewer1.ReportSource = Rptdoc;
        Response.Buffer = false;
        Response.ClearContent();
        Response.ClearHeaders();
        if (fileformat.Equals("1"))
        {
            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "MTN BANK REPORT");
        }
        else
        {
            Rptdoc.ExportToHttpResponse(ExportFormatType.Excel, Response, true, "MTN BANK REPORT");
        }
        ShowMessage(".", true);
    }
}
