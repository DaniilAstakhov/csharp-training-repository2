using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {


        public ContactHelper(ApplicationManager manager) : base(manager) { }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click(); // Submit Contact creation
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.Name);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.LastName);
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        internal ContactHelper Create(ContactData contact)
        {

            manager.NavigationHelper.OpenMainPage();
            InitContactCreation();
            FillContactForm(new ContactData("Name1", "LastName1"));
            SubmitContactCreation();
            return this;
        }
    }
}
