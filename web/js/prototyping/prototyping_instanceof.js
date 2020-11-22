

function P() {
    this.a = "a";
}


function S() {
    P.call(this);
    this.d = 1;
}



function s_instanceof(obj) {
   console.log("obj instanceof P?" + (obj instanceof P));
   console.log("obj instanceof S?" + (obj instanceof S));
   console.log(obj);
}

var s1 = new S();
s_instanceof(s1);

S.prototype = Object.create(P.prototype);
var s2 = new S();
s_instanceof(s2);




