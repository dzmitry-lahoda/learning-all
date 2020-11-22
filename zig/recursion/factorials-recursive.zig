const assert = @import("std").debug.assert;

fn fac(n:u32) u32{
    if (n == 0) {
        return 1;
    } else {
        return n * fac(n-1);
    }
}

test "factorials" {
    assert(fac(0) == 1);
    assert(fac(1) == 1);
    assert(fac(2) == 2);
    assert(fac(3) == 6);
}