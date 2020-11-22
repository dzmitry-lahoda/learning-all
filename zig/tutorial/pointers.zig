const std = @import("std");
const assert = std.debug.assert;
test "main" {
    // if x is const, than fails to compile
    var x:u32 = 42;
    var ptr = &x;
    ptr.* = 13;
    assert(x == 13);
}