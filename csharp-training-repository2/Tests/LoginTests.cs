using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            //prepare
            app.Auth.Logout();

            //action
            UserData account = new UserData("admin", "secret");
            app.Auth.Login(account);

            //verification
            Assert.IsTrue(app.Auth.IsLoggedIn(account));
        }
        
        [Test]
        public void LoginWithInValidCredentials()
        {
            //prepare
            app.Auth.Logout();

            //action
            UserData account = new UserData("admin", "123456");
            app.Auth.Login(account);

            //verification
            Assert.IsFalse(app.Auth.IsLoggedIn(account));
        }
    }
}
