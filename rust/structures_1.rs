
enum List<T>{
  Nil,
  Cons(T,~List<T>),
}

fn main(){
  // list is trait
  // '~' pointer to trait, trait can be of diffent size to allocate, hence should always use pointer
  let list: List<int> = Cons(1,~Cons(2,~Cons(3,~Nil)));
  println!("{:?}", list);
}
