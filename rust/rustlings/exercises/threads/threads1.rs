// threads1.rs
// Make this compile! Execute `rustlings hint threads1` for hints :)
// The idea is the thread spawned on line 21 is completing jobs while the main thread is
// monitoring progress until 10 jobs are completed. If you see 6 lines
// of "waiting..." and the program ends without timing out when running,
// you've got it :)

use std::sync::*;
use std::sync::Mutex;
use std::thread;
use std::time::Duration;
use std::ops::Deref;
struct JobStatus {
    jobs_completed: u32,
}

fn main() {
    let status = Arc::new(Mutex::new(JobStatus { jobs_completed: 0 }));
    for _ in 0..10 {
        let status_shared = status.clone();
        thread::spawn(move || {
            thread::sleep(Duration::from_millis(350));
            let mut cnt = status_shared.lock().unwrap();
            cnt.jobs_completed += 1;
        });
    }
    while status.lock().unwrap().jobs_completed < 10 {
        println!("waiting... ");
        thread::sleep(Duration::from_millis(200));
    }
}