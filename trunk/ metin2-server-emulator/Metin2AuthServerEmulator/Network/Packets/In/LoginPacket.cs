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
using System.Text;

namespace Metin2AuthServerEmulator.Network.Packets.In
{
    internal class LoginPacket : IPacketIn
    {
        private const int PacketLength = 72;
        public string Username { get; private set; }
        public string Password { get; private set; }
        public byte[] Key { get; private set; }

        #region IPacketIn Members

        public byte Id
        {
            get { return (byte) InPackets.LoginPacket; }
        }

        public int Length
        {
            get { return PacketLength; }
        }

        public byte[] Data { get; set; }

        public void ParseBuffer(byte[] buffer)
        {
            Username = Encoding.ASCII.GetString(buffer, 1, 30).Trim('\0');
            Password = Encoding.ASCII.GetString(buffer, 32, 16).Trim('\0');
            Key = new byte[16];
            Array.Copy(buffer, 49, Key, 0, 16);
        }

        #endregion
    }
}