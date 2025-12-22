namespace SunamoPlatformUwpInterop;

public class TFAbstract<StorageFile>
{
    public Func<StorageFile, string> readAllText;
    public Action<StorageFile, List<byte>> writeAllBytes = null;
    public Action<StorageFile, string> writeAllText = null;
}