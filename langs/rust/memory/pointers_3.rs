

struct Point{
  x:int,
  y:int,
}

fn transform(p:Point) -> Point {// copying is ok for performance until proven otherwice via testing
  Point {x: p.x + 1, y: p.y +1}
}

fn main(){
  let p = Point {x:5,y:10};
  let p1 = transform(p);
  println!("{:?}",p1);//uses formating wich reflects upon point (seems in compile time) to show all fields
}
