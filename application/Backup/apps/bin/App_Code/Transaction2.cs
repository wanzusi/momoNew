using System;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for Transaction
/// </summary>
public class Transaction2
{

    string transCategory, FromAccount, recordId, ToAccount, CustName, VendorTranId, TranAmount, TranCharge, FromNetwork, ToNetwork, PaymentDate, RecordDate,
        PaymentType, TranType, VendorCode, Phone, MNOCharge, CashoutCharge, Charge1, Charge2, PegasusCommisionAccount,
        TelecomCommissionAccount, CashoutAccount, PegPayId, TelecomID, TranStatus, SentToVendorStatus, pegID;

    public string PegID
    {
        get { return pegID; }
        set { pegID = value; }
    }

    public string TransCategory
    {
        get { return transCategory; }
        set { transCategory = value; }
    }

    public string RecordId
    {
        get { return recordId; }
        set { recordId = value; }
    }




    public string getSentToVendorStatus
    {
        get
        {
            return SentToVendorStatus;
        }
        set
        {
            SentToVendorStatus = value;
        }
    }
    public string getTranStatus
    {
        get
        {
            return TranStatus;
        }
        set
        {
            TranStatus = value;
        }
    }

    public string getTelecomID
    {
        get
        {
            return TelecomID;
        }
        set
        {
            TelecomID = value;
        }
    }


    public string getPegPayId
    {
        get
        {
            return PegPayId;
        }
        set
        {
            PegPayId = value;
        }
    }

    public string getRecordDate
    {
        get
        {
            return RecordDate;
        }
        set
        {
            RecordDate = value;
        }
    }

    public string getCashoutAccount
    {
        get
        {
            return CashoutAccount;
        }
        set
        {
            CashoutAccount = value;
        }
    }


    public string getTelecomCommissionAccount
    {
        get
        {
            return TelecomCommissionAccount;
        }
        set
        {
            TelecomCommissionAccount = value;
        }
    }
    public string getPegasusCommisionAccount
    {
        get
        {
            return PegasusCommisionAccount;
        }
        set
        {
            PegasusCommisionAccount = value;
        }
    }


    public string getCharge2
    {
        get
        {
            return Charge2;
        }
        set
        {
            Charge2 = value;
        }
    }

    public string getCharge1
    {
        get
        {
            return Charge1;
        }
        set
        {
            Charge1 = value;
        }
    }


    public string getCashoutCharge
    {
        get
        {
            return CashoutCharge;
        }
        set
        {
            CashoutCharge = value;
        }
    }


    public string getMNOCharge
    {
        get
        {
            return MNOCharge;
        }
        set
        {
            MNOCharge = value;
        }
    }


    public string getFromAccount
    {
        get
        {
            return FromAccount;
        }
        set
        {
            FromAccount = value;
        }
    }

    public string getToAccount
    {
        get
        {
            return ToAccount;
        }
        set
        {
            ToAccount = value;
        }
    }
    public string getCustName
    {
        get
        {
            return CustName;
        }
        set
        {
            CustName = value;
        }
    }
    public string getVendorTranId
    {
        get
        {
            return VendorTranId;
        }
        set
        {
            VendorTranId = value;
        }
    }
    public string getTranAmount
    {
        get
        {
            return TranAmount;
        }
        set
        {
            TranAmount = value;
        }
    }
    public string getTranCharge
    {
        get
        {
            return TranCharge;
        }
        set
        {
            TranCharge = value;
        }
    }
    public string getFromNetwork
    {
        get
        {
            return FromNetwork;
        }
        set
        {
            FromNetwork = value;
        }
    }
    public string getToNetwork
    {
        get
        {
            return ToNetwork;
        }
        set
        {
            ToNetwork = value;
        }
    }
    public string getPaymentDate
    {
        get
        {
            return PaymentDate;
        }
        set
        {
            PaymentDate = value;
        }
    }
    public string getPaymentType
    {
        get
        {
            return PaymentType;
        }
        set
        {
            PaymentType = value;
        }
    }
    public string GetTranType
    {
        get
        {
            return TranType;
        }
        set
        {
            TranType = value;
        }
    }
    public string GetTranAmount
    {
        get
        {
            return TranAmount;
        }
        set
        {
            TranAmount = value;
        }
    }
    public string getVendorCode
    {
        get
        {
            return VendorCode;
        }
        set
        {
            VendorCode = value;
        }
    }
    public string getPhone
    {
        get
        {
            return Phone;
        }
        set
        {
            Phone = value;
        }
    }

}
