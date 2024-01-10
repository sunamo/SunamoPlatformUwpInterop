namespace SunamoPlatformUwpInterop.AppData;


/// <summary>
///
/// </summary>
public partial class AppData
{
    public List<string> ReadFileOfSettingsList(string key)
    {
        return ReadFileOfSettingsWorker<List<string>>(loadedSettingsList, key);
    }

    /// <summary>
    /// CryptHelper.ApplyCryptData(CryptHelper.RijndaelBytes.Instance, CryptDataWrapper.rijn);
    /// Každá aplikace musí specifikovat jaké klíče používá
    /// Ty se potom načtou při startu aplikace
    /// Dodržuji tu zásadu že nic souvisejícího s nastavením se nenačítá po inicializaci
    ///
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public override string GetCommonSettings(string key, bool isCrypted = true)
    {
        if (!loadedCommonSettings.ContainsKey(key))
        {
            ThrowEx.Custom(key + " was not added into dependencies");
        }

        return loadedCommonSettings[key];


    }

    public static AppData CreateForApp(string rootFolderFromCreatedAppData, string v)
    {
        var ad = new AppData();
        ad.RootFolder = ad.GetRootFolderForApp(rootFolderFromCreatedAppData, v);
        return ad;
    }
}
