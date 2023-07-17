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
/// Summary description for ReversalObj
/// </summary>
public class ReversalObj
{
    string tranamount, tranStatus, batchNo, description, tranCategory, entryType;

    DateTime date;

    public string EntryType
    {
        get { return entryType; }
        set { entryType = value; }
    }


    public string TranCategory
    {
        get { return tranCategory; }
        set { tranCategory = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public string BatchNo
    {
        get { return batchNo; }
        set { batchNo = value; }
    }
    public string TranStatus
    {
        get { return tranStatus; }
        set { tranStatus = value; }
    }
    public string Tranamount
    {
        get { return tranamount; }
        set { tranamount = value; }
    }

    public DateTime Date
    {
        get { return date; }
        set { date = value; }
    }

}
