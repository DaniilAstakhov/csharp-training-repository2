﻿using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            OpenHomePage();
            Login(new UserData("admin", "secret"));
            GoToGroupsPage();
            InitGroupCreation();
            FillGroupForm(new GroupData("aaa", "sss", "ddd"));
            SubmitGroupCreation();
            ReturnToGroupsPage();
            Logout();
        }
    }
}
