using System;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for Transaction
/// </summary>
public class Transaction
{
    private string fromTelecom, toTelecom, customerRef, customerName, vendorTranId, tranAmount, tranCharge, telecom, tranType, toAccount, fromAccount,paymentDate,password,vendorCode,digitalSignature,paymentCode;

    public string DigitalSignature
    {
        get
        {
            return digitalSignature;
        }
        set
        {
            digitalSignature = value;
        }
    }
    public string FromTelecom
    {
        get
        {
            return fromTelecom;
        }
        set
        {
           fromTelecom = value;
        }
    }
    public string ToTelecom
    {
        get
        {
            return toTelecom;
        }
        set
        {
            toTelecom = value;
        }
    }
    public string PaymentCode
    {
        get
        {
            return paymentCode;
        }
        set
        {
            paymentCode = value;
        }
    }
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
    public string PaymentDate
    {
        get
        {
            return paymentDate;
        }
        set
        {
            paymentDate = value;
        }
    }
    public string Telecom
    {
        get
        {
            return telecom;
        }
        set
        {
           telecom = value;
        }
    }
    public string CustomerRef
    {
        get
        {
            return customerRef;
        }
        set
        {
            customerRef = value;
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
    public string TranAmount
    {
        get
        {
            return tranAmount;
        }
        set
        {
            tranAmount = value;
        }
    }
    public string TranCharge
    {
        get
        {
            return tranCharge;
        }
        set
        {
            tranCharge = value;
        }
    }
    public string VendorTranId
    {
        get
        {
            return vendorTranId;
        }
        set
        {
            vendorTranId = value;
        }
    }
    public string ToAccount
    {
        get
        {
            return toAccount;
        }
        set
        {
            toAccount = value;
        }
    }
    public string FromAccount
    {
        get
        {
            return fromAccount;
        }
        set
        {
            fromAccount = value;
        }
    }
    public string TranType
    {
        get
        {
            return tranType;
        }
        set
        {
            tranType = value;
        }
    }
}
