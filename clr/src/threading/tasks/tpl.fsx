open System
open System.Threading.Tasks
open System.Threading

let cancelationToken = new CancellationTokenSource()



do cancelationToken.Cancel()
do printfn "%b" (cancelationToken.IsCancellationRequested)
do cancelationToken.Cancel()


let p = new System.Progress<int>() :> IProgress<int>
p.Report(100);