
// TODO: recirsove solution with inlined function(fn in fn)which is called to return result
// TODO: functions whic uses let candidates = 2.. and divides by each assuming that division is cheap and we have eat all non primes before, infinite fold?
// TODO: for f in (2..).take_while
pub fn factors(n: u64) -> Vec<u64> {
    // we need to know how much prime factors will we have
    // or increase list
    let mut r = Vec::new();
    let mut d = n;
    while d % 2 == 0 && d > 0{
        d = d / 2;
        r.push(2);
    }

    let mut odd = 3;
    let max = (d as f64).sqrt() as u64 + 1;
    loop {
        if odd > max {
            break;
        }

        while  d % odd == 0 {
            d = d / odd;
            r.push(odd);
        }

        odd += 2;
    }
    
    if d > 2 { r.push(d);}
    r
}
