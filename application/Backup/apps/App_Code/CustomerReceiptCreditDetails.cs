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
/// Summary description for CustomerReceiptCreditDetails
/// </summary>
public class CustomerReceiptCreditDetails
{
    
         private string  statusDescription, statusCode, customerCode, customerAccount, customerCreditAmount, balance;

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
        public string CustomerAccount
        {
            get
            {
                return customerAccount;
            }
            set
            {
                customerAccount = value;
            }
        }
    public string CustomerCreditAmount
        {
            get
            {
                return customerCreditAmount;
            }
            set
            {
                customerCreditAmount = value;
            }
        }
        public string Balance
        {
            get
            {
                return balance;
            }
            set
            {
                balance = value;
            }
        }
    }
   
