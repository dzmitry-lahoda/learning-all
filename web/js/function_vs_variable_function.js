//`var P = function (){ ... }` similar to `function P() { ... }` but
//  P.toString() differs
// settings  P = undfined leads to different consequnces on rewriting 
function P() { return 1 };
console.log(P !== undefined);
console.log(P.toString() !== undefined);
console.log(P === window.P);
console.log(P.toString() === "function P() { return 1 }");

P = undefined;
console.log(P === undefined);

function P() { return 1 };
console.log(P === undefined);
console.log(P === window.P);

P = undefined;

var P = function  () { return 1 };
console.log(P !== undefined);
console.log(P === window.P);
console.log(P.toString() === "function () { return 1 }");