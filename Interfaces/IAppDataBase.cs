namespace SunamoPlatformUwpInterop.Interfaces;

public interface IAppDataBase<StorageFolder, StorageFile>
{
    string GetFileCommonSettings(string key);
    string RootFolderCommon(bool inFolderCommon);
}
