
function P() {
    "use strict";
    this.a = "a";
    this.b = "b";
    return this;
}

var p1 = new P();
var p2 = P.apply({});
var p3 = P.prototype;
var p4 = P.constructor()();
var p5 = {a : "a", b : "b"};
var p6 = Object.create(p1);

console.log(p1);
console.log(p2);
console.log(p3);
console.log(p4);
console.log(p5);
console.log(p6);

