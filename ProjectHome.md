# Metin2 Server Emulator #

**Note: This is a proof of concept with educative purpose only! We are not responsible for any copyright infringiment!**

This is a Metin2 Server Emulator, the project is discontinued because of lack of time and Metin2 bad packet structure.
Currently you can get the client to a working character selection screen.

To get it working you have to freeze client encryption keys (Freezer is included in the solution)

Whole solution is written in C#, here are the contained projects:
  * Metin2 Auth Server Emulator `*`
  * Metin2 Game Server Emulator `*`
  * Metin2 Server Emulator Common (DLL with shared code)
  * Metin2 Key Stat (Encryption keys freezer) `**`
  * Metin2 Packet Dumper (Packet sniffer, bad code but it works fine) `**`

`*` Tested and working with mono on linux

`**` Compatible only with a certain launcher version, you may have to edit the code to get it fully working [Link](http://localhostr.com/files/0k4Oyhn/capture.png)



---


[![](http://www.gnu.org/graphics/gplv3-127x51.png)](http://www.gnu.org/licenses/gpl.html)

Whole project is licensed under GNU GPL v3