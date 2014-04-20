using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace EPAM.Trainings
{
    public class Finder:IDisposable
    {
        private FileStream fileStream;

        public void Write(string message)
        {
             fileStream = new FileStream("log.txt", FileMode.Append, FileAccess.Write, FileShare.None);
             using (fileStream)
             {
                 StringBuilder stringBuilder = new StringBuilder(null);
                 Regex regex=new Regex("12");
                 if (regex.Matches(message).Count > 0)
                 {
                     stringBuilder.Append("Bad message ");
                 }
                 else
                 {
                     stringBuilder.Append("Good message ");  
                 }
                 byte[] info = new UTF8Encoding(true).GetBytes(stringBuilder.ToString());
                 AsyncCallback asyncCallback = new AsyncCallback(delegate
                 {
                     fileStream.Close();
                 }
                 );

                 IAsyncResult asyncResult = fileStream.BeginWrite(info,
                     0,
                     info.Length,
                     asyncCallback,
                     "log.txt");
                 fileStream.EndWrite(asyncResult);
             }
        }

        public void Dispose()
        {
            fileStream.Dispose();
        }

    }
}
