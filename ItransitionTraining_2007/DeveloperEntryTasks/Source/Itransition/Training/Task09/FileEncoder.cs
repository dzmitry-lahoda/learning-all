using System.IO;
using System.Text;

namespace Itransition.Training.Task09
{
    /// <summary>
    /// Represents type which provides method to read one file and write another file in new encoding
    /// </summary>
    public static class FileEncoder
    {
        /// <summary>
        ///  Try to read one file and writes another file in new encoding.
        /// A return value indicates whether the encoding succeeded or failed
        /// </summary>
        /// <param name="infile">Input filename</param>
        /// <param name="outfile">Output filename</param>
        /// <param name="incoding">Input coding name</param>
        /// <param name="outcoding">Output coding name</param>
        /// <returns>true if encoding was successfull; otherwise, false</returns>
        public static bool TryEncode(string infile, string outfile, string incoding, string outcoding)
        {
            try
            {
                Encoding inEncoding = Encoding.GetEncoding(incoding);
                Encoding outEncoding = Encoding.GetEncoding(outcoding);
                byte[] bytes = File.ReadAllBytes(infile);
                bytes = Encoding.Convert(inEncoding, outEncoding, bytes);
                File.WriteAllBytes(outfile, bytes);
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
