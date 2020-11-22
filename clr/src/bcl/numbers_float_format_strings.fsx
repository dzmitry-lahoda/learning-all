open System


// format decimal values with lenght less or equals to X

let a = 1.0m

let examples = 
    [
0.0;
0.000000001;
0.00000001;
0.00001;
0.1;
0.9999999;
1.0;
1.1111;
1.1111111111;
1111.1;
1111.1111;
11111.111111;
9999999.0;
1111111.0;
111111111.11;
1111111111.0;
111111111111111111.0;
11111111111111111111111111.0;
    ]

let format f = 
  printfn ""
  printfn "%s" f
  for e in examples do
    Console.WriteLine(e.ToString(f))

format "#,##0,,,.#########0"

format "F5"

format "0.##########"
