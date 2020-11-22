
// manuall currying
var f1 = function (a, b, c) {
        "use strict";
        // missed arguments are passed as function
        if (c === undefined) {
            return function (cc) {
                return a + b + cc;
            };
        }
        return a + b + c;
    };



console.log(f1(1, 2, 3));
console.log(f1(1, 2)(3));