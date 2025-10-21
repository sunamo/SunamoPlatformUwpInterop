// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoPlatformUwpInterop.Args;

public class CreateAppFoldersIfDontExistsArgs
{
    public string AppName = "";


    public List<string> keysCommonSettings = new();
    public List<string> keysSettingsBool = new();


    public List<string> keysSettingsList = new();
    public List<string> keysSettingsOther = new();
    public List<string> keysSettingsDateTime = new();
    public Func<List<byte>, List<byte>> RijndaelBytesDecrypt;
    public Func<List<byte>, List<byte>> RijndaelBytesEncrypt;
}