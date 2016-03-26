using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public interface IPropertyNameResolver
	{
		string ResolvePropertyName(Expression expression);
	}
}
