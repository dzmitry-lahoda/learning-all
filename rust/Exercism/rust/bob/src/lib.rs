pub fn reply(message: &str) -> &str {
    let message = message.trim();
    let has_letters = message.chars().any(|x| x.is_alphabetic());
    let uppercase = message == message.to_uppercase() && has_letters;
    let question = message.ends_with("?");
    match message {
        "" => "Fine. Be that way!",
        _ if uppercase && question => "Calm down, I know what I'm doing!",
        _ if uppercase => "Whoa, chill out!",
        _ if question => "Sure.",
        _ => "Whatever."
    }
}
