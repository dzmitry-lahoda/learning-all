open Microsoft.FSharp.Quotations
open System
open Microsoft.FSharp

type Life = Alive | Dead 
type Status = Civil | Military | Demobilized
type Id = int

type Resource() = 
  member val life = Life.Alive with get, set
  member val status = Status.Civil with get, set
  
type Role = abstract member  id :  Id
type Doctor = interface Role with member x.id = 0
type Pasportist = interface Role with member x.id = 1
type Commissar = interface Role with member x.id = 2

type user(id:Id) = 
  member val roles : Id = 1

type transition<'a> = {input : Option<'a>; output : Option<'a>}
let indiffirent<'a> : transition<'a> = { input = None; output = None}

// type operation =  should pure marker
  
type domain_operation =
  abstract member life  : transition<Life>
  abstract member status  : transition<Status>

type recruit =
  interface domain_operation with
    member x.life : transition<Life> = <@ fun from -> Some(Life.Alive) @> 
    member x.status : transition<Life> = <@ fun from -> Some(Status.Military) @> 
   