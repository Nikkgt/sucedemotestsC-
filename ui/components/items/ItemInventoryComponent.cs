using OpenQA.Selenium;

namespace saucedemotests.ui.components.items
{
    public class ItemInventoryComponent : BaseItemComponent
    {
        public const string addToCartText = "Add to cart";
        public const string removeFromCartText = "Remove";

         // Locators
        private static readonly By titleLoc = By.XPath(".//*[@class = 'inventory_item_label']/a");
        private static readonly By descriptionLoc = By.XPath(".//*[@class = 'inventory_item_label']/div");
        private static readonly By priceLoc = By.XPath(".//*[@class = 'pricebar']/div");
        private static readonly By ItemButtonLocator = By.XPath(".//*[@class='pricebar']/button");

        public ItemInventoryComponent(IWebElement itemBlock)
        {
            ItemBlock = itemBlock;
            Title = itemBlock.FindElement(titleLoc);
            Description = itemBlock.FindElement(descriptionLoc);
            Price = itemBlock.FindElement(priceLoc);
        }

        public void ClickItemButton(){
            ItemBlock.FindElement(ItemButtonLocator).Click();
        }

         public string GetItemButtonText(){
            return ItemBlock.FindElement(ItemButtonLocator).Text;
        }


    }
}