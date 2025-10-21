// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
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