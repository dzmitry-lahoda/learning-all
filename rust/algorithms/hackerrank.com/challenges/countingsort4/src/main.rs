mod hackerank_python;

use crate::hackerank_python::*;
use std::collections::*;
use std::fmt::*;
use std::io::*;
use std::iter::*;
use std::str::*;
use std::*;
fn sort(arr: &mut [u32]) {
    let max = arr.iter().max().unwrap() + 1;
    let mut cnts = vec![0; max as usize];
    for e in arr.iter() {
        cnts[*e as usize] += 1;
    }

    let mut idx = 0;
    for (i, e) in cnts.enumerate() {
        let mut r = *e;
        while r > 0 {
            r -= 1;
            arr[idx] = i as u32;
            idx += 1;
        }
    }
}

fn main() {
    let cnt = stdin().read_n::<u32>();
    let mut vec: Vec<Vec<String>> = vec![Vec::new(); 100];
    for c in 0..cnt {
        let (index, val) = stdin().read_tuple::<usize, String>();
        if c < cnt / 2 {
            let mut x = String::new();
            x.push_str("-");
            vec[index].push(x.clone());
        } else {
            vec[index].push(val);
        }
    }

    for a in vec {
        for b in a {
            println!("{} ", b);
        }
    }
}
