using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;//
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using InterLinkClass.EntityObjects;
using System.Collections;
using IntXLib;
using System.Globalization;
using PegasusEnviroment;

public class DataLogin
{
    private Database MMoney_DB, SchoolDB, MMoney_DBArc;
    private Database VAS_DB;
    private Database UtilitiesDB;
    private Database POS_DB;
    private DbCommand procommand;
    private DataSet returnDataset;
    private DataTable datatable;
    public static int updateTransactionsuccess;
    PegasusEnviroment.Connection momoConn, vasConn, utilityConn, posConn, schoolConn, momoConnArc; 
    public string ReturnConsring()
    {
        //string constring = "Jab_2008";
        string constring = "MobileMoney";
        //string constring = "TestPegPay";
        
        return constring;
    }
    public string ReturnConsringArc()
    {
        //string constring = "Jab_2008";
        string constring = "LivePegPayArc";
        //string constring = "TestPegPay";

        return constring;
    }
    public string ReturnVASConString()
    {
        string constring = "LiveVasPegPay";
        //string constring = "TestVasPegPay";
        return constring;
    }
    public string ReturnConsringUtilitiesDB()
    {
        //string constring = "Jab_2008";
        string constring = "LiveUtilitiesDB";
        //string constring = "TestPegPay";
        return constring;
    }
    public string ReturnPosConsring()
    {
        string constring = "LivePosPegPay";
        //string constring = "internet";
        //string constring = "TestPosPegPay";
        return constring;
    }
    public string ReturnSchoolConsring()
    {
        string constring = "LiveSchoolFees";
        //string constring = "internet";
        //string constring = "TestPosPegPay";
        return constring;
    }

    
    public DataLogin()
    {
        try
        {

            momoConn = new PegasusEnviroment.Connection(".9",
                PegpayConfigs.ENV_LIVE, ReturnConsring());
            momoConn = momoConn.GetConnection();

            momoConnArc = new PegasusEnviroment.Connection(".9",
                PegpayConfigs.ENV_LIVE, ReturnConsringArc());
            momoConnArc = momoConnArc.GetConnection();

            vasConn = new PegasusEnviroment.Connection(PegpayConfigs.SERVER_DBSERVER2,
                PegpayConfigs.ENV_LIVE, ReturnVASConString());
            vasConn = vasConn.GetConnection();

            posConn = new PegasusEnviroment.Connection(PegpayConfigs.SERVER_DBSERVER,
                PegpayConfigs.ENV_LIVE, ReturnPosConsring());
            posConn = posConn.GetConnection();

            utilityConn = new PegasusEnviroment.Connection(PegpayConfigs.SERVER_DBSERVER2,
                PegpayConfigs.ENV_LIVE, ReturnConsringUtilitiesDB());
            utilityConn = utilityConn.GetConnection();

            schoolConn = new PegasusEnviroment.Connection(PegpayConfigs.SERVER_DBSERVER2,
               PegpayConfigs.ENV_LIVE, ReturnSchoolConsring());
            schoolConn = schoolConn.GetConnection();


           // MMoney_DB = momoConn.Database;
            MMoney_DB = DatabaseFactory.CreateDatabase("ConnectionString");
            //DatabaseFactory.CreateDatabase(ReturnConsring());
            //MMoney_DB = DatabaseFactory.CreateDatabase(ReturnConsring());
            MMoney_DBArc = momoConnArc.Database; //DatabaseFactory.CreateDatabase(ReturnConsringArc());
            UtilitiesDB = utilityConn.Database; // DatabaseFactory.CreateDatabase(ReturnConsringUtilitiesDB());
            POS_DB = posConn.Database; //DatabaseFactory.CreateDatabase(ReturnPosConsring());
            VAS_DB = vasConn.Database; //DatabaseFactory.CreateDatabase(ReturnVASConString());

            SchoolDB = schoolConn.Database;
       
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Database ShareBilling_Con()
    {
        return MMoney_DB;
    }

    public Database MMoneyBDArc()
    {
        return MMoney_DBArc;
    }

    //GET TRANSFER BATCH STATUS
    public DataTable GetTransferBatchStatus()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetTrasferBatchStatus");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetUserAccessibility(SystemUser user)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetUserLoginDetails", user.Uname, user.Passwd);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetNetworkTariff()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetNetworkTariff");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAreaList()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAreas");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetDeletedTransactionToReverse(string PegPayId)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetDeletedTransactionToReverse", PegPayId);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetBanks()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetChequeBanks");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetActiveBanks()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetActiveChequeBanks");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAreas()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAreaDetails2");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetBeneficaryType()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetBeneficaryType");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetUserTypes()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetUserTypes");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string GetSystemParameter(int GroupCode, int ValueCode)
    {
        string ret = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetSystemSetting", GroupCode, ValueCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            if (datatable.Rows.Count > 0)
            {
                ret = datatable.Rows[0]["ValueVarriable"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ret;
    }

    internal DataTable CheckUsername(string userName)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetUserLogins", userName);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable CheckCompanyCode(string companyCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetActiveCompany", companyCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable getInterfaceErrors()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("getInterfaceErrors");
            DataTable datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable GetAreaByName(string AreaName)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAreaByName", AreaName);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal int GetNumberOfBranches(int RegionID)
    {
        int ret = 0;
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetNumofBranches", RegionID);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            if (datatable.Rows.Count > 0)
            {
                ret = int.Parse(datatable.Rows[0]["Branches"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ret;
    }

    internal DataTable GetUserByNames(SystemUser user)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetUserByfullName", user.Fname, user.Oname, user.Sname);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetBranches(int AreaID)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetDistrictsByArea", AreaID);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetVendorDataById(string Id)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetVendorDataById", Id);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            //GetNetworkInMobileMoneyDB();
            throw ex;
        }
    }

    public DataTable GetEODOVABalance(string Network, string OvaName, DateTime Fromdate, DateTime Todate)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetEODOVABalance_portal", Network, OvaName, Fromdate, Todate);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            //GetNetworkInMobileMoneyDB();
            throw ex;
        }
    }

    public DataTable GetNewVendorsFromDB()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAllNewVendors");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            //GetNetworkInMobileMoneyDB();
            throw ex;
        }
    }

    public DataTable GetVendorsToApprove(string agentCode, DateTime fromDate, DateTime toDate)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetVendorsToApprove", agentCode, fromDate, toDate);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool UpdateApprovedVendors(int RecordId, out bool updated)
    {

        updated = false;
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("UpdateApprovedVendors", RecordId);
            MMoney_DB.ExecuteNonQuery(procommand);
            updated = true;
        }
        catch (Exception ex)
        {
            updated = false;
            //throw ex;
        }

        return updated;
    }

    public DataTable GetDistrictByRegionCode(string RegionCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetDistrictByRegionCode", RegionCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable getInterfaceErrorVendorCodes()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("getInterfaceErrorVendorCodes");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable getInterfaceErrorUtilities()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("getInterfaceErrorUtilities");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetUserDetailsByID(int UserID)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetLoginDetailsByID", UserID);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetSystemRoles(bool CustomerUser)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetSystemRoles", CustomerUser);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetNotificationTypes(string category)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetNotificationType", category);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetSystemUsers(SystemUser user)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetSystemUsers", user.Name, user.Area, user.Branch, user.Role);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetSystemCustomers(PegPayCustomer cust)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetSystemCustomers", cust.Fullname, cust.PegpayAccountNumber, cust.MoMoAccountNumber, cust.CustomerType);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetVendors(Vendor vendor)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetVendors", vendor.VendorName, vendor.Active, vendor.Parameter, vendor.Value);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAllVendors(string VendorCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetVendorDetails", VendorCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetPayTypes()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetPayTypes");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetActivePayTypes()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetActivePayTypes");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetPaymentTypes()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetPaymentTypes");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetVendorById(Vendor vendor)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetVendorById", vendor.Vendorid);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SaveActivationForReconciliation(bool act, string user)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("UpdateReconActivateStatus", act, user);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetFileProcesses(string file_type, DateTime fromDate, DateTime toDate)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetFileProcesses", file_type, fromDate, toDate);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetTransactionToPend(string @recordId)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetTransactionToPend", @recordId);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetTransactionToComplete(string @recordId)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetTransactionToComplete", @recordId);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetLogs(SystemUser user)
    {
        try
        {
            string RoleCode = HttpContext.Current.Session["RoleCode"].ToString();
            if (RoleCode.Equals("016"))
            {

                user.CompanyCode = HttpContext.Current.Session["CompanyCode"].ToString();
            }
            else
            {
                user.CompanyCode = "";
            }
            procommand = MMoney_DB.GetStoredProcCommand("GetUserLogs", user.Name, user.Role, user.FromDate, user.ToDate, user.CompanyCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    public DataTable GetSystemParameters()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetSystemParameters");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAreaDetailsByID(int areaid)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAreaDetailsByID", areaid);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetBankDetailsByID(int bankid)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetBankDetailsByID", bankid);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable GetTransByBatch(string BatchCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetTransByBatch", BatchCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetDistricts(string regioncode, string name, bool Isactive)
    {
        try
        {
            int regionid = int.Parse(regioncode);
            procommand = MMoney_DB.GetStoredProcCommand("GetDistricts", regionid, name, Isactive);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetDistrictDetails(string districtcode)
    {
        try
        {
            int districtid = int.Parse(districtcode);
            procommand = MMoney_DB.GetStoredProcCommand("GetDistrictDetails", districtid);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable GetVendorDetails(string vendorCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetVendorDetails", vendorCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetPayTypeByCode(string PayTypeCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetPayTypeByCode", PayTypeCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCashiers(string districtcode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCashiers", districtcode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetTellerSession(int tellerId, string districtCode, DateTime date)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetTellerSession", tellerId, districtCode, date);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetTellerSessions(string districtCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetTellerSessionTokens", districtCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetPayTypesByShortName(string shortname)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetPayTypeCodeByShortName", shortname);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable GetTellerSession(int TellerID, DateTime date, string DistrictCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetTellerSession", TellerID, DistrictCode, date);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetErrorSubs()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetErrorSub");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable GetErrorSubByEmail(string email)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetErrorSubByEmail", email);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal DataTable GetErrorSubByPhone(string phone)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetErrorSubByPhone", phone);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public string GetPayTypeCodeByShortName(string ShortName)
    {
        string ret = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetPayTypeCodeByShortName", ShortName);
            procommand.CommandTimeout = 120;
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            if (datatable.Rows.Count > 0)
            {
                ret = datatable.Rows[0]["PaymentCode"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ret;
    }
    public Hashtable GetNetworkCodes()
    {
        Hashtable networkCodes = new Hashtable();
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetNetworkCodes");
            DataSet ds = MMoney_DB.ExecuteDataSet(procommand);
            int recordCount = ds.Tables[0].Rows.Count;
            if (recordCount != 0)
            {
                for (int i = 0; i < recordCount; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    string network = dr["Network"].ToString();
                    string code = dr["Code"].ToString();
                    networkCodes.Add(code, network);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return networkCodes;
    }
    public DataTable GetNetworks()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetNetworks");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /////////////////////////////////////////////
    //////////////////////////

    ////
    public void LogActivity(string UserCode, string Action)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("LogSystemActivity", UserCode, Action);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void UpdatePassword(int UserID, string EncryptedPassword)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("PasswordChange", UserID, EncryptedPassword);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable GetAccountDetails(string username)
    {
        DataTable table = new DataTable();
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAccountDetails", username);
            table = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return table;
    }

    public void SaveLoginOTP(string username)
    {
        try
        {
            int num = new Random().Next(100000, 999999);
            string pin = Convert.ToString(num);
            BusinessLogin bll = new BusinessLogin();
            bll.SendOTPSms(username, pin);
            pin = bll.EncryptString(pin);
            username = bll.EncryptString(username);
            procommand = MMoney_DB.GetStoredProcCommand("SaveLoginOTP", username, pin);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable PickLoginOTP(string UserName, string PIN)
    {
        try
        {
            BusinessLogin bllo = new BusinessLogin();
            UserName = bllo.EncryptString(UserName);
            PIN = bllo.EncryptString(PIN);

            procommand = MMoney_DB.GetStoredProcCommand("PickLoginOTP", UserName, PIN);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void UpdateLoginOtp(string UserName, string PIN)
    {
        try
        {
            BusinessLogin bllo = new BusinessLogin();
            UserName = bllo.EncryptString(UserName);
            PIN = bllo.EncryptString(PIN);

            procommand = MMoney_DB.GetStoredProcCommand("UpdateLoginOtp2", UserName, PIN);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void ResetOTPCount(string UserName)
    {
        try
        {
          
            procommand = MMoney_DB.GetStoredProcCommand("ResetOTPCount", UserName);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void UpdateOTPRetries(string UserName)
    {
        try
        {
            
            procommand = MMoney_DB.GetStoredProcCommand("UpdateOtpRetries", UserName);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void DeactivateUserAccount(string UserName)
    {
        try
        {

            procommand = MMoney_DB.GetStoredProcCommand("DeactivateUserWithFailedOTP", UserName);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public void DeactivateLogInOTPs(string UName)
    {
        try
        {
            BusinessLogin bllo = new BusinessLogin();
            string UserName = bllo.EncryptString(UName);

            procommand = MMoney_DB.GetStoredProcCommand("DeactivateLogInOTPs", UName,UserName);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string GetPhoneForOTP(string UserName)
    {
        string phone = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetPhoneForOTP", UserName);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            phone = datatable.Rows[0]["UserPhone"].ToString();
            return phone;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomerDetails(string UserName)
    {
        DataTable dt = new DataTable();
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerNotificationDetails", UserName);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
           // phone = datatable.Rows[0]["UserPhone"].ToString();
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal string GetEmailForOTP(string username)
    {
        string usermail = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetEmailForOTP", username);
            DataTable table = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            if (table.Rows.Count > 0)
            {
                usermail = table.Rows[0]["UserEmail"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return usermail;
    }


    internal void LogOTPSMS(string phone, string message)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("InsertSmsToSend", phone, message, "SMS-INFO", "PEGASUS");
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    internal void LoginStatus(SystemUser user)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("LoginStatus", user.Userid, user.LoggedOn);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal void SaveLoginDetails(SystemUser user)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveSystemUser2", user.Userid, user.Fname, user.Sname, user.Oname,
                user.Uname, user.Passwd, user.Role, user.UserType, user.CompanyCode, user.Email, user.Phone, user.Title, user.Active, user.LoggedOn, user.User, user.IsCoporate, user.IsCustomer, user.FileLevel, user.NotificationType);
            MMoney_DB.ExecuteNonQuery(procommand);
            if (user.Userid.Equals(0))
            {
                // Save in SMS Credit Table
                SaveInCredit(user, 0);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void SaveInCredit(SystemUser user, int credit)
    {
        try
        {
            DateTime now = DateTime.Now;
            procommand = MMoney_DB.GetStoredProcCommand("AddCredit", user.Uname, credit, user.User, now);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void SaveVendorDetails(Vendor vendor, Merchant merchant)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveVendorDetails1", vendor.Vendorid, vendor.VendorCode, vendor.BillSysCode, vendor.VendorName,
             vendor.Contract, vendor.Passwd, vendor.Email, vendor.Active, vendor.User, vendor.PegpayCharge, vendor.ChargeType, vendor.IsRequiredCert,vendor.AccountManager, vendor.AccountRep);
            MMoney_DB.ExecuteNonQuery(procommand);
            //merchant.PegPayVendorCode = vendor.VendorCode;
            //SaveMerchantDetails(merchant, vendor.User);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SaveMerchantDetails(Merchant merchant, string user)
    {
        procommand = MMoney_DB.GetStoredProcCommand("SaveMerchantsDetails", merchant.PegPayVendorCode, merchant.ClientId,
            merchant.TerminalId, merchant.OperatorId,
            merchant.Password, merchant.Active, user);
        MMoney_DB.ExecuteNonQuery(procommand);
    }

    internal void ResetPassword(SystemUser user)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("ResetPassword", user.Userid, user.Passwd);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal void ResetVendorPassword(Vendor vendor)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("ResetVendorPassword", vendor.Vendorid, vendor.Passwd);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    internal void UpdateSystemParameter(int valueid, string Varriable, string CreatedBy)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("UpdateSystemParameter", valueid, Varriable, CreatedBy);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void SaveAreaDetails(int areaid, string areacode, string areaname, bool active, string CreatedBy)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveAreaDetail", areaid, areacode, areaname, active, CreatedBy);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void SaveDistrictDetails(int districtid, string code, string name, int regionid, bool Isactive, string CreatedBy)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveDistrictDetails", districtid, code, name, regionid, Isactive, CreatedBy);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void SavePayType(string code, string name, bool IsActive, string CreatedBy)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SavePayType", code, name, IsActive, CreatedBy);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void ConfirmBatchUpdate(string BatchCode, string CreatedBy)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("ConfirmBatchUpdate", BatchCode, CreatedBy);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void SaveBankDetails(int bankid, string name, string phone, string email, bool active, string CreatedBy)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveBankDetails", bankid, name, phone, email, CreatedBy, active);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void SaveTellerSession(int TellerID, DateTime date, string User, string DistrictCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveTellerSession", TellerID, date, DistrictCode, User);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void ChangeTokenState(int tokenId, bool active, string User)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("ChangeTokenState", tokenId, active, User);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void SaveReceiptRange(int recordId, int startpoint, int endpoint, string cashier, string districtcode, double amount, string user)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveManualReceiptRange", recordId, startpoint, endpoint, cashier, amount, districtcode, user);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void SaveErrorSub(string name, string phone, string email)
    {
        try
        {
            string user = HttpContext.Current.Session["Username"].ToString();
            procommand = MMoney_DB.GetStoredProcCommand("SaveErrorSub", name, phone, email, user);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void ChangeSubStatus(int recordId, bool status)
    {
        try
        {
            string user = HttpContext.Current.Session["Username"].ToString();
            procommand = MMoney_DB.GetStoredProcCommand("UpdateErrorSubStatus", recordId, status, user);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void SaveNetworkTariff(string code, int tarrifNo)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveNetworkTariff", code, tarrifNo);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAllUtilities(string UtilityCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetUtilityDetails", UtilityCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                string text = datatable.Rows[0]["Utility"].ToString();
                string text1 = datatable.Rows[0]["UtilityCode"].ToString();
            }
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetUtilityCredentials(string vendorCode, string utilityCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetUtilityCredentials", vendorCode, utilityCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SaveUtilityCredentials(string vendorCode, string utilityCode, string username, string password, string bankCode, string createdBy, DateTime createDate)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveUtilityCredentials", vendorCode, utilityCode, username, password, bankCode, createdBy, createDate);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void SaveUtilityDetails(UtilityDetails utility)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveUtilityDetails", utility.UtilityCode, utility.Utility, utility.UtilityContact, utility.Email, utility.CreatedBy, utility.CreationDate, utility.Active);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public ArrayList GetExcelColNames()
    {
        ArrayList excelCols = new ArrayList();
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetExcelColNames");
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    excelCols.Add(dr["ExcelColumnNames"].ToString().ToUpper().Trim());
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return excelCols;
    }

    public DataTable GetTranType()
    {

        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetTranType");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCompanies(int UserType)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCompanies", UserType);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable GetDuplicateNumber(string Phone)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetDuplicateNumber", Phone);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomerByEmail(object Email)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerByEmail", Email);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomerTypes()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerTypes");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomerByID(string ID)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerByID", ID);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetCustomerByCode(string Code)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerByCode", Code);
            procommand.CommandTimeout = 720;
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //public void SaveUser(Customer customer, bool p)
    //{
    //    throw new Exception("The method or operation is not implemented.");

    public string InsertintoCustomersHolding(PegPayCustomer cust, bool Corporate, string type)
    {
        string status = "";
        try
        {
            if (Corporate)
            {
                procommand = MMoney_DB.GetStoredProcCommand("InsertintoCorporateCustomer", cust.ID, cust.FirstName, cust.Email, cust.Phone, cust.Address, cust.CustomerType, cust.RecordedBy, cust.RecordDate, cust.Active, cust.Password, cust.PegasusCharge, cust.ChargeType);
            }
            else
            {
                procommand = MMoney_DB.GetStoredProcCommand("InsertintoRetailCustomer", cust.ID, cust.FirstName, cust.LastName, cust.Email, cust.Phone, cust.Address, cust.CustomerType,
                cust.Gender, cust.PlaceOfWork, cust.WorkId, cust.PassportNo, cust.DrivingPermit, cust.RecordedBy, cust.RecordDate, cust.Active, cust.Password, cust.PegasusCharge, cust.ChargeType);
            }
            MMoney_DB.ExecuteNonQuery(procommand);
            status = "OK";
            return status;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string ApproveCustomer(int CustomerId, string ApprovedBy)
    {
        string status = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("ApproveCustomer", CustomerId, ApprovedBy);
            MMoney_DB.ExecuteNonQuery(procommand);
            status = "SUCCESS";
            return status;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomersToApprove(PegPayCustomer cust)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomersToApprove", cust.Fullname, cust.CustomerType);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetBeneficiaryTypes()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetBeneficiaryTypes");
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomerBeneficiaryDetails(string Code)
    {

        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerBeneficiaryDetails", Code);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCorporations(string BranchCode)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public void SaveCustomerBeneficiaryforApproval(Beneficiary beneficiary)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveCustomerBeneficiaryforApproval", beneficiary.RecordCode, beneficiary.Mobile, beneficiary.Name,
                 beneficiary.CustomerCode, beneficiary.CustomerId, beneficiary.TypeCode, beneficiary.Email, beneficiary.Location, beneficiary.Active, beneficiary.RecordedBy, beneficiary.NetworkCode, beneficiary.Title, beneficiary.TelRegisteredNumber);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SaveCustomerBeneficiary(Beneficiary beneficiary)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveCustomerBeneficiary", beneficiary.RecordCode, beneficiary.Mobile, beneficiary.Name,
                 beneficiary.CustomerCode, beneficiary.CustomerId, beneficiary.TypeCode, beneficiary.Email, beneficiary.Location, beneficiary.Active, beneficiary.RecordedBy, beneficiary.NetworkCode, beneficiary.Title,beneficiary.TelRegisteredNumber);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SaveApprovedCustomerBeneficiary(Beneficiary beneficiary)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveApprovedCustomerBeneficiary", beneficiary.RecordCode, beneficiary.Mobile, beneficiary.Name,
                 beneficiary.CustomerCode, beneficiary.CustomerId, beneficiary.TypeCode, beneficiary.Email, beneficiary.Location, beneficiary.Active, beneficiary.RecordedBy, beneficiary.NetworkCode, beneficiary.Title);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomersByName(string CustomerCode, string customerName, string CustomerType)
    {


        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomersByName", CustomerCode, customerName, CustomerType);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetTransCategories()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetTransCategories");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomerBeneficaries(string name, string phone, string benType, string CustomerCode, string Location)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerBeneficaries2", name, phone, benType, CustomerCode, Location);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetCustomerBeneficariesUpdatedToApprove(string name, string phone, string benType, string CustomerCode, string Location)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerBeneficariesUpdatedToApprove", name, phone, benType, CustomerCode, Location);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetCustomerBeneficariesToApprove(string name, string phone, string benType, string CustomerCode, string Location)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerBeneficariesToApprove", name, phone, benType, CustomerCode, Location);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetAccountBalance(string CustomerAccount)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAccountBalance", CustomerAccount);
            procommand.CommandTimeout = 720;
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetMinimumBalance(string CustomerTypeCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetMinimumBalance", CustomerTypeCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable CheckActiveBeneficiary(Beneficiary beneficiary)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("CheckActiveCustomerBeneficiary", beneficiary.Code, beneficiary.CustomerCode, beneficiary.TypeCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable CheckBeneficiary(Beneficiary beneficiary)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("CheckCustomerBeneficiary", beneficiary.Mobile, beneficiary.CustomerCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string DeductCustomerAccount(string CustomerAccount, double TranAmount)
    {
        string status = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("DeductCustomerAccount", CustomerAccount, TranAmount);
            MMoney_DB.ExecuteNonQuery(procommand);
            status = "SUCCESS";
            return status;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetBeneficiaryDetails(Beneficiary ben)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetBeneficiaryDetails", ben.Code, ben.CustomerCode, ben.TypeCode);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public string UpdateCustomerAccountBalance(string CustomerAccount, double totalbalance)
    {
        string status = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("UpdateCustomerAccountBalance", CustomerAccount, totalbalance);
            MMoney_DB.ExecuteNonQuery(procommand);
            status = "SUCCESS";
            return status;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string SaveTransferFileRecord(Beneficiary beneficiary)
    {
        string PaymentNo = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveTransferFileRecord", beneficiary.CustomerCode, beneficiary.Name, beneficiary.Mobile, beneficiary.TransferAmount, beneficiary.PaymentDate, beneficiary.BatchCode, beneficiary.TransferType, beneficiary.RecordedBy,
                beneficiary.CashOutCharge, beneficiary.PegasusCharge);
            DataSet ds = MMoney_DB.ExecuteDataSet(procommand);
            int recordCount = ds.Tables[0].Rows.Count;
            if (recordCount != 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                PaymentNo = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return PaymentNo;
    }

    public string CreateTransferBatch(string CustomerCode, string TypeCode, double total, string StatusCode, string LevelId, string RecordedBy, string PaymentReason, string verifier, string transCat)
    {
        string BatchCode = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("CreatedTransferBatch2", CustomerCode, TypeCode, total, StatusCode, LevelId, RecordedBy, PaymentReason, verifier, transCat);
            DataSet ds = MMoney_DB.ExecuteDataSet(procommand);
            int recordCount = ds.Tables[0].Rows.Count;
            if (recordCount != 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                BatchCode = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return BatchCode;
    }

    public DataTable GetBatchforApproval(string CustomerCode, string StatusCode, string TypeCode, string LevelCode, DateTime StartDate, DateTime EndDate, string verifier)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetBatchesVerifier", CustomerCode, StatusCode, TypeCode, LevelCode, StartDate, EndDate, verifier);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetBatchforAuthorization(string CustomerCode, string StatusCode, string TypeCode, string LevelCode, DateTime StartDate, DateTime EndDate, string verifier)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetBatchesAuthorization", CustomerCode, StatusCode, TypeCode, LevelCode, StartDate, EndDate, verifier);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetFileLevels()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetFileLevels");
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable CheckLevelBelow(string LevelCode, string RoleCode, string CustomerId)
    {

        try
        {
            int LevelID = Convert.ToInt16(LevelCode);
            int LevelBelow = LevelID - 1;
            procommand = MMoney_DB.GetStoredProcCommand("CheckLevelBelow", CustomerId, RoleCode, LevelBelow);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetBatchRecords(string BatchCode, string CustomerCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetBatchDetails2", BatchCode, CustomerCode);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetBatchRecords1(string BatchCode, string CustomerCode, string status, string chargetype, double charge)
    {
        //Herbert
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetBatchDetails1", BatchCode, CustomerCode, status, chargetype, charge);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetRejectedBatches(string CustomerCode, string StatusCode, string verifier)
    {
        try
        {
            // int CorporationID = Convert.ToInt32(CorporationCode);

            procommand = MMoney_DB.GetStoredProcCommand("GetRejectedBatchesVerifier", CustomerCode, StatusCode, verifier);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public DataTable GetBatchLogs(string BatchCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetBatchLogs", BatchCode);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable RejectedBatchComment(string BatchCode, string Status)
    {
        try
        {

            procommand = MMoney_DB.GetStoredProcCommand("GetBatchRejectComment", BatchCode, Status);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetBatchDetailsByCode(string BatchCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetBatchDetailsByCode", BatchCode);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable CheckOtherLevels(string LevelCode)
    {
        try
        {

            string CustomerCode = HttpContext.Current.Session["CustomerCode"].ToString();
            string RoleCode = HttpContext.Current.Session["RoleCode"].ToString();
            procommand = MMoney_DB.GetStoredProcCommand("CheckOtherLevels", LevelCode, RoleCode, CustomerCode);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LogBatchTransaction(string BatchCode, string StatusTo, string LevelID, string Comment)
    {
        try
        {
            string RecordedBy = HttpContext.Current.Session["UserName"].ToString();
            procommand = MMoney_DB.GetStoredProcCommand("LogBatchTransaction", BatchCode, StatusTo, Comment, LevelID, RecordedBy);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string ApproveUpdatedCustomerBeneficiary(int BeneficairyId)
    {
        string Status = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("ApproveUpdatedCustomerBeneficiary", BeneficairyId);
            MMoney_DB.ExecuteNonQuery(procommand);
            Status = "SUCCESS";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Status;
    }

    public string ApproveCustomerBeneficiary(int BeneficairyId)
    {
        string Status = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("ApproveCustomerBeneficiary", BeneficairyId);
            MMoney_DB.ExecuteNonQuery(procommand);
            Status = "SUCCESS";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Status;
    }

    public string RejectCustomerBeneficiary(int BeneficairyId)
    {
        string Status = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("RejectCustomerBeneficiary", BeneficairyId);
            MMoney_DB.ExecuteNonQuery(procommand);
            Status = "SUCCESS";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Status;
    }

    public void LogBatchAccount(string BatchCode, string StatusTo, string LevelID, string Accountfrom, string Comment)
    {
        try
        {
            string RecordedBy = HttpContext.Current.Session["UserName"].ToString();
            procommand = MMoney_DB.GetStoredProcCommand("LogBatchAccount", BatchCode, StatusTo, Comment, LevelID, Accountfrom, RecordedBy);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void UpdateAuthorizer(string BatchCode, string authorizer)
    {
        try
        {
            string RecordedBy = HttpContext.Current.Session["UserName"].ToString();
            procommand = MMoney_DB.GetStoredProcCommand("UpdateAuthorizer", BatchCode, authorizer);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomerAccountInfor(string CustomerCode, string Account)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerAccountsInfor", CustomerCode, Account);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomerAccounts(string CustomerCode)
    {
        try
        {
            DbCommand procommand = MMoney_DB.GetStoredProcCommand("GetCustomerAccounts", CustomerCode);
            procommand.CommandTimeout = 0;
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomerAccountsTypes()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerAccountsType");
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAvailableVerifiers(string companyCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAvailableVerifiers", companyCode);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAvailableAuthorizers(string companyCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAvailableAuthorizers", companyCode);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void UpdatePaymentSchedule(string batchCode, DateTime date, string Scheduledby)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("UpdatePaymentSchedule", batchCode, date, Scheduledby);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetTelecomCredentials(string ToTelecom)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetTelecomCredentials", ToTelecom);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAccountDetailsByName(string name, string CustomerCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAccountDetailsByName", name, CustomerCode);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetVendorCredentials(string vendorCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetVendorCredentials", vendorCode);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void UpdateTranferfileStatus(object PaymentNo, string Status, string Reason)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("UpdateTranferfileStatus", PaymentNo, Status, Reason);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomerPayments(string benName, string benContact, string benType, string PayTye, DateTime fromdate, DateTime todate, string ChargeType, double charge)
    {
        try
        {
            string CustomerCode = HttpContext.Current.Session["CustomerCode"].ToString();
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerPayments2", CustomerCode, benName, benContact, benType, PayTye, fromdate, todate, ChargeType, charge);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetBatchStatuses()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetBatchStatuses");
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomerBatches(string CustomerCode, string TypeCode, string BatchNo, string StatusCode, DateTime StartDate, DateTime EndDate)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerBatches", CustomerCode, StatusCode, TypeCode, BatchNo, StartDate, EndDate);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetScheduledBatches(string CustomerCode, string TypeCode, string BatchNo, object StatusCode, bool paused, DateTime StartDate, DateTime EndDate)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetScheduledBatches", CustomerCode, StatusCode, TypeCode, BatchNo, paused, StartDate, EndDate);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable GetCustomerUserByRole(string CustomerCode, string RoleCode, string LevelCode)
    {
        try
        {

            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerUserByRole", LevelCode, RoleCode, CustomerCode);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetFromAccount(string CustomerCode, string ToTelecom, string AccountType)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetFromAccount", CustomerCode, ToTelecom, AccountType);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetPegasusOvaCompanies()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetPegasusOvaCompanies");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetSystemCompanies(string CompanyName, string CompanyCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetSystemCompanies", CompanyName, CompanyCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetSystemCompanyAccounts(string CompanyName, string CompanyCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetSystemCompanyAccounts", CompanyName, CompanyCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAllPegasusAccountNames(string PegasusAccountNames)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetDifferentPegasusAccountNames", PegasusAccountNames);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetNetworkInMobileMoneyDB()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAllMobileMoneyNetworks");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            //GetNetworkInMobileMoneyDB();
            throw ex;
        }
    }

    public DataTable GetVendorsInDB()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAllVendorsWithCredentials");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            //GetNetworkInMobileMoneyDB();
            throw ex;
        }
    }

    public DataTable GetNetworkInMobileMoneyDBNew(string NetworkCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAllMobileMoneyNetworksNew", NetworkCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            //GetNetworkInMobileMoneyDB();
            throw ex;
        }
    }
    public int UpdateTransactionWithTelecomId(string network, string pegpayTranId, string telecomid)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("UpdateTransactionWithTelecomId", network, pegpayTranId, telecomid);
            procommand.CommandTimeout = 120;
            updateTransactionsuccess = MMoney_DB.ExecuteNonQuery(procommand);
            return updateTransactionsuccess;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCompanyByCode(string CompanyCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCompanyByCode", CompanyCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAccountDetailsById(int Id)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAccountDetailsById", Id);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable CheckAccountDetails(string AccountNumber)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("CheckAccountDetails", AccountNumber);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SaveAccountDetails(int recordId, string CompanyCode, string AccountName, string AccountNumber, string AccountType, string Network, bool Active, string RecordedBy)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveAccountDetails", recordId, CompanyCode, AccountName, AccountNumber, AccountType, Network, Active, RecordedBy);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetDistinctNetwork()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetDistinctNetwork");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void ExcludeBatchPayment(int PaymentsId)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("ExcludeBatchPayment", PaymentsId);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetSubServices(string service)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetSubServices", service);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetSubServicesById(int recordId)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetSubServicesById", recordId);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void logSMS(string Phone, string message, string Mask, string sender)
    {
        try
        {

            procommand = MMoney_DB.GetStoredProcCommand("InsertSmsToSend", Phone, message, Mask, sender);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetPegPayAccount(string CompanyCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetPegPayAccount", CompanyCode);
            procommand.CommandTimeout = 3600;
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public string GetVendorCodeEmail(string CompanyVendorCode)
    {
        string email = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetVendorDetails", CompanyVendorCode);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            if (dt.Rows.Count > 0)
            {
                email = dt.Rows[0]["VendorEmail"].ToString();
                return email;
            }
            else
            {
                procommand = MMoney_DB.GetStoredProcCommand("GetCustomerBatchDetails", CompanyVendorCode);
                DataTable dt2 = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
                email = dt2.Rows[0]["Email"].ToString();
                return email;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return email;
    }

    internal void CreditAccountWithTelecomId(string CompanyCode, string Account, double Amount, string RecordedBy, string Network, string TelecomId,string Reason)
    {
        try
        {

            //procommand = MMoney_DB.GetStoredProcCommand("CreditAccount", Account, Amount);
            //MMoney_DB.ExecuteNonQuery(procommand);     
            procommand = MMoney_DB.GetStoredProcCommand("CreditAccountHoldingsWithTelecomIdWithReason", CompanyCode, Account, Amount, Network, RecordedBy, TelecomId,Reason);
            MMoney_DB.ExecuteNonQuery(procommand);


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void CreditAccount(string CompanyCode, string Account, double Amount, string RecordedBy, string Network)
    {
        try
        {

            //procommand = MMoney_DB.GetStoredProcCommand("CreditAccount", Account, Amount);
            //MMoney_DB.ExecuteNonQuery(procommand);
            procommand = MMoney_DB.GetStoredProcCommand("CreditAccountHoldings", CompanyCode, Account, Amount, Network, RecordedBy);
            MMoney_DB.ExecuteNonQuery(procommand);


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable GetCashOutCharge(double Amount, string Network)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCashOutCharge", Amount, Network);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCreditsToApprove(string CustName, string CustAccount, DateTime fromDate, DateTime toDate)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCreditsToApprove", CustName, CustAccount, fromDate, toDate);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable GetCreditToApproveById(int recordId)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCreditToApproveById", recordId);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void UpdateApprovedCredit(int RecordId, string username)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("UpdateApprovedCredit", RecordId, username);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public string UpdateRejectedCredit(int RecordId, string username)
    {
        int UpdateTransactionsuccess = 0;
        string results = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("UpdateRejectedCredit", RecordId, username);
            UpdateTransactionsuccess = MMoney_DB.ExecuteNonQuery(procommand);
            //UpdateTransactionsuccess = PegPay_DB.ExecuteNonQuery(procommand);
            results = "SUCCESSFUL";


            //return UpdateTransactionsuccess;
        }
        catch (Exception ex)
        {
            results = ex.ToString();
            UpdateTransactionsuccess = 0;

        }
        if (UpdateTransactionsuccess == 1 && results.Equals("SUCCESSFUL"))
        {

            //results = "SUCCESSFUL";

            return results;

        }

        else
        {

            return results;
        }
    }

    //internal void reverseTransaction(string PegPayId)
    //{
    //    try
    //    {
    //        procommand = MMoney_DB.GetStoredProcCommand("Reversetransaction", PegPayId);
    //        MMoney_DB.ExecuteNonQuery(procommand);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    internal void reverseTransaction(string PegPayId)
    {
        try
        {
            string Reason = "MANUALLY REVERSED OFF THE WEB PORTAL";
            string TelecomID = "";
            procommand = MMoney_DB.GetStoredProcCommand("ReverseTransactionWithReason2", PegPayId, Reason, TelecomID);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void ResendTransaction(string recordId)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("ResendTransaction", recordId);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public DataTable GetMNOCashOutFees()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetMNOCashOutFees");
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetTranById(string PegpayId)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetTranById", PegpayId);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    internal DataTable GetCustomerName(string VendorCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerName", VendorCode);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable GetTranCashoutFee(string VendorTranId)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetTranCashoutFee2", VendorTranId);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void UpdateTransactionStatus(string PegPayId, string MOMTransactionID, string Status, string Narration, string statusCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("UpdateTransactionStatus", PegPayId, MOMTransactionID, Status, Narration, statusCode);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    internal void UpdateFailedBatchTran(string paymentnum, string batchnum)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("UpdateFailedBatchTran", paymentnum, batchnum);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public void LogError(string vendorCode, string fromAccount, string toAccount, string vendorTranId, string errorMessage, string SatusCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("LogError", vendorCode, fromAccount, toAccount, vendorTranId, errorMessage, SatusCode);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LogError2(string vendorCode, string fromAccount, string toAccount, string vendorTranId, string errorMessage, string SatusCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("LogError", vendorCode, fromAccount, toAccount, vendorTranId, errorMessage, SatusCode);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
        }
    }

    public DataTable GetChargeTypes()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetChargeTypes");
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable GetVendorPegPayAccount(string VendorCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetPegPayAccount", VendorCode);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal string SaveCustomerKYC(CustomerKYC cusKyc)
    {
        string status = "";
        try
        {
            procommand = POS_DB.GetStoredProcCommand("SaveCustomerKYC", cusKyc.Vendorcode, cusKyc.Fname, cusKyc.Lname, cusKyc.OtherName, cusKyc.DateofBirth, cusKyc.Contact1, cusKyc.Contact2, cusKyc.Gender, cusKyc.Nationality, cusKyc.Address, cusKyc.Email, cusKyc.CustomerType, cusKyc.BusinessType, cusKyc.TradingName, cusKyc.CompanyRegNo, cusKyc.CompanyTin, cusKyc.Region, cusKyc.District, cusKyc.CustomerIdType, cusKyc.CustomerIdNo, cusKyc.Isactive, cusKyc.CreatedBy);
            POS_DB.ExecuteNonQuery(procommand);
            status = "OK";
            return status;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void SaveCustomerCredentials(string VendorCode, string Password)
    {
        try
        {
            procommand = POS_DB.GetStoredProcCommand("SaveCustomerCredentials", VendorCode, Password);
            POS_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetPosCustomerTypes()
    {
        try
        {
            procommand = POS_DB.GetStoredProcCommand("GetPosCustomerTypes");
            DataTable dt = POS_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetBusinessTypes()
    {
        try
        {
            procommand = POS_DB.GetStoredProcCommand("GetBusinessTypes");
            DataTable dt = POS_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetIdTypes()
    {
        try
        {
            procommand = POS_DB.GetStoredProcCommand("GetIdTypes");
            DataTable dt = POS_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomerKYC(Vendor vendor)
    {
        try
        {
            procommand = POS_DB.GetStoredProcCommand("GetCustomerKYC", vendor.VendorName, vendor.Active);
            DataTable dt = POS_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomerDevices(string AgentCode)
    {
        try
        {
            procommand = POS_DB.GetStoredProcCommand("GetCustomerDevices", AgentCode);
            DataTable dt = POS_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable GetDeviceById(string AgentId)
    {
        try
        {
            procommand = POS_DB.GetStoredProcCommand("GetDeviceById", AgentId);
            DataTable dt = POS_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void SaveDeviceDetails(string RecordId, string AgentId, string AgentName, string AgentAddress, string OwnerId, bool Active, string DeviceSerial, string DeviceDataSim, string DeviceType, string createdBY, string AgentTel)
    {
        int isactive = 0;
        if (Active)
        {
            isactive = 1;
        }
        try
        {
            procommand = POS_DB.GetStoredProcCommand("SaveDeviceDetails", RecordId, AgentId, AgentName, AgentAddress, OwnerId, isactive, DeviceSerial, DeviceDataSim, DeviceType, createdBY, AgentTel);
            POS_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomerKycById(string RecordId)
    {
        try
        {
            procommand = POS_DB.GetStoredProcCommand("GetCustomerKycById", RecordId);
            DataTable dt = POS_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustomerDeviceById(string RecordId)
    {
        try
        {
            procommand = POS_DB.GetStoredProcCommand("GetCustomerDeviceById", RecordId);
            DataTable dt = POS_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void ReverseTransaction(string PegPayId,  string Reason)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("Reversal", PegPayId, Reason);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable CheckAtPagasusPushPull(string vendorref)
    {
        DataTable dt1 = new DataTable();
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("CheckAtPagasusPushPull", vendorref);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            if (dt.Rows.Count > 0)
            {
                dt1 = dt;
            }
            else
            {
                procommand = UtilitiesDB.GetStoredProcCommand("CheckAtUtilitiesPushPull", vendorref);
                DataTable Utilitiesdt = UtilitiesDB.ExecuteDataSet(procommand).Tables[0];
                dt1 = Utilitiesdt;
            }
        }
        catch (Exception ex)
        {
        }
        return dt1;
    }

    internal DataTable CheckAtPagasus(string vendorref)
    {
        DataTable dt1 = new DataTable();
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("CheckAtPagasus", vendorref);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];

            dt1 = dt;
        }
        catch (Exception ex)
        {
        }
        return dt1;
    }
    public string GetSystemParameters(int GroupCode, int ValueCode)
    {
        string ret = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetSystemSetting", GroupCode, ValueCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            if (datatable.Rows.Count > 0)
            {
                ret = datatable.Rows[0]["ValueVarriable"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ret;
    }
    internal DataTable GetVendorSenderId(string VendorCode, string Network)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetVendorSenderId", VendorCode, Network);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAirtelPullAccountDetails(string VendorCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAirtelPullAccountDetails", VendorCode);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void SaveUploadedFilesDetail(string pegasusfilepath, string clientfilepath, string user, string SessionEmail, int Reconciledflag)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SavePegasusClientFilePathDetails", pegasusfilepath, clientfilepath, user, SessionEmail, Reconciledflag);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public CustomerReceiptCreditDetails GetCustomerReceiptDetails(int recordId)
    {
        CustomerReceiptCreditDetails cust = new CustomerReceiptCreditDetails();
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerReceiptDetails", recordId);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            if (dt.Rows.Count > 0)
            {
                cust.CustomerCode = dt.Rows[0]["CustomerCode"].ToString();
                cust.CustomerAccount = dt.Rows[0]["CustomerAccount"].ToString();
                cust.CustomerCreditAmount = dt.Rows[0]["CreditAmount"].ToString();
                //cust.Balance = dt.Rows[0]["AccountBal"].ToString();
                //cust.ReceiptNumber = recordId;
                cust.ReceiptNumber = GetPegPayTranId(cust.CustomerCode, cust.CustomerCreditAmount, recordId);
                cust.StatusCode = "0";
                cust.StatusDescription = "SUCCESS";
            }
            else
            {
                cust.StatusCode = "100";
                cust.StatusDescription = "INVALID CUSTOMER CODE.";
            }
        }
        catch (Exception ex)
        {
            GetCustomerReceiptDetails(recordId);
            throw ex;
        }
        return cust;
    }
    internal int GetPegPayTranId(string VendorCode, string Amount, int recordId)
    {
        int pegpayid = 0;
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerReceiptPegPayTranldDetails", VendorCode, Amount, recordId);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            if (dt.Rows.Count > 0)
            {
                pegpayid = int.Parse(dt.Rows[0]["PegPayTranId"].ToString());
            }

            else
            {

                pegpayid = recordId;
            }


        }
        catch (Exception ex)
        {
            GetPegPayTranId(VendorCode, Amount, recordId);
        }
        return pegpayid;
    }
    public BatchCustomerDetails GetCustomerBatchDetails(string customercode)
    {
        BatchCustomerDetails batchcust = new BatchCustomerDetails();
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerBatchDetails", customercode);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            if (dt.Rows.Count > 0)
            {
                batchcust.CustomerName = dt.Rows[0]["Fname"].ToString();
                batchcust.CustomerEmail = dt.Rows[0]["Email"].ToString();
                batchcust.StatusCode = "0";
                batchcust.StatusDescription = "SUCCESS";
            }
            else
            {
                batchcust.StatusCode = "100";
                batchcust.StatusDescription = "INVALID CUSTOMER CODE.";
            }
        }
        catch (Exception ex)
        {
            GetCustomerBatchDetails(customercode);
            throw ex;
        }
        return batchcust;
    }

    public void SaveUploadedTelecomReconFileDetail(string telecomfilepath, string user, string SessionEmail, int Reconciledflag, string vendorcode, string ova, string fromDate, string toDate)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("TelecomReconFilePathDetails_Insert", telecomfilepath, user, SessionEmail, Reconciledflag, vendorcode, ova, fromDate, toDate);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void ResetTransactionStatusBackToUnsentStatus(string recordId)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("ResetTransactionStatusToUnsent", recordId);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void UpdateFailedStanbicVasTransaction(string vendorID, string status, string utilityTranRef)
    {
        try
        {
            procommand = UtilitiesDB.GetStoredProcCommand("UpdateFailedStanbicVasTransaction", vendorID, status, utilityTranRef);
            UtilitiesDB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {

        }
    }

    internal DataTable GetChargeType(string customerCode)
    {
        try
        {
            customerCode = HttpContext.Current.Session["CustomerCode"].ToString();
            procommand = MMoney_DB.GetStoredProcCommand("GetChargeType", customerCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return datatable;
    }

    public DataTable GetAllStanbicVasUtilities()
    {
        try
        {
            procommand = UtilitiesDB.GetStoredProcCommand("GetAllStanbicVasUtilities");
            datatable = UtilitiesDB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SaveUploadedVASReconFileDetail(string telecomfilepath, string user, string SessionEmail, int Reconciledflag, string vendorcode)
    {
        try
        {
            procommand = UtilitiesDB.GetStoredProcCommand("SaveUploadedVASReconFileDetails", telecomfilepath, user, SessionEmail, Reconciledflag, vendorcode);
            UtilitiesDB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal string ActivateAcc(string Phone, string pin)
    {
        string res = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("ActivateAcc", Phone, pin);
            MMoney_DB.ExecuteNonQuery(procommand);
            res = "Activated";

        }
        catch (Exception ex)
        {
            res = "NotActivated";
        }
        return res;
    }
    internal void DebitClientAccountBalanceWithAmount(string TxnVendorTranld, string VendorCode, string AgentAccountNumber, double Amount, double PegasusAccountNumber, string PegasusAccountNetwork)
    {//agent account number parameter was double, but some accts are alphanumeric
        try
        {
            string CustName = VendorCode + "  Account Debit";
            DateTime todaydate = DateTime.Now;
            // string datetoday = todaydate.ToString("yy-MM-dd hh:mm:ss").Replace("/", "").Replace(":", "").Replace(" ", "").Replace("-", "");
            string VendorTranId = TxnVendorTranld;
            string TranCharge = "0";
            //string datetodayPaymentDate = todaydate.ToString("dd-MM-yy hh:mm:ss");
            string datetodayPaymentDate = todaydate.ToString("yyyy-MM-dd hh:mm:ss");
            string PaymentDate = datetodayPaymentDate;
            string PaymentType = "1";
            string TranType = "2";
            string Phone = "";
            string MNOCharge = "0";
            string CashOutCharge = "0";
            string Charge1 = "0";
            string Charge2 = "0";
            string PegasusCommisionAccount = "";
            string TelecomCommissionAccount = "";
            string CashoutAccount = "";

            procommand = MMoney_DB.GetStoredProcCommand("InsertDebitClient",
                AgentAccountNumber, PegasusAccountNumber, CustName, VendorTranId,
                Amount, TranCharge, PegasusAccountNetwork, PegasusAccountNetwork,
               PaymentDate, PaymentType, TranType, VendorCode, Phone,
               MNOCharge, CashOutCharge, Charge1, Charge2,
               PegasusCommisionAccount, TelecomCommissionAccount, CashoutAccount);


            MMoney_DB.ExecuteNonQuery(procommand);


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public CustomerReceiptDebitDetails GetCustomerReceiptDebitDetails(string VendorTranId, string VendorCode, string validAgentAccountNumber,
     double validAgentdebitAmount, double validPegasusAccountNumber, string PegasusAccountNetwork)
    {
        CustomerReceiptDebitDetails cust = new CustomerReceiptDebitDetails();
        CustomerReceiptDebitDetails newcust = new CustomerReceiptDebitDetails();
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetCustomerReceiptDebitDetails", VendorTranId, VendorCode,
                validAgentAccountNumber, validAgentdebitAmount, validPegasusAccountNumber, PegasusAccountNetwork);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            if (dt.Rows.Count > 0)
            {
                cust.CustomerCode = dt.Rows[0]["VendorCode"].ToString();
                cust.CustomerDebitAmount = dt.Rows[0]["TranAmount"].ToString();
                cust.ReceiptNumber = int.Parse(dt.Rows[0]["PegPayTranId"].ToString());
                cust.PegasusAccountNumber = dt.Rows[0]["ToAccount"].ToString();
                //cust.
                cust.PegasusAccountNumberNetwork = dt.Rows[0]["ToNetwork"].ToString();
                newcust = GetCurrentPegasusAccountBalanceAndAccountName("PEGASUS", cust.PegasusAccountNumber, cust.PegasusAccountNumberNetwork);



                cust.PegasusAccountName = newcust.PegasusAccountName;
                cust.CurrentPegasusAccountBalance = newcust.CurrentPegasusAccountBalance;
                cust.StatusCode = "0";
                cust.StatusDescription = "SUCCESS";
            }
            else
            {
                cust.StatusCode = "100";
                cust.StatusDescription = "INVALID CUSTOMER CODE.";
            }
        }
        catch (Exception ex)
        {
            GetCustomerReceiptDebitDetails(VendorTranId, VendorCode, validAgentAccountNumber,
         validAgentdebitAmount, validPegasusAccountNumber, PegasusAccountNetwork);
        }
        return cust;
    }

    private CustomerReceiptDebitDetails GetCurrentPegasusAccountBalanceAndAccountName(string Code, string AccountNumber, string AccountNetwork)
    {
        CustomerReceiptDebitDetails cust = new CustomerReceiptDebitDetails();

        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetPegasusAccountBalanceDetails", Code, AccountNumber, AccountNetwork);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            if (dt.Rows.Count > 0)
            {
                cust.CurrentPegasusAccountBalance = double.Parse(dt.Rows[0]["AccountBalance"].ToString());
                cust.PegasusAccountName = dt.Rows[0]["AccountName"].ToString();
            }
            else
            {
                cust.StatusCode = "100";
                cust.StatusDescription = "INVALID CUSTOMER CODE.";
            }
        }
        catch (Exception ex)
        {
            GetCurrentPegasusAccountBalanceAndAccountName(Code, AccountNumber, AccountNetwork);
            //throw ex;
        }
        return cust;
    }
    public DataTable GetPegasusAccountNameDetails(string PegasusAccountName)
    {
        DataTable dt1 = new DataTable();
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetPegasusAccountNameDetails", PegasusAccountName);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            dt1 = dt;

        }
        catch (Exception ex)
        {
            GetPegasusAccountNameDetails(PegasusAccountName);
        }
        return dt1;
    } //GetPegasusAccountNameDetails
    public DataTable GetDuplicateDebitClientAmount(string VendorCode, double AgentdebitAmount, double AgentAccountNumber, double PegasusAccountNumber, string PegasusAccountNetwork, DateTime postDate)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetDuplicateDebitClientAmount", VendorCode, AgentdebitAmount, AgentAccountNumber, PegasusAccountNumber, PegasusAccountNetwork, PegasusAccountNetwork, postDate);
            DataTable returndetails = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return returndetails;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable CheckVASAirtelTran(string vendorref)
    {
        procommand = MMoney_DB.GetStoredProcCommand("CheckVASAirtelTran", vendorref);
        return datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
    }

    internal DataTable CheckVasTranAtPagasus(string vendorref)
    {
        DataTable dt1 = new DataTable();
        try
        {
            procommand = VAS_DB.GetStoredProcCommand("CheckVasTranAtPagasus", vendorref);
            DataTable dt = VAS_DB.ExecuteDataSet(procommand).Tables[0];
            if (dt.Rows.Count > 0) { dt1 = dt; } else { dt1 = CheckVASAirtelTran(vendorref); }

        }
        catch (Exception ex)
        {
        }
        return dt1;
    }

    public DataTable checkIfFirstRecord(int pegPayID, string vendorCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("checkIfFirstRecord", pegPayID, vendorCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return datatable;
    }

    internal void SavePOSAccountDetails(CustomerKYC kyc)
    {
        //string res = "";
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SavePOSAccountDetails", kyc.Vendorcode, kyc.Username, kyc.Password, kyc.Spid, "MTN");
            MMoney_DB.ExecuteNonQuery(procommand);
            //res = "OK";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        // return res;
    }

    public DataTable getPosAccountIfExist(string vendorCode)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("getPosAccountIfExist", vendorCode);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return datatable;
    }

    public List<DataTable> GenerateAccountStatement(string VendorCode, string FromDate, string ToDate)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GenerateAccountStatement", VendorCode, FromDate, ToDate);
            procommand.CommandTimeout = 0;
            DataSet ds = MMoney_DB.ExecuteDataSet(procommand);
            List<DataTable> results = GenerateRunningBalances(ds);
            return results;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //trnid,trnType,fromAccount,toAccount,amount,statust,
    //PegPayId,VendorTranId,VendorCode,Phone,RecordDate
    private List<DataTable> GenerateRunningBalances(DataSet ds)
    {
        List<DataTable> resultsSet = new List<DataTable>();
        DataTable dt = new DataTable();
        string OpeningBalString = ds.Tables[0].Rows[0][0].ToString();
        IntX OpeningBal = GetAmount(OpeningBalString);
        IntX RunningBal = OpeningBal;
        IntX ClosingBal = 0;
        dt.Columns.Add("TranId");
        dt.Columns.Add("RecordDate");
        dt.Columns.Add("VendorTranId");
        dt.Columns.Add("PegPayId");
        dt.Columns.Add("TranType");
        dt.Columns.Add("Amount");
        dt.Columns.Add("VendorCode");
        dt.Columns.Add("Phone");
        dt.Columns.Add("Status");
        dt.Columns.Add("RunningBal");

        DataRow openingRow = dt.NewRow();
        openingRow["TranType"] = "STATEMENT OPENING BALANCE";
        long OB = (long)OpeningBal;
        openingRow["RunningBal"] = "" + OB.ToString("#,##0"); 
        dt.Rows.Add(openingRow);

        DataTable TransactionsTable = ds.Tables[1];
        if (TransactionsTable.Rows.Count==0)
        {
            ClosingBal = OpeningBal;
        }
        foreach (DataRow dr in TransactionsTable.Rows)
        {
            DataRow newRow = dt.NewRow();
            newRow["TranId"] = dr["TranId"].ToString();
            newRow["RecordDate"] = dr["RecordDate"].ToString().Trim();
            newRow["VendorTranId"] = dr["VendorTranId"].ToString();
            newRow["PegPayId"] = dr["PegPayId"].ToString();
            string Amount = dr["Amount"].ToString();
            long lnAmount = (long)GetAmount(Amount);
            newRow["Amount"] = lnAmount.ToString("#,##0");
            newRow["VendorCode"] = dr["VendorCode"].ToString();
            newRow["Phone"] = dr["Phone"].ToString();
            newRow["Status"] = dr["TranStatus"].ToString();
            newRow["TranType"] = dr["TranType"].ToString();
            
            //generate running balance
            RunningBal = RunningBal + GetAmount(Amount);
            long lnRunBal = (long)RunningBal;
            newRow["RunningBal"] = "" + lnRunBal.ToString("#,##0");
            dt.Rows.Add(newRow);
            ClosingBal = RunningBal;
        }

        //Add closing balance as last row on DataTable
        DataRow closingRow = dt.NewRow();
        long CB = (long)ClosingBal;
        closingRow["TranType"] = "STATEMENT CLOSING BALANCE";
        closingRow["RunningBal"] = "" + CB.ToString("#,##0");
        dt.Rows.Add(closingRow);

        //return 3rd datatable having only closing balance
        DataTable closingBalTable = new DataTable();
        closingBalTable.Columns.Add("ClosingBal");
        DataRow row = closingBalTable.NewRow();
        row["ClosingBal"] = "" + ClosingBal;
        closingBalTable.Rows.Add(row);

        resultsSet.Add(ds.Tables[0]);
        resultsSet.Add(dt);
        resultsSet.Add(closingBalTable);
        return resultsSet;
    }

    public IntX GetAmount(string amount)
    {
        //Note: We use IntX because some account balances exceed the maximum for an Integer
        //maximum value for int32 is 2,147,483,647. Wave for example sometimes has balance greater than this
        //Maximu value for IntX is 18,446,744,073,709,551,615 (18 quintillion)
        IntX amt = 0;

        try
        {
            amt = IntX.Parse(amount.Split('.')[0]);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return amt;
    }

    public DataTable GetTransactionToReverse(string vendorCode, string pegpayId, string telecomId)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetTransactionToReverse", vendorCode, pegpayId, telecomId);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return datatable;
    }

    public bool ReverseSuccessFullTelecomTransaction(string pegpayId, string amount, string reason, string telecomId)
    {
        bool reversed = false;
        int value = -1;
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("ReversetransactionWithReason2WithEnteredAmount", pegpayId, amount, reason, telecomId);
            value = MMoney_DB.ExecuteNonQuery(procommand);//.Tables[0];
            if (value > 0)
            {
                reversed = true;
            }
        }
        catch (Exception ee)
        {

        }

        return reversed;
    }

    internal void UpdateStanbicTransaction(string VendorTranId, string UtilityTranRef, string Status)
    {
        try
        {
            procommand = VAS_DB.GetStoredProcCommand("UpdateSentTransactionStanbic", VendorTranId, UtilityTranRef, Status);
            VAS_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }

    public DataTable GetAgentsToApprove(string agentCode, DateTime fromDate, DateTime toDate)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetAgentsToApprove", agentCode, fromDate, toDate);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void UpdateApprovedAgents(int RecordId)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("UpdateApprovedAgent", RecordId);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public bool UpdateTransactionReversalStatuus(string pegpayId, string status, string reason, string userId)
    {
        bool reversed = false;
        int value = -1;
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("UpdateTransactionReversalStatuus", pegpayId, status, reason, userId);
            value = MMoney_DB.ExecuteNonQuery(procommand);
            if (value > 0)
            {

                reversed = true;
            }
        }
        catch (Exception ee)
        {

            throw ee;
        }
        return reversed;
    }
    public DataTable GetTransactionToInitiateReversal(string vendorCode, string vendorTranId)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetTransactionToInitiateReversal", vendorCode, vendorTranId);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return datatable;
    }

    public bool SaveReversalRequest(string companyCode, string vendorId, string telecomId, string pegpayId, string amount, string reason, string status, string paymentdate, string userId)
    {
        bool reversed = false;
        int value = -1;
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveReversalRequest", companyCode, vendorId, telecomId, pegpayId, amount, reason, status, paymentdate, userId);
            value = MMoney_DB.ExecuteNonQuery(procommand);//.Tables[0];
            if (value > 0)
            {
                reversed = true;
            }
        }
        catch (Exception ee)
        {

        }

        return reversed;
    }

    public DataTable getReversalRequest(string vendorcode, string status, string vendorref, string fromdate, string todate)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("getReversalRequest", vendorcode, vendorref, status, fromdate, todate);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return datatable;
    }

    internal DataSet GetMailingList(string vendorCode, string network, string type)
    {
        DataSet dset = new DataSet();
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetMailingList", vendorCode, network, type);
            dset = MMoney_DB.ExecuteDataSet(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dset;
    }

    public DataTable getReversalRequest2(string vendorcode, string vendorref, string telecomId)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("getReversalRequestMon", vendorcode, vendorref);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return datatable;
    }

    internal void LogLoginRequest(string username, string password, bool isValid, string ip)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("SaveLoginRequest", username, password, isValid, ip);
            MMoney_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal DataTable GetLoginAttempts(string username)
    {
        //DataSet dset = new DataSet();
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetLoginAttempts", username);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return datatable;
    }

    public DataTable getReversalsById(string reversalId)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("getReversalsById", reversalId);
            datatable = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return datatable;
    }

    public DataSet ExecuteDataSet(string procedure, params object[] parameters)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand(procedure, parameters);
            procommand.CommandTimeout = 300;
            return MMoney_DB.ExecuteDataSet(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetApprovedCredits(string CustName, string CustAccount, DateTime fromDate, DateTime toDate)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetApprovedCredits", CustName, CustAccount, fromDate, toDate);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int ExecuteNonQuery(string storedProcedureName, params object[] parameters)
    {
        try
        {
            return MMoney_DB.ExecuteNonQuery(storedProcedureName, parameters);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet ExecuteDataSetInUtilitiesDB(string storedProcedureName, params object[] parameters)
    {
        try
        {
            return UtilitiesDB.ExecuteDataSet(storedProcedureName, parameters);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void UpdateSchoolFeesOnlinePayments(string vendorId, string telecomid, string senttopegpay, string sentToTelecom, string status)
    {
        try
        {
            procommand = SchoolDB.GetStoredProcCommand("UpdateOnlinePayments", vendorId, telecomid, senttopegpay, sentToTelecom, status);
            SchoolDB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public DataTable GetOnlinePaymentTransaction(string vendorId, string tranAmount)
    {
        try
        {
            procommand = SchoolDB.GetStoredProcCommand("GetOnlinePaymentTransaction", vendorId, tranAmount);
            DataTable dt = SchoolDB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SaveStatementPath(string vendorcode, string ova, string telecomreconfile, string fromDate, string toDate, string user, string sessionEmail)
    {
        ExecuteNonQuery("Statements_Insert",
            vendorcode,
            ova,
            telecomreconfile,
            fromDate,
            toDate,
            user,
            sessionEmail
            );
    }
    public void SaveVendorStatementPath(string vendorcode, string vendorReconfile, string fromDate, string toDate, string user, string sessionEmail)
    {
        ExecuteNonQuery("VendorStatements_Insert",
            vendorcode,
            vendorReconfile,
            fromDate,
            toDate,
            user,
            sessionEmail
            );
    }
    public void SaveStatementPathOfArchieves(string vendorcode, string ova, string telecomreconfile, string fromDate, string toDate, string user, string sessionEmail)
    {
       procommand = MMoney_DBArc.GetStoredProcCommand("Statements_Insert", vendorcode,
            ova,
            telecomreconfile,
            fromDate,
            toDate,
            user,
            sessionEmail);
        MMoney_DBArc.ExecuteNonQuery(procommand);
    }
    public  DataTable GetOvaAccounts(string selector)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetOvaAccounts_Portals", selector);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        } 
    }

    internal DataTable GetEmailDetails(string fromEmail)
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetEmailDetails", fromEmail);
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        } 
    }

    public ArrayList GetHoursNotToCredit()
    {
        ArrayList badHours = new ArrayList();
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetHoursNotToCredit");
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int badHour = int.Parse(dr["Hour"].ToString());
                    badHours.Add(badHour);
                }
            }
            return badHours;
        }
        catch (Exception ex)
        {
            throw ex;
        } 
    }

    public ArrayList GetDaysNotToCredit()
    {
        ArrayList badDays = new ArrayList();
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetDaysNotToCredit");
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string badDay = dr["Day"].ToString();
                    badDays.Add(badDay);
                }
            }
            return badDays;
        }
        catch (Exception ex)
        {
            throw ex;
        } 
    }

    public DataTable GetRolePageMatrix()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetRolePageMatrix");
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal DataTable GetVendorsToReconcile()
    {
        try
        {
            procommand = MMoney_DB.GetStoredProcCommand("GetVendorsToReconcile");
            DataTable dt = MMoney_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
