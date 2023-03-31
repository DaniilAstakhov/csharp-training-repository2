using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {


        protected ApplicationManager app;

        [SetUp]
        public void SetupTest()
        {
            app = new ApplicationManager();
            app.NavigationHelper.OpenHomePage();
            app.Auth.Login(new UserData("admin", "secret"));
        }

        [TearDown]
        public void TeardownTest()
        {
            app.Stop();
        }
    }
}
