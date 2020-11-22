use std::collections::*;


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


// 185 -> array[n]=fib() ->
// fibonacci
fn fib(n: u8) -> u128 {
    // u7 - 128, or custom Fib185
    // if fib_unsafe u128 unsafe and fib Result<u128, Overflow>, fib_float:)
    // multi value define
    let mut window = (1, 1);
    for _ in window.len()..n + 1 {
        window = (window.0 + window.1, window.0)
    }

    window.0
}

fn fib_rec(n: u8) -> u128 {
    if n == 0 || n == 1 {
        1
    } else {
        fib_rec(n - 1) + fib_rec(n - 2)
    }
}

fn main() {}

#[cfg(test)]
mod tests {
    use super::*;
    #[test]
    fn fib4() {
        assert_eq!(fib(0), 1);
        assert_eq!(fib(1), 1);
        assert_eq!(fib(3), 3);
        assert_eq!(fib(4), 5);
        assert_eq!(fib(5), 8);
        assert_eq!(fib(6), 13);
        for i in 0..22 {
            assert_eq!(fib(i), fib_rec(i));
        }
        assert_eq!(fib(185), 332825110087067562321196029789634457848);
        for n in 2..22 {
            assert_eq!(fib(n), fib(n - 1) + fib(n - 2));
        }
    }
}
