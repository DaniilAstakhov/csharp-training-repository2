using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {        
        [Test]
        public void GroupRemovalTest1()
        {
            app.GroupHelper.RemoveGroup(1);            
            app.Auth.Logout();
        }
    }
}
