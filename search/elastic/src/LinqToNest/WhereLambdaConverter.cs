using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Nest;

namespace Nest
{
	internal class WhereLambdaConverter : ExpressionVisitor, IExpressionConverter<IEnumerable<Func<FilterDescriptor<object>, FilterContainer>>>
	{
		private readonly IPropertyNameResolver _propertyNameResolver;
		private readonly List<Func<FilterDescriptor<object>, FilterContainer>> _result; 
		
		public WhereLambdaConverter(IPropertyNameResolver propertyNameResolver)
		{
			this._propertyNameResolver = propertyNameResolver;
			this._result = new List<Func<FilterDescriptor<object>, FilterContainer>>();
		}

		public IEnumerable<Func<FilterDescriptor<object>, FilterContainer>> Convert(Expression expression)
		{
			this.Visit(expression);
			return this._result;
		}

		protected override Expression VisitBinary(BinaryExpression node)
		{
			switch (node.NodeType)
			{
				case ExpressionType.Equal:
					{
						var propertyName = _propertyNameResolver.ResolvePropertyName(node.Left);
						var right = node.Right as ConstantExpression;
						if (right != null)
						{
							if (right.Value == null)
							{
								this._result.Add(x => x.Bool(b => b.Must(f => f.Not(n => n.Exists(propertyName)))));
							}
							else
							{
								this._result.Add(x => x.Bool(b => b.Must(f => f.Term(propertyName, right.Value))));
							}
						}
						else
						{
							var evaluator =  Expression.Lambda(node.Right);
							var value = evaluator.Compile().DynamicInvoke();
							this._result.Add(x => x.Bool(b => b.Must(f => f.Term(propertyName, value))));
						}

						break;
					}
				case ExpressionType.NotEqual:
					{
						var propertyName = _propertyNameResolver.ResolvePropertyName(node.Left);
						var right = node.Right as ConstantExpression;
						if (right != null)
						{
							if (right.Value == null)
							{
								this._result.Add(x => x.Bool(b => b.Must(n => n.Exists(propertyName))));
							}
							else
							{
								this._result.Add(x => x.Bool(b => b.Must(g => g.Not(f => f.Term(propertyName, right.Value)))));
							}
						}
						else
						{
							var evaluator = Expression.Lambda(node.Right);
							var value = evaluator.Compile().DynamicInvoke();
							this._result.Add(x => x.Bool(b => b.Must(g => g.Not(f => f.Term(propertyName, value)))));
						}

						break;
					}
			}
			
			return base.VisitBinary(node);
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			var member = node.Arguments[0] as MemberExpression;
			LambdaExpression lambda = Expression.Lambda(node.Object);
			var fn = lambda.Compile();
			var collection = fn.DynamicInvoke();
			
			switch (node.Method.Name)
			{
				case "Contains":
					{
						this._result.Add(f => f.Bool(b => b.Must(fd => fd.Terms(_propertyNameResolver.ResolvePropertyName(member), collection as IEnumerable<string>))));
						break;
					}
			}
			return base.VisitMethodCall(node);
		}
	}
}