---
marp: true
---

# Architecture and coding of real time 3D shooter

Dzmitry Lahoda, lahoda.pro

Vlad Shimkovich, Heyworks

C# 7.3, .NET Standard 2.0

---

## The beginning

- Small game design studio does prototype of network shooter.

- Publishes likes it and ensues funding.

- No previous experience of doing `realtime fast paced cooperative` games.

---

## The agenda

- Architecture (what server talk to each other server, deployment)
- Development optimization and code reuse
- Realtime code  
  - network, serialization, custom reliability protocol(why)
  - game logic and (user perceived) state consistency

- Meta(kind of enterprise CRUD app)
  - matchmaking
  - realtime communication

---

## Not an agenda

- Analytics and Monetization (shop, ads)
- Level up and upgrade mechanics, logic of lobby, logic configuration
- (3D)Graphics, (3D)Sound
- Authentication and data storage architecture
- Monitoring, logging.
  
Hint: These are important and essential and influence what will be discussed.

Hint: Lobby if where you upgrade and ammunition your army before realtime fight

---

## Overview of architecture

DL: Simple diagram with several clients and metas and realtimes. No details.
DL: will nodes properties (meta and realtime )

Hint: Realtime edge server are very important for gameplay. Laggy games do not earn.

---

## Before game begins. Speculative execution

DL: Upgrade trooper with default money.
DL: Upgrade logic is requested to be done by server.
DL: Upgrade logic is done on client using shared logic
DL: Server runs same logic on it.
DL: No conflic - OK. Conflic - new state is send to client.

Here we see first hint of hack with time, and need for code reuse.
No visible delay must be beetwing user and his desrie to equip helmet for 10 USD.

---

# Before game begins. Match making

DL: client SIgnarR(WebSockets) into Meta.
DL: Queu with matchking alogirht works (different maps, games, for differnt players)
DL: Set of playr to play togter identified
DL: Whole game configueation is send to realtime and is sealed during match.

---

# Code reuse and namespaces

DL: Will show how to setup namespaces and assemblies to reuse code and how.
DL: will try to have just usual dot net project and generate dependencies
DL: stress test run
---

# Non stop servers. Meta

Sometimes we what to change mathaking and upgrade rules without redeploy.
Example: Level 100 needs 42 extra exepricnes.
Example: If level is more than 13, allow to play on new map.

How:
- Game config JSON is edited in grid view editor by game designer
- JSON is validated by JSON Shema (NSwagger) 
- Dropped into one of meta folders via secured file (SCP)
- Folder is monitore by ASP net file montoore
- JSON is read and validaed, stored in database
- New JSON is porpogated into all actors

Hint: there are may be subtle issues during upgrade, to avoid this all actors items pass config version alongside.

--- 

# Several words about deployment

- Fresh git clone runs as local host out of box Unity run and dotnet run.
- Deployed for testing onto single instnace
- Meta deloyed on centra storge, database in replica, realtiems onto edges
- May stop and start meta and realtiems separately
- Using `pwsh` and `ssh`
- Hardware no VM (5-10 times faster than cloud VMs for same price)

See 12 factor app. See deploument

---

# Realtime. Constraints.

- Player must perceive games as if it is running on his local device.
- Which includes immediate feedback on pressed shoot button.
- But allow to prevent cheating
- Player must
- Performance must be predictable and linear in time.

---

# Realtime. Architecture.

- There are several, very different.
- Exensivelu documented https://github.com/MFatihMAR/Awesome-Game-Networking

Hints: They way you do physics, what genre you are doing, how many live entities in game at once and how fast they change state and monteization stategy greatly - all these influcne options chosen.

---

# Realtime. Arhitecture. Our

- no physics simulation on server
- client does physics (no map, no collisions)
- sever and client maintain common world state
- world stated updated by commands from client
  
- Example: clip and shots count, damage (dependant on current player parameters) will realized as part of model
- Example: who was under attack will came from client (physical world location), but than game snapshots model will work

Hint: Cheating is possible, anticheaing either.  Improve step by step as game becames popular

---

# Realime. Netcode. Client shoots.

Frame 42. 3600 milliseconds since game started.

- *Input* Gamer presses shot
- *Graphics* Fire from gun and ammo decrement
- *Netcode* Client sends input into networks

Hint: Should we wait for delivery of command? Do we care if exactly that command reached the server?
Hint: Commands - shoot, use skill, move, ...

---

# Realtime. From server to client

Frame 42. 3700 milliseconds since game started.

-
DL: flow from server to client
DL: if new world state available, send it

---


# Round Trip time (rtt)
- ping
- Sender: Record time 1. Sequenc number 10.
- Server: Last aknowledege packet. Sequnce number 10. SEqver squences 5. Time since 10 was received was 30 (time processing of server).
- So RTT is cloent = time recvied - time processing - time send 
- measrue on every packet X

- other way of server to say the time it wants client to send its input?

---


# Realtimem. Netcode. TCP

TCP does guaranded delivery, so went message lost - we wait to deliver what is already usessle.
Custom protocl on UDP:
DL: issues with UDP.

---

# Size of data

10 playes eah 10 properies, 10 game object 5 properies, each player has 5 skills, each skill has 5 properites.

---

# MTU
- 1.4kb or 476b.
- could allow cust into 476b and do cross packet compression? so that imporatnt stuff in first packet. not imporatant in second.


---

# Realtime. Netcode. Message size.JSON

---


# Realtime. Netcode. Strings with known shema.

---

# Realtime. Know. Numbers compresion.


---

# Let loot at numbers.

Negative integers in computers start with 1. So negative value can be `1000 0001` or `1000 1111`. 
`ZigZag` converts that into something which does not starts from 1, e.g. `00000111`. 
Positive values are encoded either.
Eventually, these zeroes in start may be dropped during networks serialization.

https://gist.github.com/mfuerstenau/ba870a29e16536fdbaba

---

# Lossles. SevenBit

---

# Lossles. Fibonacci

---

# Floatrs and precision loss.

Half, quatenion. 

---

# Range

We now number is from 0 to 10000. It needs max 14 bits.
We now then number is from 0 to 100, with needed precisions 0.01. So need 14 bits.

Hint: If you see mistake in our slides, be sure these are correct in game - becasuse we TDD.

---

# Protocoal based.

- Sequence number (will be used for other puposes either)
- Client tels sever what he received to server

---

# Diff

1. Server send client number 10. 
2. We still have 10 on server. Send nothing this time.
3. Sever has 12 now. Send 12 to client.

--- 

# Diff with confirmation


0. We send client 10.
1. We

               Send bitmask of received tick baseliend on LAST_RECEIVED_SEQUENCE
                bit mask is must if doing delta -  server will resend delta until got into client
                if at least one delta will not get in buffer size cycle - disconnect (allow send robustly events)


No changes.

On Server A has value 123456. server knows that Client confirmed value of A to be 123456. Server sends one or couple of bits to state there is no change of that value.

Changed.

On Server A has value 123456. server knows that Client confirmed value of A to be 123456. Server sends value of 123456 with some bit(s) to state there is change.


---

# Delta

1. Client received Trooper position 7 at Server simulaiton framr 88.
2. Server got configramtion from client that he recieved at 91.
3. Trooper position is 10 at 91 server frame.
4. Server sends 3 to client basing on frame 88.

---

# Prediction

1. Trooper moved 7, 8, 9, 10 at frame 88, 89, 90, 91 at Server.
2. Client knows 7, 8, 9 moves and predicts 10.
3. Server knows that client will predict 10 as same formula used.
4. Server thends 0, i.t delta from predicion.

Hint: Prediction could be non linear, but complex, and ulimately learned from many games and used.
   
---


# Huffaman and fast huffman.


Affter diff and delta `00000000000111111000000011111110000` - can compress

Hint: may look into zstanda and agd to look for faster codeds
Hint: all this logic should take only part of rednerign frame 
Hint: could we learn prediciton?
Other pssobilieis.

---

# Game stucutre example

---

## Realtime. Netcode. Serializtion. Unsafe reflecion. 

---

## Componens into network.

---

## Code gen.

---

## Time sharing.

---

## Server time

DL: if we have larger than 42*30 than tick

---

## Client in future

--- 

# Tuning future

---

# other plauers in the past

---

# delay. interplocate locatio.

---

# delay. interpolafe velocity.

---

---


# how far we can shoot into the past?

 Minimazing delay

VS: Using variance to ensure client in the futur for RTT

---

# handle dissconnection. sliding windows.

---


# What if our speculation was wrong?

VS: Reconcilation

---

# Recall

No GC and all this logic.

Example: cannot jump if rooted.
Example: Cannot be hurt if immortal.
Example: Must became visible when uses grenade.

---

## Server vertical scale architecture.

Input = Sequencye buffer (MUST SHOW WHAT IS SEQUNCE BUFFER EARLIER)

Room = `System.Threading.Channell<GameCommand>`. Rooms = `List<System.Threading.Channell<GameCommand>>

Game Server= Rooms + `Task.Run` eaual to number of hardware threads minus - 1.

---

## Server horizontal scale code.


Hint: Vote for  to allow GC free channels!

---

# Industyc knowled.

ECS, funcional, not oop.

---

# ECS constrainst

DL: Why we need ECS?

---

# ECS memory layout

DL: Drawing and links

---

# ECS

struct Postion { float x, float y, float z}
struct Wearpong { WeaponStates state, ushort bullets}
struct Player { ushort id}

Others: RotationAndPitch, Grenade, ImmoratlSkill, ...

---

# Systems

DL:

--

# ECS. Struncs, ref, index, many worlds, C# 

Past, seraliztion, history.

readonly systrems are

---

# ECS is ideal?

---

## Most valuable resource protection? (enterposzy stuff)

### Production 

### Administrator (root)
- password is only possible from office IP
- by key available from any device

#### Database
- servers are behind one special passphrased key, must be stored secured in copies and not used daily.

---

### Read Database

- read user is created for server and could login only via key
- key is used to tunnel into database accessed via read database user behind password
- new read access can be added by root(when used with special key)