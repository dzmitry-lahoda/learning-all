use nth_prime as np;

#[test]
fn test_is_prime_2() {
    assert_eq!(np::is_prime(2), true);
}

#[test]
fn test_is_prime_3() {
    assert_eq!(np::is_prime(3), true);
}

#[test]
fn test_is_prime_5() {
    assert_eq!(np::is_prime(5), true);
}

#[test]
fn test_is_prime_12() {
    assert_eq!(np::is_prime(12), false);
}

#[test]
fn test_first_prime() {
    assert_eq!(np::nth(0), 2);
}

#[test]
fn test_second_prime() {
    assert_eq!(np::nth(1), 3);
}

#[test]
fn test_sixth_prime() {
    assert_eq!(np::nth(5), 13);
}

#[test]
fn test_big_prime() {
    assert_eq!(np::nth(10_000), 104_743);
}
