package common

object lecture_113 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(85); 
 val capitalOfCountry = Map("US"-> "Washington");System.out.println("""capitalOfCountry  : scala.collection.immutable.Map[String,String] = """ + $show(capitalOfCountry ));$skip(24); val res$0 = 
 capitalOfCountry("US");System.out.println("""res0: String = """ + $show(res$0));$skip(29); val res$1 = 
 
 capitalOfCountry get "US";System.out.println("""res1: Option[String] = """ + $show(res$1));$skip(34); val res$2 = 
 
 capitalOfCountry get "Andorra"
 
    
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
  };System.out.println("""res2: Option[String] = """ + $show(res$2));$skip(691); 
  
  val p1 = new Poly(1->2.0,3-> 4.0,5->6.2);System.out.println("""p1  : common.lecture_113.Poly = """ + $show(p1 ));$skip(44); 
  
  val p2 = new Poly(Map(0->3.0,3-> 7.0));System.out.println("""p2  : common.lecture_113.Poly = """ + $show(p2 ));$skip(16); val res$3 = 
  p1.terms(100);System.out.println("""res3: Double = """ + $show(res$3));$skip(10); val res$4 = 
  p1 + p2;System.out.println("""res4: common.lecture_113.Poly = """ + $show(res$4));$skip(13); val res$5 = 
 p1 plus2 p2;System.out.println("""res5: common.lecture_113.Poly = """ + $show(res$5));$skip(60); val res$6 = 
 (capitalOfCountry withDefaultValue "<unknown>")("Andorra");System.out.println("""res6: String = """ + $show(res$6));$skip(26); val res$7 = 
 Map(1->2.0) + (1 -> 3.0);System.out.println("""res7: scala.collection.immutable.Map[Int,Double] = """ + $show(res$7));$skip(32); val res$8 = 
 
 capitalOfCountry ("Andorra");System.out.println("""res8: String = """ + $show(res$8))}
                          
 
 }
 
   trait MyOption[+A];
 case class MySome[+A](value:A) extends MyOption[A];
  object None extends MyOption[Nothing];
