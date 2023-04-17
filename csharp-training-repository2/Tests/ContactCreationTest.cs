using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreation : AuthTestBase
    {        
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Name1", "LastName1");

            List<ContactData> oldContacts = app.ContactHelper.GetContactList();

            app.ContactHelper.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.ContactHelper.GetContacCount());

            List<ContactData> newContacts = app.ContactHelper.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            //app.Auth.Logout();
        }
    }
}
