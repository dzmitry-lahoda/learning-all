use std::*;
use std::collections::*;
use std::io::*;
use std::fmt::*;

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
    fn read_u32(&self) -> Vec<u32>;
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
    
    fn read_u32(&self) -> Vec<u32> {
        let mut input = String::new(); 
        self.read_line(& mut input).unwrap();
        input.trim().split_whitespace().map(|x| x.parse::<u32>().unwrap()).collect()
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

fn sort(arr:&mut [u32] )  {
    let mut shifts = 0;
    for j in 1..arr.len() {
        for i in (0..j).rev() {
                if arr[i+1] < arr[i] {
                    let tmp = arr[i+1];
                    arr[i+1] = arr[i];
                    arr[i] = tmp;
                    shifts+=1;
                }
                else {
                    break;
                }
        }


        for i in 1..j {
            assert_eq!(i-1 < i, true);
        }
    }
    println!("{}", shifts);
}

fn main(){
    let _ = stdin().read_u16();
    let mut arr = stdin().read_u32();   
    sort(&mut arr);
    print(&arr);
}
