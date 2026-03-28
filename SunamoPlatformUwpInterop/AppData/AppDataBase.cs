namespace SunamoPlatformUwpInterop.AppData;

/// <summary>
/// Base class for application data management providing settings storage, folder management
/// and initialization of application directories.
/// </summary>
/// <typeparam name="StorageFolder">The type representing a storage folder.</typeparam>
/// <typeparam name="StorageFile">The type representing a storage file.</typeparam>
public abstract partial class AppDataBase<StorageFolder, StorageFile> : IAppDataBase<StorageFolder, StorageFile>
{
    /// <summary>
    /// The file name used to store the path to the folder containing application files.
    /// </summary>
    public const string FolderWithAppsFiles = "folderWithAppsFiles.txt";

    private static bool isInitialized;

    /// <summary>
    /// Gets or sets the Rijndael encryption function for byte lists.
    /// </summary>
    public static Func<List<byte>, List<byte>>? RijndaelBytesEncrypt { get; set; }

    private string fileFolderWithAppsFiles = "";

    /// <summary>
    /// Gets or sets the base path for the application data.
    /// </summary>
    public string? BasePath { get; set; }

    private readonly string? folderWithAppsFilesOrDefault = null;

    /// <summary>
    /// Gets or sets the dictionary of loaded common settings.
    /// </summary>
    public Dictionary<string, string>? LoadedCommonSettings { get; set; }

    /// <summary>
    /// Gets or sets the dictionary of loaded boolean settings.
    /// </summary>
    public Dictionary<string, bool>? LoadedSettingsBool { get; set; }

    /// <summary>
    /// Gets or sets the dictionary of loaded list settings.
    /// </summary>
    public Dictionary<string, List<string>>? LoadedSettingsList { get; set; }

    /// <summary>
    /// Gets or sets the dictionary of loaded string settings.
    /// </summary>
    public Dictionary<string, string>? LoadedSettingsOther { get; set; }

    /// <summary>
    /// Gets or sets the dictionary of loaded DateTime settings.
    /// </summary>
    public Dictionary<string, DateTime>? LoadedSettingsDateTime { get; set; }

    /// <summary>
    /// After startup will be set up in AppData/Roaming.
    /// Then from fileFolderWithAppsFiles the app can load an alternative path.
    /// For all apps either AppData/Roaming or the alternative path will be valid.
    /// </summary>
    protected StorageFolder rootFolder = default!;

    /// <summary>
    /// The root folder for the parent application.
    /// </summary>
    protected StorageFolder rootFolderPa = default!;

    /// <summary>
    /// Gets or sets the Sunamo folder path.
    /// </summary>
    public string? SunamoFolder { get; set; }

    private AppDataAbstractBase<StorageFolder, StorageFile> abstractNon => (AppDataAbstractBase<StorageFolder, StorageFile>)this;

    /// <summary>
    /// Gets or sets the root folder for the application data.
    /// This path already includes ThisApp.Name.
    /// Use with caution and always ensure you are not setting it to empty string.
    /// </summary>
    public StorageFolder RootFolder
    {
        get
        {
            var isNull = abstractNon.IsRootFolderNull();
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
    /// Gets or sets the root folder for the parent application.
    /// </summary>
    public StorageFolder RootFolderPa
    {
        get
        {
            var isNull = abstractNon.IsRootFolderNull();
            if (isNull)
                throw new Exception("Application files folder was not specified. Look directly into IsRootFolderNull.");
            return rootFolderPa;
        }

        set => rootFolderPa = value;
    }

    /// <summary>
    /// Gets the file path for common settings. Must return always string, not StorageFile.
    /// In .NET Standard there is no StorageFile class and it is impossible to get Path.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <returns>The file path for the common settings file.</returns>
    public abstract string GetFileCommonSettings(string key);

    /// <summary>
    /// Gets the root folder path for common application data.
    /// </summary>
    /// <param name="isInFolderCommon">Whether to return the Common subfolder path.</param>
    /// <returns>The common root folder path.</returns>
    public abstract string RootFolderCommon(bool isInFolderCommon);

    /// <summary>
    /// Gets the path for a settings file with the given key.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <returns>The full file path to the settings file.</returns>
    public string GetPathForSettingsFile(string key)
    {
        return AppData.Instance.GetFile(AppFolders.Settings, key + ".txt");
    }

    /// <summary>
    /// Gets the file path in a subfolder within the specified application folder.
    /// </summary>
    /// <param name="appFolders">The application folder category.</param>
    /// <param name="subfolder">The subfolder name.</param>
    /// <param name="fileName">The file name without extension.</param>
    /// <param name="extension">The file extension.</param>
    /// <returns>The full file path.</returns>
    public abstract StorageFile GetFileInSubfolder(AppFolders appFolders, string subfolder, string fileName, string extension);

    /// <summary>
    /// Gets the path for the specified application folder.
    /// </summary>
    /// <param name="appFolders">The application folder category.</param>
    /// <returns>The folder path.</returns>
    public abstract StorageFolder GetFolder(AppFolders appFolders);

    /// <summary>
    /// Gets the Sunamo folder path. Must be here as Task because in UWP everything is async.
    /// </summary>
    /// <returns>The Sunamo folder path.</returns>
    public abstract StorageFolder GetSunamoFolder();

    private AppDataAppsAbstractBase<StorageFolder, StorageFile> AbstractNonApps()
    {
        return (AppDataAppsAbstractBase<StorageFolder, StorageFile>)this;
    }

    /// <summary>
    /// Gets the path to the file that stores the folder with application files configuration.
    /// </summary>
    /// <returns>The path to the folder with apps files configuration file.</returns>
    public string GetFolderWithAppsFiles()
    {
        var appDataRoaming = SpecialFoldersHelper.AppDataRoaming();
        var folder = Path.Combine(appDataRoaming, "sunamo\\Common", AppFolders.Settings.ToString());
        fileFolderWithAppsFiles = Path.Combine(folder, FolderWithAppsFiles);
        FS.CreateUpfoldersPsysicallyUnlessThere(fileFolderWithAppsFiles);
        return fileFolderWithAppsFiles;
    }

    /// <summary>
    /// Reads the folder with apps files path or returns the default value.
    /// </summary>
    /// <returns>The folder path or default value.</returns>
    public string? ReadFolderWithAppsFilesOrDefault()
    {
        return folderWithAppsFilesOrDefault;
    }
}
