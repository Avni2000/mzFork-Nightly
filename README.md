**Progress Tracking:**

Highlights:
- Construction of Full Sequence from Base and Modifications:
  
Implemented a method to construct a full sequence by appending modifications at the correct positions in the base sequence within the MsPathFinderTResult class. 
Given Base Sequence = PEPTIDE and mods = Oxydation, 1  Methylation, 3  Phosphorylation, 5

```
FullSeqeunce = P(Oxydation)EP(Methylation)TI(Phosphorylation)DE
```

- Creating an Interface for All Result Files:
  
Introduced an interface that standardizes the structure for result files, defining properties such as:
OneBasedScanNumber, BaseSequence, FullSequence, Accession, IsDecoy, Modifications, Charge, and Mass. **Allows for consolidated set of information across search engines.**

- Creating a Method to Make a List of Said Interface in Each Class of Consensus Dataset:
  
Added methods in the HolyDatasetMST class to convert result files into a list of IResult interface implementations, which allows for unified handling and processing of different types of results across the consensus dataset.


- Creating a Unified Search Engine Handling System with Handler in Consensus Dataset:
  
Introduced the Handler class in the Readers.ConsensusDataset namespace. This class manages different subclasses such as HolyDatasetMM, HolyDatasetMST, and HolyDatasetTopPIC. It includes methods to find executable paths and initialize these subclasses, providing a unified system for handling various search engines.

Here's some cool psuedocode I drew up in notepad++ for findEXE
Obviously, the user shouldn't have to input the exepath at all (as seen in the main branch). Coming soon is a search function, here's some pseudocode I wrote out in Notepad for fun on a Friday night -- 

``` C#
FUNCTION FindExecutable(toolName, predefinedPaths, searchDirectories):

 # 1. Check predefined paths

 FOR each path IN predefinedPaths:

 IF FileExists(path + "\\" + toolName):

 RETURN path + "\\" + toolName // Found the executable



 # 2. Recursively search common directories

 FOR each directory IN searchDirectories:

 result = RecursiveSearch(directory, toolName)

 IF result IS NOT NULL:

 RETURN result // found in recursive search

 RETURN NULL // Executable not found

FUNCTION RecursiveSearch(directory, toolName):

 IF NOT DirectoryExists(directory):

 RETURN NULL // skip if directory doesn't exist

 files = GetFilesInDirectory(directory)

 FOR each file IN files:

 IF file == toolName:

 RETURN directory + "\\" + toolName // Found

 subDirs = GetSubDirectories(directory)

 FOR each subDir IN subDirs:

 result = RecursiveSearch(subDir, toolName)

 IF result IS NOT NULL:

 RETURN result

 RETURN NULL //not found in a certain dir

```

FindExecuteable is currently implemented! 

