namespace RunnerPlatformUwpInterop;

using Microsoft.Extensions.DependencyInjection;
using SunamoPlatformUwpInterop.AppData;
using SunamoPlatformUwpInterop.Tests;

/// <summary>
/// Runner program for testing AppData functionality.
/// </summary>
internal class Program
{
    static ServiceCollection Services = new();
    const string appName = "RunnerPlatformUwpInterop";
    const string dtLastDt = "LastDt";
    const string other = "other";
    const string boolean = "boolean";
    const string list = "list";


    static void Main()
    {
        MainAsync().GetAwaiter().GetResult();
    }

    static async Task MainAsync()
    {
        AppData.Instance.CreateAppFoldersIfDontExists(new SunamoPlatformUwpInterop.Args.CreateAppFoldersIfDontExistsArgs() { AppName = appName, KeysSettingsDateTime = [dtLastDt], KeysSettingsBool = [boolean], KeysSettingsList = [list], KeysSettingsOther = [other] });

        await AppData.Instance.SaveFileOfSettingsDateTime(dtLastDt, DateTime.MaxValue);
        await AppData.Instance.SaveFileOfSettings(other, "Other2");
        await AppData.Instance.SaveFileOfSettingsBool(boolean, true);
        await AppData.Instance.SaveFileOfSettingsList(list, ["1", "2"]);

        var dt = AppData.Instance.ReadFileOfSettingsDateTime(dtLastDt);
        var builder = AppData.Instance.ReadFileOfSettingsBool(boolean);
        var o = AppData.Instance.ReadFileOfSettingsOther(other);
        var listResult = AppData.Instance.ReadFileOfSettingsList(list);
    }
}
