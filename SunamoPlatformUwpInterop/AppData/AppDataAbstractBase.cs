namespace SunamoPlatformUwpInterop.AppData;

public abstract class AppDataAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
{
    public abstract StorageFolder GetRootFolder(string appName);

    public string ReadFileOfSettingsDirectoryOrFile(string key)
    {
        return ReadFileOfSettingsWorker(LoadedSettingsOther!, key);
    }

    protected abstract
        Task
        SaveFile(string content, StorageFile storageFile);

    public string ReadFileOfSettingsOther(string key)
    {
        return LoadedSettingsOther![key];
    }

    public abstract bool IsRootFolderOk();

    public new abstract
        Task
        AppendAllText(AppFolders appFolders, string fileName, string value);

    public abstract
        Task
        AppendAllText(string value, StorageFile storageFile);

    public abstract StorageFile GetFile(AppFolders appFolders, string fileName);

    public abstract StorageFile GetFileString(string appFolderName, string fileName, bool isUsingParentAppFolder = false);

    public abstract bool IsRootFolderNull();

    public abstract StorageFolder GetCommonSettings(string key, bool isCrypted = true);

    public abstract void SetCommonSettings(string key, string value);
}
