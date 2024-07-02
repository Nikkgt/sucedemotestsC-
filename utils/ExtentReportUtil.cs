
using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace saucedemotests.utils
{
    public class ExtentReportUtil
    {
        public ExtentReports extentReports { set; get; }
        public ExtentTest test { set; get; }
        private readonly string reportPath;

        public ExtentReportUtil()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            reportPath = Path.Combine(projectDirectory, "index.html");

            var htmlReporter = new ExtentSparkReporter(reportPath);
            extentReports = new ExtentReports();
            extentReports.AttachReporter(htmlReporter);
            extentReports.AddSystemInfo("Description", "test results");

        }

        public void CreateTest()
        {
            test = extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        public void GenerateRuslt(IWebDriver driver)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            DateTime now = DateTime.Now;
            string format = "h:mm:ss";
            string screenshotName = $"screenshot_{now.ToString(format)}.png";

            if (status == TestStatus.Failed)
            {
                test.Fail("Test failed", CaptureScreenshot(driver, screenshotName));
                test.Log(Status.Fail, TestContext.CurrentContext.Result.StackTrace);
            }
            extentReports.Flush();
        }

        private static Media CaptureScreenshot(IWebDriver driver, string screenshotName)
        {
           string encodedScreenshot = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
           return MediaEntityBuilder.CreateScreenCaptureFromBase64String(encodedScreenshot, screenshotName).Build();
        }

    }
}