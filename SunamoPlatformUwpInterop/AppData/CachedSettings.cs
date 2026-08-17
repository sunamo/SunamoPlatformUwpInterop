namespace SunamoPlatformUwpInterop.AppData;

public static class CachedSettings
{
    private static readonly Dictionary<CachedSettingsKeys, string> cache = new();

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
