Do not use `Path.GetFileNameWithoutExtension(Path.GetRandomFileName())`, use `Path.GetRandomFileName()` fully
===


While full name is `cryptographically strong`  like stated in [Path.GetRandomFileName], but droping extnsions makes uniquines fail bad as run of [random_tests.fs]`random_file_name` shows:

```
Finished generation of full names like: oi0uz5if.1fn
Non unique / sequence length
0 / 2000000
0 / 2000000
0 / 2000000
0 / 2000000
0 / 2000000

Finished generation of no extension names like: oi0uz5if
Non unique / sequence length
0 / 2000000
5 / 2000000
2 / 2000000
0 / 2000000
4 / 2000000
``



[Path.GetRandomFileName]: http://msdn.microsoft.com/en-us/library/system.io.path.getrandomfilename.aspx