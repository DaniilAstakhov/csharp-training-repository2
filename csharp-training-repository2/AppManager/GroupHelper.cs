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

        public void RemoveGroup(GroupData group)
        {
            manager.NavigationHelper.GoToGroupsPage();
            SelectGroup(group.Id);
            RemoveGroupButtonClick();
            ReturnToGroupsPage();
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

        public void Modify(GroupData group, GroupData newData)
        {
            manager.NavigationHelper.GoToGroupsPage();
            SelectGroup(group.Id);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();
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

            GroupData group = new GroupData("aaa");
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
            groupCache = null;
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
            groupCache = null;
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

        public GroupHelper SelectGroup(string id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='"+id+"'])")).Click();
            return this;
        }

        public GroupHelper RemoveGroupButtonClick()
        {
            driver.FindElement(By.Name("delete")).Click();
            groupCache = null;
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

        private List<GroupData> groupCache = null;

        public List<GroupData> GetGroupList()
        {
            if (groupCache == null)
            {
                groupCache = new List<GroupData>();
                manager.NavigationHelper.GoToGroupsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                foreach (IWebElement element in elements)
                {                                                                                             
                    groupCache.Add(new GroupData(null)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
                string allGroupNames = driver.FindElement(By.CssSelector("div#content form")).Text;
                string[] parts = allGroupNames.Split('\n');
                int shift = groupCache.Count - parts.Length;
                for (int i = 0; i < groupCache.Count; i++)
                {
                   if (i < shift)
                    {
                        groupCache[i].Name = "";
                    }
                   else 
                    {
                        groupCache[i].Name = parts[i - shift].Trim();
                    }                    
                }
            }                
            return new List<GroupData>(groupCache);
        }

        public int GetGroupCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }
    }
}
