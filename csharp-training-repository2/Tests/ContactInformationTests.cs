using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            int contactIndex = 0;
            ContactData fromTable = app.ContactHelper.GetContactInformationFromTable(contactIndex);
            ContactData fromForm = app.ContactHelper.GetContactInformationFromEditForm(contactIndex);

            // verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEMails, fromForm.AllEMails);
        }

        [Test]
        public void TestContactInformation2()
        {
            int contactIndex = 0;
            ContactData fromInfoForm = app.ContactHelper.GetContactInformationFromInfoForm(contactIndex);
            ContactData fromForm = app.ContactHelper.GetContactInformationFromEditForm(contactIndex);

            // verification
            //Assert.AreEqual(fromInfoForm, fromForm);
            Assert.AreEqual(fromInfoForm.AllData, fromForm.AllData);         
        }
    }
}
