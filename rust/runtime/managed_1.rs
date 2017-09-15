#[feature(managed_boxes)]

struct Point {
  x:int,
  y:int,
}

fn main(){
  let a = @Point {x:10,y:20};//creates managed pointer
  let b = a;
  //can pass into 2 other users
  println(b.x.to_str());
  println(a.x.to_str());
}
