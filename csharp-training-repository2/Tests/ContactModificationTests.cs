using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            int ContactToModify = 1;

            ContactData editContact = new ContactData("EditName1", "EditLastName1");            

            if (app.ContactHelper.ChekIfContactDoesNotExist(ContactToModify))
            {
                app.ContactHelper.CreateContactsToNuber(ContactToModify);
            }

            app.ContactHelper.ModifyContact(editContact, ContactToModify);
            //app.Auth.Logout();
        }
    }
}
