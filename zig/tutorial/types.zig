const assert = @import("std").debug.assert;


fn stuctural(d:anytype) void {
    assert(d.@"0" == 12.3);
    assert(d.@"1"[0] == '1');
}

test "test" {
    stuctural(.{12.3, "12"});
}