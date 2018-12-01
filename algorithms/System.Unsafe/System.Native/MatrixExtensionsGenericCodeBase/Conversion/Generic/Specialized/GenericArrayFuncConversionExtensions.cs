using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;

using MatrixExtensions.Generic;
using MatrixExtensionsGenericCodeBase;

namespace MatrixExtensions.Conversion.Generic.Specialized
{
    /// <summary>
    /// Provides generic extension methods for 1D arrays wich accept <see cref="Func{T,TResult}"/>.
    /// </summary>
    [Pure]
    public static class GenericArrayFuncConversionExtensions
    {

        /// <summary>
        /// Converts vector's elements to other using conversion function.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <typeparam name="TResult">Destination type.</typeparam>
        /// <param name="vector"></param>
        /// <param name="converter">Conversion function.</param>
        /// <returns>The vector which stores the result of the conversion.</returns>
        public static TResult[] ToOther<T, TResult>(this T[] vector, Func<T, TResult> converter)
        {
            Contract.Requires<ArgumentNullException>(converter != null);
            Contract.Ensures(Contract.Result<TResult[]>().Length == vector.Length);
            var length = vector.Length;
            var newVector = new TResult[length];
            Do.For(0, length, i => newVector[i] = converter.Invoke(vector[i]));
            return newVector;
        }

        /// <summary>
        ///Converts vector's elements to other using conversion function.
        /// Second parameter of convertion function loop's index.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <typeparam name="TResult">Destination type.</typeparam>
        /// <param name="vector"></param>
        /// <param name="converter">Conversion function.
        /// First argument is loop iteration index.</param>
        /// <returns>The vector which stores the result of the conversion.</returns>
        public static TResult[] ToOther<T, TResult>(this T[] vector, Func<T, int, TResult> converter)
        {
            Contract.Requires<ArgumentNullException>(converter != null);
            Contract.Ensures(Contract.Result<TResult[]>().Length == vector.Length);
            var length = vector.Length;
            var newVector = new TResult[length];
            Do.For(0, length, i => newVector[i] = converter.Invoke(vector[i], i));
            return newVector;
        }

    }
}