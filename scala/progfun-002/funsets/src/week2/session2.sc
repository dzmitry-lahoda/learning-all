object session2 {
  def f (x:Int) = x                               //> f: (x: Int)Int
  
  def r1 = new Rat(2,4)                           //> r1: => Rat
  r1.toString()                                   //> res0: String = 1/2
  def r = new Rat(1,2)                            //> r: => Rat
  r.toString()                                    //> res1: String = 1/2
  //new Rat(1,0)
  
  r.+(r1)                                         //> res2: Rat = 1/1
  r add r1                                        //> res3: Rat = 1/1
  r.add(r1)                                       //> res4: Rat = 1/1
  r + r1 + r1                                     //> res5: Rat = 3/2
  r.add(r1).add(r1)                               //> res6: Rat = 3/2
}

  class Rat(n:Int,d: => Int){
    require(d>0)
    
    def this(x:Int) = this(x,1)
   
    private def gcd (a:Int, b:Int) : Int = if (b == 0) a else gcd(b, a % b)
    private val g = gcd(n,d)
    val numer = n / g
    val denom = d / g
    def add(r : Rat) =  new Rat(numer * r.denom + denom * r.numer, denom *r.denom)
    def + (r: Rat) = add(r)
    def neg() = new Rat(-numer,denom)
    def unary_- = neg
    def sub(r:Rat) = this + -r
    override def toString() = numer + "/" + denom
  }
  
  //file A.scala
trait A { self: B =>
  def af = 2
}

//file B.scala
trait B { self: A =>
  def bf = 3.0
}

//file C.scala
class C extends A with B
  
  