/*
 * Created by SharpDevelop.
 * User: asd
 * Date: 17.09.2009
 * Time: 12:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace dnHTM
{
	/// <summary>
	/// Description of ISensorNode.
	/// </summary>
	public interface ISensorNode
	{
			double[,] ReadData(Stream stream,int featureVectorLength);
	}
}
