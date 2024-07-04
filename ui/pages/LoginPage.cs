using OpenQA.Selenium;
using saucedemotests.pages;
using saucedemotests.utils;

namespace saucedemotests.ui.pages
{
    public class LoginPage(IWebDriver driver) : BasePage(driver)
    {
        public const string PageUrl = "https://www.saucedemo.com/";

        // Locators
        private static readonly By findElementLoc = By.Id("user-name");
        private static readonly By passwordFieldLoc = By.Id("password");
        private static readonly By loginButtonLoc = By.Id("login-button");
        private static readonly By errorMessageLoc = By.XPath("//h3[@data-test='error']");

        // Web elements
        private IWebElement usernameField;
        private IWebElement passwordField;
        private IWebElement loginButton;
        private IWebElement errorMessage;


        protected override void InitElements(IWebDriver driver)
        {
            usernameField = FindElement(findElementLoc);
            passwordField = FindElement(passwordFieldLoc);
            loginButton = FindElement(loginButtonLoc);
        }

        public bool IsPageOpened()
        {
            LoggerUtil.Info("Verifing that the Login page is opened");
            return usernameField.Displayed
            && passwordField.Displayed
            && loginButton.Displayed
            && driver.Url.Equals(PageUrl);
        }

        public InventoryPage ValidLogIn(string username, string password)
        {
            LoggerUtil.Info($"Log in with the credentials: {username} and {password}");
            usernameField.SendKeys(username);
            passwordField.SendKeys(password);
            loginButton.Click();

            return new InventoryPage(driver);
        }

        public string InvalidLogInAndGetError(string username, string password)
        {
            LoggerUtil.Info($"Invalid Log in with the credentials: {username} and {password}");
            usernameField.SendKeys(username);
            passwordField.SendKeys(password);
            loginButton.Click();

            return GetErrorMessageText();
        }

        public string GetErrorMessageText()
        {
            LoggerUtil.Info("Retrieving an error message");
            errorMessage = FindElement(errorMessageLoc);
            return errorMessage.Text;
        }

        public LoginPage EnterUsername(string username)
        {
            LoggerUtil.Info($"Entering the {username} username");
            usernameField.SendKeys(username);
            return this;
        }

        public LoginPage EnterPassword(string password)
        {
            LoggerUtil.Info($"Entering the {password} password");
            passwordField.SendKeys(password);
            return this;
        }

        public BasePage ClickLoginButton()
        {
            LoggerUtil.Info($"Clicking on the Login button");
            loginButton.Click();
            if (IsPageOpened())
            {
                return this;
            }
            else
            {
                return new InventoryPage(driver);
            }
        }
    }
}