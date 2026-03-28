namespace SunamoPlatformUwpInterop.AppData;

/// <summary>
/// Abstract base class for desktop application data management providing root folder management,
/// file operations, and common settings access.
/// </summary>
/// <typeparam name="StorageFolder">The type representing a storage folder.</typeparam>
/// <typeparam name="StorageFile">The type representing a storage file.</typeparam>
public abstract class AppDataAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
{
    /// <summary>
    /// Gets the root folder for the application with the specified name.
    /// </summary>
    /// <param name="appName">The application name.</param>
    /// <returns>The root folder for the application.</returns>
    public abstract StorageFolder GetRootFolder(string appName);

    /// <summary>
    /// Reads a settings file value for a directory or file key.
    /// If the file does not exist, creates it with empty content.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <returns>The settings value.</returns>
    public string ReadFileOfSettingsDirectoryOrFile(string key)
    {
        return ReadFileOfSettingsWorker(LoadedSettingsOther!, key);
    }

    /// <summary>
    /// Saves the specified content to the given storage file.
    /// </summary>
    /// <param name="content">The text content to save.</param>
    /// <param name="storageFile">The storage file to save to.</param>
    protected abstract
#if ASYNC
        Task
#else
        void
#endif
        SaveFile(string content, StorageFile storageFile);

    /// <summary>
    /// Reads a string settings value for the specified key from loaded settings.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <returns>The settings value.</returns>
    public string ReadFileOfSettingsOther(string key)
    {
        return LoadedSettingsOther![key];
    }

    /// <summary>
    /// Checks whether the root folder exists in the file system.
    /// If rootFolder is empty or null, returns false.
    /// </summary>
    /// <returns>True if the root folder exists; otherwise false.</returns>
    public abstract bool IsRootFolderOk();

    /// <summary>
    /// Appends text content to a file in the specified application folder.
    /// </summary>
    /// <param name="appFolders">The application folder category.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="value">The text to append.</param>
    public new abstract
#if ASYNC
        Task
#else
        void
#endif
        AppendAllText(AppFolders appFolders, string fileName, string value);

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
    /// Gets the file path as a string for the specified application folder name and file name.
    /// </summary>
    /// <param name="appFolderName">The application folder name as a string.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="isUsingParentAppFolder">Whether to use the parent application root folder.</param>
    /// <returns>The file path as a string.</returns>
    public abstract StorageFile GetFileString(string appFolderName, string fileName, bool isUsingParentAppFolder = false);

    /// <summary>
    /// Checks whether the root folder is null or empty.
    /// </summary>
    /// <returns>True if the root folder is null or empty; otherwise false.</returns>
    public abstract bool IsRootFolderNull();

    /// <summary>
    /// Gets a common settings value for the specified key.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <param name="isCrypted">Whether the value is encrypted.</param>
    /// <returns>The common settings value.</returns>
    public abstract StorageFolder GetCommonSettings(string key, bool isCrypted = true);

    /// <summary>
    /// Sets a common settings value for the specified key.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <param name="value">The value to set.</param>
    public abstract void SetCommonSettings(string key, string value);
}
