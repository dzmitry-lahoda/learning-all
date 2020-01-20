package recfun
import common._

object Main {
  def main(args: Array[String]) {
    println("Pascal's Triangle")
    for (row <- 0 to 10) {
      for (col <- 0 to row)
        print(pascal(col, row) + " ")
      println()
    }
  }

  /**
   * Exercise 1
   */
  def pascal(c: Int, r: Int): Int = {
     if (c > r || c < 0 || r<0) 
       throw new java.lang.IllegalArgumentException()
     else if (r == 0 || r == 1 || c == 0 || r == c) 
       1
     else pascal(c-1, r-1)+pascal(c, r-1)
  }
  /**
   * Exercise 2
   */
  def balance(chars: List[Char]): Boolean = {
    def balance_rec(chars:List[Char],unbalanced:Int): Boolean ={
      if (unbalanced < 0)
        false
      else if (chars.isEmpty )
         unbalanced == 0
      else if (chars.head == '(')
        balance_rec(chars.tail,unbalanced+1)
      else if (chars.head == ')')
        balance_rec(chars.tail,unbalanced-1)
      else 
        balance_rec(chars.tail,unbalanced)
    }      
    balance_rec(chars,0)      
  }

  /**
   * Exercise 3
   */
  def countChange(money: Int, coins: List[Int]): Int = {
    def countChange_rec(money: Int, coins: List[Int],hand:Int): Int = {
    if (money == 0)
     0
    else if (coins.length == 0)
     0
    else if (hand == money)
      1
    else if (hand > money)
     0
    else
       countChange_rec(money,coins,hand+coins.head) +
       countChange_rec(money,coins.tail,hand)
    }
    countChange_rec(money,coins,0)

     
  }
  
}
