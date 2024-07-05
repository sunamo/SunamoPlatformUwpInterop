namespace SunamoPlatformUwpInterop.PlatformUwpInterop;




/// <summary>
/// In non UWP is always passing as null
/// In appData is pass ci
/// in FSAbstract.TFAbstract is passed new instance
///
/// Nikdy nevolám přímo metodu z FS, např. _folder = ac.appData.GetFolder(af); ale AppData.ci.GetFolder a ta už se rozhodce zda použije ca (není null) nebo ne
/// </summary>
/// <typeparam name="StorageFolder"></typeparam>
/// <typeparam name="StorageFile"></typeparam>
public class AbstractCatalog<StorageFolder, StorageFile> : AbstractCatalogBase<StorageFolder, StorageFile>
{
    public AppDataBase<StorageFolder, StorageFile> appData;
    public FSAbstract<StorageFolder, StorageFile> fs = null;

    public TFAbstract<StorageFile> tf;

    public AbstractCatalog()
    {

    }
}
