fn a(a: i64) {}

pub trait Hello {
    fn to_name(&self) -> String;
}

#[macro_export]
macro_rules! identity {
    ($x:expr) => {
        $x
    };
}

#[macro_export]
macro_rules! myvec {
    ( $($x:expr),* ) => {
        {
            let mut temp_vec = Vec::new();
            $(
                temp_vec.push($x);
            )*

            temp_vec
        }
    }
}

#[cfg(test)]
mod tests {
    #[test]
    fn it_works() {
        assert_eq!(2 + 2, 4);
    }
}
