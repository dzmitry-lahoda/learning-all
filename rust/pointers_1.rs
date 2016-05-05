
fn succ(x:&int) -> int { // expects int reference as argument
  return *x + 1;
}


fn main(){
  let number = 5;//allocates value onto stack, not reference
  let succ_number = succ(number);//error: expects &int
  println!("{}",succ_number);
}
