using System;
using System.Collections.Generic;
using Script.Builders;
using Script.Constants;
using WixSharp;

namespace Script.Configuration.Configurations
{
    /// <summary>
    /// Configuration for REA Analysis product.
    /// </summary>
    public class REAAssocConfiguration : ProductConfiguration
    {
        public REAAssocConfiguration(string version)
        {
            ProductName = "REA Analysis";
            InstallationPath = @"%ProgramFiles%\Race Engineering Associates\REA Analysis";
            ExecutableName = "REA_Analysis.exe";
            ExecutablePath = PathConstants.REAAnalysisExePath;
            IconPath = PathConstants.REAIconPath;
            ProductGuid = new Guid("8cfd3c5d-c4e3-4cb5-8846-1a1bdc7ebbbb");
            Version = version;
            LicenseFilePath = PathConstants.READisclaimerPath;
            DataFolderPath = @"%PersonalFolder%\REA Wall\Data Files";
            OutputFileName = "setup_REA_Analysis_" + version.Replace("26.", "2026.");
        }

        public List<WixEntity> CreateDataDirectories()
        {
            var dataDirectories = new List<WixEntity>();

            var dataFolder = new Dir(DataFolderPath,
                DirectoryBuilder.CreateDirectoryWithPattern("Reinforcing", 
                    PathConstants.READataFilesPath + @"\Reinforcing\*.*"),
                DirectoryBuilder.CreateDirectoryWithPattern("CornerStone 4.0", 
                    PathConstants.READataFilesPath + @"\CornerStone 4.0\*.*"),
                DirectoryBuilder.CreateDirectoryWithPattern("MagnumStone 4.0", 
                    PathConstants.READataFilesPath + @"\MagnumStone 4.0\*.*"),
                DirectoryBuilder.CreateDirectoryWithPattern("KeyStone 4.0", 
                    PathConstants.READataFilesPath + @"\KeyStone 4.0\*.*"),
                DirectoryBuilder.CreateDirectoryWithPattern("Anchor 4.0", 
                    PathConstants.READataFilesPath + @"\Anchor 4.0\*.*"),
                DirectoryBuilder.CreateDirectoryWithPattern("VertiBlock 4.0", 
                    PathConstants.READataFilesPath + @"\VertiBlock 4.0\*.*"),
                new Dir("Baskets",
                    new Files(PathConstants.READataFilesPath + @"\Baskets\*.bcd")),
                new Dir("ReCon 4.0",
                    new File(PathConstants.READataFilesPath + @"\ReCon 4.0\ReCon 4.0.brd"))
            );

            dataDirectories.Add(dataFolder);
            return dataDirectories;
        }
    }
}
