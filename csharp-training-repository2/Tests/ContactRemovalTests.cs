using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.ContactHelper.RemoveContact(1);
            app.Auth.Logout();
        }
    }
}
