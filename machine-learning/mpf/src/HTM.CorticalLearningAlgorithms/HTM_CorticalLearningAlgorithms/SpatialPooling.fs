
module SpatialPooling
open System
open System.Collections.Generic
open State
open SpatialPoolingSupportingRoutines

let overlap (state:state) (columns:List<column>) =
    for c in columns do
        let mutable overlapC = overlap(c)
        overlapC <- 0.0
        for s in connectedSynapses(c) do
            let inputVal = SpatialPoolingSupportingRoutines.input(state.t)
            overlapC <- overlapC + inputVal s.sourceInput
            if overlapC < (state.minOverlap)
            then overlapC <- 0.0
            else overlapC <- overlapC * boost(c)

