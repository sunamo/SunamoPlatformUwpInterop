namespace SunamoPlatformUwpInterop.AppData;

/// <summary>
/// Partial class providing settings read methods and factory functionality for AppData.
/// </summary>
public partial class AppData
{
    /// <summary>
    /// Reads a list of strings from the settings file for the specified key.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <returns>The list of strings from the settings file.</returns>
    public List<string> ReadFileOfSettingsList(string key)
    {
        return ReadFileOfSettingsWorker(LoadedSettingsList!, key);
    }

    /// <summary>
    /// Gets a common settings value for the specified key.
    /// Each application must specify which keys it uses.
    /// They are loaded at application startup and nothing related to settings
    /// is loaded after initialization.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <param name="isCrypted">Whether the value is encrypted.</param>
    /// <returns>The common settings value.</returns>
    public override string GetCommonSettings(string key, bool isCrypted = true)
    {
        if (!LoadedCommonSettings!.ContainsKey(key)) throw new Exception(key + " was not added into dependencies");

        return LoadedCommonSettings[key];
    }

    /// <summary>
    /// Creates an AppData instance configured for a specific application.
    /// </summary>
    /// <param name="rootFolderFromCreatedAppData">The root folder from an existing AppData instance.</param>
    /// <param name="appName">The target application name.</param>
    /// <returns>A new AppData instance configured for the specified application.</returns>
    public static AppData CreateForApp(string rootFolderFromCreatedAppData, string appName)
    {
        var appData = new AppData();
        appData.RootFolder = appData.GetRootFolderForApp(rootFolderFromCreatedAppData, appName);
        return appData;
    }
}
