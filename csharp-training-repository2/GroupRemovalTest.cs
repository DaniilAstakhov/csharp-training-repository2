using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {        
        [Test]
        public void GroupRemovalTest1()
        {
            OpenHomePage();
            Login(new UserData("admin", "secret"));
            GoToGroupsPage();
            SelectGroup(1);
            RemoveGroup();
            ReturnToGroupsPage();
            Logout();
        }
    }
}
