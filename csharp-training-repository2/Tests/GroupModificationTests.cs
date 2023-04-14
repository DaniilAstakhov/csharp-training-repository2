using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            int groupToModify = 10;
            
            GroupData newData = new GroupData("zzz");
            newData.Header = null;
            newData.Footer = null;

            if (app.GroupHelper.ChekIfGroupDoesNotExist(groupToModify))
            {
                app.GroupHelper.CreateGroupsToNuber(groupToModify);
            }

            List<GroupData> oldGroups = app.GroupHelper.GetGroupList();

            app.GroupHelper.Modify(groupToModify, newData);

            List<GroupData> newGroups = app.GroupHelper.GetGroupList();
            oldGroups[groupToModify - 1].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
