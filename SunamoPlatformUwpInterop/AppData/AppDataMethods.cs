namespace SunamoPlatformUwpInterop.AppData;

/// <summary>
/// Represents the app data methods type.
/// </summary>
public class AppDataMethods
{
    /// <summary>
    /// Get file data.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <returns>The get file data result.</returns>
    public static string GetFileData(string fileName)
    {
        return AppData.Instance.GetFile(AppFolders.Data, fileName);
    }

    /// <summary>
    /// Get file settings.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <returns>The get file settings result.</returns>
    public static string GetFileSettings(string fileName)
    {
        return AppData.Instance.GetFile(AppFolders.Settings, fileName);
    }
}
