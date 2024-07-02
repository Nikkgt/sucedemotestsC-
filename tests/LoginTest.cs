using Newtonsoft.Json;
using saucedemotests.dataprovider;
using saucedemotests.pococlasses;
using saucedemotests.ui.pages;
using saucedemotests.utils;

namespace saucedemotests.tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    public class LoginTest : BaseTest
    {
        private const string invalidCredsErrorText = "Epic sadface: Username and password do not match any user in this service";
        private const string usernameRequiredErrorText = "Epic sadface: Username is required";
        private const string passwordRequiredErrorText = "Epic sadface: Password is required";


        [Test]
        [Description("Verify login with valid credentials")]
        public void ValidLoginTest()
        {
            var validCreds = UserProvider.GetStandardUser();
            var inventroyPage = loginPage.ValidLogIn(validCreds.Item1, "validCreds.Item2");

            Assert.That(inventroyPage.IsProductsTitleVisible(), Is.True, "The 'Products' title should be visible");
        }

        [Test]
        [Description("Verify login with invalid password")]
        public void LoginWithInvalidPasswordTest()
        {
            var validCreds = UserProvider.GetStandardUser();
            string invalidPassword = "fakePass123";
            string errorMessage = loginPage.InvalidLogInAndGetError(validCreds.Item1, invalidPassword);

            Assert.That(errorMessage.Equals(invalidCredsErrorText), Is.True, "Error message must be correct");
            Assert.That(loginPage.IsPageOpened(), Is.True, "The 'LogIn' page should be opened");

        }

        [Test]
        [Description("Verify login with empty credentials")]
        public void LoginWithEmptyCredsTest()
        {
            JsonReaderUtil jsonReader = new("testdata/users.json");
            TestContext.Progress.WriteLine(jsonReader.JContent);
            loginPage = (LoginPage)loginPage.ClickLoginButton();

            Assert.That(loginPage.GetErrorMessageText().Equals(usernameRequiredErrorText), Is.True,
                 "Error message must be correct");

            loginPage = (LoginPage)loginPage.EnterUsername("user")
            .ClickLoginButton();

            Assert.That(loginPage.GetErrorMessageText().Equals(passwordRequiredErrorText), Is.True,
                "Error message must be correct");

        }

        [Test]
        [TestCaseSource(nameof(GetTestUserDataForLogin))]
        [Description("Verify valid login with all users")]
        public void LoginWithWithAllUsersTest(string username, string password)
        {
            var inventroyPage = loginPage.ValidLogIn(username, password);

            Assert.That(inventroyPage.IsProductsTitleVisible(), Is.True, "The 'Products' title should be visible");

        }

        private static object[] GetTestUserDataForLogin()
        {
            JsonReaderUtil jsonReader = new("testdata/users.json");
            TestContext.Progress.WriteLine(jsonReader.JContent);
            UsersPOCO users = JsonConvert.DeserializeObject<UsersPOCO>(jsonReader.JContent);

            var testCases = new object[users.Users.Count];
            for (int i = 0; i < users.Users.Count; i++)
            {
                testCases[i] = new object[] { users.Users[i].Username, users.Users[i].Password };
            }
            return testCases;
        }
    }
}