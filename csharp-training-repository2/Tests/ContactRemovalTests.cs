using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            int contactToDeleteNum = 1;

            if (app.ContactHelper.ChekIfContactDoesNotExist(contactToDeleteNum))
            {
                app.ContactHelper.CreateContactsToNuber(contactToDeleteNum);
            }

            List<ContactData> oldContacts = app.ContactHelper.GetContactList();

            app.ContactHelper.RemoveContact(contactToDeleteNum);

            Assert.AreEqual(oldContacts.Count - 1, app.ContactHelper.GetContacCount());

            List<ContactData> newContacts = app.ContactHelper.GetContactList();
            oldContacts.RemoveAt(contactToDeleteNum - 1);
            Assert.AreEqual(oldContacts, newContacts);
            //app.Auth.Logout();
        }

        [Test, Repeat(10)]
        public void ContactRemovalTest2()
        {
            int contactToDeleteNum = 1;

            if (app.ContactHelper.ChekIfContactDoesNotExist(contactToDeleteNum))
            {
                app.ContactHelper.CreateContactsToNuber(contactToDeleteNum);
            }

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeRemoved = oldContacts[contactToDeleteNum - 1];

            app.ContactHelper.RemoveContact(toBeRemoved);

            Assert.AreEqual(oldContacts.Count - 1, app.ContactHelper.GetContacCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.RemoveAt(contactToDeleteNum - 1);
            Assert.AreEqual(oldContacts, newContacts);
            //app.Auth.Logout();
        }
    }
}
