# mzFork-Nightly

**Note**: The only documentation in this ReadMe pertains solely to the ConsensusDataset project, and any further uses of it down the line.

## Brief Description

How do we validate massive amounts of search result data from three different search engines? Namely: **MSPathFinderT, MetaMorpheus, and TopPic**.  
For verification purposes, this framework tests the success of using **three MS1-only searches** (with respect to the search engines above).  
Since this builds off of **MzLib**, it is necessary to have specific paths to the executable files for **TopPic** and **MSPathFinderT**.

---

## Initializing `HolyDatasetMST`

Below is an example class for those unfamiliar with **C#** but familiar with working on MS data:

```csharp
using System;

public class UserSimulation
{
    public static void Main()
    {
        // Define file paths

        string exePath = @"C:\Program Files\Informed-Proteomics\MSPathFinderT.exe"; //MSPathFinderT executeable.
        string spectraPath = @"C:\Users\avnib\Desktop\SEOutput\RAW\Ecoli_SEC4_F6.raw";
        string datPath = @"C:\Users\avnib\Desktop\Databases\uniprotkb_proteome_UP000005640_2025_03_11.fasta";
        string outputPath = @"C:\Users\avnib\Desktop\SEOutput\MST";
        string exampleTsv = @"C:\Users\avnib\Desktop\02-17-20_jurkat_td_rep1_fract1_IcTda.tsv";

        // Initialize HolyDatasetMST
        HolyDatasetMST holyDataset = new HolyDatasetMST(exePath, spectraPath, datPath);
        // Runs MSPathFinderT locally. This is a VERY long process, but it's in the process of being optimized

        Console.WriteLine("HolyDatasetMST initialized successfully.");
    }
}

```
Obviously, the user shouldn't have to input the exepath at all. Coming soon is a search function, here's some pseudocode I wrote out in Notepad for fun-- 

```
FUNCTION FindExecutable(toolName, predefinedPaths, searchDirectories):
    # 1. Check predefined paths
    FOR each path IN predefinedPaths:
        IF FileExists(path + "\\" + toolName):
            RETURN path + "\\" + toolName  // Found the executable


    # 2. Recursively search common directories
    FOR each directory IN searchDirectories:
        result = RecursiveSearch(directory, toolName)
        IF result IS NOT NULL:
            RETURN result  // found in recursive search

    RETURN NULL  // Executable not found


FUNCTION RecursiveSearch(directory, toolName):
    IF NOT DirectoryExists(directory):
        RETURN NULL  // skip if directory doesn't exist

    files = GetFilesInDirectory(directory)
    FOR each file IN files:
        IF file == toolName:
            RETURN directory + "\\" + toolName  // Found

    subDirs = GetSubDirectories(directory)
    FOR each subDir IN subDirs:
        result = RecursiveSearch(subDir, toolName)
        IF result IS NOT NULL:
            RETURN result 

    RETURN NULL //not found in a certain dir

```

Example Usage:

```
predefinedPaths = ["C:\\Program Files\\MSPathFinderT", "C:\\Program Files (x86)\\MSPathFinderT"]
searchDirectories = ["C:\\Users\\Public\\Downloads", "C:\\Users\\Public\\Desktop", "C:\\Program Files", "C:\\Program Files (x86)"]

msPathfinderPath = FindExecutable("MSPathFinderT.exe", predefinedPaths, searchDirectories)
topPicPath = FindExecutable("TopPic.exe", ["C:\\Program Files\\TopPic"], searchDirectories)

IF msPathfinderPath IS NOT NULL:
    PRINT "MSPathFinderT found at: " + msPathfinderPath
ELSE:
    PRINT "MSPathFinderT not found."

IF topPicPath IS NOT NULL:
    PRINT "TopPic found at: " + topPicPath
ELSE:
    PRINT "TopPic not found."
```

**Features soon to come:**
- Initialize a class with a certain TopPIC executeable, perform the same action. 
- Create an SQL database with data from all 3 search engines

