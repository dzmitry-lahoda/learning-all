const assert = @import("std").debug.assert;
const print = @import("std").debug.print;
const std = @import("std");

pub fn binomial(n:u16, k:u16) u32 {
    var m = [_]u32{0} ** 256;
    m[0]=1;
    var i:u32 = 1;
    while (i<=n) : (i+=1) {
        var j = std.math.min(i,k);
        while (j > 0 ) : (j-=1) {
            m[j] = m[j] + m[j-1];
        }
    }
    
    // 1
    // 1 1
    // 1 2 1
    // 1 3 3 1
    // 1 4 6 4 1
    return m[k];
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