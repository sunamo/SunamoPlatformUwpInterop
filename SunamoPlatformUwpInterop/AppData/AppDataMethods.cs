namespace SunamoPlatformUwpInterop.AppData;

public class AppDataMethods
{
    public static string GetFileData(string fileName)
    {
        return AppData.Instance.GetFile(AppFolders.Data, fileName);
    }

    public static string GetFileSettings(string fileName)
    {
        return AppData.Instance.GetFile(AppFolders.Settings, fileName);
    }
}
