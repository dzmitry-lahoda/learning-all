// creares mutable vector and passes it into spawned thread after wrapping into locks
extern mod extra;
use extra::arc::RWArc;//shared mutable data

fn main(){

  let numbers = [1,2,3]; //creates vector(resizable array) of numbers
  let number_arc = RWArc::new(numbers); // creates mutext lockable wrapper
  
  for num in range(0,3) {
    let (port,chan) = Chan::new();
    chan.send(number_arc.clone());// vector is not copied
    do spawn{
      let local_arc = port.recv();
      
      // locks for write
      local_arc.write( |nums| {  // closure passed with access to parameter 
        nums[num]+=1
      });

      // locks for read
      local_arc.read( |nums| { // closure passed with access to parameter 
        // nums[num]+=1; //compi erroer error: cannot assign to immutable vec content     
        println!("{:d}",nums[num]);
      });      
      
    }
  }
}


