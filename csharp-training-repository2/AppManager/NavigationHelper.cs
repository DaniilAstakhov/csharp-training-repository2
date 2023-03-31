﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager) { this.baseURL = baseURL; }

        public void OpenHomePage()
        {
            driver.Navigate().GoToUrl(baseURL);
        }

        public void GoToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }
        public void ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click(); // Go to home page
        }

        public void OpenMainPage() 
        {
            driver.FindElement(By.XPath("//a[.='home']")).Click();
        }
    }
}
