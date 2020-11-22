
namespace bcl_tests
open System  
open System.Threading.Tasks
open System.Threading
open System.IO

module random_tests = 

   
  let random_file_name  = 
    let size = 2000000
    let repeats = 5
    let random_file_name_sub (pathGenerator: unit  -> string) = 
     async {
      let randoms = seq { for i in 1..size do yield pathGenerator()}    
      let unique = randoms |> Seq.distinct |> Array.ofSeq
      let diff = size - unique.Length
      return (diff,size)
     }

     
    let random() = Path.GetRandomFileName()
    let shortened() =  Path.GetFileNameWithoutExtension(random()) // = Path.GetFileName((new System.CodeDom.Compiler.TempFileCollection()).BasePath) as of .NET 4.0
    let diffsFull = [for i in 1..repeats -> random_file_name_sub(random) ] |> Async.Parallel  |> Async.RunSynchronously
    printfn "Finished generation of full names like: oi0uz5if.1fn"
    printfn "Non unique / sequence length"
    for diff in diffsFull do
           printfn "%d / %d" (fst(diff)) (snd(diff))

    let diffShort = [for i in 1..repeats -> random_file_name_sub(shortened) ] |> Async.Parallel  |> Async.RunSynchronously
    printfn "Finished generation of no extension names like: oi0uz5if"
    printfn "Non unique / sequence length"
    for diff in diffShort do
           printfn "%d / %d" (fst(diff)) (snd(diff))

