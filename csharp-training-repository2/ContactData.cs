using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    internal class ContactData
    {
        private string name;
        private string lastName;

        public ContactData(string name, string lastName) //конструктор для имени и фамилии
        {
            this.name = name;
            this.lastName = lastName;
        }
        public string Name { get { return name; } set { name = value; } } 
        public string LastName { get { return lastName; } set { lastName = value; } }
    }
}
