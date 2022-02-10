// See https://aka.ms/new-console-template for more information

using System.Reflection;
using StorefrontProductWatcher.Sdk.Data;
using StorefrontProductWatcher.Sdk.Interfaces;

AppDomain.CurrentDomain.Load("StorefrontProductWatcher.Fangamer");

var watchers = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => typeof(IStorefrontWatcher).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
    .Select(t => Activator.CreateInstance(t) as IStorefrontWatcher)
    .Where(a => a != null)
    .ToList();

var newStorefrontProducts = new Dictionary<string, List<Product>>();

foreach (var watcher in watchers)
{
    var products = await watcher?.GetNewProductsAsync()!;

    newStorefrontProducts[watcher.WatcherName] = products;
}

var productActions = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => typeof(IProductAction).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
    .Select(t => Activator.CreateInstance(t) as IProductAction)
    .Where(a => a != null)
    .ToList();

foreach (var productAction in productActions)
{
    foreach (var (storefront, products) in newStorefrontProducts)
    {
        foreach (var product in products)
        {
            productAction?.Execute(storefront, product);
        }
    }
}