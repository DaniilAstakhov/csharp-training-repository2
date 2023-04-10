﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase
    {

        public GroupHelper(ApplicationManager manager) : base(manager) { }

        public GroupHelper Create(GroupData group)
        {
            manager.NavigationHelper.GoToGroupsPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }
        public GroupHelper RemoveGroup(int v)
        {
            manager.NavigationHelper.GoToGroupsPage();
            SelectGroup(v);
            RemoveGroupButtonClick();
            ReturnToGroupsPage();
            return this;
        }
        public GroupHelper Modify(int v, GroupData newData)
        {
            manager.NavigationHelper.GoToGroupsPage();
            SelectGroup(v);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();
            return this;
        }
        public bool ChekIfGroupDoesNotExist(int groupNum)
        {
            manager.NavigationHelper.GoToGroupsPage();
            if (IsElementPresent(By.XPath("//div[@id='content']/form/span[" + groupNum + "]/input")))
                return false;
            else
                return true;
        }

        public GroupHelper CreateGroupsToNuber(int groupNum)
        {
            manager.NavigationHelper.GoToGroupsPage();
            int numToAdd = ChekHowManyGroupsNeedToAdd();

            GroupData group = new GroupData("");
            for (int i = 0; i < groupNum - numToAdd; i++)
            {
                Create(group);
            }
            return this;
        }

        public int ChekHowManyGroupsNeedToAdd()
        {
            IReadOnlyCollection<IWebElement> numToAdd = driver.FindElements(By.XPath("//div[@id='content']/form/span/input"));
            return numToAdd.Count();
        }

        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/span[" + index + "]/input")).Click();
            return this;
        }

        public GroupHelper RemoveGroupButtonClick()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }
        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);            
            return this;
        }
    }
}
