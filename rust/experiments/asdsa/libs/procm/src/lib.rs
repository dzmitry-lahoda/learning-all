extern crate proc_macro;

use inflector::cases::*;
use inflector::Inflector;
use inflector::*;
use proc_macro::TokenStream;
use quote::{format_ident, quote};
use syn::{self, GenericParam, TypeParam};
#[proc_macro_derive(Hello)]
pub fn hello_macro_derive(input: TokenStream) -> TokenStream {
    let ast: syn::DeriveInput = syn::parse(input).unwrap();
    let name = &ast.ident;
    let r = quote! {
        impl Hello for #name {
            fn to_name(&self) -> String {
                println!("{:#?}", stringify!(#ast));
                stringify!(#name).to_string()
            }
        }
    };
    r.into()
}

#[proc_macro_attribute]
pub fn identity_2(attr: TokenStream, item: TokenStream) -> TokenStream {
    item
}

#[proc_macro]
pub fn identity_3(input: TokenStream) -> TokenStream {
    input
}

// https://github.com/p0lunin/teloc
#[proc_macro_attribute]
pub fn inject(
    _: proc_macro::TokenStream,
    item: proc_macro::TokenStream,
) -> proc_macro::TokenStream {
    println!("FUCk");
    println!("{:#?}", item.clone());

    let input = syn::parse_macro_input!(item as syn::DeriveInput);
    let ident = input.ident.clone();
    let retained = input.generics.params.clone();
    let retained = retained.iter();
    if input.generics.params.is_empty() {
        let output = quote::quote! {
            #input
        };
        return output.into();
    }
    let fields: Vec<_> = input
        .generics
        .params
        .iter()
        .map(|x| match x {
            GenericParam::Type(x) => Some(x),
            _ => None,
        })
        .filter_map(|x| x)
        .collect();
    let fields2 = fields
        .iter()
        .map(|x| format_ident!("{}", format!("{}", x.ident).to_snake_case()));
    let cloned = fields.clone();
    //input.attrs
    //println!("{:#?}", input.generics);
    let output = quote::quote! {
        struct #ident<#(#retained,)*> {

            #(#fields2:#fields),*
        }
    };
    println!("ASDASDDSDADAS");
    println!("{:#?}", output.clone());
    output.into()
}
