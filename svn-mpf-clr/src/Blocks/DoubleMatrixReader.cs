using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Blocks
{
    /// <summary>
    /// Creates a <see cref="Matrix"/> from a delimited text file. If the user does not
    /// specify a delimiter, then any whitespace is used.
    /// </summary>
    public class DoubleMatrixReader
    {
        private static readonly Regex sWhite = new Regex(@"\s+");
        private readonly Regex mDelimiter;
        private static NumberFormatInfo  _numberFormatProvider = new NumberFormatInfo() { NumberDecimalSeparator = "." };

        public DoubleMatrixReader()
        {
            mDelimiter = sWhite;
        }

        public DoubleMatrixReader(char delimiter)
        {
            mDelimiter = new Regex(new string(delimiter, 1));
        }

        public DoubleMatrixReader(string delimiter)
        {
            if (delimiter == null)
            {
                throw new ArgumentNullException("delimiter");
            }
            mDelimiter = new Regex(delimiter);
        }

        public double[,] ReadMatrix(string file)//, StorageType storageType)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            return ReadMatrix(File.OpenRead(file));
        }

        public double[,] ReadMatrix(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            return DoReadMatrix(stream);
        }


        protected double[,] DoReadMatrix(Stream stream)
        {
            IList<string[]> data = new List<string[]>();
            int max = -1;
            TextReader reader = new StreamReader(new BufferedStream(stream));
            string line = reader.ReadLine();
            while (line != null)
            {
                line = line.Trim();
                if (line.Length > 0)
                {
                    string[] row = mDelimiter.Split(line);
                    max = System.Math.Max(max, row.Length);
                    data.Add(row);
                }
                line = reader.ReadLine();
            }

            var ret = new double[data.Count, max];
  
            if (data.Count != 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    string[] row = data[i];
                    for (int j = 0; j < row.Length; j++)
                    {
                        ret[i, j] = Double.Parse(row[j], NumberStyles.Any, _numberFormatProvider);
                    }
                }
            }
            reader.Close();
            reader.Dispose();
            return ret;
        }
    }
}
