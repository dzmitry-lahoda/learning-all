use chrono::{DateTime, Utc, Duration};

// Returns a Utc DateTime one billion seconds after start.
pub fn after(start: DateTime<Utc>) -> DateTime<Utc> {
   // Naive date time
    // let d = time::Duration::seconds(1_000_000_000);
   let b = ::chrono::Duration::seconds(1_000_000_000);
  //let c = std::time::Duration::from_secs(1_000_000_000);
    start + b
}
