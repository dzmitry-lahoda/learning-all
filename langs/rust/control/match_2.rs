
use std::f64;
use std::num::atan;

fn angle(vector : (f64,f64)) -> f64 {
  let pi = f64::consts::PI;
  match vector {
   (0.0,y) if y < 0.0 => 1.5 * pi,  // if EXPR is `pattern guard`
   (0.0,_)            => 0.5 * pi,
   (x,y)              => atan (y/x)
  }
}

fn main(){
  let v = angle((0.1,1.111));
  println!("{}",v);
  
  let age = 20;
  match age {
    a @ 0..20 => println!("{} young",a),//gives names to match
    _ => println!("old")
  }
}
