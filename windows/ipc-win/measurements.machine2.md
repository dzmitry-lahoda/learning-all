Native request - response
=========================

Client sending raw bytes via Shared Memory
--------------------------------

**Client process requests ~100kb and gets ~1000kb response of server process**

  *  Avg. time 0.00901299 sec
  *  Min. time 0 sec
  *  Max. time 0.0801153 sec
  *  Tot. time 0.0901299 sec
  *  Stops 10

**Client process requests ~50kb and gets ~500kb response of server process**

  *  Avg. time 0.000500715 sec
  *  Min. time 0 sec
  *  Max. time 0.0100143 sec
  *  Tot. time 0.0100143 sec
  *  Stops 20

**Client process requests ~10kb and gets ~100kb response  of server process**

  *  Avg. time 0.000400577 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.0400577 sec
  *  Stops 100

**Client process requests ~1kb and gets ~10kb response  of server process**

  *  Avg. time 0.000180259 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.180259 sec
  *  Stops 1000

**Client process requests ~0.1kb and gets ~1kb response  of server process**

  *  Avg. time 0.000165238 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.330475 sec
  *  Stops 2000
  
Client sending raw bytes via Named Pipes reusing initialized resources
----------------------
**Client process requests ~100kb and gets ~1000kb response of server proces**

  *  Avg. time 0.00200288 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.0200288 sec
  *  Stops 10

**Client process requests ~50kb and gets ~500kb response of server process**

  *  Avg. time 0.00100144 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.0200288 sec
  *  Stops 20


**Client process requests ~10kb and gets ~100kb response  of server process**

  *  Avg. time 0.000200288 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.0200288 sec
  *  Stops 100

**Client process requests ~1kb and gets ~10kb response  of server process**

  *  Avg. time 3.00431e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.0300431 sec
  *  Stops 1000

**Client process requests ~0.1kb and gets ~1kb response  of server process**

  *  Avg. time 1.00143e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.0100143 sec
  *  Tot. time 0.0200286 sec
  *  Stops 2000
  

Client sending request via Named pipes using custom objects reusing initialized resources
--------------------------------

**Client process requests ~100kb and gets ~1000kb response of server process**

  *  Avg. time 0.0741066 sec
  *  Min. time 0.0600863 sec
  *  Max. time 0.0901296 sec
  *  Tot. time 0.741066 sec
  *  Stops 10

**Client process requests ~50kb and gets ~500kb response of server process**

  *  Avg. time 0.0315454 sec
  *  Min. time 0.0300431 sec
  *  Max. time 0.0400577 sec
  *  Tot. time 0.630907 sec
  *  Stops 20
  
**Client process requests ~10kb and gets ~100kb response  of server process**

  *  Avg. time 0.00550792 sec
  *  Min. time 0.0100143 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.550792 sec
  *  Stops 100

**Client process requests ~1kb and gets ~10kb response  of server process**

  *  Avg. time 0.000490705 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.490705 sec
  *  Stops 1000
  
**Client process requests ~0.1kb and gets ~1kb response  of server process**

  *  Avg. time 6.50938e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.130188 sec
  *  Stops 2000

Client sending request via Named pipes using custom objects translated into Protobuf messages, expecting messages and translating them back reusing initialized resources
--------------------------------

**Client process requests ~100kb and gets ~1000kb response of server process**

  *  Avg. time 0.122176 sec
  *  Min. time 0.120173 sec
  *  Max. time 0.130187 sec
  *  Tot. time 1.22176 sec
  *  Stops 10

**Client process requests ~50kb and gets ~500kb response of server process**

  *  Avg. time 0.0610878 sec
  *  Min. time 0.0600863 sec
  *  Max. time 0.0701008 sec
  *  Tot. time 1.22176 sec
  *  Stops 20

**Client process requests ~10kb and gets ~100kb response  of server process**

  *  Avg. time 0.0122176 sec
  *  Min. time 0.0100143 sec
  *  Max. time 0.0200288 sec
  *  Tot. time 1.22176 sec
  *  Stops 100

**Client process requests ~1kb and gets ~10kb response  of server process**

  *  Avg. time 0.000981411 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.981411 sec
  *  Stops 1000

**Client process requests ~0.1kb and gets ~1kb response  of server process**

  *  Avg. time 9.51368e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.190274 sec
  *  Stops 2000

  
Managed request - native response
===================================

Client sending raw bytes via Named Pipes reusing initialized resources
--------------------------------

**Client process requests ~100kb and gets ~1000kb response of server process**

  * Avg. time 0.0193477 sec
  * Tot. time 0.193477 sec
  * Stops 10

**Client process requests ~50kb and gets ~500kb response of server process**

  * Avg. time 0.0083569 sec
  * Tot. time 0.167138 sec
  * Stops 20

**Client process requests ~10kb and gets ~100kb response  of server process**

  * Avg. time 0.00231169 sec
  * Tot. time 0.231169 sec
  * Stops 100

**Client process requests ~1kb and gets ~10kb response  of server process**

  * Avg. time 4.38944E-05 sec
  * Tot. time 0.0438944 sec
  * Stops 1000

**Client process requests ~0.1kb and gets ~1kb response  of server process**

  * Avg. time 2.5953E-04 sec
  * Tot. time 0.51906 sec
  * Stops 2000

 
Client sending request via Named pipes using custom objects back reusing initialized resources
--------------------------------

**Client process requests ~100kb and gets ~1000kb response of server process**

  * Avg. time 0.05974452 sec
  * Tot. time 0.5974452 sec
  * Stops 10

**Client process requests ~50kb and gets ~500kb response of server process**

  * Avg. time 0.02218758 sec
  * Tot. time 0.4437516 sec
  * Stops 20

**Client process requests ~10kb and gets ~100kb response  of server process**

  * Avg. time 0.003804522 sec
  * Tot. time 0.3804522 sec
  * Stops 100

**Client process requests ~1kb and gets ~10kb response  of server process**

  * Avg. time 0.0003793859 sec
  * Tot. time 0.3793859 sec
  * Stops 1000

**Client process requests ~0.1kb and gets ~1kb response  of server process**

  * Avg. time 5.76457E-05 sec
  * Tot. time 0.1152914 sec
  * Stops 2000

One way (waiting confirmation that data was read)
================================================

Client sending raw bytes and no response raw bytes via Windows Messaging
-----------------------------------------------------------------------

**Client process requests ~1000kb**

  *  Avg. time 0.00100143 sec
  *  Min. time 0 sec
  *  Max. time 0.0100143 sec
  *  Tot. time 0.0100143 sec
  *  Stops 10

**Client process requests ~500kb**

  *  Avg. time 0.000500727 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.0100145 sec
  *  Stops 20

**Client process requests ~100kb**

  *  Avg. time 0.000300431 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.0300431 sec
  *  Stops 100

**Client process requests ~10kb**

  *  Avg. time 3.00434e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.0300434 sec
  *  Stops 1000
  
**Client process requests ~1kb**

  *  Avg. time 1.50214e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.0100143 sec
  *  Tot. time 0.0300429 sec
  *  Stops 2000
  
 
Client sending raw bytes and no response via Named Pipes reusing initialized resources
-----------------------------------------------------------------------

**Client process requests ~1000kb**

  *  Avg. time 0.00100143 sec
  *  Min. time 0 sec
  *  Max. time 0.0100143 sec
  *  Tot. time 0.0100143 sec
  *  Stops 10

**Client process requests ~500kb**

  *  Avg. time 0.00100145 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.0200291 sec
  *  Stops 20

**Client process requests ~100kb**

  *  Avg. time 0.000200286 sec
  *  Min. time 0 sec
  *  Max. time 0.0100143 sec
  *  Tot. time 0.0200286 sec
  *  Stops 100

**Client process requests ~10kb**

  *  Avg. time 3.00431e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.0300431 sec
  *  Stops 1000

**Client process requests ~1kb**

  *  Avg. time 1.00145e-005 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.0200291 sec
  *  Stops 2000

# Serialization

**Batching 2000 requests ~0.1kb and ~1kb responses**

  *  Avg. time 0.0620893 sec
  *  Min. time 0.0400574 sec
  *  Max. time 0.0701008 sec
  *  Tot. time 0.620893 sec
  *  Stops 10
  
~100kb request
------------------------------------------------------

**Custom request creation**

  *  Avg. time 0.00200286 sec
  *  Min. time 0 sec
  *  Max. time 0.0100143 sec
  *  Tot. time 0.0200286 sec
  *  Stops 10

**Custom request to byte array serialization**

  *  Avg. time 0.00100145 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.0100145 sec
  *  Stops 10

**Custom request instatiation from byte array**

  *  Avg. time 0.00500717 sec
  *  Min. time 0.0100145 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.0500717 sec
  *  Stops 10

**Protobuf request message creation**

  *  Avg. time 0.00300431 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.0300431 sec
  *  Stops 10

**Protobuf request message serialization**

  *  Avg. time 0 sec
  *  Min. time 0 sec
  *  Max. time 0 sec
  *  Tot. time 0 sec
  *  Stops 10
  
**Protobuf request message deserialization**

  *  Avg. time 0.00100143 sec
  *  Min. time 0 sec
  *  Max. time 0.0100143 sec
  *  Tot. time 0.0100143 sec
  *  Stops 10
  
 ~1 megabyte response
-----------------------------------------------------------------------

**Custom response object creation**

  *  Avg. time 0.0150216 sec
  *  Min. time 0.0100143 sec
  *  Max. time 0.0200288 sec
  *  Tot. time 0.150216 sec
  *  Stops 10

**Custom response object serialization**

  *  Avg. time 0.0110158 sec
  *  Min. time 0.0100143 sec
  *  Max. time 0.0200288 sec
  *  Tot. time 0.110158 sec
  *  Stops 10

**Custom response object deserialization**

  *  Avg. time 0.0220317 sec
  *  Min. time 0.0200288 sec
  *  Max. time 0.0300434 sec
  *  Tot. time 0.220317 sec
  *  Stops 10

**Protobuf response message creation**

  *  Avg. time 0.0200288 sec
  *  Min. time 0.0200286 sec
  *  Max. time 0.0200288 sec
  *  Tot. time 0.200288 sec
  *  Stops 10

**Protobuf response message serialization**

  *  Avg. time 0.00400577 sec
  *  Min. time 0 sec
  *  Max. time 0.0100145 sec
  *  Tot. time 0.0400577 sec
  *  Stops 10

**Protobuf response message deserialization**

  *  Avg. time 0.0280403 sec
  *  Min. time 0.0200286 sec
  *  Max. time 0.0400577 sec
  *  Tot. time 0.280403 sec
  *  Stops 10


