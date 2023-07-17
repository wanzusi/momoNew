using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using InterLinkClass.EntityObjects;
using System.Collections.Generic;

/// <summary>
/// Summary description for ProcessPay
/// </summary>
public class ProcessPay
{
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    SendMail mailer = new SendMail();
    DataTable dTable = new DataTable();
    DataTable datatable = new DataTable();
    public ProcessPay()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //public Responseobj InternalReversestr(string str, string reversal_ref, string naration, bool cheque)
    //{
    //    Responseobj output = new Responseobj();
    //    string createdby = HttpContext.Current.Session["Username"].ToString();
    //    int recordid = int.Parse(str);
    //    datatable = datapay.Get_TransactionByTranID(recordid);
    //    if (datatable.Rows.Count > 0)
    //    {
    //        Transaction t = new Transaction();
    //        t = GetTransObject(datatable);
    //        t.VendorTranId = reversal_ref;
    //        t.Teller = createdby;
    //        if (!bll.IsduplicateVendorRef(t))
    //        {
    //            double amt = double.Parse(t.TranAmount);
    //            if (amt > 0)
    //            {
    //                output.Errorcode = "201";
    //                output.Message = "ARITHMATIC FAILURE";
    //            }
    //            else
    //            {
    //                string recieptNo = datapay.PostUmemeTransaction(t);
    //                if (!recieptNo.Equals(""))
    //                {
    //                    string res_log = datapay.LogInternaltran(recieptNo, t.Teller);
    //                    if (res_log.Equals("LOGGED"))
    //                    {
    //                        string res_reconcile = bll.Reconcile_InternalTrans(recieptNo, t.Teller);
    //                        output.Errorcode = "0";
    //                        if (res_reconcile.Equals("RECONCILED"))
    //                        {
    //                            output.Message = "Transaction Posted Successfully [" + recieptNo + "]";
    //                        }
    //                        else
    //                        {
    //                            output.Message = "Transaction Posted Successfully [" + t.VendorTranId + "] but Reconciled failed, Please reconciled it";
    //                        }
    //                        bll.SendSms(t, recieptNo, true);
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            output.Errorcode = "20";
    //            output.Message = datapay.GetStatusDescr(output.Errorcode);
    //        }
    //    }
    //    else
    //    {
    //        output.Errorcode = "2500";
    //        output.Message = "FAILED TO LOCATE MAIN TRANSACTION DETAILS";
    //    }
    //    return output;
    //}

    //private Transaction GetTransObject(DataTable datatable)
    //{
    //    Transaction t = new Transaction();
    //    string str_amount = datatable.Rows[0]["TranAmount"].ToString();
    //    double amt = double.Parse(str_amount);
    //    double TransAmount = 0 - amt;
    //    t.TranAmount = TransAmount.ToString();
    //    t.CustomerRef = datatable.Rows[0]["CustomerRef"].ToString();
    //    t.CustomerType = datatable.Rows[0]["CustomerType"].ToString();
    //    t.CustomerName = datatable.Rows[0]["CustomerName"].ToString();
    //    t.TranType = datatable.Rows[0]["TranType"].ToString();
    //    t.PaymentType = datatable.Rows[0]["PaymentType"].ToString();
    //    t.CustomerTel = datatable.Rows[0]["CustomerTel"].ToString();
    //    t.Reversal = "1";
    //    t.VendorCode = datatable.Rows[0]["VendorCode"].ToString();
    //    t.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");
    //    return t;
    //}
    public string Binstr(string str, string trans_type)
    {
        string output = "";
        if (str.Equals(""))
        {
            output = "Please Select Transaction to archived";
        }
        else
        {
            bool trans_status = Gettrans_status(trans_type);
            string createdby = HttpContext.Current.Session["Username"].ToString();
            int recordid = 0;
            string[] arr = str.Split(',');
            int i = 0;
            for (i = 0; i < arr.Length; i++)
            {
                recordid = int.Parse(arr[i].ToString());
                datapay.BinTransaction(recordid, trans_status, createdby);
            }
            /// Update Batch Details
            output = i + " Transaction(s) have been archived";
        }
        return output;
    }
    public string Restorestr(string str)
    {
        string output = "";
        if (str.Equals(""))
        {
            output = "Please Select Transaction to Restore";
        }
        else
        {
            string createdby = HttpContext.Current.Session["Username"].ToString();
            int recordid = 0;
            string[] arr = str.Split(',');
            int i = 0;
            for (i = 0; i < arr.Length; i++)
            {
                recordid = int.Parse(arr[i].ToString());
                datapay.RestoreTransaction(recordid, createdby);
            }
            /// Update Batch Details
            output = i + " Transaction(s) have been Restored";
        }
        return output;
    }
    public bool Gettrans_status(string trans_type)
    {
        if (trans_type.Equals("1"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public string Reconcilestr(string str)
    {
        string output = "";
        if (str.Equals(""))
        {
            output = "Please Select Transaction to Reconcile";
        }
        else
        {
            string createdby = HttpContext.Current.Session["Username"].ToString();
            string source = "RECEIVED";
            int BatchNo = datapay.SaveReconBatch(0, 0, 0, 0, createdby);
            //int recordid = 0;
            string PegpayId = "";
            string[] arr = str.Split(',');
            int i = 0;
            for (i = 0; i < arr.Length; i++)
            {
                //recordid = int.Parse(arr[i].ToString());
                PegpayId = arr[i].ToString();
                string ReconType = "MR";
                datapay.ReconcileTransaction(PegpayId, BatchNo, source, ReconType, createdby);
            }
            /// Update Batch Details
            datapay.SaveReconBatch(BatchNo, i, 0, i, createdby);
            output = i + " Transaction(s) have been reconciled";
        }
        return output;
    }
    public string Batchstr(string str, string Agentcode, string option)
    {
        string output = "";
        if (option.Equals("0"))
        {
            output = "Please Select Batch Type";
        }
        else if (str.Equals(""))
        {
            output = "Please Select Transactions to Batch";
        }
        else
        {
            string createdby = HttpContext.Current.Session["Username"].ToString();
            int recordid = 0;
            string[] arr = str.Split(',');
            int i = 0;
            int Transactions = arr.Length;
            string BatchNo = datapay.SavePayBatch(Agentcode, Transactions, option, createdby);
            if (BatchNo.Equals(""))
            {
                output = "Failed to generate batch Number";
            }
            else
            {
                for (i = 0; i < arr.Length; i++)
                {
                    recordid = int.Parse(arr[i].ToString());
                    datapay.BatchPayment(recordid, BatchNo);
                }
                output = i + " Payments have been batched Successfully";
            }
        }
        return output;
    }
    public bool ProcessCustomer(Cust cust)
    {
        ArrayList failedCustTransactions = new ArrayList();
        bool output = false;
        //string source = "RECEIVED";
        //string reason = "";
        //dTable = datapay.GetTransactionforReconciliation(tran.VendorRef, tran.VendorCode);
        //if (dTable.Rows.Count > 0)
        //{
        //    datatable = datapay.GetCustomerDetails(cust.CustRef, cust.Utility);
        //    if (datatable.Rows.Count > 0)
        //    {

        //            reason = "Customer "+cust.CustRef+","+cust.CustName;
        //            tran.Reason = reason;
        //            failedBankTransactions.Add(tran);
        //            output = false;

        //    }
        //    else
        //    {
        //        int recordid = int.Parse(dTable.Rows[0]["TranId"].ToString());
        //        double interamount = double.Parse(dTable.Rows[0]["TranAmount"].ToString());
        //        DateTime interDate = DateTime.Parse(dTable.Rows[0]["PaymentDate"].ToString());

        //        string strInterDate = interDate.ToString("dd/MM/yyyy");
        //        string strStateDate = tran.PayDate;

        //        if (interamount.Equals(tran.TransAmount))
        //        {
        //            if (strInterDate.Equals(strStateDate))
        //            {
        //                datapay.ReconcileTransaction(recordid, Reconcode, source, tran.ReconType, tran.ReconciledBy);
        //                output = true;
        //            }
        //            else
        //            {
        //                reason = "Payment Dates dont match";
        //                tran.Reason = reason;
        //                failedBankTransactions.Add(tran);
        //                output = false;
        //            }
        //        }
        //        else
        //        {
        //            reason = "Amount in Umeme Database does not match with that on the Statement";
        //            tran.Reason = reason;
        //            failedBankTransactions.Add(tran);
        //            output = false;
        //        }
        //    }
        //}
        //else
        //{
        //    datatable = datapay.GetReconciledTransaction(tran.VendorRef, tran.VendorCode);
        //    if (datatable.Rows.Count > 0)
        //    {
        //        DateTime recondate = DateTime.Parse(datatable.Rows[0]["ReconciledDate"].ToString());
        //        string recondatestr = recondate.ToString("dd/MM/yyyy : HH:MM:ss");
        //        reason = "Transaction already reconciled on " + recondatestr;
        //        tran.Reason = reason;
        //        failedBankTransactions.Add(tran);
        //        output = false;
        //    }
        //    else
        //    {
        //        reason = "Not found in the System database";
        //        tran.Reason = reason;
        //        failedBankTransactions.Add(tran);
        //        output = false;
        //    }
        //}
        return output;
    }
    public bool ReconcileTrans(Recontran tran, ArrayList failedBankTransactions, int Reconcode)
    {
        bool output;
        string source = "RECEIVED";
        string reason = "";
        dTable = datapay.GetTransactionforReconciliation(tran.VendorRef, tran.VendorCode);
        if (dTable.Rows.Count > 0)
        {
            datatable = datapay.GetReconciledTransaction(tran.VendorRef, tran.VendorCode);
            if (datatable.Rows.Count > 0)
            {
                int ReconId = int.Parse(datatable.Rows[0]["ReconciliationCode"].ToString());
                if (ReconId.Equals(Reconcode))
                {
                    reason = "Duplicate transaction with (" + datatable.Rows[0]["ReconciliationCode"].ToString() + ")";
                    tran.Reason = reason;
                    failedBankTransactions.Add(tran);
                    output = false;
                }
                else
                {
                    DateTime recondate = DateTime.Parse(datatable.Rows[0]["ReconciledDate"].ToString());
                    string recondatestr = recondate.ToString("dd/MM/yyyy : HH:MM:ss");
                    reason = "Transaction already reconciled on " + recondatestr;
                    tran.Reason = reason;
                    failedBankTransactions.Add(tran);
                    output = false;
                }
            }
            else
            {
                //int recordid = int.Parse(dTable.Rows[0]["TranId"].ToString());
                double interamount = double.Parse(dTable.Rows[0]["TranAmount"].ToString());
                double CashoutFee = datapay.GetCashOutFee(tran.VendorRef);
                interamount = interamount + CashoutFee;
                DateTime interDate = DateTime.Parse(dTable.Rows[0]["PaymentDate"].ToString());
                string PegPayId = dTable.Rows[0]["PegPayTranId"].ToString();
                //string Network = tran.VendorCode;
                string strInterDate = interDate.ToString("dd/MM/yyyy");
                string strStateDate = tran.PayDate;

                if (interamount.Equals(tran.TransAmount))
                {
                    if (strInterDate.Equals(strStateDate) || !strInterDate.Equals(strStateDate))
                    {
                        datapay.ReconcileTransaction(PegPayId, Reconcode, source, tran.ReconType, tran.ReconciledBy);
                        output = true;
                    }
                    else
                    {
                        reason = "Payment Dates dont match";
                        tran.Reason = reason;
                        failedBankTransactions.Add(tran);
                        output = false;
                    }
                }
                else
                {
                    reason = "Amount in PegPay Database does not match with that on the Statement";
                    tran.Reason = reason;
                    failedBankTransactions.Add(tran);
                    output = false;
                }
            }
        }
        else
        {
            datatable = datapay.GetReconciledTransaction(tran.VendorRef, tran.VendorCode);
            if (datatable.Rows.Count > 0)
            {
                DateTime recondate = DateTime.Parse(datatable.Rows[0]["ReconciledDate"].ToString());
                string recondatestr = recondate.ToString("dd/MM/yyyy : HH:MM:ss");
                reason = "Transaction already reconciled on " + recondatestr;
                tran.Reason = reason;
                failedBankTransactions.Add(tran);
                output = false;
            }
            else
            {
                reason = "Not found in the System database";
                tran.Reason = reason;
                failedBankTransactions.Add(tran);
                output = false;
            }
        }
        return output;
    }











    public string PendTransaction(string str)
    {
        try
        {
            List<int> output = new List<int>();
            string error = "";
            if (str.Length <= 0)
            {
                error = "Please Select Transaction(s) to Pend";
            }
            else
            {
                string RecordID = "";
                string[] arr = str.Split(',');

                int i = 0;
                int success = 0;
                int failed = 0;
                int totalRowsAffected = 0;

                for (i = 0; i < arr.Length; i++)
                {
                    RecordID = arr[i].ToString();
                    int rowsAffected = datapay.UpdateTransByID(RecordID);

                    if (rowsAffected > 0)
                    {
                        success++;
                        totalRowsAffected += rowsAffected;
                    }
                    else
                    {
                        failed++;
                    }
                }
                error = "SUCCESSFULLY PENDED  " + (success).ToString() + "  MAIN   TRANSACTIONS.";
                // str.Replace("@", Environment.NewLine);
                error = error.Replace("@", Environment.NewLine);
            }

            return error;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }











    //public List<int> PendTransactions(string pegPayId)
    //{
    //    DataTable DeletedRecord = new DataTable();
    //    Transaction2 Trans = new Transaction2();
    //    BusinessLogin bl = new BusinessLogin();

    //    List<int> response = new List<int>();

    //    int countSuccessful = 0;
    //    int CountFailed = 0;
    //    int cou = 0;
    //    try
    //    {
    //        DeletedRecord = datafile.GetTransactionToPend(pegPayId);

    //        string telecomId = "";
    //        Trans.getTelecomID = telecomId;

    //        if (DeletedRecord.Rows.Count > 0)
    //        {
    //            foreach (DataRow dr in DeletedRecord.Rows)
    //            {
    //                //string TransactionCategory = dr["TransCategory"].ToString();
    //                //string Trantype = dr["TranType"].ToString();

    //                //    if (Trantype == "1")
    //                //    {
    //                //        Trans.getSentToVendorStatus = "1";
    //                //    }
    //                //    if (TransactionCategory == "01")TransCategory
    //                //    {
    //                Trans.RecordId = dr["RecordId"].ToString();
    //                Trans.TransCategory = dr["TransCategory"].ToString();
    //                Trans.getFromAccount = dr["FromAccount"].ToString();
    //                Trans.getToAccount = dr["ToAccount"].ToString();
    //                Trans.getCustName = dr["CustName"].ToString();
    //                Trans.getVendorTranId = dr["VendorTranId"].ToString();
    //                Trans.getTranAmount = dr["TranAmount"].ToString();
    //                Trans.getFromNetwork = dr["FromNetwork"].ToString();
    //                Trans.getToNetwork = dr["ToNetwork"].ToString();
    //                Trans.getPaymentDate = dr["PaymentDate"].ToString();
    //                Trans.getRecordDate = DateTime.Now.ToString(); //dr["RecordDate"].ToString();
    //                Trans.getPaymentType = dr["PaymentType"].ToString();
    //                Trans.GetTranType = dr["TranType"].ToString();
    //                Trans.getVendorCode = dr["VendorCode"].ToString();
    //                Trans.getPhone = dr["Phone"].ToString();
    //                Trans.PegID = dr["PegPayTranId"].ToString();
    //                // Trans.ge

    //                //Trans.getSentToVendorStatus = "";
    //                //Trans.getPegasusCommisionAccount = "";
    //                Trans.getTranCharge = dr["TranCharge"].ToString();
    //                //Trans.getTelecomCommissionAccount = "";
    //                //Trans.getMNOCharge = dr["MNOCharge"].ToString();
    //                //Trans.getCashoutCharge = "0";
    //                //Trans.getCashoutAccount = "0";
    //                Trans.getTranStatus = dr["Status"].ToString();
    //                //Trans.getSentToVendorStatus = "0";

    //                //    }
    //                //    else if (TransactionCategory == "02")
    //                //    {
    //                //        Trans.getPegasusCommisionAccount = dr["ToAccount"].ToString();
    //                //        Trans.getTranCharge = dr["TranAmount"].ToString();
    //                //    }
    //                //    else if (TransactionCategory == "03")
    //                //    {
    //                //        Trans.getTelecomCommissionAccount = dr["ToAccount"].ToString();
    //                //        Trans.getMNOCharge = dr["TranAmount"].ToString();
    //                //        Trans.getCashoutCharge = "0";
    //                //        Trans.getCashoutAccount = "0";
    //                //    }
    //                //    else if (TransactionCategory == "04")
    //                //    {
    //                //        Trans.getCashoutCharge = dr["TranAmount"].ToString();
    //                //        Trans.getCashoutAccount = dr["ToAccount"].ToString();
    //                //    }
    //                //    else
    //                //    {
    //                //    }
    //            }

    //            string Response = (datapay.UpdateTransByID(Trans)).ToString();
    //            cou = datapay.countUpdateTransByID(Trans);
    //            if (Response == "1")
    //            {
    //                countSuccessful++;
    //                cou++;
    //            }
    //            else
    //            {
    //                CountFailed++;
    //            }
    //        }
    //    }
    //    catch (Exception exx)
    //    {

    //    }
    //    response.Add(countSuccessful);
    //    response.Add(CountFailed);
    //    response.Add(cou);
    //    return response;
    //}









    public List<int> ListBatchTransactionsToComplete(string PegpayId)
    {
        DataTable DeletedRecord = new DataTable();
        Transaction2 Trans = new Transaction2();
        BusinessLogin bl = new BusinessLogin();

        List<int> response = new List<int>();

        int countSuccessful = 0;
        int CountFailed = 0;
        int cou = 0;
        try
        {
         //   DataTable DeletedRecord = new DataTable();
            DeletedRecord = datafile.GetTransactionToComplete(PegpayId);

            string telecomId = "";
            Trans.getTelecomID = telecomId;
            
            if (DeletedRecord.Rows.Count > 0)
            {
                foreach (DataRow dr in DeletedRecord.Rows)
                {

                    Trans.RecordId = dr["RecordId"].ToString();
                 
                    Trans.getFromAccount = dr["BatchNo"].ToString();
                    Trans.getToAccount = dr["BatchCode"].ToString();
                       Trans.PegID = dr["Cleared"].ToString();
                 
                    Trans.getVendorTranId = dr["PaymentNo"].ToString();
                 
                    Trans.getPhone = dr["BeneficaryAccount"].ToString();
                 
                  
                    Trans.getTranStatus = dr["Status"].ToString();

                   
   

    


                    Trans.getTranAmount = dr["CustomerCode"].ToString();
                    Trans.getFromNetwork = dr["Amount"].ToString();
                    Trans.getToNetwork = dr["PaymentDate"].ToString();
                    Trans.getPaymentDate = dr["ExcludedBy"].ToString();
                    Trans.getRecordDate = dr["RecordDate"].ToString();
                    Trans.getPaymentType = dr["ExclusionDate"].ToString();
                    Trans.GetTranType = dr["Reason"].ToString();
                    Trans.getVendorCode = dr["RecordedBy"].ToString();





                  
                }
                string Response = datapay.BatchRowsUpdatedID(Trans);
                cou = datapay.countBatchRowsUpdatedID(Trans);
                if (Response == "1")
                {
                    countSuccessful++;
                    cou++;
                }
                else
                {
                    CountFailed++;
                }
            }
        }
        catch (Exception exx)
        {

        }
        response.Add(countSuccessful);
        response.Add(CountFailed);
        response.Add(cou);
        return response;
    }








    public string completeFailedBatchTransaction(string str)
    {
        Transaction2 Trans = new Transaction2();
        int cou = (datapay.countBatchRowsUpdatedID(Trans));
        try
        {
            List<int> output = new List<int>();
            string error = "";
            if (str.Length <= 0)
            {
                error = "Please Select batch Transaction(s) to complete";
            }
            else
            {
                string PegpayId = "";
                string[] arr = str.Split(',');
                int i = 0;
                int success = 0;
                int failed = 0;
                for (i = 0; i < arr.Length; i++)
                {
                    PegpayId = arr[i].ToString();

                    output = ListBatchTransactionsToComplete(PegpayId);
                    success = success + output[0];
                    failed = failed + output[1];
                    cou = cou + output[2];
                    cou++;

                }
                error = "SUCCESSFULLY COMPLETED TRANSACTIONS  ....   (TOTAL OF  " + cou + "  ROWS UPDATED)";
                // str.Replace("@", Environment.NewLine);
                error = error.Replace("@", Environment.NewLine);
            }

            return error;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }















    public string SaveInvoiceDetails(string fname, string mname, string lname, string phone, string email, string amount)
    {
        string ret = "";
        PhoneValidator ph = new PhoneValidator();
        if (!bll.IsValidEmailAddressOptional(email))
        {
            ret = "Please Provide a valid email address";
        }
        else if (!ph.PhoneNumbersOk(phone) && !phone.Equals(""))
        {
            ret = phone + " is not a valid phone number";
        }
        else
        {

        }
        return ret;
    }

    public InvoiceTran SaveInvoiceDetails(InvoiceTran inv)
    {
        InvoiceTran resp = new InvoiceTran();
        PhoneValidator ph = new PhoneValidator();
        if (!bll.IsValidEmailAddress(inv.Email) && !inv.Email.Equals(""))
        {
            resp.ErrorCode = "1";
            resp.Error = "Please Provide a valid email address";
        }
        else if (!ph.PhoneNumbersOk(inv.Phone) && !inv.Phone.Equals(""))
        {
            resp.ErrorCode = "1";
            resp.Error = inv.Phone + " is not a valid phone number";
        }
        else
        {
            inv.PayTypeCode = datafile.GetPayTypeCodeByShortName(inv.ShortName);
            inv.User = HttpContext.Current.Session["Username"].ToString();
            inv.RegionCode = HttpContext.Current.Session["AreaCode"].ToString();
            inv.DistrictCode = HttpContext.Current.Session["DistrictCode"].ToString();
            inv.Vat = GetVatAmount(inv);
            resp.InvoiceSerial = datapay.SaveInvoiceDetails(inv);
            if (resp.InvoiceSerial.Equals(""))
            {
                resp.ErrorCode = "1";
                resp.Error = "Failed to create Invoice serial number";
            }
            else
            {
                resp.ErrorCode = "0";
                resp.Error = "Invoice created successfully[" + resp.InvoiceSerial + "]";
            }
        }
        return resp;
    }

    private double GetVatAmount(InvoiceTran inv)
    {
        double vat = 0;
        if (inv.Vatable)
        {
            vat = inv.Amount * 0.18;
        }
        return vat;
    }
    public string SaveChequeDetails(string agentref, string chequenumber, string accountnumber, string bank, string chequeDate)
    {
        string ret = "";
        try
        {
            DateTime chqDate = DateTime.Parse(chequeDate);
            datapay.SaveChequeDatails(agentref, chequenumber, accountnumber, bank, chqDate);
            ret = "SUCCESS";
        }
        catch (Exception ex)
        {
            ret = ex.Message;
        }
        return ret;
    }


    public string ReverseTransactions(string str, System.Web.SessionState.HttpSessionState Session)
    {
        string output = "";
        if (str.Equals(""))
        {
            output = "Please Select Transaction to Reverse";
        }
        else
        {
            string userId = Session["Username"] as string;
            string fullname = Session["Fullname"] as string;
            string userBranch = Session["UserBranch"] as string;
            string page = bll.GetCurrentPageName();

            string PegpayId = "";
            string[] arr = str.Split(',');
            int i = 0;
            for (i = 0; i < arr.Length; i++)
            {
                PegpayId = arr[i].ToString();
                datatable = datafile.GetTranById(PegpayId);
                if (datatable.Rows.Count > 0)
                {
                    string VendorCode = datatable.Rows[0]["VendorCode"].ToString();
                    string VendorId = datatable.Rows[0]["VendorTranId"].ToString();

                    if (VendorCode.ToUpper() == "STANBIC_VAS")
                    {
                        string status = "FAILED";
                        string StatusCode = "100";
                        string Reason = "TRANSACTION MANUALLY FAILED";
                        datafile.UpdateFailedStanbicVasTransaction(VendorId, status, Reason);
                        datafile.reverseTransaction(PegpayId);
                    }
                    else
                    {
                        datafile.reverseTransaction(PegpayId);
                    }
                    bll.InsertIntoAuditLog(VendorId, "UPDATE", "Reverse Transaction", userBranch, userId, page,
   fullname + " reversed the transaction [" + VendorId + "]  with the vendorCode [" + VendorCode + "] at " + DateTime.Now.ToString());

                }
            }
            output = i + " Transaction(s) have been Reversed";
        }
        return output;
    }

    public string ReverseTranToRecieved(string str)
    {
        try
        {
            List<int> output = new List<int>();
            string error = "";
            if (str.Length <= 0)
            {
                error = "Please Select Transaction to Transfer";
            }
            else
            {
                string PegpayId = "";
                string[] arr = str.Split(',');
                int i = 0;
                int success = 0;
                int failed = 0;
                for (i = 0; i < arr.Length; i++)
                {
                    PegpayId = arr[i].ToString();
                    output = ReverseTransToReceived(PegpayId);
                    success = success + output[0];
                    failed = failed + output[1];
                }
                error = "SUCCESSFUL: " + (success).ToString() + " FAILED :" + (failed).ToString();
            }

            return error;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public List<int> ReverseTransToReceived(string pegPayId)
    {
        DataTable DeletedRecord = new DataTable();
        Transaction2 Trans = new Transaction2();
        BusinessLogin bl = new BusinessLogin();

        List<int> response = new List<int>();

        int countSuccessful = 0;
        int CountFailed = 0;
        try
        {
            DeletedRecord = datafile.GetDeletedTransactionToReverse(pegPayId);

            string telecomId = "";
            Trans.getTelecomID = telecomId;

            if (DeletedRecord.Rows.Count > 0)
            {
                foreach (DataRow dr in DeletedRecord.Rows)
                {
                    string TransactionCategory = dr["TransCategory"].ToString();
                    string Trantype = dr["TranType"].ToString();

                    if (Trantype == "1")
                    {
                        Trans.getSentToVendorStatus = "1";
                    }
                    if (TransactionCategory == "01")
                    {
                        Trans.getFromAccount = dr["FromAccount"].ToString();
                        Trans.getToAccount = dr["ToAccount"].ToString();
                        Trans.getCustName = dr["CustName"].ToString();
                        Trans.getVendorTranId = dr["VendorTranId"].ToString();
                        Trans.getTranAmount = dr["TranAmount"].ToString();
                        Trans.getFromNetwork = dr["FromNetwork"].ToString();
                        Trans.getToNetwork = dr["ToNetwork"].ToString();
                        Trans.getPaymentDate = dr["PaymentDate"].ToString();
                        Trans.getRecordDate = DateTime.Now.ToString(); //dr["RecordDate"].ToString();
                        Trans.getPaymentType = dr["PaymentType"].ToString();
                        Trans.GetTranType = dr["TranType"].ToString();
                        Trans.getVendorCode = dr["VendorCode"].ToString();
                        Trans.getPhone = dr["Phone"].ToString();
                        Trans.getPegPayId = dr["PegPayTranId"].ToString();

                        //Trans.getSentToVendorStatus = "";
                        Trans.getPegasusCommisionAccount = "";
                        Trans.getTranCharge = dr["TranCharge"].ToString();
                        Trans.getTelecomCommissionAccount = "";
                        Trans.getMNOCharge = dr["MNOCharge"].ToString();
                        Trans.getCashoutCharge = "0";
                        Trans.getCashoutAccount = "0";
                        Trans.getTranStatus = "INSERTED PUSH";
                        Trans.getSentToVendorStatus = "0";

                    }
                    else if (TransactionCategory == "02")
                    {
                        Trans.getPegasusCommisionAccount = dr["ToAccount"].ToString();
                        Trans.getTranCharge = dr["TranAmount"].ToString();
                    }
                    else if (TransactionCategory == "03")
                    {
                        Trans.getTelecomCommissionAccount = dr["ToAccount"].ToString();
                        Trans.getMNOCharge = dr["TranAmount"].ToString();
                        Trans.getCashoutCharge = "0";
                        Trans.getCashoutAccount = "0";
                    }
                    else if (TransactionCategory == "04")
                    {
                        Trans.getCashoutCharge = dr["TranAmount"].ToString();
                        Trans.getCashoutAccount = dr["ToAccount"].ToString();
                    }
                    else
                    {
                    }
                }
                string Response = datapay.ReverseDeletedTransaction(Trans);
                if (Response == "1")
                {
                    countSuccessful++; ;
                }
                else
                {
                    CountFailed++;
                }
            }
        }
        catch (Exception exx)
        {

        }
        response.Add(countSuccessful);
        response.Add(CountFailed);
        return response;
    }


    public string ResendTransactions(string str, System.Web.SessionState.HttpSessionState Session)
    {
        try
        {
            string output = "";
            if (str.Equals(""))
            {
                output = "Please Select Transaction to Resend";
            }
            else
            {
                string userId = Session["Username"] as string;
                string fullname = Session["Fullname"] as string;
                string userBranch = Session["UserBranch"] as string;
                string page = bll.GetCurrentPageName();

                string PegpayId = "";
                string[] arr = str.Split(',');
                int i = 0;
                int success = 0;
                int failed = 0;
                for (i = 0; i < arr.Length; i++)
                {
                    PegpayId = arr[i].ToString();
                    datatable = datafile.GetTranById(PegpayId);
                    if (datatable.Rows.Count > 0)
                    {
                        string VendorCode = datatable.Rows[0]["VendorCode"].ToString();
 
                        string recordId = datatable.Rows[0]["RecordId"].ToString();
                        datafile.ResendTransaction(recordId);
                        //datafile.ResetTransactionStatusBackToUnsentStatus(recordId);
                        //int cashoutfee = GetCashoutFee(VenodrTranId);
                        //bll.SendTransaction(datatable, cashoutfee);
                        bll.InsertIntoAuditLog(PegpayId, "UPDATE", "Resend Transaction", userBranch, userId, page,
fullname + " resent the transaction [" + PegpayId + "]  with the vendorCode [" + VendorCode + "] from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());

                    }
                }
                output = i + " Transaction(s) have been Resent";
            }
            return output;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private int GetCashoutFee(string VenodrTranId)
    {
        try
        {
            int CashoutFee = 0;
            dTable = datafile.GetTranCashoutFee(VenodrTranId);
            if (dTable.Rows.Count > 0)
            {
                double amt = Convert.ToDouble(dTable.Rows[0]["TranAmount"].ToString().Trim());
                CashoutFee = Convert.ToInt32(amt);
            }
            else
            {
                CashoutFee = 0;
            }
            return CashoutFee;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string UpdateTranStatus(string PegPayId, string TelecomId)
    {
        try
        {
            string status = "";
            string telComID = TelecomId.Trim();

            datafile.UpdateTransactionStatus(PegPayId, telComID, "SUCCESS", "", "01");
            status = "OK";
            return status;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string MarkTransactionAsInserted(string tranlist, System.Web.SessionState.HttpSessionState Session)
    {
        try
        {
            string userId = Session["Username"] as string;
            string fullname = Session["Fullname"] as string;
            string userBranch = Session["CustomerCode"] as string;
            string page = bll.GetCurrentPageName();

            string telecomId = "";
            string status = "INSERTED PUSH";
            string narration = "";
            string statusCode = "0";

            string[] transactions = tranlist.Split(',');

            foreach (string pegpayId in transactions)
            {
                string[] data = { pegpayId, telecomId, status, narration, statusCode };
                datafile.ExecuteNonQuery("UpdateTransactionStatusOnly", data);

                bll.InsertIntoAuditLog(pegpayId, "UPDATE", "Update Transaction To INSERTED PUSH", userBranch, userId, page,
fullname + " updated the transaction [" + pegpayId + "]  to  INSERTED PUSH from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());

            }
            return "Resent";
        }
        catch (Exception e)
        {
            //do nothing
            throw e;
        }
    }

    public string UpdateFailedBatchTran(string paymentnum, string batchnum)
    {
        try
        {
            string status = "";
           // string telComID = TelecomId.Trim();


            datafile.UpdateFailedBatchTran(paymentnum, batchnum);
            //datafile.UpdateTransactionStatus(PegPayId, telComID, "SUCCESS", "", "01");
            status = "OK";
            return status;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string ReverseTrans(string PegPayId, string Reason)
    {
        try
        {
            string status = "";
            datafile.ReverseTransaction(PegPayId, Reason);
            status = "OK";
            return status;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string AcivateAccount(List<string> list)
    {
        string Phone=list[0].ToString();
        string pin=list[1].ToString();
        string res = datafile.ActivateAcc(Phone, pin);

        return res;
    }

    
}
