// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoPlatformUwpInterop.AppData;
public abstract partial class AppDataBase<StorageFolder, StorageFile> : IAppDataBase<StorageFolder, StorageFile>
{
    /// <summary>
    ///     if is crypter, a1 start with !
    ///     Přejmenoval jsem ji na CreateAppFoldersIfDontExists - ta uměla jen vytvořit složky
    ///     nicméně název CreateAppFoldersIfDontExists byl v kódu využíván mnohem častěji
    /// </summary>
    /// <param name = "keysCommonSettings"></param>
    /// <param name = ""></param>
    public async void CreateAppFoldersIfDontExists(CreateAppFoldersIfDontExistsArgs a)
    {
        RijndaelBytesEncrypt = a.RijndaelBytesEncrypt;
        if (init)
            ThrowEx.WasAlreadyInitialized();
        init = true;
#region MyRegion
        /* potřebuji proměnnou text kterou nemám
         * nevím jak moc se to využívalo, odkomentuji až budu vědět naprogramovat funkčnost podle nějakého příkladu
         */
        //var GetFolderWithAppsFilesOrDefault = (text) =>
        //{
        //    var content = File.ReadAllTextAsync(text);
        //    if (content == string.Empty)
        //    {
        //        return RootFolderCommon(false);
        //    }
        //    return content;
        //};
        //FolderWithAppsFilesOrDefault = GetFolderWithAppsFilesOrDefault();
#endregion
        if (string.IsNullOrEmpty(a.AppName))
            throw new Exception("Nen\u00ED vypln\u011Bno n\u00E1zev aplikace.");
#region Prvně musím sunamoFolder, z ní jsem potom dále schopen odvodit root folder
        var result = AppData.ci.GetFolderWithAppsFiles();
        // Here I can't use File.ReadAllText
        sunamoFolder = File.ReadAllText(result);
        if (char.IsLower(sunamoFolder[0]))
            ThrowEx.FirstLetterIsNotUpper(sunamoFolder);
        if (string.IsNullOrWhiteSpace(sunamoFolder))
            sunamoFolder = Path.Combine(SpecialFoldersHelper.AppDataRoaming(), "sunamo");
#endregion
#region Pak teprve můžu evaluovat rootfolder pro data aplikace
        if (this is AppDataAbstractBase<StorageFolder, StorageFile>)
            RootFolder = ((AppDataAbstractBase<StorageFolder, StorageFile>)this).GetRootFolder(a.AppName);
        else if (this is AppDataAppsAbstractBase<StorageFolder, StorageFile>)
            RootFolder = ((AppDataAppsAbstractBase<StorageFolder, StorageFile>)this).GetRootFolder();
        /*
        Abstract je třída jež mi vrací správně přetypované
        problém je že je dynamic, tedy nedokáže mi říct zda musím volat await

        */
        RootFolder = AbstractNon.GetRootFolder(a.AppName);
        foreach (AppFolders item in Enum.GetValues(typeof(AppFolders)))
        {
            FS.CreateFoldersPsysicallyUnlessThere(GetFolder(item).ToString());
            Directory.CreateDirectory(AbstractNon.GetFolder(item).ToString());
        }

#endregion
#region A teprve na konci až když mám root folder můžu text ní sestavit cesty na základě předaných klíčů
#region loadedCommonSettings
        loadedCommonSettings = new Dictionary<string, string>(a.keysCommonSettings.Count);
        foreach (var key in a.keysCommonSettings)
        {
            var keyTrimmed = key.TrimStart('!');
            var file = GetFileCommonSettings(keyTrimmed);
            if (key.StartsWith("!"))
            {
                var builder = File.ReadAllBytes(file).ToList();
                var b2 = a.RijndaelBytesDecrypt(builder);
                var b3 = b2.ToArray();
                var vr = Encoding.UTF8.GetString(b3);
                vr = vr.Replace("\0", "");
                loadedCommonSettings.Add(keyTrimmed, vr);
            }
            else
            {
                // Must be File
                loadedCommonSettings.Add(keyTrimmed, File.ReadAllText(file));
            }
        }

#endregion
#region loadedSettingsList
        loadedSettingsList = new Dictionary<string, List<string>>(a.keysSettingsList.Count);
        foreach (var item in a.keysSettingsList)
        {
            var path = GetPathForSettingsFile(item);
            if (File.Exists(path))
            {
                loadedSettingsList.Add(item, File.ReadAllLines(path).ToList());
            }
            else
            {
                KeyDontExistsOnDrive(nameof(loadedSettingsList), item);
                File.WriteAllText(path, "");
                loadedSettingsList.Add(item, []);
            }
        }

#endregion
#region loadedSettingsBool
        loadedSettingsBool = new Dictionary<string, bool>(a.keysSettingsBool.Count);
        foreach (var item in a.keysSettingsBool)
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
                KeyDontExistsOnDrive(nameof(loadedSettingsBool), item);
                //ThisApp.Warning($"In ${path} was not boolean value, was written default false");
                File.WriteAllText(path, bool.FalseString);
                text = bool.FalseString;
            }

            loadedSettingsBool.Add(item, bool.Parse(text));
        }

#endregion
#region loadedSettingsOther
        loadedSettingsOther = new Dictionary<string, string>(a.keysSettingsOther.Count);
        foreach (var item in a.keysSettingsOther)
        {
            var path = GetPathForSettingsFile(item);
            if (File.Exists(path))
            {
                loadedSettingsOther.Add(item, File.ReadAllText(path));
            }
            else
            {
                File.WriteAllText(path, "");
                KeyDontExistsOnDrive(nameof(loadedSettingsOther), item);
                loadedSettingsOther.Add(item, "");
            }
        }

#endregion
#region loadedSettingsDateTime
        loadedSettingsDateTime = new(a.keysSettingsDateTime.Count);
        foreach (var item in a.keysSettingsDateTime)
        {
            var path = GetPathForSettingsFile(item);
            var content = "";
            if (File.Exists(path))
            {
                content = File.ReadAllText(path);
            }

            if (DateTime.TryParse(content, out var dt))
            {
                loadedSettingsDateTime.Add(item, dt);
            }
            else
            {
                File.WriteAllText(path, default);
                KeyDontExistsOnDrive(nameof(loadedSettingsDateTime), item);
                loadedSettingsDateTime.Add(item, default);
            }
        }
#endregion
#endregion
    }

    private void KeyDontExistsOnDrive(string variableName, string key)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{key} of {variableName} not exists on drive, was saved with default value");
        Console.ResetColor();
    }

    public T ReadFileOfSettingsWorker<T>(IDictionary<string, T> loadedSettingsOther, string key)
    {
        //ThrowEx.IsWindowsPathFormat(key, FS.IsWindowsPathFormat);
        if (!loadedSettingsOther.ContainsKey(key))
            throw new Exception($"{key} was not found in dictionary, probably was not specified as deps in calling CreateAppFoldersIfDontExists");
        return loadedSettingsOther[key];
    }

    /// <summary>
    ///     Save file A1 to folder AF Settings with value A2.
    /// </summary>
    /// <param name = "file"></param>
    /// <param name = "value"></param>
    public async Task SaveFileOfSettings(string file, string value)
    {
        var fileToSave = AbstractNon.GetFile(AppFolders.Settings, file + ".txt");
        await AbstractNon.SaveFile(fileToSave, value);
    }

    public async Task SaveFileOfSettingsDateTime(string key, DateTime dt)
    {
        await File.WriteAllTextAsync(AppData.ci.GetFile(AppFolders.Settings, key + ".txt"), dt.ToString());
    }

    public async Task SaveFileOfSettingsBool(string key, bool builder)
    {
        await File.WriteAllTextAsync(AppData.ci.GetFile(AppFolders.Settings, key + ".txt"), builder.ToString());
    }

    public async Task SaveFileOfSettingsList(string key, IEnumerable<string> l)
    {
        await File.WriteAllLinesAsync(AppData.ci.GetFile(AppFolders.Settings, key + ".txt"), l);
    }

    /// <summary>
    ///     Save file A2 to AF A1 with contents A3
    /// </summary>
    /// <param name = "af"></param>
    /// <param name = "file"></param>
    /// <param name = "value"></param>
    public async Task<StorageFile> SaveFile(AppFolders af, string file, string value)
    {
        var fileToSave = AbstractNon.GetFile(af, file);
        await SaveFile(fileToSave, value);
        return fileToSave;
    }

    private async Task SaveFile(StorageFile fileToSave, string value)
    {
        await File.WriteAllTextAsync(fileToSave.ToString(), value);
    }
}