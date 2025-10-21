// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoPlatformUwpInterop.AppData;

public partial class AppData : AppDataAbstractBase<string, string>
{
    public static AppData ci = new();
    private static Type type = typeof(AppData);

    private AppData()
    {
    }



    //private string RemoveProjectDisctinction(string basePath)
    //{
    //    if (string.IsNullOrEmpty(basePath))
    //        return basePath;
    //    var parameter = .Split(basePath, '\\');
    //    var list = parameter[parameter.Count - 1];
    //    //var lbs = list[list.Length - 1] == '\\';
    //    if (list.Contains("."))
    //    {
    //        list = .RemoveAfterLast('.', list);
    //        parameter[parameter.Count - 1] = list;
    //    }

    //    return FS.CombineDir(parameter.ToArray());
    //}

    ///// <summary>
    ///// Dříve měla string basePath = null ale nevím k čemu se využíval
    ///// </summary>
    //public void CreateAppFoldersIfDontExists(CreateAppFoldersIfDontExistsArgs a)
    //{
    //    //basePath = RemoveProjectDisctinction(basePath);

    //    if (!string.IsNullOrEmpty(a.AppName))
    //    {
    //        RootFolder = GetRootFolder(a.AppName);

    //        foreach (AppFolders item in Enum.GetValues(typeof(AppFolders)))
    //        {
    //            FS.CreateFoldersPsysicallyUnlessThere(GetFolder(item));
    //        }
    //    }
    //    else
    //    {
    //        ThrowEx.Custom("Nen\u00ED vypln\u011Bno n\u00E1zev aplikace.");
    //    }

    //}

    public override string GetSunamoFolder()
    {
        var result = ci.GetFolderWithAppsFiles();
        // Here I can't use File.ReadFile
        var sunamoFolder = File.ReadAllText(result);

        if (char.IsLower(sunamoFolder[0])) ThrowEx.FirstLetterIsNotUpper(sunamoFolder);

        if (string.IsNullOrWhiteSpace(sunamoFolder))
            sunamoFolder = Path.Combine(SpecialFoldersHelper.AppDataRoaming(), "sunamo");
        return sunamoFolder;
    }

    public override string GetFileInSubfolder(AppFolders output, string subfolder, string file, string ext)
    {
        return ci.GetFile(AppFolders.Output, subfolder + @"\" + file + ext);
    }

    /// <summary>
    ///     Return always in User's AppData
    /// </summary>
    /// <param name="inFolderCommon"></param>
    public override string RootFolderCommon(bool inFolderCommon)
    {
        //string appDataFolder = SpecialFO
        var sunamo2 = Path.Combine(SpecialFoldersHelper.AppDataRoaming(), "sunamo");
        var redirect = GetSunamoFolder();
        if (!string.IsNullOrEmpty(redirect)) sunamo2 = redirect;
        if (inFolderCommon) return Path.Combine(sunamo2, "Common");
        return sunamo2;
    }

    public override string GetFileString(string af, string file, bool pa = false)
    {
        string slozka2, soubor;
        // if (Exc.aspnet)
        // {
        //     slozka2 = Path.Combine(basePath, af.ToString());
        //     soubor = Path.Combine(slozka2, file);
        //     return soubor;
        // }
        // else
        // {
        var rf = RootFolder;
        if (pa) rf = RootFolderPa;
        slozka2 = Path.Combine(rf, af);
        soubor = Path.Combine(slozka2, file);
        return soubor;
        //}
    }

    public string GetFileString(string af, string file)
    {
        return GetFileString(af, file, false);
    }

    public override string GetFile(AppFolders af, string file)
    {
        return GetFileString(af.ToString(), file);
    }

    public string GetFileAppTypeAgnostic(AppFolders af, string file)
    {
        return GetFileString(af.ToString(), file, true);
    }

    public override string GetFolder(AppFolders af)
    {
        var f = RootFolder;
        // if (Exc.aspnet)
        // {
        //     f = basePath;
        // }
        var vr = Path.Combine(f, af.ToString());
        vr = vr.TrimEnd('\\') + "\\";
        return vr;
    }

    public override bool IsRootFolderOk()
    {
        if (string.IsNullOrEmpty(rootFolder)) return false;
        return Directory.Exists(rootFolder);
    }

    /// <summary>
    ///     Bylo by fajn si napsat kdy se rootFolder nastavuje volá abych příště mohl rychleji chybu opravit
    ///     Nastvuje se v GetRootFolder jež se volá pouze v CreateAppFoldersIfDontExists
    /// </summary>
    /// <returns></returns>
    public override bool IsRootFolderNull()
    {
        var def = default(string);
        if (!EqualityComparer<string>.Default.Equals(rootFolder, def))
            // is not null
            return rootFolder == string.Empty;
        return true;
    }

    public override
#if ASYNC
        async Task
#else
void
#endif
        AppendAllText(string content, string sf)
    {
#if ASYNC
        await
#endif
            File.AppendAllTextAsync(sf, content);
    }

    public string GetRootFolderForApp(string rootFolderFromCreatedAppData, string app)
    {
        return Path.Combine(Path.GetDirectoryName(rootFolderFromCreatedAppData), app);
    }

    public override string GetRootFolder(string ThisAppName)
    {
        rootFolder = GetSunamoFolder();
        //pa ? SHParts.RemoveAfterFirst(ThisApp.Name, '.') :
        RootFolder = Path.Combine(rootFolder, ThisAppName);
        RootFolderPa = Path.Combine(Path.GetDirectoryName(rootFolder),
            SHParts.RemoveAfterFirst(ThisAppName, "."));
        Directory.CreateDirectory(RootFolder);
        Directory.CreateDirectory(RootFolderPa);
        return RootFolder;
    }

    /// <summary>
    ///     Zůstane to pojmenované SaveFile protože mám tu další metoday Save*
    /// </summary>
    /// <param name="content"></param>
    /// <param name="sf"></param>
    /// <returns></returns>
    protected override
#if ASYNC
        async Task
#else
void
#endif
        SaveFile(string content, string sf)
    {
#if ASYNC
        await
#endif
            File.WriteAllTextAsync(sf, content);
    }

    public override
#if ASYNC
        async Task
#else
void
#endif
        AppendAllText(AppFolders af, string file, string value)
    {
        ThrowEx.NotImplementedMethod();
    }

    /// <summary>
    ///     Dont use - instead of this GetCommonSettings
    ///     Without ext because all is crypted and in bytes
    ///     Folder is possible to obtain A1 = null
    /// </summary>
    /// <param name="filename"></param>
    public override string GetFileCommonSettings(string filename)
    {
        var fc = RootFolderCommon(true);
        var vr = Path.Combine(fc, AppFolders.Settings.ToString(), filename);
        return vr;
    }

    public override void SetCommonSettings(string key, string value)
    {
        var file = GetFileCommonSettings(key);
        File.WriteAllBytes(file, RijndaelBytesEncrypt(Encoding.UTF8.GetBytes(value).ToList()).ToArray());
    }
}