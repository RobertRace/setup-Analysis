using System;
using System.Collections.Generic;
using Script.Builders;
using Script.Constants;
using WixSharp;

namespace Script.Configuration.Configurations
{
    /// <summary>
    /// Configuration for Envirolok Analysis product.
    /// </summary>
    public class EnvirolokConfiguration : ProductConfiguration
    {
        public EnvirolokConfiguration(string version, string versionDate)
        {
            ProductName = "Envirolok Analysis";
            InstallationPath = @"%ProgramFiles%\Race Engineering Associates\Envirolok Analysis";
            ExecutableName = "Envirolok_Analysis.exe";
            ExecutablePath = PathConstants.EnvirolokExePath;
            IconPath = PathConstants.EnvirolokIconPath;
            ProductGuid = new Guid("3b9570bf-73ec-46e9-b47f-8877ec37bfda");
            Version = version;
            LicenseFilePath = PathConstants.READisclaimerPath;
            DataFolderPath = @"%PersonalFolder%\REA Wall\Data Files";
            OutputFileName = "setup_Envirolok_" + version;
        }

        public List<WixEntity> CreateDataDirectories()
        {
            var dataDirectories = new List<WixEntity>();

            var dataFolder = new Dir(DataFolderPath,
                new Dir("Reinforcing",
                    new Files(PathConstants.READataFilesPath + @"\Reinforcing\*.*")),
                new Dir("Envirolok",
                    new WixSharp.File(@"E:\OneDrive - rea-llc.com\REA Data Files\Envirolok\Envirolok.bed"))
            );

            dataDirectories.Add(dataFolder);
            return dataDirectories;
        }
    }
}
