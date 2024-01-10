namespace SunamoPlatformUwpInterop.AppData;


/// <summary>
///
/// </summary>
public abstract partial class AppDataBase<StorageFolder, StorageFile>
{

    public T ReadFileOfSettingsWorker<T>(IDictionary<string, T> loadedSettingsOther, string key)
    {
        ThrowEx.IsWindowsPathFormat(key, FS.IsWindowsPathFormat);

        if (!loadedSettingsOther.ContainsKey(key))
        {
            ThrowEx.Custom($"{key} was not found in dictionary, probably was not specified as deps in calling CreateAppFoldersIfDontExists");
        }

        return loadedSettingsOther[key];
    }

    public AppDataBase()
    {
    }



    /// <summary>
    /// Save file A1 to folder AF Settings with value A2.
    /// </summary>
    /// <param name = "file"></param>
    /// <param name = "value"></param>
    public void SaveFileOfSettings(string file, string value)
    {
        StorageFile fileToSave = AbstractNon.GetFile(AppFolders.Settings, file);
        AbstractNon.SaveFile(value, fileToSave);
    }

    /// <summary>
    /// Save file A2 to AF A1 with contents A3
    /// </summary>
    /// <param name = "af"></param>
    /// <param name = "file"></param>
    /// <param name = "value"></param>
    public StorageFile SaveFile(AppFolders af, string file, string value)
    {
        StorageFile fileToSave = AbstractNon.GetFile(af, file);
        SaveFile(value, fileToSave);
        return fileToSave;
    }

    private void SaveFile(string value, StorageFile fileToSave)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Append to file A2 in AF A1 with contents A3
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
    /// Just call TF.AppendAllText
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
        TFSE.AppendAllText(file, value);
    }
}
