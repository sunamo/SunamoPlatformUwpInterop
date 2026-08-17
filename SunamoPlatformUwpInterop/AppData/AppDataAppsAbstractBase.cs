namespace SunamoPlatformUwpInterop.AppData;

public abstract class AppDataAppsAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
{
    public abstract StorageFolder GetRootFolder();

    protected abstract void SaveFile(string content, StorageFile storageFile);

    public abstract bool IsRootFolderOk();

    public abstract
        Task
        AppendAllText(string value, StorageFile storageFile);

    public abstract StorageFile GetFile(AppFolders appFolders, string fileName);

    public abstract bool IsRootFolderNull();
}
