


// each funtion is attached to object
function A(){
  console.log("A function");
  console.log("window === this ?" + (window === this));
  console.log(this);
}


console.log(A === window.A);
A();

var b = {};
b.a = A;
b.a();


