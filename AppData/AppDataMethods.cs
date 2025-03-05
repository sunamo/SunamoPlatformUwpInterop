namespace SunamoPlatformUwpInterop.AppData;

public class AppDataMethods
{
    public static string GetFileData(string fn)
    {
        return AppData.ci.GetFile(AppFolders.Data, fn);
    }
    public static string GetFileSettings(string fn)
    {
        return AppData.ci.GetFile(AppFolders.Settings, fn);
    }
}