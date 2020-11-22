// For want of a nail the shoe was lost.
// For want of a shoe the horse was lost.
// For want of a horse the rider was lost.
// For want of a rider the message was lost.
// For want of a message the battle was lost.
// For want of a battle the kingdom was lost.
// nail.

use std::iter;

pub fn pairwise<'a, I>(xs: I) -> Box<dyn Iterator<Item = (Option<I::Item>, I::Item)> + 'a>
where
    I: 'a + IntoIterator + Clone,
{
    let left = iter::once(None).chain(xs.clone().into_iter().map(Some));
    let right = xs.into_iter();
    Box::new(left.zip(right))
}

pub fn pairwise2<'a, I>(xs: I) -> Box<dyn Iterator<Item = (I::Item, Option<I::Item>)> + 'a>
where
    I: 'a + IntoIterator + Clone,
{
    let left = xs.clone().into_iter();
    let right = xs.into_iter().skip(1).map(Some).chain(iter::once(None));
    Box::new(left.zip(right))
}

fn asd(fst:&str, i:(&&str, Option<&&str>)) -> String {
    match i {
        (l,Some(r)) => 
            format!("For want of a {} the {} was lost.\n", l, r).to_owned(),
        (_, None) =>
            format!("And all for the want of a {}.", fst).to_owned()
    }
}

pub fn build_proverb(list: &[&str]) -> String {
    if list.len() == 0 {
        String::new()
    }
    else {

    let fst = list[0];
    //let ifst = iter::once(&fst);
    //let a = list.into_iter().chain(ifst);
    // TODO: use next
    //list.windows() collect join
    pairwise2(list).into_iter().fold("".to_owned(), |acc, x| acc + &asd(fst, x))
    }
}
