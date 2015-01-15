// Learn more about F# at http://fsharp.net

namespace Module1

open MathNet.Numerics.Algorithms.LinearAlgebra

open MathNet.Numerics.LinearAlgebra.Double
open MathNet.Numerics.LinearAlgebra.Double.Factorization
open MathNet.Numerics.Distributions

open MathNet.Numerics.Statistics
    
type NaiveBayes2() =
    
    let mutable NEAREST = 5

    member this.Fit (data:Matrix,lables:array<int>) =
        
        ()

    member this.Predict (data:Matrix) =
        
        ([| 1; 2; 3 |])
