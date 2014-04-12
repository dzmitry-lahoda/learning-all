module Model

open System.Collections.Generic
open System.Threading
open System 


type State = 
    | Clean
    | Dirty

type Position = 
    | Left 
    | Right 
    
type public Floor() =    
    member public f.Positions =  
        let positions = new Dictionary<Position, State>()
        positions.Add(Position.Left,State.Clean)
        positions.Add(Position.Right,State.Clean);
        positions
    

type public VacuumCleaner =
    { f : Floor; }

    member vc.Position =  
        Position.Left 

    member vc.Work = 
        if vc.f.Positions.[vc.Position] = State.Dirty then
            vc.Suck()            
        Thread.Sleep(1000)

    member vc.Suck() = 
        vc.f.Positions.[vc.Position] <- State.Clean

    member vc.Left() =
        //vc.Position <-  Position.Left
        vc.Work

    /// Move right
    member vc.Right() =
        //vc.Position <-  Position.Right
        vc.Work