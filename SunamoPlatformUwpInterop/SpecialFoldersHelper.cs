// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoPlatformUwpInterop;

public class SpecialFoldersHelper
{
    // public static bool aspnet
    // {
    //     get
    //     {
    //         return Exc.aspnet;
    //     }
    //     set
    //     {
    //         Exc.aspnet /*= SunamoExceptions.Exc.aspnet*/ = value;
    //     }
    // }

    public static string AppDataRoaming()
    {
        string vr = null;

        //if (Exc.aspnet || VpsHelperXlf.IsVps)
        //{
        // Create junction to Administrator
        vr = @"C:\Users\Administrator\AppData\Roaming";
        //}
        //else
        //{
        //    var n = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        //    // Vracelo mi to empty string  s Environment.GetFolderPath
        //    //vr = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        //    vr = @"C:\Users\"+ Path.GetFileName(n) + @"\AppData\Roaming";
        //}

        return vr;
    }

    /// <summary>
    ///     Return root folder of AppData (as C:\Users\n\AppData\)
    /// </summary>
    public static string ApplicationData()
    {
        return Path.GetDirectoryName(AppDataRoaming());
    }
}