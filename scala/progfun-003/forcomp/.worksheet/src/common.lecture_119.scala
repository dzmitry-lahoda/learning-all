package common

object lecture_119 {
 abstract class IntSet {
   def incl(x:Int) : IntSet
   def contains(x:Int):Boolean
 }
 
 class NonEmpty extends IntSet{
   
 }
  
 object Empty extends IntSet{
   def contains(x:Int):Boolean = false
   def incl(x:Int): IntSet = NonEmpty(x,Empty,Empty)
 };import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(314); val res$0 = 

 0 #:: Stream(1,2,3);System.out.println("""res0: scala.collection.immutable.Stream[Int] = """ + $show(res$0))}
}
