use std::num::sqrt;// finds square root
use std::rc::Rc;// reference counted pointers

// piece of memory with 2 32 bit fields layout one by one into 64 bit chunk
struct Point{
  x: f32,
  y: f32,
}

// finds euclidian distance between to points
fn compute_distance(p1: &Point, p2: &Point) -> f32 { 
  let x_d = p1.x - p2.x;
  let y_d = p1.y - p2.y;
  sqrt(x_d*x_d + y_d * y_d)
}

fn main(){
  let p1 = ~Point {x:0.0,y:5.0};// owning unique pointer
  let p2 = Rc::new( Point {x:5.0,y:0.0});//reference counted sscollected one
  println! ("{:?}",compute_distance(p1,p2.borrow()));//let function borrow pointers
  println! ("{:?}",p1);
}
