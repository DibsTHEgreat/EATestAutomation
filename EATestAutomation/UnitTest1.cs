using EaApplicationTest.Pages;
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
        public async Task Test2()
        {
            var page = await _driver.Page;

            await page.GotoAsync("http://localhost:33084/");

            ProductListPage productListPage = new ProductListPage(page);
            ProductPage productPage = new ProductPage(page);


            await productListPage.CreateProductAsync();
            await productPage.CreateProduct("Speaker", "Gaming Speaker", 2000, "2");
            await productPage.ClickCreate();

            await productListPage.ClickProductFromList("Speaker");


            var element = productListPage.IsProductCreated("Speaker");
            await Assertions.Expect(element).ToBeVisibleAsync();
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