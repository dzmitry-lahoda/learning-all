const assert = @import("std").debug.assert;
const print = @import("std").debug.print;
const binomial = @import("binomial-triangle.zig").binomial;

var m = [_]u32{0} ** 256;
fn catalan(n:u16) u32{
    return binomial(2*n, n) / (n + 1);
}

test "catalan" {
    assert(catalan(0) == 1);
    assert(catalan(1) == 1);
    assert(catalan(2) == 2);
    assert(catalan(3) == 5);
    assert(catalan(4) == 14);
    assert(catalan(5) == 42);
    assert(catalan(6) == 132);
    assert(catalan(7) == 429);
}
