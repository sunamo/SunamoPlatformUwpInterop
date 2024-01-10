namespace SunamoPlatformUwpInterop.AppData;

/// <summary>
/// Another methods which is adecvate only in desktop apps
/// AppDataAppsAbstractBase - methods which are applied only on UAP
/// AppDataAbstractBase (this) - methods which are applied only on desktop
/// </summary>
/// <typeparam name="StorageFolder"></typeparam>
/// <typeparam name="StorageFile"></typeparam>
public abstract partial class AppDataAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
{
    public abstract StorageFolder GetRootFolder();

    /// <summary>
    /// If file A1 dont exists, then create him with empty content and G SE. When optained file/folder doesnt exists, return it anyway
    /// </summary>
    /// <param name = "path"></param>
    public string ReadFileOfSettingsDirectoryOrFile(string key)
    {
        return ReadFileOfSettingsWorker(loadedSettingsOther, key);
    }

    protected abstract
#if ASYNC
 Task
#else
void  
#endif
SaveFile(string content, StorageFile sf);

    /// <summary>
    /// If file A1 dont exists or have empty content, then create him with empty content and G SE
    /// </summary>
    /// <param name = "path"></param>
    public string ReadFileOfSettingsOther(string path)
    {
        return loadedSettingsOther[path];
    }



    /// <summary>
    /// Pokud rootFolder bude SE nebo null, G false, jinak vrátí zda rootFolder existuej ve FS
    /// </summary>
    public abstract bool IsRootFolderOk();
    public abstract new
#if ASYNC
 Task
#else
void  
#endif
AppendAllText(AppFolders af, string file, string value);

    public abstract
#if ASYNC
 Task
#else
void  
#endif
AppendAllText(string value, StorageFile file);

    /// <summary>
    /// G path file A2 in AF A1.
    /// Automatically create upfolder if there dont exists.
    /// </summary>
    /// <param name = "af"></param>
    /// <param name = "file"></param>
    public abstract StorageFile GetFile(AppFolders af, string file);

    public abstract StorageFile GetFileString(string af, string file, bool pa = false);

    public abstract bool IsRootFolderNull();
    //public abstract StorageFolder GetSunamoFolder();
    public abstract StorageFolder GetCommonSettings(string key, bool isCrypted = true);

    public abstract void SetCommonSettings(string key, string value);


}
