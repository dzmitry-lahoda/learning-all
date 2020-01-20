package common

object lecture_103 {
  Vector(1,2,3)// is shallow tree                 //> res0: scala.collection.immutable.Vector[Int] = Vector(1, 2, 3)
  // 32 is large - 32 of 32 = 20^10, etc
  
  // log_32 N access
  Vector(1,2,3)(1)                                //> res1: Int = 2
  
  // maps 32 to cache of processor and done as batch, good localyty
  Vector(1,2,3) map (x=> x* x)                    //> res2: scala.collection.immutable.Vector[Int] = Vector(1, 4, 9)
  
  
  //log_32 N
  val v1 = Vector(1,2) :+ 3                       //> v1  : scala.collection.immutable.Vector[Int] = Vector(1, 2, 3)
  
  1 +: Vector(2,3)                                //> res3: scala.collection.immutable.Vector[Int] = Vector(1, 2, 3)
  
  (1 +: Vector(2,3)  ) == (1 +: Vector(2,3)  )    //> res4: Boolean = true
  
  val i2:Iterable[Int] = v1                       //> i2  : Iterable[Int] = Vector(1, 2, 3)
  i2.iterator.next                                //> res5: Int = 1
  val i3 = i2.iterator                            //> i3  : Iterator[Int] = non-empty iterator
  i3.next                                         //> res6: Int = 1
  i3.next                                         //> res7: Int = 2
  val ar = Array(1,2,3)                           //> ar  : Array[Int] = Array(1, 2, 3)
 
  // iter
  //seq          set     map
  // vector list
  //
  // array string
  
  val r:Range = 1 until 11 by 3                   //> r  : Range = Range(1, 4, 7, 10)
  
  val s= "Hello"                                  //> s  : String = Hello
  s flatMap (c => List('.',c))                    //> res8: String = .H.e.l.l.o
  2 % 3                                           //> res9: Int(2) = 2
  1 to 3 flatMap (x=> 4 to 6 map (y=> (x,y)))     //> res10: scala.collection.immutable.IndexedSeq[(Int, Int)] = Vector((1,4), (1,
                                                  //| 5), (1,6), (2,4), (2,5), (2,6), (3,4), (3,5), (3,6))
  
  (for (p<- List(1,2,3) if p==2; t<-List(1,2,3) if t>1) yield  p*t).isEmpty
                                                  //> res11: Boolean = false
 
  def isPrime(n:Int):Boolean = (2 until n).forall(x=> n % x != 0)
                                                  //> isPrime: (n: Int)Boolean
  
   isPrime(1)                                     //> res12: Boolean = true
   isPrime(2)                                     //> res13: Boolean = true
   isPrime(3)                                     //> res14: Boolean = true
   isPrime(4)                                     //> res15: Boolean = false
   isPrime(5)                                     //> res16: Boolean = true
   isPrime(6)                                     //> res17: Boolean = false
   isPrime(7)                                     //> res18: Boolean = true
   
   
   def scalarProduct(xs: List[Double], ys: List[Double]) : Double =
     (for( xy <- xs zip ys) yield xy._1*xy._2) sum//> scalarProduct: (xs: List[Double], ys: List[Double])Double
   
   scalarProduct(List(1.0,2),List(3.0,4))         //> res19: Double = 11.0
  }