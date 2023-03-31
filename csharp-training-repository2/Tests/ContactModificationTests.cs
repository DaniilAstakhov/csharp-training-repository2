using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData editContact = new ContactData("EditName1", "EditLastName1");

            app.ContactHelper.ModifyContact(editContact);
            app.Auth.Logout();
        }
    }
}
