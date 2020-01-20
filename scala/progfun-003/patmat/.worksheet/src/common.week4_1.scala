package common

object week4_1 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(76); 
  println("Welcome to the Scala worksheet");$skip(73); 
  def calc = Prod(Number(2),Sum(Var("one"), Sum ( Number(3),Number(4))));System.out.println("""calc: => common.Prod""");$skip(88); val res$0 = 
  //def calc = Arith(Arith (1), Arith ( Arith(3),Arith(4)))
  calc.eval(Map(("one",1)));System.out.println("""res0: Int = """ + $show(res$0));$skip(12); val res$1 = 
  calc.show;System.out.println("""res1: String = """ + $show(res$1));$skip(48); val res$2 = 
  Sum(Prod(Number(1),Number(2)),Number(1)).show;System.out.println("""res2: String = """ + $show(res$2));$skip(27); 
  def l1 = 1 :: (2 :: Nil);System.out.println("""l1: => List[Int]""");$skip(25); 
  def l2 = 1 :: 2 :: Nil;System.out.println("""l2: => List[Int]""");$skip(21); 
  def l3 = Nil.::(4);System.out.println("""l3: => List[Int]""")}
  
}


trait Expr{
  def show:String = this match {
    case Number(0) => "0"
    case Number(n) => n.toString
    case Var(name) => name
    case Sum(e1,e2) => "(" + e1.show + "+" + e2.show + ")"
    case Prod(e1:Sum,e2) => "(" + e1.show + "*" + e2.show + ")"
    case Prod(e1,e2:Sum) => "(" + e1.show + "*" + e2.show + ")"
    case Prod(e1,e2) =>   e1.show + "*" + e2.show
  }

  def eval(vars: Map[String,Int] = null):Int = this match {
    case Number(0) => 123
    case Number(n) => n
    case Sum(e1,e2) => e1.eval(vars) + e2.eval(vars)
    case Prod(e1,e2) => e1.eval(vars) * e2.eval(vars)
    case Var(name) => vars(name)
  }
}
case class Number(n: Int) extends Expr
case class Sum(e1: Expr,e2: Expr) extends Expr
case class Prod(e1: Expr,e2: Expr) extends Expr
case class Var(name: String) extends Expr
/*
NOTE: quadratic explosion of methods
trait Expr{
  def isNumber: Boolean
  def isSum:Boolean
  def numVal:Int
  def leftOp : Expr
  def rightOp: Expr
}

object Arith{
   def apply(numVal:Int) = new Number(numVal)
   def apply(leftOp:Expr,rightOp:Expr) = new Sum(leftOp,rightOp)
   def eval(e:Expr):Int = {
     if (e.isNumber) e.numVal
     else if (e.isSum) eval(e.leftOp) + eval(e.rightOp)
     else throw new Error()
   }
   def eval(e:Number):Int = {
    e.numVal
   }
   def eval(e: Sum):Int = {
     eval(e.leftOp) + eval(e.rightOp)
   }
}

class Number(val numVal:Int) extends Expr{

   def isNumber = true
   def isSum = false
   def leftOp = null
   def rightOp = null
}

class Sum(val leftOp:Expr,val rightOp:Expr) extends Expr{

   def numVal = leftOp.numVal + rightOp.numVal
   def isNumber = false
   def isSum = true
}
*/
