using System;

namespace usafefx
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}


//1

//4 3
//2 1

//9 8 7
//2 1 6
//3 4 5

//0 0 0 0
//0 3 4 0
//0 2 1 0
//0 0 0 0


//int[][] matrix;
//int totalRows, totalColumns;

//var center = r / 2;
//var row, column = c;
//var v = 1;
//enum direction = {none, left,top,right, bottom};
//direction previous = none;
//// 2  2
//while(true)
//{  

//   matrix[row, column] = v;
//   //      1       true                 2    1   true          true                 
//   if (column-1 >= 0  && matrix[row, column - 1] == 0 && (previous != left || matrix[row - 1, column] != 0 )
//   {
//      previous = left;
//      // 2 1
//      column--;
//   }  
//   else if (row-1 >= 0 && matrix[row - 1, column] == 0  && (previous != top || matrix[row, column + 1] != 0))
//   {
//       previous = top;
//       row--;
//   }
//   else if (column+1 < totalColumns  && matrix[row, column + 1] == 0  && (previous != right || matrix[row + 1, column] != 0))
//   {
//      previous = right;
//      column++;
//   } 
//   else if (row+1 < totalRows && matrix[row + 1, column] == 0 && (previous != bottom || matrix[row, column - 1] != 0))
//   {
//      previous = bottom;
//      row++;
//   }
//   else break;
   
//   v++;    
//}



