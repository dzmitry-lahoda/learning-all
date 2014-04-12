using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVM;

namespace dnHTM.Extensions
{
    public static class ConversionExtensions
    {


        public static Node[][] GetJaggedArray(this double[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);
            var jagged = new Node[rows][];
            for (var i = 0; i < jagged.Length; i++)
            {
                jagged[i] = new Node[columns];
            }

            for (var i = 0; i < rows; i++)
            {
                var index = 0;
                for (var j = 0; j < columns; j++)
                {
                    index++;
                    jagged[i][j] = new Node(index,matrix[i, j]);
                }
            }
            return jagged;
        }
    }
}
