namespace SunamoPlatformUwpInterop.AppData;

public abstract partial class AppDataBase<StorageFolder, StorageFile> : IAppDataBase<StorageFolder, StorageFile>
{
    /// <summary>
    /// Creates application folders if they do not exist and loads all specified settings.
    /// If a key is prefixed with '!', the value will be decrypted using the provided Rijndael function.
    /// </summary>
    /// <param name="args">The arguments specifying the application name, settings keys and encryption functions.</param>
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
            SunamoFolder = Path.Combine(SpecialFoldersHelper.AppDataRoaming(), "sunamo");

        if (this is AppDataAbstractBase<StorageFolder, StorageFile>)
            RootFolder = ((AppDataAbstractBase<StorageFolder, StorageFile>)this).GetRootFolder(args.AppName);
        else if (this is AppDataAppsAbstractBase<StorageFolder, StorageFile>)
            RootFolder = ((AppDataAppsAbstractBase<StorageFolder, StorageFile>)this).GetRootFolder();

        RootFolder = abstractNon.GetRootFolder(args.AppName);
        foreach (AppFolders item in Enum.GetValues(typeof(AppFolders)))
        {
            FS.CreateFoldersPsysicallyUnlessThere(GetFolder(item)!.ToString()!);
            Directory.CreateDirectory(abstractNon.GetFolder(item)!.ToString()!);
        }

        LoadedCommonSettings = new Dictionary<string, string>(args.KeysCommonSettings.Count);
        foreach (var key in args.KeysCommonSettings)
        {
            var keyTrimmed = key.TrimStart('!');
            var file = GetFileCommonSettings(keyTrimmed);
            if (key.StartsWith("!"))
            {
                var encryptedBytes = File.ReadAllBytes(file).ToList();
                var decryptedBytes = args.RijndaelBytesDecrypt!(encryptedBytes);
                var decryptedArray = decryptedBytes.ToArray();
                var decryptedText = Encoding.UTF8.GetString(decryptedArray);
                decryptedText = decryptedText.Replace("\0", "");
                LoadedCommonSettings.Add(keyTrimmed, decryptedText);
            }
            else
            {
                LoadedCommonSettings.Add(keyTrimmed, File.ReadAllText(file));
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
                KeyDontExistsOnDrive(nameof(LoadedSettingsList), item);
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

            bool.TryParse(text, out var isBool);
            if (!isBool)
            {
                KeyDontExistsOnDrive(nameof(LoadedSettingsBool), item);
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
                KeyDontExistsOnDrive(nameof(LoadedSettingsOther), item);
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
                KeyDontExistsOnDrive(nameof(LoadedSettingsDateTime), item);
                LoadedSettingsDateTime.Add(item, default(DateTime));
            }
        }
    }

    private void KeyDontExistsOnDrive(string variableName, string key)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{key} of {variableName} not exists on drive, was saved with default value");
        Console.ResetColor();
    }

    /// <summary>
    /// Reads a value from the specified settings dictionary.
    /// </summary>
    /// <typeparam name="T">The type of the settings value.</typeparam>
    /// <param name="settingsDictionary">The dictionary to read from.</param>
    /// <param name="key">The settings key.</param>
    /// <returns>The settings value.</returns>
    public T ReadFileOfSettingsWorker<T>(IDictionary<string, T> settingsDictionary, string key)
    {
        if (!settingsDictionary.ContainsKey(key))
            throw new Exception($"{key} was not found in dictionary, probably was not specified as deps in calling CreateAppFoldersIfDontExists");
        return settingsDictionary[key];
    }

    /// <summary>
    /// Saves a string value to a settings file.
    /// </summary>
    /// <param name="fileName">The settings file name (without extension).</param>
    /// <param name="value">The value to save.</param>
    public async Task SaveFileOfSettings(string fileName, string value)
    {
        var fileToSave = abstractNon.GetFile(AppFolders.Settings, fileName + ".txt");
        await abstractNon.SaveFile(fileToSave, value);
    }

    /// <summary>
    /// Saves a DateTime value to a settings file.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <param name="dateTime">The DateTime value to save.</param>
    public async Task SaveFileOfSettingsDateTime(string key, DateTime dateTime)
    {
        await File.WriteAllTextAsync(AppData.Instance.GetFile(AppFolders.Settings, key + ".txt"), dateTime.ToString());
    }

    /// <summary>
    /// Saves a boolean value to a settings file.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <param name="value">The boolean value to save.</param>
    public async Task SaveFileOfSettingsBool(string key, bool value)
    {
        await File.WriteAllTextAsync(AppData.Instance.GetFile(AppFolders.Settings, key + ".txt"), value.ToString());
    }

    /// <summary>
    /// Saves a list of strings to a settings file.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <param name="enumerable">The enumerable of strings to save.</param>
    public async Task SaveFileOfSettingsList(string key, IEnumerable<string> enumerable)
    {
        await File.WriteAllLinesAsync(AppData.Instance.GetFile(AppFolders.Settings, key + ".txt"), enumerable);
    }

    /// <summary>
    /// Saves a file with the specified contents to the given application folder.
    /// </summary>
    /// <param name="appFolders">The application folder category.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="value">The content to write.</param>
    /// <returns>The storage file that was saved to.</returns>
    public async Task<StorageFile> SaveFile(AppFolders appFolders, string fileName, string value)
    {
        var fileToSave = abstractNon.GetFile(appFolders, fileName);
        await SaveFile(fileToSave, value);
        return fileToSave;
    }

    private async Task SaveFile(StorageFile fileToSave, string value)
    {
        await File.WriteAllTextAsync(fileToSave!.ToString()!, value);
    }
}
