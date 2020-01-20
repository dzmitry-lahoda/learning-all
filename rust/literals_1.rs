
fn main(){
  let myInt = 1;
  let myUnsignedInt = 1u;
  let myBinary = 0b10101010;
  let myOct = 0o123;
  let myHex = 0x123F;
  let myByte:u8 = 252;
  
  let myFloat = 2.1e-4f64;
  
  let myUnit = ();
  
  let myChar = 'c';
  let sizeOfChar = std::mem::size_of::<char>();
  println!("{}",sizeOfChar);//char is 4 bytes
  let myString = "my string";
  let myRawString = r##"
  -------
  -<box>-
  -------
  "##;
  println!("{}",myRawString);
  
}
