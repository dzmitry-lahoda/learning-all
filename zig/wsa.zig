const std = @import("std");
const io = @import("stdio.h"); 
const a2 = @cImport(@cInclude("d:/zig/lib/zig/libc/include/any-windows-any/vadefs.h")); 
const a1 = @cImport(@cInclude("d:/zig/lib/zig/libc/include/any-windows-any/winapifamily.h"));  
const winsock2 = @cImport(@cInclude("d:/zig/lib/zig/libc/include/any-windows-any/winsock2.h")); 
pub fn main() void {winsock2.WSACleanup(); std.debug.warn("Hell", .{});}