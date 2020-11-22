

var a = {a:"a"};

var b = {};
b.__proto__  = a;
console.log("b.a=" + b.a);

var c = {};
c.__proto__ = b;
console.log("c.a=" +  c.a );

   