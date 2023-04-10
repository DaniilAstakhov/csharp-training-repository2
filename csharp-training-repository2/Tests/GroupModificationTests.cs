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
            int groupToModify = 1;
            
            GroupData newData = new GroupData("zzz");
            newData.Header = null;
            newData.Footer = null;

            if (app.GroupHelper.ChekIfGroupDoesNotExist(groupToModify))
            {
                app.GroupHelper.CreateGroupsToNuber(groupToModify);
            }

            app.GroupHelper.Modify(groupToModify, newData);
        }
    }
}
