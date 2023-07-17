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

public partial class SystemReports : System.Web.UI.Page
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

                 }
                 else
                 {
                     LoadData();
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
    private void LoadData()
    {
        bll.LoadSystemCompanies(userBranch, ddCompany);
        bll.LoadTelecoms(ddTelecom);
        bll.LoadReportTypes(userBranch, ddReport, userRole);

        if (userBranch.ToLower().Equals("pegpay"))
        {
            simpleFilterTable.Visible = true;
            advancedFilterTable.Visible = true;
        }
    }

    protected void ddReport_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        string reportCode = ddReport.SelectedValue.ToString();
        try
        {
            ClearPage();

            if (string.IsNullOrEmpty(reportCode))
            {
                bll.LoadTelecoms(ddTelecom);
            }
            else
            {
                SystemReport report = bll.GetReportById(reportCode, userBranch);
                bll.LoadUtilities(ddTelecom, report);
                try
                {
                    bll.LoadVendorsDynamic(ddCompany, report);
                }
                catch (Exception)
                {
                    bll.LoadSystemCompanies(userBranch, ddCompany);
                }
            }

        }
        catch (Exception ex)
        {
            bll.LoadTelecoms(ddTelecom);
            bll.LoadSystemCompanies(userBranch, ddCompany);
        }

        //show button field if specific report is selected
        dataGridResults.Columns[0].Visible = reportCode.Equals("BILLER_CUSTOMERS") ? true : false;
    }

    private void ClearPage()
    {
        dataGridResults.DataSource = new DataTable();
        dataGridResults.DataBind();
        lblCount.Text = "";
        lblTotal.Text = "";
        lblmsg.Text = "";
    }

    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = SearchDb();
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
                        bll.ExportToExcel(dt, "", Response);
                    }
                    else if (rdPdf.Checked)
                    {
                        bll.ExportToPdf(dt, "", Response);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
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

    private void SetSessionVariables(DataTable dt)
    {
        Session["StatementDataTable"] = dt;
        if (string.IsNullOrEmpty(txtFromDate.Text) || string.IsNullOrEmpty(txtToDate.Text))
        {
            Session["fromDate"] = "THE START";
            Session["toDate"] = "TO TODAY";
        }
        else
        {
            Session["fromDate"] = txtFromDate.Text;
            Session["toDate"] = txtToDate.Text;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SearchDb();
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    private DataTable SearchDb()
    {
        DataTable dt = new DataTable();
        try
        {
            SystemReport report = bll.GetReportById(ddReport.SelectedValue, userBranch);
            if (report.StatusCode != "0")
            {
                string msg = report.StatusDesc;
                bll.ShowMessage(lblmsg, msg, true, Session);

            }
            else if (txtFromDate.Text.Equals("") && report.IsDateDelimited)
            {
                bll.ShowMessage(lblmsg, "From Date is required", true, Session);
                txtFromDate.Focus();
            }
            else
            {
                string[] searchParams = GetSearchParameters();
                DataSet ds = bll.ExecuteDataAccess(report.Database, report.StoredProcedure, searchParams);
                dt = ds.Tables[0];

                dt = ApplyCustomFilter(dt);

                if (dt.Rows.Count > 0)
                {
                    CalculateTotal(dt);
                    dataGridResults.DataSource = dt;
                    dataGridResults.DataBind();
                    string msg = "Found " + dt.Rows.Count + " Records Matching Search Criteria";
                    Multiview2.ActiveViewIndex = 0;
                    bll.ShowMessage(lblmsg, msg, false, Session);
                }
                else
                {
                    CalculateTotal(dt);
                    dataGridResults.DataSource = dt;
                    dataGridResults.DataBind();
                    string msg = "No Records Found Matching Search Criteria";
                    bll.ShowMessage(lblmsg, msg, true, Session);
                }

            }

        }
        catch (Exception ex)
        {
            CalculateTotal(dt);
            dataGridResults.DataSource = dt;
            dataGridResults.DataBind();
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
        return dt;
    }

    private DataTable ApplyCustomFilter(DataTable dt)
    {
        string query = "", queryType = "";
        if (!string.IsNullOrEmpty(simpleFilter.Text) && !string.IsNullOrEmpty(advancedFilter.Text))
        {
            throw new Exception("You can only use one search type: Simple or Advanced");
        }

        if (!string.IsNullOrEmpty(simpleFilter.Text.Trim()))
        {
            query = BuildSimpleQuery(simpleFilter.Text.Trim(), dt);
            queryType = "simpleFilter";
        }
        else
        {
            //person knows what they are doing
            query = advancedFilter.Text.Trim();
            queryType = "advancedFilter";
        }

        if (string.IsNullOrEmpty(query))
        {
            return dt;
        }

        DataRow[] rows = dt.Select(query);

        DataTable dataTable = dt.Clone();
        foreach (DataRow dr in rows)
        {
            dataTable.Rows.Add(dr.ItemArray);
        }

        bll.InsertIntoAuditLog(queryType, "SELECT", ddReport.SelectedValue, userBranch, username, bll.GetCurrentPageName(), fullname + " ran the following query : " + query + "  Results:" + dataTable.Rows.Count.ToString());

        return dataTable;
    }

    private string BuildSimpleQuery(string columnsAndValues, DataTable dt)
    {
        string query = "";

        //first get all filters
        string[] filters = columnsAndValues.Split(',');

        foreach (string filter in filters)
        {
            string[] data = filter.Split(':');
            string column = data[0];
            string value = data[1];

            if (bll.TableContainsColumn(column, dt))
            {
                query = " and " + column + " = '" + value + "'";
            }
        }

        string finalQuery = string.IsNullOrEmpty(query) ? query : query.Remove(0, 4);

        return finalQuery;
    }

    private void CalculateTotal(DataTable Table)
    {
        try
        {
            SetTotal(Table, "TranAmount");
        }
        catch (Exception ex)
        {
            try
            {
                SetTotal(Table, "Total");
            }
            catch (Exception exe)
            {
                lblTotal.Text = "";
            }
        }
    }

    private void SetTotal(DataTable Table, string column)
    {
        double total = 0;
        foreach (DataRow dr in Table.Rows)
        {
            double amount = double.Parse(dr[column].ToString());
            total += amount;
        }

        lblTotal.Text = "Total Amount [" + total.ToString("#,##0") + "]";
    }

    private string[] GetSearchParameters()
    {
        List<string> searchCriteria = new List<string>();
        string Vendor = ddCompany.SelectedValue;
        string utility = ddTelecom.SelectedValue;
        string reference = txtValue.Text;
        string FromDate = txtFromDate.Text;
        string ToDate = txtToDate.Text;

        searchCriteria.Add(Vendor);
        searchCriteria.Add(utility);
        searchCriteria.Add(reference);
        searchCriteria.Add(FromDate);
        searchCriteria.Add(ToDate);

        return searchCriteria.ToArray();
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataGridResults.PageIndex = e.NewPageIndex;
        SearchDb();
    }

    protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //make this better later, caters only for merchant requests report
            if (e.CommandName == "Action")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                // Retrieve the row that contains the button clicked by the user from the Rows collection.

                GridViewRow row = dataGridResults.Rows[index];

                string id = row.Cells[1].Text;

                Server.Transfer("AddBillerCustomer.aspx?customerId=" + id);
            }
        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
    }
}

