// macros4.rs
// Make me compile! Execute `rustlings hint macros4` for hints :)

macro_rules! my_macro {
    () => {
        println!("Check out my macro!");
    };
    ($val:expr, &str) => {
        println!("You know the truth: {}", $val);    
    };
    ($val:expr) => {        
        println!("Look at this other macro: {}", $val + 1);
    };
    // macro for loop/reapeat
    ($($val:expr),*) => {        
        $(
            println!("Loooop: {}", $val);
        )*
    };
}

fn main() {
    my_macro!();
    my_macro!(7777);
    my_macro!("42", &str);
    
    my_macro!(1,2,3);
}