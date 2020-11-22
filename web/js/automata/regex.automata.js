var r = /{\s*return/g;

var a = "{ return ";
console.log(a.replace(r,"") === " ");

var b = "{  return ";
console.log(b.replace(r,"") === " ");

var g = /({)\s*return/g;
var c = "{  return ";
console.log(c.replace(g,"$1") === "{ ");