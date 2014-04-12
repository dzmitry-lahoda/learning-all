// Learn more about F# at http://fsharp.net

namespace Module1

open MathNet.Numerics.LinearAlgebra.Double
 

type Neuron(weigth:float, treshhold:float)=
    new() = Neuron (1.5,2.5) 
    //let mutable weigth = 1.5
    //let mutable treshhold = 2.5
    member n.Weight = weigth
    member n.Treshold = treshhold

    member n.Output input =
        let result = weigth * input 
        result

type ANDNetwork() =
    let l1n1 = new Neuron(1.0,0.0)
    let l1n2 = new Neuron(1.0,0.0)
    let l2n1 = new Neuron(1.0,1.5)

    member t.Answer (A,B) =
        let o1 = l1n1.Output(A)
        let o2 = l1n2.Output(B)
        let mutable a = l2n1.Output(o1+o2)
        if a< l2n1.Treshold then
            a <- 0.0
        (a)

module BiPolarUtil =
    let bipF (x:float) = 2.0*x - 1.0

    let Bipolar2double (vector:Vector) = 
       let arr =  vector.ToArray();
       let maaap = Array.map bipF arr
       let res = new DenseVector(maaap)
       (res)

    let TrainingFunction (rate:float) (input) (output) = 
        rate*input*output

type HopfiledNetwork(size:int) =
    let mutable weightMatrix:Matrix = new DenseMatrix(size) :> Matrix

    member this.LayerMatrix = weightMatrix
    member this.Size = weightMatrix.RowCount

    member this.Present(pattern:Vector) =
       let output:Vector = new DenseVector(pattern.Count) :> Vector
       let bipolar = BiPolarUtil.Bipolar2double(pattern)
       let inputMatrix = bipolar

       for col=0 to pattern.Count-1 do
           let mutable columnMatrix = this.LayerMatrix.Column(col)
           let dotProduct = inputMatrix.DotProduct(columnMatrix)
           if dotProduct > 0.0 then
               output.Item(col) <- 1.0
           else
               output.Item(col) <- 0.0
       (output)

    member this.Train(pattern:Vector) =
        let bipolarPattern = (BiPolarUtil.Bipolar2double pattern).ToColumnMatrix()
        let bipolarTransposedPattern = bipolarPattern.Transpose()
        let weigthAddition = bipolarPattern.Multiply(bipolarTransposedPattern)
        let diagonal =  Array.init (weigthAddition.ColumnCount) (fun x-> 0.0)
        weigthAddition.SetDiagonal(diagonal)
        let newWeightMatrix = weightMatrix.Add(weigthAddition).ToArray();
        weightMatrix <- new DenseMatrix(newWeightMatrix) 
        ()


type ErrorCalculation() =
    let mutable globalError:float = 0.0
    let mutable setSize:int = 0
    
    member this.CalculateRMS = 
        let err = System.Math.Sqrt(globalError / float setSize)
        err
    member this.Reset = 
        globalError <- 0.0
        setSize <- 0
        
    member this.UpdateError (actual:Vector) (ideal:Vector) = 
        for i = 0 to actual.Count-1 do
            let delta = ideal.[i] - actual.[i]
            globalError <- globalError + delta * delta
            setSize <- setSize + ideal.Count

type FeedforwardLayer(size:int)=
    let mutable weightMatrix:Matrix = new DenseMatrix(size) :> Matrix
    member this.LayerMatrix = weightMatrix

type FeedforwardNetwork()=
    let layers:System.Collections.Generic.List<FeedforwardLayer> = new System.Collections.Generic.List<FeedforwardLayer>()

    member this.Layers =
        layers

    member this.AddLayer(layer:FeedforwardLayer) =     
        layers.Add(layer)
    
    member this.Reset() =   
        (ignore)

type DeltaRuleTrain(nn:FeedforwardNetwork,input:float,idealOutput:float,rate:float)  = 
    let train (rate:float,input:float,error:float) = 
        2.0* rate*input*error
    let mutable error:float = 0.0
    member this.Error = error        

    member this.Iteration() =
        for i=0 to nn.Layers.Count-1 do
            let lm = nn.Layers.[i].LayerMatrix;
            let result = lm.[0,0] * input
            printf "result %f" result
            error <- idealOutput - result

            let delta = train(rate,lm.[0,0],error)
            printf "delta %f" delta
            lm.[0,0] <- lm.[0,0] + delta
    
        


           
