

fn main(){
  let a = ~1;//creates pointer and owns it
  let b = a;// passses ownership to other variable
  println!("{:?}",a);//fails to compile because of a
}
