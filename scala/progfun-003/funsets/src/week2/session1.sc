object session1 {
  def f (x:Int) = x                               //> f: (x: Int)Int
  
  // this by default
  def sum(f:Int => Int, a:Int, b:Int): Int = {
     def loop(a:Int, acc:Int):Int = {
       if (a > b) acc
       else loop(a+1,acc+f(a))
     }
     loop(a,0)
   }                                              //> sum: (f: Int => Int, a: Int, b: Int)Int
   
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
   }                                              //> sumc: (f: Int => Int)(a: Int, b: Int)Int
   
   def oc(combine: (Int,Int) => Int)(f:Int => Int)(a:Int,b:Int) : Int = {
     def ocf(): Int = {
       def loop(a:Int, acc:Int):Int = {
         if (a > b) acc
         else loop(a+1,combine(acc,f(a)))
       }
       loop(a,1)
     }
    ocf
   }                                              //> oc: (combine: (Int, Int) => Int)(f: Int => Int)(a: Int, b: Int)Int
   
   def product = oc ((x,y) => x * y) _            //> product: => (Int => Int) => ((Int, Int) => Int)
   def fact(n:Int) = product(x=> x)(1,n)          //> fact: (n: Int)Int
   fact(5)                                        //> res0: Int = 120
   sumc(x=>x)(0,3)                                //> res1: Int = 6
      //if (a>b) 0
    //else f(a) + sum(f,a+1,b)
  sum(x => x,0,3)                                 //> res2: Int = 6
}