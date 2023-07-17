using System;
using System.Data;
using System.Configuration;


/// <summary>
/// Summary description for Customer
/// </summary>
//namespace InterLinkClass.EntityObjects
//{
    public class PegPayCustomer
    {
        private string fullname, fname, lname, email, phone, address, gender, placeofWork, pegpayAccountNumber, moMoAccountNumber, contactPerson, moMoAccountBalance, pegpayAccountBalance
         , customerCode, customerType, workID, passport, drivingPermit, recordedBy,password;
        private int id,chargeType;
        private double pegasusCharge;
        private bool active;
        private DateTime recordDate;

        public string PegpayAccountNumber
        {
            get
            {
                return pegpayAccountNumber;
            }
            set
            {
                pegpayAccountNumber = value;
            }
        }
        public string MoMoAccountNumber
        {
            get
            {
                return moMoAccountNumber;
            }
            set
            {
                moMoAccountNumber = value;
            }
        }
        public DateTime RecordDate
        {
            get
            {
                return recordDate;
            }
            set
            {
                recordDate = value;
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
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string FirstName
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
        public string LastName
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

        public string Fullname
        {
            get
            {
                return fullname;
            }
            set
            {
                fullname = value;
            }
        }
        public string ContactPerson
        {
            get
            {
                return contactPerson;
            }
            set
            {
                contactPerson = value;
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

        public string CustomerCode
        {
            get
            {
                return customerCode;
            }
            set
            {
                customerCode = value;
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
        public string PlaceOfWork
        {
            get
            {
                return placeofWork;
            }
            set
            {
                placeofWork = value;
            }
        }
        public string WorkId
        {
            get
            {
                return workID;
            }
            set
            {
                workID = value;
            }
        }
        public string PassportNo
        {
            get
            {
                return passport;
            }
            set
            {
                passport = value;
            }
        }
        public string DrivingPermit
        {
            get
            {
                return drivingPermit;
            }
            set
            {
                drivingPermit = value;
            }
        }
        public string RecordedBy
        {
            get
            {
                return recordedBy;
            }
            set
            {
                recordedBy = value;
            }
        }
        public string MoMoAccountBalance
        {
            get
            {
                return moMoAccountBalance;
            }
            set
            {
                moMoAccountBalance = value;
            }
        }
        public string PegpayAccountBalance
        {
            get
            {
                return pegpayAccountBalance;
            }
            set
            {
                pegpayAccountBalance = value;
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

        public double PegasusCharge
        {
            get
            {
                return pegasusCharge;
            }
            set
            {
                pegasusCharge = value;
            }
        }

        public int ChargeType
        {
            get
            {
                return chargeType;
            }
            set
            {
                chargeType = value;
            }
        }
    }
//}