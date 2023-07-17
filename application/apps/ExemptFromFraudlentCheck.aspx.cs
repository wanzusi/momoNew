using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class ExemptFromFraudlentCheck : System.Web.UI.Page
{
    DataTable dtable = new DataTable();
    DataLogin datafile = new DataLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string RoleId = Session["RoleCode"].ToString();
            if (isRoleAuthorisedToVisitPage(RoleId))
            {
                if (IsPostBack == false)
                {
                    LoadVendors();
                    //MultiView1.ActiveViewIndex = -1;
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

    private void LoadVendors()
    {
        //dtable = datafile.GetAllVendors("0");
        dtable = datafile.GetSystemCompanies("", "");
        cboVendor.DataSource = dtable;
        cboVendor.DataValueField = "CompanyCode";
        cboVendor.DataTextField = "Company";
        cboVendor.DataBind();

        //disbale drop down if user is not from Pegasus
        string UsersCompanyCode = Session["UserBranch"].ToString();
        if (UsersCompanyCode.ToUpper() != "PEGPAY")
        {
            cboVendor.SelectedValue = UsersCompanyCode;
            cboVendor.Enabled = false;
        }
    }


    private string GetCurrentPage()
    {
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string sRet = oInfo.Name;
        return sRet;
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

    protected void cboVendor_DataBound(object sender, EventArgs e)
    {
       // cboVendor.Items.Insert(0, new ListItem("All Agents", "0"));
    }

    protected void cboType_DataBound(object sender, EventArgs e)
    {

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
    protected void btnOK_Click(object sender, EventArgs e)
    {
        BusinessLogin bll = new BusinessLogin();
        string phone ="";
        try
        {
            if (string.IsNullOrEmpty(cboType.SelectedValue)) { ShowMessage("NO EXEMPTION TYPE SELECTED", true); return; }
            if (string.IsNullOrEmpty(txtPhone.Text)) { ShowMessage("PLEASE ENTER PHONE NUMBER TO EXEMPT", true); return; }
            if (string.IsNullOrEmpty(cboVendor.SelectedValue)) { ShowMessage("PLEASE SELECT WHICH VENDOR TO EXEMPT", true); return; }

            if (!cboType.SelectedValue.Equals("ALL"))
            {
                if (ValidPhoneNumber(txtPhone.Text.Trim(), out phone))
                {
                }
                else 
                {
                    ShowMessage("PLEASE PROVIDE A VALID PHONE NUMBER",true);
                    return;
                }

            }
            else
            {
                phone = "ALL";
            }
            string[] paramz = { cboVendor.SelectedValue.ToString(), phone };
            bll.InsertIntoAuditLog("EXEMPTFRAUDCHECK", "INSERT", "NumbersExemptedFromFrauduentCheck", cboVendor.SelectedValue.ToString(), Session["UserName"].ToString(), GetCurrentPage(), "Inserted Exemption of " + phone + " for " + cboVendor.SelectedValue.ToString() + " by " + Session["UserName"].ToString()+" On "+DateTime.Now.ToString());
            datafile.ExecuteNonQuery("ExemptFromCheckForFraudulentTransactions", paramz);
            ShowMessage("EXEMPTION OF "+phone+" FOR "+cboVendor.SelectedItem.ToString()+".: SUCCESS",false);
        }
        catch (Exception ex)
        {
            ShowMessage("EXEMPTION FAILED DUE TO "+ex.Message, true);
        }
    }
    public bool ValidPhoneNumber(string phone,out string correctPhone)
    {
        int len = phone.Length;
        if (len < 9)
        {
            correctPhone = "";
            return false;
        }
        else if (len.Equals(11))
        {
            correctPhone = "";
            return false;
        }
        else
        {
            string firststr = phone.Substring(0, 3);
            if (firststr == "256" && len == 12)
            {
                correctPhone = phone;
                return true;
            }
            else
            {
                string fst = phone.Substring(0, 2);
                if ((fst == "07" || fst == "03" || fst == "04") && len == 10)
                {
                    correctPhone = "256" + phone.Remove(0,1);
                    return true;
                }
                else
                {
                    string fstr = phone.Substring(0, 1);
                    if ((fstr == "7" || fstr == "3" || fstr == "4") && len == 9)
                    {
                        correctPhone = "256" + phone;
                        return true;
                    }
                    else
                    {
                        correctPhone = "";
                        return false;

                    }
                }
            }
        }
    }

    protected void cboType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboType.SelectedValue.Equals("ALL"))
        {
            txtPhone.Enabled = false;
            txtPhone.Text = "ALL";
        }
        else
        {
            txtPhone.Enabled = true;
            txtPhone.Text = "";
        }
    }
}