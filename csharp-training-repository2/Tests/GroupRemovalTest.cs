using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {        
        [Test]
        public void GroupRemovalTest1()
        {
            int groupToDeleteNum = 1;

            if (app.GroupHelper.ChekIfGroupDoesNotExist(groupToDeleteNum))
            {
                app.GroupHelper.CreateGroupsToNuber(groupToDeleteNum);
            }

            List<GroupData> oldGroups = app.GroupHelper.GetGroupList();

            app.GroupHelper.RemoveGroup(groupToDeleteNum);

            List<GroupData> newGroups = app.GroupHelper.GetGroupList();
            oldGroups.RemoveAt(groupToDeleteNum - 1);
            Assert.AreEqual(oldGroups, newGroups);
            //app.Auth.Logout();
        }
    }
}
