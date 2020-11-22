const assert = @import("std").debug.assert;
fn solve(n:u32, isa: bool) bool {
    if (n == 2) {
        return true;
    }
    if (n == 3) {
        return false;
    }
    var ans = !isa;
    
    var i:u32 = 1;
    // todo: SQRT
    while (i <= (n/2)): (i+=1){
        if (n % i == 0){
            if (isa) {
                ans = ans or solve(n-i, false);
            } else {
                ans = ans and solve(n-i, true);
            }
        }
    }
    return ans;
} 

test "divisor" {
    assert(solve(2, true) == true);
    assert(solve(3, true) == false);
    assert(solve(4, true) == true);
    assert(solve(5, true) == false);
    assert(solve(6, true) == true);
    assert(solve(7, true) == false);
    assert(solve(8, true) == true);
    assert(solve(9, true) == false);
}