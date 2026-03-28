namespace SunamoPlatformUwpInterop.AppData;

/// <summary>
/// Provides caching for common settings values to avoid repeated file reads.
/// </summary>
public static class CachedSettings
{
    private static readonly Dictionary<CachedSettingsKeys, string> cache = new();

    /// <summary>
    /// Gets the cached value for the specified settings key, reading from file if not yet cached.
    /// </summary>
    /// <param name="cachedSettingsKey">The settings key to retrieve.</param>
    /// <returns>The cached settings value.</returns>
    public static
#if ASYNC
        async Task<string>
#else
        string
#endif
        Get(CachedSettingsKeys cachedSettingsKey)
    {
        if (!cache.ContainsKey(cachedSettingsKey))
            cache.Add(cachedSettingsKey,
#if ASYNC
                await
#endif
                    File.ReadAllTextAsync(AppData.Instance.GetFileCommonSettings(cachedSettingsKey.ToString())));
        return cache[cachedSettingsKey];
    }
}
