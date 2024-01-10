namespace SunamoPlatformUwpInterop.AppData;



public abstract partial class AppDataBase<StorageFolder, StorageFile> : IAppDataBase<StorageFolder, StorageFile>
{
    public string GetPathForSettingsFile(string key)
    {
        return AppData.ci.GetFile(AppFolders.Settings, key);
    }

    public string sunamoFolder { get; set; }
    public abstract StorageFile GetFileInSubfolder(AppFolders output, string subfolder, string file, string ext);
    public abstract StorageFolder GetFolder(AppFolders af);
    /// <summary>
    /// Must return always string, not StorageFile - in Standard is not StorageFile class and its impossible to get Path
    /// </summary>
    /// <param name="key"></param>
    public abstract string GetFileCommonSettings(string key);
    private string _fileFolderWithAppsFiles = "";
    public const string folderWithAppsFiles = "folderWithAppsFiles.txt";

    /// <summary>
    /// After startup will setted up in AppData/Roaming
    /// Then from fileFolderWithAppsFiles App can load alternative path -
    /// For all apps will be valid either AppData/Roaming or alternative path
    /// </summary>
    protected StorageFolder rootFolder = default(StorageFolder);
    protected StorageFolder rootFolderPa = default(StorageFolder);

    /// <summary>
    /// Must be here and Tash because in UWP is everything async
    ///
    /// </summary>
    public abstract StorageFolder GetSunamoFolder();

    //public  string CommonFolder()
    //{
    //    var path = GetSunamoFolder().Result.ToString();
    //    return Path.Combine(path, sess.i18n(XlfKeys.Common), AppFolders.Settings.ToString());
    //}
    //public abstract StorageFolder CommonFolder();


    /// <summary>
    /// DOčasně ji zakomentuji, s apps stejně nepracuji
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

    AppDataAbstractBase<StorageFolder, StorageFile> AbstractNon =>
    (AppDataAbstractBase<StorageFolder, StorageFile>)this;


    AppDataAppsAbstractBase<StorageFolder, StorageFile> AbstractNonApps()
    {
        return (AppDataAppsAbstractBase<StorageFolder, StorageFile>)this;
    }

    static Type type = typeof(AppDataBase<StorageFolder, StorageFile>);

    /// <summary>
    /// Tato cesta je již s ThisApp.Name
    /// Set používej s rozvahou a vždy se ujisti zda nenastavuješ na SE(null moc nevadí, v takovém případě RootFolder bude vracet složku v dokumentech)
    /// </summary>
    public StorageFolder RootFolder
    {
        get
        {
            bool isNull = AbstractNon.IsRootFolderNull();
            if (isNull)
            {
                ThrowEx.Custom("Slo\u017Eka ke soubor\u016Fm aplikace nebyla zad\u00E1na " + sess.i18n(XlfKeys.LookDirectIntoIsRootFolderNull) + ".");
            }

            return rootFolder;
        }
        set
        {
            if (value != null && char.IsLower(value.ToString()[0]))
            {
                ThrowEx.FirstLetterIsNotUpper(value.ToString());
            }
            rootFolder = value;
        }
    }

    public StorageFolder RootFolderPa
    {
        get
        {
            bool isNull = AbstractNon.IsRootFolderNull();
            if (isNull)
            {
                ThrowEx.Custom("Slo\u017Eka ke soubor\u016Fm aplikace nebyla zad\u00E1na " + sess.i18n(XlfKeys.LookDirectIntoIsRootFolderNull) + ".");
            }

            return rootFolderPa;
        }
        set
        {
            rootFolderPa = value;
        }
    }

    public abstract string RootFolderCommon(bool inFolderCommon);

    public string GetFolderWithAppsFiles()
    {
        //Common(true)
        var ad = SpecialFoldersHelper.AppDataRoaming();


        string slozka = Path.Combine(ad, "sunamo\\Common", AppFolders.Settings.ToString());
        _fileFolderWithAppsFiles = Path.Combine(slozka, folderWithAppsFiles);


        if (FS.CreateUpfoldersPsysicallyUnlessThere != null)
        {
            FS.CreateUpfoldersPsysicallyUnlessThere(_fileFolderWithAppsFiles);
        }




        return _fileFolderWithAppsFiles;
    }

    public string basePath = null;

    static bool init = false;

    string FolderWithAppsFilesOrDefault = null;

    public string ReadFolderWithAppsFilesOrDefault(string s)
    {
        return FolderWithAppsFilesOrDefault;
    }

    public Dictionary<string, string> loadedCommonSettings = null;
    public Dictionary<string, List<string>> loadedSettingsList = null;
    public Dictionary<string, bool> loadedSettingsBool = null;
    public Dictionary<string, string> loadedSettingsOther = null;

    //private string ReadFileOfSettingsOtherWorker(string path)
    //    {
    //        return null;
    //        //           if (!path.Contains(AllStrings.bs) && !path.Contains(AllStrings.slash))
    //        //           {
    //        //               path = AppData.ci.GetFile(AppFolders.Settings, path);
    //        //           }

    //        //           TF.CreateEmptyFileWhenDoesntExists(path);
    //        //           return

    //        //TF.ReadAllText(path);
    //    }

    /// <summary>
    /// if is crypter, a1 start with !
    /// </summary>
    /// <param name="keysCommonSettings"></param>
    /// <param name=""></param>
    public void CreateAppFoldersIfDontExists(CreateAppFoldersIfDontExistsArgs a, string basePath = null)
    {
        if (init)
        {
            ThrowEx.WasAlreadyInitialized();
        }

        init = true;

        #region MyRegion
        /* potřebuji proměnnou s kterou nemám
        * nevím jak moc se to využívalo, odkomentuji až budu vědět naprogramovat funkčnost podle nějakého příkladu
        */
        //var GetFolderWithAppsFilesOrDefault = (s) =>
        //{
        //    var content = TF.ReadAllTextSync(s);
        //    if (content == string.Empty)
        //    {
        //        return RootFolderCommon(false);
        //    }
        //    return content;
        //};

        //FolderWithAppsFilesOrDefault = GetFolderWithAppsFilesOrDefault();
        #endregion



        if (string.IsNullOrEmpty(ThisAppSE.Name))
        {
            ThrowEx.Custom("Nen\u00ED vypln\u011Bno n\u00E1zev aplikace.");
        }

        #region Prvně musím sunamoFolder, z ní jsem potom dále schopen odvodit root folder
        string r = AppData.ci.GetFolderWithAppsFiles();
        // Here I can't use TF.ReadAllText
        sunamoFolder = TFSE.ReadAllTextSync(r);

        if (char.IsLower(sunamoFolder[0]))
        {
            ThrowEx.FirstLetterIsNotUpper(sunamoFolder);
        }

        if (string.IsNullOrWhiteSpace(sunamoFolder))
        {
            sunamoFolder = Path.Combine(SpecialFoldersHelper.AppDataRoaming(), Consts.@sunamo);
        }
        #endregion

        #region Pak teprve můžu evaluovat rootfolder pro data aplikace
        if (this is AppDataAbstractBase<StorageFolder, StorageFile>)
        {
            RootFolder =
            ((AppDataAbstractBase<StorageFolder, StorageFile>)this).GetRootFolder();
        }
        else if (this is AppDataAppsAbstractBase<StorageFolder, StorageFile>)
        {
            RootFolder = ((AppDataAppsAbstractBase<StorageFolder, StorageFile>)this).GetRootFolder();
        }

        /*
        Abstract je třída jež mi vrací správně přetypované
        problém je že je dynamic, tedy nedokáže mi říct zda musím volat await

        */

        RootFolder = AbstractNon.GetRootFolder();

        foreach (AppFolders item in Enum.GetValues(typeof(AppFolders)))
        {
            if (FS.CreateFoldersPsysicallyUnlessThere != null)
            {
                FS.CreateFoldersPsysicallyUnlessThere(GetFolder(item).ToString());
            }

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
                var b = TFSE.ReadAllBytesSync(file);
                var b2 = _.RijndaelBytesDecrypt(b);
                var b3 = b2.ToArray();
                var vr = Encoding.UTF8.GetString(b3);
                vr = vr.Replace("\0", "");

                loadedCommonSettings.Add(keyTrimmed, vr);
            }
            else
            {
                // Must be File
                loadedCommonSettings.Add(keyTrimmed, TFSE.ReadAllTextSync(file));
            }
        }
        #endregion

        #region loadedSettingsList
        loadedSettingsList = new Dictionary<string, List<string>>(a.keysSettingsList.Count);

        foreach (var item in a.keysSettingsList)
        {
            loadedSettingsList.Add(item, TFSE.ReadAllLinesSync(GetPathForSettingsFile(item), true));
        }
        #endregion

        #region loadedSettingsBool
        loadedSettingsBool = new Dictionary<string, bool>(a.keysSettingsBool.Count);

        foreach (var item in a.keysSettingsBool)
        {
            var path = GetPathForSettingsFile(item);
            var text = TFSE.ReadAllTextSync(path);
            var isBool = BTS.IsBool(text);

            if (!isBool)
            {
                ThisApp.Warning($"In ${path} was not boolean value, was written default false");
                TFSE.WriteAllTextSync(path, bool.FalseString);
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
            loadedSettingsOther.Add(item, TFSE.ReadAllTextSync(path, true));
        }
        #endregion
        #endregion
    }
}
