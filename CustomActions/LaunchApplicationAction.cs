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
