const assert = @import("std").debug.assert;


test "test" {
    var arr = [_]u8 {1} ** 42;
    
    for (arr) |*item, i| {
       item.*=2; 
    }

    var sum:u8 = 0;
    for (arr) |item| {
       sum+=item;
    }

    assert(arr[1] == 2);
    assert(sum == 84);

    var d2 = [_][4]u8{
       [4]u8 {1,2,3,4},
       [4]u8 {4,3,2,1}
    };

    
}