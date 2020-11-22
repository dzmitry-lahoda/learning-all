

var a = {b:"b", c: function() {} };
var props = Object.getOwnPropertyNames(a);
for (var p of props){
    console.log(p);
}