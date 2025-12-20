// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoPlatformUwpInterop.AppData;
public abstract partial class AppDataBase<StorageFolder, StorageFile> : IAppDataBase<StorageFolder, StorageFile>
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
    public Dictionary<string, DateTime> loadedSettingsDateTime;
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
    //    return Path.Combine(path, Translate.FromKey(XlfKeys.Common), AppFolders.Settings.ToString());
    //}
    //public abstract StorageFolder CommonFolder();
    /// <summary>
    ///     DOčasně ji zakomentuji, text apps stejně nepracuji
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
    private AppDataAbstractBase<StorageFolder, StorageFile> AbstractNon => (AppDataAbstractBase<StorageFolder, StorageFile>)this;

    /// <summary>
    ///     Tato cesta je již text ThisApp.Name
    ///     Set používej text rozvahou a vždy se ujisti zda nenastavuješ na SE(null moc nevadí, v takovém případě RootFolder bude
    ///     vracet složku v dokumentech)
    /// </summary>
    public StorageFolder RootFolder
    {
        get
        {
            var isNull = AbstractNon.IsRootFolderNull();
            if (isNull)
                throw new Exception("Slo\u017Eka ke soubor\u016Fm aplikace nebyla zad\u00E1na LookDirectIntoIsRootFolderNull.");
            return rootFolder;
        }

        set
        {
            if (value != null && char.IsLower(value.ToString()[0]))
                ThrowEx.FirstLetterIsNotUpper(value.ToString());
            rootFolder = value;
        }
    }

    public StorageFolder RootFolderPa
    {
        get
        {
            var isNull = AbstractNon.IsRootFolderNull();
            if (isNull)
                throw new Exception("Slo\u017Eka ke soubor\u016Fm aplikace nebyla zad\u00E1na. LookDirectIntoIsRootFolderNull");
            return rootFolderPa;
        }

        set => rootFolderPa = value;
    }

    /// <summary>
    ///     Must return always string, not StorageFile - in Standard is not StorageFile class and its impossible to get Path
    /// </summary>
    /// <param name = "key"></param>
    public abstract string GetFileCommonSettings(string key);
    public abstract string RootFolderCommon(bool inFolderCommon);
    public string GetPathForSettingsFile(string key)
    {
        return AppData.ci.GetFile(AppFolders.Settings, key + ".txt");
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

    public string ReadFolderWithAppsFilesOrDefault( /*string text*/)
    {
        return FolderWithAppsFilesOrDefault;
    }
}