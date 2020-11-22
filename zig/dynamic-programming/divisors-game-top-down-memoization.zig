const assert = @import("std").debug.assert;
var l = [2][256]u32{
    [_]u32{0} ** 256,
    [_]u32{0} ** 256
};

fn solve(n:u32, isa: bool) bool {
    if (n == 2) return true;    
    if (n == 3) return false;
    var an:u8 = if (isa) 0 else 1;    
    if (l[an][n] != 0) 
        return if (l[an][n] == 1) true else false;

    var ans = !isa;
    var i:u32 = 1;
    // todo: SQRT
    while (i <= (n/2)): (i+=1){
        if (n % i == 0){
            if (isa) {
                ans = ans or solve(n-i, false);
            } else
                ans = ans and solve(n-i, true);        
        }
    }
    
    l[an][n] = if (ans) 1 else 2;

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