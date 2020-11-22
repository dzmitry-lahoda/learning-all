

// TODO: do it HTK - TraitBased
pub fn verse(n: u32) -> String {
    // via pattern
    if n == 0 {
        "No more bottles of beer on the wall, no more bottles of beer.\nGo to the store and buy some more, 99 bottles of beer on the wall.\n".to_owned()
    } else if n == 1 {
        "1 bottle of beer on the wall, 1 bottle of beer.\nTake it down and pass it around, no more bottles of beer on the wall.\n".to_owned()
    } else if n == 2 {
        "2 bottles of beer on the wall, 2 bottles of beer.\nTake one down and pass it around, 1 bottle of beer on the wall.\n".to_owned()
    } else {
        format!("{} bottles of beer on the wall, {} bottles of beer.\nTake one down and pass it around, {} bottles of beer on the wall.\n", n, n, n-1)
    }
}

pub fn nl(n: u32) -> String {
    if n > 0 {
        "\n".to_owned()
    } else {
        "".to_owned()
    }
}

pub fn sing(start: u32, end: u32) -> String {
    // TOOD: simplify this
    (end..start + 1)
        .rev()
        .fold("".to_owned(), |acc, x| acc + &verse(x) + &nl(x-end))
        // join \n
}
