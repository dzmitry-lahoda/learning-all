package common

object lecture_103 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(70); val res$0 = 
  Vector(1,2,3);System.out.println("""res0: scala.collection.immutable.Vector[Int] = """ + $show(res$0));$skip(84); val res$1 = // is shallow tree
  // 32 is large - 32 of 32 = 20^10, etc
  
  // log_32 N access
  Vector(1,2,3)(1);System.out.println("""res1: Int = """ + $show(res$1));$skip(102); val res$2 = 
  
  // maps 32 to cache of processor and done as batch, good localyty
  Vector(1,2,3) map (x=> x* x);System.out.println("""res2: scala.collection.immutable.Vector[Int] = """ + $show(res$2));$skip(47); 
  
  
  //log_32 N
  val v1 = Vector(1,2) :+ 3;System.out.println("""v1  : scala.collection.immutable.Vector[Int] = """ + $show(v1 ));$skip(22); val res$3 = 
  
  1 +: Vector(2,3);System.out.println("""res3: scala.collection.immutable.Vector[Int] = """ + $show(res$3));$skip(50); val res$4 = 
  
  (1 +: Vector(2,3)  ) == (1 +: Vector(2,3)  );System.out.println("""res4: Boolean = """ + $show(res$4));$skip(31); 
  
  val i2:Iterable[Int] = v1;System.out.println("""i2  : Iterable[Int] = """ + $show(i2 ));$skip(19); val res$5 = 
  i2.iterator.next;System.out.println("""res5: Int = """ + $show(res$5));$skip(23); 
  val i3 = i2.iterator;System.out.println("""i3  : Iterator[Int] = """ + $show(i3 ));$skip(10); val res$6 = 
  i3.next;System.out.println("""res6: Int = """ + $show(res$6));$skip(10); val res$7 = 
  i3.next;System.out.println("""res7: Int = """ + $show(res$7));$skip(24); 
  val ar = Array(1,2,3);System.out.println("""ar  : Array[Int] = """ + $show(ar ));$skip(116); 
 
  // iter
  //seq          set     map
  // vector list
  //
  // array string
  
  val r:Range = 1 until 11 by 3;System.out.println("""r  : Range = """ + $show(r ));$skip(20); 
  
  val s= "Hello";System.out.println("""s  : String = """ + $show(s ));$skip(31); val res$8 = 
  s flatMap (c => List('.',c));System.out.println("""res8: String = """ + $show(res$8));$skip(8); val res$9 = 
  2 % 3;System.out.println("""res9: Int(2) = """ + $show(res$9));$skip(46); val res$10 = 
  1 to 3 flatMap (x=> 4 to 6 map (y=> (x,y)));System.out.println("""res10: scala.collection.immutable.IndexedSeq[(Int, Int)] = """ + $show(res$10));$skip(79); val res$11 = 
  
  (for (p<- List(1,2,3) if p==2; t<-List(1,2,3) if t>1) yield  p*t).isEmpty;System.out.println("""res11: Boolean = """ + $show(res$11));$skip(68); 
 
  def isPrime(n:Int):Boolean = (2 until n).forall(x=> n % x != 0);System.out.println("""isPrime: (n: Int)Boolean""");$skip(17); val res$12 = 
  
   isPrime(1);System.out.println("""res12: Boolean = """ + $show(res$12));$skip(14); val res$13 = 
   isPrime(2);System.out.println("""res13: Boolean = """ + $show(res$13));$skip(14); val res$14 = 
   isPrime(3);System.out.println("""res14: Boolean = """ + $show(res$14));$skip(14); val res$15 = 
   isPrime(4);System.out.println("""res15: Boolean = """ + $show(res$15));$skip(14); val res$16 = 
   isPrime(5);System.out.println("""res16: Boolean = """ + $show(res$16));$skip(14); val res$17 = 
   isPrime(6);System.out.println("""res17: Boolean = """ + $show(res$17));$skip(14); val res$18 = 
   isPrime(7);System.out.println("""res18: Boolean = """ + $show(res$18));$skip(127); 
   
   
   def scalarProduct(xs: List[Double], ys: List[Double]) : Double =
     (for( xy <- xs zip ys) yield xy._1*xy._2) sum;System.out.println("""scalarProduct: (xs: List[Double], ys: List[Double])Double""");$skip(46); val res$19 = 
   
   scalarProduct(List(1.0,2),List(3.0,4));System.out.println("""res19: Double = """ + $show(res$19))}
  }
