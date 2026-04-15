using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Script.Builders;
using Script.Configuration;
using Script.Configuration.Configurations;
using WixSharp;

namespace Script
{
    public partial class MainWindow : Window
    {
        private string _lastBuiltMsiPath = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BuildButton_Click(object sender, RoutedEventArgs e)
        {
            // Disable the button during build
            BuildButton.IsEnabled = false;
            OutputTextBox.Clear();

            try
            {
                await Task.Run(() => BuildInstaller());
            }
            catch (Exception ex)
            {
                AppendOutput($"\nERROR: {ex.Message}\n{ex.StackTrace}");
                MessageBox.Show($"Build failed: {ex.Message}", "Build Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                BuildButton.IsEnabled = true;
            }
        }

        private void BuildInstaller()
        {
            string version = string.Empty;
            string versionDate = string.Empty;
            string selectedProduct = string.Empty;
            bool useSoftworkzDNA = false;

            // Get values from UI thread
            Dispatcher.Invoke(() =>
            {
                version = VersionTextBox.Text;
                versionDate = VersionDateTextBox.Text;
                selectedProduct = ((ComboBoxItem)ProductComboBox.SelectedItem).Content.ToString();
                useSoftworkzDNA = SoftworkzDNACheckBox.IsChecked == true;
            });

            AppendOutput($"Building installer for: {selectedProduct}");
            AppendOutput($"Version: {version}");
            AppendOutput($"Version Date: {versionDate}");
            AppendOutput($"SoftworkzDNA: {useSoftworkzDNA}");
            AppendOutput("----------------------------------------\n");

            ProductConfiguration config;
            List<WixEntity> dataDirectories;

            // Select the appropriate configuration based on the product type
            switch (selectedProduct)
            {
                case "REA Assoc":
                    var reaConfig = new REAAssocConfiguration(version);
                    config = reaConfig;
                    dataDirectories = reaConfig.CreateDataDirectories();
                    AppendOutput("Using REA Assoc configuration");
                    break;

                case "ReCon":
                    var reconConfig = new ReConConfiguration(version, versionDate);
                    config = reconConfig;
                    dataDirectories = reconConfig.CreateDataDirectories();
                    AppendOutput("Using ReCon configuration");
                    break;

                case "BigBlock":
                    var bigBlockConfig = new BigBlockConfiguration(version, versionDate);
                    config = bigBlockConfig;
                    dataDirectories = bigBlockConfig.CreateDataDirectories();
                    AppendOutput("Using BigBlock configuration");
                    break;

                case "UltraBlock":
                    var ultraConfig = new UltraBlockConfiguration(version);
                    config = ultraConfig;
                    dataDirectories = ultraConfig.CreateDataDirectories();
                    AppendOutput("Using UltraBlock configuration");
                    break;

                case "Envirolok":
                    var envirolokConfig = new EnvirolokConfiguration(version, versionDate);
                    config = envirolokConfig;
                    dataDirectories = envirolokConfig.CreateDataDirectories();
                    AppendOutput("Using Envirolok configuration");
                    break;

                default:
                    throw new ArgumentException("Invalid product type selected");
            }

            // Build the project using the selected configuration
            var builder = new ProjectBuilder(config);

            // Determine DLL source path based on product
            string dllSourcePath = GetDllSourcePath(selectedProduct, useSoftworkzDNA);
            AppendOutput($"DLL Source Path: {dllSourcePath}\n");

            // Create program files
            AppendOutput("Creating program files...");
            var programFiles = builder.CreateProgramFiles(
                config.ExecutablePath,
                config.IconPath,
                config.ProductName,
                dllSourcePath);

            // Build the final project
            AppendOutput("Building WiX project...");
            var project = builder.Build(programFiles, dataDirectories);

            // Set output directory to where the executable is running from
            string outputDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            project.OutDir = outputDir;

            // Compile the MSI
            AppendOutput("Compiling MSI...");
            AppendOutput($"Output directory: {outputDir}");
            Compiler.BuildMsi(project);

            // Get the full output path
            string msiFileName = config.OutputFileName + ".msi";
            string fullMsiPath = Path.Combine(outputDir, msiFileName);

            _lastBuiltMsiPath = fullMsiPath;

            AppendOutput($"\n✓ Successfully built installer for: {config.ProductName}");
            AppendOutput($"✓ Output file: {msiFileName}");
            AppendOutput($"✓ Full path: {fullMsiPath}");

            Dispatcher.Invoke(() =>
            {
                var result = MessageBox.Show(
                    $"Installer built successfully!\n\n" +
                    $"Product: {config.ProductName}\n" +
                    $"File: {msiFileName}\n" +
                    $"Location: {outputDir}\n\n" +
                    $"Would you like to open the output folder?",
                    "Build Complete", 
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    OpenOutputFolder(fullMsiPath);
                }
            });
        }

        private string GetDllSourcePath(string productType, bool useSoftworkzDNA)
        {
            // Base paths
            string basePath2024 = @"E:\Programs\REA-Analysis-and-Layout\REA_Analysis\bin\Release\net48\publish";
            string basePath2025 = @"E:\Programs\REA-Analysis-and-Layout (2025)\REA_Analysis\bin\Release\net48\publish";
            string envirolokPath = @"E:\Programs\REA-Analysis-and-Layout\REA_Analysis\bin\ReleaseEnvirolok\net48";

            // If SoftworkzDNA is enabled, you might want to use different paths
            // For now, using the same logic as original but can be customized
            switch (productType)
            {
                case "REA Assoc":
                case "UltraBlock":
                    return basePath2024;

                case "ReCon":
                case "BigBlock":
                    return basePath2025;

                case "Envirolok":
                    return envirolokPath;

                default:
                    return basePath2024;
            }
        }

        private void AppendOutput(string message)
        {
            Dispatcher.Invoke(() =>
            {
                OutputTextBox.AppendText(message + Environment.NewLine);
                OutputTextBox.ScrollToEnd();
            });
        }

        private void OpenOutputFolder(string filePath)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    // Open Explorer and select the file
                    Process.Start("explorer.exe", $"/select,\"{filePath}\"");
                }
                else
                {
                    // Just open the folder
                    string folder = Path.GetDirectoryName(filePath);
                    if (Directory.Exists(folder))
                    {
                        Process.Start("explorer.exe", folder);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open folder: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
