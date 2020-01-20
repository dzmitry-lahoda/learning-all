package week1
import scala.math._
object seession5 {

def sqrt (x:Double)  =  {
    def isGoodEnough (guess:Double) = abs(guess * guess - x) / x < 0.01
      
    def improve(guess:Double) = (guess + x / guess)/2
  
    /// Newton's method
    def sqrtIter(guess:Double): Double =
      if (isGoodEnough(guess)) guess
      else sqrtIter(improve(guess))
         
    sqrtIter(1.0)
  }                                               //> sqrt: (x: Double)Double
  (1 +
  1)                                              //> res0: Int(2) = 2
  sqrt(2.0)                                       //> res1: Double = 1.4166666666666665
  sqrt(4.0)                                       //> res2: Double = 2.000609756097561
  sqrt(1e-20)                                     //> res3: Double = 1.0020750635502768E-10
  sqrt(1e4)                                       //> res4: Double = 100.00714038711746
  sqrt(1e26)                                      //> res5: Double = 1.0017592846850025E13
  sqrt(1e70)                                      //> res6: Double = 1.0026014327522157E35
}