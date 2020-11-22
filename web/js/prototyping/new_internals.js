var A = function (){
    this.a = "a";
}
A.prototype.aa = "aa";

var B = function () {
    A.apply(this);
    this.b = "b";
}
B.prototype = A.prototype;

function created(aa,bb) {
    console.log(aa);
    console.log(aa.prototype === undefined);
    console.log(aa.constructor);
    console.log(aa instanceof A);
    console.log(bb);
    console.log(bb.prototype  === undefined);
    console.log(bb.constructor);
    console.log(bb instanceof A);
    console.log(bb instanceof B);
}

// same as `Object.create()`;
function ObjectCreate(proto) { 
    var created = {};
    created.__proto__ = proto;
    return created;
}

// same as `new Ctor()`;
function New (Ctor) {
    var created = ObjectCreate(Ctor.prototype);
    Object.defineProperty(created,"constructor", {
       configurable : false,
       enumerable : false,
       value : Ctor,
    });
    created.constructor();
    return created;
}


var a = new A();
var b = new B();
created(a,b);

var a1 = New(A);
var b1 = New(B);
created(a1,b1);




