using System;
using System.Collections.Generic;
using System.Text;

namespace InterLinkClass.EntityObjects
{
   
    public class CustomerKYC
    {
        private string vendorcode,password, fname, lname, otherName,contact1,contact2,gender,nationality,address,email,customerType,businessType,tradingName,companyRegNo,companyTin,region,district,customerIdType,customerIdNo,createdBy;
        private DateTime dateofBirth;
        private bool isactive;
        private string username, spid;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public string Spid
        {
            get
            {
                return spid;
            }
            set
            {
                spid = value;
            }
        }

        public string Vendorcode
        {
            get
            {
                return vendorcode;
            }
            set
            {
                vendorcode = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
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
        public string OtherName
        {
            get
            {
                return otherName;
            }
            set
            {
                otherName = value;
            }
        }
        public string Contact1
        {
            get
            {
                return contact1;
            }
            set
            {
                contact1 = value;
            }
        }
        public string Contact2
        {
            get
            {
                return contact2;
            }
            set
            {
                contact2 = value;
            }
        }
        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
            }
        }
        public string Nationality
        {
            get
            {
                return nationality;
            }
            set
            {
                nationality = value;
            }
        }
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
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
        public string BusinessType
        {
            get
            {
                return businessType;
            }
            set
            {
                businessType = value;
            }

        }
        public string TradingName
        {
            get
            {
                return tradingName;
            }
            set
            {
                tradingName = value;
            }
        }
        public string CompanyRegNo
        {
            get
            {
                return companyRegNo;
            }
            set
            {
                companyRegNo = value;
            }
        }
        public string CompanyTin
        {
            get
            {
                return companyTin;
            }
            set
            {
                companyTin = value;
            }
        }
        public string Region
        {
            get
            {
                return region;
            }
            set
            {
                region = value;
            }
        }
        public string CustomerIdType
        {
            get
            {
                return customerIdType;
            }
            set
            {
                customerIdType = value;
            }
        }
        public string District
        {
            get
            {
                return district;
            }
            set
            {
                district = value;
            }
        }
        public string CustomerIdNo
        {
            get
            {
                return customerIdNo;
            }
            set
            {
                customerIdNo = value;
            }
        }
        public DateTime DateofBirth
        {
            get
            {
                return dateofBirth;
            }
            set
            {
                dateofBirth = value;
            }
        }

        public bool Isactive
        {
            get
            {
                return isactive;
            }
            set
            {
                isactive = value;
            }
        }

        public string CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }
    }
}
