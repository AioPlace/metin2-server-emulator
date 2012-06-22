#region License

//     This file is part of Metin 2 Server Emulator.
// 
//     Metin 2 Server Emulator is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     Metin 2 Server Emulator is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with Metin 2 Server Emulator.  If not, see <http://www.gnu.org/licenses/>

#endregion

namespace Metin2GameServerEmulator.Network.Packets.Out
{
    internal class ItemCreatePacket : IPacket
    {
        private const int Length = 48;

        // TODO create item class
        public ItemCreatePacket(object item)
        {
            // TODO there are many things missing.
            Data = new byte[Length];
            Data[0] = Id;

            // Linee guida per questo pacchetto:
            // 0x01 @ 1 = Slot dell'inventario
            // 0x02 @ 4 = Vnum dell'oggetto
            // 0x06 @ 2 = Value da riconoscere, probabilmente quantità
            // 0x0B @ 4 = Socket0
            // 0x0F @ 4 = Socket1
            // 0x13 @ 4 = Socket2
            // da i = 0 a 6 :
            //  0x17 + (i*3) @ 1 = attrtype(i)
            //  0x18 + (i*3) @ 2 = attrvalue(i)
        }

        #region IPacket Members

        public byte Id
        {
            get { return (byte) OutPackets.ItemCreatePacket; }
        }

        public byte[] Data { get; private set; }

        #endregion
    }
}