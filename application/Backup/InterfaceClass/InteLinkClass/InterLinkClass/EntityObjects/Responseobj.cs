using System;
using System.Collections.Generic;
using System.Text;

    public class Responseobj
    {
        private string vendorRef, receiptno, errorcode, message;
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
        public string Receiptno
        {
            get
            {
                return receiptno;
            }
            set
            {
                receiptno = value;
            }
        }
        public string Errorcode
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
    }
