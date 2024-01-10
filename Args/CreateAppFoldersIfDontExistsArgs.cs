namespace SunamoPlatformUwpInterop.Args;

/// <summary>
/// Musejí být všechny init protože už dále nedělám žádné checky na null
/// </summary>
public class CreateAppFoldersIfDontExistsArgs
{
    /// <summary>
    /// override
    /// </summary>
    public List<string> keysCommonSettings = new List<string>();

    /// <summary>
    /// vylepšení pro non uwp apps
    /// </summary>
    public List<string> keysSettingsList = new List<string>();

    public List<string> keysSettingsBool = new List<string>();
    public List<string> keysSettingsOther = new List<string>();
}
