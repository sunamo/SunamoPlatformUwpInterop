namespace SunamoPlatformUwpInterop._public.SunamoEnums.Enums;

/// <summary>
/// Represents the app folders type.
/// </summary>
public enum AppFolders
{
    #region Not backuped

    /// <summary>
    /// The logs value.
    /// </summary>
    Logs,
    /// <summary>
    /// The output value.
    /// </summary>
    Output,
    /// <summary>
    /// The reports value.
    /// </summary>
    Reports,
    /// <summary>
    /// The backup value.
    /// </summary>
    Backup,

    #endregion

    #region Backuped

    /// <summary>
    /// The input value.
    /// </summary>
    Input,
    /// <summary>
    /// The settings value.
    /// </summary>
    Settings,
    /// <summary>
    /// The data value.
    /// </summary>
    Data,
    /// <summary>
    /// The controls value.
    /// </summary>
    Controls,
    /// <summary>
    /// The crypted value.
    /// </summary>
    Crypted

    #endregion
}

/// <summary>
/// Represents the app folders helper type.
/// </summary>
public static class AppFoldersHelper
{
    /// <summary>
    /// Is not backuped.
    /// </summary>
    /// <param name="folder">The folder.</param>
    /// <returns>The is not backuped result.</returns>
    public static bool IsNotBackuped(AppFolders folder) =>
        folder is AppFolders.Logs or AppFolders.Output or AppFolders.Reports or AppFolders.Backup;
}
