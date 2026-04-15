using System.Collections.Generic;
using WixSharp;

namespace Script.Builders
{
    /// <summary>
    /// Helper class for building directory structures.
    /// </summary>
    public static class DirectoryBuilder
    {
        public static Dir CreateDataDirectory(string basePath, Dictionary<string, string> dataFolders)
        {
            var subDirs = new List<WixEntity>();

            foreach (var folder in dataFolders)
            {
                subDirs.Add(new Dir(folder.Key, new DirFiles(folder.Value)));
            }

            return new Dir(basePath, subDirs.ToArray());
        }

        public static Dir CreateDirectoryWithFiles(string folderName, params string[] filePaths)
        {
            var files = new List<WixEntity>();

            foreach (var filePath in filePaths)
            {
                files.Add(new File(filePath));
            }

            return new Dir(folderName, files.ToArray());
        }

        public static Dir CreateDirectoryWithPattern(string folderName, string sourcePath)
        {
            return new Dir(folderName, new DirFiles(sourcePath));
        }
    }
}
