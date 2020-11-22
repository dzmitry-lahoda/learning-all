
var d = function () {
        return d;  
    };

d()()();


var sum = 0;
var adder = function (number) {
       sum = sum + number;
       return adder;
    };
adder(1)(2)(3);
console.log(sum);


var incrementer = function () {
       var state = 1;//start with one cause first call will not call ++, but just return function
       return function() {
          return state++;
       };
    }();
var incr1 = incrementer();
var incr2 = incrementer();
console.log(incr1);
console.log(incr2);


var  incrementer2 = function () {
       var state = 0;
       var incrementer2_rec = function () {
           state++;
           console.log(state);
           return incrementer2_rec;
       };
       return incrementer2_rec;
    }();
incrementer2()()()()();



var  adder2 = function (number) {
       var state = number;
       return function(x) {
          return state = state + x;
       };
    }(0);
adder2(1);
adder2(2);
var sum3 = adder2(3);
console.log(sum3);

var  adder3 = function (init) {
       var state = init;
       var adder3_rec = function (x) {
           state = state + x;
           console.log(state);
           return adder3_rec;
       };
       return adder3_rec;
    }(0);
adder3(1)(2)(3);

var chain = {
        b : 0,
        c : function (x) { 
            this.b = this.b + x;
            return this; 
        }
    };

chain.c(1).c(2).c(3);
console.log(chain);


