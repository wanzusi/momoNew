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
using CrystalDecisions.Shared;
public partial class OvaReport : System.Web.UI.Page
{
    ProcessUsers Process = new ProcessUsers();
    DataLogin datafile = new DataLogin();
    BusinessLogin bll = new BusinessLogin();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
             string RoleId = Session["RoleCode"].ToString();
             if (isRoleAuthorisedToVisitPage(RoleId))
             {
                 string Id = Request.QueryString["Id"];
                 if (Session["RoleName"].ToString().Equals("Manager"))
                 {
                     if (IsPostBack == false)
                     {
                         LoadData();
                     }
                 }
                 else
                 {
                     MultiView1.ActiveViewIndex = -1;
                     bll.ShowMessage(lblmsg, "YOU DO NOT HAVE RIGHTS TO VIEW THIS PAGE", true, Session);
                 }
                 if (Id != null)
                 {
                     LoadData();
                     MultiView1.ActiveViewIndex = 0;
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
    protected void LoadData()
    {
        bll.LoadVendors(ddVendor);
        bll.LoadTelecoms(ddTelecom);

        ddTranType.Items.Clear();
        ddTranType.Items.Add(new ListItem("All ", ""));
        ddTranType.Items.Add(new ListItem("Pull", "PULL"));
        ddTranType.Items.Add(new ListItem("Push", "PUSH"));

        ddOvaChoice.Items.Clear();
        ddOvaChoice.Items.Add(new ListItem("All ", ""));
        ddOvaChoice.Items.Add(new ListItem("Pegasus", "Pegasus"));
        ddOvaChoice.Items.Add(new ListItem("Client", "Client"));
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            SearchDb();
        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
    }

    private DataTable SearchDb()
    {
        DataTable dataTable = new DataTable();
        try
        {
            string vendor = ddVendor.SelectedValue;
            string ovaChoice = ddOvaChoice.SelectedValue;
            string telecom = ddTelecom.SelectedValue;
            string tranType = ddTranType.SelectedValue;


            dataTable = datafile.ExecuteDataSet("SearchOvaDetails", vendor, ovaChoice, telecom, tranType).Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                bll.ShowMessage(lblmsg, dataTable.Rows.Count + " Record(s) found", false, Session);
                MultiView1.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = 0;
                dataGridResults.DataSource = dataTable;
                dataGridResults.DataBind();
            }
            else
            {
                bll.ShowMessage(lblmsg, "No Records Found", true, Session);
                MultiView1.ActiveViewIndex = -1;
                MultiView2.ActiveViewIndex = -1;
            }

        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
        return dataTable;
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataGridResults.PageIndex = e.NewPageIndex;
        SearchDb();
    }

    protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = 0;
        GridViewRow row;
        GridView grid = sender as GridView;

        if (e.CommandName.Equals("editing"))
        {
            index = Convert.ToInt32(e.CommandArgument);
            row = grid.Rows[index];
            string id = row.Cells[1].Text;
            Server.Transfer("ManageVendor.aspx?id=" + id);
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        btnOK.Enabled = true;
        bll.ShowMessage(lblmsg, ".", false, Session);
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            string beneficiaryId = lblbeneficiaryId.Text;
            string reason = txtReason.Text;
            string name = lblName.Text;
            string phone = lblPhone.Text;
            string userId = Session["Username"] as string;
            string fullname = Session["Fullname"] as string;
            string userBranch = Session["CustomerCode"] as string;

            if (string.IsNullOrEmpty(reason))
            {
                throw new Exception("Please provide a reason for deleting this beneficiary");
            }


            bool actioned = bll.DeleteBeneficiaryById(beneficiaryId, reason, userId);
            if (actioned)
            {
                bll.InsertIntoAuditLog(phone, "DELETE", "CustomerBeneficiaries", userBranch, userId, "ViewCustomerBeneficiaries.aspx",
   fullname + " deleted the beneficiary [" + name + "]  with the phone [" + phone + "] at " + DateTime.Now.ToString());

                string msg = "You have successfully deleted the beneficiary [" + name + "] with phone [" + phone + "]";
                bll.ShowMessage(lblmsg, msg, false, Session);
                MultiView1.SetActiveView(View1);
                SearchDb();
            }
            else
            {
                string msg = "This operation failed. Please contact us for further help";
                bll.ShowMessage(lblmsg, msg, false, Session);
                MultiView1.SetActiveView(View1);
                SearchDb();
            }
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
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
}