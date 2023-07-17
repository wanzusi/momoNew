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
/// Summary description for BatchCustomerDetails
/// </summary>
public class BatchCustomerDetails
{
    private string statusDescription, statusCode, customerName,customerEmail;

    
    public string StatusDescription
    {
        get
        {
            return statusDescription;
        }
        set
        {
            statusDescription = value;
        }
    }
    public string StatusCode
    {
        get
        {
            return statusCode;
        }
        set
        {
            statusCode = value;
        }
    }
    public string CustomerName
    {
        get
        {
            return customerName;
        }
        set
        {
            customerName = value;
        }
    }
    public string CustomerEmail
    {
        get
        {
            return customerEmail;
        }
        set
        {
            customerEmail = value;
        }
    }
   
}
