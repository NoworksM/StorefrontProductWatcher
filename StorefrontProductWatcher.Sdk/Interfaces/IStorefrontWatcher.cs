using StorefrontProductWatcher.Sdk.Data;

namespace StorefrontProductWatcher.Sdk.Interfaces;

public interface IStorefrontWatcher
{
    string StorefrontName { get; }
    string WatcherName { get; }

    Task<List<Product>> GetNewProductsAsync();
}
