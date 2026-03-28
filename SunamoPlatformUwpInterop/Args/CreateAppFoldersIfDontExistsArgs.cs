namespace SunamoPlatformUwpInterop.Args;

/// <summary>
/// Arguments for creating application folders if they do not exist.
/// </summary>
public class CreateAppFoldersIfDontExistsArgs
{
    /// <summary>
    /// Gets or sets the application name used for creating the root folder.
    /// </summary>
    public string AppName { get; set; } = "";

    /// <summary>
    /// Gets or sets the list of keys for common settings (prefix with ! for encrypted).
    /// </summary>
    public List<string> KeysCommonSettings { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of keys for boolean settings.
    /// </summary>
    public List<string> KeysSettingsBool { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of keys for list settings.
    /// </summary>
    public List<string> KeysSettingsList { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of keys for other string settings.
    /// </summary>
    public List<string> KeysSettingsOther { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of keys for DateTime settings.
    /// </summary>
    public List<string> KeysSettingsDateTime { get; set; } = new();

    /// <summary>
    /// Gets or sets the function for Rijndael byte decryption.
    /// </summary>
    public Func<List<byte>, List<byte>>? RijndaelBytesDecrypt { get; set; }

    /// <summary>
    /// Gets or sets the function for Rijndael byte encryption.
    /// </summary>
    public Func<List<byte>, List<byte>>? RijndaelBytesEncrypt { get; set; }
}
