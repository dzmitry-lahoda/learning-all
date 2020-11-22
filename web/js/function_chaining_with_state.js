

var chain = {
        b : 0,
        c : function (x) { 
            if (x === undefined) {
                return this.b;
            }
            var co = this;// seems needed to capture this
            var cc = function (n) { 
              if (n === undefined) {
                  return co.b;
              }
              co.b = co.b + n;
              return cc;
            }; 
            return cc(x);
        }
    };
chain.c(1)(2)(3);
chain.c(4);
console.log(chain.b);//10
console.log(chain.c());//10

