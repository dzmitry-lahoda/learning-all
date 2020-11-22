const assert = @import("std").debug.assert;
fn solve(n:i32) u32{
    if (n < 0) {
        return 0;
    }
    if (n == 0){
        return 1;
    }
    return solve(n-5) + solve(n-3) + solve(n-1);
}

test "Solved" {
    //             6
    //   1         3             5
    //-4 -2 0    -2 0 2       0  2  4
    //               - - 1       1  3 1 
     //                    0       0  0 0   0
    assert(solve(6) == 8);

    // 1 2 in 5    
    //    4            3
    // 3     2      2         1
    //2 1   1  0 
    //1  0     0   0   
    //0
}