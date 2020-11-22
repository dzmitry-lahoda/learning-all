const assert = @import("std").debug.assert;
const fac = @import("factorial-bottom-up.zig").fac;
const print = @import("std").debug.print;


pub fn binomial(n:u16, k:u16) u32 {
    return fac(n) / (fac(k) * fac(@intCast(u16,n-k)));
}

test "binomial" {
    assert(binomial(1,1) == 1);
    assert(binomial(2,1) == 2);
    assert(binomial(3,1) == 3);
    assert(binomial(2,2) == 1);
    assert(binomial(3,2) == 3);
    assert(binomial(4,2) == 6);
    assert(binomial(5,2) == 10);
}