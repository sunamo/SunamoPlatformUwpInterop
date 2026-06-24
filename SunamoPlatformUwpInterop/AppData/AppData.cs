namespace SunamoPlatformUwpInterop.AppData;

public partial class AppData : AppDataAbstractBase<string, string>
{
    public static AppData Instance = new();

    private AppData()
    {
    }

    public override string GetSunamoFolder()
    {
        var configFilePath = Instance.GetFolderWithAppsFiles();
        var sunamoFolderPath = File.ReadAllText(configFilePath);

        if (char.IsLower(sunamoFolderPath[0])) ThrowEx.FirstLetterIsNotUpper(sunamoFolderPath);

        if (string.IsNullOrWhiteSpace(sunamoFolderPath))
            sunamoFolderPath = Path.Combine(SpecialFoldersHelper.AppDataRoaming(), "sunamo");
        return sunamoFolderPath;
    }

    public override string GetFileInSubfolder(AppFolders appFolders, string subfolder, string fileName, string extension)
    {
        return Instance.GetFile(AppFolders.Output, subfolder + @"\" + fileName + extension);
    }

    public override string RootFolderCommon(bool isInFolderCommon)
    {
        var sunamoPath = Path.Combine(SpecialFoldersHelper.AppDataRoaming(), "sunamo");
        var configuredPath = GetSunamoFolder();
        if (!string.IsNullOrEmpty(configuredPath)) sunamoPath = configuredPath;
        if (isInFolderCommon) return Path.Combine(sunamoPath, "Common");
        return sunamoPath;
    }

    public override string GetFileString(string appFolderName, string fileName, bool isUsingParentAppFolder = false)
    {
        var rootPath = RootFolder;
        if (isUsingParentAppFolder) rootPath = RootFolderPa;
        var folder = Path.Combine(rootPath, appFolderName);
        var filePath = Path.Combine(folder, fileName);
        return filePath;
    }

    public string GetFileString(string appFolderName, string fileName)
    {
        return GetFileString(appFolderName, fileName, false);
    }

    public override string GetFile(AppFolders appFolders, string fileName)
    {
        return GetFileString(appFolders.ToString(), fileName);
    }

    public string GetFileAppTypeAgnostic(AppFolders appFolders, string fileName)
    {
        return GetFileString(appFolders.ToString(), fileName, true);
    }

    public override string GetFolder(AppFolders appFolders)
    {
        var rootPath = RootFolder;
        var result = Path.Combine(rootPath, appFolders.ToString());
        result = result.TrimEnd('\\') + "\\";
        return result;
    }

    public override bool IsRootFolderOk()
    {
        if (string.IsNullOrEmpty(rootFolder)) return false;
        return Directory.Exists(rootFolder);
    }

    public override bool IsRootFolderNull()
    {
        var defaultValue = default(string);
        if (!EqualityComparer<string>.Default.Equals(rootFolder, defaultValue))
            return rootFolder == string.Empty;
        return true;
    }

    public override
        async Task
        AppendAllText(string content, string filePath)
    {
        await
            File.AppendAllTextAsync(filePath, content);
    }

    public string GetRootFolderForApp(string rootFolderFromCreatedAppData, string appName)
    {
        return Path.Combine(Path.GetDirectoryName(rootFolderFromCreatedAppData)!, appName);
    }

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

    protected override
        async Task
        SaveFile(string content, string filePath)
    {
        await
            File.WriteAllTextAsync(filePath, content);
    }

    public override
        async Task
        AppendAllText(AppFolders appFolders, string fileName, string value)
    {
        ThrowEx.NotImplementedMethod();
    }

    public override string GetFileCommonSettings(string fileName)
    {
        var commonFolder = RootFolderCommon(true);
        var result = Path.Combine(commonFolder, AppFolders.Settings.ToString(), fileName);
        return result;
    }

    public override void SetCommonSettings(string key, string value)
    {
        var filePath = GetFileCommonSettings(key);
        File.WriteAllBytes(filePath, RijndaelBytesEncrypt!(Encoding.UTF8.GetBytes(value).ToList()).ToArray());
    }
}
