using EaFramework.Config;
using EAFramework.Config;
using EAFramework.Driver;

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

        [TearDown]
        public async Task TearDown()
        {
            var browser = await _driver.Browser;
            await browser.CloseAsync();
            await browser.DisposeAsync();
        }

    }
}