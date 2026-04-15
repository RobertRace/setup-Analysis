using System;
using System.Collections.Generic;
using Script.Configuration;
using WixSharp;

namespace Script.Builders
{
    /// <summary>
    /// Main builder class for creating WixSharp projects.
    /// </summary>
    public class ProjectBuilder
    {
        private readonly ProductConfiguration _config;

        public ProjectBuilder(ProductConfiguration config)
        {
            _config = config;
        }

        public Project Build(List<WixEntity> programDirectoryEntities, List<WixEntity> dataDirectoryEntities)
        {
            var allEntities = new List<WixEntity>();

            // Add program directory
            var programDir = new Dir(_config.InstallationPath, programDirectoryEntities.ToArray());
            allEntities.Add(programDir);

            // Add data directories if any
            if (dataDirectoryEntities != null && dataDirectoryEntities.Count > 0)
            {
                allEntities.AddRange(dataDirectoryEntities);
            }

            var project = new Project(_config.ProductName, allEntities.ToArray())
            {
                GUID = _config.ProductGuid,
                OutFileName = _config.OutputFileName,
                LicenceFile = _config.LicenseFilePath,
                Version = Version.Parse(_config.Version),
                UI = WUI.WixUI_InstallDir,
                MajorUpgradeStrategy = MajorUpgradeStrategy.Default,
                ControlPanelInfo =
                {
                    Comments = _config.Comments,
                    HelpLink = _config.HelpLink,
                    HelpTelephone = _config.HelpTelephone,
                    Contact = _config.Contact,
                    Manufacturer = _config.Manufacturer,
                    InstallLocation = "[INSTALLDIR]",
                    NoModify = false
                },
                PreserveTempFiles = true
            };

            return project;
        }

        public List<WixEntity> CreateProgramFiles(string exePath, string iconPath, string shortcutName, string dllSourcePath)
        {
            var entities = new List<WixEntity>();

            // Main executable with shortcuts
            var exeFile = new File(exePath,
                ShortcutBuilder.CreateInstallDirShortcut(shortcutName),
                ShortcutBuilder.CreateProgramMenuShortcut(shortcutName, iconPath),
                ShortcutBuilder.CreateDesktopShortcut(shortcutName, iconPath));

            entities.Add(exeFile);

            // Uninstall shortcut
            entities.Add(ShortcutBuilder.CreateUninstallShortcut(_config.ProductName));

            // DLLs and config files
            entities.Add(new Files(dllSourcePath + @"\*.dll"));
            entities.Add(new Files(dllSourcePath + @"\*.config"));

            return entities;
        }
    }
}
