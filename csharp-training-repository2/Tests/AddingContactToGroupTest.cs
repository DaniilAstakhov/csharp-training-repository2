using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTest : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            if (GroupData.GetAll().Count == 0)
            {
                app.GroupHelper.CreateGroupsToNuber(1);
            }

            GroupData group = GroupData.GetAll()[0];

            ContactData contact = new ContactData();
            List<ContactData> oldList = new List<ContactData>();
            if (group.GetContacts().Count == 0)
            {
                app.ContactHelper.CreateContactsToNuber(1);
                contact = ContactData.GetAll().First();
                oldList = group.GetContacts();
            }
            else
            {
                oldList = group.GetContacts();                                
                if (ContactData.GetAll().Count == oldList.Count)
                {
                    app.ContactHelper.CreateContactsToNuber(ContactData.GetAll().Count + 1);
                    contact = ContactData.GetAll().Except(oldList).First();
                }
                else
                contact = ContactData.GetAll().Except(oldList).First();
            }
            
            //List<ContactData> oldList = group.GetContacts();
            //ContactData contact = ContactData.GetAll().Except(oldList).First();

            app.ContactHelper.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

        [Test]
        public void TestRemoveContactFromGroup()
        {
            GroupData groupToRemoveFrom = GroupData.GetGroupWithContactsInIt()[0];
            List<ContactData> oldList = groupToRemoveFrom.GetContacts();
            ContactData contact = oldList.First();

            app.ContactHelper.RemoveContactFromGroup(contact, groupToRemoveFrom);

            List<ContactData> newList = groupToRemoveFrom.GetContacts();
            oldList.RemoveAt(0);
            newList.Sort();
            oldList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
