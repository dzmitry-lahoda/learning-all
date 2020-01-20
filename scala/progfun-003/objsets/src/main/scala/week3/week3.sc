object week3 {
  type A = IntSet => NonEmpty
  type B = NonEmpty => IntSet
  
  var a:A = (s:IntSet) => new NonEmpty(1,s,s)     //> a  : IntSet => NonEmpty = <function1>
  var b:B = (s:NonEmpty) => new NonEmpty(1,s,s)   //> b  : NonEmpty => IntSet = <function1>
  //a = b
  // a receives more then and returns more specific then b
  // a can recive what b can and return what b can
  // b can receive a can, b returns includes a
  
  b = a
  println("Welcome to the Scala worksheet")       //> Welcome to the Scala worksheet
  
  
  def funSet = new FunIntSet(22) incl 3 incl 4 incl 7 incl 33
                                                  //> funSet: => IntSet
  def binaryTreeSet = new NonEmpty(3) incl 4 incl 22 incl 7 incl 33
                                                  //> binaryTreeSet: => IntSet
  def bts2 = new NonEmpty(5) incl 123             //> bts2: => IntSet
  def ft2 = new FunIntSet(5) incl 42              //> ft2: => IntSet
  funSet union ft2                                //> res0: IntSet =  3 4 5 7 22 33 42
  binaryTreeSet union bts2                        //> res1: IntSet =  3 4 5 7 22 33 123 
  binaryTreeSet union ft2                         //> res2: IntSet =  3 4 5 7 22 33 42
  funSet union bts2                               //> res3: IntSet =  3 4 5 7 22 33 123
  funSet                                          //> res4: IntSet =  3 4 7 22 33
  binaryTreeSet                                   //> res5: IntSet =  3 4 7 22 33 
  funSet contains 22                              //> res6: Boolean = true
  binaryTreeSet contains 11                       //> res7: Boolean = false
  GeneticVariance.subtype_bound(binaryTreeSet)    //> class NonEmpty
  GeneticVariance.supertype_bound(ft2)            //> class FunIntSet
  val aaa:Array[NonEmpty] = Array(new NonEmpty(5))//> aaa  : Array[NonEmpty] = Array( 5 )
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