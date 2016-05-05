
fn main(){
  let mut x = ~5;//pointer to number
  if (*x<10){
    let y = &x;//borrow
    println!("{:?}",y);
    return;
  }//end of scope, x was not mutated
  *x = *x + 1;
  println!("{:?}",x);
}
