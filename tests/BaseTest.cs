
using AventStack.ExtentReports;
using OpenQA.Selenium;
using saucedemotests.config;
using saucedemotests.ui.pages;
using saucedemotests.utils;

namespace saucedemotests.tests
{
    public abstract class BaseTest
    {
        private Configuration config;
        protected LoginPage loginPage;
        protected ThreadLocal<IWebDriver> threadLocalDriver = new ThreadLocal<IWebDriver>();
        private string browserID;
        private ExtentReportUtil reportUtil;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            reportUtil = new ExtentReportUtil();
        }
        [SetUp]
        public virtual void Init()
        {
            reportUtil.CreateTest();
            config = Configuration.GetInstance();
            browserID = TestContext.Parameters.Get("browserID", config.GetBrowserID());
            threadLocalDriver.Value = config.GetWebDriver(browserID);
            threadLocalDriver.Value.Navigate().GoToUrl(config.GetBaseURL());
            threadLocalDriver.Value.Manage().Window.Maximize();
            loginPage = new LoginPage(threadLocalDriver.Value);
        }

        [TearDown]
        public void Destroy()
        {
            reportUtil.GenerateRuslt(threadLocalDriver.Value);
            threadLocalDriver.Value.Quit();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            if (threadLocalDriver.IsValueCreated)
            {
                threadLocalDriver.Value.Dispose();
                threadLocalDriver.Dispose();
            }
        }
    }
}