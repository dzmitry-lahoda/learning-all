Native request - response
=========================

Client sending raw bytes via Shared Memory
--------------------------------
**Client process requests ~100kb and gets ~1000kb response of server proces**

  *  Avg. time 0.000500011 sec
  *  Min. time 0 sec
  *  Max. time 0.00100017 sec
  *  Tot. time 0.00500011 sec
  *  Stops 10


**Client process requests ~50kb and gets ~500kb response of server process**

  *  Avg. time 0.000300002 sec
  *  Min. time 0.00100017 sec
  *  Max. time 0.00100017 sec
  *  Tot. time 0.00600004 sec
  *  Stops 20


**Client process requests ~10kb and gets ~100kb response  of server process**

  *  Avg. time 9.99999e-005 sec
  *  Min. time 0.00100017 sec
  *  Max. time 0.00100017 sec
  *  Tot. time 0.00999999 sec
  *  Stops 100


**Client process requests ~1kb and gets ~10kb response  of server process**

  *  Avg. time 4.90003e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.00100017 sec
  *  Tot. time 0.0490003 sec
  *  Stops 1000


**Client process requests ~0.1kb and gets ~1kb response  of server process**

  *  Avg. time 4.40001e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.00100017 sec
  *  Tot. time 0.0880003 sec
  *  Stops 2000
  
  
## Client sending raw bytes via Named Pipes reusing initialized resources 

**Client process requests ~100kb and gets ~1000kb response of server process**

  *  Avg. time 0.0013001 sec
  *  Min. time 0.000999928 sec
  *  Max. time 0.00200033 sec
  *  Tot. time 0.013001 sec
  *  Stops 10

**Client process requests ~50kb and gets ~500kb response of server process**

  *  Avg. time 0.00065006 sec
  *  Min. time 0 sec
  *  Max. time 0.0010004 sec
  *  Tot. time 0.0130012 sec
  *  Stops 20


**Client process requests ~10kb and gets ~100kb response  of server process**

  *  Avg. time 0.000140016 sec
  *  Min. time 0 sec
  *  Max. time 0.0010004 sec
  *  Tot. time 0.0140016 sec
  *  Stops 100


**Client process requests ~1kb and gets ~10kb response  of server process**

  *  Avg. time 2.60024e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.0010004 sec
  *  Tot. time 0.0260024 sec
  *  Stops 1000


**Client process requests ~0.1kb and gets ~1kb response  of server process**

  *  Avg. time 1.40014e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.0010004 sec
  *  Tot. time 0.0280027 sec
  *  Stops 2000

## Client sending raw bytes via MS-RPC  reusing initialized resources

**Client process requests ~100kb and gets ~1000kb response of server process**

  *  Avg. time 0.00479953 sec
  *  Min. time 0.00299954 sec
  *  Max. time 0.00699925 sec
  *  Tot. time 0.0479953 sec
  *  Stops 10


**Client process requests ~50kb and gets ~500kb response of server process**

  *  Avg. time 0.0017498 sec
  *  Min. time 0.000999689 sec
  *  Max. time 0.00299978 sec
  *  Tot. time 0.034996 sec
  *  Stops 20


**Client process requests ~10kb and gets ~100kb response  of server process**

  *  Avg. time 0.000259976 sec
  *  Min. time 0 sec
  *  Max. time 0.00100017 sec
  *  Tot. time 0.0259976 sec
  *  Stops 100


**Client process requests ~1kb and gets ~10kb response  of server process**

  *  Avg. time 4.29957e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.00100017 sec
  *  Tot. time 0.0429957 sec
  *  Stops 1000


**Client process requests ~0.1kb and gets ~1kb response  of server process**

  *  Avg. time 1.94982e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.00100017 sec
  *  Tot. time 0.0389965 sec
  *  Stops 2000

## Client sending raw bytes via MS-RPC ALPC reusing initialized resources

**Client process requests ~100kb and gets ~1000kb response of server process**

  *  Avg. time 0.00200019 sec
  *  Min. time 0.000999928 sec
  *  Max. time 0.0030005 sec
  *  Tot. time 0.0200019 sec
  *  Stops 10


**Client process requests ~50kb and gets ~500kb response of server process**

  *  Avg. time 0.000850081 sec
  *  Min. time 0.000999928 sec
  *  Max. time 0.00200009 sec
  *  Tot. time 0.0170016 sec
  *  Stops 20


**Client process requests ~10kb and gets ~100kb response  of server process**

  *  Avg. time 0.00019002 sec
  *  Min. time 0 sec
  *  Max. time 0.0010004 sec
  *  Tot. time 0.019002 sec
  *  Stops 100


*Client process requests ~1kb and gets ~10kb response  of server process**

 *  Avg. time 4.50044e-005 sec
 *  Min. time 0 sec
 *  Max. time 0.0010004 sec
 *  Tot. time 0.0450044 sec
 *  Stops 1000


**Client process requests ~0.1kb and gets ~1kb response  of server process**

  *  Avg. time 1.60016e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.0010004 sec
  *  Tot. time 0.0320032 sec
  *  Stops 2000

  
Client sending request via Named pipes using custom objects reusing initialized resources
---------------------------------
**Client process requests ~100kb and gets ~1000kb response of server process**

  *  Avg. time 0.0375112 sec
  *  Min. time 0.0360107 sec
  *  Max. time 0.0400121 sec
  *  Tot. time 0.375112 sec
  *  Stops 10


**Client process requests ~50kb and gets ~500kb response of server process**

  *  Avg. time 0.0180054 sec
  *  Min. time 0.017005 sec
  *  Max. time 0.0190058 sec
  *  Tot. time 0.360108 sec
  *  Stops 20

**Client process requests ~10kb and gets ~100kb response  of server process**

  *  Avg. time 0.00346104 sec
  *  Min. time 0.00300074 sec
  *  Max. time 0.00400138 sec
  *  Tot. time 0.346104 sec
  *  Stops 100

**Client process requests ~1kb and gets ~10kb response  of server process**

  *  Avg. time 0.000374112 sec
  *  Min. time 0 sec
  *  Max. time 0.0010004 sec
  *  Tot. time 0.374112 sec
  *  Stops 1000

**Client process requests ~0.1kb and gets ~1kb response  of server process**

  *  Avg. time 5.90177e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.0010004 sec
  *  Tot. time 0.118035 sec
  *  Stops 2000

  
Client sending request via Named pipes using custom objects translated into Protobuf messages, expecting messages and translating them back reusing in itialized resources
--------------------------------
**Client process requests ~100kb and gets ~1000kb response of server proces**

  *  Avg. time 0.0569171 sec
  *  Min. time 0.0560167 sec
  *  Max. time 0.0580173 sec
  *  Tot. time 0.569171 sec
  *  Stops 10

**Client process requests ~50kb and gets ~500kb response of server process**

  *  Avg. time 0.0275083 sec
  *  Min. time 0.0260077 sec
  *  Max. time 0.0290086 sec
  *  Tot. time 0.550165 sec
  *  Stops 20

**Client process requests ~10kb and gets ~100kb response  of server process**

  *  Avg. time 0.00509153 sec
  *  Min. time 0.00400114 sec
  *  Max. time 0.00600219 sec
  *  Tot. time 0.509153 sec
  *  Stops 100

**Client process requests ~1kb and gets ~10kb response  of server process**

  *  Avg. time 0.000558167 sec
  *  Min. time 0 sec
  *  Max. time 0.0010004 sec
  *  Tot. time 0.558167 sec
  *  Stops 1000

**Client process requests ~0.1kb and gets ~1kb response  of server process**

  *  Avg. time 7.80233e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.0010004 sec
  *  Tot. time 0.156047 sec
  *  Stops 2000
  
Managed request - native response
==================================
Client sending raw bytes via Named Pipes reusing initialized resources
---------------------------------
**Client process requests ~100kb and gets ~1000kb response of server process**

  * Avg. time 0.00162074 sec
  * Tot. time 0.0162074 sec
  * Stops 10


**Client process requests ~50kb and gets ~500kb response of server process**

  * Avg. time 0.00076057 sec
  * Tot. time 0.0152114 sec
  * Stops 20


**Client process requests ~10kb and gets ~100kb response  of server process**

  * Avg. time 0.00018862 sec
  * Tot. time 0.018862 sec
  * Stops 100


**Client process requests ~1kb and gets ~10kb response  of server process**

  * Avg. time 2.65937E-05 sec
  * Tot. time 0.0265937 sec
  * Stops 1000


**Client process requests ~0.1kb and gets ~1kb response  of server process**

  * Avg. time 1.45056E-05 sec
  * Tot. time 0.0290112 sec
  * Stops 2000

  
Client sending request via Named pipes using custom objects back reusing initialized resources
---------------------------------
**Client process requests ~100kb and gets ~1000kb response of server process**

  * Avg. time 0.02973002 sec
  * Tot. time 0.2973002 sec
  * Stops 10


**Client process requests ~50kb and gets ~500kb response of server process**

  * Avg. time 0.014858985 sec
  * Tot. time 0.2971797 sec
  * Stops 20


**Client process requests ~10kb and gets ~100kb response  of server process**

  * Avg. time 0.002794471 sec
  * Tot. time 0.2794471 sec
  * Stops 100


**Client process requests ~1kb and gets ~10kb response  of server process**

  * Avg. time 0.000297758 sec
  * Tot. time 0.297758 sec
  * Stops 1000


**Client process requests ~0.1kb and gets ~1kb response  of server process**

  * Avg. time 4.887E-05 sec
  * Tot. time 0.09774 sec
  * Stops 2000
 

Managed request - Managed responces
=================================================
Client sending raw bytes via WCF managed binding reusing initialized resources (WCF does not uses optimiations)
-----------------------------------------------------------------------
**Client process requests ~100kb and gets ~1000kb response of server process**

  * Min. time 0.0111614 sec
  * Max. time 0.0136722 sec
  * Avg. time 0.0121099 sec
  * Tot. time 0.1210996 sec
  * Stops 10

**Client process requests ~50kb and gets ~500kb response of server process**

  * Min. time 0.0053891 sec
  * Max. time 0.0068156 sec
  * Avg. time 0.0060897 sec
  * Tot. time 0.1217944 sec
  * Stops 20

**Client process requests ~10kb and gets ~100kb response  of server process**

  * Min. time 0.0014368 sec
  * Max. time 0.0022723 sec
  * Avg. time 0.0016874 sec
  * Tot. time 0.1687463 sec
  * Stops 100

**Client process requests ~1kb and gets ~10kb response  of server process**

  * Min. time 0.0002617 sec
  * Max. time 0.0021911 sec
  * Avg. time 0.0003074 sec
  * Tot. time 0.3074838 sec
  * Stops 1000

**Client process requests ~0.1kb and gets ~1kb response  of server process**

  * Min. time 0.0001221 sec
  * Max. time 0.0004496 sec
  * Avg. time 0.0001394 sec
  * Tot. time 0.2788995 sec
  * Stops 2000

Client sending raw bytes via MS-RPC managed binding reusing initialized resources
---------------------------------------------------------------------------------
**Client process requests ~100kb and gets ~1000kb response of server process**

  * Min. time 0.002344 sec
  * Max. time 0.0033109 sec
  * Avg. time 0.002757 sec
  * Tot. time 0.0275708 sec
  * Stops 10

**Client process requests ~50kb and gets ~500kb response of server process**

  * Min. time 0.0008852 sec
  * Max. time 0.001524 sec
  * Avg. time 0.0010476 sec
  * Tot. time 0.0209523 sec
  * Stops 20

**Client process requests ~10kb and gets ~100kb response  of server process**

  * Min. time 0.0002248 sec
  * Max. time 0.0014574 sec
  * Avg. time 0.0003101 sec
  * Tot. time 0.0310125 sec
  * Stops 100

**Client process requests ~1kb and gets ~10kb response  of server process**

  * Min. time 5.85E-05 sec
  * Max. time 0.0002628 sec
  * Avg. time 7.08E-05 sec
  * Tot. time 0.0708481 sec
  * Stops 1000

**Client process requests ~0.1kb and gets ~1kb response  of server process**

  * Min. time 2.36E-05 sec
  * Max. time 0.0001272 sec
  * Avg. time 2.92E-05 sec
  * Tot. time 0.0584007 sec
  * Stops 2000

One way (waiting confirmation that data was read)
=================================================
Client sending raw bytes and no response via Named Pipes reusing initialized resources
-----------------------------------------------------------------------
**Client process requests ~1000kb**

  *  Avg. time 0.00130014 sec
  *  Min. time 0.000999928 sec
  *  Max. time 0.00200057 sec
  *  Tot. time 0.0130014 sec
  *  Stops 10


**Client process requests ~500kb**

  *  Avg. time 0.000450039 sec
  *  Min. time 0 sec
  *  Max. time 0.0010004 sec
  *  Tot. time 0.00900078 sec
  *  Stops 20


**Client process requests ~100kb**

  *  Avg. time 0.000100007 sec
  *  Min. time 0 sec
  *  Max. time 0.00100017 sec
  *  Tot. time 0.0100007 sec
  *  Stops 100


**Client process requests ~10kb**

  *  Avg. time 2.20022e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.00100017 sec
  *  Tot. time 0.0220022 sec
  *  Stops 1000


**Client process requests ~1kb**

  *  Avg. time 1.1001e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.0010004 sec
  *  Tot. time 0.022002 sec
  *  Stops 2000
  
  
Client sending raw bytes and no response raw bytes via Windows Messaging
-------------------------------------------------------------------------
**Client process requests ~1000kb**

  *  Avg. time 0.00179999 sec
  *  Min. time 0.000999928 sec
  *  Max. time 0.00200009 sec
  *  Tot. time 0.0179999 sec
  *  Stops 10

**Client process requests ~500kb**

  *  Avg. time 0.000899994 sec
  *  Min. time 0.000999928 sec
  *  Max. time 0.00199986 sec
  *  Tot. time 0.0179999 sec
  *  Stops 20

**Client process requests ~100kb**

  *  Avg. time 0.000160003 sec
  *  Min. time 0 sec
  *  Max. time 0.00100017 sec
  *  Tot. time 0.0160003 sec
  *  Stops 100

**Client process requests ~10kb**

  *  Avg. time 3.80001e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.00100017 sec
  *  Tot. time 0.0380001 sec
  *  Stops 1000


**Client process requests ~1kb**

  *  Avg. time 2.1e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.00100017 sec
  *  Tot. time 0.0420001 sec
  *  Stops 2000
 
# Serialization

**Batching 2000 requests ~0.1kb and ~1kb responses **

  *  Avg. time 0.0221023 sec
  *  Min. time 0.0210021 sec
  *  Max. time 0.0240026 sec
  *  Tot. time 0.221023 sec
  *  Stops 10
 
## ~100kb request


**Custom response object creation**

  *  Avg. time 0.00780077 sec
  *  Min. time 0.00700045 sec
  *  Max. time 0.00800085 sec
  *  Tot. time 0.0780077 sec
  *  Stops 10


**Custom response object serialization**

  *  Avg. time 0.0117012 sec
  *  Min. time 0.0110009 sec
  *  Max. time 0.0130014 sec
  *  Tot. time 0.117012 sec
  *  Stops 10


**Custom response object deserialization**

  *  Avg. time 0.0129013 sec
  *  Min. time 0.012001 sec
  *  Max. time 0.0140016 sec
  *  Tot. time 0.129013 sec
  *  Stops 10


**Protobuf response message creation**

  *  Avg. time 0.00790071 sec
  *  Min. time 0.00700045 sec
  *  Max. time 0.00900078 sec
  *  Tot. time 0.0790071 sec
  *  Stops 10


**Protobuf response message serialization**

  *  Avg. time 0.00320039 sec
  *  Min. time 0.00300002 sec
  *  Max. time 0.00400043 sec
  *  Tot. time 0.0320039 sec
  *  Stops 10


**Protobuf response message deserialization**

  *  Avg. time 0.010801 sec
  *  Min. time 0.0100009 sec
  *  Max. time 0.0110011 sec
  *  Tot. time 0.10801 sec
  *  Stops 10

## ~1 megabyte response


**Custom request creation**

  *  Avg. time 0.000900221 sec
  *  Min. time 0.00100017 sec
  *  Max. time 0.0010004 sec
  *  Tot. time 0.00900221 sec
  *  Stops 10


**Custom request to byte array serialization*

  *  Avg. time 0.000800085 sec
  *  Min. time 0.000999928 sec
  *  Max. time 0.00100017 sec
  *  Tot. time 0.00800085 sec
  *  Stops 10


**Custom request instatiation from byte array*

  *  Avg. time 0.00119996 sec
  *  Min. time 0.000999928 sec
  *  Max. time 0.00200009 sec
  *  Tot. time 0.0119996 sec
  *  Stops 10


**Protobuf request message creation**

  *  Avg. time 0.00130017 sec
  *  Min. time 0.000999928 sec
  *  Max. time 0.00200033 sec
  *  Tot. time 0.0130017 sec
  *  Stops 10


**Protobuf request message serialization**

  *  Avg. time 0.000500131 sec
  *  Min. time 0.00100017 sec
  *  Max. time 0.0010004 sec
  *  Tot. time 0.00500131 sec
  *  Stops 10


**Protobuf request message deserialization**

  *  Avg. time 0.00100012 sec
  *  Min. time 0.000999928 sec
  *  Max. time 0.00100017 sec
  *  Tot. time 0.0100012 sec
  *  Stops 10