using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeLapseExp.Instrumentation
{
    /// <summary>
    /// Generates file name depending on layout string, current time and input data.
    /// </summary>
    // TODO: Use third party text layout engine (e.g StringTemplate).
    // TODO: Use some extension framework to change behaviour.
    public class FileNamingEngine
    {
        
        private static Tuple<string, string> _year = new Tuple<string, string>("${year}", "{0:D4}");
        private static Tuple<string, string> _month = new Tuple<string, string>("${month}", "{1:D2}");
        private static Tuple<string, string> _day = new Tuple<string, string>("${day}", "{2:D2}");

        private static Tuple<string, string> _hour = new Tuple<string, string>("${hour}", "{3:D2}");
        private static Tuple<string, string> _minute = new Tuple<string, string>("${minute}", "{4:D2}");
        private static Tuple<string, string> _second = new Tuple<string, string>("${second}", "{5:D2}");

        private static Tuple<string, string> _message = new Tuple<string, string>("${message}", "{6}");

        public static readonly String DefaultLayout = "EXP_D${year}_${month}_${day}_T${hour}_${minute}_${second}_${message}S";

        public static String LastTimeUsageLayout = DefaultLayout;

        /// <summary>
        /// Transforms "EXP_D${year}_${month}_${day}_T${hour}_${minute}_${second}_${message}S"
        /// to "EXP_D{0:D4}_{1:D2}_{2:D2}_T{3:D2}_{4:D2}_{5:D2}_{6}S".
        /// </summary>
        /// <param name="layout"></param>
        ///  <returns></returns>
        private static String ReplaceContainers(string layout)
        {
            var newLayout = layout.Replace(_year.Item1, _year.Item2);
            newLayout = newLayout.Replace(_month.Item1, _month.Item2);
            newLayout = newLayout.Replace(_day.Item1, _day.Item2);
            newLayout = newLayout.Replace(_hour.Item1, _hour.Item2);
            newLayout = newLayout.Replace(_minute.Item1, _minute.Item2);
            newLayout = newLayout.Replace(_second.Item1, _second.Item2);
            newLayout = newLayout.Replace(_message.Item1, _message.Item2);
            return newLayout;
        }

        public static String GetFolderName()
        {
            throw new NotImplementedException();
        }

        public static String GetContarstMeasurmentFileName()
        {
            throw new NotImplementedException();
        }

        public static String GetShannonEntropyFileName()
        {
            throw new NotImplementedException();
        }

        public static String GetExperimentSetupFileName()
        {
            var temp = GetPictureNamePrefix(null, null);
            return temp.Remove(temp.Length-1);
        }

        /// <summary>
        /// Gets default picture prefix which depends only on current date and time.
        /// </summary>
        /// <returns></returns>
        public static String GetPictureNamePrefix()
        {
            return GetPictureNamePrefix(null, null);
        }

        /// <summary>
        /// Gets custom picture prefix which depends on current date,time and layout.
        /// </summary>
        /// <param name="layout"></param>
        /// <returns></returns>
        public static String GetPictureNamePrefix(string layout)
        {
            return GetPictureNamePrefix(layout, null);
        }

        /// <summary>
        /// Gets custom picture prefix.
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static String GetPictureNamePrefix(string layout, string message)
        {
            if (String.IsNullOrEmpty(layout))
            {
                layout = DefaultLayout;
            }
            layout = ReplaceContainers(layout);
            LastTimeUsageLayout = layout;
            var dateTime = DateTime.Now;
            return String.Format(layout,
                                 dateTime.Year,
                                 dateTime.Month,
                                 dateTime.Day,
                                 dateTime.Hour,
                                 dateTime.Minute,
                                 dateTime.Second,
                                 message);
        }
    }
}