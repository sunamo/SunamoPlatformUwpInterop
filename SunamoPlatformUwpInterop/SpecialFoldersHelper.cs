namespace SunamoPlatformUwpInterop;

/// <summary>
/// Provides helper methods for accessing special system folders.
/// </summary>
public class SpecialFoldersHelper
{
    /// <summary>
    /// Gets the path to the AppData\Roaming folder.
    /// </summary>
    /// <returns>The path to the AppData\Roaming folder.</returns>
    public static string AppDataRoaming()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }

    /// <summary>
    /// Gets the root folder of AppData (e.g. C:\Users\username\AppData\).
    /// </summary>
    /// <returns>The path to the AppData root folder.</returns>
    public static string ApplicationData()
    {
        return Path.GetDirectoryName(AppDataRoaming())!;
    }
}
