using System;
using System.Collections.Generic;
using System.IO;
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
                Platform = Platform.x64,
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

            var exeName = Path.GetFileName(exePath);
            var shortcutWorkingDirectory = string.Equals(exeName, "ProgramDataUI.exe", StringComparison.OrdinalIgnoreCase)
                ? "INSTALLDIR"
                : "%Temp%";

            // Main executable with shortcuts
            var exeFile = new WixSharp.File(exePath,
                ShortcutBuilder.CreateInstallDirShortcut(shortcutName),
                ShortcutBuilder.CreateProgramMenuShortcut(shortcutName, iconPath, shortcutWorkingDirectory),
                ShortcutBuilder.CreateDesktopShortcut(shortcutName, iconPath, shortcutWorkingDirectory));

            entities.Add(exeFile);

            // Uninstall shortcut
            entities.Add(ShortcutBuilder.CreateUninstallShortcut(_config.ProductName));

            // Include full .NET app payload (deps/runtimeconfig/resources/runtimes/etc.)
            entities.AddRange(CreateRootFileEntities(dllSourcePath, exePath));
            entities.AddRange(CreateSubdirectoryEntities(dllSourcePath));

            return entities;
        }

        private static List<WixEntity> CreateRootFileEntities(string sourceDirectory, string excludedExecutablePath)
        {
            var entities = new List<WixEntity>();
            var excludedExecutableName = Path.GetFileName(excludedExecutablePath);

            foreach (var filePath in Directory.GetFiles(sourceDirectory))
            {
                if (string.Equals(Path.GetFileName(filePath), excludedExecutableName, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (string.Equals(Path.GetExtension(filePath), ".pdb", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                entities.Add(new WixSharp.File(filePath));
            }

            return entities;
        }

        private static List<WixEntity> CreateSubdirectoryEntities(string sourceDirectory)
        {
            var entities = new List<WixEntity>();

            foreach (var directory in Directory.GetDirectories(sourceDirectory))
            {
                entities.Add(CreateDirectoryEntity(directory));
            }

            return entities;
        }

        private static Dir CreateDirectoryEntity(string sourceDirectory)
        {
            var children = new List<WixEntity>();

            foreach (var filePath in Directory.GetFiles(sourceDirectory))
            {
                if (string.Equals(Path.GetExtension(filePath), ".pdb", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                children.Add(new WixSharp.File(filePath));
            }

            foreach (var directory in Directory.GetDirectories(sourceDirectory))
            {
                children.Add(CreateDirectoryEntity(directory));
            }

            return new Dir(Path.GetFileName(sourceDirectory), children.ToArray());
        }
    }
}
