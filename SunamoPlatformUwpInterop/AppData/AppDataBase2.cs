namespace SunamoPlatformUwpInterop.AppData;

public abstract partial class AppDataBase<StorageFolder, StorageFile> : IAppDataBase<StorageFolder, StorageFile>
{
    /// <summary>
    /// Appends text content to a file in the specified application folder.
    /// </summary>
    /// <param name="appFolders">The application folder category.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="value">The text to append.</param>
    public
#if ASYNC
        async Task
#else
    void
#endif
    AppendAllText(AppFolders appFolders, string fileName, string value)
    {
        var fileToSave = desktopBase.GetFile(appFolders, fileName);
#if ASYNC
        await
#endif
        AppendAllText(fileToSave!.ToString()!, value);
    }

    /// <summary>
    /// Appends text content to the specified file path.
    /// </summary>
    /// <param name="filePath">The path of the file to append to.</param>
    /// <param name="value">The text to append.</param>
    public
#if ASYNC
        async Task
#else
    void
#endif
    AppendAllText(string filePath, string value)
    {
#if ASYNC
        await
#endif
        File.AppendAllTextAsync(filePath, value);
    }

    /// <summary>
    /// Reads a settings file value for an existing directory or file key.
    /// If the file does not exist, creates it with empty content.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <returns>The settings value.</returns>
    public string ReadFileOfSettingsExistingDirectoryOrFile(string key)
    {
        return ReadFileOfSettingsWorker(LoadedSettingsOther!, key);
    }

    /// <summary>
    /// Reads a boolean settings value for the specified key.
    /// If the file does not exist or has empty content, creates it with false value.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <returns>The boolean settings value.</returns>
    public bool ReadFileOfSettingsBool(string key)
    {
        return ReadFileOfSettingsWorker(LoadedSettingsBool!, key);
    }

    /// <summary>
    /// Reads a DateTime settings value for the specified key.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <returns>The DateTime settings value.</returns>
    public DateTime ReadFileOfSettingsDateTime(string key)
    {
        return ReadFileOfSettingsWorker(LoadedSettingsDateTime!, key);
    }
}
