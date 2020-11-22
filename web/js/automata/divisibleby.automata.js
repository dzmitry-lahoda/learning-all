var DivisibleBy = function (number) {
    this.state = 0;
    this.start = function () {
        this.state = 0;
        var t = this;
        var trans = function (input) {
            if (input === 1) {
                t.state = (2 * t.state + 1) % number;
            } else {
                t.state = (2 * t.state) % number;
            }
            return trans;
        }
        return trans;
    };
};

var by5 = new DivisibleBy(5);

by5.start()(1)(0)(1);//5
console.log("5 by 5 = " + by5.state);//0

by5.start()(1)(1)(1);//7
console.log("7 by 5 = " + by5.state);//2

by5.start()(1)(0)(1)(0);//10
console.log("10 by 5 = " + by5.state);//0

var by23 = new DivisibleBy(23);

by23.start()(1)(0)(1)(1)(1);//23
console.log("23 by 23 = " + by23.state);//0

by23.start()(1)(1)(1)(1)(0);//30
console.log("30 by 23 = " + by23.state);//7

by23.start()(1)(0)(1)(1)(1)(0);//46
console.log("46 by 23 = " + by23.state);//0

by23.start()(1)(1)(0)(0)(1)(0);//50
console.log("50 by 23 = " + by23.state);//4

