
using OpenQA.Selenium;
using saucedemotests.config;
using saucedemotests.ui.pages;

namespace saucedemotests.tests
{
    public abstract class BaseTest
    {
        private WebDriver driver;
        private Configuration config;
        protected LoginPage loginPage;

        [SetUp]
        public virtual void Init()
        {
            config = Configuration.GetInstance();
            driver = config.GetWebDriver();
            driver.Navigate().GoToUrl(config.GetBaseURL());
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(0);
            driver.Manage().Window.Maximize();
            loginPage = new LoginPage(driver);
        }

        [TearDown]
        public void Destroy()
        {
            driver?.Dispose();
        }
    }
}