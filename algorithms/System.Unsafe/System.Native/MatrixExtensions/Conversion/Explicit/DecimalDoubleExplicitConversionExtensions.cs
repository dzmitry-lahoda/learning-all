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
    public static class DecimalDoubleExplicitConversionExtensions
    {
        /// <summary>
        /// Converts vector of one numerical type to other with lost of data.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>The vector containing result of conversion.</returns>
        public static double[] ToDouble(this decimal[] vector)
        {
            var length = vector.Length;
            var newVector = new double[length];
            if (vector.Length == 0)
            {
                return newVector;
            }
            Do.For(0, length, i => newVector[i] = (double)vector[i]);
            return newVector;
        }

        /// <summary>
        /// Converts matrix of one numerical type to other with lost of data.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>The matrix containing result of conversion.</returns>
        public static double[,] ToDouble(this decimal[,] matrix)
        {
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var newMatrix = new double[rowCount, columnCount];
            if (matrix.Length == 0)
            {
                return newMatrix;
            }
            Do.For2(0,rowCount,0,columnCount,(r,c) => newMatrix[r, c] = (double) matrix[r, c] );
            return newMatrix;
        }

      

    }
}