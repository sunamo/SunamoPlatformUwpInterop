namespace SunamoPlatformUwpInterop.AppData;

public partial class AppData
{
    /// <summary>
    /// Read file of settings list.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>The read file of settings list result.</returns>
    public List<string> ReadFileOfSettingsList(string key)
    {
        return ReadFileOfSettingsWorker(LoadedSettingsList!, key);
    }

    /// Each application must specify which keys it uses.
    /// They are loaded at application startup and nothing related to settings
    /// is loaded after initialization.
    public override string GetCommonSettings(string key, bool isCrypted = true)
    {
        if (!LoadedCommonSettings!.ContainsKey(key)) throw new Exception(key + " was not added into dependencies");

        return LoadedCommonSettings[key];
    }

    /// <summary>
    /// Create for app.
    /// </summary>
    /// <param name="rootFolderFromCreatedAppData">The root folder from created app data.</param>
    /// <param name="appName">The app name.</param>
    /// <returns>The create for app result.</returns>
    public static AppData CreateForApp(string rootFolderFromCreatedAppData, string appName)
    {
        var appData = new AppData();
        appData.RootFolder = appData.GetRootFolderForApp(rootFolderFromCreatedAppData, appName);
        return appData;
    }
}
