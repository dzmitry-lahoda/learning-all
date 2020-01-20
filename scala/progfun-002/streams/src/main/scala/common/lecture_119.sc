package common

object lecture_119 {
 abstract class IntSet {
   def incl(x:Int) : IntSet
   def contains(x:Int):Boolean
 }
 

 
 0 #:: Stream(1,2,3)                              //> res0: scala.collection.immutable.Stream[Int] = Stream(0, ?)
}