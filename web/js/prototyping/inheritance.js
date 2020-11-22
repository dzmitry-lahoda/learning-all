

var P = function () {
    this.a = "a";
}


var S = function () {
    P.call(this);
    this.b = 1;
}

S.prototype = Object.create(P.prototype);
var s = new S();
console.log(s);
console.log(S.prototype.constructor == P);





