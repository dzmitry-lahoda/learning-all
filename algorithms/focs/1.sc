


object Chapter1{
          
         println("Help")                          //> Help
     SlowOneBitAdder.sum(1,1,1)                   //> res0: BitOut = BitOut(1,1)
     SlowOneBitAdder.sum(0,0,0)                   //> res1: BitOut = BitOut(0,0)
}

class RippleCarryAdder(size: Int){
 
}

case class BitInput(x:Int, y:Int, carryInBit:Int)
  
 
case class BitOut(carryOutBit:Int, sumBit:Int)

object SlowOneBitAdder{
  def sum(x:Int, y:Int, carryInBit:Int ): BitOut = sum(new BitInput(x,y,carryInBit))
  
  def sum(inp: BitInput) = inp match {
    case BitInput(0,0,0) =>  BitOut(0,0)
    case BitInput(0,0,1) =>  BitOut(0,1)
    case BitInput(0,1,0) =>  BitOut(0,1)
    case BitInput(0,1,1) =>  BitOut(1,0)
    case BitInput(1,0,0) =>  BitOut(0,1)
    case BitInput(1,0,1) =>  BitOut(1,0)
    case BitInput(1,1,0) =>  BitOut(1,0)
    case BitInput(1,1,1) =>  BitOut(1,1)
  }

}