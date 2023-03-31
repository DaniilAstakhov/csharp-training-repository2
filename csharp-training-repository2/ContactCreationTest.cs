using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreation : TestBase
    {        
        [Test]
        public void ContactCreationTest()
        {
            OpenHomePage();
            Login(new UserData("admin", "secret"));
            InitContactCreation();
            FillContactForm(new ContactData("Name1", "LastName1"));
            SubmitContactCreation();
            ReturnToHomePage();
            Logout();
        }
    }
}
