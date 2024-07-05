namespace SunamoPlatformUwpInterop._public.SunamoArgs;





public class CreateAppDirsIfDontExistsArgs
{
    public string AppName = "";
    
    
    
    public List<string> keysCommonSettings = new List<string>();
    
    
    
    public List<string> keysSettingsList = new List<string>();
    public List<string> keysSettingsBool = new List<string>();
    public List<string> keysSettingsOther = new List<string>();
    public Func<List<byte>, List<byte>> RijndaelBytesDecrypt;
    public Func<List<byte>, List<byte>> RijndaelBytesEncrypt;
}