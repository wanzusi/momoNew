using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FortuneBetRequest
/// </summary>
public class FortuneBetRequest
{
    public FortuneBetRequest()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string signature { get; set; }
    public string PaymentDate { get; set; }
    public string PegpayId { get; set; }
    public string RecordDate { get; set; }
    public string StatusCode { get; set; }
    public string StatusDescription { get; set; }
    public string TelecomID { get; set; }
    public string TelecomPosted { get; set; }
    public string TranAmount { get; set; }
    public string VendorCode { get; set; }
    public string VendorTranId { get; set; }
    public string oid { get; set; }
    public DateTime ts { get; set; }
}