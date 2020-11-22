
const std = @import("std");
const assert = @import("std").debug.assert;
threadlocal var x:i32 = 42;

fn testTls(ctx:void) void {
    assert(x == 42);
    x+=1;
    assert(x==43);
}

test "thread" {
    const t1 = try std.Thread.spawn({}, testTls);
    const t2 = try std.Thread.spawn({}, testTls);
    testTls({});
    t1.wait();
    t2.wait();
}
