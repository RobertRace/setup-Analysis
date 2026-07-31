using System;
using System.Linq;
using System.Windows;

namespace Script
{
    public partial class App : Application
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (IsMsBuildInvocation(args))
            {
                return;
            }

            var app = new App();
            app.InitializeComponent();
            app.ShutdownMode = ShutdownMode.OnMainWindowClose;
            app.Run();
        }

        private static bool IsMsBuildInvocation(string[] args)
        {
            return args != null && args.Any(a =>
                a.StartsWith("/MSBUILD", StringComparison.OrdinalIgnoreCase) ||
                a.StartsWith("/WIXBIN", StringComparison.OrdinalIgnoreCase));
        }
    }
}
