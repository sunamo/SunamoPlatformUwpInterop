namespace SunamoPlatformUwpInterop;


/// <summary>
/// Musejí být všechny init protože už dále nedělám žádné checky na null
/// </summary>
internal class CreateAppFoldersIfDontExistsArgs
{
    internal string AppName = "";
    /// <summary>
    /// override
    /// </summary>
    internal List<string> keysCommonSettings = new List<string>();
    /// <summary>
    /// vylepšení pro non uwp apps
    /// </summary>
    internal List<string> keysSettingsList = new List<string>();
    internal List<string> keysSettingsBool = new List<string>();
    internal List<string> keysSettingsOther = new List<string>();
    internal Func<List<byte>, List<byte>> RijndaelBytesDecrypt;
    internal Func<List<byte>, List<byte>> RijndaelBytesEncrypt;
}