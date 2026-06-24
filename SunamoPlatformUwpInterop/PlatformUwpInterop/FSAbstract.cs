namespace SunamoPlatformUwpInterop.PlatformUwpInterop;

public class FSAbstract<StorageFolder, StorageFile>
{
    public Func<string, StorageFile> StorageFileFromPath { get; set; }
    public Func<string, StorageFolder> StorageFolderFromPath { get; set; }
    public Func<string, bool?> ExistsDirectory { get; set; }
    public Func<StorageFile, bool> ExistsFile { get; set; }
    public Func<StorageFile, StorageFolder> GetDirectoryName { get; set; }
    public Func<StorageFolder, StorageFolder> GetDirectoryNameFolder { get; set; }
    public Func<StorageFile, string> GetFileName { get; set; }
    public Func<StorageFolder, string, bool, List<StorageFile>> GetFiles { get; set; }
    public Func<StorageFolder, string, List<StorageFile>> GetFilesOfExtensionCaseInsensitiveRecursively { get; set; }
    public Func<StorageFolder, string, StorageFile> GetStorageFile { get; set; }
    public Func<StorageFolder, StorageFolder, bool> IsFoldersEquals { get; set; }
    public Func<StorageFile, Stream> OpenStreamForReadAsync { get; set; }
    public Func<StorageFile, string> PathFromStorageFile { get; set; }
    public Func<StorageFile, string> StorageFilePath { get; set; }
}
