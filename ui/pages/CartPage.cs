
using OpenQA.Selenium;
using saucedemotests.pages;
using saucedemotests.ui.components.items;

namespace saucedemotests.ui.pages
{
    public class CartPage(IWebDriver driver) : BasePage(driver)
    {
        // Locators
        private static readonly By yourCartTitleLoc = By.ClassName("title");
        private static readonly By itmesLoc = By.ClassName("cart_item");
        private static readonly By checkoutButtonLoc = By.Id("checkout");
        private static readonly By continueShoppingButtonLoc = By.Id("continue-shopping");

        // Web elements
        private IWebElement yourCartTitle;
        private List<ItemCartComponent> items = new List<ItemCartComponent>();
        private IWebElement checkoutButton;
        private IWebElement continueShoppingButton;

        protected override void InitElements(IWebDriver driver)
        {
            yourCartTitle = FindElement(yourCartTitleLoc);
            foreach (var cartItemsBlock in FindElements(itmesLoc))
            {
                items.Add(new ItemCartComponent(cartItemsBlock));
            }
            checkoutButton = FindElement(checkoutButtonLoc);
            continueShoppingButton = FindElement(continueShoppingButtonLoc);
        }

        public bool IsYourCartTitleeVisible()
        {
            return yourCartTitle.Displayed;
        }

        public ItemCartComponent GetItem(int index){
            if(index<0 || index>items.Count){
                throw new ArgumentOutOfRangeException(nameof(index), index, "Index is out of range.");
            }
            return items[index];
        }

        public ItemCartComponent GetFirstItem()
        {
            if (items == null || items.Count == 0)
            {
                throw new InvalidOperationException("Cannot retrieve the first item - The items list is empty.");
            }
            return items[0];
        }

        public CartPage RemoveAllItems()
        {
           foreach(var item in items){
            item.ClickItemButton();
           }
           return this;
        }

        public InventoryPage ClickContinueShoppingButton(){
            continueShoppingButton.Click();
            return new InventoryPage(driver);
        }

        public HeaderPage GetHeaderPage(){
            return new HeaderPage(driver);
        }

    }
}