//namespace SunamoPlatformUwpInterop._sunamo;

//public class FSND
//{
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
//            v = v.TrimEnd(AllCharsSE.bs) + AllCharsSE.bs;
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
//}
