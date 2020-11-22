#light


namespace bcl_tests
open System  

module string_tests = 

 let equality = 
  let a1 =   [|'T';'e'|]
  let s1 = String(a1)
  let s2 = String([|'T';'e'|]) 
  let o1 =  s1 :> Object
  let o2 = s2 :> Object
 
  printfn "%b" (s1 = s2) //true 
  printfn "%b" (String.Equals(s1,s2)) // true

  printfn "%b" (o1 = o2) //true 
  printfn "%b" (Object.Equals(o1,o2)) // true

 let length =

   let string255 = new string(Array.ofSeq (seq { for x in 0..255 do yield (x % 10).ToString().[0] }))

   printfn "%s" string255 

   printfn "Hello world"

 


