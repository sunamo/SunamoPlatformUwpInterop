namespace SunamoPlatformUwpInterop.AppData;

/// <summary>
/// Represents the app data base storage folder storage file type.
/// </summary>
public abstract partial class AppDataBase<StorageFolder, StorageFile> : IAppDataBase<StorageFolder, StorageFile>
{
    /// <summary>
    /// The folder with apps files constant.
    /// </summary>
    public const string FolderWithAppsFiles = "roamingSunamoRoot.txt";

    private static bool isInitialized;

    /// <summary>
    /// Gets or sets the rijndael bytes encrypt.
    /// </summary>
    public static Func<List<byte>, List<byte>>? RijndaelBytesEncrypt { get; set; }

    private string folderWithAppsFilesPath = "";

    /// <summary>
    /// Gets or sets the base path.
    /// </summary>
    public string? BasePath { get; set; }

    private readonly string? folderWithAppsFilesOrDefault = null;

    /// <summary>
    /// Gets or sets the loaded common settings.
    /// </summary>
    public Dictionary<string, string>? LoadedCommonSettings { get; set; }

    /// <summary>
    /// Gets or sets the loaded settings bool.
    /// </summary>
    public Dictionary<string, bool>? LoadedSettingsBool { get; set; }

    /// <summary>
    /// Gets or sets the loaded settings list.
    /// </summary>
    public Dictionary<string, List<string>>? LoadedSettingsList { get; set; }

    /// <summary>
    /// Gets or sets the loaded settings other.
    /// </summary>
    public Dictionary<string, string>? LoadedSettingsOther { get; set; }

    /// <summary>
    /// Gets or sets the loaded settings date time.
    /// </summary>
    public Dictionary<string, DateTime>? LoadedSettingsDateTime { get; set; }

    /// After startup will be set up in AppData/Roaming.
    /// Then from folderWithAppsFilesPath the app can load an alternative path.
    /// For all apps either AppData/Roaming or the alternative path will be valid.
    protected StorageFolder rootFolder = default!;

    /// <summary>
    /// The root folder pa.
    /// </summary>
    protected StorageFolder rootFolderPa = default!;

    /// <summary>
    /// Gets or sets the sunamo folder.
    /// </summary>
    public string? SunamoFolder { get; set; }

    /// Root for non-backed-up folders (Logs, Output, Reports, Backup). Points to %LOCALAPPDATA%\_Sunamo\AppName, never to OneDrive.
    public string LocalRootFolder { get; set; } = "";

    private AppDataAbstractBase<StorageFolder, StorageFile> desktopBase => (AppDataAbstractBase<StorageFolder, StorageFile>)this;

    /// Gets or sets the root folder for the application data.
    /// This path already includes ThisApp.Name.
    /// Use with caution and always ensure you are not setting it to empty string.
    public StorageFolder RootFolder
    {
        get
        {
            var isNull = desktopBase.IsRootFolderNull();
            if (isNull)
                throw new Exception("Application files folder was not specified. Look directly into IsRootFolderNull.");
            return rootFolder;
        }

        set
        {
            if (value != null && char.IsLower(value.ToString()![0]))
                ThrowEx.FirstLetterIsNotUpper(value.ToString()!);
            rootFolder = value;
        }
    }

    /// <summary>
    /// The root folder pa.
    /// </summary>
    public StorageFolder RootFolderPa
    {
        get
        {
            var isNull = desktopBase.IsRootFolderNull();
            if (isNull)
                throw new Exception("Application files folder was not specified. Look directly into IsRootFolderNull.");
            return rootFolderPa;
        }

        set => rootFolderPa = value;
    }

    /// Gets the file path for common settings. Must return always string, not StorageFile.
    /// In .NET Standard there is no StorageFile class and it is impossible to get Path.
    public abstract string GetFileCommonSettings(string key);

    /// <summary>
    /// Initializes a new instance of the root folder common class.
    /// </summary>
    /// <param name="isNull">The is null.</param>
    public abstract string RootFolderCommon(bool isInFolderCommon);

    /// <summary>
    /// Get path for settings file.
    /// </summary>
    /// <param name="!">The !.</param>
    /// <returns>The get path for settings file result.</returns>
    public string GetPathForSettingsFile(string key)
    {
        return AppData.Instance.GetFile(AppFolders.Settings, key + ".txt");
    }

    /// <summary>
    /// Initializes a new instance of the get file in subfolder class.
    /// </summary>
    /// <param name="!">The !.</param>
    public abstract StorageFile GetFileInSubfolder(AppFolders appFolders, string subfolder, string fileName, string extension);

    /// <summary>
    /// Get folder.
    /// </summary>
    /// <returns>The get folder result.</returns>
    public abstract StorageFolder GetFolder(AppFolders appFolders);

    /// Gets the Sunamo folder path. Must be here as Task because in UWP everything is async.
    public abstract StorageFolder GetSunamoFolder();

    /// <summary>
    /// Get folder with apps files.
    /// </summary>
    /// <returns>The get folder with apps files result.</returns>
    public string GetFolderWithAppsFiles()
    {
        var appDataRoaming = SpecialFoldersHelper.AppDataRoaming();
        var folder = Path.Combine(appDataRoaming, "_Sunamo");
        folderWithAppsFilesPath = Path.Combine(folder, FolderWithAppsFiles);
        FS.CreateUpfoldersPsysicallyUnlessThere(folderWithAppsFilesPath);
        return folderWithAppsFilesPath;
    }

    /// <summary>
    /// Read folder with apps files or default.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>The read folder with apps files or default result.</returns>
    public string? ReadFolderWithAppsFilesOrDefault()
    {
        return folderWithAppsFilesOrDefault;
    }
}
