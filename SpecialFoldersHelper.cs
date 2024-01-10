namespace SunamoPlatformUwpInterop;


public partial class SpecialFoldersHelper //: SpecialFoldersHelperSE
{
    /// <summary>
    /// Return root folder of AppData (as C:\Users\n\AppData\)
    /// </summary>
    public static string ApplicationData()
    {
        return FS.GetDirectoryName(AppDataRoaming());
    }
}
