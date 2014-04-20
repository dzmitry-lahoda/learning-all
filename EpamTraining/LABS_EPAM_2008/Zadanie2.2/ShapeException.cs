using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings.Zadanie2_2
{
    public class ShapeException:ApplicationException
    {

        protected ShapeException()
        {
        }       

        public ShapeException(String source)
        {
            this.Source = source;       
        }

        public override string Message
        {
            get
            {
                return "Wrong shape parameters.";
            }
        }
    }

    public class CircleException : ShapeException
    {

        protected CircleException()
        {
        }

        public CircleException(Point[] points)
        {
            this.Source = this.GetType().ToString();
            this.Data["Points"] = points;
        }

        public override string Message
        {
            get
            {
                return "All points are placed in one line.";
            }
        }
    }
}
