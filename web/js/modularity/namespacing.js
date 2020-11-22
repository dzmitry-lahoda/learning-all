// object literal with only public properties
// like public static properties 
var a = {
        b : 1,
        c : function () {
            "use strict";
            return "b is " + this.b;
        },
        // how d can references parent if instead `a` long name and used many times (e.g. has several enumrations)
        d : { e : "e", f : function () { "use strict"; return "f() access a.b = " + a.b; } }
    };

console.log(a.c());
console.log(a.d.f());




