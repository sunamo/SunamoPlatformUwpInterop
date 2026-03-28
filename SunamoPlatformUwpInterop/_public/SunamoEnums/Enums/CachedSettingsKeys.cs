namespace SunamoPlatformUwpInterop._public.SunamoEnums.Enums;

/// <summary>
/// Keys for cached settings stored in the application data folder.
/// </summary>
public enum CachedSettingsKeys
{
    /// <summary>
    /// Path to the Visual Studio folder containing project folders.
    /// </summary>
    vsFolderWithProjectsFolders,

    /// <summary>
    /// Path to the Sunamo parent application.
    /// </summary>
    paSunamo,

    /// <summary>
    /// Path to the Sunamo parent application on the VPS server.
    /// </summary>
    paSunamoOnVps,

    /// <summary>
    /// List of applications in the Sunamo ecosystem.
    /// </summary>
    AppsInSunamo,

    /// <summary>
    /// List of applications in the Sunamo ecosystem on the VPS server.
    /// </summary>
    AppsInSunamoOnVps
}