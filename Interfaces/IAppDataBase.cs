namespace SunamoPlatformUwpInterop;

public interface IAppDataBase<StorageFolder, StorageFile>
{
    string GetFileCommonSettings(string key);
    string RootFolderCommon(bool inFolderCommon);
}
