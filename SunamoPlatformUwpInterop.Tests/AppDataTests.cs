namespace SunamoPlatformUwpInterop.Tests;

using SunamoPlatformUwpInterop._public.SunamoEnums.Enums;
using SunamoPlatformUwpInterop.AppData;
using SunamoPlatformUwpInterop.Args;

public class AppDataTests : TestsBase //: ProgramShared
{


    [Fact]
    public void Test1()
    {
        /*
         * Nevím jestli je zde správné užívat ThisApp
         * SunamoThisApp existuje a bude existovat jako SunamoPlatformUwpInterop
         * 
         * Oba projekty mají svůj vlastní účel:
         * SunamoThisApp - Třída ThisApp
         * SunamoPlatformUwpInterop - AppData a jiné metody které mají ac
         * 
         * 
         */
        //ThisApp.Name = "Test";
        //CreatePathToFiles(AppData.AppData.ci.GetFileString);



        var d = AppData.ci.GetFolder(AppFolders.Cache);
        var f = AppData.ci.GetFile(AppFolders.Crypted, "a");
    }

    [Fact]
    public void ReadFileOfSettingsListTest()
    {
        var c = AppData.ci.ReadFileOfSettingsBool("a.txt");
    }
}
