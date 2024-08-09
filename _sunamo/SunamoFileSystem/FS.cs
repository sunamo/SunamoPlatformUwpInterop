namespace SunamoPlatformUwpInterop._sunamo.SunamoFileSystem;

internal class FS
{
    internal static void CreateUpfoldersPsysicallyUnlessThere(string nad)
    {
        CreateFoldersPsysicallyUnlessThere(Path.GetDirectoryName(nad));
    }

    internal static void CreateFoldersPsysicallyUnlessThere(string nad)
    {
        ThrowEx.IsNullOrEmpty("nad", nad);
        ThrowEx.IsNotWindowsPathFormat("nad", nad);


        if (Directory.Exists(nad)) return;

        var slozkyKVytvoreni = new List<string>
        {
            nad
        };

        while (true)
        {
            nad = Path.GetDirectoryName(nad);

            // TODO: Tady to nefunguje pro UWP/UAP apps protoze nemaji pristup k celemu disku. Zjistit co to je UWP/UAP/... a jak v nem ziskat/overit jakoukoliv slozku na disku
            if (Directory.Exists(nad)) break;

            var kopia = nad;
            slozkyKVytvoreni.Add(kopia);
        }

        slozkyKVytvoreni.Reverse();
        foreach (var item in slozkyKVytvoreni)
        {
            var folder = item;
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        }
    }


    //    internal static Func<string, bool> IsWindowsPathFormat;
    //    internal static Func<string, string> GetDirectoryName;
    //    internal static Action<string> CreateDirectory;
    //    internal static Func<string, bool> ExistsDirectory;
    //    internal static Action<string> CreateUpfoldersPsysicallyUnlessThere;
    //    internal static Action<string> CreateFoldersPsysicallyUnlessThere;

    //    internal static string WithEndSlash(string v)
    //    {
    //        return WithEndSlash(ref v);
    //    }

    //    /// <summary>
    //    ///     Usage: Exceptions.FileWasntFoundInDirectory
    //    /// </summary>
    //    /// <param name="v"></param>
    //    /// <returns></returns>
    //    internal static string WithEndSlash(ref string v)
    //    {
    //        if (v != string.Empty)
    //        {
    //            v = v.TrimEnd(AllChars.bs) + AllChars.bs;
    //        }

    //        FirstCharUpper(ref v);
    //        return v;
    //    }

    //    internal static void FirstCharUpper(ref string nazevPP)
    //    {
    //        nazevPP = FirstCharUpper(nazevPP);
    //    }

    //    internal static string FirstCharUpper(string nazevPP)
    //    {
    //        if (nazevPP.Length == 1)
    //        {
    //            return nazevPP.ToUpper();
    //        }

    //        string sb = nazevPP.Substring(1);
    //        return nazevPP[0].ToString().ToUpper() + sb;
    //    }
}