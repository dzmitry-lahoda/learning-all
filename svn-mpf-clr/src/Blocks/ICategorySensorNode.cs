/*
 * Created by SharpDevelop.
 * User: asd
 * Date: 17.09.2009
 * Time: 12:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace dnHTM
{
	/// <summary>
	/// Description of ICategorySensorNode.
	/// </summary>
	public interface ICategorySensorNode
	{
			int[] ReadCategories(Stream stream);
	}
}
