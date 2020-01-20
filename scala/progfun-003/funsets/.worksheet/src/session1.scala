object session1 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(37); 
  def f (x:Int) = x;System.out.println("""f: (x: Int)Int""");$skip(189); 
  
  // this by default
  def sum(f:Int => Int, a:Int, b:Int): Int = {
     def loop(a:Int, acc:Int):Int = {
       if (a > b) acc
       else loop(a+1,acc+f(a))
     }
     loop(a,0)
   };System.out.println("""sum: (f: Int => Int, a: Int, b: Int)Int""");$skip(454); 
   
   //currying
   //def sumc(f:Int => Int): (Int,b:Int) => Int = {
   // next is short hand
   def sumc(f:Int => Int)(a:Int,b:Int) : Int = {
   
     def sumf(/*a and b here*/): Int = {
       //substition model: body returned as result
       def loop(a:Int, acc:Int):Int = {
         if (a > b) acc
         else loop(a+1,acc+f(a))//substition model: f replaced by body
       }
       loop(a,0)
       //substition model: body
     }
    sumf
   };System.out.println("""sumc: (f: Int => Int)(a: Int, b: Int)Int""");$skip(254); 
   
   def oc(combine: (Int,Int) => Int)(f:Int => Int)(a:Int,b:Int) : Int = {
     def ocf(): Int = {
       def loop(a:Int, acc:Int):Int = {
         if (a > b) acc
         else loop(a+1,combine(acc,f(a)))
       }
       loop(a,1)
     }
    ocf
   };System.out.println("""oc: (combine: (Int, Int) => Int)(f: Int => Int)(a: Int, b: Int)Int""");$skip(43); 
   
   def product = oc ((x,y) => x * y) _;System.out.println("""product: => (Int => Int) => ((Int, Int) => Int)""");$skip(41); 
   def fact(n:Int) = product(x=> x)(1,n);System.out.println("""fact: (n: Int)Int""");$skip(11); val res$0 = 
   fact(5);System.out.println("""res0: Int = """ + $show(res$0));$skip(19); val res$1 = 
   sumc(x=>x)(0,3);System.out.println("""res1: Int = """ + $show(res$1));$skip(68); val res$2 = 
      //if (a>b) 0
    //else f(a) + sum(f,a+1,b)
  sum(x => x,0,3);System.out.println("""res2: Int = """ + $show(res$2))}
}
