namespace SunamoPlatformUwpInterop.AppData;

public abstract partial class AppDataBase<StorageFolder, StorageFile> : IAppDataBase<StorageFolder, StorageFile>
{
    /// <summary>
    /// Append all text.
    /// </summary>
    /// <param name="appFolders">The app folders.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="value">The value.</param>
    public
        async Task
    AppendAllText(AppFolders appFolders, string fileName, string value)
    {
        var fileToSave = desktopBase.GetFile(appFolders, fileName);
        await
        AppendAllText(fileToSave!.ToString()!, value);
    }

    /// <summary>
    /// Append all text.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="value">The value.</param>
    public
        async Task
    AppendAllText(string filePath, string value)
    {
        await
        File.AppendAllTextAsync(filePath, value);
    }

    /// <summary>
    /// Read file of settings existing directory or file.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>The read file of settings existing directory or file result.</returns>
    public string ReadFileOfSettingsExistingDirectoryOrFile(string key)
    {
        return ReadFileOfSettingsWorker(LoadedSettingsOther!, key);
    }

    /// <summary>
    /// Read file of settings bool.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>The read file of settings bool result.</returns>
    public bool ReadFileOfSettingsBool(string key)
    {
        return ReadFileOfSettingsWorker(LoadedSettingsBool!, key);
    }

    /// <summary>
    /// Read file of settings date time.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>The read file of settings date time result.</returns>
    public DateTime ReadFileOfSettingsDateTime(string key)
    {
        return ReadFileOfSettingsWorker(LoadedSettingsDateTime!, key);
    }
}
