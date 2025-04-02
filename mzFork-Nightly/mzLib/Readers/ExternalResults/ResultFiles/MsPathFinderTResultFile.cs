using CsvHelper;
using Easy.Common.Extensions;
using System.Text;

namespace Readers
{
    public class MsPathFinderTResultFile : ResultFile<MsPathFinderTResult>, IResultFile
    {
        public override SupportedFileType FileType { get; }
        public override Software Software { get; set; }

        public MsPathFinderTResultFile(string filePath) : base(filePath, Software.MsPathFinderT)
        {
            FileType = filePath.ParseFileType();
        }

        public MsPathFinderTResultFile() : base()
        {
            FileType = FilePath.IsNullOrEmpty() ? SupportedFileType.MsPathFinderTAllResults : FilePath.ParseFileType();
        }

        

        public Dictionary<string, List<List<string>>> ToDictListList()
        {
            var dictList = new Dictionary<string, List<List<string>>>();
            using var csv = new CsvReader(new StreamReader(FilePath), MsPathFinderTResult.CsvConfiguration);
            Results = csv.GetRecords<MsPathFinderTResult>().ToList();

            foreach (MsPathFinderTResult res in Results)
            {
                string baseKey = res.BaseSequence;
                var valuesList = new List<string>
                {
                    res.OneBasedScanNumber.ToString(),
                    res.PreviousResidue.ToString(),
                    res.BaseSequence,
                    res.NextResidue.ToString(),
                    res.Modifications,
                    res.ChemicalFormula.ToString(),
                    res.ProteinName,
                    res.ProteinDescription,
                    res.Length.ToString(),
                    res.OneBasedStartResidue.ToString(),
                    res.OneBasedEndResidue.ToString(),
                    res.Charge.ToString(),
                    res.MostAbundantIsotopeMz.ToString(),
                    res.MonoisotopicMass.ToString(),
                    res.Ms1Features.ToString(),
                    res.NumberOfMatchedFragments.ToString(),
                    res.Probability.ToString(),
                    res.SpecEValue.ToString(),
                    res.EValue.ToString(),
                    res.QValue.ToString(),
                    res.PepQValue.ToString(),
                    res.FileNameWithoutExtension
                };

                if (!dictList.ContainsKey(baseKey))
                {
                    dictList[baseKey] = new List<List<string>>();
                }

                dictList[baseKey].Add(valuesList);
            }

            return dictList;
        }


        public override void LoadResults()
        {
            using var csv = new CsvReader(new StreamReader(FilePath), MsPathFinderTResult.CsvConfiguration);
            Results = csv.GetRecords<MsPathFinderTResult>().ToList();
            if (Results.Any() && Results.First().FileNameWithoutExtension.IsNullOrEmpty())
                Results.ForEach(p => p.FileNameWithoutExtension = string.Join("_", Path.GetFileNameWithoutExtension(FilePath).Split('_')[..^1]));
        }

        public override void WriteResults(string outputPath)
        {
            if (!CanRead(outputPath))
                outputPath += FileType.GetFileExtension();

            using (var csv = new CsvWriter(new StreamWriter(File.Create(outputPath)), MsPathFinderTResult.CsvConfiguration))
            {
                csv.WriteHeader<MsPathFinderTResult>();
                foreach (var result in Results)
                {
                    csv.NextRecord();
                    csv.WriteRecord(result);
                }
            }
        }
    }
}
