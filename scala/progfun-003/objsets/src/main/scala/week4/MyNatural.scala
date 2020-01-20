package week4


abstract class MyNatural{
  def isZero: Boolean
  def predecessor: MyNatural
  def successor: MyNatural
  def + (that: MyNatural): MyNatural
  def - (that: MyNatural): MyNatural
}
