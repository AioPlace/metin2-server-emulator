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

using System;
using Metin2GameServerEmulator.World.Entities;

namespace Metin2GameServerEmulator.Network.Packets.Out
{
    internal class CharacterInfoPacket : IPacket
    {
        private const int Length = 24;

        public CharacterInfoPacket(int type, int quantity, Character character)
        {
            Data = new byte[Length];
            Data[0] = Id;
            // VID 0x4 + 4
            BitConverter.GetBytes(character.Vid).CopyTo(Data, 0x4);
            // Type 0x8 + 4?
            BitConverter.GetBytes(type).CopyTo(Data, 0x8);
            // Quantity 0xD + 4?
            BitConverter.GetBytes(quantity).CopyTo(Data, 0xD);
        }

        #region IPacket Members

        public byte Id
        {
            get { return (byte) OutPackets.CharacterInfoPacket; }
        }

        public byte[] Data { get; private set; }

        #endregion
    }
}