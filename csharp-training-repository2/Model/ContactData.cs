﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string name;
        private string lastName;
        private string allPhones;
        private string allEMails;

        public ContactData(string name, string lastName) //конструктор для имени и фамилии
        {
            this.name = name;
            this.lastName = lastName;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;

            if (Object.ReferenceEquals(this, other))
                return true;

            return Name == other.Name && LastName == other.LastName;            
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name=" + Name + "|" + "lastname=" + LastName;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
                return 1;

            int result = Name.CompareTo(other.Name);
            if (result == 0)
                result = LastName.CompareTo(other.LastName);
            return result;
        }
        public string Name { get { return name; } set { name = value; } } 
        public string LastName { get { return lastName; } set { lastName = value; } }
        public string id { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }        
        public string EMail1 { get; set; }
        public string EMail2 { get; set; }
        public string EMail3 { get; set; }
        public string AllPhones 
        {
            get
            {
                if(allPhones != null)
                    return allPhones;
                else return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
            }
            set{
                allPhones = value;
                    } 
        }
        public string AllEMails
        {
            get
            {
                if (allEMails != null)
                    return allEMails;
                else return (CleanUp(EMail1) + CleanUp(EMail2) + CleanUp(EMail3)).Trim();
            }
            set
            {
                allEMails = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
                return "";
            
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
        }        
    }
}
