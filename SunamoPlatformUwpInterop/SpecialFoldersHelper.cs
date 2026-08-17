namespace SunamoPlatformUwpInterop;

public class SpecialFoldersHelper
{
    public static string AppDataRoaming()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }

    public static string ApplicationData()
    {
        return Path.GetDirectoryName(AppDataRoaming())!;
    }
}
