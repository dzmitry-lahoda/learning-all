//will not compile
fn main(){
  let mut x = ~5;//pointer to number
  if (*x<10){
    let y = &x;//borrow 
    *x = *x + 1;// cannot assign to `*x` because it is borrowed
    println!("{:?}",y);
    return;
  }//end of scope
  *x = *x + 1;
  println!("{:?}",x);
}
