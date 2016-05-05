

fn main(){
  let item = "cream";
  // `if` `else` is of expressions, not statements
  let price = // value of price is value of expression in {..}; block
     if item == "carrot" {
      1.0
     }
     else if item == "meat"{
       2.2
     } 
     else {
       3.3
     };     
     println!("{}",price);  
}
