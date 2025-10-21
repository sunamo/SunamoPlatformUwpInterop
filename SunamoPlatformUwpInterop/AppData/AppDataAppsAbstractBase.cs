// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoPlatformUwpInterop.AppData;

public abstract class AppDataAppsAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
{
    public abstract StorageFolder GetRootFolder();

    protected abstract void SaveFile(string content, StorageFile sf);


    /// <summary>
    ///     Pokud rootFolder bude SE nebo null, G false, jinak vr�t� zda rootFolder existuej ve FS
    /// </summary>
    public abstract bool IsRootFolderOk();

    public abstract
#if ASYNC
        Task
#else
void
#endif
        AppendAllText(string value, StorageFile file);

    /// <summary>
    ///     AppDataAppsAbstractBase (this) - methods which are applied only on UAP
    ///     AppDataAbstractBase  - methods which are applied only on desktop
    ///     G path file A2 in AF A1.
    ///     Automatically create upfolder if there dont exists.
    /// </summary>
    /// <param name="af"></param>
    /// <param name="file"></param>
    public abstract StorageFile GetFile(AppFolders af, string file);

    public abstract bool IsRootFolderNull();


    //public abstract StorageFolder> GetSunamoFolder();
}