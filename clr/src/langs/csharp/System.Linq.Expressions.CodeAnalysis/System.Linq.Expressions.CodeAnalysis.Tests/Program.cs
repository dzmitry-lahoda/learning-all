using System;

namespace System.Linq.Expressions.CodeAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var sut = new ExpressionToSyntaxTreeTests();
            sut.MultipleStatementsBlockTest();
        }
    }
}
