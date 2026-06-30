namespace SunamoPlatformUwpInterop.AppData;

public abstract partial class AppDataBase<StorageFolder, StorageFile> : IAppDataBase<StorageFolder, StorageFile>
{
    public const string FolderWithAppsFiles = "folderWithAppsFiles.txt";

    private static bool isInitialized;

    public static Func<List<byte>, List<byte>>? RijndaelBytesEncrypt { get; set; }

    private string folderWithAppsFilesPath = "";

    public string? BasePath { get; set; }

    private readonly string? folderWithAppsFilesOrDefault = null;

    public Dictionary<string, string>? LoadedCommonSettings { get; set; }

    public Dictionary<string, bool>? LoadedSettingsBool { get; set; }

    public Dictionary<string, List<string>>? LoadedSettingsList { get; set; }

    public Dictionary<string, string>? LoadedSettingsOther { get; set; }

    public Dictionary<string, DateTime>? LoadedSettingsDateTime { get; set; }

    /// After startup will be set up in AppData/Roaming.
    /// Then from folderWithAppsFilesPath the app can load an alternative path.
    /// For all apps either AppData/Roaming or the alternative path will be valid.
    protected StorageFolder rootFolder = default!;

    protected StorageFolder rootFolderPa = default!;

    public string? SunamoFolder { get; set; }

    /// Root for non-backed-up folders (Logs, Output, Cache, Temp). Points to %LOCALAPPDATA%\sunamo\AppName, never to OneDrive.
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

    public abstract string RootFolderCommon(bool isInFolderCommon);

    public string GetPathForSettingsFile(string key)
    {
        return AppData.Instance.GetFile(AppFolders.Settings, key + ".txt");
    }

    public abstract StorageFile GetFileInSubfolder(AppFolders appFolders, string subfolder, string fileName, string extension);

    public abstract StorageFolder GetFolder(AppFolders appFolders);

    /// Gets the Sunamo folder path. Must be here as Task because in UWP everything is async.
    public abstract StorageFolder GetSunamoFolder();

    public string GetFolderWithAppsFiles()
    {
        var appDataRoaming = SpecialFoldersHelper.AppDataRoaming();
        var folder = Path.Combine(appDataRoaming, "sunamo\\Common", AppFolders.Settings.ToString());
        folderWithAppsFilesPath = Path.Combine(folder, FolderWithAppsFiles);
        FS.CreateUpfoldersPsysicallyUnlessThere(folderWithAppsFilesPath);
        return folderWithAppsFilesPath;
    }

    public string? ReadFolderWithAppsFilesOrDefault()
    {
        return folderWithAppsFilesOrDefault;
    }
}
