using NUnit.Framework;

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

            app.ContactHelper.RemoveContact(ContactToDeleteNum);
            //app.Auth.Logout();
        }
    }
}
