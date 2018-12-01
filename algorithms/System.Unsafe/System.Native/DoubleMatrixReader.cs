using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;
using MatrixExtensions.Conversion.Explicit;
using MatrixExtensions.Generic;
using MatrixExtensions.Manipulation;
using MatrixExtensions.Conversion.Generic;
using MatrixExtensions.Manipulation.Generic;
using MatrixExtensions.Operations;
using System.IO;

namespace AlglibSilverlightSamples
{

    public class DoubleMatrixReader
    {
        /// <summary>
        /// Reads data from resources.
        /// </summary>
        /// <param name="fileWithData"></param>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        public static List<List<double>> Read(string fileWithData,string delimeter)
        {
            var resource = ApplicationHelper.GetLocalDataSetResourceStream(fileWithData);
            var reader = new StreamReader(resource.Stream);
            //var reader = new CsvReader(resource.Stream);
            //var csv = reader.ReadDataRecordsAsStrings();
            var list = new List<List<double>>();
            //foreach (var rowString in csv)
            //{
            //    var rowList =
            //        rowString.Select(number => Double.Parse(number, System.Globalization.NumberFormatInfo.InvariantInfo)).
            //        ToList();
            //    list.Add(rowList);
            //}
            while (!reader.EndOfStream)
            {
                var row = reader.ReadLine();
                var numbers = row.Split(new[] { delimeter }, StringSplitOptions.None);
                var rowList =
                    numbers.Select(number => Double.Parse(number, System.Globalization.NumberFormatInfo.InvariantInfo)).
                    ToList();
                list.Add(rowList);
            }
            return list;
        }
    }
}
