using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
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

            Assert.AreEqual(oldGroups.Count - 1, app.GroupHelper.GetGroupCount());

            List<GroupData> newGroups = app.GroupHelper.GetGroupList();
            GroupData toBeRemoved = oldGroups[groupToDeleteNum - 1];          
            oldGroups.RemoveAt(groupToDeleteNum - 1);           
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }    
            //app.Auth.Logout();
        }

        [Test]
        public void GroupRemovalTest2()
        {
            int groupToDeleteNum = 1;

            if (app.GroupHelper.ChekIfGroupDoesNotExist(groupToDeleteNum))
            {
                app.GroupHelper.CreateGroupsToNuber(groupToDeleteNum);
            }

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeRemoved = oldGroups[groupToDeleteNum - 1];

            app.GroupHelper.RemoveGroup(toBeRemoved);

            Assert.AreEqual(oldGroups.Count - 1, app.GroupHelper.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            
            oldGroups.RemoveAt(groupToDeleteNum - 1);
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
