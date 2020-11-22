const assert = @import("std").debug.assert;
var m = [_]u32{0} ** 256;

fn min2(comptime T:type, a:T,  b:T, c:T, d:T) T {
    return a;
}

fn min3(comptime T:type, a:T,  b:T, c:T) T {
    if (a <= b and a <= c) {
        return a;
    }
    else if (b <= a and b <= c){
        return b;
    }
    else {
        return c;
    }
}

fn solve(n:u32) u32{
    m[1] = 1;
    var v2:u32 = 1;
    var v3:u32 = 1; 
    var v5:u32 = 1;
    var i:u32  = 2;
    while (i<=n) : (i+=1) {
        const nv2 = m[v2] * 2;
        const nv3 = m[v3] * 3;
        const  nv5 = m[v5] * 5;
        const nv = min3(u32, nv2,nv3,nv5);
        if (nv2 == nv) v2+=1;
        if (nv3 == nv) v3+=1;
        if (nv5 == nv) v5+=1;
        m[i] = nv;
    }
    return m[n];
}

test "Solved" {
    assert(solve(1) == 1);
    assert(solve(2) == 2);
    assert(solve(3) == 3);
    assert(solve(4) == 4);
    assert(solve(5) == 5);
    assert(solve(6) == 6);
    assert(solve(7) == 8);
    assert(solve(8) == 9);
    assert(solve(9) == 10);
    assert(solve(10) == 12);
    assert(solve(150) == 5832);
}