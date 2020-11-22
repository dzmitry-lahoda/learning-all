using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csharp
{
	public partial class Model
	{
		//BUG: properties cannot be partial
		public Guid Prop { get; set; }

		///PARTIAL only private
		partial void Do();

		public void Do1() { }
	}
}
