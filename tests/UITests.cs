using Microsoft.Playwright;
using Allure.NUnit.Attributes;
using tests.Pages;
using Allure.NUnit;

namespace tests
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("UITests")]
    public class UITests
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;

        [SetUp]
        public async Task Setup()
        {
            _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            
            var context = await _browser.NewContextAsync();
            _page = await context.NewPageAsync();
            await _page.GotoAsync("https://www.saucedemo.com");
        }

        [Test]
        [AllureFeature("Login")]
        public async Task LoginWithLockedOutUser()
        {
            var loginPage = new LoginPage(_page);
            await loginPage.FillLoginForm("locked_out_user", "secret_sauce");

            var errorMessage = await loginPage.GetErrorMessageAfterLogin();

            Assert.That(errorMessage, Is.EqualTo("Epic sadface: Sorry, this user has been locked out."));
        }

        [Test]
        [AllureFeature("Shopping")]
        public async Task VerifyShoppingPageTest()
        {
            var loginPage = new LoginPage(_page);
            await loginPage.FillLoginForm("standard_user", "secret_sauce");

            var shoppingPage = new ShoppingPage(_page);
            await shoppingPage.VerifyShoppingPage();
        }

        [TearDown]
        public async Task TearDown() 
        {
            await _browser.CloseAsync();
        }
    }
}
