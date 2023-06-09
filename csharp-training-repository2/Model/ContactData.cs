﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Policy;
using LinqToDB.Mapping;
using Microsoft.Office.Interop.Excel;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string name;
        private string lastName;
        private string allPhones;
        private string allEMails;
        private string allData;
        private string id;

        public ContactData()
        {

        }
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

            if (this.Id == null || this.Id == "" || other.Id == null || other.Id == "")
            {
                return Name == other.Name && LastName == other.LastName;
            }
            return Name == other.Name && LastName == other.LastName && Id == other.Id;            
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name= " + Name + "|" + "lastname= " + LastName + " | " + "id= " + Id;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
                return 1;

            int result = Name.CompareTo(other.Name);
            if (result == 0)
                result = LastName.CompareTo(other.LastName);
            if (result == 0)
            {
                if (Id == null || Id == "" || other.Id == null || other.Id == "")
                {
                    return result;
                }
                result = Id.CompareTo(other.Id);
            }
                
            return result;
        }

        [Column(Name = "firstname"), NotNull]
        public string Name { get { return name; } set { name = value; } }

        [Column(Name = "lastname"), NotNull]
        public string LastName { get { return lastName; } set { lastName = value; } }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "address"), NotNull]
        public string Address { get; set; }

        [Column(Name = "home"), NotNull]
        public string HomePhone { get; set; }

        [Column(Name = "mobile"), NotNull]
        public string MobilePhone { get; set; }

        [Column(Name = "work"), NotNull]
        public string WorkPhone { get; set; }

        [Column(Name = "email"), NotNull]
        public string EMail1 { get; set; }

        [Column(Name = "email2"), NotNull]
        public string EMail2 { get; set; }

        [Column(Name = "email3"), NotNull]
        public string EMail3 { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

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
            
            return Regex.Replace(phone, "[ ()-]", "") + "\r\n";
        }

        private string SmartCleanUp(string value, int parameter)
        {
            if (value == null || value == "")
                return "";
                switch (parameter)
                {
                    case 1: value = "H: " + value; break;
                    case 2: value = "M: " + value; break;
                    case 3: value = "W: " + value; break;
                    default:
                        break;  
                }
            return value;
        }

        public string AllData
        {
            get
            {
                if (allData != null)
                {
                    return allData;
                }
                else return (Name + " " + LastName + "\r\n\r\n" + Address + SmartCleanUp(HomePhone , 1) + "\r\n" + SmartCleanUp(MobilePhone, 2) + "\r\n" + SmartCleanUp(WorkPhone, 3) + "\r\n\r\n" + EMail1 + "\r\n" + EMail2 + "\r\n" + EMail3).Trim();
            }
            set 
            { 
                allData = value; 
            }    
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }
    }
}
