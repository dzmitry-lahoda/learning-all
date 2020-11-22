fn solve(start_finish: &mut [(u32, u32)]) -> Vec<(u32,u32)> {
    start_finish.sort_by(|(_, finish1), (_, finish2)| finish1.partial_cmp(finish2).unwrap());
    let mut result = Vec::new();
    for (i, (start, finish)) in start_finish.iter().enumerate() {
        if result.len() == 0 {
            result.push((*start, *finish));
        } else {
            if let Some((_, f2)) = result.last() {
                if *start > *f2 {
                    result.push((*start, *finish))
                }
            }
        }
    }
    result
}

fn main() {
    let mut test1 = [(1, 3), (1, 2), (1, 1)];
    solve(&mut test1);
    println!("Hello, world!");
}

#[cfg(test)]
mod tests {
    use super::*;
    #[test]
    fn default() {
        let mut test2 = [(1, 2), (0, 6), (3, 4), (5, 9), (5, 7), (8, 9)];
        assert_eq!(solve(&mut test2), [(1,2), (3,4), (5,7), (8,9)]);
    }
}
