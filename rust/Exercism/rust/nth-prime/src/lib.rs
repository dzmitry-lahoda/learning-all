pub fn is_prime(n: u32) -> bool {
    if n <= 1 {
        false
    } else if n <= 3 {
        true
    } else if n % 2 == 0 || n % 3 == 0 {
        false
    } else {
        let mut isp = true;
        let mut i = 5;
        loop {
            if i * i > n {
                break;
            }

            if n % i == 0 || n % (i + 2) == 0 {
                isp = false;
                break;
            }

            i += 6
        }

        isp
    }
}

pub fn nth(n: u32) -> u32 {
    let mut red = 0;
    for i in 2..std::u32::MAX {
        if is_prime(i) {
            red += 1;
        }

        if red == n + 1 {
            red = i;
            break;
        }
    }

    red
}
