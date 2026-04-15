using System;
using System.Collections.Generic;
using Script.Constants;
using WixSharp;

namespace Script.Configuration.Configurations
{
    /// <summary>
    /// Configuration for BigBlock Analysis product.
    /// </summary>
    public class BigBlockConfiguration : ProductConfiguration
    {
        public BigBlockConfiguration(string version, string versionDate)
        {
            ProductName = "BigBlock";
            InstallationPath = @"%ProgramFiles%\BigBlock\BigBlock Analysis";
            ExecutableName = "BigBlock Analysis.exe";
            ExecutablePath = PathConstants.BigBlockExePath;
            IconPath = PathConstants.BigBlockIconPath;
            ProductGuid = new Guid("6614175a-ee49-47d6-91fc-8c1269825c73");
            Version = version;
            LicenseFilePath = PathConstants.READisclaimerPath;
            DataFolderPath = @"%PersonalFolder%\BigBlock\Data Files";
            OutputFileName = "setup BigBlock Analysis" + version + " " + versionDate;
        }

        public List<WixEntity> CreateDataDirectories()
        {
            var dataDirectories = new List<WixEntity>();

            var dataFolder = new Dir(DataFolderPath,
                new Dir("BigBlock",
                    new Files(@"C:\Users\Public\REAWall\REA Data Files\BigBlock\BigBlock.bid")),
                new Dir("Reinforcing",
                    new File(@"C:\Users\Public\REAWall\REA Data Files\Reinforcing\Mirafi.bcd"),
                    new File(@"C:\Users\Public\REAWall\REA Data Files\Reinforcing\Stratagrid.bcd"),
                    new File(@"C:\Users\Public\REAWall\REA Data Files\Reinforcing\Synteen.bcd"))
            );

            dataDirectories.Add(dataFolder);
            return dataDirectories;
        }
    }
}
