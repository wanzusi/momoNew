using System;
using System.Collections.Generic;
using System.Text;

    public class InvoiceTran
    {
        private string invoiceserial, fname, mname, lname, phone, email, user, errorcode,
            error, paytypecode,shortname, paytype, districtcode, regioncode;
        private double amount, bal, paidamount, vat;
        private bool active, vatable;

        public string InvoiceSerial
        {
            get
            {
                return invoiceserial;
            }
            set
            {
                invoiceserial = value;
            }
        }
        public string Fname
        {
            get
            {
                return fname;
            }
            set
            {
                fname = value;
            }
        }
        public string Mname
        {
            get
            {
                return mname;
            }
            set
            {
                mname = value;
            }
        }
        public string Lname
        {
            get
            {
                return lname;
            }
            set
            {
                lname = value;
            }
        }
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
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
        public string PayTypeCode
        {
            get
            {
                return paytypecode;
            }
            set
            {
                paytypecode = value;
            }
        }
        public string ShortName
        {
            get
            {
                return shortname;
            }
            set
            {
                shortname = value;
            }
        }
        public string PayType
        {
            get
            {
                return paytype;
            }
            set
            {
                paytype = value;
            }
        }
        public string DistrictCode
        {
            get
            {
                return districtcode;
            }
            set
            {
                districtcode = value;
            }
        }
        public string RegionCode
        {
            get
            {
                return regioncode;
            }
            set
            {
                regioncode = value;
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
        public string Error
        {
            get
            {
                return error;
            }
            set
            {
                error = value;
            }
        }
        public string ErrorCode
        {
            get
            {
                return errorcode;
            }
            set
            {
                errorcode = value;
            }
        }
        public double Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
            }
        }
        public double PaidAmount
        {
            get
            {
                return paidamount;
            }
            set
            {
                paidamount = value;
            }
        }

        public double Vat
        {
            get
            {
                return vat;
            }
            set
            {
                vat = value;
            }
        }
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }
        public bool Vatable
        {
            get
            {
                return vatable;
            }
            set
            {
                vatable = value;
            }
        }
    }

