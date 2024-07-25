using Microsoft.Playwright;

namespace EAApplicationTest.Pages
{
    public class ProductListPage
    {
        private readonly IPage _page;

        public ProductListPage(IPage page)
        {
            _page = page;
        }
        
        private ILocator _lnkProductList => _page.GetByRole(AriaRole.Link, new() { Name = "Product" });

        private ILocator _lnkCreate => _page.GetByRole(AriaRole.Link, new() { Name = "Create" });

        public async Task CreateProductAsync()
        {
            await _lnkProductList.ClearAsync();
            await _lnkCreate.ClickAsync();
        }
    }
}
