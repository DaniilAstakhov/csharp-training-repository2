using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
            return this;
        }

        public void RemoveContact(ContactData contact)
        {
            manager.NavigationHelper.GoToMainPage();
            SelectContact(contact.id);
            RemoveContactButtonClick();
            manager.NavigationHelper.AcceptAlertWindow();
            manager.NavigationHelper.GoToMainPage();
        }

        public ContactHelper CreateContactsToNuber(int contactToDeleteNum)
        {
            manager.NavigationHelper.GoToMainPage();
            int numToAdd = ChekHowManyContactsNeedToAdd();

            ContactData contact = new ContactData("test", "test1");
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

        public void ModifyContact(ContactData editContact, ContactData contact)
        {
            manager.NavigationHelper.GoToMainPage();
            EditContactButtonClick(contact.id);
            FillContactForm(editContact);
            UpdateContact();
            manager.NavigationHelper.ReturnToHomePage();
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

        public ContactHelper EditContactButtonClick(string id)
        {
            driver.FindElement(By.XPath("//input[@id='"+id+"']/../..//img[@alt='Edit']")).Click();
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

        public ContactHelper SelectContact(string id)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']//input[@id='"+id+"']")).Click();
            return this;
        }

        private void SelectContactCheckBox(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
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
            string information = driver.FindElement(By.Id("content")).Text;
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

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.NavigationHelper.GoToMainPage();
            ClearGroupFilter();
            SelectContactCheckBox(contact.id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        private void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public void RemoveContactFromGroup(ContactData contact, GroupData groupToRemoveFrom)
        {
            manager.NavigationHelper.GoToMainPage();
            SelectGroupInFilter(groupToRemoveFrom);
            SelectContactCheckBox(contact.id);
            CommitRemovingContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void CommitRemovingContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private void SelectGroupInFilter(GroupData groupToRemoveFrom)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByValue(groupToRemoveFrom.Id);
        }
    }
}
