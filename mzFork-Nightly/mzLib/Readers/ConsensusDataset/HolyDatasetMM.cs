using CsvHelper;
using Easy.Common.Extensions;
using Readers.ExternalResults.BaseClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readers.ConsensusDataset
{
    public class HolyDatasetMM
    {
        private string outPath;
        private string exePath;
        private string spectraPath;
        private string dataPath;

        /*
         * Returns output given by outPath, user Defined
         */
        public HolyDatasetMM(string exePath, string spectraPath, string dataPath,
            string outPath) //TODO TEST
        {
            //initialize to class variables
            this.exePath = exePath;
            this.spectraPath = spectraPath;
            this.dataPath = dataPath;
            this.outPath = outPath;

            exePath = @"" + exePath;
            spectraPath = @"" + spectraPath;
            dataPath = @"" + dataPath;
            outPath = @"" + outPath;
            string exeParams = " -s " + "\"" + spectraPath + "\"" + " -d " + "\"" + dataPath + "\"" + " -o " + "\"" +
                               outPath + "\"" +
                               " -ic 2 -f 20 -MinLength 7 -MaxLength 1000000 -MinCharge 1 -MaxCharge 60 -MinFragCharge 1 -MaxFragCharge 10 -MinMass 0 -MaxMass 30000 -tda 1"; //100000
            var process = new Process
            {
                StartInfo =
                {
                    FileName = exePath,
                    WorkingDirectory = Path.GetDirectoryName(exePath),
                    Arguments = exeParams,
                }
            };

            process.Start();
            process.WaitForExit();
        }

        public HolyDatasetMM(string exePath, string spectraPath, string dataPath) : this(exePath, spectraPath,
            dataPath,
            Path.GetDirectoryName(spectraPath) ?? throw new ArgumentNullException(nameof(spectraPath))) //out path is the same as the directory of the spectraPath
        {
            outPath = Path.GetDirectoryName(spectraPath) ?? throw new ArgumentNullException(nameof(spectraPath)); // @"\Task1SearchTask\AllProteoforms.psmtsv")
            //result handling
            MsPathFinderTResultFile result =
                new MsPathFinderTResultFile(Path.ChangeExtension(spectraPath,
                    "_IcTda.tsv")); //suppose spectraPath.raw -> spectraPath_IcTda.tsv. TODO double check. Just a guess.
            var dict = ToCertainList(result);
        }

        public List<IResult> ToCertainList(MsPathFinderTResultFile resultFile)
        {
            List<IResult> results = new List<IResult>();
            using var csv = new CsvReader(new StreamReader(outPath), MsPathFinderTResult.CsvConfiguration);
            var Results = csv.GetRecords<MsPathFinderTResult>().ToList();
            foreach (MsPathFinderTResult res in Results)
            {
                results.Add(res);
            }

            return results;
        }

    }
}
