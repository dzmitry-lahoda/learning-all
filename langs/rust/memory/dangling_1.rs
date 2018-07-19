
//will fail to compile
pub fn cldangling() -> &int {
    let i = 1234;//allocated into heap
    return &i; // borrowed value does not live long enough
}

fn main(){
  
}
