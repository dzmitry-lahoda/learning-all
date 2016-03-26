using System.Linq.Expressions;

namespace Nest
{
	public interface IExpressionConverter<out TResult>
  {
		TResult Convert(Expression expression);
  }
}
