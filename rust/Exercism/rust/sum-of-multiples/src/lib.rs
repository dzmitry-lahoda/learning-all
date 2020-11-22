pub fn sum_of_multiples(limit: u32, factors: &[u32]) -> u32 {
    (1..limit).filter(|n| factors.iter().any(|&x| x != 0 && n % x == 0)).fold(0, |acc, e| acc + e)
}
