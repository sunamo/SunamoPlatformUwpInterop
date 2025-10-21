// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace RunnerPlatformUwpInterop;

using Microsoft.Extensions.DependencyInjection;
using SunamoPlatformUwpInterop.AppData;
using SunamoPlatformUwpInterop.Tests;

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
        AppData.ci.CreateAppFoldersIfDontExists(new SunamoPlatformUwpInterop.Args.CreateAppFoldersIfDontExistsArgs() { AppName = appName, keysSettingsDateTime = [dtLastDt], keysSettingsBool = [boolean], keysSettingsList = [list], keysSettingsOther = [other] });

        await AppData.ci.SaveFileOfSettingsDateTime(dtLastDt, DateTime.MaxValue);
        await AppData.ci.SaveFileOfSettings(other, "Other2");
        await AppData.ci.SaveFileOfSettingsBool(boolean, true);
        await AppData.ci.SaveFileOfSettingsList(list, ["1", "2"]);

        var dt = AppData.ci.ReadFileOfSettingsDateTime(dtLastDt);
        var builder = AppData.ci.ReadFileOfSettingsBool(boolean);
        var o = AppData.ci.ReadFileOfSettingsOther(other);
        var list = AppData.ci.ReadFileOfSettingsList(list);
    }
}