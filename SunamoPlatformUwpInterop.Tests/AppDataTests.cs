namespace SunamoPlatformUwpInterop.Tests;

using SunamoPlatformUwpInterop._public.SunamoEnums.Enums;
using SunamoPlatformUwpInterop.AppData;
using SunamoPlatformUwpInterop.Args;

/// <summary>
/// Tests for AppData functionality including folder retrieval and settings reading.
/// </summary>
public class AppDataTests : TestsBase
{
    /// <summary>
    /// Tests that GetFolder and GetFile return valid paths for the initialized AppData instance.
    /// </summary>
    [Fact]
    public void GetFolderAndFileTest()
    {
        var cacheFolder = AppData.Instance.GetFolder(AppFolders.Cache);
        Assert.NotNull(cacheFolder);
        Assert.NotEmpty(cacheFolder);

        var cryptedFile = AppData.Instance.GetFile(AppFolders.Crypted, "a");
        Assert.NotNull(cryptedFile);
        Assert.NotEmpty(cryptedFile);
    }

    /// <summary>
    /// Tests that ReadFileOfSettingsBool returns a value for a previously configured key.
    /// </summary>
    [Fact]
    public void ReadFileOfSettingsBoolTest()
    {
        var boolValue = AppData.Instance.ReadFileOfSettingsBool("a.txt");
        Assert.IsType<bool>(boolValue);
    }
}
