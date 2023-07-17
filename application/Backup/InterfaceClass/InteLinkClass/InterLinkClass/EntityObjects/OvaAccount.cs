using System;
using System.Collections.Generic;
using System.Text;

namespace InterLinkClass.EntityObjects
{
    public class OvaAccount
    {
        public string Balance = "";
        public string Status = "";
        public string Currency = "";
        public string SenderId = "";
        public string SpId = "";
        public string Password = "";
        public string Msisdn = "";
        public double Threshold;
        public List<Vendor> vendors;
    }
}
