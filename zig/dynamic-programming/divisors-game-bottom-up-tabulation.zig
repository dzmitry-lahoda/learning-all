const assert = @import("std").debug.assert;
var l = [2][256]u32{
    [_]u32{0} ** 256,
    [_]u32{0} ** 256
};

// TODO:  brainfuck - fix it
fn solve(n:u32) bool {
    l[0][2] = 1;
    l[1][3] = 2;
    var j = 3;
    var isa = true;
    while (j<=n) : (j+=1) {
        var an:u8 = if (isa) 0 else 1;    
        var ans = !isa;
        var i:u32 = 1;
        // todo: SQRT
        while (i <= (j/2)): (i+=1){
            if (j % i == 0){
                if (isa) {
                    ans = ans or l[if (!isa) 0 else 1][j-i]
                } else
                    ans = ans or l[if (!isa) 0 else 1][j-i]
            }
        }
        l[an][j] = if (ans) 1 else 2;
    }
    if (l[an][n] != 0) 
        return if (l[an][n] == 1) true else false;
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