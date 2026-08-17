namespace SunamoPlatformUwpInterop.PlatformUwpInterop;

public class AbstractCatalog<StorageFolder, StorageFile> : AbstractCatalogBase<StorageFolder, StorageFile>
{
    public AppDataBase<StorageFolder, StorageFile> AppData { get; set; }
    public FSAbstract<StorageFolder, StorageFile> FileSystem { get; set; }
    public TFAbstract<StorageFile> TextFile { get; set; }
}
