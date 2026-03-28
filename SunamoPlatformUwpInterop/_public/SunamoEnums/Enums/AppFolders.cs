namespace SunamoPlatformUwpInterop._public.SunamoEnums.Enums;

/// <summary>
/// Represents the different application data folders used for organizing files.
/// </summary>
public enum AppFolders
{
    #region Not backuped

    /// <summary>
    /// Folder for application log files.
    /// </summary>
    Logs,

    /// <summary>
    /// Folder for application output files.
    /// </summary>
    Output,

    /// <summary>
    /// Folder for cached data.
    /// </summary>
    Cache,

    /// <summary>
    /// Folder for temporary files.
    /// </summary>
    Temp,

    #endregion

    #region Backuped

    /// <summary>
    /// Folder for input files.
    /// </summary>
    Input,

    /// <summary>
    /// Folder for application settings files.
    /// </summary>
    Settings,

    /// <summary>
    /// Folder for application data files.
    /// </summary>
    Data,

    /// <summary>
    /// Folder for other miscellaneous files.
    /// </summary>
    Other,

    /// <summary>
    /// Folder for control-related files.
    /// </summary>
    Controls,

    /// <summary>
    /// Folder for local application data.
    /// </summary>
    Local,

    /// <summary>
    /// Folder for roaming application data.
    /// </summary>
    Roaming,

    /// <summary>
    /// Folder for encrypted files.
    /// </summary>
    Crypted,

    /// <summary>
    /// Folder for report files.
    /// </summary>
    Reports,

    /// <summary>
    /// Folder for backup files.
    /// </summary>
    Backup

    #endregion
}