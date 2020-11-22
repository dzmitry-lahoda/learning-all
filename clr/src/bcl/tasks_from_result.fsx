


open System
open System.Threading
open System.Threading.Tasks

let completion1 = new TaskCompletionSource<Object>()
completion1.SetResult(null);
try 
  completion1.SetResult(new Object());
with
 | :? InvalidOperationException as ex -> printfn "Cannot result object with previous null"

let completion2 = new TaskCompletionSource<Object>()

completion2.SetResult(new Object());
completion2.SetResult(new Object());
try 
  completion2.SetResult(new Object());
with
 | :? InvalidOperationException as ex -> printfn "Cannot result object with previous set object"