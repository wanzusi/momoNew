using System;
using System.Collections.Generic;
using System.Text;


public class Statement
{
    private string no, batchNo, valueDate, description, credit, debit, balance, type;

    public string No
    {
        get
        {
            return no;
        }
        set
        {
            no = value;
        }
    }
    public string BatchNo
    {
        get
        {
            return batchNo;
        }
        set
        {
            batchNo = value;
        }
    }
    public string ValueDate
    {
        get
        {
            return valueDate;
        }
        set
        {
            valueDate = value;
        }
    }
    public string Description
    {
        get
        {
            return description;
        }
        set
        {
            description = value;
        }
    }
    public string Credit
    {
        get
        {
            return credit;
        }
        set
        {
            credit = value;
        }
    }

    public string Debit
    {
        get
        {
            return debit;
        }
        set
        {
            debit = value;
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


    public string Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }

}

