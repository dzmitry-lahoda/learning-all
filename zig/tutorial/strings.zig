const assert = @import("std").debug.assert;
const mem = @import("std").mem;

test "test" {    
   const bytes = "123";
   assert(@TypeOf(bytes) == *const [3:0]u8); // pointer to 0 terminated array of mem of 8 bit chars
   assert(bytes.len == 3);
   assert(bytes[3] == 0);
   assert(mem.eql(u8, bytes, "1\x323"));
   const multiline = 
   \\1
   \\2
   ;
   assert(mem.eql(u8, "1\n2", multiline));
   const read = @embedFile("./include.zig");
   assert(mem.eql(u8, "const template = \"{A} {B}\";", read));
   var mut = "42";
   assert(@TypeOf(mut) == *const[2:0]u8);
  //mut[1] = '1';
  //assert(mem.eql(u8, "41", mut));

}