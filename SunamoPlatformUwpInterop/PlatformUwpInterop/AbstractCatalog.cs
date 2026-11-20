namespace SunamoPlatformUwpInterop.PlatformUwpInterop;

/// <summary>
/// Represents the abstract catalog storage folder storage file type.
/// </summary>
public class AbstractCatalog<StorageFolder, StorageFile> : AbstractCatalogBase<StorageFolder, StorageFile>
{
    /// <summary>
    /// Gets or sets the app data.
    /// </summary>
    public AppDataBase<StorageFolder, StorageFile> AppData { get; set; }
    /// <summary>
    /// Gets or sets the file system.
    /// </summary>
    public FSAbstract<StorageFolder, StorageFile> FileSystem { get; set; }
    /// <summary>
    /// Gets or sets the text file.
    /// </summary>
    public TFAbstract<StorageFile> TextFile { get; set; }
}
