package week4

import List._




object MyFalse extends MyBoolean{;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(112); 
   def ifThenElse[T] (t: => T, e: => T):T = t;System.out.println("""ifThenElse: [T](t: => T, e: => T)T""")}
}
object MyTrue  extends MyBoolean{
   def ifThenElse[T] (t: => T, e: => T):T = t
}
abstract class MyBoolean{
  def ifThenElse[T] (t: => T, e: => T):T
  def && (x: => MyBoolean): MyBoolean = ifThenElse(x,MyFalse)
  def || (x: => MyBoolean): MyBoolean = ifThenElse(x,MyTrue)
  def unary_! : MyBoolean              = ifThenElse(MyFalse,MyTrue)
  def ==  (x: => MyBoolean): MyBoolean = ifThenElse(x,!x)
  def !=  (x: => MyBoolean): MyBoolean = ifThenElse(!x,x)
  def <  (x: => MyBoolean): MyBoolean = ifThenElse(MyFalse,x)
}


object week4 {
  MyFalse.ifThenElse(true,false)
  MyTrue.ifThenElse(true,false)
  
  println("Welcome to the Scala worksheet")
  def l1 = !!(3)
  def l2 = !!(5,l1)
  List(1,2)
  l2.index(0)
  l2.index(1)
  //l2.index(2)
}
