
fn main(){
   
   let mut cake_num = 8;
   while cake_num > 0{
     cake_num -= 1;
     println!("while eating {}",cake_num);
   }
   
   let mut x  = 5u;
   loop{
     x+= x - 3;
     if x % 5 == 0 {break;}
     println!("loop {}",x);
   }
   
   for n in range(0,5){
     println!("for range {}",n);
   }
   
   let  s = "Hello";
   for c in s.chars(){ // gets Iterator
     println!("Iterate {}",c);
   }
}
