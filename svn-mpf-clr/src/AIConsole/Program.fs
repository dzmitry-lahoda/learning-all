

module Run

open System.Collections.Generic
open System.Threading
open System 
open Model

let environment =  new Floor()
//let agent = new VacuumCleaner(environment)

//Async.Parallel [ for i in 1 .. Environment.ProcessorCount -> async { do loop()} ] 
//      |> Async.Ignore  
//      |> Async.RunSynchronously 
