using CsvHelper;
using Readers.ExternalResults.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readers.ConsensusDataset
{
    class Handler
    {
        public Handler(string spectraPath, string dataPath, string outPath)
        {
            HolyDatasetMST MST = new HolyDatasetMST(FindExePath("MSPathFinderT.exe"), spectraPath, dataPath); //ALWAYS LEAVE OUT PATH EMPTY.
            HolyDatasetTopPIC Toppic = new HolyDatasetTopPIC(FindExePath("TopPIC.exe"), spectraPath, dataPath); //TODO 
             
            //   HolyDatasetMM MM = new HolyDatasetMM(FindExePath("CMD.exe"), spectraPath, dataPath);
        }
        public string FindExePath(string toolName)
        {
            switch (toolName)
            {
                case "MSPathFinderT.exe":
                    string[] predefinedPath =
                        { @"C:\Program Files\Informed-Proteomics\", @"C:\Program Files (x86)\Informed-Proteomics\", };
                    string[] searchDir =
                    {
                        @"C:\Program Files (x86)", @"C:\Program Files",
                    };
                    foreach (string path in predefinedPath)
                    {
                        if (File.Exists(path + @"\" + toolName))
                        {
                            return path + @"\" + toolName;

                        }
                    }

                    foreach (string search in searchDir)
                    {
                                string[] files = Directory.GetFiles(search, toolName, SearchOption.AllDirectories);
                                if (files.Length > 0)
                                {
                                    return @"" + files[0];
                                }
                    }
                    break;
            
                    

                case "TopPIC.exe":
                    predefinedPath = new string[]
                    {
                        @"C:\Program Files\TopPIC\", @"C:\Program Files (x86)\TopPIC\",
                    }; //TODO check after starting TopPIC.
                    searchDir = new string[]
                    {
                        @"C:\Program Files (x86)", @"C:\Program Files",
                    };
                    foreach (string path in predefinedPath)
                    {
                        if (File.Exists(path + @"\" + toolName))
                        {
                            return path + @"\" + toolName;

                        }
                    }

                    foreach (string search in searchDir)
                    {
                        string[] files = Directory.GetFiles(search, toolName, SearchOption.AllDirectories);
                            if (files.Length > 0)
                            {
                                return @"" + files[0];
                            }
                    }

                    

                    break;

                case "CMD.exe":
                    predefinedPath = new string[]
                    {
                        @"C:\Program Files\MetaMorpheus\", @"C:\Program Files (x86)\MetaMorpheus\"
                    };
                    searchDir = new string[]
                    {
                        @"C:\Program Files (x86)", @"C:\Program Files",
                    };
                    foreach (string path in predefinedPath)
                    {
                        if (File.Exists(path + @"\" + toolName))
                        {
                            return path + @"\" + toolName;

                        }
                    }

                    foreach (string search in searchDir)
                    {
                        string[] files = Directory.GetFiles(search, toolName, SearchOption.AllDirectories);
                        if (files.Length > 0)
                        {
                            return @"" + files[0];
                        }
                    }
                    break;
            }
            return null;
        }
    }
}
