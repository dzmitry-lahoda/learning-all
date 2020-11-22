const assert = @import("std").debug.assert;

pub fn fac(n:u32) u32{
    var i:u32 = 1;
    var r:u32 = 1;
    while (i <n) : (i+=1) {
       r = (i + 1)* r;   
    } 
    return r;
}

test "factorials" {
    assert(fac(0) == 1);
    assert(fac(1) == 1);
    assert(fac(2) == 2);
    assert(fac(3) == 6);
}

test "factorial 4" {
    assert(fac(4) == 24);
}