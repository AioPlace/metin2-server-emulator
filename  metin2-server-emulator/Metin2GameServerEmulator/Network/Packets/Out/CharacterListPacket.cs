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
using Metin2GameServerEmulator.World.Entities;

namespace Metin2GameServerEmulator.Network.Packets.Out
{
    internal class CharacterListPacket : IPacket
    {
        private const int Length = 352;

        public CharacterListPacket(Character[] characters, byte empire)
        {
            Data = new byte[Length];
            Data[0] = Id;

            Header(empire).CopyTo(Data, 1);
            for (int i = 0; i < characters.Length; i++)
            {
                int offset = 17 + 63*i;
                CharStruct(characters[i], i).CopyTo(Data, offset);
            }
        }

        #region IPacket Members

        public byte Id
        {
            get { return (byte) OutPackets.CharacterListPacket; }
        }

        public byte[] Data { get; private set; }

        #endregion

        private byte[] Header(byte empire)
        {
            byte[] ret = new byte[16];
            ret[0] = empire;
            ret[7] = 0xFD;
            ret[8] = 0x03;
            ret[0xF] = 0x20;
            return ret;
        }

        private byte[] CharStruct(Character character, int index)
        {
            byte[] ret = new byte[63];
            // Player ID 0x0, 0x3
            byte[] pid = BitConverter.GetBytes(character.Pid);
            pid.CopyTo(ret, 0);
            // Name 0x4, 0x1C
            Encoding.ASCII.GetBytes(character.Name).CopyTo(ret, 0x4);
            // Race 0x1D
            ret[0x1D] = character.Race;
            // Level 0x1E
            ret[0x1E] = character.Level;
            // GameTime 0x1F, 0x22
            byte[] gametime = BitConverter.GetBytes(character.PlayTime);
            gametime.CopyTo(ret, 0x1F);
            // Status 0x23, 0x26
            ret[0x23] = (byte) character.Strong;
            ret[0x24] = (byte) character.Vitality;
            ret[0x25] = (byte) character.Dexterity;
            ret[0x26] = (byte) character.Intelligence;
            // Armor 0x27, 0x29
            byte[] armor = BitConverter.GetBytes(character.Armor);
            armor.CopyTo(ret, 0x27);
            // Hair 0x2A, 0x2C
            byte[] hair = BitConverter.GetBytes(character.Hair);
            hair.CopyTo(ret, 0x2A);

            // Guild
            byte[] guildID = BitConverter.GetBytes(character.Guild);
            guildID.CopyTo(Data, 0x10D + (index*4));
            byte[] guildName = Encoding.ASCII.GetBytes(character.GuildName);
            guildName.CopyTo(Data, 0x11D + (index*13));
            // EndGuild

            return ret;
        }
    }
}