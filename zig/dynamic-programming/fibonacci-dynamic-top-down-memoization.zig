const assert = @import("std").debug.assert;
var lookup = [_]u32{0} ** 256;
fn fib(n:u32) u32{
    if (n == 0 or n == 1) {
        return n;
    }
    else {
        if (lookup[n] == 0)
            lookup[n] = fib(n-1) + fib(n-2); 
        return lookup[n];
    }
}

test "fibonacci" {
    assert(fib(2) == 1);
    assert(fib(3) == 2);
    assert(fib(4) == 3);
    assert(fib(5) == 5);
    assert(fib(6) == 8);
}