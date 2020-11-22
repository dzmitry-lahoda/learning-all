
// if not initialized init with empty object
var mymodule2 = mymodule2 || {};

(function (pub, $) {
    "use strict";
    pub.show2 = function () {
        console.log($("myid"));
    };
}(mymodule2, myquery.select));


var it = mymodule2;

it.show2();