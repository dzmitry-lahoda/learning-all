using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;

using MatrixExtensionsGenericCodeBase;

namespace MatrixExtensions.Manipulation.Generic
{
    /// <summary>
    /// Provides generic extension methods for 1D arrays manipulation.
    /// </summary>
     /// <seealso href="http://www.mathworks.com/access/helpdesk/help/techdoc/ref/circshift.html"/>
    /// <seealso href="http://www.mathworks.com/access/helpdesk/help/techdoc/ref/shiftdim.html"/>
   
    [Pure]
    public static class GenericArrayManipulationExtensions
    {
        /// <summary>
        /// Gets new array shifted by count to the right if count>0,
        /// otherwise to the left.
        /// Empty elements are fullfilled with default values.
        /// </summary>
        /// <typeparam name="T">Type with default constructot.</typeparam>
        /// <param name="vector"></param>
        /// <param name="count">Length of shift.</param>
        /// <returns>The vector which stores shifted elements of the original vector.</returns>
        public static T[] Shift<T>(this T[] vector, int count) where T : new()
        {
            Contract.Requires<ArgumentOutOfRangeException>(vector.Length >= Math.Abs(count), "count");
            Contract.Ensures(Contract.Result<T[]>().Length == vector.Length);
            var length = vector.Length;
            var newVector = new T[length];
            
            if (count >= 0)
            {

                Do.Invoke(
                    () => Do.For(0, count, i => newVector[i] = new T()),
                    () => Do.For(count, length, i => newVector[i] = vector[i - count])
                    );
            }
            else
            {
                Do.Invoke(
                    () => Do.For(0, length + count, i => newVector[i] = vector[i - count]),
                    () => Do.For(length + count, length, i => newVector[i] = new T())
                    );
            }
            return newVector;
        }

        /// <summary>
        /// Gets new array shifted by count to the right if count>0,
        /// otherwise to the left.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vector"></param>
        /// <param name="count">Length of shift.</param>
        /// <returns>The vector which stores shifted elements of the original vector.</returns>
        public static T[] CircularShift<T>(this T[] vector, int count)
        {
            Contract.Requires<ArgumentOutOfRangeException>(vector.Length >= Math.Abs(count),"count");
            Contract.Ensures(Contract.Result<T[]>().Length == vector.Length);
            var length = vector.Length;
            var newVector = new T[length];

            if (count >= 0)
            {
                Do.Invoke(
                    () => Do.For(0, count, i => newVector[i] = vector[count + i + 1]),
                    () => Do.For(count, length, i => newVector[i] = vector[i - count])
                    );
            }
            else
            {
                Do.Invoke(
                    () => Do.For(0, length + count, i => newVector[i] = vector[i - count]),
                    () => Do.For(length + count, length, i => newVector[i] = vector[i + count + 1])
                    );
            }
            return newVector;
        }

    }
}