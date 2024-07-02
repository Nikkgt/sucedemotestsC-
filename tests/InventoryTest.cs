using saucedemotests.dataprovider;
using saucedemotests.pages;
using saucedemotests.ui.components.items;

namespace saucedemotests.tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    public class InventoryTest : BaseTest
    {
        private InventoryPage inventoryPage;
        [SetUp]
        public override void Init()
        {
            base.Init();
            var user = UserProvider.GetStandardUser();
            inventoryPage = loginPage.ValidLogIn(user.Item1, user.Item2);

        }

        [Test]
        [Description("Verify an inventory item can be added to a cart")]
        public void AddItemToCartTest()
        {
            var inventoryItem = inventoryPage.GetFirstItem();
            inventoryItem.ClickItemButton();

            Assert.That(inventoryItem.GetItemButtonText(), Is.EqualTo(ItemInventoryComponent.removeFromCartText),
             "The button must contain valid text");
            var inventoryItemModel = inventoryItem.GetItemModel();

            var cartPage = inventoryPage.GetHeaderPage()
               .ClickCartButton();

            var cartItemModel = cartPage.GetFirstItem()
                .GetItemModel();

            Assert.That(inventoryItemModel.Equals(cartItemModel), Is.True, "Items must be the same");

            // Postcondition
            cartPage.RemoveAllItems();

        }

        [Test]
        [Description("Verify an inventory item can be removed from a cart on Inventory page")]
        public void RemoveItemFromCartTest()
        {

            var inventoryItem = inventoryPage.GetFirstItem();
            inventoryItem.ClickItemButton();

            Assert.That(inventoryItem.GetItemButtonText(), Is.EqualTo(ItemInventoryComponent.removeFromCartText),
             "The button must contain valid text");
            string count = inventoryPage.GetHeaderPage()
               .GetCartItemsCount();

            Assert.That(count, Is.EqualTo("1"),
         "The count of items should be correct");

        }
    }
}