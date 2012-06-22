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

namespace Metin2AuthServerEmulator.Network.Packets.In
{
    internal class HandshakePacket : IPacketIn
    {
        private const int PacketLength = 13;

        #region IPacketIn Members

        public byte Id { get; set; }

        public int Length
        {
            get { return PacketLength; }
        }

        public byte[] Data { get; set; }

        public void ParseBuffer(byte[] buffer)
        {
            //TODO Parse buffer?
        }

        #endregion
    }
}