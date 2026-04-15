using WixSharp;

namespace Script.Builders
{
    /// <summary>
    /// Helper class for building shortcuts.
    /// </summary>
    public static class ShortcutBuilder
    {
        public static FileShortcut CreateDesktopShortcut(string name, string iconPath, string workingDirectory = "%Temp%")
        {
            return new FileShortcut(name, @"%Desktop%")
            {
                IconFile = iconPath,
                WorkingDirectory = workingDirectory
            };
        }

        public static FileShortcut CreateProgramMenuShortcut(string name, string iconPath, string workingDirectory = "%Temp%")
        {
            return new FileShortcut(name, "%ProgramMenu%")
            {
                IconFile = iconPath,
                WorkingDirectory = workingDirectory
            };
        }

        public static FileShortcut CreateInstallDirShortcut(string name)
        {
            return new FileShortcut(name, "INSTALLDIR");
        }

        public static ExeFileShortcut CreateUninstallShortcut(string productName)
        {
            return new ExeFileShortcut($"Uninstall {productName}", "[System64Folder]msiexec.exe", "/x [ProductCode]")
            {
                WorkingDirectory = "%Temp%"
            };
        }
    }
}
