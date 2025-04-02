using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chemistry;
using Easy.Common.Extensions;
using Proteomics.AminoAcidPolymer;
using Readers.ExternalResults.BaseClasses; //imported interface

namespace Readers
{
    public class MsPathFinderTResult:IResult
    {
        public static CsvConfiguration CsvConfiguration { get; } = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
        {
            Delimiter = "\t",
            HasHeaderRecord = true,
            IgnoreBlankLines = true,
            TrimOptions = CsvHelper.Configuration.TrimOptions.InsideQuotes,
            BadDataFound = null,
        };


        [Name("Scan")]
        public int OneBasedScanNumber { get; set; }

        [Name("Pre")]
        public char PreviousResidue { get; set; }

        [Name("Sequence")]
        public string BaseSequence { get; set; }

        [Name("Post")]
        public char NextResidue { get; set; }

        [Name("Modifications")]
        public string? Modifications { get; set; } //declared nullable considering blank mods.

        [Name("Composition")]
        [TypeConverter(typeof(MsPathFinderTCompositionToChemicalFormulaConverter))]
        public ChemicalFormula ChemicalFormula { get; set; }

        [Name("ProteinName")]
        public string ProteinName { get; set; }

        [Name("ProteinDesc")]
        public string ProteinDescription { get; set; }

        [Name("ProteinLength")]
        public int Length { get; set; }

        [Name("Start")]
        public int OneBasedStartResidue { get; set; }

        [Name("End")]
        public int OneBasedEndResidue { get; set; }

        [Name("Charge")]
        public int Charge { get; set; }

        [Name("MostAbundantIsotopeMz")]
        public double MostAbundantIsotopeMz { get; set; }

        [Name("Mass")]
        public double Mass { get; set; } //called it mass for IResult

        [Name("Ms1Features")]
        public int Ms1Features { get; set; }

        [Name("#MatchedFragments")]
        public int NumberOfMatchedFragments { get; set; }

        [Name("Probability")]
        public double Probability { get; set; }

        [Name("SpecEValue")]
        public double SpecEValue { get; set; }

        [Name("EValue")]
        public double EValue { get; set; }

        [Name("QValue")]
        [Optional]
        public double QValue { get; set; }

        [Name("PepQValue")]
        [Optional]
        public double PepQValue { get; set; }

        


        #region InterpretedFields

        [Ignore] private string _accession = null;
        [Ignore] public string Accession => _accession ??= ProteinName.Split('|')[1].Trim();

        [Ignore] private bool? _isDecoy = null;
        [Ignore] public bool IsDecoy => _isDecoy ??= ProteinName.StartsWith("XXX");
        [Optional] public string FileNameWithoutExtension { get; set; }
        
        public string FullSequence // Append modifications at the correct positions in the base sequence
        {
            get
            {
                string baseSequence = BaseSequence;
                string[] delimiters = { ",", " " };
                string mods = Modifications;
                if (mods.IsNullOrEmpty())
                {return BaseSequence;}

                
                string[] result = mods.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<int, string> dict = new Dictionary<int, string>();

                for (int i = 0; i < result.Length; i += 2)
                {
                    if (int.TryParse(result[i + 1], out int position))
                    {
                        dict[position] = "(" + result[i] + " " + position + ")"; // Example: P(Oxy 1)EPTIDE
                    }
                }

                StringBuilder fullSeqBuilder = new StringBuilder(baseSequence);
                List<int> positions = new List<int>(dict.Keys);
                positions.Sort();

                for (int i = positions.Count - 1; i >= 0; i--) // Insert from right to left, for much of the same reasons that you can't remove left to right
                {
                    int pos = positions[i];
                    if (pos >= 1 && pos <= fullSeqBuilder.Length)
                    {
                        fullSeqBuilder.Insert(pos, dict[pos]);
                    }
                }

                return fullSeqBuilder.ToString();
            }
        }

        #endregion
    }
}
