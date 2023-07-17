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
using System.Text;
using System.Collections.Generic;

public partial class Pegasus_ClientReconcilation : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    DataTable FileVendorRef = new DataTable();
    DataTable PegasusFileDataTable = new DataTable();
    DataTable ClientFileDataTable = new DataTable();

    DataTable PegasusClientDataTable = new DataTable();
    private ReportDocument Rptdoc = new ReportDocument();
    DataTable PegasusDataTable = new DataTable();
    DataTable ClientDataTable = new DataTable();
    DataTable myTable = new DataTable();
    string pegasusfileuploadedpath = "";
    string clientfileuploadedpath = "";
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
       // Button1.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button1, "").ToString());
        //Button2.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button2, "").ToString());
    }
    
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.FileName.Trim().Equals(""))
            {
                ShowMessage("Please Browser Pegasus file to reconcile", true);
            }
            else if (FileUpload2.FileName.Trim().Equals(""))
            {
                ShowMessage("Please Browser Client file to reconcile", true);
            }
            else
            {
              bool uploadpegasusfilestatus =ReadFileToRecon1("");
              bool uploadclientfilestatus = ReadFileToRecon2("");

              if (uploadpegasusfilestatus && uploadclientfilestatus)
              {
                  //string session = "anthea.martha@pegasustechnologies.co.ug";
                  string sessionEmail = Session["UserEmail"].ToString();
                  //string sessionEmail = Session["UserEmail"].ToString();
                  string name = Session["FullName"].ToString();
                  string user = Session["Username"].ToString();

                  //datafile.SaveUploadedFilesDetail(pegasusfileuploadedpath, clientfileuploadedpath, sessionEmail, 0);
                  datafile.SaveUploadedFilesDetail(pegasusfileuploadedpath, clientfileuploadedpath, user, sessionEmail, 0);

                  ShowMessage("Hello\t"+name+"\t\tPegasus File and Client File have been Uploaded Successfully Reconciliation has will start and the reports will be sent to your Email Shortly", true);
                }
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
    private bool ReadFileToRecon1(string vendorcode)
    {
        bool PegasusFileupload;

        string pegasusfilename = Path.GetFileName(FileUpload1.FileName);
        string pegasusfilenameextension = Path.GetExtension(pegasusfilename);

        


        if (pegasusfilenameextension.ToUpper().Equals(".CSV") || pegasusfilenameextension.ToUpper().Equals(".TXT"))
        {
            string pegasusfilePath = bll.ReconFilePath(vendorcode, pegasusfilename);
            FileUpload1.SaveAs(pegasusfilePath);
            pegasusfileuploadedpath = pegasusfilePath;


            PegasusFileupload = true;

        }
        else
        {
            ShowMessage("Please Browser PEGASUS CSV File, " + pegasusfilenameextension + " file not supported", true);
            PegasusFileupload = false;
        }


        return PegasusFileupload;
       
    }

    private bool ReadFileToRecon2(string vendorcode)
    {
        bool ClientFileupload;
        

        string clientfilename = Path.GetFileName(FileUpload2.FileName);
        string clientfilenameextension = Path.GetExtension(clientfilename);


      

        if (clientfilenameextension.ToUpper().Equals(".CSV") || clientfilenameextension.ToUpper().Equals(".TXT"))
        {
            string filePath = bll.ReconFilePath(vendorcode, clientfilename);
            FileUpload2.SaveAs(filePath);
            clientfileuploadedpath = filePath;

            ClientFileupload = true;

        }
        else
        {
            ShowMessage("Please Browser CSV File, " + clientfilenameextension + " file not supported", true);
            ClientFileupload = false;
        }

        return ClientFileupload;

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
   


    private void CancelRecnBatch(int Reconcode)
    {
        datapay.CancelReconBatch(Reconcode);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        ShowMessage(".", true);
    }
   

    

   
}
