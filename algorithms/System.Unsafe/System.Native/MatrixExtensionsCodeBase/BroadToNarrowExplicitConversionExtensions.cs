//***********************************************************************
// Assembly         : MatrixExtensionCodeBase
// Author           : asd.and.Rizzo
// Created          : 09-21-2009
//
// Last Modified By : asd.and.Rizzo
// Last Modified On : 10-28-2009
// Description      : 
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;


using MatrixExtensions.Generic;
using MatrixExtensionsGenericCodeBase;

namespace MatrixExtensions.Conversion.Explicit
{
    /// <summary>
    /// Provides extension methods for numeric arrays conversion with lost of precision or data range.
    /// </summary>
   [Pure]
    public static class BroadToNarrowExplicitConversionExtensions
    {
        /// <summary>
        /// Converts vector of one numerical type to other with lost of data.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>The vector containing result of conversion.</returns>
        public static NumericNarrow[] ToNumericNarrow(this NumericBroad[] vector)
        {
            var length = vector.Length;
            var newVector = new NumericNarrow[length];
            if (vector.IsEmpty())
            {
                return newVector;
            }
            Do.For(0,length, i => newVector[i] = (NumericNarrow)vector[i]);
            return newVector;
        }

        /// <summary>
        /// Converts matrix of one numerical type to other with lost of data.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>The matrix containing result of conversion.</returns>
        public static NumericNarrow[,] ToNumericNarrow(this NumericBroad[,] matrix)
        {
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newMatrix = new NumericNarrow[rowCount, columnCount];
            if (matrix.IsEmpty())
            {
                return newMatrix;
            }
            Do.For2(0,rowCount,0,columnCount,(r,c) => newMatrix[r, c] = (NumericNarrow) matrix[r, c] );
            return newMatrix;
        }

      

    }
}