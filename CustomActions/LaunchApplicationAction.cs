using System;
using Microsoft.Deployment.WindowsInstaller;

namespace Script.CustomActions
{
    /// <summary>
    /// Custom actions for WiX installer.
    /// </summary>
    public class LaunchApplicationAction
    {
        [CustomAction]
        public static ActionResult LaunchApplication(Session session)
        {
            var launchProp = session["LAUNCHAPP"] ?? string.Empty;
            if (!string.Equals(launchProp, "1", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(launchProp, "true", StringComparison.OrdinalIgnoreCase))
            {
                return ActionResult.Success;
            }

            var installDir = session["INSTALLDIR"];
            var executableName = session["EXECUTABLE_NAME"];

            if (!string.IsNullOrEmpty(installDir) && !string.IsNullOrEmpty(executableName))
            {
                var executablePath = System.IO.Path.Combine(installDir, executableName);

                if (System.IO.File.Exists(executablePath))
                {
                    System.Diagnostics.Process.Start(executablePath);
                }
            }

            return ActionResult.Success;
        }
    }
}
