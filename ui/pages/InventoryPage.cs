using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using saucedemotests.ui.components;
using saucedemotests.ui.components.items;
using saucedemotests.ui.pages;
using saucedemotests.utils;

namespace saucedemotests.pages
{
    public class InventoryPage(IWebDriver driver) : BasePage(driver)
    {
        // Locators
        private static readonly By productsTitleLoc = By.XPath("//*[@class='header_secondary_container']/span");
        private static readonly By itemsLoc = By.XPath("//*[@class='inventory_item']");
        private static readonly By filterSelectLoc = By.ClassName("product_sort_container");

        // Web elements
        private IWebElement productsTitle;
        private List<ItemInventoryComponent> items = new List<ItemInventoryComponent>();
        private SelectElement filterSelect;

        protected override void InitElements(IWebDriver driver)
        {
            productsTitle = FindElement(productsTitleLoc);
            foreach (IWebElement inventoryItemsBlock in FindElements(itemsLoc))
            {
                items.Add(new ItemInventoryComponent(inventoryItemsBlock));
            }
            filterSelect = new SelectElement(FindElement(filterSelectLoc));
        }

        public bool IsProductsTitleVisible()
        {
            LoggerUtil.Info("Verifying visibility of the Products title");
            return productsTitle.Displayed;
        }

        public ItemInventoryComponent GetItem(int index)
        {
            LoggerUtil.Info($"Retrieving an inventory item at index {index}");
            if (index < 0 || index > items.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "Index is out of range.");
            }
            return items[index];
        }

        public ItemInventoryComponent GetFirstItem()
        {
            LoggerUtil.Info("Retrieving a first inventory item");
            if (items == null || items.Count == 0)
            {
                throw new InvalidOperationException("Cannot retrieve the first item - The items list is empty.");
            }
            return items[0];
        }


        public HeaderPage GetHeaderPage()
        {
            return new HeaderPage(driver);
        }

    }
}