// variables names: ok
namespace SunamoPlatformUwpInterop._public.SunamoEnums.Enums;

public enum AppFolders
{
    #region Not backuped

    Logs,
    Output,
    Cache,
    Temp,

    #endregion

    #region Backuped

    Input,
    Settings,
    Data,
    Other,
    Controls,
    Local,
    Roaming,
    Crypted,
    Reports,
    Backup

    #endregion
}

public static class AppFoldersHelper
{
    public static bool IsNotBackuped(AppFolders folder) =>
        folder is AppFolders.Logs or AppFolders.Output or AppFolders.Cache or AppFolders.Temp;
}
