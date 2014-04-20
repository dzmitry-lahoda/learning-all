using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings.Zadanie2_2
{
    /// <summary>
    /// Represents point
    /// </summary>
    public class Point:IEquatable<Point>
    {
        int x;
        int y;

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        /// <summary>
        /// Distance between two points
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static int Distance(Point point1, Point point2)
        {
            return (int)Math.Round(
                Math.Sqrt(
                    (point2.X - point1.X) * (point2.X - point1.X) + (point2.Y - point1.Y) * (point2.Y - point1.Y)
                    )
            );
        }

        /// <summary>
        /// Returns point where X=Y=0;
        /// </summary>
        public static Point Zero
        {
            get
            {
                return new Point(0,0);
            }
        }

        private Point()
        {
        }

        /// <summary>
        /// Creates point with x and y coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        #region Equality,ToString,GetHashCode
        public override string ToString()
        {
            return String.Format("({0}, {1})",X,Y);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static bool Equals(Point a, Point b)
        {
            if ((Object)a != null && (Object)b != null)
            {
                return (a.ToString() == b.ToString());
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            return Point.Equals(this, (obj as Point));
        }

        public bool Equals(Point other)
        {
            return Point.Equals(this, other);
        }

        public static bool operator ==(Point a,Point b)
        {
            return Point.Equals(a, b);
        }

        public static bool operator !=(Point a, Point b)
        {
            return !Point.Equals(a, b);
        }
        #endregion


    }
}
