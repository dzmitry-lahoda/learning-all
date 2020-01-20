package recfun

import org.scalatest.FunSuite

import org.junit.runner.RunWith
import org.scalatest.junit.JUnitRunner

@RunWith(classOf[JUnitRunner])
class CountChangeSuite extends FunSuite {
  import Main.countChange

 test("countChange: 0") {
    assert(countChange(0,List(1,2)) === 0)
  }
  
  test("countChange: 1, no coins") {
    assert(countChange(1,List()) === 0)
  }
  
  test("countChange: 2, no coins") {
    assert(countChange(2,List()) === 0)
  }
  
  test("countChange: 2, coins: 1") {
    assert(countChange(2,List(1)) === 1)
  }  
  
  test("countChange: 3, coins: 1 2") {
    assert(countChange(3,List(1,2)) === 2)
  }  
  
  test("countChange: 3, coins: 1") {
    assert(countChange(3,List(1)) === 1)
  }  
  
  test("countChange: 3, coins: 3") {
    assert(countChange(3,List(3)) === 1)
  }  
  
  test("countChange: 3, coins: 4") {
    assert(countChange(3,List(4)) === 0)
  }  
  
  test("countChange: 3, coins: 1 2 3") {
    assert(countChange(3,List(1,2,3)) === 3)
  }  
  
  test("countChange: example given in instructions") {
    assert(countChange(4,List(1,2)) === 3)
  }

  test("countChange: sorted CHF") {
    assert(countChange(300,List(5,10,20,50,100,200,500)) === 1022)
  }

  test("countChange: no pennies") {
    assert(countChange(301,List(5,10,20,50,100,200,500)) === 0)
  }

  test("countChange: unsorted CHF") {
    assert(countChange(300,List(500,5,50,100,20,200,10)) === 1022)
  }
}
