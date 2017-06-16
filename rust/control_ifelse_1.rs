

fn sign(x:i64) -> i64 {
  if x > 0 {1} else if x < 0 {-1} else {0}
}

fn main(){
 println!("{}", sign(-1));
 if false {
   println!(":(")
 }
 else if true {
   println!(":)")
 }  
}
