#[macro_export]
extern crate mymacro;

#[macro_use]
extern crate derive_new;

#[macro_use]
extern crate frunk; // allows us to use the handy hlist! macro
use frunk::prelude::*; // for Result::into_validated
