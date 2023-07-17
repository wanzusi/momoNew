using System;
using System.Collections.Generic;
using System.Text;

namespace InterLinkClass.EntityObjects
{
    public class UtilityDetails
    {
        private string utilityCode, utility, email, contact, createdBy, utilityTelephone;
        private DateTime creationDate;
        private bool active;

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
        public DateTime CreationDate
        {
            get
            {
                return creationDate;
            }
            set
            {
                creationDate = value;
            }
        }
        public string UtilityTelephone
        {
            get
            {
                return utilityTelephone;
            }
            set
            {
                utilityTelephone = value;
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
        public string UtilityContact
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
        public string UtilityCode
        {
            get
            {
                return utilityCode;
            }
            set
            {
                utilityCode = value;
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
    }
}
