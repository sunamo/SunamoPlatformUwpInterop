namespace SunamoPlatformUwpInterop.AppData;

/// <summary>
/// Provides application data management for desktop applications.
/// Manages root folders, settings files, and common configuration.
/// </summary>
public partial class AppData : AppDataAbstractBase<string, string>
{
    /// <summary>
    /// Singleton instance of the AppData class.
    /// </summary>
    public static AppData Instance = new();
    public static AppData ci = Instance;

    private AppData()
    {
    }

    /// <summary>
    /// Gets the Sunamo folder path from the configuration file.
    /// Falls back to AppData\Roaming\sunamo if not configured.
    /// </summary>
    /// <returns>The Sunamo folder path.</returns>
    public override string GetSunamoFolder()
    {
        var result = Instance.GetFolderWithAppsFiles();
        var sunamoFolderPath = File.ReadAllText(result);

        if (char.IsLower(sunamoFolderPath[0])) ThrowEx.FirstLetterIsNotUpper(sunamoFolderPath);

        if (string.IsNullOrWhiteSpace(sunamoFolderPath))
            sunamoFolderPath = Path.Combine(SpecialFoldersHelper.AppDataRoaming(), "sunamo");
        return sunamoFolderPath;
    }

    /// <summary>
    /// Gets the file path in a subfolder within the specified application folder.
    /// </summary>
    /// <param name="appFolders">The application folder category.</param>
    /// <param name="subfolder">The subfolder name.</param>
    /// <param name="fileName">The file name without extension.</param>
    /// <param name="extension">The file extension.</param>
    /// <returns>The full file path.</returns>
    public override string GetFileInSubfolder(AppFolders appFolders, string subfolder, string fileName, string extension)
    {
        return Instance.GetFile(AppFolders.Output, subfolder + @"\" + fileName + extension);
    }

    /// <summary>
    /// Gets the common root folder path, always located in User's AppData.
    /// </summary>
    /// <param name="isInFolderCommon">Whether to return the Common subfolder path.</param>
    /// <returns>The common root folder path.</returns>
    public override string RootFolderCommon(bool isInFolderCommon)
    {
        var sunamoPath = Path.Combine(SpecialFoldersHelper.AppDataRoaming(), "sunamo");
        var redirect = GetSunamoFolder();
        if (!string.IsNullOrEmpty(redirect)) sunamoPath = redirect;
        if (isInFolderCommon) return Path.Combine(sunamoPath, "Common");
        return sunamoPath;
    }

    /// <summary>
    /// Gets the file path as a string for the specified application folder name and file name.
    /// </summary>
    /// <param name="appFolderName">The application folder name as a string.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="isUsingParentAppFolder">Whether to use the parent application root folder.</param>
    /// <returns>The file path as a string.</returns>
    public override string GetFileString(string appFolderName, string fileName, bool isUsingParentAppFolder = false)
    {
        var rootPath = RootFolder;
        if (isUsingParentAppFolder) rootPath = RootFolderPa;
        var folder = Path.Combine(rootPath, appFolderName);
        var filePath = Path.Combine(folder, fileName);
        return filePath;
    }

    /// <summary>
    /// Gets the file path for the specified application folder name and file name.
    /// </summary>
    /// <param name="appFolderName">The application folder name as a string.</param>
    /// <param name="fileName">The file name.</param>
    /// <returns>The file path.</returns>
    public string GetFileString(string appFolderName, string fileName)
    {
        return GetFileString(appFolderName, fileName, false);
    }

    /// <summary>
    /// Gets the file path for the specified file in the given application folder.
    /// </summary>
    /// <param name="appFolders">The application folder category.</param>
    /// <param name="fileName">The file name.</param>
    /// <returns>The file path.</returns>
    public override string GetFile(AppFolders appFolders, string fileName)
    {
        return GetFileString(appFolders.ToString(), fileName);
    }

    /// <summary>
    /// Gets the file path in a folder that is application type agnostic (uses parent app folder).
    /// </summary>
    /// <param name="appFolders">The application folder category.</param>
    /// <param name="fileName">The file name.</param>
    /// <returns>The file path.</returns>
    public string GetFileAppTypeAgnostic(AppFolders appFolders, string fileName)
    {
        return GetFileString(appFolders.ToString(), fileName, true);
    }

    /// <summary>
    /// Gets the path for the specified application folder.
    /// </summary>
    /// <param name="appFolders">The application folder category.</param>
    /// <returns>The folder path ending with a backslash.</returns>
    public override string GetFolder(AppFolders appFolders)
    {
        var rootPath = RootFolder;
        var result = Path.Combine(rootPath, appFolders.ToString());
        result = result.TrimEnd('\\') + "\\";
        return result;
    }

    /// <summary>
    /// Checks whether the root folder exists in the file system.
    /// </summary>
    /// <returns>True if the root folder exists; otherwise false.</returns>
    public override bool IsRootFolderOk()
    {
        if (string.IsNullOrEmpty(rootFolder)) return false;
        return Directory.Exists(rootFolder);
    }

    /// <summary>
    /// Checks whether the root folder is null or empty.
    /// The root folder is set in GetRootFolder which is called only from CreateAppFoldersIfDontExists.
    /// </summary>
    /// <returns>True if the root folder is null or empty; otherwise false.</returns>
    public override bool IsRootFolderNull()
    {
        var defaultValue = default(string);
        if (!EqualityComparer<string>.Default.Equals(rootFolder, defaultValue))
            return rootFolder == string.Empty;
        return true;
    }

    /// <summary>
    /// Appends the specified content to the given file path.
    /// </summary>
    /// <param name="content">The text content to append.</param>
    /// <param name="filePath">The path of the file to append to.</param>
    public override
#if ASYNC
        async Task
#else
        void
#endif
        AppendAllText(string content, string filePath)
    {
#if ASYNC
        await
#endif
            File.AppendAllTextAsync(filePath, content);
    }

    /// <summary>
    /// Gets the root folder for a different application based on an existing root folder.
    /// </summary>
    /// <param name="rootFolderFromCreatedAppData">The existing application's root folder.</param>
    /// <param name="appName">The name of the target application.</param>
    /// <returns>The root folder path for the target application.</returns>
    public string GetRootFolderForApp(string rootFolderFromCreatedAppData, string appName)
    {
        return Path.Combine(Path.GetDirectoryName(rootFolderFromCreatedAppData)!, appName);
    }

    /// <summary>
    /// Gets or creates the root folder for the application with the specified name.
    /// Also creates the parent application folder.
    /// </summary>
    /// <param name="appName">The application name.</param>
    /// <returns>The root folder path.</returns>
    public override string GetRootFolder(string appName)
    {
        rootFolder = GetSunamoFolder();
        RootFolder = Path.Combine(rootFolder, appName);
        RootFolderPa = Path.Combine(Path.GetDirectoryName(rootFolder)!,
            SHParts.RemoveAfterFirst(appName, "."));
        Directory.CreateDirectory(RootFolder);
        Directory.CreateDirectory(RootFolderPa);
        return RootFolder;
    }

    /// <summary>
    /// Saves the specified content to the given file path.
    /// </summary>
    /// <param name="content">The text content to save.</param>
    /// <param name="filePath">The path of the file to save to.</param>
    protected override
#if ASYNC
        async Task
#else
        void
#endif
        SaveFile(string content, string filePath)
    {
#if ASYNC
        await
#endif
            File.WriteAllTextAsync(filePath, content);
    }

    /// <summary>
    /// Appends text content to a file in the specified application folder.
    /// </summary>
    /// <param name="appFolders">The application folder category.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="value">The text to append.</param>
    public override
#if ASYNC
        async Task
#else
        void
#endif
        AppendAllText(AppFolders appFolders, string fileName, string value)
    {
        ThrowEx.NotImplementedMethod();
    }

    /// <summary>
    /// Gets the file path for common settings.
    /// Without extension because all values are encrypted and stored in bytes.
    /// </summary>
    /// <param name="fileName">The settings file name.</param>
    /// <returns>The file path for the common settings file.</returns>
    public override string GetFileCommonSettings(string fileName)
    {
        var commonFolder = RootFolderCommon(true);
        var result = Path.Combine(commonFolder, AppFolders.Settings.ToString(), fileName);
        return result;
    }

    /// <summary>
    /// Sets an encrypted common settings value for the specified key.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <param name="value">The value to encrypt and store.</param>
    public override void SetCommonSettings(string key, string value)
    {
        var filePath = GetFileCommonSettings(key);
        File.WriteAllBytes(filePath, RijndaelBytesEncrypt!(Encoding.UTF8.GetBytes(value).ToList()).ToArray());
    }
}
