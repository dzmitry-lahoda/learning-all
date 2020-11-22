const assert = @import("std").debug.assert;


const FooBar  = struct {
        var y:i32 = 13;
    };

fn stuct_var() i32{
    const S  = struct {
        var x:i32 = 42;
    };
    S.x += 1;
    return S.x;
}

fn assign() i2 {
    var x:i2 = undefined;
    x = 0;
    var y = x + 1;
    return y;
}

fn comptime_var() void {
    comptime var y:i32 = 1;
    y+=1;
    assert(y == 2);
    if (y != 2){
        @compileError("Hello");
    }
    else {
        //print("asd", {});
    }
}

test "test" {
    assert(assign() == 1);
    assert(stuct_var() == 43);
    comptime_var();
}