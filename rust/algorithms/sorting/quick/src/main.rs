use std::*;
use std::collections::*;
use std::io::*;
use std::fmt::*;
use std::str::*;

trait TupleLen {
    fn len(&self) -> u8;
}

impl<T1> TupleLen for (T1,) {
    fn len(&self) -> u8 { 1 }
}
impl<T1,T2> TupleLen for (T1,T2) {
    fn len(&self) -> u8 { 2 }
}
impl<T1,T2,T3> TupleLen for (T1,T2,T3) {
    fn len(&self) -> u8 { 3 }
}
impl<T1,T2,T3,T4> TupleLen for (T1,T2,T3,T4) {
    fn len(&self) -> u8 { 4 }
}

trait ReadNumberStdin {
    fn read_u8(&self) -> u8;
    fn read_u16(&self) -> u16;
    fn read_vec<T:FromStr + Debug>(&self) -> Vec<T>;
} 

impl ReadNumberStdin for Stdin {
    fn read_u8(&self) -> u8 {
        let mut examples_input = String::new();
        self.read_line(& mut examples_input).unwrap();
        examples_input.trim().parse::<u8>().unwrap()
    }

    // TODO: make it generic like parse::<u32> with restiction of being parsable
    fn read_u16(&self) -> u16 {
        let mut examples_input = String::new();
        self.read_line(& mut examples_input).unwrap();
        examples_input.trim().parse::<u16>().unwrap()
    }
    
    fn read_vec<T:FromStr + Debug>(&self) -> Vec<T> {
        let mut input = String::new(); 
        self.read_line(& mut input).unwrap();
        input.trim().split_whitespace().map(|x| x.parse::<T>().ok().unwrap()).collect()
    }
}

fn print<T:Display>(items:&[T]) {
    for (i, e) in items.iter().enumerate(){
        print!("{}", e);
        if i != items.len() - 1 {
            print!(" ");
        }
    }
    println!("");
}

fn print_iter<'a>(items:impl Iterator<Item=&'a i64>) {
    let first = true;
    for i in items {
        print!("{}", i);
        print!(" ");        
    }
}

fn partition(arr:&[i64] )  {
    let left = arr.iter().filter(|x| **x < arr[0]);
    let right = arr.iter().filter(|x| **x > arr[0]);
    print_iter(left);
    print!("{} ", arr[0]);
    print_iter(right);    
    println!("");
}

fn main(){
    let _ = stdin().read_u16();
    let mut arr = stdin().read_vec::<i64>();   
    partition(&mut arr);
}
