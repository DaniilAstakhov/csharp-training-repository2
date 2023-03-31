using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreation : TestBase
    {        
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Name1", "LastName1");

            app.ContactHelper.Create(contact);
            app.NavigationHelper.ReturnToHomePage();
            app.Auth.Logout();
        }
    }
}
