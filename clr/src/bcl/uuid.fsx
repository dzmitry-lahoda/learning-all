
open System

let guid = Guid.NewGuid()
let nodash = guid.ToString("N")

assert (nodash.Contains("-") = true)

let parse = Guid.Parse(nodash)
let guidref =   ref Guid.Empty
let tryParseNull = Guid.TryParse(null, guidref)
assert (tryParseNull = false)
assert (guidref.Value = Guid.Empty)

try
  let parsedFromNull = Guid.Parse(null)
  printfn ""
with
    | :? System.ArgumentNullException as ex -> 
          printfn "%s" ex.Message    
          
// size of payload of collection of guids
let count = 10*1000*1000
let megabyte = 1024*1024
let sizeOfGuid = sizeof<System.Guid>
let sizeOfGuidString = System.Guid.Empty.ToString("N").Length * sizeof<System.Char>
let sizeOfnumber = (count * sizeOfGuid)/megabyte
let sizeOfStrings = (count * sizeOfGuidString)/megabyte                
 