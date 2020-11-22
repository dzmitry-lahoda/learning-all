open System


let a = 1.0m

let examples = 
    [
        0;
        1;
        123;
        123456789;
    ]

let format f = 
  printfn ""
  printfn "%s" f
  for e in examples do
    Console.WriteLine(e.ToString(f))

format "X"


format "000000"

let x = 123
x.ToString()