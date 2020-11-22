
Tooling
===
Process hacker does not show good private bytes increase for dll loaded (nor Modules view nor Memory view).
System Internals VMMap shows good private bytes for shared dlls loaded (both native and managed from GAC which where NGENed).
VMMap fails to show private bytes size for local managed dlls loaded.

Context
=======
Measurements can vary basing on environment of tests. Next was used for this doc:

- Windows 7 x86-64
- .NET 4.5.1
- MS Office 2010 Professional 32 bit



Pure native dll 
===============
`native-dll-loader.exe`, `972 kB`, 12.51 MB, , , 1620, 1, 872 kB, 11, 0, 0

Process Hacker shows more memory via dlls then private bytes

`ntdll.dll`: Image (Commit), 0x77b60000, 856 kB, RX
`kernel32.dll`: Image (Commit), 0x760d0000, 772 kB, RX

No `rpcrt4.dll` by default

After `rpcrt4.dll` loaded

`native-dll-loader.exe`, `1.05 MB`, 14.7 MB, , , 10364, 1, 952 kB, 14, 0, 0

Adds approx. `103 kB`

`c:\Windows\System32\rpcrt4.dll 1,217,024 bytes`

No good mapping

rpcrt4.dll: Image (Commit), 0x75e20000, 4 kB, R
rpcrt4.dll: Image (Commit), 0x75e30000, 600 kB, RX
rpcrt4.dll: Image (Commit), 0x75ed0000, 12 kB, RX
rpcrt4.dll: Image (Commit), 0x75ee0000, 4 kB, RW
rpcrt4.dll: Image (Commit), 0x75ef0000, 16 kB, R
rpcrt4.dll: Image (Commit), 0x75f00000, 20 kB, R
rpcrt4.dll: Image (Reserve), 0x75e21000, 60 kB, 
rpcrt4.dll: Image (Reserve), 0x75ec6000, 40 kB, 
rpcrt4.dll: Image (Reserve), 0x75ed3000, 52 kB, 
rpcrt4.dll: Image (Reserve), 0x75ee1000, 60 kB, 
rpcrt4.dll: Image (Reserve), 0x75ef4000, 48 kB, 
rpcrt4.dll: Image (Reserve), 0x75f05000, 44 kB, 


empty.dll: Image (Commit), 0x510c0000, 4 kB, R
empty.dll: Image (Commit), 0x510c1000, 4 kB, RX
empty.dll: Image (Commit), 0x510c2000, 4 kB, R
empty.dll: Image (Commit), 0x510c3000, 4 kB, RW
empty.dll: Image (Commit), 0x510c4000, 8 kB, R


Managed dlls
=========================
*Local* 

Managed dlls with zero or one type add much more bytes then size of dll.

`one-type-dll.dll` which is `3.5 KB` adds more then `30KB` Private Bytes.

*GAC*

Loads compiled dlls. Accessing types does not involves JIT.

Started dll  takes `5.6 MB` private bytes. Loading basic functional makes process `15 MB` private bytes.

Preloading most of any `PresentationFramework`(WPF most high level layer) dependences still adds private bytes `8MB`. `PresentationFramework.ni.dll` is  17.95 MB.

`System.ServiceModel` (WCF) adds approx `9MB` after all WPF loaded. `System.ServiceModel.ni.dll` is `18.8` MB.


Increase visible not only in Private bytes but in other Private XYZ (e.g. Private WS) values in tools. 
 
.NET in host MS office process
=========================
How much .NET eats out of heavy C++ based Excel.exe:

| What in process                                                            | Private Bytes (MB) | Virtual Bytes (MB)
 
| Excel without add-ins, navigate over some native tabs, no functional usage | 26 	              | 336
 
| As first case plus dummy Shim COM addin which loads CLR                    | 33                 | 397
 
| As first case plus dummy VSTO add-in                                       | 59                 | 471

.NET adds minimally  33-26=7 MB of private bytes, but when really used adds 59-26=33MB 



