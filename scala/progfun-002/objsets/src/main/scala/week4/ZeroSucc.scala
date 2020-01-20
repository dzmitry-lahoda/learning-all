package week4

// Peano numbers
object Zero extends MyNatural{
  def isZero:Boolean = true
  def predecessor:MyNatural = throw new Error()
  def successor:MyNatural = new Succ(this)
  def + (that: MyNatural):MyNatural = that
  def - (that: MyNatural):MyNatural = if (that.isZero) this else throw new Error()
}
class Succ(n: MyNatural) extends MyNatural{
  def isZero:Boolean = false
  def predecessor:MyNatural = n
  def successor: MyNatural = new Succ(this)
  def + (that: MyNatural): MyNatural = new Succ(n+that)
  /*{
    def iter(acc:MyNatural,dec:MyNatural):MyNatural =
      if (dec.isZero) acc
      else iter(acc.successor,dec.predecessor)
    iter(this,that)
  }*/
   def - (that: MyNatural): MyNatural = if (that.isZero) this else n - that.predecessor
   /*{
    def iter(acc:MyNatural,dec:MyNatural):MyNatural =
      if (dec.isZero) acc
      else iter(acc.predecessor,dec.predecessor)
    iter(this,that)
  }*/
}