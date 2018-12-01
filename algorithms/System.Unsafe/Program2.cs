using System;
using Xunit;
namespace run
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                long al = 1L + Int32.MaxValue;
                var a = new bool[al];
            }
            catch (System.OverflowException ex)
            {
                
            }


            var sut = new TreeHeightMerge();
            sut.Test();
        }
    }
}