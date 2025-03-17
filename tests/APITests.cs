using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Playwright;
using Allure.NUnit.Attributes;
using Allure.NUnit;

namespace tests
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("APITests")]
    public class APITests
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;
        private HttpClient _client;
        private const string BaseUrl = "https://gorest.co.in/public/v1/";
        private const string AccessToken = "3a29ffb616e724ec2ecfd3761bbc6019bc920be6da1a761b370c7a95ec26310f";

        [SetUp]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync();
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();

            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
        }

        [Test]
        [AllureFeature("User management")]
        public async Task CreateNewUserTest()
        {
            var newUser = new
            {
                name = "Juan Pérez",
                gender = "male",
                email = $"juan.perez{Guid.NewGuid()}@example.com",
                status = "active"
            };

            var response = await _client.PostAsJsonAsync("users", newUser);
            Assert.That(response.IsSuccessStatusCode, Is.True);

            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content, Does.Contain(newUser.name));
        }

        [Test]
        [AllureFeature("User management")]
        public async Task GetUsersListTest()
        {
            var response = await _client.GetAsync("users");
            Assert.That(response.IsSuccessStatusCode, Is.True);

            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content, Does.Contain("\"meta\""));
        }

        [TearDown]
        public async Task Cleanup()
        {
            await _browser.CloseAsync();
            _client.Dispose();
        }
    }
}
