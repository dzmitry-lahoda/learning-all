using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public sealed class PropertyNameResolver : IPropertyNameResolver
	{
		public string ResolvePropertyName(Expression expression)
		{
			MemberExpression memberExpression = expression as MemberExpression;
			return memberExpression != null ? memberExpression.Member.Name : string.Empty;
		}
	}
}
