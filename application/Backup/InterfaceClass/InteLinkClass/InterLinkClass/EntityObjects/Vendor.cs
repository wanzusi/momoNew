using System;
using System.Collections.Generic;
using System.Text;

namespace InterLinkClass.EntityObjects
{
    public class Vendor
    {
        private string vendorCode, billsyscode, vendor, email, contact, passwd, user, subject, message, status, accountManager, accountRep;
        private int vendorid, invalidCount, chargeType;
        private bool active, sendemail, reset, isRequiredCert;

        public string Parameter = "";
        public string Value = "";

        public string AccountRep
        {
            get { return accountRep; }
            set { accountRep = value; }
        }
        public string AccountManager
        {
            get { return accountManager; }
            set { accountManager = value; }
        }
        private double pegpayCharge, minBal;

        public double MinBal
        {
            get { return minBal; }
            set { minBal = value; }
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
        public string Subject
        {
            get
            {
                return subject;
            }
            set
            {
                subject = value;
            }
        }
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
        public string BillSysCode
        {
            get
            {
                return billsyscode;
            }
            set
            {
                billsyscode = value;
            }
        }
        public string VendorName
        {
            get
            {
                return vendor;
            }
            set
            {
                vendor = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        public string Contract
        {
            get
            {
                return contact;
            }
            set
            {
                contact = value;
            }
        }
        public string Passwd
        {
            get
            {
                return passwd;
            }
            set
            {
                passwd = value;
            }
        }
        public string User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }
        public int Vendorid
        {
            get
            {
                return vendorid;
            }
            set
            {
                vendorid = value;
            }
        }
        public int InvalidCount
        {
            get
            {
                return invalidCount;
            }
            set
            {
                invalidCount = value;
            }
        }
        public bool Active
        {
            get
            {
                return active; /// 0701047275 Irene
            }
            set
            {
                active = value;
            }
        }
        public bool Sendemail
        {
            get
            {
                return sendemail; /// 0701047275 Irene
            }
            set
            {
                sendemail = value;
            }
        }
        public bool Reset
        {
            get
            {
                return reset; /// 0701047275 Irene
            }
            set
            {
                reset = value;
            }
        }

        public bool IsRequiredCert
        {
            get
            {
                return isRequiredCert; /// 0701047275 Irene
            }
            set
            {
                isRequiredCert = value;
            }
        }
        public double PegpayCharge
        {
            get
            {
                return pegpayCharge; /// 0701047275 Irene
            }
            set
            {
                pegpayCharge = value;
            }
        }

        public int ChargeType
        {
            get
            {
                return chargeType; /// 0701047275 Irene
            }
            set
            {
                chargeType = value;
            }
        }
    }
}
