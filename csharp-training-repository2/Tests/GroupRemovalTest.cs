using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {        
        [Test]
        public void GroupRemovalTest1()
        {
            int groupToDeleteNum = 3;

            if (app.GroupHelper.ChekIfGroupDoesNotExist(groupToDeleteNum))
            {
                app.GroupHelper.CreateGroupsToNuber(groupToDeleteNum);
            }
                    
            app.GroupHelper.RemoveGroup(groupToDeleteNum);            
            //app.Auth.Logout();
        }
    }
}
