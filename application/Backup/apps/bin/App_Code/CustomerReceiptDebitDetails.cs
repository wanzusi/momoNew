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
/// Summary description for CustomerDebitReceiptDetails
/// </summary>
public class CustomerReceiptDebitDetails
{

    public string statusDescription, statusCode, customerCode, pegasusAccountName, pegasusAccountNumber,  pegasusAccountNumberNetwork,
        customerDebitAmount, currentCustomerAccountBalance;

    public double currentPegasusAccountBalance;

    private int receiptNumber;

    public int ReceiptNumber
    {
        get
        {
            return receiptNumber;
        }
        set
        {
            receiptNumber = value;
        }
    }
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
    public string CustomerCode
    {
        get
        {
            return customerCode;
        }
        set
        {
            customerCode = value;
        }
    }
    public string PegasusAccountNumber
    {
        get
        {
            return pegasusAccountNumber;
        }
        set
        {
            pegasusAccountNumber = value;
        }
    }
    public string CustomerDebitAmount
    {
        get
        {
            return customerDebitAmount;
        }
        set
        {
            customerDebitAmount = value;
        }
    }
    public string CurrentCustomerAccountBalance
    {
        get
        {
            return currentCustomerAccountBalance;
        }
        set
        {
            currentCustomerAccountBalance = value;
        }
    }
    public Double CurrentPegasusAccountBalance
    {
        get
        {
            return currentPegasusAccountBalance;
        }
        set
        {
            currentPegasusAccountBalance = value;
        }
    }
    
   public string PegasusAccountName
    {
        get
        {
            return pegasusAccountName;
        }
        set
        {
            pegasusAccountName = value;
        }
    }
     public string PegasusAccountNumberNetwork
    {
        get
        {
            return pegasusAccountNumberNetwork;
        }
        set
        {
            pegasusAccountNumberNetwork = value;
        }
    }
    //pegasusAccountNumberNetwork
}
