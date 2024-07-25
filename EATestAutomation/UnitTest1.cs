using EAApplicationTest.Pages;
using EaFramework.Config;
using EAFramework.Config;
using EAFramework.Driver;
using Microsoft.Playwright;

namespace EAApplicationTest
{
    public class Tests
    {
        private PlaywrightDriver _driver;
        private PlaywrightDriverInitializer _playwrightDriverinitializer;
        private TestSettings _settings;

        [SetUp]
        public void Setup()
        {

            _settings = ConfigReader.ReadConfig();

            _playwrightDriverinitializer = new PlaywrightDriverInitializer();

            _driver = new PlaywrightDriver(_settings, _playwrightDriverinitializer);
        }

        [Test]
        public async Task Test1()
        {
            var page = await _driver.Page;
            await page.GotoAsync(_settings.ApplicationUrl);
            await page.ClickAsync("text=Login");
        }

        [Test]
        public async Task LoginTest()
        {
            var page = await _driver.Page;
            await page.GotoAsync(_settings.ApplicationUrl);
            await page.ClickAsync("text=Login");
            await page.GetByLabel("Username").FillAsync("admin");
            await page.GetByLabel("Password").FillAsync("password");
        }

        [Test]
        public async Task MyTest()
        {
            var page = await _driver.Page;
            await page.GotoAsync("http://localhost:33084/");

            ProductListPage productListPage = new ProductListPage(page);

            await productListPage.CreateProductAsync();

            await page.GetByLabel("Name").ClickAsync();
            await page.GetByLabel("Name").FillAsync("Not UPS");
            await page.GetByLabel("Description").ClickAsync();
            await page.GetByLabel("Description").FillAsync("Something related to a power supply");
            await page.Locator("#Price").ClickAsync();
            await page.Locator("#Price").FillAsync("135");
            await page.GetByLabel("ProductType").SelectOptionAsync(new[] { "3" });
            await page.GetByRole(AriaRole.Button, new() { Name = "Create" }).ClickAsync();

            await page.GetByRole(AriaRole.Row, new() { Name = "71 Not UPS Something related" }).GetByRole(AriaRole.Link).First.ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Back to List" }).ClickAsync();
            await page.GetByRole(AriaRole.Row, new() { Name = "71 Not UPS Something related" }).GetByRole(AriaRole.Link).Nth(1).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Edit" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            var browser = await _driver.Browser;
            await browser.CloseAsync();
            await browser.DisposeAsync();
        }

    }
}