using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Services;
using System.Web.Services.Protocols;
using AjaxControlToolkit;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CarData
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class CascadingddlService : System.Web.Services.WebService
{
   

    public CascadingddlService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }  
}