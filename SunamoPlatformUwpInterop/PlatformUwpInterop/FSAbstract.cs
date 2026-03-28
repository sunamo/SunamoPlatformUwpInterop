namespace SunamoPlatformUwpInterop.PlatformUwpInterop;

/// <summary>
/// Provides abstract file system operations for platform-independent file access.
/// </summary>
/// <typeparam name="StorageFolder">The type representing a storage folder.</typeparam>
/// <typeparam name="StorageFile">The type representing a storage file.</typeparam>
public class FSAbstract<StorageFolder, StorageFile>
{
    /// <summary>
    /// Gets or sets the function for creating a StorageFile from a path.
    /// </summary>
    public Func<string, StorageFile> StorageFileFromPath { get; set; }

    /// <summary>
    /// Gets or sets the function for creating a StorageFolder from a path.
    /// </summary>
    public Func<string, StorageFolder> StorageFolderFromPath { get; set; }

    /// <summary>
    /// Gets or sets the function for checking if a directory exists.
    /// </summary>
    public Func<string, bool?> ExistsDirectory { get; set; }

    /// <summary>
    /// Gets or sets the function for checking if a file exists.
    /// </summary>
    public Func<StorageFile, bool> ExistsFile { get; set; }

    /// <summary>
    /// Gets or sets the function for getting the directory name from a file.
    /// </summary>
    public Func<StorageFile, StorageFolder> GetDirectoryName { get; set; }

    /// <summary>
    /// Gets or sets the function for getting the directory name from a folder.
    /// </summary>
    public Func<StorageFolder, StorageFolder> GetDirectoryNameFolder { get; set; }

    /// <summary>
    /// Gets or sets the function for getting the file name from a storage file.
    /// </summary>
    public Func<StorageFile, string> GetFileName { get; set; }

    /// <summary>
    /// Gets or sets the function for getting files from a folder (folder, mask, recursive).
    /// </summary>
    public Func<StorageFolder, string, bool, List<StorageFile>> GetFiles { get; set; }

    /// <summary>
    /// Gets or sets the function for getting files by extension recursively (case insensitive).
    /// </summary>
    public Func<StorageFolder, string, List<StorageFile>> GetFilesOfExtensionCaseInsensitiveRecursively { get; set; }

    /// <summary>
    /// Gets or sets the function for getting a StorageFile from a folder and name.
    /// </summary>
    public Func<StorageFolder, string, StorageFile> GetStorageFile { get; set; }

    /// <summary>
    /// Gets or sets the function for comparing two folders for equality.
    /// </summary>
    public Func<StorageFolder, StorageFolder, bool> IsFoldersEquals { get; set; }

    /// <summary>
    /// Gets or sets the function for opening a read stream from a storage file.
    /// </summary>
    public Func<StorageFile, Stream> OpenStreamForReadAsync { get; set; }

    /// <summary>
    /// Gets or sets the function for getting the path from a StorageFile.
    /// </summary>
    public Func<StorageFile, string> PathFromStorageFile { get; set; }

    /// <summary>
    /// Gets or sets the function for getting the storage file path.
    /// </summary>
    public Func<StorageFile, string> StorageFilePath { get; set; }
}
