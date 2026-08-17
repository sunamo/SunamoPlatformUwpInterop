namespace SunamoPlatformUwpInterop._sunamo.SunamoFileSystem;

internal class FS
{
    internal static void CreateUpfoldersPsysicallyUnlessThere(string path)
    {
        var parentDirectory = Path.GetDirectoryName(path);
        if (parentDirectory != null)
            CreateFoldersPsysicallyUnlessThere(parentDirectory);
    }

    internal static void CreateFoldersPsysicallyUnlessThere(string path)
    {
        ThrowEx.IsNullOrEmpty("path", path);

        if (Directory.Exists(path)) return;

        var foldersToCreate = new List<string>
        {
            path
        };

        var currentPath = Path.GetDirectoryName(path);
        while (currentPath != null)
        {
            // TODO: This does not work for UWP/UAP apps because they do not have access to the full disk. Investigate how to get/verify any folder on the disk in UWP/UAP.
            if (Directory.Exists(currentPath)) break;

            foldersToCreate.Add(currentPath);
            currentPath = Path.GetDirectoryName(currentPath);
        }

        foldersToCreate.Reverse();
        foreach (var item in foldersToCreate)
        {
            if (!Directory.Exists(item)) Directory.CreateDirectory(item);
        }
    }
}
