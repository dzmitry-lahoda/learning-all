const std = @import("std");
const win32 = @import("win32").c;
pub fn main() void {
    _ = win32.MessageBoxA(0, "Hello world", "title", 0);
    }