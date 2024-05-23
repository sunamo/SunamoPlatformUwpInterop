namespace SunamoPlatformUwpInterop;

//namespace SunamoPlatformUwpInterop;

public class FSND
{
    public static void CreateUpfoldersPsysicallyUnlessThere(string nad)
    {
        CreateFoldersPsysicallyUnlessThere(Path.GetDirectoryName(nad));
    }

    public static void CreateFoldersPsysicallyUnlessThere(string nad)
    {
        ThrowEx.IsNullOrEmpty("nad", nad);
        ThrowEx.IsNotWindowsPathFormat("nad", nad);


        if (Directory.Exists(nad))
        {
            return;
        }

        List<string> slozkyKVytvoreni = new List<string>
{
nad
};

        while (true)
        {
            nad = Path.GetDirectoryName(nad);

            // TODO: Tady to nefunguje pro UWP/UAP apps protoze nemaji pristup k celemu disku. Zjistit co to je UWP/UAP/... a jak v nem ziskat/overit jakoukoliv slozku na disku
            if (Directory.Exists(nad))
            {
                break;
            }

            string kopia = nad;
            slozkyKVytvoreni.Add(kopia);
        }

        slozkyKVytvoreni.Reverse();
        foreach (string item in slozkyKVytvoreni)
        {
            string folder = item;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
    }


    //    public static Func<string, bool> IsWindowsPathFormat;
    //    public static Func<string, string> GetDirectoryName;
    //    public static Action<string> CreateDirectory;
    //    public static Func<string, bool> ExistsDirectory;
    //    public static Action<string> CreateUpfoldersPsysicallyUnlessThere;
    //    public static Action<string> CreateFoldersPsysicallyUnlessThere;

    //    public static string WithEndSlash(string v)
    //    {
    //        return WithEndSlash(ref v);
    //    }

    //    /// <summary>
    //    ///     Usage: Exceptions.FileWasntFoundInDirectory
    //    /// </summary>
    //    /// <param name="v"></param>
    //    /// <returns></returns>
    //    public static string WithEndSlash(ref string v)
    //    {
    //        if (v != string.Empty)
    //        {
    //            v = v.TrimEnd(AllChars.bs) + AllChars.bs;
    //        }

    //        FirstCharUpper(ref v);
    //        return v;
    //    }

    //    public static void FirstCharUpper(ref string nazevPP)
    //    {
    //        nazevPP = FirstCharUpper(nazevPP);
    //    }

    //    public static string FirstCharUpper(string nazevPP)
    //    {
    //        if (nazevPP.Length == 1)
    //        {
    //            return nazevPP.ToUpper();
    //        }

    //        string sb = nazevPP.Substring(1);
    //        return nazevPP[0].ToString().ToUpper() + sb;
    //    }
}
