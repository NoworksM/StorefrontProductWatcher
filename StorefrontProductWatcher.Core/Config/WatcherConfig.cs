using System.Text.Json.Serialization;

namespace StorefrontProductWatcher.Core.Config;

public class WatcherConfig
{
    [JsonPropertyName("watcher")]
    public string Watcher { get; set; }
}
