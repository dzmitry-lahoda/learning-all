using System;

using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;

namespace Itransition.Training.Task05
{
    /// <summary>
    /// Represents a complex numbers and basic operations upon them. 
    /// </summary>
    [Serializable]
    public struct ComplexNumber:IEquatable<ComplexNumber>,IFormattable
    {
        private double r;
        private double i;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Itransition.Training.Task08.ComplexNumber
        /// </summary>
        /// <param name="real">Real part</param>
        /// <param name="imagine">Imagine part</param>
        public ComplexNumber(double real, double imagine)
        {
            r = real;
            i = imagine;
        }

        /// <summary>
        /// Initializes a new instance of the Itransition.Training.Task08.ComplexNumber whith zero imagine part
        /// </summary>
        /// <param name="real">Real part</param>
        public ComplexNumber(double real)
        {
            r = real;
            i = 0;
        }
        #endregion

        #region Real, Imagine, Length properties
        /// <summary>
        /// Gets and sets Real part
        /// </summary>
        public double Real
        {
            get
            {
                return r;
            }
            set
            {
                r = value;
            }
        }

        /// <summary>
        /// Gets and sets Imagine part
        /// </summary>
        public double Imagine
        {
            get
            {
                return i;
            }
            set
            {
                i = value;
            }
        }

        /// <summary>
        /// Returns Modulus of ComplexNumber
        /// </summary>
        public double Modulus
        {
            get
            {
                return Math.Sqrt(r*r+i*i);
            }
        }
        #endregion

        #region Equality members
        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="obj">An object to compare with this instance</param>
        /// <returns>true if value is a Itransition.Training.Task05.ComplexNumber object that has the same 
        /// Real and Image parts as the current Itransition.Training.Task05.ComplexNumber object;
        /// otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is ComplexNumber)
            {
                ComplexNumber temp = (ComplexNumber)obj;
                return Equals(temp);
            }
            return false; 
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified Itransition.Training.Task05.ComplexNumber object
        /// </summary>
        /// <param name="number">An Itransition.Training.Task05.ComplexNumber object to compare with this instance</param>
        /// <returns> true if obj has the same Real and Image parts as this instance; otherwise, false</returns>
        bool IEquatable<ComplexNumber>.Equals(ComplexNumber number)
        {
            if (ToString() == number.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a value indicating whether two specified instances of 
        /// Itransition.Training.Task05.ComplexNumber are equal
        /// </summary>
        /// <param name="number1">An Itransition.Training.Task05.ComplexNumber</param>
        /// <param name="number2">A ComplexNumber</param>
        /// <returns>true if the values of number1 and number2 are equal; otherwise, false</returns>
        public static bool Equals(ComplexNumber number1, ComplexNumber number2)
        {
            return number1.Equals(number2);
        }
        #endregion

        #region Add, Subtract, Multiply, Divide methods
        /// <summary>
        /// Adds two complex numbers
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public static ComplexNumber Add(ComplexNumber number1, ComplexNumber number2)
        {
            return new ComplexNumber(number1.r + number2.r, number1.i + number2.i);
        }
        
        /// <summary>
        /// Substracts second complex number from first complex number
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public static ComplexNumber Subtract(ComplexNumber number1, ComplexNumber number2)
        {
            return new ComplexNumber(number1.r - number2.r, number1.i - number2.i);
        }
        
        /// <summary>
        /// Multiplies two complex numbers
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public static ComplexNumber Multiply(ComplexNumber number1, ComplexNumber number2)
        {
            return new ComplexNumber(number1.r * number2.r - number1.i * number2.i, number1.r * number2.i + number1.i * number2.r);
        }

        /// <summary>
        /// Divides first complex number on second complex number
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        /// <exception cref="DivideByZeroException"></exception>
        public static ComplexNumber Divide(ComplexNumber number1, ComplexNumber number2)
        {
            if (number2.r != 0.0 || number2.i != 0.0)
            {
                double div = number2.r * number2.r + number2.i * number2.i;
                return number1 * new ComplexNumber(number2.r / div, -number2.i / div);
            }
            else
            {
                throw new DivideByZeroException();
            }
        }
        #endregion

        #region Overrided ToString,GetHashCode

        /// <summary>
        /// Returns the string representation of the value of this instance
        /// </summary>
        /// <param name="format">"Real" or "Imagine"</param>
        /// <param name="formatProvider">Can be null(uses current culture)</param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            // If no format is passed, display like this: (x, y).
            if (format == null) return String.Format("({0}, {1})", r, i);

            // For "x" formatting, return just the x value as a string
            if (format == "Real") return r.ToString();

            // For "y" formatting, return just the y value as a string
            if (format == "Imagine") return i.ToString();

            // For any unrecognized format, throw an exception.
            throw new FormatException(String.Format("Invalid format string: '{0}'.", format));
        }

        /// <summary>
        /// Returns the string representation of the value of this instance
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(null, null);
        }

        /// <summary>
        /// Returns a hash code for this instance
        /// </summary>
        /// <returns>A 32-bit signed integer hash code</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        #endregion
        
        #region +, -, *, /, ==, != operators
        public static implicit operator ComplexNumber(double number2)
        {
            return new ComplexNumber(number2);
        }

        /*public static implicit operator ComplexNumber(double[] number2)
        {
            return new ComplexNumber(number2[0]);
        }*/

        public static ComplexNumber operator +(ComplexNumber number1, ComplexNumber number2)
        {
            return Add(number1,number2);
        }

        public static ComplexNumber operator -(ComplexNumber number1, ComplexNumber number2)
        {
            return Subtract(number1, number2);
        }

        public static ComplexNumber operator *(ComplexNumber number1, ComplexNumber number2)
        {
            return Multiply(number1,number2);
        }

        public static ComplexNumber operator /(ComplexNumber number1, ComplexNumber number2)
        {
            return Divide(number1, number2);
        }
 
        public static bool operator ==(ComplexNumber number1, ComplexNumber number2)
        {
            return (number1.Equals(number2));
        }
        public static bool operator !=(ComplexNumber number1, ComplexNumber number2)
        {
            return !(number1.Equals(number2));
        }
        #endregion

        /// <summary>
        /// Converts the string representation of a number to its complex number equivalent
        /// </summary>
        /// <param name="s">A string containing a number to convert</param>
        /// <returns> A complex number equivalent to the symbol specified in s</returns>
        /// <exception cref="System.ArgumentNullException">s is null</exception>
        /// <exception cref="System.FormatException">s is not a comlex number in a valid format</exception>
        public static ComplexNumber Parse(string s)
        {
            if (s == null)
            {
                throw (new ArgumentNullException(
                         "s",
                         "A null cannot be passed into the Parse method."));
            }
            double r = 0;
            double i = 0;
            String separator = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            String patternDouble = @"[-+0-9,.e]+";
            String pattern = @"\s*\(\s*(?<r>" + patternDouble + @")\s*\,\s+(?<i>" + patternDouble + @")\s*\)\s*";
            MatchCollection matches = Regex.Matches(s, pattern);
            if (matches.Count == 1)
            {
                String rstr = Regex.Replace(matches[0].Groups["r"].Value, "[,.]", separator);
                String istr = Regex.Replace(matches[0].Groups["i"].Value, "[,.]", separator);
                if 
                    ((!Double.TryParse(rstr, out r)) || (!Double.TryParse(istr, out i)))
                {
                    throw (new ArgumentException("The value " + s +
                        " is not a well formed ComplexNumber value"));
                }
            }
            else
            {
                throw (new ArgumentException("The value " + s +
                                             " is not a well formed ComplexNumber value"));
            }
            return (new ComplexNumber(r,i)); 

        }
    }
}
