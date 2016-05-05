
fn main(){
  let my_num = 22;
  
  match my_num{
   0     => println!("zero"),//single value match
   1 | 2 => println!("one or two"),// or match
   3..10 => println!("three or ten"),//range match
   _     => println!("else") // is must be exhaustive to compile
   }  
}
