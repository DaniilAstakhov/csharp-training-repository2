using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            int contactToModify = 15;

            ContactData editContact = new ContactData("EditName1", "EditLastName1");            

            if (app.ContactHelper.ChekIfContactDoesNotExist(contactToModify))
            {
                app.ContactHelper.CreateContactsToNuber(contactToModify);
            }

            List<ContactData> oldContacts = app.ContactHelper.GetContactList();

            app.ContactHelper.ModifyContact(editContact, contactToModify);

            Assert.AreEqual(oldContacts.Count, app.ContactHelper.GetContacCount());

            List<ContactData> newContacts = app.ContactHelper.GetContactList();
            oldContacts[contactToModify - 1].Name = editContact.Name;
            oldContacts[contactToModify - 1].LastName = editContact.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            //app.Auth.Logout();
        }

        [Test]
        public void ContactModificationTest2()
        {
            int contactToModify = 10;

            ContactData editContact = new ContactData("EditName1", "EditLastName1");

            if (app.ContactHelper.ChekIfContactDoesNotExist(contactToModify))
            {
                app.ContactHelper.CreateContactsToNuber(contactToModify);
            }

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeModified = oldContacts[contactToModify - 1];

            app.ContactHelper.ModifyContact(editContact, toBeModified);

            Assert.AreEqual(oldContacts.Count, app.ContactHelper.GetContacCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[contactToModify - 1].Name = editContact.Name;
            oldContacts[contactToModify - 1].LastName = editContact.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            //app.Auth.Logout();
        }
    }
}
