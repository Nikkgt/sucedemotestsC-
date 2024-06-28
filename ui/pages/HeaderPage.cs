using OpenQA.Selenium;

namespace saucedemotests.ui.pages
{
    public class HeaderPage(IWebDriver driver) : BasePage(driver)
    {
        // Locators
        private static readonly By menuButtonLocator = By.Id("react-burger-menu-btn");
        private static readonly By swagLabsTitleLocator = By.ClassName("app_logo");
        private static readonly By cartButtonLocator = By.ClassName("shopping_cart_link");
        private static readonly By cartItemsCountLocator = By.ClassName("shopping_cart_badge");

        // Web elements
        private IWebElement menuButton;
        private IWebElement swagLabsTitle;
        private IWebElement cartButton;
        private IWebElement cartItemsCounter;

        protected override void InitElements(IWebDriver driver)
        {
            menuButton = FindElement(menuButtonLocator);
            swagLabsTitle = FindElement(swagLabsTitleLocator);
            cartButton = FindElement(cartButtonLocator);
            cartItemsCounter = FindElement(cartItemsCountLocator);
        }

        public CartPage ClickCartButton(){
            cartButton.Click();
            return new CartPage(driver);
        }

        public string GetCartItemsCount(){
            return cartItemsCounter.Text;
        }

    }
}