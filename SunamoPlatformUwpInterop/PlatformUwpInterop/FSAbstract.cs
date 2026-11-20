namespace SunamoPlatformUwpInterop.PlatformUwpInterop;

/// <summary>
/// Represents the FS abstract storage folder storage file type.
/// </summary>
public class FSAbstract<StorageFolder, StorageFile>
{
    /// <summary>
    /// Gets or sets the storage file from path.
    /// </summary>
    public Func<string, StorageFile> StorageFileFromPath { get; set; }
    /// <summary>
    /// Gets or sets the storage folder from path.
    /// </summary>
    public Func<string, StorageFolder> StorageFolderFromPath { get; set; }
    /// <summary>
    /// Gets or sets the exists directory.
    /// </summary>
    public Func<string, bool?> ExistsDirectory { get; set; }
    /// <summary>
    /// Gets or sets the exists file.
    /// </summary>
    public Func<StorageFile, bool> ExistsFile { get; set; }
    /// <summary>
    /// Gets or sets the get directory name.
    /// </summary>
    public Func<StorageFile, StorageFolder> GetDirectoryName { get; set; }
    /// <summary>
    /// Gets or sets the get directory name folder.
    /// </summary>
    public Func<StorageFolder, StorageFolder> GetDirectoryNameFolder { get; set; }
    /// <summary>
    /// Gets or sets the get file name.
    /// </summary>
    public Func<StorageFile, string> GetFileName { get; set; }
    /// <summary>
    /// Gets or sets the get files.
    /// </summary>
    public Func<StorageFolder, string, bool, List<StorageFile>> GetFiles { get; set; }
    /// <summary>
    /// Gets or sets the get files of extension case insensitive recursively.
    /// </summary>
    public Func<StorageFolder, string, List<StorageFile>> GetFilesOfExtensionCaseInsensitiveRecursively { get; set; }
    /// <summary>
    /// Gets or sets the get storage file.
    /// </summary>
    public Func<StorageFolder, string, StorageFile> GetStorageFile { get; set; }
    /// <summary>
    /// Gets or sets the is folders equals.
    /// </summary>
    public Func<StorageFolder, StorageFolder, bool> IsFoldersEquals { get; set; }
    /// <summary>
    /// Gets or sets the open stream for read async.
    /// </summary>
    public Func<StorageFile, Stream> OpenStreamForReadAsync { get; set; }
    /// <summary>
    /// Gets or sets the path from storage file.
    /// </summary>
    public Func<StorageFile, string> PathFromStorageFile { get; set; }
    /// <summary>
    /// Gets or sets the storage file path.
    /// </summary>
    public Func<StorageFile, string> StorageFilePath { get; set; }
}
