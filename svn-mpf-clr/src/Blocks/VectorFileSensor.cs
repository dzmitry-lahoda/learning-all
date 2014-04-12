/*
 * Created by SharpDevelop.
 * User: asd
 * Date: 16.09.2009
 * Time: 18:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using Blocks;

namespace dnHTM
{
	/// <summary>
	/// Reads csv or txt
	/// </summary>
	public class VectorFileSensor:ISensorNode
	{
		
		private double[,] _data;
		
		public VectorFileSensor()
		{
			
		}
		
		public void Execute()
		{
			
	    }
		
		public virtual void LoadFile(string url)
		{
			var asd = new DoubleMatrixReader(" ");
		}


	    public double[,] ReadData(Stream stream, int featureVectorLength)
	    {
	        throw new NotImplementedException();
	    }
	}
}
