using OpenQA.Selenium;
using saucedemotests.models;

namespace saucedemotests.ui.components.items
{
    public abstract class BaseItemComponent
    {
        // Web elements
        public IWebElement Title { protected set; get; }
        public IWebElement Description { protected set; get; }
        public IWebElement Price { protected set; get; }
        public IWebElement ItemBlock { protected set; get; }

        public string GetPrice()
        {
            return Price.Text.Replace("$", "");
        }


        public ItemModel GetItemModel(){
            return new ItemModel(Title.Text, Description.Text, Price.Text);
        }


    }
}