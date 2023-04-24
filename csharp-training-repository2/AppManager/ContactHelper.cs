using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
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
            manager.NavigationHelper.GoToMainPage();
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
            manager.NavigationHelper.ReturnToHomePage();
            return this;
        }

        public ContactHelper UpdateContact()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
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
            contactCache = null;
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
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int v)
        {
            driver.FindElement(By.XPath("//tr[@name='entry']["+ v +"]//*[@type='checkbox']")).Click();
            return this;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.NavigationHelper.GoToMainPage();
                ICollection<IWebElement> names = driver.FindElements(By.XPath("//table[@id='maintable']//tr/td[3]"));
                ICollection<IWebElement> lastNames = driver.FindElements(By.XPath("//table[@id='maintable']//tr/td[2]"));
                for (int i = 0; i < names.Count; i++)
                {
                    contactCache.Add(new ContactData(names.ElementAt(i).Text, lastNames.ElementAt(i).Text));
                }
            }
            return new List<ContactData>(contactCache);
        }

        public int GetContacCount()
        {
            return driver.FindElements(By.XPath("//table[@id='maintable']//tr/td[3]")).Count();
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.NavigationHelper.GoToMainPage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;
            string allEMails = cells[4].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEMails = allEMails,
            };

        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.NavigationHelper.GoToMainPage();
            EditContactButtonClick(index + 1);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string eMail1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            string eMail2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string eMail3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                WorkPhone = workPhone,
                MobilePhone = mobilePhone,
                EMail1 = eMail1,
                EMail2 = eMail2,
                EMail3 = eMail3,
            };
        }

        public ContactData GetContactInformationFromInfoForm(int index)
        {
            manager.NavigationHelper.GoToMainPage();
            ContactInformationButtonClick(index + 1);
            string information = driver.FindElement(By.Id("content")).GetAttribute("textContent");
            return new ContactData(null, null)
            {
                AllData = information.Trim(),
            };
        }

        public ContactHelper ContactInformationButtonClick(int v)
        {
            driver.FindElement(By.XPath("//tr[@name='entry'][" + v + "]//img[@alt='Details']")).Click();
            return this;
        }
    }
}
