package greeter



object TestRepl {

  println("Welcome to the Scala worksheet")       //> Welcome to the Scala worksheet
  val i = 1                                       //> i  : Int = 1
  def increase(i:Int) = i + 1                     //> increase: (i: Int)Int
  increase(i)                                     //> res0: Int = 2
}