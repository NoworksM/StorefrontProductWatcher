using Microsoft.Playwright;
using StorefrontProductWatcher.Sdk;
using StorefrontProductWatcher.Sdk.Data;
using StorefrontProductWatcher.Sdk.Interfaces;

namespace StorefrontProductWatcher.Fangamer;

public class FangamerStorefrontWatcher : IStorefrontWatcher
{
    public string StorefrontName => "Fangamer";
    public string WatcherName => "StorefrontProductWatcher.FangamerStorefrontWatcher";

    public async Task<List<Product>> GetNewProductsAsync()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Firefox.LaunchAsync(new()
        {
            // Headless = false,
            // SlowMo = 50
        });
        var page = await browser.NewPageAsync();
        await page.GotoAsync("https://www.fangamer.com/collections/new-and-featured");

        var productElements = page.Locator("#products-inner .item-view");

        var products = new List<Product>();

        async Task<bool> MoveToNextPageAsync()
        {
            try
            {
                string nextButtonSelector = "#pagination .next-page.page-link";

                var nextButton = page.Locator(nextButtonSelector);

                await nextButton.ClickAsync();
                page.Locator(nextButtonSelector);
            }
            catch (TimeoutException)
            {
                return false;
            }

            return true;
        }

        do
        {
            for (int idx = 0; idx < await productElements.CountAsync(); idx++)
            {
                var productElement = productElements.Nth(idx);

                var link = productElement.Locator("> a");

                string url = await link.GetAttributeAsync("href") ?? string.Empty;

                var figure = productElement.Locator("figure.item-view-dotmatrix");

                var images = figure.Locator(".img-container > img");

                ILocator image;

                if (await images.CountAsync() > 1)
                {
                    image = images.First;
                }
                else
                {
                    image = images;
                }

                string? imageUrl = await image.GetAttributeAsync("src");

                var titleElement = figure.Locator("figcaption span.title");

                string title = await titleElement.InnerTextAsync();

                var priceElement = figure.Locator("figcaption span.price");

                string rawPrice = await priceElement.InnerTextAsync();

                decimal price = decimal.Parse(rawPrice.Substring(1, rawPrice.Length - 1));

                var product = new Product(Guid.NewGuid(), title, url, imageUrl, price);

                products.Add(product);
            }
        } while (await MoveToNextPageAsync());

        return products;
    }
}
