const assert = @import("std").debug.assert;
const print = @import("std").debug.print;
var m = [_]u32{0} ** 256;
fn catalan(n:u32) u32{
    m[0] = 1;
    m[1] = 1;
    var i:u32 = 2;
    while (i<=n) : (i+=1) {        
        var j:u32 = 0;
        m[i] = 0;
            while (j<i) : (j+=1) {
                //print("{} {}\n", .{i, m[i]});
                m[i] += m[j] * m[i-1-j];
            } 
        }
    return m[n];
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
