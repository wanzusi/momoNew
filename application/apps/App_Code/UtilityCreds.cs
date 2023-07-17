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
/// Summary description for UtilityCreds
/// </summary>
public class UtilityCreds
{
    private string vendorCode, senderId, password, spId;
    public string VendorCode
    {
        get
        {
            return vendorCode;
        }
        set
        {
            vendorCode = value;
        }
    }
    public string SenderId
    {
        get
        {
            return senderId;
        }
        set
        {
            senderId = value;
        }
    }
    public string Password
    {
        get
        {
            return password;
        }
        set
        {
            password = value;
        }
    }
    public string SpId
    {
        get
        {
            return spId;
        }
        set
        {
            spId = value;
        }
    }
}
