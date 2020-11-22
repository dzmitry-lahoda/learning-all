const assert = @import("std").debug.assert;
const print = @import("std").debug.print;
fn fib(n:u32) u32{
    var v0:u32 = 0;
    var v1:u32 = 1;
    var vn:u32 = v1;    
    var i:u32 = 0;
    if (n >= 2) {
       while (i <= n - 2): (i+=1){
            vn = v1 + v0;
            v0 = v1;
            v1 = vn;            
        }
    }

    return vn;
}

test "fibonacci" {
    assert(fib(1) == 1);
    assert(fib(2) == 1);
    assert(fib(3) == 2);
    assert(fib(4) == 3);
    assert(fib(5) == 5);
    assert(fib(6) == 8);
}