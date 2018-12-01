using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;

using MatrixExtensions.Generic;
using MatrixExtensionsGenericCodeBase;

namespace MatrixExtensions.Equality
{
    /// <summary>
    /// Provides extension methods for numeric arrays related to equality checking.
    /// </summary>
    [Pure]
    public static class GenericArray2DEqualityExtensions
    {

        /// <summary>
        /// Determines whether the specified matrix elements are 
        /// pairwise equal to current matrix's elements.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="matrix"></param>
        /// <param name="other">The other matrix.</param>
        /// <returns>True, if all correspondent elements are equal;otherwise, false.</returns>
        public static bool EqualsElements<T1, T2>(this T1[,] matrix, T2[,] other)
        {
            Contract.Requires<ArgumentNullException>(other != null, "other");
            Contract.Requires<ArgumentOutOfRangeException>(matrix.RowCount() == other.RowCount());
            Contract.Requires<ArgumentOutOfRangeException>(matrix.ColumnCount() == other.ColumnCount());
            bool result = Do.For2UntilFalse(0, matrix.RowCount(), 0, matrix.ColumnCount(),
                (r, c) => matrix[r, c].Equals(other[r, c]));
            return result;
        }

        /// <summary>
        /// Determines whether the specified matrix elements are 
        /// pairwise equal to current matrix's elements.
        /// Matrix elements considered equal if <see cref=" Func{T1,T2,TResult}"/>
        /// invocation returns true. 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="matrix"></param>
        /// <param name="other">The other matrix.</param>
        /// <param name="equals">The delegate which checks elements for equality.</param>
        /// <returns>True, if all correspondent elements are equal;otherwise, false.</returns>
        public static bool EqualsElements<T1, T2>(this T1[,] matrix, T2[,] other, Func<T1, T2, bool> equals)
        {
            Contract.Requires<ArgumentNullException>(other != null, "other");
            Contract.Requires<ArgumentOutOfRangeException>(matrix.RowCount() == other.RowCount(), "other.RowCount()");
            Contract.Requires<ArgumentOutOfRangeException>(matrix.ColumnCount() == other.ColumnCount(), " other.ColumnCount()");
            bool result =
                Do.For2UntilFalse(0, matrix.RowCount(), 0, matrix.ColumnCount(),
                                  (r, c) => equals.Invoke(matrix[r, c], other[r, c]));
            return result;
        }
    }
}