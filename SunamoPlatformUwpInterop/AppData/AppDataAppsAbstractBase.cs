namespace SunamoPlatformUwpInterop.AppData;

/// <summary>
/// Represents the app data apps abstract base storage folder storage file type.
/// </summary>
public abstract class AppDataAppsAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
{
    /// <summary>
    /// Get root folder.
    /// </summary>
    /// <returns>The get root folder result.</returns>
    public abstract StorageFolder GetRootFolder();

    /// <summary>
    /// Save file.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="storageFile">The storage file.</param>
    protected abstract void SaveFile(string content, StorageFile storageFile);

    /// <summary>
    /// Is root folder ok.
    /// </summary>
    /// <returns>The is root folder ok result.</returns>
    public abstract bool IsRootFolderOk();

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
    /// Is root folder null.
    /// </summary>
    /// <returns>The is root folder null result.</returns>
    public abstract bool IsRootFolderNull();
}
