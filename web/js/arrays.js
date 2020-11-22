var a = [1, 2, 3];
a.shift();
console.log(a.length === 2);
console.log(a[0] === 2);
console.log(a[1] === 3);


var b = [1,2];
var c = b.concat(3);
console.log(b.length === 2);
console.log(c.length === 3);
console.log(c[0] === 1);
console.log(c[2] === 3);