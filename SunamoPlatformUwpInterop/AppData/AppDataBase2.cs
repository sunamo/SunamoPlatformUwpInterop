namespace SunamoPlatformUwpInterop.AppData;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public abstract partial class AppDataBase<StorageFolder, StorageFile> : IAppDataBase<StorageFolder, StorageFile>
{
    /// <summary>
    ///     Append to file A2 in AF A1 with contents A3
    /// </summary>
    /// <param name = "af"></param>
    /// <param name = "file"></param>
    /// <param name = "value"></param>
    public 
#if ASYNC
        async Task
#else
    void 
#endif
    AppendAllText(AppFolders af, string file, string value)
    {
        var fileToSave = AbstractNon.GetFile(af, file);
#if ASYNC
        await
#endif
        AppendAllText(fileToSave.ToString(), value);
    }

    /// <summary>
    ///     Just call TF.AppendAllText
    /// </summary>
    /// <param name = "file"></param>
    /// <param name = "value"></param>
    public 
#if ASYNC
        async Task
#else
    void 
#endif
    AppendAllText(string file, string value)
    {
#if ASYNC
        await
#endif
        File.AppendAllTextAsync(file, value);
    }

    /// <summary>
    ///     If file A1 dont exists, then create him with empty content and G . When optained file/folder doesnt exists, return
    ///     SE
    /// </summary>
    /// <param name = "key"></param>
    public string ReadFileOfSettingsExistingDirectoryOrFile(string key)
    {
        return ReadFileOfSettingsWorker(loadedSettingsOther, key);
    }

    /// <summary>
    ///     If file A1 dont exists or have empty content, then create him with empty content and G false
    /// </summary>
    /// <param name = "path"></param>
    public bool ReadFileOfSettingsBool(string key)
    {
        return ReadFileOfSettingsWorker(loadedSettingsBool, key);
    }

    public DateTime ReadFileOfSettingsDateTime(string key)
    {
        return ReadFileOfSettingsWorker(loadedSettingsDateTime, key);
    }
}