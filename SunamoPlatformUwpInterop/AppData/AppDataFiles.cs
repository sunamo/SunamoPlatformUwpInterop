namespace SunamoPlatformUwpInterop.AppData;

public partial class AppData
{
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

    public static AppData CreateForApp(string rootFolderFromCreatedAppData, string appName)
    {
        var appData = new AppData();
        appData.RootFolder = appData.GetRootFolderForApp(rootFolderFromCreatedAppData, appName);
        return appData;
    }
}
