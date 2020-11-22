// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System
open System.Threading
open System.Threading.Tasks

type Foo() =
  let mutable answer : int = 0
  let mutable complete = false  
  member public this.a() = 
    answer <- 42
    complete <- true
    ()

   member public this.b() = 
     if (complete) then 
       Console.WriteLine(answer)

let r = Random()

let test() =
  
  let f = Foo()
  let calls = [f.a; f.b] |> Seq.sortBy (fun x -> r.Next())
  for c in calls do
    Task.Factory.StartNew(c) |> ignore
  ()

[<EntryPoint>]
let main argv = 
    for i = 1 to 100 do
      Task.Factory.StartNew(test) |> ignore
    
    Thread.Sleep(10000)
    printfn "%A" argv
    0 // return an integer exit code
