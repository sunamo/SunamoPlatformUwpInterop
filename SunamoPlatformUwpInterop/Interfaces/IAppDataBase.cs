namespace SunamoPlatformUwpInterop.Interfaces;

/// <summary>
/// Interface for application data base providing access to common settings and root folder.
/// </summary>
/// <typeparam name="StorageFolder">The type representing a storage folder.</typeparam>
/// <typeparam name="StorageFile">The type representing a storage file.</typeparam>
public interface IAppDataBase<StorageFolder, StorageFile>
{
    /// <summary>
    /// Gets the file path for the specified common settings key.
    /// </summary>
    /// <param name="key">The settings key to look up.</param>
    /// <returns>The file path for the common settings file.</returns>
    string GetFileCommonSettings(string key);

    /// <summary>
    /// Gets the root folder path for common application data.
    /// </summary>
    /// <param name="isInFolderCommon">Whether to return the Common subfolder path.</param>
    /// <returns>The root folder path.</returns>
    string RootFolderCommon(bool isInFolderCommon);
}
