package week4

abstract class MyBoolean {
  def ifThenElse[T] (t: => T, e: => T):T
  def && (x: => MyBoolean): MyBoolean = ifThenElse(x,MyFalse)
  def || (x: => MyBoolean): MyBoolean = ifThenElse(x,MyTrue)
  def unary_! : MyBoolean              = ifThenElse(MyFalse,MyTrue)
  def ==  (x: => MyBoolean): MyBoolean = ifThenElse(x,!x)
  def !=  (x: => MyBoolean): MyBoolean = ifThenElse(!x,x)
  def <  (x: => MyBoolean): MyBoolean = ifThenElse(MyFalse,x)
}