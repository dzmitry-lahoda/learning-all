using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;using System.Threading.Tasks;
using System.Threading.Tasks;
using MatrixExtensions.Generic;

namespace MatrixExtensions.Manipulation.Generic
{
    /// <summary>
    /// Provides generic extension methods for 2D arrays manipulation.
    /// </summary>
    [Pure]
    public static class GenericArray2DManipulationExtensions
    {

        /// <summary>
        /// Returns the transpose of this matrix.
        /// </summary>        
        /// <seealso href="http://en.wikipedia.org/wiki/Transpose"/>
        /// <returns>The transpose of this matrix.</returns>
        public static T[,] Transpose<T>(this T[,] matrix)
        {
            Contract.Ensures(Contract.Result<T[,]>().ColumnCount() == matrix.RowCount());
            Contract.Ensures(Contract.Result<T[,]>().RowCount() == matrix.ColumnCount());
            var columnCount= matrix.RowCount();
            var rowCount = matrix.ColumnCount();
            var transposedMatrix = new T[rowCount, columnCount];
            Parallel.For(0, rowCount, r => Parallel.For(0, columnCount, c =>
                                                                            {

                                                                                transposedMatrix[r, c] = matrix[columnCount-1 - c,r ];
                                                                            }));
            return transposedMatrix;
        }
    }
}