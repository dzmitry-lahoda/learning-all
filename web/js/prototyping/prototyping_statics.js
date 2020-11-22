


function A() { 
}
A.prototype.a = [];

var a1 = new A();
var a2 = new A();

console.log("adding value to array statically via prototype - OK");
A.prototype.a.push("A");
console.log(a1);
console.log(a2);

console.log("adding value to array statically via isntance which is BAD");
a1.a.push("a1");

console.log(a1);
console.log(a2);
console.log (a1.a[0] === a2.a[0]);
