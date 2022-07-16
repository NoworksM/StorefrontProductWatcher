using StorefrontProductWatcher.Sdk.Data;

namespace StorefrontProductWatcher.Sdk.Interfaces;

public interface IBatchProductAction
{
    Task ExecuteAsync(string storefront, IEnumerable<Product> products, IDictionary<string, object> config);
}
