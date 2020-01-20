package week1
import scala.math._
object seession5 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(385); 

def sqrt (x:Double)  =  {
    def isGoodEnough (guess:Double) = abs(guess * guess - x) / x < 0.01
      
    def improve(guess:Double) = (guess + x / guess)/2
  
    /// Newton's method
    def sqrtIter(guess:Double): Double =
      if (isGoodEnough(guess)) guess
      else sqrtIter(improve(guess))
         
    sqrtIter(1.0)
  };System.out.println("""sqrt: (x: Double)Double""");$skip(12); val res$0 = 
  (1 +
  1);System.out.println("""res0: Int(2) = """ + $show(res$0));$skip(12); val res$1 = 
  sqrt(2.0);System.out.println("""res1: Double = """ + $show(res$1));$skip(12); val res$2 = 
  sqrt(4.0);System.out.println("""res2: Double = """ + $show(res$2));$skip(14); val res$3 = 
  sqrt(1e-20);System.out.println("""res3: Double = """ + $show(res$3));$skip(12); val res$4 = 
  sqrt(1e4);System.out.println("""res4: Double = """ + $show(res$4));$skip(13); val res$5 = 
  sqrt(1e26);System.out.println("""res5: Double = """ + $show(res$5));$skip(13); val res$6 = 
  sqrt(1e70);System.out.println("""res6: Double = """ + $show(res$6))}
}
