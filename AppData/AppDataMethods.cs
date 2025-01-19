namespace SunamoPlatformUwpInterop.AppData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AppDataMethods
{
    public static string GetFileData(string fn)
    {
        return AppData.ci.GetFile(AppFolders.Data, fn);
    }

    public static string GetFileSettings(string fn)
    {
        return AppData.ci.GetFile(AppFolders.Settings, fn);
    }
}