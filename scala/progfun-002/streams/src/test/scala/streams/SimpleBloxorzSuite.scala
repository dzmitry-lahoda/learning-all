package streams

import org.scalatest.FunSuite

import org.junit.runner.RunWith
import org.scalatest.junit.JUnitRunner

import Bloxorz._

@RunWith(classOf[JUnitRunner])
class SimpleBloxorzSuite extends FunSuite {

  trait SolutionChecker extends GameDef with Solver with StringParserTerrain {
    /**
     * This method applies a list of moves `ls` to the block at position
     * `startPos`. This can be used to verify if a certain list of moves
     * is a valid solution, i.e. leads to the goal.
     */
    def solve(ls: List[Move]): Block =
      ls.foldLeft(startBlock) { case (block, move) => move match {
        case Left => block.left
        case Right => block.right
        case Up => block.up
        case Down => block.down
      }
    }
  }

  
  
  
  
  
    trait Level0 extends SolutionChecker {
    val level =
    """SooT""".stripMargin

  }

  
    test("level 0") {
    new Level0 {
      assert(startPos == new Pos(0,0))
      assert(goal == new Pos(0,3))
      assert(Block(goal, goal).isLegal)

    }
  }
    
        test("optimal solution for level 0") {
    new Level0 {
    
     

      assert(solve(solution) === Block(goal, goal))
    }
  }

  
}
