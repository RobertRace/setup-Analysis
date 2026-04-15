using System;
using System.Collections.Generic;
using Script.Builders;
using Script.Constants;
using WixSharp;

namespace Script.Configuration.Configurations
{
    /// <summary>
    /// Configuration for UltraWall product.
    /// </summary>
    public class UltraBlockConfiguration : ProductConfiguration
    {
        public UltraBlockConfiguration(string version)
        {
            ProductName = "UltraWall";
            InstallationPath = @"%ProgramFiles%\UltraBlock, Inc\UltraWall";
            ExecutableName = "UltraWall.exe";
            ExecutablePath = PathConstants.UltraBlockExePath;
            IconPath = PathConstants.UltraBlockIconPath;
            ProductGuid = new Guid("b1b0561a-3117-421c-82a2-0b9240884bdc");
            Version = version;
            LicenseFilePath = PathConstants.UltraBlockDisclaimerPath;
            DataFolderPath = @"%PersonalFolder%\UltraWall Files";
            OutputFileName = "setup_UltraWall_" + version;
        }

        public List<WixEntity> CreateDataDirectories()
        {
            var dataDirectories = new List<WixEntity>();

            var dataFolder = new Dir(DataFolderPath,
                new File(PathConstants.UltraBlockDisclaimerPath),
                new Dir("Data Files",
                    new Dir("UltraBlock",
                        new File(PathConstants.READataFilesPath + @"\UltraBlock\UltraBlock.bud")),
                    new Dir("StoneTerra",
                        new File(PathConstants.READataFilesPath + @"\StoneTerra\StoneTerra.bud"),
                        new File(PathConstants.READataFilesPath + @"\StoneTerra\StoneTerra_EX.bud"),
                        new File(PathConstants.READataFilesPath + @"\StoneTerra\StoneTerra_UC.bud")),
                    new Dir("Reinforcing",
                        new File(PathConstants.READataFilesPath + @"\Reinforcing\Mirafi.bcd"),
                        new File(PathConstants.READataFilesPath + @"\Reinforcing\Stratagrid.bcd"),
                        new File(PathConstants.READataFilesPath + @"\Reinforcing\Synteen.bcd"))
                )
            );

            dataDirectories.Add(dataFolder);
            return dataDirectories;
        }
    }
}
