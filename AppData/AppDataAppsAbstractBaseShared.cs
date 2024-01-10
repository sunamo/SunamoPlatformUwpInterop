namespace SunamoPlatformUwpInterop.AppData;

public abstract partial class AppDataAppsAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
{
    public abstract StorageFolder GetRootFolder();

    protected abstract void SaveFile(string content, StorageFile sf);





    /// <summary>
    /// Pokud rootFolder bude SE nebo null, G false, jinak vrátí zda rootFolder existuej ve FS
    /// </summary>
    public abstract bool IsRootFolderOk();
    public abstract
#if ASYNC
 Task
#else
void  
#endif
AppendAllText(string value, StorageFile file);
}
