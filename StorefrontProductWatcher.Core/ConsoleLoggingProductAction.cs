using StorefrontProductWatcher.Sdk.Data;
using StorefrontProductWatcher.Sdk.Interfaces;

namespace StorefrontProductWatcher.Core;

public class ConsoleLoggingProductAction : IProductAction
{

    public Task Execute(string storefront, Product product)
    {
        Console.WriteLine($"New {storefront} product: {product.Name} ${product.Price}");

        return Task.CompletedTask;
    }
}
