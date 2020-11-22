
const Builder = @import("std").build.Builder;
const CrossTarget = @import("std").zig.CrossTarget;
const Abi = @import("std").Target.Abi;
pub fn build(b: *Builder) void {
    const target = b.standardTargetOptions(.{ .default_target = CrossTarget{ .abi = Abi.gnu } });
    const mode = b.standardReleaseOptions();
    const exe = b.addExecutable("main", "main.zig");
    exe.addPackagePath("win32", "./../../zig-win32/src/main.zig");
    exe.linkSystemLibrary("c");
    exe.linkSystemLibrary("gdi32");
    exe.linkSystemLibrary("user32");
    exe.linkSystemLibrary("kernel32");
    exe.setTarget(target);
    exe.setBuildMode(mode);
    exe.install();

    const run_cmd = exe.run();
    run_cmd.step.dependOn(b.getInstallStep());

    const run_step = b.step("run", "Run the app");
    run_step.dependOn(&run_cmd.step);
}