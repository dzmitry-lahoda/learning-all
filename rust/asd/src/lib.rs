//! # asd
//! Contains helpers methods which ease coding competitive and algorithmic programming faster like it is possible in python

/// reads line from stdin, trim empty end and start
/// # Examples
/// ```
/// assert_eq!(asd::input(),"");
/// ```
/// #
/// # Panics
/// When it fails to read stdin
/// 
pub fn input() -> String{
    let mut input = String::new();
    std::io::stdin().read_line(&mut input).unwrap();
    input.trim().to_owned()
}

#[cfg(test)]
mod tests {
    #[test]
    fn it_works() {
        assert_eq!(2 + 2, 4);
    }
}
