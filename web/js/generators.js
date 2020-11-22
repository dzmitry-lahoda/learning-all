

function* a(){
   yield 1;
   yield 2;
}

var b = a();
console.log(b.next());re