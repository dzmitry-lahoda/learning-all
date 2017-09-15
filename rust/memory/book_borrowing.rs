pub struct Book {
  title: String,
  chapters : Vec<String>
}

// & means landing
fn print_book (book:&Book){
    println!("The program \"{}\" calculates the value {} ",book.title,book.chapters.len());
}

fn print_book

fn main() {
    //TODO: fix strings to be simple to declare
    let book = Book {title : "Foo".to_string(), chapters : vec!["1".to_string(),"2".to_string()]};
    print_book(&book);//land book and send book back
    // lack of & in previous line will fail to compile next line
    println!("The program \"{}\" calculates the value {} ",book.title,book.chapters.len());
}