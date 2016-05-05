
extern mod extra;
use extra::arc::Arc;//shared immutable data

fn main(){

  let numbers = [1,2,3]; //creates vector(resizable array) of numbers
  let number_arc = Arc::new(numbers); // creates reference counted immutable wrapper
  
  for num in range(0,3) {
    let (port,chan) = Chan::new();
    chan.send(number_arc.clone());// vector is not copied, cause can be shared because of immutability
    do spawn{
      let local_arc = port.recv();
      let task_numbers = local_arc.get();
      println!("{:d}",task_numbers[num]);
    }
  }
}

