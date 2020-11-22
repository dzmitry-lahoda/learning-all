use std::str::*;
use std::*;
use std::collections::*;
use std::io::*;
use std::fmt::*;
use std::iter::*;
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
impl<T1,T2,T3,T4,T5> TupleLen for (T1,T2,T3,T4,T5) {
    fn len(&self) -> u8 { 4 }
}

impl<T1,T2,T3,T4,T5, T6> TupleLen for (T1,T2,T3,T4,T5, T6) {
    fn len(&self) -> u8 { 4 }
}

trait Python<T> {
    fn enumerate(&self) -> Enumerate<std::slice::Iter<T>>;
}

impl<T> Python<T> for Vec<T> {
    fn enumerate(&self) -> Enumerate<std::slice::Iter<T>>{
        self.iter().enumerate()
    }
}

impl<T> Python<T> for [T] {
    fn enumerate(&self) -> Enumerate<std::slice::Iter<T>>{
        self.iter().enumerate()
    }
}

trait ReadNumberStdin {
    fn read_u8(&self) -> u8;
    fn read_u16(&self) -> u16;    
    fn read_n<T:FromStr>(&self) -> T;
    fn read_u32s(&self) -> Vec<u32>;
} 

impl ReadNumberStdin for Stdin {
    fn read_u8(&self) -> u8 {
        let mut examples_input = String::new();
        self.read_line(& mut examples_input).unwrap();
        examples_input.trim().parse::<u8>().unwrap()
    }

    // TODO: make it generic like parse::<u32> with restriction of being parsable
    fn read_u16(&self) -> u16 {
        let mut examples_input = String::new();
        self.read_line(& mut examples_input).unwrap();
        examples_input.trim().parse().unwrap()
    }

    fn read_n<T:FromStr>(&self) -> T {
        let mut examples_input = String::new();
        self.read_line(& mut examples_input).unwrap();
        examples_input.trim().parse().ok().unwrap()
    }
    
    fn read_u32s(&self) -> Vec<u32> {
        let mut input = String::new(); 
        self.read_line(& mut input).unwrap();
        input.trim().split_whitespace().map(|x| x.parse::<u32>().unwrap()).collect()
    }
}

fn print<T:Display>(items:&[T]) {
    for (i, e) in items.enumerate(){
        print!("{}", e);
        if i != items.len() - 1 {
            print!(" ");
        }
    }
    println!("");
}

fn sort(arr:&mut [u32] )  {
    let max = arr.iter().max().unwrap() + 1;
    let mut cnts = vec![0;max as usize];
    for e in arr.iter() {
        cnts[*e as usize] += 1;        
    }

    let mut idx = 0;
    for (i, e) in cnts.enumerate() {
        let mut r = *e;
        while r > 0 {
            r-=1;
            arr[idx] = i as u32; 
            idx+=1;
        }
    }
}

fn main(){
    let _ = stdin().read_n::<u32>();
    let mut arr = stdin().read_u32s();   
    sort(&mut arr);
    print(&arr);
}
