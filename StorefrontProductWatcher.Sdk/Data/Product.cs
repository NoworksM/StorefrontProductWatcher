namespace StorefrontProductWatcher.Sdk.Data;

public record Product(Guid Id, string Name, string Url, string? ImageUrl, decimal Price);
