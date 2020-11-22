const assert = @import("std").debug.assert;
const print = @import("std").debug.print;
fn catalan(n:u32) u32{
    if (n==0) return 1;
    var i:u32 = 0;
    var answer:u32 = 0;
    while (i<n) : (i+=1) {
        answer += catalan(i) * catalan(n-1-i);
    }

    return answer;
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
