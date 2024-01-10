namespace SunamoPlatformUwpInterop.AppData;


public abstract partial class AppDataBase<StorageFolder, StorageFile>
{
    /// <summary>
    /// If file A1 dont exists, then create him with empty content and G SE. When optained file/folder doesnt exists, return SE
    /// </summary>
    /// <param name = "key"></param>
    public string ReadFileOfSettingsExistingDirectoryOrFile(string key)
    {
        return ReadFileOfSettingsWorker<string>(loadedSettingsOther, key);
    }

    /// <summary>
    /// If file A1 dont exists or have empty content, then create him with empty content and G false
    /// </summary>
    /// <param name = "path"></param>
    public bool ReadFileOfSettingsBool(string key)
    {
        return ReadFileOfSettingsWorker<bool>(loadedSettingsBool, key);
    }
}
