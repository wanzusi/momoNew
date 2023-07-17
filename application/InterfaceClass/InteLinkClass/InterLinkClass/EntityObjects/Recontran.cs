using System;
using System.Collections.Generic;
using System.Text;


    public class Recontran
    {
        private string vendorRef,reason,custref,vendorCode,reconciledby,reconNo,reconType;
        private string payDate;
        private double transAmount;
        private int tranId;
        public string VendorRef
        {
            get
            {
                return vendorRef;
            }
            set
            {
                vendorRef = value;
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
        public string CustRef
        {
            get
            {
                return custref;
            }
            set
            {
                custref = value;
            }
        }
        public string Reason
        {
            get
            {
                return reason;
            }
            set
            {
                reason = value;
            }
        }
        public string ReconciledBy
        {
            get
            {
                return reconciledby;
            }
            set
            {
                reconciledby = value;
            }
        }
        public string ReconNo
        {
            get
            {
                return reconNo;
            }
            set
            {
                reconNo = value;
            }
        }
        public string ReconType
        {
            get
            {
                return reconType;
            }
            set
            {
                reconType = value;
            }
        }
        public string PayDate
        {
            get
            {
                return payDate;
            }
            set
            {
                payDate = value;
            }
        }
        public double TransAmount
        {
            get
            {
                return transAmount;
            }
            set
            {
                transAmount = value;
            }
        }
       public int TranId
        {
            get
            {
                return tranId;
            }
            set
            {
                tranId = value;
            }
        }
    }
