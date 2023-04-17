using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            int ContactToDeleteNum = 4;

            if (app.ContactHelper.ChekIfContactDoesNotExist(ContactToDeleteNum))
            {
                app.ContactHelper.CreateContactsToNuber(ContactToDeleteNum);
            }

            List<ContactData> oldContacts = app.ContactHelper.GetContactList();

            app.ContactHelper.RemoveContact(ContactToDeleteNum);

            Assert.AreEqual(oldContacts.Count - 1, app.ContactHelper.GetContacCount());

            List<ContactData> newContacts = app.ContactHelper.GetContactList();
            oldContacts.RemoveAt(ContactToDeleteNum - 1);
            Assert.AreEqual(oldContacts, newContacts);
            //app.Auth.Logout();
        }
    }
}
