
using OpenQA.Selenium;

namespace saucedemotests.ui.components.items
{
    public class ItemCartComponent : BaseItemComponent
    {

         // Locators
        private static readonly By titleLoc = By.XPath(".//*[@id = 'item_4_title_link']");
        private static readonly By descriptionLoc = By.XPath(".//*[@class = 'inventory_item_desc']");
        private static readonly By priceLoc = By.XPath(".//*[@class = 'item_pricebar']/div");
        private static readonly By ItemButtonLocator = By.XPath(".//*[@class='item_pricebar']/button");

        public ItemCartComponent(IWebElement itemBlock)
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