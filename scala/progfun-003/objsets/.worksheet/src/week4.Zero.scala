package week4

import List._


object Zero extends MyNatural{;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(89); 
  def isZero:Boolean = true;System.out.println("""isZero: => Boolean""");$skip(52); 
  def predecessor:MyNatural = throw new Exception();System.out.println("""predecessor: => week4.MyNatural""");$skip(43); 
  def successor:MyNatural = new Succ(Zero);System.out.println("""successor: => week4.MyNatural""");$skip(43); 
  def + (that: MyNatural):MyNatural = that;System.out.println("""+ : (that: week4.MyNatural)week4.MyNatural""");$skip(60); 
  def - (that: MyNatural):MyNatural = throw new Exception();System.out.println("""- : (that: week4.MyNatural)week4.MyNatural""")}
}
class Succ(n: MyNatural) extends MyNatural{
  def isZero:Boolean = false
  def predecessor:MyNatural = n
  def successor: MyNatural = new Succ(this)
  def + (that: MyNatural): MyNatural ={
    def iter(acc:MyNatural,dec:MyNatural) =
      acc
    iter(this,that)
  }
  def - (that: MyNatural): MyNatural = Zero
}




object week4 {
  MyFalse ifThenElse(true,false)
  MyTrue ifThenElse(true,false)
  
  println("Welcome to the Scala worksheet")
  def l1 = !!(3)
  def l2 = !!(5,l1)
  List(1,2)
  l2.index(0)
  l2.index(1)
  //l2.index(2)
}
