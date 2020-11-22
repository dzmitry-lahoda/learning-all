

function a(one) {
   this.b = "c";   
   return one;
}


var d = {};

var e = a.bind(d);

console.log(e(1) === 1);
console.log(d.b === "c");

var f = {};
f.g = e;

f.g();

console.log(f.b === undefined);