// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoPlatformUwpInterop;

public class TFAbstract<StorageFile>
{
    public Func<StorageFile, string> readAllText;
    public Action<StorageFile, List<byte>> writeAllBytes = null;
    public Action<StorageFile, string> writeAllText = null;
}