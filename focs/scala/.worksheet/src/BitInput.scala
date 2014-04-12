
case class BitInput(x:Int, y:Int, carryInBit:Int)
case class BitOut(carryOutBit:Int, sumBit:Int)

class SlowOneBitAdder{
  def sum(inp: BitInput) = inp match {
    case BitInput(1,1,1) =>  BitOut(1,1)
  }

}

object Chapter1{
          
          System.out.print("Help")

}
