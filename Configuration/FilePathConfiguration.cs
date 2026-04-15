namespace Script.Configuration
{
    /// <summary>
    /// Represents file path configurations for data files and directories.
    /// </summary>
    public class FilePathConfiguration
    {
        public string SourcePath { get; set; }
        public string DestinationFolder { get; set; }
        public string Pattern { get; set; }

        public FilePathConfiguration(string sourcePath, string destinationFolder, string pattern = "*.*")
        {
            SourcePath = sourcePath;
            DestinationFolder = destinationFolder;
            Pattern = pattern;
        }
    }
}
