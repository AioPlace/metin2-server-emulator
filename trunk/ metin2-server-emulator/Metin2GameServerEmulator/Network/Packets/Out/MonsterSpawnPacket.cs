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
    internal class MonsterSpawnPacket : IPacket
    {
        private const int Length = 40;

        public MonsterSpawnPacket(object monster)
        {
            // TODO Build this packet when we'll have a Monster Class.
            Data[0] = Id;
        }

        #region IPacket Members

        public byte Id
        {
            get { return (byte) OutPackets.EntitySpawnPacket; }
        }

        public byte[] Data { get; private set; }

        #endregion
    }
}