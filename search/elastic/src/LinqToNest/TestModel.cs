using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public class TestModel
	{
		public string Prop1 { get; set; }

		public string Id { get; set; }
		public IEnumerable<string> Prop2Enum { get; set; } 
	}
}
