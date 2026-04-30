using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
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
        private const string ReaWallIniPath = @"E:\OneDrive - rea-llc.com\Documents\REA Wall\REAWall.ini";
        private volatile bool _isClosing;
        private string _lastBuiltMsiPath = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            LoadLastBuildSettings();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            _isClosing = true;
            Application.Current.Shutdown();
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
                if (!_isClosing && !Dispatcher.HasShutdownStarted && !Dispatcher.HasShutdownFinished)
                {
                    AppendOutput($"\nERROR: {ex.Message}\n{ex.StackTrace}");
                    MessageBox.Show($"Build failed: {ex.Message}", "Build Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            finally
            {
                if (!_isClosing && !Dispatcher.HasShutdownStarted && !Dispatcher.HasShutdownFinished)
                {
                    BuildButton.IsEnabled = true;
                }
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

            SaveLastBuildSettings(version, versionDate, selectedProduct, useSoftworkzDNA);

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

            // Validate x64 input paths before building
            if (!System.IO.File.Exists(config.ExecutablePath))
            {
                throw new FileNotFoundException("Executable not found. Build/publish the app first.", config.ExecutablePath);
            }

            if (!Directory.Exists(dllSourcePath))
            {
                throw new DirectoryNotFoundException("DLL source folder not found: " + dllSourcePath);
            }

            EnsureBinaryIsX64(config.ExecutablePath, selectedProduct + " executable");

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

            if (_isClosing || Dispatcher.HasShutdownStarted || Dispatcher.HasShutdownFinished)
            {
                return;
            }

            Dispatcher.Invoke(() =>
            {
                if (_isClosing || Dispatcher.HasShutdownStarted || Dispatcher.HasShutdownFinished)
                {
                    return;
                }

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
            string basePath2024 = @"E:\Programs\REA-Analysis-and-Layout\REA_Analysis\bin\x64\Release\net48";
            string basePath2025 = @"E:\Programs\REA-Analysis-and-Layout (2025)\REA_Analysis\bin\x64\Release\net48";
            string envirolokPath = @"E:\Programs\REA-Analysis-and-Layout\REA_Analysis\bin\x64\Release\net48";

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

        private static void EnsureBinaryIsX64(string filePath, string description)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new BinaryReader(stream))
            {
                stream.Seek(0x3C, SeekOrigin.Begin);
                int peHeaderOffset = reader.ReadInt32();
                stream.Seek(peHeaderOffset + 4, SeekOrigin.Begin);
                ushort machine = reader.ReadUInt16();

                // IMAGE_FILE_MACHINE_AMD64 = 0x8664
                if (machine != 0x8664)
                {
                    throw new InvalidOperationException($"{description} is not x64. Found machine type 0x{machine:X4} at: {filePath}");
                }
            }
        }

        private void LoadLastBuildSettings()
        {
            try
            {
                if (!System.IO.File.Exists(ReaWallIniPath))
                {
                    return;
                }

                var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (var line in System.IO.File.ReadAllLines(ReaWallIniPath))
                {
                    var trimmed = line.Trim();
                    if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith(";") || !trimmed.Contains("="))
                    {
                        continue;
                    }

                    int idx = trimmed.IndexOf('=');
                    var key = trimmed.Substring(0, idx).Trim();
                    var value = trimmed.Substring(idx + 1).Trim();
                    values[key] = value;
                }

                if (values.TryGetValue("Version", out var version) && !string.IsNullOrWhiteSpace(version))
                {
                    VersionTextBox.Text = version;
                }

                if (values.TryGetValue("VersionDate", out var versionDate) && !string.IsNullOrWhiteSpace(versionDate))
                {
                    VersionDateTextBox.Text = versionDate;
                }

                if (values.TryGetValue("Product", out var product) && !string.IsNullOrWhiteSpace(product))
                {
                    foreach (ComboBoxItem item in ProductComboBox.Items)
                    {
                        if (string.Equals(item.Content?.ToString(), product, StringComparison.OrdinalIgnoreCase))
                        {
                            ProductComboBox.SelectedItem = item;
                            break;
                        }
                    }
                }

                if (values.TryGetValue("SoftworkzDNA", out var softworkzDna) && bool.TryParse(softworkzDna, out var isChecked))
                {
                    SoftworkzDNACheckBox.IsChecked = isChecked;
                }
            }
            catch
            {
                // Ignore persistence errors to keep UI usable.
            }
        }

        private void SaveLastBuildSettings(string version, string versionDate, string product, bool softworkzDna)
        {
            try
            {
                var directory = Path.GetDirectoryName(ReaWallIniPath);
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var content = new StringBuilder()
                    .AppendLine("[REAWall]")
                    .AppendLine("Version=" + version)
                    .AppendLine("VersionDate=" + versionDate)
                    .AppendLine("Product=" + product)
                    .AppendLine("SoftworkzDNA=" + softworkzDna)
                    .ToString();

                System.IO.File.WriteAllText(ReaWallIniPath, content);
            }
            catch
            {
                // Ignore persistence errors to keep build flow working.
            }
        }

        private void AppendOutput(string message)
        {
            if (_isClosing || Dispatcher.HasShutdownStarted || Dispatcher.HasShutdownFinished)
            {
                return;
            }

            Dispatcher.Invoke(() =>
            {
                if (_isClosing || Dispatcher.HasShutdownStarted || Dispatcher.HasShutdownFinished)
                {
                    return;
                }

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
