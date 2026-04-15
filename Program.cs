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

        // NOTE: This Main method has been moved to App.xaml.cs for WPF application
        // Uncomment this method if you want to use console mode instead of WPF
        /*
        public static void Main(string[] args)
        {
            const string version = "26.1.51";
            const string versionDate = "2026-04-14"; // Update as needed

            ProductConfiguration config;
            List<WixEntity> dataDirectories;

            // Select the appropriate configuration based on the product type
            switch (SelectedProduct)
            {
                case ProductType.REAAssoc:
                    var reaConfig = new REAAssocConfiguration(version);
                    config = reaConfig;
                    dataDirectories = reaConfig.CreateDataDirectories();
                    break;

                case ProductType.ReCon:
                    var reconConfig = new ReConConfiguration(version, versionDate);
                    config = reconConfig;
                    dataDirectories = reconConfig.CreateDataDirectories();
                    break;

                case ProductType.BigBlock:
                    var bigBlockConfig = new BigBlockConfiguration(version, versionDate);
                    config = bigBlockConfig;
                    dataDirectories = bigBlockConfig.CreateDataDirectories();
                    break;

                case ProductType.UltraBlock:
                    var ultraConfig = new UltraBlockConfiguration(version);
                    config = ultraConfig;
                    dataDirectories = ultraConfig.CreateDataDirectories();
                    break;

                case ProductType.Envirolok:
                    var envirolokConfig = new EnvirolokConfiguration(version, versionDate);
                    config = envirolokConfig;
                    dataDirectories = envirolokConfig.CreateDataDirectories();
                    break;

                default:
                    throw new ArgumentException("Invalid product type selected");
            }

            // Build the project using the selected configuration
            var builder = new ProjectBuilder(config);

            // Determine DLL source path based on product
            string dllSourcePath = GetDllSourcePath(SelectedProduct);

            // Create program files
            var programFiles = builder.CreateProgramFiles(
                config.ExecutablePath,
                config.IconPath,
                config.ProductName,
                dllSourcePath);

            // Build the final project
            var project = builder.Build(programFiles, dataDirectories);

            // Compile the MSI
            Compiler.BuildMsi(project);

            Console.WriteLine($"\nSuccessfully built installer for: {config.ProductName}");
            Console.WriteLine($"Output file: {config.OutputFileName}.msi");
            Console.ReadLine();
        }
        */

        private static string GetDllSourcePath(ProductType productType)
        {
            switch (productType)
            {
                case ProductType.REAAssoc:
                case ProductType.UltraBlock:
                    return @"E:\Programs\REA-Analysis-and-Layout\REA_Analysis\bin\Release\net48\publish";

                case ProductType.ReCon:
                case ProductType.BigBlock:
                case ProductType.Envirolok:
                    return @"E:\Programs\REA-Analysis-and-Layout (2025)\REA_Analysis\bin\Release\net48\publish";

                default:
                    return @"E:\Programs\REA-Analysis-and-Layout\REA_Analysis\bin\Release\net48\publish";
            }
        }
    }
}
