using System;
using System.Collections.Generic;
using System.Text;

namespace InterLinkClass.EntityObjects
{
    public class SystemUser
    {
        private string name, fname, sname, oname, uname, passwd, cpasswd, opasswd, username, branch,
            title, role, user, phone, email, status, action, companyCode,fileLevel,notificationtype;
        private int area, userid,userType;
        private bool active, loggedon, reset,isCoporate, isCustomer;
        private DateTime fromdate, todate;
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
        public string NotificationType
        {
            get
            {
                return notificationtype;
            }
            set
            {
                notificationtype = value;
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
        public string Sname
        {
            get
            {
                return sname;
            }
            set
            {
                sname = value;
            }
        }
        public string Oname
        {
            get
            {
                return oname;
            }
            set
            {
                oname = value;
            }
        }
        public string Uname
        {
            get
            {
                return uname;
            }
            set
            {
                uname = value;
            }
        }
        public string UserName
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

        public string FileLevel
        {
            get
            {
                return fileLevel;
            }
            set
            {
                fileLevel = value;
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
        public string Cpasswd
        {
            get
            {
                return cpasswd;
            }
            set
            {
                cpasswd = value;
            }
        }
        public string Opasswd
        {
            get
            {
                return opasswd;
            }
            set
            {
                opasswd = value;
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
        public string Role
        {
            get
            {
                return role;
            }
            set
            {
                role = value;
            }
        }
        public string Action
        {
            get
            {
                return action;
            }
            set
            {
                action = value;
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
        public int Userid
        {
            get
            {
                return userid;
            }
            set
            {
                userid = value;
            }
        }

        public int UserType
        {
            get
            {
                return userType;
            }
            set
            {
                userType = value;
            }
        }
        public string CompanyCode
        {
            get
            {
                return companyCode;
            }
            set
            {
                companyCode = value;
            }
        }
        public int Area
        {
            get
            {
                return area;
            }
            set
            {
                area = value;
            }
        }
        public string Branch
        {
            get
            {
                return branch;
            }
            set
            {
                branch = value;
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
        public bool LoggedOn
        {
            get
            {
                return loggedon;
            }
            set
            {
                loggedon = value;
            }
        }
        public bool Reset
        {
            get
            {
                return reset;
            }
            set
            {
                reset = value;
            }
        }
        public DateTime FromDate
        {
            get
            {
                return fromdate;
            }
            set
            {
                fromdate = value;
            }
        }

        public DateTime ToDate
        {
            get
            {
                return todate;
            }
            set
            {
                todate = value;
            }
        }
        public bool IsCoporate
        {
            get
            {
                return isCoporate;
            }
            set
            {
                isCoporate = value;
            }
        }
         public bool IsCustomer
        {
            get
            {
                return isCustomer;
            }
            set
            {
                isCustomer = value;
            }
        }
    }
}
