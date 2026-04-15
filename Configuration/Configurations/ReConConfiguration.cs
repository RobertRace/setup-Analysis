using System;
using System.Collections.Generic;
using Script.Builders;
using Script.Constants;
using WixSharp;

namespace Script.Configuration.Configurations
{
    /// <summary>
    /// Configuration for ReCon Wall product.
    /// </summary>
    public class ReConConfiguration : ProductConfiguration
    {
        public ReConConfiguration(string version, string versionDate)
        {
            ProductName = "ReConWall";
            InstallationPath = @"%ProgramFiles%\ReCon Retaining Wall Systems\ReConWall";
            ExecutableName = "ReCon Wall.exe";
            ExecutablePath = PathConstants.ReConExePath;
            IconPath = PathConstants.ReConIconPath;
            ProductGuid = new Guid("320e5f9e-ba56-42c5-a267-705b60b20132");
            Version = version;
            LicenseFilePath = PathConstants.ReConDisclaimerPath;
            DataFolderPath = @"%PersonalFolder%\ReCon Wall";
            OutputFileName = "setup ReCon Wall" + version + " " + versionDate;
        }

        public List<WixEntity> CreateDataDirectories()
        {
            var dataDirectories = new List<WixEntity>();

            var dataFolder = new Dir(DataFolderPath,
                new File(PathConstants.ReConDisclaimerPath),
                new Dir("Data Files",
                    new Dir("ReCon",
                        new File(PathConstants.READataFilesPath + @"\ReCon\ReCon SetBack.brd"),
                        new File(PathConstants.READataFilesPath + @"\ReCon\ReCon.brd"),
                        new File(PathConstants.READataFilesPath + @"\ReCon\ReCon R Lipped.brd")),
                    new Dir("ReCon 4.0",
                        new File(PathConstants.READataFilesPath + @"\ReCon 4.0\ReCon 4.0.brd")),
                    new Dir("Reinforcing",
                        new File(PathConstants.READataFilesPath + @"\Reinforcing\Mirafi.bcd"),
                        new File(PathConstants.READataFilesPath + @"\Reinforcing\Stratagrid.bcd"),
                        new File(PathConstants.READataFilesPath + @"\Reinforcing\StratagridSGU.bcd"),
                        new File(PathConstants.READataFilesPath + @"\Reinforcing\Synteen.bcd"))),
                new Dir("PDF Files",
                    new DirFiles(PathConstants.READataFilesPath + @"\ReCon\PDF Files\*.*"))
            );

            dataDirectories.Add(dataFolder);
            return dataDirectories;
        }
    }
}
