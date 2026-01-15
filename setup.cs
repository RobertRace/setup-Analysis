// be sure only one is checked //

//#if Owner
#define REAssoc
#define Softworkz

//=======================
#if Owner
#define REAssoc
#define Softworkz
#define DualUse

#define ReCon

#define ReConBlk
#define UltraBlock

#define CornerStone
#define BigBlock
#define CDPTest
#define Envirolok
#define RediRock

#define EarthWallProducts
#define UltraBlock

#define LedgeRock

#define ReCon

#define LondonBoulder
//data files
#define CornerStn
#define WestBlock
#define ReConBlk

#endif
// be sure only one is checked // ===============================

using System;

using Microsoft.Deployment.WindowsInstaller;

// Ensure that the WixSharp library is referenced in your project.  
// You can do this by installing the WixSharp NuGet package.  
// Run the following command in the Package Manager Console:  
// Install-Package WixSharp  

using WixSharp;

namespace Script
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            const string version = "26.1.9";

            var project =
#if REAssoc
                    new Project("REA Analysis",
                    new Dir(@"%ProgramFiles%\Race Engineering Associates\REA Analysis",
                    new File(@"E:\Programs\REA-Analysis-and-Layout\REA_Analysis\bin\Release\REA_Analysis.exe",
                        new FileShortcut("REA Analysis", "INSTALLDIR"),
                        new FileShortcut("REA Analysis", "%ProgramMenu%")
                        {
                            IconFile =
                                @"E:\Programs\REA-Analysis-and-Layout\REA_Analysis\Images\REAssoc.ico",
                            WorkingDirectory = "%Temp%"
                        },
                        new FileShortcut("REA Analysis", @"%Desktop%")
                        {
                            IconFile =
                                @"E:\Programs\REA-Analysis-and-Layout\REA_Analysis\Images\REAssoc.ico"
                        }),
                        new ExeFileShortcut("Uninstall REA Analysis", "[System64Folder]msiexec.exe", "/x [ProductCode]")
                        {
                            WorkingDirectory = "%Temp%"
                        },
#elif ReCon
                    new Project("ReConWall",
                    new Dir(@"%ProgramFiles%\ReCon Retaining Wall Systems\ReConWall",
                    new File(@"E:\Programs\REA-Analysis-and-Layout (2025)\REA_Analysis\bin\Release\ReCon Wall_Secure\ReCon Wall.exe",
                    new FileShortcut("ReCon Wall", "INSTALLDIR"), //INSTALLDIR is the ID of "%ProgramFiles%\My Company\My Product" 
                    new FileShortcut("ReCon Wall", @"%Desktop%")
                    {
                        IconFile = @"E:\Programs\REA-Analysis-and-Layout (2025)\REA_Analysis\Images\ReCon.ico",
                        WorkingDirectory = "%Temp%"
                    }),
                    new ExeFileShortcut("Uninstall ReCon Wall", "[System64Folder]msiexec.exe", "/x [ProductCode]")
                    {
                        WorkingDirectory = "%Temp%"
                    },
#elif BigBlock
                    new Project("BigBlock",
                    new Dir(@"%ProgramFiles%\BigBlock\BigBlock Analysis",
                    new File(@"E:\Programs\REA-Analysis-and-Layout (2025)\REA_Analysis\bin\Release\BigBlock Analysis_Secure\BigBlock Analysis.exe",
                    new FileShortcut("BigBlock Analysis", "INSTALLDIR"), //INSTALLDIR is the ID of "%ProgramFiles%\My Company\My Product" 
                    new FileShortcut("BigBlock Analysis", @"%Desktop%") { IconFile =
                        @"E:\Programs\REA-Analysis-and-Layout (2025)\REA_Analysis\Images\BigBlock.ico", WorkingDirectory = "%Temp%" }),
                    new ExeFileShortcut("Uninstall BigBlock Analysis", "[System64Folder]msiexec.exe", "/x [ProductCode]")
                        {
                            WorkingDirectory = "%Temp%"
                        },
#elif UltraBlock
            new Project("UltraWall",
                    new Dir(@"%ProgramFiles%\UltraBlock, Inc\UltraWall",
                    new File(@"E:\Programs\REA-Analysis-and-Layout (2025)\REA_Analysis\bin\Release\UltraWall_Secure\UltraWall.exe",
                    new FileShortcut("UltraWall", "INSTALLDIR"), //INSTALLDIR is the ID of "%ProgramFiles%\My Company\My Product" 
                    new FileShortcut("UltraWall", @"%Desktop%")
                    {
                        IconFile = @"E:\Programs\REA-Analysis-and-Layout (2025)\REA_Analysis\Images\Ultrablock.ico",
                        WorkingDirectory = "%Temp%"
                    }),
                    new ExeFileShortcut("Uninstall UltraWall", "[System64Folder]msiexec.exe", "/x [ProductCode]")
                    {
                        WorkingDirectory = "%Temp%"
                    },

#elif Envirolok
                new Project("Envirolok Analysis",
                    new Dir(@"%ProgramFiles%\Race Engineering Associates\Envirolok Analysis",
                        new File(@"E:\Programs\REA-Analysis-and-Layout (2025)\REA_Analysis\bin\Release\Envirolok Analysis_Secure\Envirolok Analysis.exe",
                            new FileShortcut("Envirolok Analysis", "INSTALLDIR"), //INSTALLDIR is the ID of "%ProgramFiles%\My Company\My Product" 
                            new FileShortcut("Envirolok Analysis", @"%Desktop%") { IconFile =
                         @"E:\Programs\REA-Analysis-and-Layout (2025)\REA_Analysis\Images\Envirolok.ico", WorkingDirectory = "%Temp%" }),
                        new ExeFileShortcut("Uninstall Envirolok Analysis", "[System64Folder]msiexec.exe", "/x [ProductCode]")
                        {
                            WorkingDirectory = "%Temp%"
                        },
#endif

            #region ProgramDLLs
                        new Files(@"E:\Programs\REA-Analysis-and-Layout\REA_Analysis\bin\Release\*.dll")
            #endregion
                    ),

            #region data Directory

#if REAssoc
                    new Dir(@"%PersonalFolder%\REA Wall\Data Files",
                        new Dir(@"Reinforcing",
                            new DirFiles(@"D:\OneDrive - rea-llc.com\REA Data Files\Reinforcing\*.*")
                            ),
                        new Dir(@"CornerStone 4.0",
                            new DirFiles(@"D:\OneDrive - rea-llc.com\REA Data Files\CornerStone 4.0\*.*")
                            ),
                        new Dir(@"MagnumStone 4.0",
                            new DirFiles(@"D:\OneDrive - rea-llc.com\REA Data Files\MagnumStone 4.0\*.*")
                            ),
                        new Dir(@"KeyStone 4.0",
                            new DirFiles(@"D:\OneDrive - rea-llc.com\REA Data Files\KeyStone 4.0\*.*")
                            ),
                        new Dir(@"Anchor 4.0",
                            new DirFiles(@"D:\OneDrive - rea-llc.com\REA Data Files\Anchor 4.0\*.*")
                            ),

                        new Dir(@"VertiBlock 4.0",
                            new DirFiles(@"D:\OneDrive - rea-llc.com\REA Data Files\VertiBlock 4.0\*.*")
                            ),

                         new Dir(@"Baskets",
                            new Files(@"D:\OneDrive - rea-llc.com\REA Data Files\Baskets\*.bcd")
                            ),

                        new Dir(@"ReCon 4.0",
                            new File(@"D:\OneDrive - rea-llc.com\REA Data Files\ReCon 4.0\ReCon 4.0.brd")
                            )
                        )
                    //if no blocks specified

#if CornerStn //rea folder for CornerStone products
						//data directory
						new Dir(@"MagnumStone",
							new DirFiles(@"C:\Users\Public\REAWall\REA Data Files\MagnumStone\*.*")),
						new Dir(@"CornerStone",
							new DirFiles(@"C:\Users\Public\REAWall\REA Data Files\CornerStone\*.*")
							),
#endif

#if ReConBlk
						//data directory
						new Dir(@"ReCon",
								new File(@"D:\OneDrive - rea-llc.com\REA Data Files\ReCon\ReCon SetBack.brd"),
								new File(@"D:\OneDrive - rea-llc.com\REA Data Files\ReCon\ReCon.brd")
						),
                        new Dir(@"ReCon 4.0",
                            new File(@"D:\OneDrive - rea-llc.com\REA Data Files\ReCon 4.0\ReCon 4.0.brd")
#endif

#if LondonBldr
                    new Dir(@"London Boulder",
	                    new DirFiles(@"C:\Users\Public\REAWall\REA Data Files\London Boulder\*.*")
	                    )
                    ), 
#endif

#elif ReCon
            //data directory
            new Dir(@"%PersonalFolder%\ReCon Wall",
                new File(@"D:\OneDrive - rea-llc.com\ReCon Retaining Walls\Software\Acknowledgement Acceptance of Terms of Usage and Disclaimer.rtf"),

            new Dir(@"Data Files",
                new Dir("ReCon",
            new File(@"D:\OneDrive - rea-llc.com\REA Data Files\ReCon\ReCon SetBack.brd"),
            new File(@"D:\OneDrive - rea-llc.com\REA Data Files\ReCon\ReCon.brd"),
            new File(@"D:\OneDrive - rea-llc.com\REA Data Files\ReCon\ReCon R Lipped.brd")),

            new Dir(@"ReCon 4.0",
                new File(@"D:\OneDrive - rea-llc.com\REA Data Files\ReCon 4.0\ReCon 4.0.brd")),

            new Dir("Reinforcing",
            new File(@"D:\OneDrive - rea-llc.com\REA Data Files\Reinforcing\Mirafi.bcd"),
            new File(@"D:\OneDrive - rea-llc.com\REA Data Files\Reinforcing\Stratagrid.bcd"),
            new File(@"D:\OneDrive - rea-llc.com\REA Data Files\Reinforcing\StratagridSGU.bcd"),
            new File(@"D:\OneDrive - rea-llc.com\REA Data Files\Reinforcing\Synteen.bcd"))),

            //Personal Directory
            new Dir(@"PDF Files",
            new DirFiles(@"D:\OneDrive - rea-llc.com\REA Data Files\ReCon\PDF Files\*.*"))
            )

#if NotUsedCommonAppDataFolder
                    new Dir(@"CommonAppDataFolder\ReCon Wall\Data Files",
                new File(@"D:\OneDrive - rea-llc.com\ReCon Retaining Walls\Software\Acknowledgement Acceptance of Terms of Usage and Disclaimer.rtf"),

                new Dir("ReCon",
                    new File(@"C:\Users\Public\REAWall\REA Data Files\ReCon\ReCon Channel Block.brd"),
                    new File(@"C:\Users\Public\REAWall\REA Data Files\ReCon\ReCon.brd")),

                new Dir("Reinforcing",
                    new File(@"C:\Users\Public\REAWall\REA Data Files\Reinforcing\Mirafi.bcd"),
                    new File(@"C:\Users\Public\REAWall\REA Data Files\Reinforcing\Stratagrid.bcd"),
                    new File(@"C:\Users\Public\REAWall\REA Data Files\Reinforcing\Synteen.bcd"))
            ), 
#endif

#elif BigBlock
//data directory
            new Dir(@"%PersonalFolder%\BigBlock\Data Files",
            new Dir("BigBlock",
            new Files(@"C:\Users\Public\REAWall\REA Data Files\BigBlock\BigBlock.bid")),

            new Dir("Reinforcing",
            new File(@"C:\Users\Public\REAWall\REA Data Files\Reinforcing\Mirafi.bcd"),
            new File(@"C:\Users\Public\REAWall\REA Data Files\Reinforcing\Stratagrid.bcd"),
            new File(@"C:\Users\Public\REAWall\REA Data Files\Reinforcing\Synteen.bcd"))
            ),
#elif UltraBlock
            //data directory
            //Personal Directory
            new Dir(@"%PersonalFolder%\UltraWall Files",
                new File(@"D:\OneDrive - rea-llc.com\ULTRABLOCK, INC\Acknowledgement and Acceptance of Terms of Usage and Disclaimer.rtf"),

                new Dir(@"Data Files",
                    new Dir("UltraBlock",
                    new File(@"D:\OneDrive - rea-llc.com\REA Data Files\UltraBlock\UltraBlock.bud")
                    ),

                new Dir("StoneTerra",
                    new File(@"D:\OneDrive - rea-llc.com\REA Data Files\StoneTerra\StoneTerra.bud"),
                    new File(@"D:\OneDrive - rea-llc.com\REA Data Files\StoneTerra\StoneTerra_EX.bud"),
                    new File(@"D:\OneDrive - rea-llc.com\REA Data Files\StoneTerra\StoneTerra_UC.bud")
                    ),

                new Dir("Reinforcing",
                new File(@"D:\OneDrive - rea-llc.com\REA Data Files\Reinforcing\Mirafi.bcd"),
                new File(@"D:\OneDrive - rea-llc.com\REA Data Files\Reinforcing\Stratagrid.bcd"),
                new File(@"D:\OneDrive - rea-llc.com\REA Data Files\Reinforcing\Synteen.bcd")
                )
                )
            )
#elif Envirolok
            new Dir(@"%PersonalFolder%\REA Wall\Data Files",
            new Dir("Reinforcing",
            new Files(@"D:\OneDrive - rea-llc.com\REA Data Files\Reinforcing\*.*")
                ),

            new Dir("Envirolok",
                new File(@"D:\OneDrive - rea-llc.com\REA Data Files\Envirolok\Envirolok.bed"))
            )
#endif

            #endregion
                    //new ManagedAction(CustomActions.MyAction,
                    //                  Return.ignore,
                    //                  When.After,
                    //                  Step.InstallFinalize,
                    //                  Condition.NOT_Installed)
                    )
                    {
#if REAssoc
                        GUID = new Guid("8cfd3c5d-c4e3-4cb5-8846-1a1bdc7ebbbb"),
#if DualUse
                        OutFileName = "setup REA Analysis DU " + version.Replace("24.", "2024.") + " " + versionDate,
#else
                        OutFileName = "setup_REA_Analysis_" + version.Replace("26.", "2026."),
#endif
                        LicenceFile = @"D:\OneDrive - rea-llc.com\Software\REAWall 4.0 Disclaimer (Jan 2015).rtf",

#elif CDP
                    GUID = new Guid("8cfd3c5d-c4e3-4cb5-8846-1a1bdc7ebbbb"),
						OutFileName = "setup CDP Wall " + version + " " + versionDate,
						LicenceFile = @"D:\OneDrive - rea-llc.com\Software\REAWall 4.0 Disclaimer (Jan 2015).rtf",                    

#elif ReCon
                        GUID = new Guid("320e5f9e-ba56-42c5-a267-705b60b20132"),
                        OutFileName = "setup ReCon Wall" + version + " " + versionDate,
                        LicenceFile = @"D:\OneDrive - rea-llc.com\ReCon Retaining Walls\Software\Acknowledgement Acceptance of Terms of Usage and Disclaimer.rtf",
#elif BigBlock
            GUID = new Guid("6614175a-ee49-47d6-91fc-8c1269825c73"),
            OutFileName = "setup BigBlock Analysis" + version + " " + versionDate,
            LicenceFile = @"D:\OneDrive - rea-llc.com\Software\REAWall 4.0 Disclaimer (Jan 2015).rtf",
#elif UltraBlock
                GUID = new Guid("b1b0561a-3117-421c-82a2-0b9240884bdc"),
                OutFileName = "setup UltraWall SWZ" + version + " " + versionDate,
                //LicenceFile = @"D:\OneDrive - rea - llc.com\Software\REAWall 4.0 Disclaimer(Jan 2015).rtf",
                LicenceFile = @"D:\OneDrive - rea-llc.com\ULTRABLOCK, INC\Acknowledgement and Acceptance of Terms of Usage and Disclaimer.rtf",
#elif CornerStone
            project.GUID = new Guid("3b9570bf-73ec-46e9-b47f-8877ec37bfda");
            project.OutFileName = "setup CornerStone Analysis" + version + " " + version_date;
            project.LicenceFile = @"E:\REA Engineering\REAWall 4.0 Disclaimer (Jan 2015).rtf";
#elif Envirolok
            GUID = new Guid("3b9570bf-73ec-46e9-b47f-8877ec37bfda"),
            OutFileName = "setup Envirolok Analysis" + version + " " + versionDate,
            LicenceFile = @"D:\OneDrive - rea-llc.com\Software\REAWall 4.0 Disclaimer (Jan 2015).rtf",
#elif EarthWallProducts
            GUID = new Guid("0b74a7fc-bc12-4100-b035-27370c35a15c"),
            OutFileName = "setup Earth Wall Products Analysis" + version + " " + versionDate,
            LicenceFile = @"E:\Box Sync\REA Engineering\REAWall 4.0 Disclaimer (Jan 2015).rtf",
#endif
                        Version = Version.Parse(version),
                        UI = WUI.WixUI_InstallDir,
                        ControlPanelInfo =
                    {
                    Comments = "Design MSE / Gravity Retaining Walls",
                        HelpLink = "https://REA-llc.com//REA Help",
                        HelpTelephone = "612-670-7009",
                        Contact = "Robert Race, P.E.",
                        Manufacturer = "Race Engineering Assoc, LLC",
                        InstallLocation = "[INSTALLDIR]",
                        NoModify = false
                    },
                        PreserveTempFiles = true
                    };

            Compiler.BuildMsi(project);
            Console.ReadLine();
        }
    }
}

public class CustomActions
{
    [CustomAction]
    public static ActionResult MyAction(Session session)
    {
#if REAssoc
#if CDP
		System.Diagnostics.Process.Start(session["INSTALLDIR"] + "\\" + "CDP Wall.exe");
#else
        System.Diagnostics.Process.Start(session["INSTALLDIR"] + "\\" + "REA Analysis.exe");
#endif

#elif ReCon
        System.Diagnostics.Process.Start(session["INSTALLDIR"] + "\\" + "ReCon Wall.exe");

#elif CornerStone
#elif Envirolok
            System.Diagnostics.Process.Start(session["INSTALLDIR"] + "\\" + "Envirolok Analysis.exe");
#elif EarthWallProducts
            System.Diagnostics.Process.Start(session["INSTALLDIR"] + "\\" + "Earth Wall Product Analysis.exe");
#elif BigBlock
            System.Diagnostics.Process.Start(session["INSTALLDIR"] + "\\" + "BigBlock Analysis.exe");
#elif UltraBlock
        System.Diagnostics.Process.Start(session["INSTALLDIR"] + "\\" + "UltraWall.exe");
#endif

        return ActionResult.Success;
    }
}