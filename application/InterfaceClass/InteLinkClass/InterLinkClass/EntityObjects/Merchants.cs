using System;
using System.Collections.Generic;
using System.Text;

namespace InterLinkClass.EntityObjects
{
    public class Merchant
    {
        private string clientId, terminalId, operatorId, password, pegpayVendorCode, errorMessage;
        private int merchantId;
        private bool active;

        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
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
        public int MerchantId
        {
            get
            {
                return merchantId;
            }
            set
            {
                merchantId = value;
            }
        }
        public string PegPayVendorCode
        {
            get
            {
                return pegpayVendorCode;
            }
            set
            {
                pegpayVendorCode = value;
            }
        }
        public string ClientId
        {
            get
            {
                return clientId;
            }
            set
            {
                clientId = value;
            }
        }
        public string TerminalId
        {
            get
            {
                return terminalId;
            }
            set
            {
                terminalId = value;
            }
        }
        public string OperatorId
        {
            get
            {
                return operatorId;
            }
            set
            {
                operatorId = value;
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
    }

}
