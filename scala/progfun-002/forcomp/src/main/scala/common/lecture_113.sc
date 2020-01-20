package common

object lecture_113 {
 val capitalOfCountry = Map("US"-> "Washington")  //> capitalOfCountry  : scala.collection.immutable.Map[String,String] = Map(US ->
                                                  //|  Washington)
 capitalOfCountry("US")                           //> res0: String = Washington
 
 capitalOfCountry get "US"                        //> res1: Option[String] = Some(Washington)
 
 capitalOfCountry get "Andorra"                   //> res2: Option[String] = None
 
    
  class Poly(terms0:Map[Int,Double]){
    def this(bindings: (Int,Double)* ) = this(bindings.toMap)
    val terms = terms0 withDefaultValue(0.0)
    def +(other:Poly)=  new Poly(terms map   ( term => term._1 -> (other.terms(term._1) + term._2)))
    
    def addTerm(terms: Map[Int, Double], term: (Int, Double)):Map[Int, Double] =
       {
       val (exp,coeff) =term
       terms + (exp-> (coeff + terms(exp)))
       }
    
    def plus2 (other: Poly) =   new Poly((other.terms foldLeft terms)(addTerm))


    
    override def toString =   (for ((exp,coeff) <- terms.toList.sorted.reverse) yield coeff + "x^"+ exp) mkString "+ "
  }
  
  val p1 = new Poly(1->2.0,3-> 4.0,5->6.2)        //> p1  : common.lecture_113.Poly = 6.2x^5+ 4.0x^3+ 2.0x^1
  
  val p2 = new Poly(Map(0->3.0,3-> 7.0))          //> p2  : common.lecture_113.Poly = 7.0x^3+ 3.0x^0
  p1.terms(100)                                   //> res3: Double = 0.0
  p1 + p2                                         //> res4: common.lecture_113.Poly = 6.2x^5+ 11.0x^3+ 2.0x^1
 p1 plus2 p2                                      //> res5: common.lecture_113.Poly = 6.2x^5+ 11.0x^3+ 2.0x^1+ 3.0x^0
 (capitalOfCountry withDefaultValue "<unknown>")("Andorra")
                                                  //> res6: String = <unknown>
 Map(1->2.0) + (1 -> 3.0)                         //> res7: scala.collection.immutable.Map[Int,Double] = Map(1 -> 3.0)
 
 capitalOfCountry ("Andorra")                     //> java.util.NoSuchElementException: key not found: Andorra
                                                  //| 	at scala.collection.MapLike$class.default(MapLike.scala:228)
                                                  //| 	at scala.collection.AbstractMap.default(Map.scala:58)
                                                  //| 	at scala.collection.MapLike$class.apply(MapLike.scala:141)
                                                  //| 	at scala.collection.AbstractMap.apply(Map.scala:58)
                                                  //| 	at common.lecture_113$$anonfun$main$1.apply$mcV$sp(common.lecture_113.sc
                                                  //| ala:39)
                                                  //| 	at org.scalaide.worksheet.runtime.library.WorksheetSupport$$anonfun$$exe
                                                  //| cute$1.apply$mcV$sp(WorksheetSupport.scala:76)
                                                  //| 	at org.scalaide.worksheet.runtime.library.WorksheetSupport$.redirected(W
                                                  //| orksheetSupport.scala:65)
                                                  //| 	at org.scalaide.worksheet.runtime.library.WorksheetSupport$.$execute(Wor
                                                  //| ksheetSupport.scala:75)
                                                  //| 	at common.lecture_113$.main(common.lecture_113.scala:3)
                                                  //| 	at common.lecture_113.main(common.lecture_113.scala)
                          
 
 }
 
   trait MyOption[+A];
 case class MySome[+A](value:A) extends MyOption[A];
  object None extends MyOption[Nothing];