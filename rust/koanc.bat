:: compiles rust koan into `bin` folder
mkdir -p bin
rustc %* --out-dir bin