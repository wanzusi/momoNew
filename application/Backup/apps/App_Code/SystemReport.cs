using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for SystemReport
/// </summary>
public class SystemReport
{
    public string Company = "";
    public string ReportType = "";
    public string ReportName = "";
    public string CanAccess = "";
    public string Database = "";
    public string StoredProcedure = "";
    public string StatusCode = "";
    public string StatusDesc = "";
    public bool IsDateDelimited;
}
