namespace SunamoPlatformUwpInterop;

/// <summary>
/// Represents the TF abstract storage file type.
/// </summary>
public class TFAbstract<StorageFile>
{
    /// <summary>
    /// Gets or sets the read all text.
    /// </summary>
    public Func<StorageFile, string>? ReadAllText { get; set; }

    /// <summary>
    /// Gets or sets the write all bytes.
    /// </summary>
    public Action<StorageFile, List<byte>>? WriteAllBytes { get; set; }

    /// <summary>
    /// Gets or sets the write all text.
    /// </summary>
    public Action<StorageFile, string>? WriteAllText { get; set; }
}
