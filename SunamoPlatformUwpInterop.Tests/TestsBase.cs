namespace SunamoPlatformUwpInterop.Tests;

using SunamoPlatformUwpInterop._public.SunamoEnums.Enums;
using SunamoPlatformUwpInterop.AppData;
using SunamoPlatformUwpInterop.Args;

public class TestsBase
{
    public TestsBase()
    {
        AppData.ci.CreateAppFoldersIfDontExists(new CreateAppFoldersIfDontExistsArgs { AppName = "SunamoPlatformUwpInterop.Tests", keysSettingsBool = ["a.txt"] });
    }
}
