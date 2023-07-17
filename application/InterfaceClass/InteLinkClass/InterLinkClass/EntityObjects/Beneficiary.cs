using System;
using System.Collections.Generic;
using System.Text;

namespace InterLinkClass.EntityObjects
{
    public class Beneficiary
    {
        private string name, code, typeCode, reason, recordedBy, mobile, country, city, bank, address, email, customerCode, batchCode, paymentDate, transferType, location, networkCode, paymentReason, password, fromAccount, title, telRegisteredNumber;

        public string TelRegisteredNumber
        {
            get { return telRegisteredNumber; }
            set { telRegisteredNumber = value; }
        }
        private int recordCode, customerId, id;
        private bool active;
        private double amount, transCharge, mNOCharge, cashOutCharge,pegasusCharge;
        
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }

        public string PaymentReason
        {
            get
            {
                return paymentReason;
            }
            set
            {
                paymentReason = value;
            }
        }

        public string FromAccount
        {
            get
            {
                return fromAccount;
            }
            set
            {
                fromAccount = value;
            }
        }
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }
        public string Bank
        {
            get
            {
                return bank;
            }
            set
            {
                bank = value;
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
        public string BatchCode
        {
            get
            {
                return batchCode;
            }
            set
            {
                batchCode = value;
            }
        }

        public string PaymentDate
        {
            get
            {
                return paymentDate;
            }
            set
            {
                paymentDate = value;
            }
        }
        public string TransferType
        {
            get
            {
                return transferType;
            }
            set
            {
                transferType = value;
            }
        }
        public string Mobile
        {
            get
            {
                return mobile;
            }
            set
            {
                mobile = value;
            }
        }

        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        public string NetworkCode
        {
            get
            {
                return networkCode;
            }
            set
            {
                networkCode = value;
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
        public string TypeCode
        {
            get
            {
                return typeCode;
            }
            set
            {
                typeCode = value;
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
        public double TransferAmount
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
        public double TransCharge
        {
            get
            {
                return transCharge;
            }
            set
            {
                transCharge = value;
            }
        }
        public double MNOCharge
        {
            get
            {
                return mNOCharge;
            }
            set
            {
                mNOCharge = value;
            }
        }
        public double CashOutCharge
        {
            get
            {
                return cashOutCharge;
            }
            set
            {
                cashOutCharge = value;
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
        public int RecordCode
        {
            get
            {
                return recordCode;
            }
            set
            {
                recordCode = value;
            }
        }
        public int CustomerId
        {
            get
            {
                return customerId;
            }
            set
            {
                customerId = value;
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

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
    }

}
