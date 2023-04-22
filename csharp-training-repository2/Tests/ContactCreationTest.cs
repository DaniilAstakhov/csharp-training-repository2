using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreation : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(6), GenerateRandomString(6)));                
            }
            return contacts;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
            

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
