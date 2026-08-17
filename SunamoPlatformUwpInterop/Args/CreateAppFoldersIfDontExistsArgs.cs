namespace SunamoPlatformUwpInterop.Args;

public class CreateAppFoldersIfDontExistsArgs
{
    public string AppName { get; set; } = "";
    public List<string> KeysCommonSettings { get; set; } = new();
    public List<string> KeysSettingsBool { get; set; } = new();
    public List<string> KeysSettingsList { get; set; } = new();
    public List<string> KeysSettingsOther { get; set; } = new();
    public List<string> KeysSettingsDateTime { get; set; } = new();
    public Func<List<byte>, List<byte>>? RijndaelBytesDecrypt { get; set; }
    public Func<List<byte>, List<byte>>? RijndaelBytesEncrypt { get; set; }
}
