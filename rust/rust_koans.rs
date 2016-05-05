/******
 * This file is generated with RustyCage
 */


#[test]
fn test_dangling(){

  // println!("{:d}", reustl)
}


#[test]
fn formatting() {
   

    // formatted printing ordinals/integer numbers
    println!("{:d}", 1);//type checked by macro - will fail if try to print string
}

pub type Complex = (f64,f64);
pub fn dangling2() -> ~int { // returns unique pointer
   let i = ~1234;//allocate in heap
   return i;
}



pub fn add_one() -> int{
  let num = dangling2();
  return *num + 1;
}



/***
*fn private(){//private function 
*}
*/

/***
*pub fn dangling1() -> &int {
*   let i = 1234;//allocate in stack
*   return &i;//borrowed value does not live long enough
*}
*/