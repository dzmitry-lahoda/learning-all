

function A(){
    this.a = "a";
}


var a1 = new A();
var a2 = new A();


console.log(a1);
console.log(a2);

a1.a = "a1";

console.log(a1);
console.log(a2);
