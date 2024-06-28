using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace saucedemotests.ui.pages
{
    public abstract class BasePage
    {
        protected IWebDriver driver;
        protected readonly WebDriverWait wait;

        public BasePage(IWebDriver driver, int secondsToWait = 10)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
            InitElements(driver);
        }

        protected abstract void InitElements(IWebDriver driver);

        protected void WaitForElementToDisplay(IWebElement elementToWait)
        {
            wait.Until(d =>
            {
                try
                {
                    return elementToWait.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });

        }

        protected IWebElement FindElement(By locator){
            return driver.FindElement(locator);
        }
         protected ReadOnlyCollection<IWebElement> FindElements(By locator){
            return driver.FindElements(locator);
        }
    }
}