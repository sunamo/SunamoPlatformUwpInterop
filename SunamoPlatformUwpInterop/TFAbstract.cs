namespace SunamoPlatformUwpInterop;

public class TFAbstract<StorageFile>
{
    public Func<StorageFile, string>? ReadAllText { get; set; }

    public Action<StorageFile, List<byte>>? WriteAllBytes { get; set; }

    public Action<StorageFile, string>? WriteAllText { get; set; }
}
