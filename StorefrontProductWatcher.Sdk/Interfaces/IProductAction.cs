using StorefrontProductWatcher.Sdk.Data;

namespace StorefrontProductWatcher.Sdk.Interfaces;

public interface IProductAction
{
    Task Execute(string storefront, Product product);
}
