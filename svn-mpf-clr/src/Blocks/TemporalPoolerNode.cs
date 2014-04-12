/*
 * Created by SharpDevelop.
 * User: asd
 * Date: 16.09.2009
 * Time: 18:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace dnHTM
{
	/// <summary>
	/// Tempor a lPool e rNode
///The TemporalPoolerNode pooling algorithm has three main components: a grouping algorithm
///for creating groups based on temporal coherence, a capability for learning higher order
///sequences, and time-based inference (TBI).
///The temporal pooler node has two phases of operation.
///• During the learning phase, this node learns the transitions in its input space and forms temporal
///groups. The temporal groups are formed by partitioning the graph corresponding to the
///temporal transitions. Temporal grouping is done when the node is switched to inference
///mode. Depending on the configured learning complexity, higher-order temporal information
///can be stored in each temporal group.
///The default behavior of the node is to ignore higher order information and just perform temporal
///grouping based on first-order statistics.
///	• During the inference phase, the node produces an output that reflects its current degree of
///membership in each temporal group, either based on the current input or based on the sequence
///of inputs received until that point. The node can also provide predictions on its inputs
///using the predict command.
///For first-order learning the key tuning parameters are requestedGroupCount and transitionMemory.
///For higher-order learning, sequencerModelComplexity, sequencerWindowLength,
///and sequencerWindowCount are important tuning parameters.
///The following inputs, outputs and parameters determine the learning and inference processes
///in the temporal pooler nodes.
	/// </summary>
	public class TemporalPoolerHtmNode
	{
		public TemporalPoolerHtmNode()
		{
		}
			#region Properties
			
			///<summary>
			/// The input to the temporal pooler node from its children. The input typically
///comes from child spatial pooler nodes.
///	</summary>
			public Input[] BottomUpIn{get;set;}

			///<summary>
///This input can be used to reset the transition history during the learning
///mode and the time-based-inference history during the inference mode.
///Typically, this signal comes directly from the sensor.
///</summary>
	public Input[] ResetIn{get;set;}

			///<summary>
///The output of the temporal pooler node is a vector of reals. It represents the
///likelihood that the input belongs to each of the temporal groups of this
///node
///</summary>
	public double[] BottomUpOut{get;set;}

			
			///<summary>
			/// This creation-time parameter determines the maximum number of temporal
///groups that will be formed. This number must be less than or equal to the
///size of the bottomUpOut output. If set to zero, then it is set to the size of
///the bottomUpOut output.
///The actual number of temporal groups formed will normally be equal to the
///requestedGroupCount. In some cases the actual number of temporal
///groups formed can be less than the requestedGroupCount For example,
///if sequencerWindowCount is 1 and if the number of coincidence
///patterns that the Temporal pooler node is operating on is less than requestedGroupCount
///then the node can assign at most one coincidence
///per group. This can be an indicator of problems with the parameter
///settings or the training of the preceding spatial pooler nodes.
///This parameter can affect the performance of the network in many ways.
///Setting requestedGroupCount too low compared to the number of coincidence
///patterns results in over-grouping of the coincidence patterns. This
///can lead to over-generalization and reduced accuracy in classifying patterns.
///Setting requestedGroupCount to be equal to the number of coincidence
///patterns results in groups that contain one coincidence pattern
///each. That setting results in explicit memorization and poor generalization.
			/// </summary>
			public int RequestedGroupCount {get;set;}
			
			///<summary>
	/// 		Determines how far back in time the temporal pooler node looks for temporal
/// transitions. Having a high transitionMemory has the effect of
/// smoothing out the temporal transitions so that temporal jitter and repeated
/// states in the input sequence do not produce undesired behavior. It is expected
/// that nodes at the bottom of the node hierarchy need smaller values,
/// and nodes toward the top need larger values of transition memory.
			/// </summary>
			public int TransitionMemory {get;set;}

			/// 



						///<summary>
/// If this parameter is True, the temporal pooler node attempts to form temporal
/// groups that are roughly equal in size.
			/// </summary>
			public bool EqualizeGroupSize{get;set;}
			
						///<summary>
/// A string that selects the method of computing output probabilities. This parameter
/// affects only the inference behavior of the node, and has no impact
/// on what is learned.
/// • When set to maxProp, computes a more peaked score for the group
/// based on the current input only.
///• When set to sumProp, computes a smoother score for the group
///based on the current input only.
///• When set to tbi, computes a score using Time-Based Inference (TBI)
///which uses the current as well as past inputs. See Understanding Time-
///Based Inference in Advanced NuPIC Programming.
///• When set to hardcoded, the node uses pre-determined temporal
///pooling that is appropriate for vision tasks. With this “short-cut” setting it
///is not necessary to learn the temporal pooler level and the learning
///phase can proceed faster. Please see the experiments in share/vision/
///experiments for examples of using this setting.
			/// </summary>
			public string TemporalPoolerAlgorithm{get;set;}
			
			


									///<summary>	 
///The temporal pooler node looks for sequential information in multiple
///stages. The sequencerWindowCount parameter controls the number
///of stages the pooler uses for the discovery of sequential information. If this
///value is set to 1 (default), a first-order temporal model is formed. More sequential
///information is extracted from the data with increasing sequencerWindowCount.
///However, there is no straight-forward correspondence
///between the temporal order and the sequencerWindowCount.
///Use higher values of sequencerWindowCount and sequencerModelComplexity
///(see below), for applications where most of the information
///is in the temporal dimension.
			/// </summary>
			public int SequencerWindowCount{get;set;}
			
						///<summary>
///Specifies the number of iterations (training inputs) in each window that the
///temporal pooler node uses for the discovery of sequential information. The
///temporal statistics are accumulated for these many iterations for each window.
///If a very long sequence is available as the input, different segments of the
///input sequence can be used for each window. If the input data is not long
///enough to be chunked into different windows while still giving sufficient
///samples for accumulating temporal transition counts, then the same data
///can be used for the different windows. In this case, the temporal pooler
///node is making multiple passes over the same data to discover higher order
///sequential information.
			/// </summary>
			public int SequencerWindowLength {get;set;}
			
			
									///<summary>
			///Determines the temporal `order' complexity of the learned temporal groups.
///For higher values more sequential information is stored in the transition matrices
///and the nodes.
///The sequencerModelComplexity required depends on the nature of
///the application. For invariant object recognition, aggressive grouping can be
///more important compared to storing sequential information. For such cases,
///sequencerModelComplexity can be kept to zero. For some applications,
///all the information is contained in the temporal dimension. The sequencerModelComplexity
///parameter should be kept high for such
///applications.
			/// </summary>
			public int SequencerModelComplexity{get;set;}
			
						///<summary>
			/// 			
/// Indicates whether the temporal groups of a node are shared by all nodes in
/// a region. Sharing reduces storage requirements and learning time when
/// such sharing is appropriate. This is usually set to True for vision tasks.
			/// </summary>
			public bool ClonedNodes{get;set;}
			
						///<summary>
/// Determines whether the node is in inference mode or not. See learningMode
/// below.
			/// </summary>
			public  bool IsInInferenceMode{get;set;}
			
						///<summary>
///Determines whether the node is in learning mode. Only one of learningMode
///and inferenceMode can be True at a given time.
			/// </summary>
			public  bool IsInLearningMode{get;set;}
			
									///<summary>
//Array of integers that shows how often each coincidence was encountered
//during learning.
///The following parameters are available from the node after learning
/// </summary>
			public int CoincidenceVectorCounts{get;set;}
									///<summary>
///Number of groups actually generated.
///The following parameters are available from the node after learning
			/// </summary>
			public int GroupCount{get;set;}
			
						///<summary>
			
/// Set of coincidences belonging to each group, returned as a list of coincidence
/// indices.
///The following parameters are available from the node after learning
			/// </summary>
			public List<int[]> Groups{get;set;}
			
									///<summary>
///Time adjacency matrix, returned as a sparse matrix.
///The following parameters are available from the node after learning
			/// </summary>
			public double[,] TimeAdjacencyMatrix{get;set;}
			
			#endregion
			
			#region Methods
			///<summary>
			/// Predicts the likelihood of coincidences or groups for a certain number of
	///steps in the future. The command takes two arguments:
	///• 'coincidences' or 'groups' that indicates whether to return likelihoods for
	///groups or coincidences (default is 'coincidences')
	///• an integer number of steps to predict (default is 1).
	///The command returns a matrix that has as many rows as the number of
	///steps requested, and whose number of columns is either the number of coincidences
	///or the number of groups, depending on the mode.
	///This command works only in inference mode, with
	///temporalPoolerAlgorithm set to TBI.
			
			///</summary>
			public double[,] PredictCoincidences(int numberOfstepsToPredict){
				return null;
			}
			public double[,] PredictGroups (int numberOfstepsToPredict){
				return null;
			}
			#endregion
		}
	}

