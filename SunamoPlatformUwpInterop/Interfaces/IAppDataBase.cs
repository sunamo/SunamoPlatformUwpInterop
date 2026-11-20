namespace SunamoPlatformUwpInterop.Interfaces;

/// <summary>
/// Represents the i app data base storage folder storage file type.
/// </summary>
public interface IAppDataBase<StorageFolder, StorageFile>
{
    /// <summary>
    /// Get file common settings.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>The get file common settings result.</returns>
    string GetFileCommonSettings(string key);
    /// <summary>
    /// Root folder common.
    /// </summary>
    /// <param name="isInFolderCommon">The is in folder common.</param>
    /// <returns>The root folder common result.</returns>
    string RootFolderCommon(bool isInFolderCommon);
}
