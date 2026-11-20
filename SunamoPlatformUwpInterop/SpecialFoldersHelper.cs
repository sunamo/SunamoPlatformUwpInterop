namespace SunamoPlatformUwpInterop;

/// <summary>
/// Represents the special folders helper type.
/// </summary>
public class SpecialFoldersHelper
{
    /// <summary>
    /// App data roaming.
    /// </summary>
    /// <returns>The app data roaming result.</returns>
    public static string AppDataRoaming()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }

    /// <summary>
    /// Application data.
    /// </summary>
    /// <returns>The application data result.</returns>
    public static string ApplicationData()
    {
        return Path.GetDirectoryName(AppDataRoaming())!;
    }
}
