
fn succ(x:&int) -> int { return *x + 1;}


fn main(){
  let number = 5;
  let succ_number = succ(&number);//obtains and passes reference
  println!("{}",succ_number);
}

