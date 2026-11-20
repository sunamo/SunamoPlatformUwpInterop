namespace SunamoPlatformUwpInterop.AppData;

/// <summary>
/// Represents the cached settings type.
/// </summary>
public static class CachedSettings
{
    private static readonly Dictionary<CachedSettingsKeys, string> cache = new();

    /// <summary>
    /// Get.
    /// </summary>
    /// <param name="cachedSettingsKey">The cached settings key.</param>
    /// <returns>The get result.</returns>
    public static
        async Task<string>
        Get(CachedSettingsKeys cachedSettingsKey)
    {
        if (!cache.ContainsKey(cachedSettingsKey))
            cache.Add(cachedSettingsKey,
                await
                    File.ReadAllTextAsync(AppData.Instance.GetFileCommonSettings(cachedSettingsKey.ToString())));
        return cache[cachedSettingsKey];
    }
}
