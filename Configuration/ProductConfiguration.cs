using System;

namespace Script.Configuration
{
    /// <summary>
    /// Represents the configuration for a product installation.
    /// </summary>
    public class ProductConfiguration
    {
        public string ProductName { get; set; }
        public string InstallationPath { get; set; }
        public string ExecutableName { get; set; }
        public string ExecutablePath { get; set; }
        public string IconPath { get; set; }
        public Guid ProductGuid { get; set; }
        public string Version { get; set; }
        public string LicenseFilePath { get; set; }
        public string DataFolderPath { get; set; }
        public string OutputFileName { get; set; }

        // Control Panel Info
        public string Comments { get; set; }
        public string HelpLink { get; set; }
        public string HelpTelephone { get; set; }
        public string Contact { get; set; }
        public string Manufacturer { get; set; }

        public ProductConfiguration()
        {
            // Default values
            Comments = "Design MSE / Gravity Retaining Walls";
            HelpLink = "https://REA-llc.com//REA Help";
            HelpTelephone = "612-670-7009";
            Contact = "Robert Race, P.E.";
            Manufacturer = "Race Engineering Assoc, LLC";
        }
    }
}
