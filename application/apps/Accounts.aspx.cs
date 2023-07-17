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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;

public partial class AccountStatments : System.Web.UI.Page
{
    private DataTable datatable = new DataTable();
    private DataLogin dac=new DataLogin();
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
                    LoadPageInitials();
                }
                else
                {
                    Response.Redirect("UnauthorisedAccess.aspx");
                }
            }
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
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
                if(page.Trim().ToUpper().Equals(currenePage.Trim().ToUpper()))
                {
                    roleIds.Add(dr["RoleId"].ToString().Trim());
                }
            }
            if(roleIds.Contains(RoleId.Trim()))
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
    private void LoadPageInitials()
    {
        try
        {
            string RoleCode=Session["RoleCode"].ToString();
            if (Session["AreaID"].ToString().Equals("1"))
            {
                  txtCustomerCode.Enabled=true;
                   MultiView1.ActiveViewIndex=0;
            }
            else
            {
                    MultiView1.ActiveViewIndex = -1;
                    MultiView2.ActiveViewIndex = 0;
                    string CompanyCode=Session["Company"].ToString();
                    txtCustomerCode.Text = CompanyCode;
                    txtCustomerCode.Enabled = false;
                    LoadAccounts();
            }
        }
        catch(Exception ex)
        {
            throw ex;
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
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            LoadAccounts(); 
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    private void LoadAccounts()
    {
        try
        {
            string CustomerCode = txtCustomerCode.Text.ToString();
            if (CustomerCode.Equals(""))
            {
                ShowMessage("Enter Company Code",true);
            }
            else
            {
                datatable = dac.GetCustomerAccounts(CustomerCode);
                if(datatable.Rows.Count>0)
                {
                    DataGrid1.Visible=true;
                    DataGrid1.DataSource = datatable;
                   DataGrid1.DataBind();
                   MultiView2.ActiveViewIndex = 0;
                   ShowMessage(".", true);

                }
                else
                {
                     DataGrid1.Visible=false;
                     MultiView2.ActiveViewIndex = -1;
                    ShowMessage("No Accounts Registered against the Customer",true);

                } 
            }
            
    }catch(Exception ex)
    {
        throw ex;
    }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView2.ActiveViewIndex = 0;
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }
    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            ConvertToFile();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void ConvertToFile()
    {
        if (rdExcel.Checked.Equals(false) && rdPdf.Checked.Equals(false))
        {
            ShowMessage("Please Check file format to Convert to", true);
        }
        else
        {
            LoadRpt();
            if (rdPdf.Checked.Equals(true))
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Accounts");

            }
            else
            {
                //Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "Accounts");
                ShowMessage("Excel Export Not enabled",true);

            }
        }
    }
    private void LoadRpt()
    {
        string CustomerCode = txtCustomerCode.Text.ToString();
        datatable = dac.GetCustomerAccounts(CustomerCode);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);

        rptName = physicalPath + "\\Bin\\Reports\\CustomerAccounts.rpt";

        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(datatable);
        CrystalReportViewer1.ReportSource = Rptdoc;
    }
}
