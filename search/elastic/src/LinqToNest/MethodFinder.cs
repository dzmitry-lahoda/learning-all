using System.Linq.Expressions;

namespace Nest
{
  internal class MethodFinder : ExpressionVisitor
  {
    private MethodCallExpression _expression;

    private readonly string _methodName;

    public MethodFinder(string methodName)
    {
      _methodName = methodName;
    }

    public MethodCallExpression GetMethod(Expression expression)
    {
      Visit(expression);
      return _expression;
    }

    protected override Expression VisitMethodCall(MethodCallExpression expression)
    {
      if (expression.Method.Name.Equals(_methodName))
        _expression = expression;

      Visit(expression.Arguments[0]);

      return expression;
    }
  }
}