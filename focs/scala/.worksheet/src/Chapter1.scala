


object Chapter1{;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(55); 
          
         println("Help");$skip(32); val res$0 = 
     SlowOneBitAdder.sum(1,1,1);System.out.println("""res0: BitOut = """ + $show(res$0));$skip(32); val res$1 = 
     SlowOneBitAdder.sum(0,0,0);System.out.println("""res1: BitOut = """ + $show(res$1))}
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
