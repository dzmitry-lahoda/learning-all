var mymodule = (function () {
    "use strict";
    var pub = {}, c = "private c";
    pub.a = "A";
    pub.b = function () { console.log("pub.b " + c); };
    return pub;
}());


console.log(mymodule.a);
mymodule.b();