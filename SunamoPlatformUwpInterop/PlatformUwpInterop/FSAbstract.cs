namespace SunamoPlatformUwpInterop.PlatformUwpInterop;

public class FSAbstract<StorageFolder, StorageFile>
{
    /// <summary>
    ///     get StorageFile from path
    /// </summary>
    public Func<string, StorageFile> ciStorageFile;

    public Func<string, StorageFolder> ciStorageFolder;

    public Func<string, bool?> existsDirectory;

    //GetFilesOfExtensionCaseInsensitiveRecursively
    public Func<StorageFile, bool> existsFile;
    public Func<StorageFile, StorageFolder> getDirectoryName;
    public Func<StorageFolder, StorageFolder> getDirectoryNameFolder;
    public Func<StorageFile, string> getFileName;

    /// <summary>
    ///     folder,mask,recursive : List<StorageFile>
    /// </summary>
    public Func<StorageFolder, string, bool, List<StorageFile>> getFiles;

    public Func<StorageFolder, string, List<StorageFile>> getFilesOfExtensionCaseInsensitiveRecursively;

    /// <summary>
    ///     From storage folder and name
    /// </summary>
    public Func<StorageFolder, string, StorageFile> getStorageFile;

    public Func<StorageFolder, StorageFolder, bool> isFoldersEquals;
    public Func<StorageFile, Stream> openStreamForReadAsync;
    public Func<StorageFile, string> pathFromStorageFile;
    public Func<StorageFile, string> storageFilePath;
}