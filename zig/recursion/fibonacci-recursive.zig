const assert = @import("std").debug.assert;

fn fib(n:u32) u32{
    if (n == 0 or n == 1) {
        return n;
    }
    else {
        return fib(n-2) + fib(n-1);
    }
}

test "fibonacci" {
    assert(fib(2) == 1);
    assert(fib(3) == 2);
    assert(fib(4) == 3);
    assert(fib(5) == 5);
    assert(fib(6) == 8);
}