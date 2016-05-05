

pub fn isPrime(val:i8) -> bool{
  let rem = val % 2;
  match rem {
     0 => true,
     _ => false
  }
} 

fn main(){
  println!("{:?}",isPrime(6));
}
