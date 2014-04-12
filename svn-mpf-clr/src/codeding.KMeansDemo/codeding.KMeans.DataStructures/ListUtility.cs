//SplitList.cs

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace codeding.KMeans.DataStructures
{
    public class ListUtility
    {
        public static List<List<T>> SplitList<T>(List<T> items, int groupCount)
        {
            List<List<T>> allGroups = new List<List<T>>();

            //split the list into equal groups
            int startIndex = 0;
            int groupLength = (int)Math.Round((double)items.Count / (double)groupCount, 0);
            while (startIndex < items.Count)
            {
                List<T> group = new List<T>();
                group.AddRange(items.GetRange(startIndex, groupLength));
                startIndex += groupLength;

                //adjust group-length for last group
                if (startIndex + groupLength > items.Count)
                {
                    groupLength = items.Count - startIndex;
                }

                allGroups.Add(group);
            }

            //merge last two groups, if more than required groups are formed
            if (allGroups.Count > groupCount && allGroups.Count > 2)
            {
                allGroups[allGroups.Count - 2].AddRange(allGroups.Last());
                allGroups.RemoveAt(allGroups.Count - 1);
            }

            return (allGroups);
        }

        public static void Example()
        {
            List<int> recordNumbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            List<List<int>> recordGroups = SplitList<int>(recordNumbers, 3);

            List<Point> allPoints = new List<Point>();
            allPoints.Add(new Point() { X = 5, Y = 2 });
            allPoints.Add(new Point() { X = 3, Y = 1 });
            allPoints.Add(new Point() { X = 1.5, Y = 2 });
            allPoints.Add(new Point() { X = 13, Y = 7 });
            allPoints.Add(new Point() { X = 5.1, Y = 3 });
            List<List<Point>> pointGroups = SplitList<Point>(allPoints, 2);
        }
    }
}
