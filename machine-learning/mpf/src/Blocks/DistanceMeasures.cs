using MathNet.Numerics.LinearAlgebra.Generic;
using MathNet.Numerics.Statistics;
using MPF.Blocks;

namespace Blocks
{
    public static class DistanceMeasures
    {
        public static readonly DistanceMeasure CanberraDistance = c;
        public static readonly DistanceMeasure CorrelationDistance = b;
        public static readonly DistanceMeasure CosineDistance = a;
        public static readonly DistanceMeasure Default = SquaredEuclidianDistance;
        public static readonly DistanceMeasure EuclidianDistance = CalculateEuclidianDistance;
        public static readonly DistanceMeasure ManhattanDistance = e;
        public static readonly DistanceMeasure MaximumDistance = d;
        public static readonly DistanceMeasure SquaredEuclidianDistance = g;

        static DistanceMeasures()
        {
            EuclidianDistance = new DistanceMeasure(DistanceMeasures.CalculateEuclidianDistance);
            SquaredEuclidianDistance = new DistanceMeasure(DistanceMeasures.g);
            ManhattanDistance = new DistanceMeasure(DistanceMeasures.e);
            MaximumDistance = new DistanceMeasure(DistanceMeasures.d);
            CanberraDistance = new DistanceMeasure(DistanceMeasures.c);
            CosineDistance = new DistanceMeasure(DistanceMeasures.a);
            CorrelationDistance = new DistanceMeasure(DistanceMeasures.b);
            Default = SquaredEuclidianDistance;
        }

        private static double a(Vector<double> A_0, Vector<double> A_1)
        {
            if (A_0.Count != A_1.Count)
            {
                ////ThrowException.a("vector1", "vector2");
            }
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            for (int i = 0; i < A_0.Count; i++)
            {
                double num5 = A_0[i];
                double num6 = A_1[i];
                num += num5 * num6;
                num2 += num5 * num5;
                num3 += num6 * num6;
            }
            return (1.0 - (num / System.Math.Sqrt(num2 * num3)));
        }

        private static double b(Vector<double> A_0, Vector<double> A_1)
        {
            if (A_0.Count != A_1.Count)
            {
                //ThrowException.a("vector1", "vector2");
            }
            return (0.5 * (1.0 - Correlation.Pearson(A_0, A_1)));
        }

        private static double c(Vector<double> A_0, Vector<double> A_1)
        {
            if (A_0.Count != A_1.Count)
            {
                //ThrowException.a("vector1", "vector2");
            }
            double num = 0.0;
            for (int i = 0; i < A_0.Count; i++)
            {
                double num3 = A_0[i];
                double num4 = A_1[i];
                double num5 = System.Math.Abs((double)(num3 + num4));
                if (num5 != 0.0)
                {
                    num += System.Math.Abs((double)(num3 - num4)) / num5;
                }
            }
            return num;
        }

        private static double d(Vector<double> A_0, Vector<double> A_1)
        {
            if (A_0.Count != A_1.Count)
            {
                //ThrowException.a("vector1", "vector2");
            }
            double num = 0.0;
            for (int i = 0; i < A_0.Count; i++)
            {
                num = System.Math.Max(num, System.Math.Abs((double)(A_0[i] - A_1[i])));
            }
            return num;
        }

        private static double e(Vector<double> A_0, Vector<double> A_1)
        {
            if (A_0.Count != A_1.Count)
            {
                //ThrowException.a("vector1", "vector2");
            }
            double num = 0.0;
            for (int i = 0; i < A_0.Count; i++)
            {
                num += System.Math.Abs((double)(A_0[i] - A_1[i]));
            }
            return num;
        }

        private static double CalculateEuclidianDistance(Vector<double> A_0, Vector<double> A_1)
        {
            return System.Math.Sqrt(g(A_0, A_1));
        }

        private static double g(Vector<double> A_0, Vector<double> A_1)
        {
            if (A_0.Count != A_1.Count)
            {
                ////ThrowException.a("vector1", "vector2");
            }
            double num = 0.0;
            int num2 = 0;
            for (int i = 0; i < A_0.Count; i++)
            {
                double d = A_0[i] - A_1[i];
                if (double.IsNaN(d))
                {
                    num2++;
                }
                else
                {
                    num += d * d;
                }
            }
            if (num2 <= 0)
            {
                return num;
            }
            if (num2 == A_0.Count)
            {
                return double.NaN;
            }
            return ((num / ((double)(A_0.Count - num2))) * A_0.Count);
        }

        public static DistanceMeasure MinkowskiDistance(double power)
        {
            return new DistanceMeasure(new MinkowskiDistanceClass(power).Calculate);
        }

        private class MinkowskiDistanceClass
        {
            private double power;

            public MinkowskiDistanceClass(double power)
            {
                this.power = power;
            }

            public double Calculate(Vector<double> A_0, Vector<double> A_1)
            {
                if (A_0.Count != A_1.Count)
                {
                    //ThrowException.a("vector1", "vector2");
                }
                double x = 0.0;
                for (int i = 0; i < A_0.Count; i++)
                {
                    double num3 = System.Math.Abs((double)(A_0[i] - A_1[i]));
                    x += System.Math.Pow(num3, this.power);
                }
                return System.Math.Pow(x, 1.0 / this.power);
            }
        }
    }
}