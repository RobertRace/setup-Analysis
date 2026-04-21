namespace Script.Constants
{
    /// <summary>
    /// Centralized path constants used across different product configurations.
    /// </summary>
    public static class PathConstants
    {
        // Base paths
        public const string ProgramsBasePath = @"E:\Programs\REA-Analysis-and-Layout";
        public const string ProgramsBasePath2025 = @"E:\Programs\REA-Analysis-and-Layout (2025)";
        public const string OneDriveBasePath = @"E:\OneDrive - rea-llc.com";

        // REA Analysis paths
        public const string REAAnalysisExePath = ProgramsBasePath + @"\REA_Analysis\bin\x64\Release\net48\REA_Analysis.exe";
        public const string REAAnalysisDllPath = ProgramsBasePath + @"\REA_Analysis\bin\x64\Release\net48";
        public const string REAIconPath = ProgramsBasePath + @"\REA_Analysis\Images\REAssoc.ico";

        // ReCon paths
        public const string ReConExePath = ProgramsBasePath2025 + @"\REA_Analysis\bin\x64\Release\net48\ReCon Wall.exe";
        public const string ReConIconPath = ProgramsBasePath2025 + @"\REA_Analysis\Images\ReCon.ico";

        // BigBlock paths
        public const string BigBlockExePath = ProgramsBasePath2025 + @"\REA_Analysis\bin\x64\Release\net48\BigBlock Analysis.exe";
        public const string BigBlockIconPath = ProgramsBasePath2025 + @"\REA_Analysis\Images\BigBlock.ico";

        // UltraBlock paths
        public const string UltraBlockExePath = ProgramsBasePath + @"\REA_Analysis\bin\x64\Release\net48\UltraWall.exe";
        public const string UltraBlockIconPath = ProgramsBasePath + @"\REA_Analysis\Images\Ultrablock.ico";

        // Envirolok paths
        public const string EnvirolokExePath = ProgramsBasePath + @"\REA_Analysis\bin\x64\Release\net48\Envirolok_Analysis.exe";
        public const string EnvirolokIconPath = ProgramsBasePath + @"\REA_Analysis\Images\Envirolok.ico";

        // Data file base paths
        public const string READataFilesPath = OneDriveBasePath + @"\REA Data Files";
        public const string ReConDataPath = OneDriveBasePath + @"\ReCon Retaining Walls\Software";
        public const string UltraBlockDataPath = OneDriveBasePath + @"\ULTRABLOCK, INC";

        // License/Disclaimer files
        public const string READisclaimerPath = OneDriveBasePath + @"\Shared with Everyone\Software\REAWall 4.0 Disclaimer (Jan 2015).rtf";
        public const string ReConDisclaimerPath = ReConDataPath + @"\Acknowledgement Acceptance of Terms of Usage and Disclaimer.rtf";
        public const string UltraBlockDisclaimerPath = UltraBlockDataPath + @"\Acknowledgement and Acceptance of Terms of Usage and Disclaimer.rtf";
    }
}
