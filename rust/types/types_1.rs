// inference

static VALUE1 : f64 = 44.123;//ltype inference does not works here

fn main(){
   let _value0 = 123;//no warning about unused variable
   let value = VALUE1 * 10.0;// type is inferred here
   let value: int = 50;//optionally we put here type, rewrites previous declration
}
