using Microsoft.Playwright;

namespace EaApplicationTest.Pages;

public class ProductPage
{
    private readonly IPage _page;

    public ProductPage(IPage page)
    {
        _page = page;
    }

    private ILocator _txtName => _page.GetByLabel("Name");

    private ILocator _txtDescription => _page.GetByLabel("Description");

    private ILocator _txtPrice => _page.Locator("#Price");

    private ILocator _selectProduct => _page.GetByRole(AriaRole.Combobox, new() { Name = "ProductType" });

    private ILocator _lnkCreate => _page.GetByRole(AriaRole.Button, new() { Name = "Create" });

    public async Task CreateProduct(string name, string description, decimal price, string productType)
    {
        await _txtName.FillAsync(name);
        await _txtDescription.FillAsync(description);
        await _txtPrice.FillAsync(price.ToString());
        await _selectProduct.SelectOptionAsync(productType);
    }

    public async Task ClickCreate() => await _lnkCreate.ClickAsync();

}