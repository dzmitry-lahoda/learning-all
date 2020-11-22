using ExpressionToCodeLib;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;
using System;

namespace System.Linq.Expressions.CodeAnalysis
{
    [TestFixture]
    public class ExpressionToSyntaxTreeTests
    {
        [Test]
        public void MultipleStatementsBlockTest()
        {
            var e = Expression.Assign(Expression.Parameter(typeof(int), "p"), Expression.Constant(1));
            //Expression addAssignment = Expression.AddAssign(p, Expression.Constant(5));
            var adHock = ExpressionToCode.ToCode(e);
            var roslyn = ExpressionToSyntaxTree.ToSyntaxTree(e).ToString();
            PAssert.That(() => adHock == roslyn);
        }

        [Test]
        public void CanPrettyPrintVariousIndexers()
        {
            Expression<Func<bool>> e = () => new HasIndexers()[3] == new HasIndexers()["three"];
            var code = ExpressionToCodeLib.ExpressionToCode.ToCode(e);
            //() => new ExpressionToSyntaxTreeTests.HasIndexers()[3] == new ExpressionToSyntaxTreeTests.HasIndexers()["three"]
            var code2 = ExpressionToSyntaxTree.ToSyntaxTree(e);
        }

        class HasIndexers
        {
            public object this[string s] => null;

            public object this[int i] => null;
        }

        [Test]
        public void MultipleStatementsBlockTest1()
        {
            var e = Expression.Parameter(typeof(int), "p");
            var adHock = ExpressionToCode.ToCode(e);
            var roslyn = ExpressionToSyntaxTree.ToSyntaxTree(e).ToString();
            PAssert.That(() => adHock == roslyn);
        }

        [Test]
        public void Constant()
        {
            var e = Expression.Constant(1);
            var adHock = ExpressionToCode.ToCode(e);
            var roslyn = e.ToSyntaxTree().ToString();
            PAssert.That(()=> adHock == roslyn);
        }
    }
}
