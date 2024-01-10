namespace SunamoPlatformUwpInterop._sunamo;

internal class FS
{
    internal static Func<string, bool> IsWindowsPathFormat;
    internal static Func<string, string> GetDirectoryName;
    internal static Action<string> CreateDirectory;
    internal static Func<string, bool> ExistsDirectory;
    internal static Action<string> CreateUpfoldersPsysicallyUnlessThere;
    internal static Action<string> CreateFoldersPsysicallyUnlessThere;

    internal static string WithEndSlash(string v)
    {
        return WithEndSlash(ref v);
    }

    /// <summary>
    ///     Usage: Exceptions.FileWasntFoundInDirectory
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    internal static string WithEndSlash(ref string v)
    {
        if (v != string.Empty)
        {
            v = v.TrimEnd(AllCharsSE.bs) + AllCharsSE.bs;
        }

        FirstCharUpper(ref v);
        return v;
    }

    internal static void FirstCharUpper(ref string nazevPP)
    {
        nazevPP = FirstCharUpper(nazevPP);
    }

    internal static string FirstCharUpper(string nazevPP)
    {
        if (nazevPP.Length == 1)
        {
            return nazevPP.ToUpper();
        }

        string sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToUpper() + sb;
    }
}
