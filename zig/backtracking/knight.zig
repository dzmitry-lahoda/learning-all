
const assert = @import("std").debug.assert;
const fs = @import("std").fs;
const std = @import("std");
var example1 = [8][8]i8 {
    [_]i8{-1} ** 8,
    [_]i8{-1} ** 8,
    [_]i8{-1} ** 8,
    [_]i8{-1} ** 8,
    [_]i8{-1} ** 8,
    [_]i8{-1} ** 8,
    [_]i8{-1} ** 8,
    [_]i8{-1} ** 8
};
const coor = struct {
    x:i8,
    y:i8
};
pub fn solve_rec(comptime a:u8, pos:i8, c:coor, board: *[a][a]i8) bool {
    board[@intCast(usize, c.y)][@intCast(usize, c.x)] = pos;
    if (pos >= 63) {
        return true;
    }
    if (c.x + 2 < 8 and c.y + 1 < 8 and board[@intCast(usize, c.y + 1)][@intCast(usize, c.x + 2)] == -1){
       var r =  solve_rec(a,pos+1, coor{.x = c.x+2, .y = c.y+1}, board);
       if (r){
           return true;
       }
    }
    if (c.x + 1 < 8 and c.y + 2 < 8 and board[@intCast(usize, c.y + 2)][@intCast(usize, c.x + 1)] == -1){
        var r =  solve_rec(a,pos+1, coor{.x = c.x + 1, .y = c.y + 2}, board);
        if (r){
           return true;
       }
    } 
    if (c.x -1 > -1 and c.y + 2 < 8 and board[@intCast(usize, c.y + 2)][@intCast(usize, c.x - 1)] == -1){
        var r =  solve_rec(a, pos+1, coor{.x = c.x -1,.y =  c.y + 2}, board);
        if (r){
           return true;
       }
    }
    if (c.x -2 > -1 and c.y + 1 < 8 and board[@intCast(usize, c.y + 1 )][@intCast(usize, c.x -2)] == -1){
        var r =  solve_rec(a,pos+1, coor{.x = c.x - 2,.y =  c.y+1}, board);
        if (r){
           return true;
       }
    } 
     if (c.x -2 > -1 and c.y - 1  > -1 and board[@intCast(usize, c.y -1 )][@intCast(usize, c.x -2 )] == -1){
        var r =  solve_rec(a,pos+1, coor{.x = c.x -2,.y =  c.y-1}, board);
        if (r){
           return true;
       }
    } 
    if (c.x -1  > -1 and c.y -2  > -1 and board[@intCast(usize, c.y -2 )][@intCast(usize, c.x -1 )] == -1){
        var r =  solve_rec(a, pos+1, coor{.x = c.x-1, .y = c.y-2}, board);
        if (r){
           return true;
       }
    } 
     if (c.x + 1 < 8 and c.y -2  > -1 and board[@intCast(usize, c.y -2 )][@intCast(usize, c.x +1 )] == -1){
        var r =  solve_rec(a, pos+1, coor{.x = c.x+1,.y =  c.y-2}, board);
        if (r){
           return true;
       }
    } 
    if (c.x + 2  < 8 and c.y - 1  > -1 and board[@intCast(usize, c.y -1 )][@intCast(usize, c.x + 2)] == -1){
        var r =  solve_rec(a, pos+1, coor{.x = c.x+2, .y =  c.y-1}, board);
        if (r){
           return true;
       }
    } 
    if (pos == 0){
        return false;
    }
        board[@intCast(usize, c.y)][@intCast(usize, c.x)] = -1;
        return false;
    
}

pub fn solve() !void {
    var current:i8 = 0;
    var n = coor{.x = 0, .y = 0};
    var board = &example1;
    if (solve_rec(8, 0, n, &example1)) {
        var file = try fs.cwd().createFile("backtracking_knight.txt", .{});
        var pos:i8 = 1;
        for (board) | row, i | {
            for (row) | c, j | {
                var buf = [_]u8{0}**8;
                var wrote = try std.fmt.bufPrint(&buf, "  {:3}", .{c});
                var a_ = try file.write(wrote);
                //var b_ = try file.write(" ");
            }
            var a_ = try file.write("\n");
        }  
    }
}

test "solution" {
    try solve();
    assert(1 == 1);
}