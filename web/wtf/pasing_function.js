
// js parser can handle function with and without `;` after last `}` 
function a() {}
function b() {};

console.log(a !== undefined);
console.log(b !== undefined);