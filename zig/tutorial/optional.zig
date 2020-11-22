const std = @import("std");
const assert = std.debug.assert;
test "main" {
    const stdout = std.io.getStdOut().writer();
    const opt: ?i1 = null;
    assert(opt == null);
    assert(@typeName(@TypeOf(opt)) == "?i1");

    const ors: anyerror!u8 = error.ArgNotFound; 
    
    assert(@typeName(@TypeOf(ors)) == "anyerror!u8");

    const unwrapped = null orelse 42;
    assert(unwrapped == 42);

    assert(ors catch 42 == 42);

    const A = error{One};
    const B = error{Two};
    var t = (A || B) == error{One, Two};

    // TODO: report to zig
    assert(t);

    // TODO: report to zig
    const o1: ?i7 = 1;
    const o2: ?i7 = null;
    //assert(o1 orelse 2 == 1);
}