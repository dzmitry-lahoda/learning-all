NLocalIpc
=========

Using local inter process communication facilities from .NET

LPC
--
https://www.diigo.com/search?adSScope=my&what=ipc%20windows%20lpc&snapshot=no



Service oriented and message passing architecture for Desktop applications
===
Problem
---
Modern  Desktop end user solutions are complex as some server applications. Modern workstations are as powerful as some server stations. Integrating all parts into deployable package is daunting task. Old development processes try to tight components developed and deployed into monolith architecture. Monolith architecture hampers productivity and innovation of stakeholders. There is the need for mind shift in development process of Desktop solution architects and developers to tackle  requirements. Adding new things by low qualified developers should be allowed and should not hamper system as whole if addition done wrong.

Context
---
Solution consist of several running processes directly developed by company employers.
Solution depends on other processes provided as services by third parties and operating system. 
Solution developed by distributed teams (America, Europe, Asia).
Each team provides their own processes to run. 
Each team provides their modules to be loaded by hosting process.
Modules provided services for in process and for out of process consumption.
Modules code could be loaded from internet.
Old native technologies are not build for isolation by default.
Old native technologies are not build for many cores user computers.
Old native technologies are not build for managed environments.
Old native technologies are build for performance greatly sacrificing ease of development.
Modern operating systems and web browsers have inter process communication, isolation, and message passing as must for further future grow [1] [2] [3] [4] [5] [8]
End user operating systems have isolation and visualization facilities build in for sandboxed execution.

Enforces
---
Need of ideological and code basis for building big heterogeneous Desktop solutions.

Solution
---
Service oriented [6] and message passing[7] patters modified with the fact of single machine execution can be good basis for this. End user machine execution dictates necessity for higher performance and lesser memory consumption then server applications by means of lesser security and not need of compatibility with machines on network. End user machine execution allowing building some patterns using named operating system primitives like named shared memory and event waits. 


[1] http://www.barrelfish.org/
[2] http://en.wikipedia.org/wiki/Local_Procedure_Call
[3] http://en.wikipedia.org/wiki/Unix_domain_socket
[4] http://msdn.microsoft.com/en-us/library/windows/desktop/ff966507.aspx
[5] http://www.chromium.org/developers/design-documents/multi-process-architecture
[6] http://soapatterns.org
[7] http://rfc.zeromq.org/
[8] kdbus
