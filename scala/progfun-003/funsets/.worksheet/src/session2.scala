object session2 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(37); 
  def f (x:Int) = x;System.out.println("""f: (x: Int)Int""");$skip(27); 
  
  def r1 = new Rat(2,4);System.out.println("""r1: => Rat""");$skip(16); val res$0 = 
  r1.toString();System.out.println("""res0: String = """ + $show(res$0));$skip(23); 
  def r = new Rat(1,2);System.out.println("""r: => Rat""");$skip(15); val res$1 = 
  r.toString();System.out.println("""res1: String = """ + $show(res$1));$skip(30); val res$2 = 
  //new Rat(1,0)
  
  r.+(r1);System.out.println("""res2: Rat = """ + $show(res$2));$skip(11); val res$3 = 
  r add r1;System.out.println("""res3: Rat = """ + $show(res$3));$skip(12); val res$4 = 
  r.add(r1);System.out.println("""res4: Rat = """ + $show(res$4));$skip(14); val res$5 = 
  r + r1 + r1;System.out.println("""res5: Rat = """ + $show(res$5));$skip(20); val res$6 = 
  r.add(r1).add(r1);System.out.println("""res6: Rat = """ + $show(res$6))}
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
  
  