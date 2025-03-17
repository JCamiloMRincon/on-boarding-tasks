using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace tests.Pages
{
    public class ShoppingPage
    {
        private readonly IPage _page;
        private readonly ILocator _inventoryList;
        private readonly ILocator _productsTitle;
        private readonly ILocator _selectItems;

        public ShoppingPage(IPage page) 
        {
            _page = page;
            _inventoryList = page.Locator("[data-test='inventory-list']");
            _productsTitle = page.Locator("//span[text()='Products']");
            _selectItems = page.Locator("select[data-test='product-sort-container']");
        }

        public async Task VerifyShoppingPage() 
        {
            var inventoryVisible = await _inventoryList.IsVisibleAsync();
            var productsTitleVisible = await _productsTitle.IsVisibleAsync();
            var selectItemsVisible = await _selectItems.IsVisibleAsync();

            Assert.Multiple(() => 
            {
                Assert.That(inventoryVisible, Is.True);
                Assert.That(productsTitleVisible, Is.True);
                Assert.That(selectItemsVisible, Is.True);
            });
        }
    }
}
