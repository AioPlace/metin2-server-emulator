# Introduction #

Metin2 Server Emulator crypts most packets with a standard **[XTEA](http://en.wikipedia.org/wiki/XTEA) algorithm**.


XTEA requires a 128bit key to do the work, and Metin2 uses three different keys (depending on packet source): **pong**, **session key 1** and **session key 2**.

## Pong ##
**Pong** is the 128bit key used to encrypt AuthPhase (login) packets. In original server files, the Pong is hard-coded in the executable, and, by default is the ASCII equivalent of `testtesttesttest`.

As said before, Pong is used in the login phase of the game, but it is also used in the GameAuthorization packet (passage from Auth to Game).

In Metin2 Server Emulator you can edit the pong simply by editing the relative voice in the configuration file.

**Note**: You also have to edit the Pong in your Metin2 client launcher, otherwise login won't be accepted.

## Session key 1 ##
The **Session Key 1** is randomly generated at run-time by the client and it is sent to the server within the GameAuthorization packet.

This key is used to crypt packets from client to server.

## Session key 2 ##

Well, here we **must** applaude YMIR for their such great work! We don't know how the hell this key is generated, we only know that this key **does not** travel within any packet, and so it is generated at run-time at the same time by client and server with a not-known algorithm. And yes, this is the reason because this emulator needs a KeyForcer.

By the way, **session key 2** is used to encrypt packets from server to client.