package common

object week4_1 {
  println("Welcome to the Scala worksheet")       //> Welcome to the Scala worksheet
  def calc = Prod(Number(2),Sum(Var("one"), Sum ( Number(3),Number(4))))
                                                  //> calc: => common.Prod
  //def calc = Arith(Arith (1), Arith ( Arith(3),Arith(4)))
  calc.eval(Map(("one",1)))                       //> res0: Int = 16
  calc.show                                       //> res1: String = (2*(one+(3+4)))
  Sum(Prod(Number(1),Number(2)),Number(1)).show   //> res2: String = (1*2+1)
  def l1 = 1 :: (2 :: Nil)                        //> l1: => List[Int]
  def l2 = 1 :: 2 :: Nil                          //> l2: => List[Int]
  def l3 = Nil.::(4)                              //> l3: => List[Int]
  
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