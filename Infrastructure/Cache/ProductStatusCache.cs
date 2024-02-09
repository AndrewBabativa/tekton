using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

public class ProductStatusCache
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<ProductStatusCache> _logger;
    private readonly object _lock = new object();

    public ProductStatusCache(IMemoryCache cache, ILogger<ProductStatusCache> logger)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Dictionary<int, string> GetProductStatusDictionary()
    {
        if (!_cache.TryGetValue("ProductStatusDictionary", out Dictionary<int, string> productStatusDictionary))
        {
            lock (_lock)
            {
                if (!_cache.TryGetValue("ProductStatusDictionary", out productStatusDictionary))
                {
                    _logger.LogInformation("Logging_ProductLoaded");

                    productStatusDictionary = LoadProductStatusDictionary();
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    };
                    _cache.Set("ProductStatusDictionary", productStatusDictionary, cacheEntryOptions);
                }
            }
        }

        return productStatusDictionary;
    }

    private Dictionary<int, string> LoadProductStatusDictionary()
    {
        _logger.LogInformation("Carga del diccionario desde la fuente de datos...");
        return new Dictionary<int, string>
        {
            { 1, "Active" },
            { 0, "Inactive" }
        };
    }
}
