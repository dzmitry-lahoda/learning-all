using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MPF.Blocks
{
	/// <summary>
	/// Description of IntVectorReader.
	/// </summary>
	public class IntVectorReader{


        public IntVectorReader()
        {
        }

        public int[] ReadIntVector(string file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            return ReadIntVector(File.OpenRead(file));
        }

        public int[] ReadIntVector(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            return DoReadIntVector(stream);
        }


        protected int[] DoReadIntVector(Stream stream)
        {
            IList<string> data = new List<string>();
            int max = -1;
            TextReader reader = new StreamReader(new BufferedStream(stream));
            string line = reader.ReadLine();
            while (line != null)
            {
                    data.Add(line);
                    line = reader.ReadLine();
            }

            var ret = new int[data.Count];
  
            if (data.Count != 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    ret[i] = Int32.Parse(data[i], NumberStyles.Any);
                }
            }
            reader.Close();
            reader.Dispose();
            return ret;
        }
    }
}
