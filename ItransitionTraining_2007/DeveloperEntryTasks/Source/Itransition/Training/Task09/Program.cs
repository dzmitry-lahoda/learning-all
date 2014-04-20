using System;
using System.Collections.Specialized;

namespace Itransition.Training.Task09
{

    class Program
    {
        private static readonly String HelpMessage = @"

Wrong command line arguments!!!

/parameter:'value'         meaning

/infile:'infilename'       Defines input file       
/outfile:'outfilename'     Defines output file
/incoding:'incoding'       Defines input coding
/outcoding:'outcoding'     Defines output coding

EXAMPLE:
/infile:'D:\work\asd.txt' /outfile:'qwe.txt' /incoding:'windows-1251' /outcoding:'utf-8'
";

        static void Main(string[] args)
        {
            StringDictionary sd = CommandLine.Parse(args);
            if (!FileEncoder.TryEncode(sd["infile"], sd["outfile"], sd["incoding"], sd["outcoding"]))
            {
                Console.WriteLine(HelpMessage);
            }
        }
    }
}
