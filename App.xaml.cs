using System;
using System.Windows;

namespace Script
{
    public class App : Application
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new App();
            app.Run(new MainWindow());
        }
    }
}
