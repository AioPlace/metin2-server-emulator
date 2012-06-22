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

using System.Text;

namespace Metin2GameServerEmulator.Network.Packets.In
{
    internal class CharacterCreatePacket : IPacketIn
    {
        private const int PacketLength = 40;

        /// <summary>
        ///   Gets the position of the character in the owner account.
        /// </summary>
        public byte CharacterPosition { get; private set; }

        /// <summary>
        ///   Gets the name of the new character.
        /// </summary>
        public string CharacterName { get; private set; }

        /// <summary>
        ///   Gets the Sex and Race informations for the new character.
        /// </summary>
        public byte SexRace { get; private set; }

        public byte Look { get; private set; }

        //NOTE: Unknoun bytes: offset 28 & 36. And a lot of NULL bytes....

        #region IPacketIn Members

        public byte Id
        {
            get { return (byte) InPackets.CharacterCreatePacket; }
        }

        public int Length
        {
            get { return PacketLength; }
        }

        public byte[] Data { get; private set; }

        public void ParseBuffer(byte[] buffer)
        {
            CharacterPosition = buffer[1];
            CharacterName = Encoding.ASCII.GetString(buffer, 2, 13).Trim('\0');
            SexRace = buffer[27];
            Look = buffer[29];
        }

        #endregion
    }
}