
// enumeration object
var mynamespace = {
        mystate : { State1 : 1, State2 : 2 },
    };

var others = Object.freeze({State1 : {}, State2 : {}});

console.log(mynamespace.mystate);
console.log(others);



