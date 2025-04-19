**Note**: This branch specifically covers some basic documentation for ConsensusDataset functionality, as I've restarted the project at https://github.com/Avni2000/mzFork/tree/Nightly/

## Consensus Dataset

This is a unified validation system for search results from: 
- **MSPathFinderT**
- **MetaMorpheus** 
- **TopPIC**

With its intended use case being validation of MS1-only search results across engines using standardized data handling for a consistent testing framework.

## IResult Interface
```csharp
public interface IResult {
+ OneBasedScanNumber 
+ BaseSequence
+ FullSequence 
+ Accession
+ IsDecoy
+ Charge
+ Mass
+ Modifications
}
```
We can easily create a list of these attributes across search results using in-built mzLib external readers.


## Basic Initialization
```csharp

var handler = new Handler(
    @"C:\data\spectra.raw", 
    @"C:\data\database.fasta"
);
```


## Configuration - What's easily modified?

    settings.toml - MetaMorpheus parameters template. While this can be changed, 
    so must the other parameters for TopPIC and MSPathFinderT in order to maintain standardization.

    Tool Executables in default locations:

    C:\Program Files\Informed-Proteomics\MSPathFinderT.exe
    C:\Program Files\TopPIC\topPIC.exe
    C:\Program Files\MetaMorpheus\CMD.exe

Implemented features

- Automatic executable path finding 
- Unified result interface (IResult)
- Cross-tool result normalization (relatively similar parameters)
- Mass modification mapping/Sequence Construction:
 ```
  Converts: Base=PEPTIDE, mods=[Oxidation@1, Phosphorylation@3]
  To: P(Oxidation)EP(Phosphorylation)TIDE.
```
