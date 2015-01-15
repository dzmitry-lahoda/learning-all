// Learn more about F# at http://fsharp.net

namespace PCA

open MathNet.Numerics.Algorithms.LinearAlgebra

open MathNet.Numerics.LinearAlgebra.Double
open MathNet.Numerics.LinearAlgebra.Double.Factorization
open MathNet.Numerics.Distributions

open MathNet.Numerics.Statistics

type PCA =
    
    static member linearAlgebraProvider with get() = new ManagedLinearAlgebraProvider(); 

    static member mean (data:seq<float>) = 
        let m = new MathNet.Numerics.LinearAlgebra.Double.DenseMatrix(0)
        
        Statistics.Mean(data)

    static member std (data:seq<float>) = 
       
        Statistics.StandardDeviation(data)

    
    static member adjustByMean (data:seq<float>) =
        let mean = PCA.mean(data)
        let ajustData = Seq.map (fun x -> x - mean) data
        (ajustData,mean)

    static member squared(data1:seq<float>) (data2:seq<float>) = 
        Seq.map2 (fun x y-> x*y) data1 data2
            
    static member cov (data1:seq<float>) (data2:seq<float>) = 
        let ajustData1,mean1 = PCA.adjustByMean data1 
        let ajustData2,mean2 = PCA.adjustByMean data2 
        let s = List.ofSeq ( PCA.squared ajustData1 ajustData2)
        let n = s.Length
        let ret = (List.sum s)/(float n - float 1)
        (ret)

    static member covMatrix (data:DenseMatrix)= 
        let nV = data.ColumnCount
        let nP = data.RowCount
        let info,lm,ar = alglib.lrbuildz(data.ToArray(),nP,nV)
        let covMatr =ar.c
        (covMatr)    

