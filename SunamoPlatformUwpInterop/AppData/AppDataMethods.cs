namespace SunamoPlatformUwpInterop.AppData;

/// <summary>
/// Provides convenience methods for accessing application data files.
/// </summary>
public class AppDataMethods
{
    /// <summary>
    /// Gets the file path for a file in the Data folder.
    /// </summary>
    /// <param name="fileName">The name of the file.</param>
    /// <returns>The full path to the file in the Data folder.</returns>
    public static string GetFileData(string fileName)
    {
        return AppData.Instance.GetFile(AppFolders.Data, fileName);
    }

    /// <summary>
    /// Gets the file path for a file in the Settings folder.
    /// </summary>
    /// <param name="fileName">The name of the file.</param>
    /// <returns>The full path to the file in the Settings folder.</returns>
    public static string GetFileSettings(string fileName)
    {
        return AppData.Instance.GetFile(AppFolders.Settings, fileName);
    }
}
