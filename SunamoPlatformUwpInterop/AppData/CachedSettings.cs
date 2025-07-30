namespace SunamoPlatformUwpInterop.AppData;

public static class CachedSettings
{
    private static readonly Dictionary<CachedSettingsKeys, string> cs = new();

    public static
#if ASYNC
        async Task<string>
#else
string
#endif
        Get(CachedSettingsKeys k)
    {
        if (!cs.ContainsKey(k))
            cs.Add(k,
#if ASYNC
                await
#endif
                    File.ReadAllTextAsync(AppData.ci.GetFileCommonSettings(k.ToString())));
        return cs[k];
    }
}