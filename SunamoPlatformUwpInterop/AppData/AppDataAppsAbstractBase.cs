namespace SunamoPlatformUwpInterop.AppData;

/// <summary>
/// Abstract base class for UWP/UAP application data management.
/// This class provides methods that are applied only on UAP,
/// while AppDataAbstractBase provides methods only for desktop.
/// </summary>
/// <typeparam name="StorageFolder">The type representing a storage folder.</typeparam>
/// <typeparam name="StorageFile">The type representing a storage file.</typeparam>
public abstract class AppDataAppsAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
{
    /// <summary>
    /// Gets the root folder for the application.
    /// </summary>
    /// <returns>The root folder.</returns>
    public abstract StorageFolder GetRootFolder();

    /// <summary>
    /// Saves the specified content to the given storage file.
    /// </summary>
    /// <param name="content">The text content to save.</param>
    /// <param name="storageFile">The storage file to save to.</param>
    protected abstract void SaveFile(string content, StorageFile storageFile);

    /// <summary>
    /// Checks whether the root folder exists in the file system.
    /// If rootFolder is empty or null, returns false.
    /// </summary>
    /// <returns>True if the root folder exists; otherwise false.</returns>
    public abstract bool IsRootFolderOk();

    /// <summary>
    /// Appends text content to the specified storage file.
    /// </summary>
    /// <param name="value">The text to append.</param>
    /// <param name="storageFile">The storage file to append to.</param>
    public abstract
#if ASYNC
        Task
#else
        void
#endif
        AppendAllText(string value, StorageFile storageFile);

    /// <summary>
    /// Gets the file path for the specified file in the given application folder.
    /// Automatically creates the parent folder if it does not exist.
    /// </summary>
    /// <param name="appFolders">The application folder category.</param>
    /// <param name="fileName">The file name.</param>
    /// <returns>The storage file path.</returns>
    public abstract StorageFile GetFile(AppFolders appFolders, string fileName);

    /// <summary>
    /// Checks whether the root folder is null or empty.
    /// </summary>
    /// <returns>True if the root folder is null or empty; otherwise false.</returns>
    public abstract bool IsRootFolderNull();
}
