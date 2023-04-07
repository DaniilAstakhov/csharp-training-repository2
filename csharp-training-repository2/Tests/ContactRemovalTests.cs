using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.ContactHelper.RemoveContact(1);
            //app.Auth.Logout();
        }
    }
}
