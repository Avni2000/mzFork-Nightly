using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace Readers.ExternalResults.BaseClasses //ms 2 spectra, ms1 chromatogram
{
    public interface IResult
    {
        
        /// <summary>
        /// The scan number of the identification
        /// </summary>
        int OneBasedScanNumber { get; }
        /// <summary>
        /// Primary Sequence
        /// </summary>
        string BaseSequence { get; }
        /// <summary>
        /// Modified Sequence in MetaMorpheus format
        /// </summary>
        string FullSequence { get; } //construct using base + mods
        /// <summary>
        /// The accession (unique identifier) of the identification
        /// </summary>
        string Accession { get; }
        /// <summary>
        /// If the given Spectral Match is a decoy
        /// </summary>
        bool IsDecoy {get;}
        string Modifications{ get; } //total modification as list? -> add on to base sq to construct full sequence

        int Charge {get;}

        /// <summary>
        /// theoretical mass of identified proteoform
        /// </summary>
        double Mass { get; }





    }
}
