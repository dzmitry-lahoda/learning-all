


        /// <summary>
        ///Gets new matrix which elements are per element division with divisor.
        /// </summary>
        /// <see cref="http://www.mathworks.com/access/helpdesk/help/techdoc/ref/arithmeticoperators.html"/>
        /// <param name="matrix"></param>
        /// <param name="value">Divisor.</param>
        /// <returns></returns>
        public static Numeric[,] DivideEachElement(this Numeric[,] matrix, Numeric value)
        {
            Contract.Requires<ArgumentException>(value != 0);
            Contract.Ensures(Contract.Result<Numeric[,]>().RowCount() == matrix.RowCount());
            Contract.Ensures(Contract.Result<Numeric[,]>().ColumnCount() == matrix.ColumnCount());
            var rows = matrix.RowCount();
            var columns = matrix.ColumnCount();
            var newMatrix = new Numeric[rows, columns];
            Parallel.For(0, rows, i =>
            {
                Parallel.For(0, columns, j =>
                {
                    matrix[i, j] /= value;
                });
            });
            return newMatrix;
        }

    /// <summary>
        ///B = reshape(A,m,n) returns the m-by-n matrix B whose elements are taken column-wise from A.
        ///An error results if A does not have m*n elements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vector"></param>
        ///<param name="horizontalTilingCount"></param>
        ///<param name="verticalTilingCount"></param>
        /// <returns></returns>
        public static T[,] Reshape<T>(this T[,] matrix, int verticalTilingCount, int horizontalTilingCount)
        {
            Contract.Requdires(verticalTilingCount >= 1);
            Contract.Requdires(horizontalTilingCount >= 1);
            throw new NotImplementedException();
        }


        /// <summary>
        /// Creates a large matrix consisting of an tiling of copies of current array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vector"></param>
        ///<param name="horizontalTilingCount"></param>
        ///<param name="verticalTilingCount"></param>
        /// <returns></returns>
        public static T[,] Replicate<T>(this T[,] matrix, int verticalTilingCount, int horizontalTilingCount)
        {
            Contract.Requiress(verticalTilingCount >= 1);
            Contract.Requisres(horizontalTilingCount >= 1);
            throw new NotImplementedException();
        }

        //Shift
        //Circshift


        /// <summary>
        /// Pointwise multiplies this matrix with another matrix and stores the result into new matrix.
        /// </summary>
        /// <param name="other">The matrix to pointwise multiply with this one.</param>
        /// <returns>The matrix which stores the result of the pointwise multiplication.</returns>
        public static NumericBroad[,] PointwiseDivide(this NumericBroad[,] matrix, NumericNarrow[,] otherMatrix)
        {
            Contract.Requires<ArgumentNullException>(otherMatrix != null);
            Contract.Requires<ArgumentException>(matrix.RowCount() == otherMatrix.RowCount());
            Contract.Requires<ArgumentException>(matrix.ColumnCount() == otherMatrix.ColumnCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().ColumnCount() == matrix.ColumnCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().RowCount() == matrix.RowCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newMatrix = new NumericBroad[rowCount, columnCount];
            Parallel.For(0, rowCount, i =>
            {
                Parallel.For(0, columnCount, j => newMatrix[i, j] = matrix[i, j] / otherMatrix[i, j]);

            });
            return newMatrix;
        }
        
                /// <summary>
        /// Multiples two <strong>Vectors</strong>. The first vector is treated as column vector and
        /// the second as row vector.
        /// </summary>
        /// <param name="leftSide">One of the vectors to multiply.</param>
        /// <param name="rightSide">One of the vectors to multiply.</param>
        /// <returns>The result of multiplication.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="leftSide"/> or <paramref name="rightSide"/> is <see langword="null" />.</exception>
        public static DenseMatrix operator *
        
        
        /// <summary>
        /// Gets new vector which elements are per element division by other vector elements.
        /// </summary>
        /// <see cref="http://www.mathworks.com/access/helpdesk/help/techdoc/ref/arithmeticoperators.html"/>
        /// <param name="vector"></param>
        /// <param name="otherVector">Vector which elements are divisor for current vector elements.</param>
        /// <returns></returns>
        public static NumericBroad[] PointwiseDivide(this NumericBroad[] vector, NumericNarrow[] other)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentException>(vector.Length == other.Length);
            Contract.Ensures(Contract.Result<NumericBroad[]>().Length == vector.Length);
            var length = vector.Length;
            var newVector = new NumericBroad[length];
            Parallel.For(0, length, i =>
                                        {
                                            newVector[i] = vector[i] / other[i];
                                        });
            return newVector;
        }
        
        
        
                /// <summary>
        /// Normalizes the columns of a matrix.
        /// </summary>
        /// <param name="pValue">The norm under which to normalize the columns under.</param>
        /// <returns>A normalized version of the matrix.</returns>
        public virtual Matrix NormalizeColumns(int pValue)
        {
            if (pValue < 1)
            {
                throw new ArgumentOutOfRangeException("pValue", Resources.NotPositive);
            }

            Matrix ret = Clone();
            for (int i = 0; i < Columns; i++)
            {
                Vector coli = GetColumn(i);
                double norm = coli.PNorm(pValue);
                for (int j = 0; j < Rows; j++)
                {
                    ret[j, i] = coli[j]/norm;
                }
            }

            return ret;
        }
        
        
            /// <summary>
    /// Provides extension methods for 2D numeric arrays statistics.
    /// </summary>
    [Pure]
    public static class NumericStatisticsArray2DExtensions
    {
        public static Numeric[] AveragePerColumn(this Numeric[,] matrix)
        {
            Contract.Ensures(Contract.Result<Numeric[,]>().ColumnCount() == matrix.ColumnCount());
            Contract.Ensures(Contract.Result<Numeric[,]>().RowCount() == matrix.RowCount());
            var rowCount = matrix.RowCount();
            var newVector = matrix.SumPerColumn();
            newVector = newVector.DivideEachElement(rowCount);
            return newVector;
        }

        public static Numeric[] AveragePerRow(this Numeric[,] matrix)
        {
            Contract.Ensures(Contract.Result<Numeric[,]>().ColumnCount() == matrix.ColumnCount());
            Contract.Ensures(Contract.Result<Numeric[,]>().RowCount() == matrix.RowCount());
            var columnCount = matrix.ColumnCount();
            var newVector = matrix.SumPerRow();
            newVector = newVector.DivideEachElement(columnCount);
            return newVector;
        }
    }
    
    
    /// <summary>
    /// Provides extension methods for 1D numeric arrays statistics.
    /// </summary>
    [Pure]
    public static class NumericStatisticsArrayExtensions
    {
        //Mean
        //Median
        //Kurtosis
        //Variance
        //StandartDeviation
    }


namespace MatrixExtensions.Generic
{
    /// <summary>
    /// Provides generic extension methods for concatenation of 1D arrays.
    /// </summary>
    /// <see cref="http://www.mathworks.com/access/helpdesk/help/techdoc/ref/cat.html"/>
    /// <see cref="http://www.mathworks.com/access/helpdesk/help/techdoc/ref/vertcat.html"/>
    /// <see cref="http://www.mathworks.com/access/helpdesk/help/techdoc/ref/horzcat.html"/>
    [Pure]
    public static class ConcatenationArrayExtensions
    {

        /// <summary>
        /// Concatentes current vector with another.
        /// Vector considered to be a row.
        /// <example><pre>
        /// 1, 2  3
        /// append    will be returned as  1, 2, 7, 8, 9
        /// 7, 8, 9
        /// </pre></example>
       /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vector"></param>
        /// <returns>New longer vector.</returns>
        public static T[] Append<T>(this T[] vector, T[] right)
        {
            Contract.Requires<ArgumentNullException>(vector != null);
            var appendLength = right.Length;
            if (appendLength == 0)
            {
                return (T[])vector.Clone();
            }
            var length = vector.Length;
            var newLength = length + appendLength;
            var newVector = new T[newLength];
            Parallel.Invoke(
                () => Parallel.For(0, length, i => newVector[i] = vector[i]),
                () => Parallel.For(length, newLength, i => newVector[i] = right[i - length])
                );
            return newVector;
        }

        /// <summary>
        /// Stack to bottom new vector to current.
        /// Vector considered to be a row.
        /// <example><pre>
        /// 1, 2  3
        /// stack    will be returned as  1, 2, 3
        /// 7, 8, 9                       7, 8, 9
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vector"></param>
        /// <param name="bottom"></param>
        /// <returns>Matrix with 2 rows.</returns>
        public static T[,] Stack<T>(this T[] vector, T[] bottom)
        {
            Contract.Requires<ArgumentNullException>(bottom != null, "bottom");
            Contract.Requires<ArgumentOutOfRangeException>(vector.Length == bottom.Length, "bottom.Length");
            var length = vector.Length;
            var newMatrix = new T[2, length];
            if (length == 0)
            {
                return newMatrix;
            }
            Parallel.Invoke(
                () => Parallel.For(0, length, i => newMatrix[i, 0] = vector[i]),
                () => Parallel.For(0, length, i => newMatrix[i, 1] = bottom[i])
                );
            return newMatrix;
        }

        /// <summary>
        /// Appends to rigth matrix to current vector.
        /// Vector considered to be a column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vector"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static T[,] Append<T>(this T[] vector, T[,] matrix)
        {
            Contract.Requires<ArgumentException>(vector.Length == matrix.RowCount());
            Contract.Ensures(Contract.Result<T[,]>().ColumnCount() == matrix.ColumnCount() + 1);
            Contract.Ensures(Contract.Result<T[,]>().RowCount() == matrix.RowCount());
            var rows = vector.Length;
            var columns = matrix.ColumnCount() + 1;
            var result = new T[rows, columns];
            Parallel.Invoke(
            () => Parallel.For(0, rows, i =>
            {
                for (var j = 1; j < columns; j++)
                {
                    result[i, j] = matrix[i, j-1];
                }
            }),
            () => Parallel.For(0, rows, r =>
            {
                result[r, 0] = vector[r];
            })
                                      );
            return result;
        }

        /// <summary>
        /// Stacks to bottom matrix to current vector.
        /// Vector considered to be a row.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static T[,] Stack<T>(this  T[] vector, T[,] matrix)
        {
            Contract.Requires<ArgumentException>(vector.Length == matrix.ColumnCount());
            Contract.Ensures(Contract.Result<T[,]>().ColumnCount() == matrix.ColumnCount());
            Contract.Ensures(Contract.Result<T[,]>().RowCount() == matrix.RowCount()+1);
            var columntCount = vector.Length;
            var rowCount = matrix.RowCount() + 1;
            var result = new T[rowCount, columntCount];
            Parallel.Invoke(
            () => Parallel.For(1, rowCount, r =>
            {
                for (var c = 0; c < columntCount; c++)
                {
                    result[r, c] = matrix[r-1, c];
                }
            }),
            () => Parallel.For(0, columntCount, c =>
            {
                result[0, c] = vector[c];
            })
                                      );
            return result;
        }

     
    }
}

namespace MatrixExtensions.Generic
{
    /// <summary>
    /// Provides generic extension methods for concatenation of 2D arrays.
    /// </summary>
    /// <see cref="http://www.mathworks.com/access/helpdesk/help/techdoc/ref/cat.html"/>
    /// <see cref="http://www.mathworks.com/access/helpdesk/help/techdoc/ref/vertcat.html"/>
    /// <see cref="http://www.mathworks.com/access/helpdesk/help/techdoc/ref/horzcat.html"/>
    [Pure]
    public static class ConcatenationArray2DExtensions
    {
        /// <summary>
        /// Concatenates current matrix with the given vector and places the result into the result matrix.
        /// Vector considered to be a column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <param name="right">The vector to concatenate.</param>
        /// <returns>The combined matrix.</returns>
        public static T[,] Append<T>(this T[,] matrix, T[] right)
        {
            Contract.Requires<ArgumentException>(matrix.RowCount() == right.Length);
            Contract.Ensures(Contract.Result<T[,]>().ColumnCount() == matrix.ColumnCount() + 1);
            var rows = right.Length;
            var columns = matrix.ColumnCount() + 1;
            var result = new T[rows, columns];
            Parallel.Invoke(
            () => Parallel.For(0, rows, i =>
            {
                for (var j = 0; j < columns - 1; j++)
                {
                    result[i, j] = matrix[i, j];
                }
            }),
            () => Parallel.For(0, rows, i =>
            {
                result[i, columns - 1] = right[i];
            })
                                      );
            return result;
        }

        /// <summary>
        /// Concatenates current matrix with the given matrix and places the result into the result matrix.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <param name="right">The matrix to concatenate.</param>
        /// <returns>The combined matrix.</returns>
        public static T[,] Append<T>(this T[,] matrix, T[,] right)
        {
            Contract.Requires<ArgumentException>(matrix.RowCount() == right.RowCount());
            Contract.Ensures(Contract.Result<T[,]>().ColumnCount() == matrix.ColumnCount() + right.ColumnCount());
            var rows = matrix.RowCount();
            var columnsInitial = matrix.ColumnCount();
            var columnsOfConcat = right.ColumnCount();
            var columns = columnsInitial + columnsOfConcat;
            var result = new T[rows, columns];
            Parallel.Invoke(
            () => Parallel.For(0, rows, i =>
            {
                for (var j = 0; j < columnsInitial; j++)
                {
                    result[i, j] = matrix[i, j];
                }
            }),
            () => Parallel.For(0, rows, i =>
            {
                for (var j = columnsInitial; j < columns; j++)
                {
                    result[i, j] = matrix[i, j];
                }
            })
                                      );
            return result;
        }

        /// <summary>
        /// Concatenates current matrix with the given vector and places the result into the result matrix.
        /// Vector considered to be a row.
         /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <param name="bottom">The vector to concatenate.</param>
        /// <returns>The combined matrix.</returns>
        public static T[,] Stack<T>(this T[,] matrix, T[] bottom)
        {
            Contract.Requires<ArgumentException>(matrix.ColumnCount() == bottom.Length);
            Contract.Ensures(Contract.Result<T[,]>().RowCount() == matrix.RowCount() + 1);
            var columns = bottom.Length;
            var rows = matrix.RowCount() + 1;
            var result = new T[rows, columns];
            Parallel.Invoke(
            () => Parallel.For(0, rows - 1, i =>
            {
                for (var j = 0; j < columns; j++)
                {
                    result[i, j] = matrix[i, j];
                }
            }),
            () => Parallel.For(0, columns, i =>
            {
                result[rows - 1, i] = bottom[i];
            })
                                      );
            return result;
        }

        /// <summary>
        /// Concatenates current matrix with the given matrix and places the result into the result matrix.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <param name="bottom">The matrix to concatenate.</param>
        /// <returns>The combined matrix.</returns>
        public static T[,] Stack<T>(this T[,] matrix, T[,] bottom)
        {
            Contract.Requires<ArgumentException>(matrix.ColumnCount() == bottom.ColumnCount());
            Contract.Ensures(Contract.Result<T[,]>().RowCount() == matrix.RowCount() + bottom.RowCount());

            var columns = matrix.ColumnCount();
            var rowsInitial = matrix.RowCount();
            var rowsOfConcat = bottom.RowCount();
            var rows = rowsInitial + rowsOfConcat;
            var result = new T[rows, columns];
            Parallel.Invoke(
            () => Parallel.For(0, columns, i =>
            {
                for (var j = 0; j < rowsInitial; j++)
                {
                    result[j, i] = matrix[j, i];
                }
            }),
            () => Parallel.For(0, columns, i =>
            {
                for (var j = rowsInitial; j < rows; j++)
                {
                    result[j, i] = matrix[j, i];
                }
            })
                                      );
            return result;
        }
        

    }
}

