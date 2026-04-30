using System;
using System.Collections.Generic;
using Script.Builders;
using Script.Configuration;
using Script.Configuration.Configurations;
using WixSharp;

namespace Script
{
    // NOTE: This class is now replaced by the WPF application (App.xaml.cs and MainWindow.xaml)
    // The Main method is now in App.xaml.cs
    // This file is kept for reference/backup purposes
    internal class ProgramBackup
    {
        // Product selection - change this to build different products
        private enum ProductType
        {
            REAAssoc,
            ReCon,
            BigBlock,
            UltraBlock,
            Envirolok
        }

        // Select which product to build here
        private const ProductType SelectedProduct = ProductType.REAAssoc;
    }
}
