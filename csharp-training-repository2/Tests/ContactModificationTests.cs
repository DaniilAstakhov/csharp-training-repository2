using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            int ContactToModify = 15;

            ContactData editContact = new ContactData("EditName1", "EditLastName1");            

            if (app.ContactHelper.ChekIfContactDoesNotExist(ContactToModify))
            {
                app.ContactHelper.CreateContactsToNuber(ContactToModify);
            }

            List<ContactData> oldContacts = app.ContactHelper.GetContactList();

            app.ContactHelper.ModifyContact(editContact, ContactToModify);

            Assert.AreEqual(oldContacts.Count, app.ContactHelper.GetContacCount());

            List<ContactData> newContacts = app.ContactHelper.GetContactList();
            oldContacts[ContactToModify - 1].Name = editContact.Name;
            oldContacts[ContactToModify - 1].LastName = editContact.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            //app.Auth.Logout();
        }
    }
}
