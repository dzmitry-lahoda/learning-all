

var a = {a:"A value"};
var b = Object.create(a);
var c = Object.create(b);

function call(obj,prop){
   var value = Object.getOwnPropertyDescriptor(obj,prop);
    if (value)
       return value.value;
    else {
        var proto = Object.getPrototypeOf(obj);
        if (proto)
            return  call(proto,prop);
        else
            return undefined;
    }
}

function thisCall(prop) {
    return call(this,prop);
}

function attachCall(obj){
    Object.defineProperty(obj,"_", {
       configurable : false,
       enumerable : false,
       value : thisCall,
    });
}

attachCall(a);
attachCall(b);
attachCall(c);

console.log(a.a == a["a"]);
console.log(a._("a") == a.a);
console.log(b._("a") == b.a);
console.log(c._("a") == c.a);
console.log(c._("c") == c.c);//undefined
                                       