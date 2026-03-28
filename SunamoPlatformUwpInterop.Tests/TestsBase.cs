namespace SunamoPlatformUwpInterop.Tests;

using SunamoPlatformUwpInterop._public.SunamoEnums.Enums;
using SunamoPlatformUwpInterop.AppData;
using SunamoPlatformUwpInterop.Args;

/// <summary>
/// Base class for tests that initializes the AppData instance with test configuration.
/// </summary>
public class TestsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TestsBase"/> class
    /// and creates application folders with test settings.
    /// </summary>
    public TestsBase()
    {
        AppData.Instance.CreateAppFoldersIfDontExists(new CreateAppFoldersIfDontExistsArgs { AppName = "SunamoPlatformUwpInterop.Tests", KeysSettingsBool = ["a.txt"] });
    }
}
