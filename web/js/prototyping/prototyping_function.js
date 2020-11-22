

// creates `var A` which is function
function A() {
    this.a = "a";
}
console.log(A);

var a1 = new A();
console.log(a1);

// prefer this way cause states that we create variable which is function
var A = function () {
        this.a = "a";
    };
console.log(A);

var a2= new A();
console.log(a2);

var A = function () {
        this.a = "a";
    };


var a2= new A();
console.log(a2);

// seems like A is rewriten by new A
var A = function A() {
        this.a = "a";
    };
console.log(window.A);

var a3= new A();
console.log(a3);

