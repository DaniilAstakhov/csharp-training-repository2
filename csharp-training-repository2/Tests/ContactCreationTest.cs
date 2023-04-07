using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreation : AuthTestBase
    {        
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Name1", "LastName1");

            app.ContactHelper.Create(contact);            
            //app.Auth.Logout();
        }
    }
}
