

(function () {
    "use strict";
    console.log("self executing anonymous function");
}
()
);

var boot  = function () {
    "use strict";
    console.log("named executing function");
};

boot();