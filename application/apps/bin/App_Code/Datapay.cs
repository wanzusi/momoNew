using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
/// <summary>
/// Summary description for Datapay
/// </summary>
public class Datapay
{
    private Database PegPay_DB;
    private DbCommand procommand;
    private DataSet returnDataset;
    private DataTable datatable;

    DataLogin con = new DataLogin();
    public Datapay()
    {
        //
        PegPay_DB = con.ShareBilling_Con();
        //
    }
    public DataTable GetPaymentBatches(DateTime fromDate, DateTime toDate, bool confirmed)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetPaymentBatches", confirmed, fromDate, toDate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //get the transfer batch details
    public DataTable GetTransferBatchDetails(string bacthNum, string recorded, DateTime fromDate, DateTime toDate, string stat, string customerCod)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetSearchedTransferBatchDetails", bacthNum, recorded, fromDate, toDate, stat, customerCod);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }






    //get failed transfer batch details
    public DataTable GetFailedTransferBatchDetails(string bacthNum, string recorded, DateTime fromDate, DateTime toDate, string stat, string customerCod)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetFailedTransferBatchDetails", bacthNum, recorded, fromDate, toDate, stat, customerCod);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetRevBatchDetails(string BatchCode)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetRevBatchDetails", BatchCode);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetTransforBatching(string vendorcode, string vendorref, string Account, string CustName, string Paymentcode,
       string teller, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetTransforBatching", vendorcode, vendorref, teller, Account,
                CustName, Paymentcode, fromdate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetRevTransforBatching(string vendorcode, string vendorref, string Account, string CustName, string Paymentcode,
   string teller, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetRevTransforBatching", vendorcode, vendorref, teller, Account,
                CustName, Paymentcode, fromdate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetTransToBin(string vendorcode, string vendorref, string Account, string CustName, string Paymentcode,
    string teller, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetTransToBin", vendorcode, vendorref, teller, Account,
                CustName, Paymentcode, fromdate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetBinnedTransactions(string vendorcode, string vendorref, string Account, string CustName, string Paymentcode, bool Reconciled,
  string teller, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetBinnedTransactions", vendorcode, vendorref, teller, Account,
                CustName, Paymentcode, Reconciled, fromdate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetReconciledTransToBin(string vendorcode, string vendorref, string Account, string CustName, string Paymentcode,
  string teller, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetReconciledTransToBin", vendorcode, vendorref, teller, Account,
                CustName, Paymentcode, fromdate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetTransToReconcile(string vendorcode, string vendorref, string telecomId, DateTime fromdate, DateTime todate, string Telecom, string TranType)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetTransToReconcile", vendorcode, vendorref, telecomId, fromdate, todate, Telecom, TranType);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetPendingTrans(string vendorcode, string vendorref, string telecomId, DateTime fromdate, DateTime todate, string Telecom, string TranType)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetPendingTrans", vendorcode, vendorref, telecomId, fromdate, todate, Telecom, TranType);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetPendingTrans3(string vendorcode, string vendorref, string telecomId, DateTime fromdate, DateTime todate, string Telecom, string TranType,string Status)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetPendingTrans3", vendorcode, vendorref, telecomId, fromdate, todate, Telecom, TranType,Status);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetPendingTransToRetry(string vendorcode, string vendorref, string telecomId, DateTime fromdate, DateTime todate, string Telecom, string TranType)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetDeletedTransactionsToRetry", vendorcode, vendorref, "", "", "", "", fromdate, todate, TranType);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }







    public DataTable GetPendAllTransactions(string vendorcode, string vendorref, string FromAccount, string CustName, string Paymentcode,
string ToAccount, DateTime fromdate, DateTime todate, string TranType, string network, string teleId)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetPendAllTransactions", vendorcode, vendorref, ToAccount, FromAccount,
                CustName, Paymentcode, fromdate, todate, TranType, network, teleId);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    //public int UpdateTransByID(Transaction2 tran)
    //{
    //    try
    //    {
    //        procommand = PegPay_DB.GetStoredProcCommand("UpdateTransByID", tran.PegID);
    //        int count = PegPay_DB.ExecuteNonQuery(procommand);
    //        return count;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public int UpdateTransByID(string RecordID)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("UpdateTransByID", RecordID);
            int count = PegPay_DB.ExecuteNonQuery(procommand);
            return count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public string UpdateFailedBatchTransaction(Transaction2 tran)
    {
        string ret = "";
        // int count = 0;
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("UpdateFailedBatchTransaction", tran.PegID);




            //procommand.CommandTimeout = 120;
            // DataSet ds = PegPay_DB.ExecuteDataSet(procommand);
            int count = PegPay_DB.ExecuteNonQuery(procommand);

            ret = "1";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ret;
        //return count.ToString();
    }


    public int countBatchRowsUpdatedID(Transaction2 tran)
    {

        int ret = 0;

        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("countBatchRowsUpdatedID",tran.getVendorTranId);




            //procommand.CommandTimeout = 120;
            // DataSet ds = PegPay_DB.ExecuteDataSet(procommand);
           // int count = PegPay_DB.ExecuteNonQuery(procommand);

            ret = PegPay_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ret;
    }


    public string BatchRowsUpdatedID(Transaction2 tran)
    {

       string ret = "";

        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("countBatchRowsUpdatedID",tran.getVendorTranId);



            int count = PegPay_DB.ExecuteNonQuery(procommand);

            ret = "1";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ret;
    }






    public int countUpdateTransByID(Transaction2 tran)
    {

        int ret = 0;

        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("UpdateTransByID", tran.PegID);


            int count = PegPay_DB.ExecuteNonQuery(procommand);

            ret = PegPay_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ret;
    }
















    public DataTable GetTransactionforReconciliation(string vendorref, string vendorcode)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetTransactionforReconciliation", vendorref, vendorcode);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal DataTable GetReconciledTransaction(string vendorref, string vendorcode)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetReconciledTransaction", vendorref, vendorcode);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetPostedTrans(string vendorcode, string vendorref, string Account, string CustName, string Paymentcode,
 string teller, DateTime fromdate, DateTime todate, string utility)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetPostedTrans", vendorcode, vendorref, teller, Account,
                CustName, Paymentcode, fromdate, todate, utility);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public string ReverseDeletedTransaction(Transaction2 tran)
    {
        string ret = "";
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("ReverseDeletedTransaction",

                        tran.getFromAccount,
                        tran.getToAccount,
                        tran.getCustName,
                        tran.getVendorTranId,
                        tran.getTranAmount,
                        tran.getTranCharge,
                        tran.getFromNetwork,
                        tran.getToNetwork,
                        tran.getPaymentDate,
                        tran.getPaymentType,
                        tran.GetTranType,
                        tran.getVendorCode,
                        tran.getPhone,
                        tran.getMNOCharge,
                        tran.getCashoutCharge,
                        0,
                        0,
                        tran.getPegasusCommisionAccount,
                        tran.getTelecomCommissionAccount,
                        tran.getCashoutAccount,
                        tran.getPegPayId,
                        tran.getRecordDate,
                        tran.getTelecomID,
                        tran.getTranStatus,
                        tran.getSentToVendorStatus,
                        "0"
                        );
            //procommand.CommandTimeout = 120;
            // DataSet ds = PegPay_DB.ExecuteDataSet(procommand);
            int count = PegPay_DB.ExecuteNonQuery(procommand);

            ret = "1";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ret;
    }
    public DataTable GetReconciledTrans(string vendorcode, string vendorref, string FromAccount, string CustName, string Paymentcode,
     string ToAccount, DateTime fromdate, DateTime todate, string TranType)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetReconciledTrans", vendorcode, vendorref, ToAccount, FromAccount,
                CustName, Paymentcode, fromdate, todate, TranType);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetUnReconciledTrans(string vendorcode, string vendorref, string FromAccount, string CustName, string Paymentcode,
   string ToAccount, DateTime fromdate, DateTime todate, string TranType)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetUnReconciledTrans", vendorcode, vendorref, ToAccount, FromAccount,
                CustName, Paymentcode, fromdate, todate, TranType);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetTransactionsbyRef(string vendorref, string FromAccount, string mreceiptno, string Receiptno)
    {
        try
        {
            if (Receiptno.Equals(""))
            {
                Receiptno = "0";
            }
            procommand = PegPay_DB.GetStoredProcCommand("GetTransactionsByRef", vendorref, Receiptno, FromAccount,
                mreceiptno);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetTransactionsbydistrict(string regioncode, string districtcode, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetTransactionByDistrict", regioncode, districtcode, fromdate,
                todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetDistrictTotalTrans(string status, string districtcode, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetDistrictTotalTrans", status, districtcode, fromdate,
                todate);
            procommand.CommandTimeout = 0;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetInterfaceErrorTable(string vendorcode, string utility, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetInterfaceErrorTable", vendorcode, utility, fromdate,
                todate);
            procommand.CommandTimeout = 0;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetAgentTotals(string status, string agentcode, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetAgentTotalTrans", status, agentcode, fromdate, todate);
            procommand.CommandTimeout = 0;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetCustomerFile(string accountno, string custname)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetCustomerFile", accountno, custname);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetCustomercontacts(string accountnumber, string phone, DateTime fromDate, DateTime toDate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetContacts", phone, accountnumber, fromDate, toDate);
            procommand.CommandTimeout = 0;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAirtelB2WTransactions(string vendorcode, string FromAccount, DateTime fromdate, DateTime todate, string TranType, string network, string CustomerTel,string VendorTranRef)
    {
        try
        {
          //  dataTable = datapay.GetAirtelB2WTransactions(vendorcode, FromAccount, fromdate, todate, TranType, network, CustomerTel);
            procommand = PegPay_DB.GetStoredProcCommand("GetAirtelB2WTransactions", vendorcode, FromAccount, fromdate, todate, TranType, network, CustomerTel, VendorTranRef);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAllTransactions(string vendorcode, string vendorref, string FromAccount, string CustName, string Paymentcode,
  string ToAccount, DateTime fromdate, DateTime todate, string TranType, string network, string teleId, string Phone)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetAllTransactions", vendorcode, vendorref, ToAccount, FromAccount,
                CustName, Paymentcode, fromdate, todate, TranType, network, teleId, Phone);
            procommand.CommandTimeout = 300;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetTransactions(string vendorcode, string vendorref, string FromAccount, string CustName, string Paymentcode,
  string ToAccount, DateTime fromdate, DateTime todate, string TranType, string network)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetTransactions", vendorcode, vendorref, ToAccount, FromAccount,
                CustName, Paymentcode, fromdate, todate, TranType, network);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string GetAccountNo(string vendorcode)
    {
        string No = "";
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetAccountNo", vendorcode);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];


            if (datatable.Rows.Count != 0)
            {
                No = datatable.Rows[0]["AccountNumber"].ToString();
            }
            else
            {
                No = "No account No.";
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return No;
    }


    public DataTable GetAccountStatements(string vendorcode, string CustName, DateTime fromdate, DateTime todate, string accNo)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetAccountStatements", vendorcode, CustName, fromdate, todate, accNo);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetPrepayments(string vendorcode, string meterno, string receiptno, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetPrepayments", receiptno, meterno, vendorcode, fromdate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetPaymentDetails(Responseobj res)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetPaymentDetails", res.VendorRef, res.Receiptno);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetPreReceiptDetails(Responseobj res)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetPreReceiptDetails", res.Receiptno, res.VendorRef);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetPayments(string districtcode, string Receiptno, string Paymentcode, string Paymode, string teller, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetPayments", districtcode, Receiptno, teller, Paymode, Paymentcode, fromdate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetPaymentsToReverse(string districtcode, string Receiptno, string Paymentcode, string Paymode, string teller, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetPaymentsToReverse", districtcode, Receiptno, teller, Paymode, Paymentcode, fromdate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal DataTable GetPaymentById(int recordid)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetPaymentById", recordid);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetInvoiceDetails(string InvSerial)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetRptInvoiceDetails", InvSerial);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetInvoice(string Receiptno, string name, string Paymentcode, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetInvoices", Receiptno, name, Paymentcode, fromdate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetPaymentToReverse(string receiptno, string districtcode)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetPaymentToReverse", receiptno, districtcode);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetChequesToBounce(string ChequeNo, string AccountNo, string BankName, string districtcode)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetChequesToBounce", ChequeNo, AccountNo, BankName, districtcode);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal DataTable GetReceiptNumber(string ReceiptNo)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetReceiptNumber", ReceiptNo);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetReceiptRanges(string DistrictCode)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetReceiptRanges", DistrictCode);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal DataTable CheckReceiptRanges(string districtcode)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("CheckReceiptRanges", districtcode);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal DataTable GetFormerReceipt(string districtcode, int recordId)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetFormerReceipt", recordId, districtcode);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetReceiptRangeByID(int recordId)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetReceiptRangeByID", recordId);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal DataTable CheckReceiptRange(string districtcode, int cashier)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("CheckReceiptRange", districtcode, cashier);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal DataTable Get_TransactionCode(DateTime PaymentDate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("Save_ScalaTransCode", PaymentDate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal DataTable GetTransDetailsByReceipt(string recieptNo)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetTransDetailsByReceipt", recieptNo);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal string GetStatusDescr(string statusCode)
    {
        string descr = "";
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetStatusDescr", statusCode);
            DataTable dt = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            if (dt.Rows.Count != 0)
            {
                descr = dt.Rows[0]["StatusDescription"].ToString();
            }
            else
            {
                descr = "GENERAL ERROR";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return descr;
    }
    internal DataTable GetDuplicateVendorRef(Transaction trans)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetDuplicateVendorRef", trans.VendorCode, trans.VendorTranId);
            DataTable returndetails = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return returndetails;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private DataTable GetBinnedByrecordID(int recordid)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetBinnedByrecordID", recordid);
            DataTable returndetails = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return returndetails;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// ///
    /// </summary>
    /// <param name="Batchno"></param>
    /// <param name="total"></param>
    /// <param name="failed"></param>
    /// <param name="reconciled"></param>
    /// <param name="createdby"></param>
    /// <returns></returns>
    public int SaveReconBatch(int Batchno, int total, int failed, int reconciled, string createdby)
    {
        int ret = 0;
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("SaveReconBatch", Batchno, total, failed, reconciled, createdby);
            procommand.CommandTimeout = 120;
            DataSet ds = PegPay_DB.ExecuteDataSet(procommand);
            if (Batchno.Equals(0))
            {
                int recordCount = ds.Tables[0].Rows.Count;
                if (recordCount != 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    ret = int.Parse(dr[0].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ret;
    }
    public string SavePayBatch(string Agentcode, int transactions, string batch_type, string createdby)
    {
        string ret = "";
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("SavePayBatch", Agentcode, transactions, batch_type, createdby);
            procommand.CommandTimeout = 120;
            DataSet ds = PegPay_DB.ExecuteDataSet(procommand);
            int recordCount = ds.Tables[0].Rows.Count;
            if (recordCount != 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                ret = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ret;
    }
    internal void BinTransaction(int recordid, bool reconciled, string createdby)
    {
        try
        {
            if (reconciled)
            {
                procommand = PegPay_DB.GetStoredProcCommand("BinReconciledTransaction", recordid, reconciled, createdby);
            }
            else
            {
                procommand = PegPay_DB.GetStoredProcCommand("BinTransaction", recordid, reconciled, createdby);
            }
            procommand.CommandTimeout = 120;
            PegPay_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal void RestoreTransaction(int recordid, string createdby)
    {
        try
        {
            DataTable dt = GetBinnedByrecordID(recordid);
            if (dt.Rows.Count > 0)
            {
                bool reconciled = bool.Parse(dt.Rows[0]["Reconciled"].ToString());
                if (reconciled)
                {
                    procommand = PegPay_DB.GetStoredProcCommand("RestoreToReconciled", recordid, createdby);
                }
                else
                {
                    procommand = PegPay_DB.GetStoredProcCommand("RestoreToRecieved", recordid, createdby);
                }
                procommand.CommandTimeout = 120;
                PegPay_DB.ExecuteNonQuery(procommand);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void ReconcileTransaction(string PegPayId, int BatchNo, string source, string recontype, string createdby)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("ReconcileTransaction", PegPayId, BatchNo, source, recontype, createdby);
            procommand.CommandTimeout = 120;
            PegPay_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string CreateReconBatch(string User)
    {
        string BatchCode = "";
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("CreateReconciliationBatch", User);
            procommand.CommandTimeout = 120;
            DataSet ds = PegPay_DB.ExecuteDataSet(procommand);
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
    public void CancelReconBatch(int Reconcode)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("CancelReconBatch", Reconcode);
            procommand.CommandTimeout = 120;
            PegPay_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal void BatchPayment(int recordid, string BatchNo)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("BatchPayment", recordid, BatchNo);
            procommand.CommandTimeout = 120;
            PegPay_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal string SavePayment(string custref, string custname, string custtype, string phone, string paytype, string paymode, double amount, double balance, string regioncode,
        string districtcode, string rev, string Teller, string mreceiptno, bool cancelled)
    {
        string Vendorref = "";
        try
        {
            bool Reversal = false;
            if (rev.Equals("1"))
            {
                Reversal = true;
            }
            procommand = PegPay_DB.GetStoredProcCommand("SavePayment", custref, custname, custtype, phone, paytype, paymode, mreceiptno, amount, balance, regioncode
                , districtcode, Reversal, cancelled, Teller);
            procommand.CommandTimeout = 120;
            DataSet ds = PegPay_DB.ExecuteDataSet(procommand);
            int recordCount = ds.Tables[0].Rows.Count;
            if (recordCount != 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                Vendorref = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Vendorref;
    }

    internal void UpdatePayment(Responseobj ret, string mreceiptno, string districtcode, double amount, int cashier_Id, bool cancelled)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("UpdatePayment", ret.Receiptno, ret.VendorRef, mreceiptno);
            procommand.CommandTimeout = 120;
            PegPay_DB.ExecuteNonQuery(procommand);

            if (!mreceiptno.Equals(""))
            {
                int receipt_no = int.Parse(mreceiptno);
                UpdateReceiptSerial(receipt_no, cashier_Id, districtcode, amount, cancelled);
            }
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    private void UpdateReceiptSerial(int receipt_no, int cashier_Id, string districtcode, double amount, bool cancelled)
    {
        try
        {
            if (amount > 0)
            {
                procommand = PegPay_DB.GetStoredProcCommand("UpdateReceiptSerial", receipt_no, cashier_Id, districtcode, amount);
                procommand.CommandTimeout = 120;
                PegPay_DB.ExecuteNonQuery(procommand);
            }
        }
        catch (Exception ex)
        {

        }
    }

    internal string SaveInvoiceDetails(InvoiceTran inv)
    {
        string serialno = "";
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("SaveInvoiceDetails", inv.Fname, inv.Mname, inv.Lname, inv.Phone, inv.Email,
                inv.Amount, inv.Vat, inv.PayTypeCode, inv.ShortName, inv.RegionCode, inv.DistrictCode, inv.User);
            procommand.CommandTimeout = 120;
            DataSet ds = PegPay_DB.ExecuteDataSet(procommand);
            int recordCount = ds.Tables[0].Rows.Count;
            if (recordCount != 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                serialno = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return serialno;
    }



    internal void SavePaymenReversal(string oldvendorref, string newvendorref, string oldreceiptno, string newreceiptno, string user)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("SavePaymenReversal", oldvendorref, newvendorref, oldreceiptno, newreceiptno, user);
            procommand.CommandTimeout = 120;
            PegPay_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    internal void DumpPayment(string VendorRef, string Reason)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("DumpPayment", VendorRef, Reason);
            procommand.CommandTimeout = 120;
            PegPay_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    internal void SaveChequeDatails(string agentref, string chequenumber, string accountnumber, string bank, DateTime chqDate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("SaveChequeDetails", chequenumber, accountnumber, bank, chqDate, agentref);
            procommand.CommandTimeout = 120;
            PegPay_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    internal void BounceChequePay(int recordid, string createdby)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("BounceChequePay", recordid, createdby);
            procommand.CommandTimeout = 120;
            PegPay_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }
    private DataTable GetCustRefArea(string custref)
    {
        try
        {
            int num;
            if (Int32.TryParse(custref, out num))
            {
                procommand = PegPay_DB.GetStoredProcCommand("GetCustDistrict", custref);
            }
            else
            {
                procommand = PegPay_DB.GetStoredProcCommand("GetInvoiceDistrict", custref);
            }
            DataTable dt = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public string GetPayTypeCodeByShortName(string ShortName)
    {
        string ret = "NONE";
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetPayTypeCodeByShortName", ShortName);
            DataTable returndetails = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            if (returndetails.Rows.Count > 0)
            {
                ret = returndetails.Rows[0]["PaymentCode"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ret;
    }
    //internal string PostUmemeTransaction(Transaction trans)
    //{
    //    try
    //    {
    //        string receiptNo = "";
    //        string format = "dd/MM/yyyy";
    //        DataTable table = GetCustRefArea(trans.CustomerRef);
    //        if (table.Rows.Count > 0)
    //        {
    //            string regioncode = table.Rows[0]["RegionCode"].ToString();
    //            string districtcode = table.Rows[0]["DistrictCode"].ToString();
    //            string tariff = table.Rows[0]["Tariff"].ToString();
    //            int no;
    //            if (!Int32.TryParse(trans.CustomerRef, out no))
    //            {
    //                string shortname = trans.CustomerRef.Substring(0, 2).ToUpper();
    //                trans.PaymentType = GetPayTypeCodeByShortName(shortname);
    //            }
    //            bool Reversal = false;
    //            if (trans.Reversal.Equals("1"))
    //            {
    //                Reversal = true;
    //            }
    //            DateTime paymentDate = DateTime.ParseExact(trans.PaymentDate, format, CultureInfo.InvariantCulture);
    //            trans.CustomerType = trans.CustomerType.ToUpper();
    //            trans.CustomerType = trans.CustomerType.Replace(" ", "");
    //            procommand = PegPay_DB.GetStoredProcCommand("SaveReceivedTransactions", trans.CustomerRef, trans.CustomerName, trans.CustomerType, trans.CustomerTel,
    //                trans.TranAmount, paymentDate, DateTime.Now, 
    //                trans.TranType, trans.PaymentType, trans.VendorTranId, trans.TranNarration,
    //                0, trans.VendorCode, trans.Teller, "", Reversal, regioncode, districtcode, tariff);
    //            DataTable dt = PegPay_DB.ExecuteDataSet(procommand).Tables[0];

    //            if (dt.Rows.Count != 0)
    //            {
    //                receiptNo = dt.Rows[0][0].ToString();
    //                if (trans.Reversal.Equals("1"))
    //                {
    //                    // Cancel Transaction if still in the Received table.
    //                    string TranIdToReverse = trans.TranIdToReverse;
    //                    DataTable dtable = GetTransactionToCancel(trans);
    //                    if (dtable.Rows.Count > 0)
    //                    {
    //                        double amountToReverse = double.Parse(dtable.Rows[0]["TranAmount"].ToString());
    //                        double ReversingAmount = double.Parse(trans.TranAmount);
    //                        double amount = amountToReverse + ReversingAmount;
    //                        if (amount.Equals(0))
    //                        {
    //                            /// Now flag transactions as cancelled.
    //                            CancelReversedTrans(trans);
    //                        }

    //                    }
    //                }
    //            }
    //            else
    //            {
    //                receiptNo = "";
    //            }
    //        }
    //        else
    //        {
    //            receiptNo = "";
    //        }
    //        return receiptNo;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //private void CancelReversedTrans(Transaction trans)
    //{
    //    try
    //    {
    //        procommand = PegPay_DB.GetStoredProcCommand("CancelReversedTrans", trans.VendorCode, trans.TranIdToReverse, trans.VendorTranId);
    //        PegPay_DB.ExecuteNonQuery(procommand);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //} 
    //private DataTable GetTransactionToCancel(Transaction tran)
    //{
    //    try
    //    {
    //        procommand = PegPay_DB.GetStoredProcCommand("GetTransactionToCancel", tran.TranIdToReverse, tran.VendorCode);
    //        DataTable dt = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    internal DataTable Get_TransactionByTranID(int recordid)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("Get_TransactionByTranID", recordid);
            DataTable dt = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    internal void InsertSmsToSend(string phone, string name, string custref, string msg, string mask, string sender)
    {
        try
        {
            phone = FormatNumber(phone);
            InsertInContacts(phone, name, custref);
            procommand = PegPay_DB.GetStoredProcCommand("InsertSmsToSend", phone, msg, mask, sender);
            PegPay_DB.ExecuteNonQuery(procommand);
            InsertInContacts(phone, name, custref);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private string FormatNumber(string number)
    {
        if (number.Length == 9)
        {
            number = "256" + number;
        }
        else if (number.Length == 10 && number.StartsWith("0"))
        {
            number = number.Remove(0, 1);
            number = "256" + number;
        }
        else if (number.StartsWith("+") && number.Length == 13)
        {
            number = number.Remove(0, 1);
        }
        return number;
    }
    private void InsertInContacts(string phone, string name, string custref)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("InsertInContacts", phone, custref, name);
            PegPay_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal string LogInternaltran(string recieptNo, string CreatedBy)
    {
        string res = "";
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("Save_InternalTran", recieptNo, CreatedBy);
            PegPay_DB.ExecuteNonQuery(procommand);
            res = "LOGGED";
        }
        catch (Exception ex)
        {
            res = "FALED";
        }
        return res;
    }

    internal void UpdateLoggedSms(string receiptNo)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("UpdateLoggedSms", receiptNo);
            PegPay_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal string GetServerStatus()
    {
        string status = "";
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetServerStatus");
            DataTable dt = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            status = dt.Rows[0]["ServerStatus"].ToString();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return status;
    }

    internal double GetCashOutFee(string TelecomId)
    {
        double cashoutfee = 0;
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetTranCashOutFee", TelecomId);
            DataTable dt = PegPay_DB.ExecuteDataSet(procommand).Tables[0];

            if (dt.Rows.Count > 0)
            {
                string amount = dt.Rows[0]["TranAmount"].ToString().Trim();
                bool isdouble = Double.TryParse(amount, out cashoutfee);
                if (isdouble)
                {
                    cashoutfee = Convert.ToDouble(amount);
                }
                else
                {
                    cashoutfee = 0;
                }
            }
            return cashoutfee;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetDeletedTransactions(string vendorcode, string vendorref, string FromAccount, string CustName, string Paymentcode, string ToAccount, DateTime fromdate, DateTime todate, string TranType)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetDeletedTransactions", vendorcode, vendorref, ToAccount, FromAccount,
                CustName, Paymentcode, fromdate, todate, TranType);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetReconciledTransactions(string vendorcode, string vendorref, string FromAccount, string CustName, string Paymentcode, string ToAccount, DateTime fromdate, DateTime todate, string TranType)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetReconciledTransactions", vendorcode, vendorref, ToAccount, FromAccount,
                CustName, Paymentcode, fromdate, todate, TranType);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetTransToReverse(string vendorcode, string vendorref, string telecomId, DateTime fromdate, DateTime todate, string Telecom, string TranType)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetTransToReverse", vendorcode, vendorref, telecomId, fromdate, todate, Telecom, TranType);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetBatchStatement(string vendorcode, string Account, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetBatchStatement", vendorcode, Account, fromdate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetBatchStatementNew1(string vendorcode, DateTime fromdate, DateTime todate)
    {
        try
        {
            string fDate = fromdate.ToString("yyyy-MM-dd");
            string tDate = todate.ToString("yyyy-MM-dd");
            procommand = PegPay_DB.GetStoredProcCommand("GenerateAccountStatement_Batch", vendorcode, fDate, tDate);
            procommand.CommandTimeout = 120;
            DataSet ds = PegPay_DB.ExecuteDataSet(procommand);
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetBatchStatementNew(string vendorcode, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetBatchStatementNew", vendorcode, fromdate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable PeriodOpeningandClosingBalance(string accNo, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("PeriodOpeningandClosingBalance", accNo, fromdate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetBatchAmountTotal(string BatchCode, string Status, string Type)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetBatchAmountTotal", BatchCode, Status, Type);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetBatchTransactions(string BatchNo, string Type)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetBatchTransactions", BatchNo, Type);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetRunningBalances(string vendorcode, DateTime fromDate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetRunningBalances", vendorcode, fromDate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetRunningBalancesOther(string vendorcode, DateTime fromDate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("View_RunningBalances", vendorcode, fromDate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetRunningBalances2(string vendorcode, DateTime fromDate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetRunningBalances2", vendorcode, fromDate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetLineGraphTransactionsCreditsToDisplay(string vendorcode, string network, DateTime fromdate, DateTime todate, string TranType)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetLineGraphTransactionsCreditsToDisplay", vendorcode, network, fromdate, todate, TranType);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetLineGraphTransactionsDebitsToDisplay(string vendorcode, string network, DateTime fromdate, DateTime todate, string TranType)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetLineGraphTransactionsDebitsToDisplay", vendorcode, network, fromdate, todate, TranType);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public string GetAccountNumberDetails(string vendorcode)
    {
        string AccountNumber = "";
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetVendorAccountNumberDetails", vendorcode);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            if (datatable.Rows.Count != 0)
            {
                AccountNumber = datatable.Rows[0]["AccountNumber"].ToString();


            }
            else
            {
                AccountNumber = "No Account";
            }


            //return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return AccountNumber;
    }
    public DataTable GetPullsLineGraphTransactions(string vendorcode, string AccountDetails, DateTime fromdate, DateTime todate)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetPullsLineGraphTransactions", vendorcode, AccountDetails, fromdate, todate);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public Int64 GetMonthlyStartingBalance(Int64 id)
    {
        Int64 amount = 0;
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetVendorMonthlyStartingBalance", id);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            if (datatable.Rows.Count != 0)
            {
                string amount1 = datatable.Rows[0]["Balance"].ToString().Split('.')[0];
                //double nn = Convert.ToDouble(amount1.Split('.')[0]);
                amount = int.Parse(amount1);
            }
            else
            {
                amount = 0;
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
        return amount;
    }
    public DataTable GetLineGraphTransactionsMTNDebitsToDisplay(string vendorcode, DateTime fromdate, DateTime todate, string TranType)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetLineGraphTransactionsMTNDebitsToDisplay", vendorcode, fromdate, todate, TranType);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetLineGraphTransactionsAIRTELDebitsToDisplay(string vendorcode, DateTime fromdate, DateTime todate, string TranType)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetLineGraphTransactionsAIRTELDebitsToDisplay", vendorcode, fromdate, todate, TranType);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public DataTable GetBatchDetails(string BatchCode)
    //{
    //    try
    //    {

    //    }
    //    catch(Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public DataTable GetAccountsToSwitch()
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetAccountsToSwitch");
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetMTNAccountsToSwitch()
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetMTNAccountsToSwitch");
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAllVendorMTNAccountsToSwitch(string acc)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetAllVendorMTNAccountsToSwitch",acc);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetMTNAccountDetails(string acc)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetMTNAccountDetails",acc);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void UpdateVendorMTNAccount(string VendorCode, string MtnAccUserName, string MtnAccPassword)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("UpdateVendorMTNAccount", VendorCode, MtnAccUserName, MtnAccPassword);
            PegPay_DB.ExecuteNonQuery(procommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCustDetails(string vendorcode)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetCustDetails", vendorcode);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetOvaAccounts(string senderId)
    {
        try
        {
            procommand = PegPay_DB.GetStoredProcCommand("GetOvaAccounts", senderId);
            procommand.CommandTimeout = 120;
            datatable = PegPay_DB.ExecuteDataSet(procommand).Tables[0];
            return datatable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
