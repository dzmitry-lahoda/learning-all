using System;
using System.Collections.Generic;
using System.Text;
using EPAM.Trainings.Zadanie2_2;

namespace EPAM.Trainings.Zadanie2_4
{
    [Serializable]
    public class PointWorkerEventArgs : EventArgs
    {
        public PointWorkerEventArgs(Point point, Pointer pointer)
        {
            this.point = point;
            this.pointer = pointer;
        }
        public Point point = Point.Zero;
        public Pointer pointer = Pointer.CirclePoint;

   }

    public delegate void PointWorkerEventHandler(Object sender, PointWorkerEventArgs e);

    public enum Pointer
    {
        CirclePoint,
        Point
    }

    public class PointWorker
    {
        public Random random = new Random();
        public event PointWorkerEventHandler PointsChanged;
        public List<Point> circlepoints = new List<Point>();
        public List<Point> points = new List<Point>();
        public Circle circle = Circle.Zero;

        private int n = 5;
        private int m = 5;

        public PointWorker()
        {
            circle = new Circle(Point.Zero, 10000);
        }

        /// <summary>
        /// Returns all possible different circles passed through any three poins 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public List<Circle> GetCircles()
        {
            List<Circle> list = new List<Circle>();
            if (circlepoints.Count < 3)
            {
                return list;
            }
            for (int i = 0; i < circlepoints.Count; i++)
            {
                for (int j = i + 1; j < circlepoints.Count; j++)
                {
                    for (int k = j + 1; k < circlepoints.Count; k++)
                    {
                        try
                        {
                            Circle c = new Circle(circlepoints[i], circlepoints[j], circlepoints[k]);
                            if (!list.Contains(c))
                            {
                                list.Add(c);
                            }
                        }
                        catch (CircleException e)
                        {
                            Console.WriteLine(e.Source + " " + e.Message + " ");
                            Point[] p = (Point[])e.Data["Points"];
                            foreach (Point var in p)
                            {
                                Console.Write(" "+var.ToString());
                            }
                        }

                    }
                }
            }
            return list;
        }

        public void SetPoints(Point point,Pointer pointer)
        {
            if (PointsChanged != null)
            {
                PointsChanged(this,
                    new PointWorkerEventArgs(point,pointer));
            }
        }

        public void AddCirclePoints(Point p)
        {
            if (this.circlepoints.Count >= n)
            {
                this.circlepoints[random.Next(1, n)] = p;
            }
            else
            {
                this.circlepoints.Add(p);
            }
        }

        public void AddPoints(Point p)
        {
            if (this.points.Count >= m)
            {
                this.points[random.Next(1, m)] = p;
            }
            else
            {
                this.points.Add(p);
            }
        }
    }

}
