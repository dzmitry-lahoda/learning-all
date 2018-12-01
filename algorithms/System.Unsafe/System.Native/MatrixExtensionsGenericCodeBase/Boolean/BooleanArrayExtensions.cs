using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;
using MatrixExtensionsGenericCodeBase;

namespace MatrixExtensions.Boolean
{
    /// <summary>
///Provides extension methods for boolean arrays.
    /// </summary>
    [Pure]
    public static class BooleanArrayExtensions
    {

        /// <summary>
        /// Transforms <see cref="Array"/> of <see cref="bool"/> to 
        /// <see cref="Array"/> of <see cref="int"/>.
        /// False to 0, true to 1.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static int[] ToInt(this bool[] vector)
        {
            Contract.Ensures(Contract.Result<int[]>().Length == vector.Length);
            var length = vector.Length;
            var integers = new int[length];
            if (length==0)
            {
                return integers;
            }
            Do.For(0, length, 
                i =>integers[i] = vector[i] ? 1 : 0);
            return integers;
        }

    }
}