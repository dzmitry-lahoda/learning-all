const assert = @import("std").debug.assert;
const fs = @import("std").fs;
const std = @import("std");
const coor = struct {
    x: i8, y: i8
};

// var maze = [4][4]i8 {
//         [_]i8{1,0,0,0},
//         [_]i8{1,1,1,0},
//         [_]i8{1,0,1,1},
//         [_]i8{1,1,0,1},
//     };

var maze = [4][4]i8{
    [_]i8{ 1, 1, 1, 1 },
    [_]i8{ 1, 1, 1, 1 },
    [_]i8{ 1, 0, 0, 0 },
    [_]i8{ 1, 1, 1, 1 },
};

var path = [_]u8 {0} * 4;

pub fn solve_rec(c: coor, end: coor) bool {
    maze[@intCast(usize, c.y)][@intCast(usize, c.x)] = 2;
    if (c.x == 3 and c.y == 3) {
        return true;
    }
    if (c.x + 1 < 4 and maze[@intCast(usize, c.y)][@intCast(usize, c.x + 1)] == 1) {
        var r = solve_rec(coor{ .x = c.x + 1, .y = c.y }, end);
        if (r) {
            return true;
        }
    }
    if (c.y + 1 < 4 and maze[@intCast(usize, c.y + 1)][@intCast(usize, c.x)] == 1) {
        var r = solve_rec(coor{ .x = c.x, .y = c.y + 1 }, end);
        if (r) {
            return true;
        }
    }
    maze[@intCast(usize, c.y)][@intCast(usize, c.x)] = -2;
    return false;
}

pub fn solve() !void {
    var current: i8 = 0;
    var n = coor{ .x = 0, .y = 0 };

    if (solve_rec(n, n)) {
        var file = try fs.cwd().createFile("backtracking_maze_4.txt", .{});

        for (maze) |row, i| {
            for (row) |c, j| {
                var buf = [_]u8{0} ** 8;
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
