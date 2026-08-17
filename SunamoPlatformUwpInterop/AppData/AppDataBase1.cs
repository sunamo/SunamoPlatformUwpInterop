namespace SunamoPlatformUwpInterop.AppData;

public abstract partial class AppDataBase<StorageFolder, StorageFile> : IAppDataBase<StorageFolder, StorageFile>
{
    public async void CreateAppFoldersIfDontExists(CreateAppFoldersIfDontExistsArgs args)
    {
        RijndaelBytesEncrypt = args.RijndaelBytesEncrypt;
        if (isInitialized)
            ThrowEx.WasAlreadyInitialized();
        isInitialized = true;

        if (string.IsNullOrEmpty(args.AppName))
            throw new Exception("Application name is not specified.");

        var result = AppData.Instance.GetFolderWithAppsFiles();
        SunamoFolder = File.ReadAllText(result);
        if (char.IsLower(SunamoFolder[0]))
            ThrowEx.FirstLetterIsNotUpper(SunamoFolder);
        if (string.IsNullOrWhiteSpace(SunamoFolder))
            SunamoFolder = Path.Combine(SpecialFoldersHelper.AppDataRoaming(), "_Sunamo");

        if (this is AppDataAbstractBase<StorageFolder, StorageFile>)
            RootFolder = ((AppDataAbstractBase<StorageFolder, StorageFile>)this).GetRootFolder(args.AppName);
        else if (this is AppDataAppsAbstractBase<StorageFolder, StorageFile>)
            RootFolder = ((AppDataAppsAbstractBase<StorageFolder, StorageFile>)this).GetRootFolder();

        RootFolder = desktopBase.GetRootFolder(args.AppName);
        LocalRootFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "_Sunamo", args.AppName);
        Directory.CreateDirectory(LocalRootFolder);
        foreach (AppFolders item in Enum.GetValues(typeof(AppFolders)))
        {
            FS.CreateFoldersPsysicallyUnlessThere(GetFolder(item)!.ToString()!);
            Directory.CreateDirectory(desktopBase.GetFolder(item)!.ToString()!);
        }

        LoadedCommonSettings = new Dictionary<string, string>(args.KeysCommonSettings.Count);
        foreach (var key in args.KeysCommonSettings)
        {
            var keyTrimmed = key.TrimStart('!');
            var filePath = GetFileCommonSettings(keyTrimmed);
            if (key.StartsWith("!"))
            {
                var encryptedBytes = File.ReadAllBytes(filePath).ToList();
                var decryptedBytes = args.RijndaelBytesDecrypt!(encryptedBytes);
                var decryptedArray = decryptedBytes.ToArray();
                var decryptedText = Encoding.UTF8.GetString(decryptedArray);
                decryptedText = decryptedText.Replace("\0", "");
                LoadedCommonSettings.Add(keyTrimmed, decryptedText);
            }
            else
            {
                LoadedCommonSettings.Add(keyTrimmed, File.ReadAllText(filePath));
            }
        }

        LoadedSettingsList = new Dictionary<string, List<string>>(args.KeysSettingsList.Count);
        foreach (var item in args.KeysSettingsList)
        {
            var path = GetPathForSettingsFile(item);
            if (File.Exists(path))
            {
                LoadedSettingsList.Add(item, File.ReadAllLines(path).ToList());
            }
            else
            {
                KeyDoesNotExistOnDrive(nameof(LoadedSettingsList), item);
                File.WriteAllText(path, "");
                LoadedSettingsList.Add(item, []);
            }
        }

        LoadedSettingsBool = new Dictionary<string, bool>(args.KeysSettingsBool.Count);
        foreach (var item in args.KeysSettingsBool)
        {
            var path = GetPathForSettingsFile(item);
            string text = "";
            if (File.Exists(path))
            {
                text = File.ReadAllText(path);
            }

            bool.TryParse(text, out var parsedValue);
            if (!parsedValue)
            {
                KeyDoesNotExistOnDrive(nameof(LoadedSettingsBool), item);
                File.WriteAllText(path, bool.FalseString);
                text = bool.FalseString;
            }

            LoadedSettingsBool.Add(item, bool.Parse(text));
        }

        LoadedSettingsOther = new Dictionary<string, string>(args.KeysSettingsOther.Count);
        foreach (var item in args.KeysSettingsOther)
        {
            var path = GetPathForSettingsFile(item);
            if (File.Exists(path))
            {
                LoadedSettingsOther.Add(item, File.ReadAllText(path));
            }
            else
            {
                File.WriteAllText(path, "");
                KeyDoesNotExistOnDrive(nameof(LoadedSettingsOther), item);
                LoadedSettingsOther.Add(item, "");
            }
        }

        LoadedSettingsDateTime = new(args.KeysSettingsDateTime.Count);
        foreach (var item in args.KeysSettingsDateTime)
        {
            var path = GetPathForSettingsFile(item);
            var content = "";
            if (File.Exists(path))
            {
                content = File.ReadAllText(path);
            }

            if (DateTime.TryParse(content, out var dateTime))
            {
                LoadedSettingsDateTime.Add(item, dateTime);
            }
            else
            {
                File.WriteAllText(path, "");
                KeyDoesNotExistOnDrive(nameof(LoadedSettingsDateTime), item);
                LoadedSettingsDateTime.Add(item, default(DateTime));
            }
        }
    }

    private void KeyDoesNotExistOnDrive(string variableName, string key)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{key} of {variableName} not exists on drive, was saved with default value");
        Console.ResetColor();
    }

    public T ReadFileOfSettingsWorker<T>(IDictionary<string, T> settingsDictionary, string key)
    {
        if (!settingsDictionary.ContainsKey(key))
            throw new Exception($"{key} was not found in dictionary, probably was not specified as deps in calling CreateAppFoldersIfDontExists");
        return settingsDictionary[key];
    }

    public async Task SaveFileOfSettings(string fileName, string value)
    {
        var fileToSave = desktopBase.GetFile(AppFolders.Settings, fileName + ".txt");
        await desktopBase.SaveFile(fileToSave, value);
    }

    public async Task SaveFileOfSettingsDateTime(string key, DateTime dateTime)
    {
        await File.WriteAllTextAsync(AppData.Instance.GetFile(AppFolders.Settings, key + ".txt"), dateTime.ToString());
    }

    public async Task SaveFileOfSettingsBool(string key, bool value)
    {
        await File.WriteAllTextAsync(AppData.Instance.GetFile(AppFolders.Settings, key + ".txt"), value.ToString());
    }

    public async Task SaveFileOfSettingsList(string key, IEnumerable<string> enumerable)
    {
        await File.WriteAllLinesAsync(AppData.Instance.GetFile(AppFolders.Settings, key + ".txt"), enumerable);
    }

    public async Task<StorageFile> SaveFile(AppFolders appFolders, string fileName, string value)
    {
        var fileToSave = desktopBase.GetFile(appFolders, fileName);
        await SaveFile(fileToSave, value);
        return fileToSave;
    }

    private async Task SaveFile(StorageFile fileToSave, string value)
    {
        await File.WriteAllTextAsync(fileToSave!.ToString()!, value);
    }
}
