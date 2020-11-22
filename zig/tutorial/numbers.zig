const assert = @import("std").debug.assert;
const std = @import("std");

test "numbers"{
    assert(@as(u32, std.math.maxInt(u32)) +% 1 == 0);
}