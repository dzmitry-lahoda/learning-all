#load "file_tests.fsx"


open bcl_tests

open System

module program =


#if COMPILED
  [<EntryPoint>]
#endif
  let main args =
    printfn "Start"
    //string_tests.equality
    //random_tests.random_file_name
    //file_tests.path_combine
    //xml.long_names  
    file_tests.get_path_root_null()
        
    printfn "End"
    Console.Read() |> ignore
    0
 

program.main(null) |> ignore
