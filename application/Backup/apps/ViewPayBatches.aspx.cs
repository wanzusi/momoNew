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
public partial class ViewPayBatches : System.Web.UI.Page
{
    ProcessUsers Process = new ProcessUsers();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    private ReportDocument Rptdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                string RoleId = Session["RoleCode"].ToString();
                if (isRoleAuthorisedToVisitPage(RoleId))
                {
                    chkActive.Checked = false;
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
            LoadBatches();            
         }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadBatches()
    {
        DateTime fromDate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime toDate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        bool confirmed = chkActive.Checked;
        dataTable = datapay.GetPaymentBatches(fromDate, toDate, confirmed);
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
            SystemUser user = new SystemUser();
            if (e.CommandName == "btnConfirm")
            {
                string BatchCode = e.Item.Cells[0].Text;
                string ret = Process.ConfirmBillSystemUpdate(BatchCode);
                ShowMessage(ret, false);
                LoadBatches();
            }
            else if (e.CommandName == "btndownload")
            {
                string BatchCode = e.Item.Cells[0].Text;
                string BillCode = e.Item.Cells[5].Text;
                int trans = int.Parse(e.Item.Cells[6].Text);
                double total = double.Parse(e.Item.Cells[7].Text.Replace(",", ""));
                string batchType = e.Item.Cells[8].Text;
                if (batchType.Equals("P"))
                {
                    string filePath = bll.BuildBatchfile(BatchCode, BillCode, trans, total);
                    DownloadFile(filePath, true);
                }
                else
                {
                    DownloadBatch(BatchCode);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }

    private void DownloadBatch(string BatchCode)
    {
        dataTable = datapay.GetRevBatchDetails(BatchCode);
        if (dataTable.Rows.Count > 0)
        {
            string appPath, physicalPath, rptName;
            appPath = HttpContext.Current.Request.ApplicationPath;
            physicalPath = HttpContext.Current.Request.MapPath(appPath);

            rptName = physicalPath + "\\Bin\\Reports\\NegativeBatch.rpt";

            Rptdoc.Load(rptName);
            Rptdoc.SetDataSource(dataTable);
            CrystalReportViewer1.ReportSource = Rptdoc;
            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "REVERSALS");
            Rptdoc.Dispose();
        }
    }
    private void DownloadFile(string path, bool forceDownload)
    {
        string name = Path.GetFileName(path);
        string type = "text/plain";
        if (forceDownload)
        {
            Response.AppendHeader("content-disposition",
                "attachment; filename=" + name);
        }
        if (type != "")
            Response.ContentType = type;
        Response.WriteFile(path);
        Response.End();
        bll.RemoveFile(path);
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            DateTime fromDate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime toDate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            bool confirmed = chkActive.Checked;
            dataTable = datapay.GetPaymentBatches(fromDate, toDate, confirmed);        
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
}
