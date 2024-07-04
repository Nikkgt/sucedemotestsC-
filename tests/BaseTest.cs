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
            config = Configuration.GetInstance();
            browserID = TestContext.Parameters.Get("browserID", config.GetBrowserID());
            LoggerUtil.LoadConfig();
        }
        [SetUp]
        public virtual void Init()
        {
            reportUtil.CreateTest();
            threadLocalDriver.Value = config.GetWebDriver(browserID);
            threadLocalDriver.Value.Navigate().GoToUrl(config.GetBaseURL());
            threadLocalDriver.Value.Manage().Window.Maximize();
            loginPage = new LoginPage(threadLocalDriver.Value);
            LoggerUtil.Info($"-- TEST [{TestContext.CurrentContext.Test.Name}] STARTED --");
        }

        [TearDown]
        public void Destroy()
        {
            LoggerUtil.Info($"-- TEST [{TestContext.CurrentContext.Test.Name}] FINISHED --");
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