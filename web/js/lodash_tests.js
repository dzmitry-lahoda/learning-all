

rom.import(["lib/lodash.js"]);



var a = [4,5,6];
var b = _.findIndex(a, function (element,index) {
   return element === 5;
});

console.log(b === 1);

var d = [1,2];
var c = _.map(d,_.clone);
console.log(c[1] === 2);
c[1] = 3;
console.log(c[1] === 3);
console.log(d[1] === 2);