package common

object lecture_107 {

  def calcCoords(queens: List[Int]): Seq[(Int, Int)] =
    {
      var row = queens.length - 1
      (row to 0 by -1).zip(queens)
    }                                             //> calcCoords: (queens: List[Int])Seq[(Int, Int)]
  def isSafeDiagonal(col: Int, queens: List[Int]): Boolean = {
    var row = queens.length
    var coords = calcCoords(queens)
    coords.forall { case (r, c) => row - r != Math.abs(col - c) }
  }                                               //> isSafeDiagonal: (col: Int, queens: List[Int])Boolean

  def queens(n: Int): Set[List[Int]] = {

    def isSafe(col: Int, queens: List[Int]): Boolean = {
      if (queens contains col) false
      else if (isSafeDiagonal(col, queens)) true
      else false
    }

    def placeQueens(k: Int): Set[List[Int]] =
      if (k == 0) Set(List())
      else
        for {
          queens <- placeQueens(k - 1)
          col <- 0 until n
          if isSafe(col, queens)
        } yield col :: queens
    placeQueens(n)
  }                                               //> queens: (n: Int)Set[List[Int]]

  calcCoords(List(0))                             //> res0: Seq[(Int, Int)] = Vector((0,0))
  calcCoords(List(1))                             //> res1: Seq[(Int, Int)] = Vector((0,1))
  calcCoords(List(2, 3))                          //> res2: Seq[(Int, Int)] = Vector((1,2), (0,3))
  calcCoords(List(2, 3, 4))                       //> res3: Seq[(Int, Int)] = Vector((2,2), (1,3), (0,4))
  calcCoords(List(1, 0))                          //> res4: Seq[(Int, Int)] = Vector((1,1), (0,0))

  isSafeDiagonal(1, List(0))                      //> res5: Boolean = false
  isSafeDiagonal(1, List(1))                      //> res6: Boolean = true
  isSafeDiagonal(2, List(2, 2))                   //> res7: Boolean = true
  isSafeDiagonal(1, List(2, 2))                   //> res8: Boolean = false
  isSafeDiagonal(3, List(0, 2, 2, 0))             //> res9: Boolean = true
  isSafeDiagonal(1, List(1, 0))                   //> res10: Boolean = true

  queens(2)                                       //> res11: Set[List[Int]] = Set()
  queens(3)                                       //> res12: Set[List[Int]] = Set()
  queens(4)                                       //> res13: Set[List[Int]] = Set(List(1, 3, 0, 2), List(2, 0, 3, 1))
  queens(5)                                       //> res14: Set[List[Int]] = Set(List(0, 3, 1, 4, 2), List(2, 0, 3, 1, 4), List(
                                                  //| 0, 2, 4, 1, 3), List(2, 4, 1, 3, 0), List(1, 3, 0, 2, 4), List(3, 0, 2, 4, 
                                                  //| 1), List(4, 2, 0, 3, 1), List(4, 1, 3, 0, 2), List(3, 1, 4, 2, 0), List(1, 
                                                  //| 4, 2, 0, 3))
  queens(6)                                       //> res15: Set[List[Int]] = Set(List(3, 0, 4, 1, 5, 2), List(4, 2, 0, 5, 3, 1),
                                                  //|  List(2, 5, 1, 4, 0, 3), List(1, 3, 5, 0, 2, 4))

}