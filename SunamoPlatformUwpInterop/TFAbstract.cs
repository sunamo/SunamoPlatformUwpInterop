namespace SunamoPlatformUwpInterop;

/// <summary>
/// Provides abstract text file operations for platform-independent file access.
/// </summary>
/// <typeparam name="StorageFile">The type representing a storage file.</typeparam>
public class TFAbstract<StorageFile>
{
    /// <summary>
    /// Gets or sets the function for reading all text from a storage file.
    /// </summary>
    public Func<StorageFile, string>? ReadAllText { get; set; }

    /// <summary>
    /// Gets or sets the action for writing bytes to a storage file.
    /// </summary>
    public Action<StorageFile, List<byte>>? WriteAllBytes { get; set; }

    /// <summary>
    /// Gets or sets the action for writing text to a storage file.
    /// </summary>
    public Action<StorageFile, string>? WriteAllText { get; set; }
}
