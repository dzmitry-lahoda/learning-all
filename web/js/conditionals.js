

var a = "a";
//var b = undefined;
// if b undefined then use a
var c = b || a;

console.log(c);

var f = false;
var n = null;
var u = undefined;
var e = ""; //empty string
var z = 0;
var nan = NaN; // not a number

console.log(f === n);
console.log(n === u);
console.log(u === e);
console.log(z === nan);

var areNot =  f || n || u || e || z || nan;

if (areNot) {
    console.log("impossible");
} else {
    console.log(areNot);
}
