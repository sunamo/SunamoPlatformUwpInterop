namespace SunamoPlatformUwpInterop;


// must be in SE, not SS - many project would reference SS only due to delegates
/// <summary>
///     For redirect to FSApps or FS - can't add
///     Return bool? due to signalling unauthorized access - for example access .xlf in uwp
/// </summary>
/// <param name="path"></param>
internal delegate bool? ExistsDirectory(string path);