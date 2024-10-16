namespace SunamoPlatformUwpInterop.AppData;

public abstract class AppDataBase<StorageFolder, StorageFile> : IAppDataBase<StorageFolder, StorageFile>
{
    public const string folderWithAppsFiles = "folderWithAppsFiles.txt";

    private static Type type = typeof(AppDataBase<StorageFolder, StorageFile>);

    private static bool init;

    //private string ReadFileOfSettingsOtherWorker(string path)
    //    {
    //        return null;
    //        //           if (!path.Contains("\"") && !path.Contains("/"))
    //        //           {
    //        //               path = AppData.ci.GetFile(AppFolders.Settings, path);
    //        //           }

    //        //           TF.CreateEmptyFileWhenDoesntExists(path);
    //        //           return

    //        //File.ReadAllTextAsync(path);
    //    }

    public static Func<List<byte>, List<byte>> RijndaelBytesEncrypt;
    private string _fileFolderWithAppsFiles = "";

    public string basePath = null;

    private readonly string FolderWithAppsFilesOrDefault = null;

    public Dictionary<string, string> loadedCommonSettings;
    public Dictionary<string, bool> loadedSettingsBool;
    public Dictionary<string, List<string>> loadedSettingsList;
    public Dictionary<string, string> loadedSettingsOther;

    /// <summary>
    ///     After startup will setted up in AppData/Roaming
    ///     Then from fileFolderWithAppsFiles App can load alternative path -
    ///     For all apps will be valid either AppData/Roaming or alternative path
    /// </summary>
    protected StorageFolder rootFolder;

    protected StorageFolder rootFolderPa;

    public string sunamoFolder { get; set; }

    //public  string CommonFolder()
    //{
    //    var path = GetSunamoFolder().Result.ToString();
    //    return Path.Combine(path, sess.i18n(XlfKeys.Common), AppFolders.Settings.ToString());
    //}
    //public abstract StorageFolder CommonFolder();


    /// <summary>
    ///     DOčasně ji zakomentuji, s apps stejně nepracuji
    /// </summary>
    //public dynamic Abstract
    //{
    //    get
    //    {
    //        /*
    //         *if (!string.IsNullOrEmpty(ThisApp.Name))
    //        {
    //            if (this is AppDataAbstractBase<StorageFolder, StorageFile>)
    //        {
    //            RootFolder = ((AppDataAbstractBase<StorageFolder, StorageFile>)this).GetRootFolder();
    //        }
    //        else if (this is AppDataAppsAbstractBase<StorageFolder, StorageFile>)
    //        {
    //            RootFolder = ((AppDataAppsAbstractBase<StorageFolder, StorageFile>)this).GetRootFolder();
    //        }
    //         */

    //        if (this is AppDataAbstractBase<StorageFolder, StorageFile>)
    //        {
    //            return (AppDataAbstractBase<StorageFolder, StorageFile>)this;
    //        }
    //        else if (this is AppDataAppsAbstractBase<StorageFolder, StorageFile>)
    //        {
    //            return (AppDataAppsAbstractBase<StorageFolder, StorageFile>)this;
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //}

    private AppDataAbstractBase<StorageFolder, StorageFile> AbstractNon =>
        (AppDataAbstractBase<StorageFolder, StorageFile>)this;

    /// <summary>
    ///     Tato cesta je již s ThisApp.Name
    ///     Set používej s rozvahou a vždy se ujisti zda nenastavuješ na SE(null moc nevadí, v takovém případě RootFolder bude
    ///     vracet složku v dokumentech)
    /// </summary>
    public StorageFolder RootFolder
    {
        get
        {
            var isNull = AbstractNon.IsRootFolderNull();
            if (isNull)
                throw new Exception(
                    "Slo\u017Eka ke soubor\u016Fm aplikace nebyla zad\u00E1na LookDirectIntoIsRootFolderNull.");

            return rootFolder;
        }
        set
        {
            if (value != null && char.IsLower(value.ToString()[0])) ThrowEx.FirstLetterIsNotUpper(value.ToString());
            rootFolder = value;
        }
    }

    public StorageFolder RootFolderPa
    {
        get
        {
            var isNull = AbstractNon.IsRootFolderNull();
            if (isNull)
                throw new Exception(
                    "Slo\u017Eka ke soubor\u016Fm aplikace nebyla zad\u00E1na. LookDirectIntoIsRootFolderNull");

            return rootFolderPa;
        }
        set => rootFolderPa = value;
    }

    /// <summary>
    ///     Must return always string, not StorageFile - in Standard is not StorageFile class and its impossible to get Path
    /// </summary>
    /// <param name="key"></param>
    public abstract string GetFileCommonSettings(string key);

    public abstract string RootFolderCommon(bool inFolderCommon);

    public string GetPathForSettingsFile(string key)
    {
        return AppData.ci.GetFile(AppFolders.Settings, key);
    }

    public abstract StorageFile GetFileInSubfolder(AppFolders output, string subfolder, string file, string ext);
    public abstract StorageFolder GetFolder(AppFolders af);

    /// <summary>
    ///     Must be here and Tash because in UWP is everything async
    /// </summary>
    public abstract StorageFolder GetSunamoFolder();


    private AppDataAppsAbstractBase<StorageFolder, StorageFile> AbstractNonApps()
    {
        return (AppDataAppsAbstractBase<StorageFolder, StorageFile>)this;
    }

    public string GetFolderWithAppsFiles()
    {
        //Common(true)
        var ad = SpecialFoldersHelper.AppDataRoaming();


        var slozka = Path.Combine(ad, "sunamo\\Common", AppFolders.Settings.ToString());
        _fileFolderWithAppsFiles = Path.Combine(slozka, folderWithAppsFiles);


        FS.CreateUpfoldersPsysicallyUnlessThere(_fileFolderWithAppsFiles);


        return _fileFolderWithAppsFiles;
    }

    public string ReadFolderWithAppsFilesOrDefault(string s)
    {
        return FolderWithAppsFilesOrDefault;
    }

    /// <summary>
    ///     if is crypter, a1 start with !
    ///     Přejmenoval jsem ji na CreateAppFoldersIfDontExists - ta uměla jen vytvořit složky
    ///     nicméně název CreateAppFoldersIfDontExists byl v kódu využíván mnohem častěji
    /// </summary>
    /// <param name="keysCommonSettings"></param>
    /// <param name=""></param>
    public void CreateAppFoldersIfDontExists(CreateAppFoldersIfDontExistsArgs a)
    {
        RijndaelBytesEncrypt = a.RijndaelBytesEncrypt;

        if (init) ThrowEx.WasAlreadyInitialized();

        init = true;

        #region MyRegion

        /* potřebuji proměnnou s kterou nemám
         * nevím jak moc se to využívalo, odkomentuji až budu vědět naprogramovat funkčnost podle nějakého příkladu
         */
        //var GetFolderWithAppsFilesOrDefault = (s) =>
        //{
        //    var content = File.ReadAllTextAsync(s);
        //    if (content == string.Empty)
        //    {
        //        return RootFolderCommon(false);
        //    }
        //    return content;
        //};

        //FolderWithAppsFilesOrDefault = GetFolderWithAppsFilesOrDefault();

        #endregion


        if (string.IsNullOrEmpty(a.AppName)) throw new Exception("Nen\u00ED vypln\u011Bno n\u00E1zev aplikace.");

        #region Prvně musím sunamoFolder, z ní jsem potom dále schopen odvodit root folder

        var r = AppData.ci.GetFolderWithAppsFiles();
        // Here I can't use File.ReadAllText
        sunamoFolder = File.ReadAllText(r);

        if (char.IsLower(sunamoFolder[0])) ThrowEx.FirstLetterIsNotUpper(sunamoFolder);

        if (string.IsNullOrWhiteSpace(sunamoFolder))
            sunamoFolder = Path.Combine(SpecialFoldersHelper.AppDataRoaming(), "sunamo");

        #endregion

        #region Pak teprve můžu evaluovat rootfolder pro data aplikace

        if (this is AppDataAbstractBase<StorageFolder, StorageFile>)
            RootFolder =
                ((AppDataAbstractBase<StorageFolder, StorageFile>)this).GetRootFolder(a.AppName);
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

        #region A teprve na konci až když mám root folder můžu s ní sestavit cesty na základě předaných klíčů

        #region loadedCommonSettings

        loadedCommonSettings = new Dictionary<string, string>(a.keysCommonSettings.Count);

        foreach (var key in a.keysCommonSettings)
        {
            var keyTrimmed = key.TrimStart('!');
            var file = GetFileCommonSettings(keyTrimmed);

            if (key.StartsWith("!"))
            {
                var b = File.ReadAllBytes(file).ToList();
                var b2 = a.RijndaelBytesDecrypt(b);
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
            loadedSettingsList.Add(item, File.ReadAllLines(GetPathForSettingsFile(item)).ToList());

        #endregion

        #region loadedSettingsBool

        loadedSettingsBool = new Dictionary<string, bool>(a.keysSettingsBool.Count);

        foreach (var item in a.keysSettingsBool)
        {
            var path = GetPathForSettingsFile(item);
            var text = File.ReadAllText(path);
            bool.TryParse(text, out var isBool);

            if (!isBool)
            {
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
            loadedSettingsOther.Add(item, File.ReadAllText(path));
        }

        #endregion

        #endregion
    }

    public T ReadFileOfSettingsWorker<T>(IDictionary<string, T> loadedSettingsOther, string key)
    {
        //ThrowEx.IsWindowsPathFormat(key, FS.IsWindowsPathFormat);
        if (!loadedSettingsOther.ContainsKey(key))
            throw new Exception(
                $"{key} was not found in dictionary, probably was not specified as deps in calling CreateAppFoldersIfDontExists");
        return loadedSettingsOther[key];
    }

    /// <summary>
    ///     Save file A1 to folder AF Settings with value A2.
    /// </summary>
    /// <param name="file"></param>
    /// <param name="value"></param>
    public void SaveFileOfSettings(string file, string value)
    {
        var fileToSave = AbstractNon.GetFile(AppFolders.Settings, file);
        AbstractNon.SaveFile(value, fileToSave);
    }

    /// <summary>
    ///     Save file A2 to AF A1 with contents A3
    /// </summary>
    /// <param name="af"></param>
    /// <param name="file"></param>
    /// <param name="value"></param>
    public StorageFile SaveFile(AppFolders af, string file, string value)
    {
        var fileToSave = AbstractNon.GetFile(af, file);
        SaveFile(value, fileToSave);
        return fileToSave;
    }

    private void SaveFile(string value, StorageFile fileToSave)
    {
        ThrowEx.NotImplementedMethod();
    }

    /// <summary>
    ///     Append to file A2 in AF A1 with contents A3
    /// </summary>
    /// <param name="af"></param>
    /// <param name="file"></param>
    /// <param name="value"></param>
    public
#if ASYNC
        async Task
#else
void
#endif
        AppendAllText(AppFolders af, string file, string value)
    {
        var fileToSave = AbstractNon.GetFile(af, file);
#if ASYNC
        await
#endif
            AppendAllText(fileToSave.ToString(), value);
    }

    /// <summary>
    ///     Just call TF.AppendAllText
    /// </summary>
    /// <param name="file"></param>
    /// <param name="value"></param>
    public
#if ASYNC
        async Task
#else
void
#endif
        AppendAllText(string file, string value)
    {
#if ASYNC
        await
#endif
            File.AppendAllTextAsync(file, value);
    }

    /// <summary>
    ///     If file A1 dont exists, then create him with empty content and G . When optained file/folder doesnt exists, return
    ///     SE
    /// </summary>
    /// <param name="key"></param>
    public string ReadFileOfSettingsExistingDirectoryOrFile(string key)
    {
        return ReadFileOfSettingsWorker(loadedSettingsOther, key);
    }

    /// <summary>
    ///     If file A1 dont exists or have empty content, then create him with empty content and G false
    /// </summary>
    /// <param name="path"></param>
    public bool ReadFileOfSettingsBool(string key)
    {
        return ReadFileOfSettingsWorker(loadedSettingsBool, key);
    }
}