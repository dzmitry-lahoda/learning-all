
struct ManyFields{
  x: int,
  y: ~str,
}

fn main(){ 
  let i = ManyFields { x:1, y: ~"123" };
  //println!("{:s}",1);// compiler detecs 's' for string and fails with '1' as int error
  println!("{:?}",i);//macro expands all srings via reflection in compile time
  println(i.x.to_str());//print string
}
