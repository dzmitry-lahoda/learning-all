namespace State

type state =
   struct
      val t: float
      val minOverlap: float
   end

type column =
   struct

   end
   
type synapse =
   struct
      val sourceInput: float
   end