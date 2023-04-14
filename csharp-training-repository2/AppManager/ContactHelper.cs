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

        public ContactHelper Create(ContactData contact)
        {
            manager.NavigationHelper.GoToMainPage();
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.NavigationHelper.ReturnToHomePage();
            return this;
        }

        public ContactHelper RemoveContact(int v)
        {
            manager.NavigationHelper.GoToMainPage();
            SelectContact(v);
            RemoveContactButtonClick();
            manager.NavigationHelper.AcceptAlertWindow();
            //Можно будет добавить шаги, если удасться наладить проблему с удалением контактов в учбеном приложении
            return this;
        }

        public ContactHelper CreateContactsToNuber(int contactToDeleteNum)
        {
            manager.NavigationHelper.GoToMainPage();
            int numToAdd = ChekHowManyContactsNeedToAdd();

            ContactData contact = new ContactData("", "");
            for (int i = 0; i < contactToDeleteNum - numToAdd; i++)
            {
                Create(contact);
            }
            return this;
        }

        public int ChekHowManyContactsNeedToAdd()
        {
            IReadOnlyCollection<IWebElement> numToAdd = driver.FindElements(By.XPath("//tr[@name='entry']//*[@type='checkbox']"));
            return numToAdd.Count();
        }

        public bool ChekIfContactDoesNotExist(int contactToDeleteNum)
        {
            manager.NavigationHelper.GoToMainPage();
            if (IsElementPresent(By.XPath("//tr[@name='entry'][" + contactToDeleteNum + "]//*[@type='checkbox']")))
                return false;
            else
                return true;
        }

        public ContactHelper ModifyContact(ContactData editContact, int num)
        {
            manager.NavigationHelper.GoToMainPage();
            EditContactButtonClick(num);
            FillContactForm(editContact);
            UpdateContact();
            return this;
        }

        public ContactHelper UpdateContact()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper EditContactButtonClick(int v)
        {
            driver.FindElement(By.XPath("//tr[@name='entry']["+ v +"]//img[@alt='Edit']")).Click();
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click(); // Submit Contact creation
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Name);
            Type(By.Name("lastname"), contact.LastName);
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper RemoveContactButtonClick()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper SelectContact(int v)
        {
            driver.FindElement(By.XPath("//tr[@name='entry']["+ v +"]//*[@type='checkbox']")).Click();
            return this;
        }

        public List<ContactData> GetContactList()
        {
            List<ContactData> contacts = new List<ContactData>();
            manager.NavigationHelper.GoToMainPage();
            ICollection<IWebElement> names = driver.FindElements(By.XPath("//table[@id='maintable']//tr/td[3]"));
            ICollection<IWebElement> lastNames = driver.FindElements(By.XPath("//table[@id='maintable']//tr/td[2]"));

            for (int i = 0; i < names.Count; i++)
            {
                contacts.Add(new ContactData(names.ElementAt(i).Text, lastNames.ElementAt(i).Text));
            }
            return contacts;
        }
    }
}
