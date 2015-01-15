/*
 * Created by SharpDevelop.
 * User: asd
 * Date: 16.09.2009
 * Time: 18:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using MPF.Blocks;

namespace dnHTM
{
	/// <summary>
	/// The SpatialPoolerNode has two phases of operation.
///• During the learning phase, it stores coincidence patterns, optionally after sparsifying them. A
///pattern that is stored represents a co-occurrence of the temporal groups from the spatial
///pooler's child nodes.
///• During the inference phase, the node produces an output for every input pattern by comparing
///the input pattern against all the stored patterns. The output is a vector that denotes the
///degree of match of the input pattern to all the stored patterns.
///The key tuning parameters for SpatialPoolerNode are maxCoincidenceCount, max-
///Distance, sparsify, and sigma.
	/// </summary>
	public class SpatialPoolerHtmNode
	{
		public SpatialPoolerHtmNode()
		{
		}
		
		#region Properties
			///<summary>
			/// 		Spatial Pooler Node Inputs
	///		The input to the spatial pooler node from its children. The input typically
///comes from child temporal pooler nodes or from a sensor.

			///</summary>
			public Input[] BottomUpIn{get;set;}
			
///<summary>
///Spatial Pooler Node Outputs
///The output vector of the spatial pooler node. This output is meaningful only
///when the spatial pooler is in inference mode. The output vector indicates
///how well an input pattern matches stored coincidence patterns.
///</summary>
			public Output[] bottomUpOut{get;set;}

	
		
		///<summary>
		///Determines the maximum number of coincidence patterns that can be
		///	stored in a node. Specified at the time of node creation, determines the
		///	maximum number of coincidence patterns that can be stored in a node.
		///	You can think of this parameter as the capacity of the node. This is a key
		///	parameter affecting network performance. Storing too few coincidence patterns
		///	can result in loss of accuracy due to loss of information. Storing too
		///	many coincidence patterns can result in lower generalization and longer
		///	training times.
		/// </summary>
		public int MaxCoincidenceCount{get;set;}
		
		///<summary>
		///Specifies the distance by which an input pattern has to differ from a stored
		///	pattern in order to be considered as a different pattern for storage. An incoming
		///	pattern that is within the MaxDistance of any of the stored patterns
		///	is not stored. MaxDistance should be higher for noisy inputs.
		/// </summary>
		public double MaxDistance{get;set;}
	    ///<summary>
///		Specifies whether a stored pattern is sparsified or not. Sparsifying a pattern
///means setting most components to zero. In domains like vision and speech,
///sparsifying the stored coincidence patterns increases the recognition and
///generalization performance.
	/// </summary>
		public bool Sparsify{get;set;}
			    ///<summary>
///		During a node's inference stage, each input pattern is compared to the
///stored patterns assuming that the stored patterns are centers of radial basis
///functions with Gaussian tuning. The sigma parameter specifies the standard
///deviation of this Gaussian. Select a parameter value based on the
///noise in the environment. Keep sigma high for noisy situations, and low for
///non-noisy situations.
			/// </summary>
		public double Sigma{get;set;}
		 ///<summary>
		/// </summary>

				 ///<summary>
				 /// If this parameter is true, all spatial pooler nodes in a region use the same
 ///set of coincidence patterns. Sharing of coincidence patterns among different
 ///nodes in a region reduces storage requirements and learning time when
 ///such sharing is appropriate. This is usually set to True for vision tasks.
		/// </summary>
		public bool ClonedNodes{get;set;}

			 ///<summary>
			 /// 			If this parameter is True, the node is in inference mode.
		/// </summary>
public bool IsInInferenceMode{get;set;}
	
			 ///<summary>
			 /// 	If this parameter is True, the node is in learning mode. Only one of learningMode
/// and inferenceMode can be True at a given time.
		/// </summary>
			public bool IsInLearningMode{get;set;}
			

	

			 ///<summary>
			 /// 	Number of coincidence patterns that are actually stored. The number of
/// stored coincidence patterns can be less than the specified maxCoincidenceCount
/// depending on the amount of training and parameter settings.
/// If the activeOutputCount is not equal to the maxCoincidenceCount
/// at the end of a training session, it can indicate a problem with the training,
/// for example, that maxDistance is too high or not enough data were submitted
/// to the HTM Network.
		/// </summary>
			public int CoincidenceCount{get;set;}
#endregion
		
		

	}
}
