package week4

import List._

object week4 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(71); 
  val x:List[String] = Nil;System.out.println("""x  : week4.List[String] = """ + $show(x ));$skip(34); val res$0 = 

  MyFalse ifThenElse(true,false);System.out.println("""res0: Boolean = """ + $show(res$0));$skip(32); val res$1 = 
  MyTrue ifThenElse(true,false);System.out.println("""res1: Boolean = """ + $show(res$1));$skip(27); val res$2 = 
  Zero.successor.successor;System.out.println("""res2: week4.MyNatural = """ + $show(res$2));$skip(44); 
  println("Welcome to the Scala worksheet");$skip(21); 
  def l1 = 3 ::: Nil;System.out.println("""l1: => week4.Cons[Int]""");$skip(20); 
  def l2 = !!(5,l1);System.out.println("""l2: => week4.Cons[Int]""");$skip(12); val res$3 = 
  List(1,2);System.out.println("""res3: week4.List[Int] = """ + $show(res$3));$skip(14); val res$4 = 
  l2.index(0);System.out.println("""res4: Int = """ + $show(res$4));$skip(14); val res$5 = 
  l2.index(1);System.out.println("""res5: Int = """ + $show(res$5))}
  //l2.indexul(2)
}
