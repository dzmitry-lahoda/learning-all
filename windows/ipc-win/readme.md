# Background
Need of integration for Desktop applications, MS Office add-ins, IE add-ins, written managed and native code, heavily dependant on Web services.

# Fast

See chart in *measurments.png*.

## Run

Script files in *distribution* folder. Run server bat and corresponding client bat next (e.g. *managedpipesbytesclient.bat* and *pipesbytessrv.bat*).

## Pre build
- Got to *scripts\nuget.bat* to get files *lib* files in place.

## Release

- Native dependencies for lease are builed into *lib\lib*
- Build *ipc.sln*

## Debug (do not use for measurements because several times slower)

- Build Debug *gtest.sln*
- Build Debug *protobuf.sln*
- Build Debug *ipc.sln*


# Why

Having multitasking operating system [12] with memory protection by virtual memory [13] implies several tasks (processes) need ways to share some data via some kind of shared memory in order to implement shared workflows. They cannot read/write/share each other memory directly and have to ask operating system to allocate such piece of uniquely identified memory. And because 2 processes are always 2 thread leads to need of multithreaded synchronization even if user perceives all operations synchronous. Having some object scattered in virtual heap of first process that needed to be shared with other process leads us to notion of serialization. This object should be converted into continuous byte array, then written to shared memory, and converted back into the same object in other process virtual heap. There is possibility to write different parts of object into several shared memory locations, but this will bloat operating system with many kernel objects and considered to be slow.

# Considerations
- Operating system provides various IPC[1] mechanisms all based on shared memory[2].  It means that any custom mechanism cannot beat shared memory, but can provide different abstractions (e.g. remote procedure call or stream reads/writes) and use different synchronization mechanisms(e.g. spin wait instead of lock) leading to different performances. COM/OLE/RPC additionally use internal (A)LPC[8] mechanism which provides some improvements which can influence speed of sending 256 byte messages and synchronization.
- Target machines have minimum 2 hardware threads. Having several hardware threads can be similar to having one if second hardware thread is occupied with some not IPC task. Multithreading has own impact - it can make things faster because several things done simultaneously,  but need to pay for synchronization. Number of memory and CPU cache channels can influence multithreaded situation [14]. 
- Having 2 scenarios.  Request/response where client pulls data from server many times by requesting it and when client makes single request then server pushes (streams) data many times. First needs to have all synchronization and error checking, in second case minimum number of checks. 
- Requests and responses can have various sizes - several big request or many small requests.
- The main goal is performance and possibility to tune it. Layers of abstractions (e.g. representing shared memory reads/writes as generic object interface) decrease performance but ease development. Layering managed code above unmanaged ads some penalty (marshaling). Need to measure baseline of request/response and streaming scenarios with minimum overhead. Another consideration is API should be usable from managed/unmanaged code with small configuration hassle. Ideally the same API used for workflows and data pumping.


# Neglected measurements

- Mailslots shows to be slow as shown by [3] and of customer teams reports. Provides one way communication which should be doubled to provide communication in other direction.  Does things which will never be used in aforementioned scenarios (e.g. broadcasting to domain computers without guaranteed delivery [11] ).
- Sockets shows to be slow as shown by [3] and of customer teams reports. Provides hassle of choosing port [6] and potentially can be blocked of security reasons (e.g. needs administrator intervention to unblock).
- DDE is "not as efficient as newer technologies" [7], used Windows Messaging and Shared Memory inside [4], considered legacy and not suggested for use for new projects [5].
- COM provides loading/activation and object layer above MS-RPC, measuring only MS-RPC.

# Implementation

- All tests done synchronous with one sending process and one single threaded waiting process. Real multithreaded scenario can be estimated and tested after. 
- Streaming tests where done by sending big requests and receiving confirmation instead of response.
- Requests represent object with name, long list of identifiers and short list of fields for identifiers. Response represent array containing number of string items which is multiply of number of ids by number of fields. All string are short enough to visually fit into default spreadsheet cell.

# Results

## Performance

- When making few big data exchanges then IPC (synchronization and memory copying) is neglectable if to compare with object serialization to byte array. Better having simple objects (e.g. string or array of strings or with sequential memory layout) for IPC. Serialization is slowest thing in local IPC. IPC takes considerable amount of time for many small objects.
- Adhoc binary serialization is viable solution because using objects translation to 3rd party serializable form can happen to be unallowable overhead. Translating objects into serializable form can add 60% to serialization time.
- Tests show that batching of many requests (request ~100 bytes, response ~1kb) into buckets of 500 items can improve IPC by ~25% (see **batching** sheet). Batching is combining many small objects into single collection, serialize them and send to other process.  Batching and unbatching takes time, but of aforementioned size and amount of objects is tested to be good tradeoff in provided setup. Should be evaluated on task at hand if setup is different.
- Containers used influence serialization greatly (doubly linked list of strings is 10-20% slower than dynamically resized array during serialization).  But public API can prevent from using what is faster.
- Sending raw bytes over IPC using managed client is slower by ~10-15%, if server managed then will be ~20-30% (several milliseconds). Sending custom serialized objects native code is slower ~10-20% then managed during IPC, if make server managed then native run will slower ~20-40% (dozens of milliseconds). Means that custom managed serialization is faster than custom native leading to faster than native IPC, serialization is done the same way in both cases (very close translation of native to managed code). If carefully rewrite code then native serialization will outperform managed, i.e. have room for improvement. There are many cases in the internet when managed is on par with native like in [10].
- Windows Messages for data copying are ~15-20% slower than Named Pipes, which similar to [3]. Did only streaming tests.
- Synchronous IPC communication is ~1.3-2 times slower on machines with single available hardware thread (2 processes block on the same primitives but share on executing thread).
- Named Pipes sending raw bytes are faster than Shared Memory on single core VM means better pattern of synchronization and resources usage was implemented internally above Shared Memory. 
- Named Pipes send many small raw bytes items are faster than custom API above Shared Memory.
- Steaming raw bytes is ~5-20% faster than request-response. Regarding serialization, streaming has to do less of it which can make it at faster than request/response depending by difference of relative size of exchanged objects. E.g. if serialization takes 1 ms for request and 10 ms for response, then there is no request overhead when streaming.
- RPC is several times slower than raw Named Pipes but still can be considered neglectable if serialization involved. Changing RPC transport (several lines of code) to TCP sockets leads to considerably worse performance. RPC library already loaded into many Windows processes.
- MS-RPC via ALCP is faster then via Named pipes at least on 20%. Potentially ALCP can have lower memory overhead then Named Pipes. 
- Protobuf has better performance then pure .NET based serialization mechanisms[38]  and has native implementation[31]


## Development
- Developing low level (e.g. using Named Pipes) is considered harder than via abstractions. If need to support more workflow oriented scenarios then have to consider COM and MSRPC and 3rd party solutions (evaluate what managed and native abstractions provided, C#-C++ interoperability, streaming, synchronization, evening, callbacks, sync and async procedure calls, sessions, OOP, steaming, transport, security configuration), look at raw API in performance comparison. Potentially doing streaming low level and other stuff through aforementioned APIs basing on performance measurements and restrictions.
- See analysis.usage.png for techs usage layering from low level to abstracted. Or analysis.vsd with Visio 2010 UML 2.2 stencils.
- MS-RPC can use Named Pipes or ALPC. Official documented usage is IDL specific[16], has published book in public domain [18]. There is wrappers for C#[9] without using IDL. Providing transport independent function call abstraction and declarative call semantics (e.g. async/sync) good for workflows and ease of development. Is used by Excel and COM for in-process and out-of-process scenarios.
- Writing custom serialization is time consuming, easier to translate to some 3rd party format or use 3rd party format in APIs.
- COM adds configuration and registration hassle providing loading, activation, unloading services over MS-RPC. Out-of-proc COM can be easily used by managed and unmanaged code, implemented in any, and easy to support interface via Type Libraries. Need of registration adds complexity to deployment, testing and administration which good to dismiss. There are ways like Registration Free COM [22], runtime registry modification, changing Running Object Table [15] manually.
- Windows Messaging not only slower but has issue regarding multithreading [26] with window oriented semantics of resolution
- There different binary serialization formats [30], including binary XML Infoset and custom serialization. Custom serialization is viable when we need to support external API and cannot change entities to other object interface (e.g. VBA script consumer cannot work with Protobuf [31] messages). Managed code having attributes and runtime reflection build in has more easy ways to do unification � the same object can be serialized by some external library and still satisfy previous public API. Can go directly using 3rd party serialization instrumented objects internally. Protobuf was chosen because managed and native implementation exists, it is mature solution used in many circumstances.  Protobuf messages are not self-described with externally provided schemes, which make them more efficient the binary XML Infoset or [32], and making them less flexible but still extensible. Considering local IPC only scenarios without need to work with complex networking Protobuf is good solution.
 
#Conclusion
- I suggest using MS-RPC for all scenarios because its APIs are full of useful abstractions and of good performance. It is mature solution covering corner but production cases (e.g. like security [17], in-proc transport when servers serve both inside process and to outside clients) and easy to find answers via search (e.g. "codeproject RPC" leads me to [19]). I think that investigation is needed how convenient (e.g. how easy to create and support)   C++<->C# communication via RPC and investigate COM without registration for easy C#<-> C++ API support, I believe it can be done in process of doing such integration. If I was determined to develop for Excel and some Daemon I would go with it. To wrap native interfaces we have 4 manual options � convert C stubs into according C# and do PInvoke , use C++/CLI, use reg free COM or pure C wrapper with C# PInvoke. To wrap managed interfaces we also have several options. There is no ultimate option, I would for options that will work from start and give opportunity to automate later (e.g. convert C stubs to C#). MS-RPC is tech which support ALPC transport which is internal fast mechanism for IPC on Windows(can be considered repalcement for Named Pipes)[40] with hard to use reverse engineered API [36].
- There several open source projects. Like protobuf-remote[34], ZMQ/clrzmq [23], IIOP.NET [33] and DBus/DBusSharp [24]  [25] which seems to be fast enough and flexible with good abstractions, but lack of Named Pipes(sockets only) native transport which should be implemented to test their performance and possibility to use (it can take days to implement such, or searching why not implemented still). ZMQ (as MS-RPC) is good if go networking scenarios later (e.g. data server in local network), still can be needed to add custom IDL above. IIOP.NET/CORBA is IDL based solution with IPC code generation for managed and existing counterpart of native. Previous are good if need to integrate some non-Microsoft specific techs. Also there is Chromium IPC [29] which should be wrapped into C# for tests which either can take days, this tech is good if Chromium is used as UI container. Also there is  Thrift [39] which hase Named Pipe transport. [34],[33] and [39] generate client proxy and sever stub from IDLs and all work C++ and C#.
- Was able to get Registration Free Out of Process Late Binding(like CLR reflection invoke) COM object as in [35] by registering in ROT. Still struggling to make early binding(QueryInterface)to work without registry. Also it seems Out of Proc COM have to listen on STA thread Windows message pump in order to invoke COM server methods. Need to look further to dissect COM into parts and reuse in MS-RPC scenarios.
- WCF provides very rich and extensible way of doing job, but as people  WCF is showed to be slow in comparison with other techs [20] [21]. Measurements where done without optimizations (e.g. not using streaming, default to XML serialization and encoding, chatting instead of batching).  Also .NET + WCF is not lightweight solution,in terms of process working set, i.e. cannot do multiprocess architecture [27] (e.g. many small clients) because each process will take too much memory right after start[43], situation can change in future with new version of Windows and .NET framework (>4.5), or with possibility to manually cut .NET into parts. 
- .NET Remoting IPC is considered legacy and slower then WCF[37] and also not compatible with native code.
- There is WCF implemntation for native code[44], compatible Named Pipes implementaion in Windows 8 for C++[46], this implentation is extensible so can backport Named Pipes to Windows 7/XP[45]. XP has special redistributable of `webservices.dll`. Workflow can be define C# interface, generate WSDL out of interfaces, generate C++.
- WinRT supports IPC. [42] application is sample of it. Server side is defined in IDL. IDL extened with new attributes.  Was not able to build and confirm WinRT uses MS-RPC via ALPC which is visible via Process Hacker -> Properties -> Handles.

# Measurements

[Machine 1] is $800 laptop (Windows 7 SP1 Intel I7 with 4 cores, DDR3 8GB x 2). Machine 2 is cut down virtual XP 32 bit machine on Machine 1  with BIOS disabled hardware support for virtualization provided with 1 hardware thread and 2GB RAM and (xp.vbox-prev). See *measurements.machine1.htm* and *measurements.machine2.htm* generated by running scripts. Contains all kinds of IPC with or without serialization, plus serialization only tests.

Highlights
----------

See chart in *measurements.xlsx* for visualization of decision making items. 

BY DEFAULT ALL COMMUNICATION GOES EVENTUALLY THROUGHT *NAMED PIPES* EXCEPT WHERE *ALPC* DOCUMENTED DIRECTLY

**Machine 1**

* Sending raw bytes 10 requests ~100kb and getting ~1000kb response takes 0.013001 sec.
* Sending raw bytes 1000 requests ~1kb and getting ~10kb response takes 0.0260024 sec.
* Sending raw bytes 2000 requests ~0.1kb and getting ~1kb response takes 0.0280027 sec.

* Managed sending raw bytes 2000 requests ~0.1kb and getting ~1kb response takes 0.0290112 sec.

* RPC sending raw bytes 20 requests ~50kb and gets ~500kb response of server process 0.034996 sec.
* RPC sending raw bytes 1000 requests ~1kb and gets ~10kb response of server process 0.0429957 sec.
* RPC sending raw bytes 10000 requests ~0.1kb and gets ~1kb response of server process	0.1949825 sec.

* RPC ALPC sending raw bytes 20 requests ~50kb and gets ~500kb response of server process 0.0170016 sec.
* RPC ALPC sending raw bytes 1000 requests ~1kb and gets ~10kb response of server process 0.0450044 sec.
* RPC ALPC sending raw bytes 10000 requests ~0.1kb and gets ~1kb response of server process	0.160016 sec.

* Sending serialized custom objects 10 requests ~100kb and getting ~1000kb response takes 0.375112 sec.
* Sending serialized custom objects 20 requests ~50kb and getting ~500kb response takes 0.360108 sec.
* Sending serialized custom objects 1000 requests ~1kb and getting ~10kb response takes 0.374112 sec.
* Sending serialized custom objects 2000 requests ~0.1kb and getting ~1kb response takes 0.118035 sec.

* Managed sending serialized custom objects 10 requests ~100kb and getting ~1000kb response takes 0.2973002 sec.
* Managed sending serialized custom objects 20 requests ~50kb and getting ~500kb response takes 0.2971797 sec.
* Managed sending serialized custom objects 1000 requests ~1kb and getting ~10kb response takes 0.297758 sec.
* Managed sending serialized custom objects 2000 requests ~0.1kb and getting ~1kb response takes 0.09774 sec.

* WCF 20 requests ~50kb and gets ~500kb response of server process takes 0.1217944 sec
* WCF 1000 requests ~1kb and gets ~10kb response of server process takes 0.3074838 sec
* WCF 10000 requests ~0.1kb and gets ~1kb response  of server process takes 1.3944975 sec


* Sending custom objects translated into Protobuf messages 10 requests ~100kb and getting ~1000kb response takes 0.569171 sec.
* Sending custom objects translated into Protobuf messages 1000 requests ~1kb and getting ~10kb response takes 0.558167 sec.
* Sending custom objects translated into Protobuf messages 2000 requests ~0.1kb and getting ~1kb response takes 0.156047 sec.

* Pushing raw bytes 1000 ~10kb items and wait confirmation takes 0.0220022 sec.
* Pushing raw bytes 2000 ~1kb items wait confirmation takes 0.022002 sec.

**Machine 2**
 
* Sending raw bytes 10 requests ~100kb and getting ~1000kb response takes 0.0200288 sc.
* Sending raw bytes 1000 requests ~1kb and getting ~10kb response takes 0.0300431 sec.
* Sending raw bytes 2000 requests ~0.1kb and getting ~1kb response takes 0.0200286 sec.

* Sending serialized custom objects 10 requests ~100kb and getting ~1000kb response takes 0.741066 sec.
* Sending serialized custom objects 20 requests ~50kb and getting ~500kb response takes 0.630907 sec.
* Sending serialized custom objects 1000 requests ~1kb and getting ~10kb response takes 0.490705 sec.
* Sending serialized custom objects 2000 requests ~0.1kb and getting ~1kb response takes 0.130188 sec.


# To be done
- WebServices.dll over Named Pipes
- Try to load to fix early binding with ROT COM by loading server stub assembly into process manually.
- Review C++ for performance issues related to serialization because C# custom serialization is faster
- Try to use wait or spin for confirming data read instead of writing byte during streaming.
- Check 32 bit, 64 bit and 32 bit on 64 bit scenarios of communication
- Shuffle tests and shuffle data, investigate CPU cache influence
- Make tests of managed Protobuf
- Increase managed tests coverage (server for pipes and protobuf)
- Add WCF steaming
- Add WCF protobuf
- Add WCF via ALPC
- Add NDceRpc.ServiceModel (custom/Mono like hich level WCF, Protobuf, MS-RPC via ALPC)
- Wrap Chromium IPC in C#
- Default and optimized WCF tests
- Write and test named pipe transport for open sources which lack it, make tests.
- Evaluate UI synchronization impact
- Send 0.1kb and 3kb one way
- Broadcasting data to 10 processes
- 400kb and 2000kb, 1kb and 1kb requests/responses
- Make multithreaded tests (consider single channel memory issues)
- Tuning synchronization to optimize chatty (many small requests) conversations by spin vs. lock
- Clean up of non relevant stuff
- Added ASYNC RPC samples
- Add ORLEANS RPC
- Run on VS Code.
 
 [Machine 1]: http://valid.canardpc.com/2639433
 [1]: http://msdn.microsoft.com/en-us/library/windows/desktop/aa365574.aspx
 [2]: http://blogs.msdn.com/b/salvapatuel/archive/2009/06/08/working-with-memory-mapped-files-in-net-4.aspx
 [3]: https://onegazhang.wordpress.com/2008/05/28/fastest-ipc-method-on-windows-xpvista/
 [4]: http://msdn.microsoft.com/en-us/library/windows/desktop/ms648774.aspx
 [5]: http://ndde.codeplex.com/
 [6]: http://stackoverflow.com/questions/8748396/ipc-port-ranges
 [7]: http://msdn.microsoft.com/en-us/library/windows/desktop/aa365574.aspx#base.using_dde_for_ipc
 [8]: http://en.wikipedia.org/wiki/Local_Procedure_Call
 [9]: https://github.com/asd-and-Rizzo/NDceRpc
 [10]: http://www.grimes.nildram.co.uk/dotnet/man_unman.htm
 [11]: http://msdn.microsoft.com/en-us/library/windows/desktop/aa365576.aspx
 [12]: http://en.wikipedia.org/wiki/Computer_multitasking
 [13]: http://en.wikipedia.org/wiki/Virtual_memory
 [14]: http://en.wikipedia.org/wiki/Multi-channel_memory_architecture
 [15]: http://msdn.microsoft.com/en-us/library/windows/desktop/ms695276.aspx
 [16]: http://msdn.microsoft.com/en-us/library/windows/desktop/aa374169.aspx
 [17]: http://msdn.microsoft.com/en-us/library/bb625960.aspx
 [18]: http://archive.org/details/microsoftrpc00shirmiss
 [19]: http://www.codeproject.com/Articles/4837/Introduction-to-RPC-Part-1
 [20]: http://techmikael.blogspot.com/2010/02/blazing-fast-ipc-in-net-4-wcf-vs.html
 [21]: http://csharptest.net/787/benchmarking-wcf-compared-to-rpclibrary/
 [22]: http://en.wikipedia.org/wiki/Component_Object_Model#RegFree_COM
 [23]: http://www.zeromq.org/
 [24]: http://www.freedesktop.org/wiki/Software/dbus
 [25]: https://github.com/mono/dbus-sharp
 [26]: http://msdn.microsoft.com/en-us/library/ms644927.aspx#deadlocks
 [27]: http://www.chromium.org/developers/design-documents/multi-process-architecture
 [29]: http://www.chromium.org/developers/design-documents/inter-process-communication
 [30]: http://en.wikipedia.org/wiki/Comparison_of_data_serialization_formats#Comparison_of_binary_formats
 [31]: http://code.google.com/p/protobuf/
 [32]: http://bsonspec.org/
 [33]: http://iiop-net.sourceforge.net/
 [34]: http://code.google.com/p/protobuf-remote/
 [35]: https://sites.google.com/site/jozsefbekes/Home/windows-programming/dotnet-registering-an-object-to-the-running-object-table-from-a-non-com-project
 [36]: http://www.zezula.net/en/prog/lpc.html
 [37]: http://msdn.microsoft.com/en-us/library/bb310550.aspx#wcfperform_topic3d
 [38]: http://code.google.com/p/protobuf-net/wiki/Performance
 [39]: http://thrift.apache.org/
 [40]: http://blogs.msdn.com/b/winsdk/archive/2009/11/04/why-does-the-ndrclientcall2-api-call-takes-more-then-4-5-milliseconds-to-complete.aspx
 [41]: http://blogs.msdn.com/b/ntdebugging/archive/2007/07/26/lpc-local-procedure-calls-part-1-architecture.aspx
 [42]: http://code.msdn.microsoft.com/windowsapps/Creating-a-Windows-Runtime-ed84af9d
 [43]: https://gitorious.org/asdandrizzo/research-dll-memory
 [44]: http://msdn.microsoft.com/en-us/magazine/ee335693.aspx
 [45]: http://msdn.microsoft.com/en-us/library/windows/desktop/dd401780.aspx
 [46]: http://msdn.microsoft.com/en-us/library/windows/desktop/dd323319.aspx
