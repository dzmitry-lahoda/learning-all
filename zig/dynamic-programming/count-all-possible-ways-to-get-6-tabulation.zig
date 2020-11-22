const assert = @import("std").debug.assert;
var m = [_]u32{0} ** 256;
fn solve(n:i32) u32{    
    m[0] = 1;
    var i:u32 = 1;
    while (i <= n): (i+=1) {
        m[@intCast(usize, i)] += m[@intCast(usize, i-1)];
        if (i >= 3){
            m[@intCast(usize, i)] += m[@intCast(usize, i-3)];
        }
        if (i >= 5){
            m[@intCast(usize, i)] += m[@intCast(usize, i-5)];
        }
    }

    return m[@intCast(usize, n)];
}

test "Solved" {    
    assert(solve(6) == 8 );
}