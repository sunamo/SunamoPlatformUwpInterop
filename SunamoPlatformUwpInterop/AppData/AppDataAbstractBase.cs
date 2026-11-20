namespace SunamoPlatformUwpInterop.AppData;

/// <summary>
/// Represents the app data abstract base storage folder storage file type.
/// </summary>
public abstract class AppDataAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
{
    /// <summary>
    /// Get root folder.
    /// </summary>
    /// <param name="appName">The app name.</param>
    /// <returns>The get root folder result.</returns>
    public abstract StorageFolder GetRootFolder(string appName);

    /// <summary>
    /// Read file of settings directory or file.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>The read file of settings directory or file result.</returns>
    public string ReadFileOfSettingsDirectoryOrFile(string key)
    {
        return ReadFileOfSettingsWorker(LoadedSettingsOther!, key);
    }

    /// <summary>
    /// Save file.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="storageFile">The storage file.</param>
    protected abstract
        Task
        SaveFile(string content, StorageFile storageFile);

    /// <summary>
    /// Read file of settings other.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>The read file of settings other result.</returns>
    public string ReadFileOfSettingsOther(string key)
    {
        return LoadedSettingsOther![key];
    }

    /// <summary>
    /// Is root folder ok.
    /// </summary>
    /// <returns>The is root folder ok result.</returns>
    public abstract bool IsRootFolderOk();

    /// <summary>
    /// Append all text.
    /// </summary>
    /// <param name="appFolders">The app folders.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="value">The value.</param>
    public new abstract
        Task
        AppendAllText(AppFolders appFolders, string fileName, string value);

    /// <summary>
    /// Append all text.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="storageFile">The storage file.</param>
    public abstract
        Task
        AppendAllText(string value, StorageFile storageFile);

    /// <summary>
    /// Get file.
    /// </summary>
    /// <param name="appFolders">The app folders.</param>
    /// <param name="fileName">The file name.</param>
    /// <returns>The get file result.</returns>
    public abstract StorageFile GetFile(AppFolders appFolders, string fileName);

    /// <summary>
    /// Get file string.
    /// </summary>
    /// <param name="appFolderName">The app folder name.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="isUsingParentAppFolder">The is using parent app folder.</param>
    /// <returns>The get file string result.</returns>
    public abstract StorageFile GetFileString(string appFolderName, string fileName, bool isUsingParentAppFolder = false);

    /// <summary>
    /// Is root folder null.
    /// </summary>
    /// <returns>The is root folder null result.</returns>
    public abstract bool IsRootFolderNull();

    /// <summary>
    /// Get common settings.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="isCrypted">The is crypted.</param>
    /// <returns>The get common settings result.</returns>
    public abstract StorageFolder GetCommonSettings(string key, bool isCrypted = true);

    /// <summary>
    /// Set common settings.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public abstract void SetCommonSettings(string key, string value);
}
