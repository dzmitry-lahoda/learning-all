

var A = {
        a : "a",
        // may define property to hide it
        constructor : function () {
            this.b = "b";
            return this;
        }
    };

 
var a1 = Object.create(A).constructor();
a1.constructor = A.constructor;

// same as above but uses hiddend field should not be called in production code
var a2 = new A.constructor();
a2.__proto__ = A;
a2.constructor = A.constructor;


console.log("shows Object type and visible consuctor function");
console.log(a1);
console.log(a2);


