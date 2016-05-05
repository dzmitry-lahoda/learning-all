

fn main(){
  let numbers = [1,2,3]; //creates vector(resizable array) of numbers
  let (port,chan) = Chan::new();
  chan.send(numbers);//push numbers to channel
  do spawn{  //starts lightweight 'green' thread
    let numbers = port.recv();// reads out copy of vector
    println!("{:d}",numbers[0]);
  }
  println!("{:d}",numbers[1]);
}
