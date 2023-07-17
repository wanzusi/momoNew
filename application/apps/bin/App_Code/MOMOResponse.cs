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
/// Summary description for MOMOResponse
/// </summary>
public class MOMOResponse
{
	public MOMOResponse()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string processingNumber, serviceId, senderID, statusCode, statusDesc, opCoID, iMSINum, mSISDNNum, orderDateTime, thirdPartyAcctRef, mOMTransactionID;
    public string ProcessingNumber
        {
            get
            {
                return processingNumber;
            }
            set
            {
                processingNumber = value;
            }
        }
     public string ServiceId
        {
            get
            {
                return serviceId;
            }
            set
            {
                serviceId = value;
            }
        }
    public string SenderID
    {
        get
        {
            return senderID;
        }
        set
        {
            senderID = value;
        }
    }
    public string IMSINum
    {
        get
        {
            return iMSINum;
        }
        set
        {
            iMSINum = value;
        }
    }
    public string MSISDNNum
    {
        get
        {
            return mSISDNNum;
        }
        set
        {
            mSISDNNum = value;
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
    public string StatusDesc
    {
        get
        {
            return statusDesc;
        }
        set
        {
            statusDesc = value;
        }
    }
    public string OpCoID
    {
        get
        {
            return opCoID;
        }
        set
        {
            opCoID = value;
        }
    }
    public string OrderDateTime
    {
        get
        {
            return orderDateTime;
        }
        set
        {
            orderDateTime = value;
        }
    }
    public string ThirdPartyAcctRef
    {
        get
        {
            return thirdPartyAcctRef;
        }
        set
        {
            thirdPartyAcctRef = value;
        }
    }
    public string MOMTransactionID
    {
        get
        {
            return mOMTransactionID;
        }
        set
        {
            mOMTransactionID = value;
        }
    }
}
