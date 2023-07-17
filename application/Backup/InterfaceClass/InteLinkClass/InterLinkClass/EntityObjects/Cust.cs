using System;
using System.Collections.Generic;
using System.Text;

namespace InterLinkClass.EntityObjects
{
    public class Cust
    {
        private string custRef, custName, balance, paymentType, customerType, utility, utilityType,reason;

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
        public string UtilityType
        {
            get
            {
                return utilityType;
            }
            set
            {
                utilityType = value;
            }
        }
        public string Utility
        {
            get
            {
                return utility;
            }
            set
            {
                utility = value;
            }
        }
        public string CustomerType
        {
            get
            {
                return customerType;
            }
            set
            {
                customerType = value;
            }
        }
        public string PaymentType
        {
            get
            {
                return paymentType;
            }
            set
            {
                paymentType = value;
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
        public string CustName
        {
            get
            {
                return custName;
            }
            set
            {
                custName = value;
            }
        }
        public string CustRef
        {
            get
            {
                return custRef;
            }
            set
            {
                custRef = value;
            }
        }

    }
}
