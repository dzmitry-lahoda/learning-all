using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Nest
{
	class Program
	{
		static void Main(string[] args)
		{
			Debugger.Break();
			Test("value");
			
		}

		private static void Test(string val)
		{
			var collection = new List<string> { "a", "b" };
			var test = new TestModel { Id = "qverty" };
			Expression<Func<TestModel, bool>> exp = a => a.Prop1 == null && collection.Contains(a.Id) && a.Prop2Enum != null && a.Id != test.Id && a.Prop1 == val;
			var result = new WhereLambdaConverter(new PropertyNameResolver()).Convert(exp);
		}
	}
}
