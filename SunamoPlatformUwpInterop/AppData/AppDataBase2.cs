namespace SunamoPlatformUwpInterop.AppData;

public abstract partial class AppDataBase<StorageFolder, StorageFile> : IAppDataBase<StorageFolder, StorageFile>
{
    public
        async Task
    AppendAllText(AppFolders appFolders, string fileName, string value)
    {
        var fileToSave = desktopBase.GetFile(appFolders, fileName);
        await
        AppendAllText(fileToSave!.ToString()!, value);
    }

    public
        async Task
    AppendAllText(string filePath, string value)
    {
        await
        File.AppendAllTextAsync(filePath, value);
    }

    public string ReadFileOfSettingsExistingDirectoryOrFile(string key)
    {
        return ReadFileOfSettingsWorker(LoadedSettingsOther!, key);
    }

    public bool ReadFileOfSettingsBool(string key)
    {
        return ReadFileOfSettingsWorker(LoadedSettingsBool!, key);
    }

    public DateTime ReadFileOfSettingsDateTime(string key)
    {
        return ReadFileOfSettingsWorker(LoadedSettingsDateTime!, key);
    }
}
