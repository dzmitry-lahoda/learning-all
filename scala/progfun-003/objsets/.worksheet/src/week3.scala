object week3 {
  type A = IntSet => NonEmpty
  type B = NonEmpty => IntSet;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(123); 
  
  var a:A = (s:IntSet) => new NonEmpty(1,s,s);System.out.println("""a  : IntSet => NonEmpty = """ + $show(a ));$skip(48); 
  var b:B = (s:NonEmpty) => new NonEmpty(1,s,s);System.out.println("""b  : NonEmpty => IntSet = """ + $show(b ));$skip(178); 
  //a = b
  // a receives more then and returns more specific then b
  // a can recive what b can and return what b can
  // b can receive a can, b returns includes a
  
  b = a;$skip(44); 
  println("Welcome to the Scala worksheet");$skip(68); 
  
  
  def funSet = new FunIntSet(22) incl 3 incl 4 incl 7 incl 33;System.out.println("""funSet: => IntSet""");$skip(68); 
  def binaryTreeSet = new NonEmpty(3) incl 4 incl 22 incl 7 incl 33;System.out.println("""binaryTreeSet: => IntSet""");$skip(38); 
  def bts2 = new NonEmpty(5) incl 123;System.out.println("""bts2: => IntSet""");$skip(37); 
  def ft2 = new FunIntSet(5) incl 42;System.out.println("""ft2: => IntSet""");$skip(19); val res$0 = 
  funSet union ft2;System.out.println("""res0: IntSet = """ + $show(res$0));$skip(27); val res$1 = 
  binaryTreeSet union bts2;System.out.println("""res1: IntSet = """ + $show(res$1));$skip(26); val res$2 = 
  binaryTreeSet union ft2;System.out.println("""res2: IntSet = """ + $show(res$2));$skip(20); val res$3 = 
  funSet union bts2;System.out.println("""res3: IntSet = """ + $show(res$3));$skip(9); val res$4 = 
  funSet;System.out.println("""res4: IntSet = """ + $show(res$4));$skip(16); val res$5 = 
  binaryTreeSet;System.out.println("""res5: IntSet = """ + $show(res$5));$skip(21); val res$6 = 
  funSet contains 22;System.out.println("""res6: Boolean = """ + $show(res$6));$skip(28); val res$7 = 
  binaryTreeSet contains 11;System.out.println("""res7: Boolean = """ + $show(res$7));$skip(47); 
  GeneticVariance.subtype_bound(binaryTreeSet);$skip(39); 
  GeneticVariance.supertype_bound(ft2);$skip(51); 
  val aaa:Array[NonEmpty] = Array(new NonEmpty(5));System.out.println("""aaa  : Array[NonEmpty] = """ + $show(aaa ))}
  //val bbb:Array[IntSet] = aaa
}



object GeneticVariance{
  def subtype_bound[S <: IntSet](s:S) =  println(s.getClass())
  def supertype_bound[S >: IntSet](s:S) =  println(s.getClass())
}

class NonEmpty(elem:Int,left:IntSet,right:IntSet) extends IntSet{
  
  def this(elem:Int) = this(elem, EmptySet, EmptySet)
  def contains(x:Int) =
     if (x < elem) left contains x
     else if (x>elem) right contains x
     else true
  def incl(x:Int):IntSet =
    if (x< elem) new NonEmpty(elem,left incl x,right)
    else if (x > elem) new NonEmpty(elem,left, right incl x)
    else this
  def union(other:IntSet):IntSet =  left union right union other incl elem
  override def toString = left+ elem.toString() +  right//can use next for introspection "{" + left+ elem +  right + "}"
  
}

object EmptySet extends IntSet{
  def contains(x:Int) = false
  def incl(x:Int):IntSet = new NonEmpty(x,EmptySet, EmptySet)
  def union(x:IntSet):IntSet = x
  override def toString() = " "//an use next for introspection  "."
}

class FunIntSet(set:Int => Boolean) extends IntSet{
  val bound = 1000
  type FunSet = Int => Boolean
  def this(x:Int) = this( (o:Int)=> o == x)
  def singletonSet(x:Int):FunSet = (other:Int) => other == x
  def containsImpl (x:Int,s:FunSet) = s(x)
  def unionImpl (x:FunSet,y:FunSet) = (other:Int) => x(other) || y(other)
  def incl(x:Int):IntSet =  new FunIntSet(unionImpl(set,singletonSet(x)))
  def contains(x:Int):Boolean =  containsImpl(x,set)
  def union(x:IntSet): IntSet = new FunIntSet(unionImpl(this.set, (y:Int) => x.contains(y)))
  def iter(a:Int,s:String):String =
    if (a== bound) s
    else if (contains(a))  iter(a+1,s + " " + a.toString())
    else iter(a+1,s)
  override def toString =  iter(-bound, "")
}


abstract class IntSet{
 def incl(x:Int):IntSet
 def contains(x:Int):Boolean
 def union(x:IntSet):IntSet
}
