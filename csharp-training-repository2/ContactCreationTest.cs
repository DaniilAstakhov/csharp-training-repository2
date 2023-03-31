using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreation : TestBase
    {        
        [Test]
        public void ContactCreationTest()
        {
            navigationHelper.OpenHomePage();
            loginHelper.Login(new UserData("admin", "secret"));
            contactHelper.InitContactCreation();
            contactHelper.FillContactForm(new ContactData("Name1", "LastName1"));
            contactHelper.SubmitContactCreation();
            navigationHelper.ReturnToHomePage();
            loginHelper.Logout();
        }
    }
}
