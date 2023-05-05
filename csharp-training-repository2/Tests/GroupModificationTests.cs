using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            int groupToModify = 5;
            
            GroupData newData = new GroupData("zzz");
            newData.Header = null;
            newData.Footer = null;

            if (app.GroupHelper.ChekIfGroupDoesNotExist(groupToModify))
            {
                app.GroupHelper.CreateGroupsToNuber(groupToModify);
            }

            List<GroupData> oldGroups = app.GroupHelper.GetGroupList();
            GroupData oldData = oldGroups[groupToModify - 1];

            app.GroupHelper.Modify(groupToModify, newData);

            Assert.AreEqual(oldGroups.Count, app.GroupHelper.GetGroupCount());

            List<GroupData> newGroups = app.GroupHelper.GetGroupList();
            oldGroups[groupToModify - 1].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups) 
            {
                if(group.Id == oldData.Id)
                    Assert.AreEqual(newData.Name, group.Name);
            }
        }

        [Test]
        public void GroupModificationTest2()
        {
            int groupToModify = 5;

            GroupData newData = new GroupData("zzz");
            newData.Header = null;
            newData.Footer = null;

            if (app.GroupHelper.ChekIfGroupDoesNotExist(groupToModify))
            {
                app.GroupHelper.CreateGroupsToNuber(groupToModify);
            }

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeModified = oldGroups[groupToModify - 1];
            GroupData oldData = oldGroups[groupToModify - 1];

            app.GroupHelper.Modify(toBeModified, newData);

            Assert.AreEqual(oldGroups.Count, app.GroupHelper.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[groupToModify - 1].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                    Assert.AreEqual(newData.Name, group.Name);
            }
        }
    }
}
