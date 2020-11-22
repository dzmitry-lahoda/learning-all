use std::collections::*;
use std::fmt::*;
use std::io::*;
use std::iter::*;
use std::str::*;
use std::*;
use std::cmp::*;


pub trait TupleMax<T:Ord + Copy> {
    fn max(&self) -> T;
}


impl<T:Ord + Copy> TupleMax<T> for (T, T) {
    fn max(&self) -> T {
        self.0.max(self.1)
    }
}

pub trait TupleLen {
    fn len(&self) -> u8;
}



impl<T1> TupleLen for (T1,) {
    fn len(&self) -> u8 {
        1
    }
}
impl<T1, T2> TupleLen for (T1, T2) {
    fn len(&self) -> u8 {
        2
    }
}
impl<T1, T2, T3> TupleLen for (T1, T2, T3) {
    fn len(&self) -> u8 {
        3
    }
}
impl<T1, T2, T3, T4> TupleLen for (T1, T2, T3, T4) {
    fn len(&self) -> u8 {
        4
    }
}
impl<T1, T2, T3, T4, T5> TupleLen for (T1, T2, T3, T4, T5) {
    fn len(&self) -> u8 {
        4
    }
}

impl<T1, T2, T3, T4, T5, T6> TupleLen for (T1, T2, T3, T4, T5, T6) {
    fn len(&self) -> u8 {
        4
    }
}

pub trait Python<T> {
    fn enumerate(&self) -> Enumerate<std::slice::Iter<T>>;
}

impl<T> Python<T> for Vec<T> {
    fn enumerate(&self) -> Enumerate<std::slice::Iter<T>> {
        self.iter().enumerate()
    }
}

impl<T> Python<T> for [T] {
    fn enumerate(&self) -> Enumerate<std::slice::Iter<T>> {
        self.iter().enumerate()
    }
}

pub trait ReadNumberStdin {
    fn read_u8(&self) -> u8;
    fn read_u16(&self) -> u16;
    fn read_n<T: FromStr>(&self) -> T;
    fn read_tuple<T1: FromStr, T2: FromStr>(&self) -> (T1, T2);
    fn read_u32s(&self) -> Vec<u32>;
}

impl ReadNumberStdin for Stdin {
    fn read_u8(&self) -> u8 {
        let mut examples_input = String::new();
        self.read_line(&mut examples_input).unwrap();
        examples_input.trim().parse::<u8>().unwrap()
    }

    // TODO: make it generic like parse::<u32> with restriction of being parsable
    fn read_u16(&self) -> u16 {
        let mut examples_input = String::new();
        self.read_line(&mut examples_input).unwrap();
        examples_input.trim().parse().unwrap()
    }

    fn read_n<T: FromStr>(&self) -> T {
        let mut line = String::new();
        self.read_line(&mut line).unwrap();
        line.trim().parse().ok().unwrap()
    }

    fn read_tuple<T1: FromStr, T2: FromStr>(&self) -> (T1, T2) {
        let mut line = String::new();
        self.read_line(&mut line).unwrap();
        let vals: Vec<&str> = line.trim().split_whitespace().collect();
        (
            vals[0].parse::<T1>().ok().unwrap(),
            vals[1].parse::<T2>().ok().unwrap(),
        )
    }

    fn read_u32s(&self) -> Vec<u32> {
        let mut input = String::new();
        self.read_line(&mut input).unwrap();
        input
            .trim()
            .split_whitespace()
            .map(|x| x.parse::<u32>().unwrap())
            .collect()
    }
}

pub fn print<T: Display>(items: &[T]) {
    for (i, e) in items.enumerate() {
        print!("{}", e);
        if i != items.len() - 1 {
            print!(" ");
        }
    }
    println!("");
}
