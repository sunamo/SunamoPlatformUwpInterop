// variables names: ok
namespace SunamoPlatformUwpInterop._public.SunamoEnums.Enums;

public enum AppFolders
{
    #region Not backuped

    Logs,
    Output,
    Reports,
    Backup,

    #endregion

    #region Backuped

    Input,
    Settings,
    Data,
    Controls,
    Crypted

    #endregion
}

public static class AppFoldersHelper
{
    public static bool IsNotBackuped(AppFolders folder) =>
        folder is AppFolders.Logs or AppFolders.Output or AppFolders.Reports or AppFolders.Backup;
}
