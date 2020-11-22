


var a = function () {
        this.b = "b of a";
    };

var c = {
        b : "b of c",
        a : function () {
            console.log("a of c with b = " + this.b);
        }
    };

var d = {};
d.b = "b of d";
d.a = c.a;// a uses this which is of d not c
d.a();
d.a = function () { c.a(); }; // ensure this is used of c
d.a();



