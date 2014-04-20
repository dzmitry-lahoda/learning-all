using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings.Zadanie2_2
{
    /// <summary>
    /// Represents Circle
    /// </summary>
    public class Circle : IEquatable<Circle>
    {
        private readonly int maxRadius = (int)Math.Floor((Math.Sqrt(int.MaxValue/Math.PI)));
        private Point centre = new Point(0, 0);
        
        /// <summary>
        /// Gets and sets circle centre
        /// </summary>
        public Point Centre
        {
            get
            {
                return centre;
            }
            set
            {
                centre = value;
            }
        }

        private int radius = 0;

        /// <summary>
        /// Gets and sets circle radius
        /// </summary>
        public int Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }

        /// <summary>
        /// Gets circle's area
        /// </summary>
        public int Area
        {
            get
            {
                return (int)Math.Round(Math.PI*Radius*Radius);
            }
        }

        /// <summary>
        /// Returns circle with Radius=0 and Centre in Point.Zero
        /// </summary>
        public static Circle Zero
        {
            get
            {
                return new Circle();
            }
        }

        private Circle()
        {
            Centre = new Point(0,0);
            Radius = 0;
        }

        /// <summary>
        /// Creates new circle
        /// </summary>
        /// <param name="centre"></param>
        /// <param name="radius"></param>
        public Circle(Point centre,int radius)
        {
            Centre = centre;
            Radius = radius;
        }

        /// <summary>
        /// Creates new circle from three points
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="point3"></param>
        /// <exception cref="CircleException">Thrown when... something wrong, aha.</</exception>
        public Circle(Point p1, Point p2, Point p3)
        {
            if ((p1 == p2) || (p1 == p3) || (p2 == p3))
            {
                throw new CircleException(new Point[]{ p1, p2, p3 });
            }
            double deltaX1 = p2.X - p1.X;
            double deltaX2 = p3.X - p2.X;
            if (deltaX1 == 0 || deltaX2 == 0)
            {
                Point temp = p1;
                p1 = p2;
                p2 = temp;
            }
            deltaX1 = p2.X - p1.X;
            deltaX2 = p3.X - p2.X;
            if (deltaX1 == 0 || deltaX2 == 0)
            {
                throw new CircleException( new Point[] { p1, p2, p3 });
            }
            double ma = 1.0 * (p2.Y - p1.Y) / deltaX1;
            double mb = 1.0 * (p3.Y - p2.Y) / deltaX2;
            double deltaangle = mb - ma;
            if (deltaangle == 0)
            {
                throw new CircleException(new Point[] { p1, p2, p3 });
            }
            this.Centre.X = (int)Math.Round(
                (
                ma * mb * (p1.Y - p3.Y) + mb * (p1.X + p2.X) - ma * (p2.X + p3.X)
                ) /
                (2 * deltaangle)
                );
            if (ma != 0)
            {
                this.Centre.Y = (int)Math.Round(
                    -1 / ma * (this.Centre.X - (p1.X + p2.X) / 2) + (p1.Y + p2.Y) / 2
                    );
            }
            else
            {
                this.Centre.Y = (int)Math.Round(
                    -1 / mb * (this.Centre.X - (p1.X + p3.X) / 2) + (p1.Y + p3.Y) / 2
                    );
            }
            this.Radius = Point.Distance(Centre, p1);
        }


        /// <summary>
        /// Returns true if point lays in circle's area
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(Point point)
        {
             if (Point.Distance(this.Centre, point) > this.Radius)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Returns true if all points lay in circle's area
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public bool Contains(params Point[] points)
        {
            foreach (Point var in points)
            {
                if (Point.Distance(this.Centre, var) > this.Radius)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns all possible different circles passed through any three poins 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<Circle> GetCircles(params Point[] points)
        {
            List<Circle> list = new List<Circle>();
            if (points.Length < 3)
            {
                return list;
            }
            for (int i = 0; i < points.Length; i++)
            {
                for (int j = i + 1; j < points.Length; j++)
                {
                    for (int k = j + 1; k < points.Length; k++)
                    {
                        Circle c = new Circle(points[i], points[j], points[k]);
                        if (!list.Contains(c))
                        {
                            list.Add(c);
                        }
                    }
                }
            }
            Predicate<Circle> match = new Predicate<Circle>(Exists);
            list.RemoveAll(match);
            return list;
        }

        private static bool Exists(Circle c)
        {
            return c.Equals(Circle.Zero);
        }

        #region Equality,ToString,GetHashCode
        public override string ToString()
        {
            return String.Format("Centre={0}, Radius={1}", Centre, Radius);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(this, (obj as Point));
        }

        public bool Equals(Circle other)
        {
            return Equals(this, other);
        }

        public static bool Equals(Circle a,Circle b)
        {
            if ((Object)a != null && (Object)b != null)
            {
                return (a.ToString() == b.ToString());
            }
            return false;
        }

        public static bool operator ==(Circle a, Circle b)
        {
            return Circle.Equals(a, b);
        }

        public static bool operator !=(Circle a, Circle b)
        {
            return !Circle.Equals(a, b);
        }
        #endregion


    }
}
