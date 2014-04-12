/*
 * Created by SharpDevelop.
 * User: asd
 * Date: 17.09.2009
 * Time: 13:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace dnHTM
{
	/// <summary>
	/// Description of CategoryVectorFileSensor.
	/// </summary>
	public class CategoryVectorFileSensor:ICategorySensorNode
	{
		public CategoryVectorFileSensor()
		{
		}

	    public int[] ReadCategories(Stream stream)
	    {
	        throw new NotImplementedException();
	    }
	}
}
