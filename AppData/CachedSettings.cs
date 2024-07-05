namespace SunamoPlatformUwpInterop.AppData;

public static class CachedSettings
{
    static Dictionary<CachedSettingsKeys, string> cs = new Dictionary<CachedSettingsKeys, string>();

    public static
#if ASYNC
    async Task<string>
#else
string
#endif
    Get(CachedSettingsKeys k)
    {
        if (!cs.ContainsKey(k))
        {
            cs.Add(k,
#if ASYNC
            await
#endif
            TFSE.ReadAllText(AppData.ci.GetFileCommonSettings(k.ToString())));
        }
        return cs[k];
    }

}
