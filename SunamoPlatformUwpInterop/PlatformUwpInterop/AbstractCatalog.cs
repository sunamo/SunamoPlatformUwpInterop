namespace SunamoPlatformUwpInterop.PlatformUwpInterop;

/// <summary>
/// Provides an abstract catalog for platform-independent storage operations.
/// In non-UWP environments, null is always passed. In AppData, the singleton instance is used.
/// In FSAbstract/TFAbstract, a new instance is passed.
/// </summary>
/// <typeparam name="StorageFolder">The type representing a storage folder.</typeparam>
/// <typeparam name="StorageFile">The type representing a storage file.</typeparam>
public class AbstractCatalog<StorageFolder, StorageFile> : AbstractCatalogBase<StorageFolder, StorageFile>
{
    /// <summary>
    /// Gets or sets the application data provider.
    /// </summary>
    public AppDataBase<StorageFolder, StorageFile> AppData { get; set; }

    /// <summary>
    /// Gets or sets the file system abstraction.
    /// </summary>
    public FSAbstract<StorageFolder, StorageFile> FileSystem { get; set; }

    /// <summary>
    /// Gets or sets the text file abstraction.
    /// </summary>
    public TFAbstract<StorageFile> TextFile { get; set; }
}
