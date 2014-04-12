open System

let randomVector(minmax:list<list<float>>) =
    let random = new Random()
    let generator(i) = minmax.[i].[0] + ((minmax.[i].[1] - minmax.[i].[0]))*random.NextDouble()
    List.init (minmax.Length) generator

let initializeWeights(problemSize) =
    let minmax = List.init (problemSize+1) (fun i -> [-1.0;1.0])
    let weights = randomVector(minmax)
    weights

let transfer(activation) = 
    match activation>=0.0 with
        | true-> 1.0
        | false-> 0.0

let activate(weights:list<float>,vector:list<float>)=
    let mutable summ = weights.[weights.Length-1] * 1.0
    for i = 0 to vector.Length-1 do 
        summ <- summ + vector.[i] * weights.[i] 
    summ

let getOutput(weights:list<float>,vector:list<float>)=
    let activation = activate(weights,vector)
    transfer(activation)

let updateWeights(numInputs,weights,input:list<float>,outExp,outAct,learningRate) =
    let updatedWeights = Array.ofList weights
    for i=0 to input.Length-1 do
        updatedWeights.[i] <- updatedWeights.[i] + abs(outExp - outAct) * input.[i] * learningRate
    updatedWeights.[numInputs] <-updatedWeights.[numInputs]+ (outExp - outAct) *  learningRate * 1.0
    List.ofArray updatedWeights

let train(weights:list<float>,domain:list<list<float>>,num_inputs,iteration,learningRate) =
    let mutable trainedWeights = weights
    for epoch=1 to iteration do
        let mutable error = 0.0
        for pattern in domain do
            let input = List.init(num_inputs) (fun i-> pattern.[i])
            let output = getOutput(trainedWeights, input)            
            let expected = pattern.[pattern.Length-1]
            error <- error + abs(output - expected)
            trainedWeights <- updateWeights(num_inputs,trainedWeights,input,expected,output,learningRate)
        printfn "> epoch=%d, error=%f'" epoch error
    trainedWeights

let test(weights:list<float>,domain:list<list<float>>,num_inputs) =
    let mutable correct = 0
    for i=0 to domain.Length-1 do
        let pattern = domain.[i]
        let input = List.init(num_inputs) (fun i-> pattern.[i])
        let output = getOutput(weights, input)
        if (round(output) - domain.[i].[2] <= 0.01) then
            correct <- correct + 1
    printfn "Finished test with a score of %d/%d" correct domain.Length           

let execute(domain,numInputs,iterations,learningRate) = 
    let weights = initializeWeights(numInputs)
    let trainedWeights = train (weights,domain,numInputs,iterations,learningRate)
    test (trainedWeights,domain,numInputs)

let main = 
    let orProblem = [
              [0.0;0.0; 0.0];
              [0.0;1.0; 1.0];
              [1.0;0.0; 1.0];
              [1.0;1.0; 1.0];
             ]
    let inputs = 2
    let iterations = 20
    let learningRate = 0.1
    execute(orProblem,inputs,iterations,learningRate)

main