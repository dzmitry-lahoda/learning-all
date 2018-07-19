
fn dangling() -> ~int { // returns unique pointer
  let i = ~1234;//allocates in heap
  return i;
}

fn add_one() -> int {
  let num = dangling();
  return *num+1; //gets data out of pointer
}

fn main(){
  // uses macros with formatted string, 
  println!("{:d}",add_one());//'d' - stands for number replaced with funtion result
}
