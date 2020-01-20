package greeter

//import example.Learning._
import org.scalatest.FunSuite
import org.junit.runner.RunWith
import org.scalatest.junit.JUnitRunner

@RunWith(classOf[JUnitRunner])
class LearningSuite extends FunSuite {
  test("and"){
    assert(Learning.and(true,true))
    assert(!Learning.and(true,false))
    assert(!Learning.and(false,true))
    assert(!Learning.and(false,false))
  }
  

  
}