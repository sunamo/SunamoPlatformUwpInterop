namespace SunamoPlatformUwpInterop.AppData;

/// <summary>
/// Represents the app data type.
/// </summary>
public partial class AppData : AppDataAbstractBase<string, string>
{
    /// <summary>
    /// The instance.
    /// </summary>
    public static AppData Instance = new();

    private AppData()
    {
    }

    /// <summary>
    /// Get sunamo folder.
    /// </summary>
    /// <returns>The get sunamo folder result.</returns>
    public override string GetSunamoFolder()
    {
        var configFilePath = Instance.GetFolderWithAppsFiles();
        var sunamoFolderPath = File.ReadAllText(configFilePath);

        if (char.IsLower(sunamoFolderPath[0])) ThrowEx.FirstLetterIsNotUpper(sunamoFolderPath);

        if (string.IsNullOrWhiteSpace(sunamoFolderPath))
            sunamoFolderPath = Path.Combine(SpecialFoldersHelper.AppDataRoaming(), "_Sunamo");
        return sunamoFolderPath;
    }

    /// <summary>
    /// Get file in subfolder.
    /// </summary>
    /// <param name="appFolders">The app folders.</param>
    /// <param name="subfolder">The subfolder.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="extension">The extension.</param>
    /// <returns>The get file in subfolder result.</returns>
    public override string GetFileInSubfolder(AppFolders appFolders, string subfolder, string fileName, string extension)
    {
        return Instance.GetFile(AppFolders.Output, subfolder + @"\" + fileName + extension);
    }

    /// <summary>
    /// Root folder common.
    /// </summary>
    /// <param name="AppFolders.Output">The app folders output.</param>
    /// <param name="extension">The extension.</param>
    /// <returns>The root folder common result.</returns>
    public override string RootFolderCommon(bool isInFolderCommon)
    {
        var sunamoPath = Path.Combine(SpecialFoldersHelper.AppDataRoaming(), "_Sunamo");
        var configuredPath = GetSunamoFolder();
        if (!string.IsNullOrEmpty(configuredPath)) sunamoPath = configuredPath;
        if (isInFolderCommon) return Path.Combine(sunamoPath, "Common");
        return sunamoPath;
    }

    /// <summary>
    /// Get file string.
    /// </summary>
    /// <param name="appFolderName">The app folder name.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="isUsingParentAppFolder">The is using parent app folder.</param>
    /// <returns>The get file string result.</returns>
    public override string GetFileString(string appFolderName, string fileName, bool isUsingParentAppFolder = false)
    {
        var rootPath = RootFolder;
        if (isUsingParentAppFolder) rootPath = RootFolderPa;
        var folder = Path.Combine(rootPath, appFolderName);
        var filePath = Path.Combine(folder, fileName);
        return filePath;
    }

    /// <summary>
    /// Get file string.
    /// </summary>
    /// <param name="appFolderName">The app folder name.</param>
    /// <param name="fileName">The file name.</param>
    /// <returns>The get file string result.</returns>
    public string GetFileString(string appFolderName, string fileName)
    {
        return GetFileString(appFolderName, fileName, false);
    }

    /// <summary>
    /// Get file.
    /// </summary>
    /// <param name="appFolderName">The app folder name.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="false">The false.</param>
    /// <returns>The get file result.</returns>
    public override string GetFile(AppFolders appFolders, string fileName)
    {
        return GetFileString(appFolders.ToString(), fileName);
    }

    /// <summary>
    /// Get file app type agnostic.
    /// </summary>
    /// <param name="appFolders.ToString()">The app folders to string.</param>
    /// <param name="fileName">The file name.</param>
    /// <returns>The get file app type agnostic result.</returns>
    public string GetFileAppTypeAgnostic(AppFolders appFolders, string fileName)
    {
        return GetFileString(appFolders.ToString(), fileName, true);
    }

    /// <summary>
    /// Get folder.
    /// </summary>
    /// <param name="appFolders.ToString()">The app folders to string.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="true">The true.</param>
    /// <returns>The get folder result.</returns>
    public override string GetFolder(AppFolders appFolders)
    {
        var rootPath = AppFoldersHelper.IsNotBackuped(appFolders) && !string.IsNullOrEmpty(LocalRootFolder)
            ? LocalRootFolder
            : RootFolder;
        var result = Path.Combine(rootPath, appFolders.ToString());
        result = result.TrimEnd('\\') + "\\";
        return result;
    }

    /// <summary>
    /// Is root folder ok.
    /// </summary>
    /// <returns>The is root folder ok result.</returns>
    public override bool IsRootFolderOk()
    {
        if (string.IsNullOrEmpty(rootFolder)) return false;
        return Directory.Exists(rootFolder);
    }

    /// <summary>
    /// Is root folder null.
    /// </summary>
    /// <param name="rootFolder">The root folder.</param>
    /// <returns>The is root folder null result.</returns>
    public override bool IsRootFolderNull()
    {
        var defaultValue = default(string);
        if (!EqualityComparer<string>.Default.Equals(rootFolder, defaultValue))
            return rootFolder == string.Empty;
        return true;
    }

    /// <summary>
    /// Append all text.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="filePath">The file path.</param>
    public override
        async Task
        AppendAllText(string content, string filePath)
    {
        await
            File.AppendAllTextAsync(filePath, content);
    }

    /// <summary>
    /// Initializes a new instance of the get root folder for app class.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="content">The content.</param>
    public string GetRootFolderForApp(string rootFolderFromCreatedAppData, string appName)
    {
        return Path.Combine(Path.GetDirectoryName(rootFolderFromCreatedAppData)!, appName);
    }

    /// <summary>
    /// Get root folder.
    /// </summary>
    /// <param name="Path.GetDirectoryName(rootFolderFromCreatedAppData)!">The path get directory name root folder from created app data.</param>
    /// <param name="appName">The app name.</param>
    /// <returns>The get root folder result.</returns>
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
    /// Save file.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="filePath">The file path.</param>
    protected override
        async Task
        SaveFile(string content, string filePath)
    {
        await
            File.WriteAllTextAsync(filePath, content);
    }

    /// <summary>
    /// Append all text.
    /// </summary>
    /// <param name="appFolders">The app folders.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="value">The value.</param>
    public override
        async Task
        AppendAllText(AppFolders appFolders, string fileName, string value)
    {
        ThrowEx.NotImplementedMethod();
    }

    /// <summary>
    /// Initializes a new instance of the get file common settings class.
    /// </summary>
    public override string GetFileCommonSettings(string fileName)
    {
        var commonFolder = RootFolderCommon(true);
        var result = Path.Combine(commonFolder, AppFolders.Settings.ToString(), fileName);
        return result;
    }

    /// <summary>
    /// Set common settings.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>The set common settings result.</returns>
    public override void SetCommonSettings(string key, string value)
    {
        var filePath = GetFileCommonSettings(key);
        File.WriteAllBytes(filePath, RijndaelBytesEncrypt!(Encoding.UTF8.GetBytes(value).ToList()).ToArray());
    }
}
