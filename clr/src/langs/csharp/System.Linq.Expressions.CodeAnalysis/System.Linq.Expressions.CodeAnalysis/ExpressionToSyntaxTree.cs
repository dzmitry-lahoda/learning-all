using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq.Expressions.CodeAnalysis
{
    public static class ExpressionToSyntaxTree
    {
        //https://stackoverflow.com/questions/48790497/how-to-convert-expression-into-csharpcompilation-or-csharpsyntaxtree
        //http://roslynquoter.azurewebsites.net

        //    TokenList(
        //Identifier("p")))),
        //            Trivia(
        //                SkippedTokensTrivia()
        //                .WithTokens(
        //                    TokenList(
        //                        Token(SyntaxKind.EqualsToken)))),
        //            Trivia(
        //                SkippedTokensTrivia()
        //                .WithTokens(
        //                    TokenList(
        //                        Literal(1))))}),

        public static SyntaxTree ToSyntaxTree(this Expression self)
        {
            var current = new Stack<Expression>();
            current.Push(self);
            while (current.Any())
            {
                switch (current.Pop())
                {
                    case ConstantExpression c when c.Type == typeof(int):
                        var constantToken = SyntaxFactory.Literal((int)c.Value);
                        var constant = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, constantToken);
                        return SyntaxFactory.SyntaxTree(constant);
                    case ParameterExpression p when p.NodeType == ExpressionType.Parameter:
                        var parameterToken = SyntaxFactory.Parameter(new SyntaxList<AttributeListSyntax>(), SyntaxTokenList.Create(new SyntaxToken()), null, SyntaxFactory.Identifier(p.Name), null);
 
                        return SyntaxFactory.SyntaxTree(parameterToken);
                    default:
                        return null;
                }
            }

            return null;
        }
    }
}
