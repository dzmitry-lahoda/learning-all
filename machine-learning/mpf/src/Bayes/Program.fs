// Learn more about F# at http://fsharp.net
#nowarn "62"

open System

//Unovservable
type Attitude = 
    | Happy
    | Unhappy
    | Quiet

//Ovservable
type Action =
    | Smile
    | Cry
    | LookSilly

let happyActions = [ Smile, 0.6; Cry,0.2; LookSilly, 0.2];
let unHappyActions = [Smile, 0.2; Cry, 0.6; LookSilly, 0.2]
let quietActions = [Smile, 0.4; Cry, 0.3; LookSilly, 0.3]

let random =
    let rnd = new System.Random()
    rnd.NextDouble



/// Find the first value more or equal to a key in a seq<'a * 'b>.
/// The seq is assumed to be sorted
let findByKey key aSeq =
    aSeq |> Seq.find (fun (k,_) -> k >= key) |> snd

/// Simulate an inverse CDF given values and probabilities
let buildInvCdf valueProbs =
    let cdfValues =
        valueProbs
        |> Seq.scan (fun cd (_, p) -> cd + p) 0. 
        |> Seq.skip 1 
    let cdf =
        valueProbs
        |> Seq.map fst 
        |> Seq.zip cdfValues 
        |> Seq.cache     
    fun x -> cdf |> findByKey x

/// Picks an 'a in a seq<'a * float> using float as the probability to pick a particular 'a
let pickOne probs =
    let rnd = random ()
    let picker = buildInvCdf probs
    picker rnd


let MaiaSampleDistribution attitude =
    match attitude with
    | Happy     -> pickOne happyActions
    | UnHappy   -> pickOne unHappyActions
    | Quiet     -> pickOne quietActions


/// Another, mathematically more convenient, way to model the process (baby)
/// if action then attitude
let MaiaJointProb attitude action =
    match attitude with
    | Happy     -> List.find (fun (actionElem,valueElem)-> actionElem = action ) happyActions |> snd
    | UnHappy   -> unHappyActions |> List.assoc action
    | Quiet     -> quietActions |> List.assoc action

/// Conditional probability of a mental state, given a particular observed action
let MaiaLikelihood action = fun attitude -> MaiaJointProb attitude action

/// Multiple applications of the previous conditional probabilities for a series of actions (multiplied)
let MaiaLikelihoods actions =
    // terribly functional and recursively
    let composeLikelihoods previousLikelihood action  = 
        fun attitude -> previousLikelihood attitude * MaiaLikelihood action attitude     
        //Seq.fold (fun acc x-> acc * x) 1.0 [1.2;2.2;3.3];;
        //val it : float = 8.712
    Seq.fold composeLikelihoods (fun attitude -> 1.) actions 

let MaiaUniformPrior attitude = 1. / 3.

/// Calculates the unNormalized posterior given prior and likelihood
let unNormalizedPosterior (prior:'a -> float) likelihood =
    fun theta -> prior theta * likelihood theta

/// All possible values for the unobservable parameter (mental state)
let support = [Happy; Unhappy; Quiet]

/// Normalize the posterior (it integrates to 1.)
let posterior prior likelihood =
    let post = unNormalizedPosterior prior likelihood
    let sum = support |> List.sumBy (fun attitude -> post attitude)
    fun attitude -> post attitude / sum

let maiaIsANormalBaby = posterior MaiaUniformPrior (MaiaLikelihoods [Smile;Smile;Cry;Smile;LookSilly])

/// Extreme cases
let maiaIsLikelyHappyDist = posterior MaiaUniformPrior (MaiaLikelihoods [Smile;Smile;Smile;Smile;Smile;Smile;Smile])

let posteriorPredictive jointProb posterior =
    let composeProbs previousProbs attitude = 
        fun action -> previousProbs action + jointProb attitude action * posterior attitude  
    support |> Seq.fold composeProbs (fun action -> 0.)
    
let nextLikelyUnknownActionDist = posteriorPredictive MaiaJointProb maiaIsLikelyHappyDist

let likeH = MaiaLikelihoods [Smile;Smile;Cry;Smile;LookSilly]


printfn "%f" (maiaIsLikelyHappyDist Happy)
printfn "%f" (maiaIsLikelyHappyDist Quiet)
printfn "%f" (maiaIsLikelyHappyDist Unhappy)

let z =
    MaiaSampleDistribution Attitude.Happy
Console.WriteLine (z.ToString())

