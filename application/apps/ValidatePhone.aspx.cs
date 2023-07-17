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
using System.Collections.Generic;

public partial class ValidatePhone : System.Web.UI.Page
{
    BusinessLogin bll = new BusinessLogin();

    //
    string username = "";
    string fullname = "";
    string userBranch = "";
    string userRole = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string RoleId = Session["RoleCode"].ToString();
            if (isRoleAuthorisedToVisitPage(RoleId))
            {
                //Check If this is an external Request
                string Id = Request.QueryString["Id"];
                username = Session["UserName"] as string;
                fullname = Session["FullName"] as string;
                userBranch = Session["UserBranch"] as string;
                userRole = Session["RoleCode"] as string;
                Session["IsError"] = null;
                //Session is invalid
                if (username == null)
                {
                    Response.Redirect("Default.aspx");
                }
                else if (IsPostBack)
                {
                    lblmsg.Text = "";
                }
                //this is an edit request
                else if (Id != null)
                {
                    MultiView1.ActiveViewIndex = 0;
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;
                    Multiview2.ActiveViewIndex = 1;
                }
            }
            else
            {
                Response.Redirect("UnauthorisedAccess.aspx");
            }
        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
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
    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = Session["PhoneValidationResults"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (!rdPdf.Checked && !rdExcel.Checked)
                    {
                        bll.ShowMessage(lblmsg, "CHECK ONE EXPORT OPTION", true, Session);
                    }
                    else if (rdExcel.Checked)
                    {
                        bll.ExportToExcel(dt, Response);
                    }
                    else if (rdPdf.Checked)
                    {
                        bll.ExportToPdf(dt, "", Response);
                    }
                }
                catch (Exception ex)
                {
                    string msg = "FAILED: " + ex.Message;
                    bll.ShowMessage(lblmsg, msg, true, Session);
                } 
            }
            else
            {
                string msg = "No Records Found Matching Search Criteria";
                bll.ShowMessage(lblmsg, msg, true, Session);
            }
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ValidateInput();
            ValidatePhones(ddInputType.SelectedValue);
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    private void ValidateInput()
    {
        if (ddInputType.SelectedValue != "FILE" && string.IsNullOrEmpty(txtValue.Text))
        {
            throw new Exception("Please provide atleast one phone number");
        }
        if (ddInputType.SelectedValue == "FILE" && string.IsNullOrEmpty(fileUpload.FileName))
        {
            throw new Exception("Please provide a file");
        }
        if (ddInputType.SelectedValue == "FILE" && !bll.IsValidOption(Path.GetExtension(fileUpload.FileName), ".csv|.txt"))
        {
            throw new Exception("Please provide a .csv or .txt file");
        }
    }

    private void ValidatePhones(string inputType)
    {
        ArrayList phones;
        DataTable resultTable = bll.CreateDataTable("Phones", new string[] { "Phone", "Name", "Network", "Active", "Reason" });
        if (inputType == "FILE")
        {
            DataFile df = new DataFile();
            phones = df.readFile(ReturnPath());
        }
        else
        {
            phones = new ArrayList(txtValue.Text.Split(','));
        }

        foreach (string phone in phones)
        {
            InterLinkClass.LevelOne.TelephoneNumberDetails result = bll.ValidatePhoneNumber(phone);
            string number = string.IsNullOrEmpty(result.Telephonenumber) ? phone : result.Telephonenumber;
            resultTable.Rows.Add(number, result.Name, result.Network, result.IsRegistered, result.StatusDescription);
        }

        Session["PhoneValidationResults"] = resultTable;
        dataGridResults.DataSource = resultTable;
        dataGridResults.DataBind();
        string msg = "Operation Complete";
        Multiview2.ActiveViewIndex = 0;
        bll.ShowMessage(lblmsg, msg, false, Session);
        
    }

    private string ReturnPath()
    {
        string filename = HttpUtility.HtmlEncode(Path.GetFileName(fileUpload.FileName));
        string extension = HttpUtility.HtmlEncode(Path.GetExtension(filename));
        DateTime now = DateTime.Now;
        string dt = now.ToString("hhmmss");
        DataTable returnedPath = new DataTable();
        string folder = @"E:\Logs\MobileMoneyLogs\PhoneValidationFiles\";
        string User = Session["UserName"].ToString().Replace(" ", "-").Replace(".", "");
        filename = User + filename;
        string filepath = folder + dt + filename;
        if (File.Exists(filepath))
        {
            //File.Delete(filepath);
        }
        fileUpload.SaveAs(filepath);
        return filepath;
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataGridResults.PageIndex = e.NewPageIndex;
        DataTable dt = Session["PhoneValidationResults"] as DataTable;
        dataGridResults.DataSource = dt;
        dataGridResults.DataBind();
        Multiview2.ActiveViewIndex = 0;
    }

}
