# mzFork-Nightly

**Note**: The only documentation in this ReadMe pertains solely to the ConsensusDataset project, and any further uses of it down the line.


## For more up to date information, check out the readme on the develop branch

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


**Features soon to come:**

In the latest commit to develop, initialization looks something like this, but with limited functionality. 
```C#
Handler handle = new Handler(SpectraPath, DataPath, OutPath) //implements a recursive searching function, initializes constructors for HolyDatasetMST, HolyDatasetMM, HolyDatasetTopPIC.
```
From there, the idea is to call toList Methods from Each HolyDataset class, and given that each program's result file implements a certain interface (holding values like Base Sequence, Scan number, ProteinDescription, etc.)-- return a list of interfaces.
- Initialize a class with a certain TopPIC and MetaMorpheus executeable, perform the same search with identical parameters. 

- Create an SQL database with data from all 3 search engines

