package week4

import List._

object week4 {
  val x:List[String] = Nil                        //> x  : week4.List[String] = week4.Nil$@7ab32609

  MyFalse ifThenElse(true,false)                  //> res0: Boolean = false
  MyTrue ifThenElse(true,false)                   //> res1: Boolean = true
  Zero.successor.successor                        //> res2: week4.MyNatural = week4.Succ@178bf083
  println("Welcome to the Scala worksheet")       //> Welcome to the Scala worksheet
  def l1 = 3 ::: Nil                              //> l1: => week4.Cons[Int]
  def l2 = !!(5,l1)                               //> l2: => week4.Cons[Int]
  List(1,2)                                       //> res3: week4.List[Int] = week4.Cons@592e4ff9
  l2.index(0)                                     //> res4: Int = 5
  l2.index(1)                                     //> res5: Int = 3
  //l2.indexul(2)
}