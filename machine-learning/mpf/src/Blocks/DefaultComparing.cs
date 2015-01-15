using MathNet.Numerics;

namespace Blocks
{
    public static class DefaultComparing
    {
        public const int DefaultDecimalPlaces = 6;

        public const double Epsilon = 1E-5;

        public static int Compare(double x, double y)
        {
           
            return Precision.CompareToInDecimalPlaces(x, y,DefaultDecimalPlaces);
        }
        
        public static bool NearEqual(double x, double y)
        {
            var result =  Precision.CompareToInDecimalPlaces(x, y,DefaultDecimalPlaces);   
            return result == 0 ? true : false;
        }

    }
}
