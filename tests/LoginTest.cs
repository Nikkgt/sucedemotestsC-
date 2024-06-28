using saucedemotests.dataprovider;
using saucedemotests.ui.pages;

namespace saucedemotests.tests
{
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
            var inventroyPage = loginPage.ValidLogIn(validCreds.Item1, validCreds.Item2);

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
            loginPage = (LoginPage)loginPage.ClickLoginButton();

            Assert.That(loginPage.GetErrorMessageText().Equals(usernameRequiredErrorText), Is.True,
                 "Error message must be correct");

            loginPage = (LoginPage)loginPage.EnterUsername("user")
            .ClickLoginButton();

            Assert.That(loginPage.GetErrorMessageText().Equals(passwordRequiredErrorText), Is.True,
                "Error message must be correct");

        }
    }
}