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
    internal class CharacterStatsPacket : IPacket
    {
        private const int Length = 1024;

        public CharacterStatsPacket(Character character)
        {
            Data = new byte[Length];
            Data[5] = character.Level;
            BitConverter.GetBytes(character.Exp).CopyTo(Data, 0xD);
            BitConverter.GetBytes(character.RequiredExp).CopyTo(Data, 0x11);
            BitConverter.GetBytes(character.CurHP).CopyTo(Data, 0x15);
            BitConverter.GetBytes(character.MaxHP).CopyTo(Data, 0x19);
            // TODO: Add Mana, MaxMana properties to Database's character table
            BitConverter.GetBytes(character.Gold).CopyTo(Data, 0x2D);
            BitConverter.GetBytes(character.Strong).CopyTo(Data, 0x31);
            BitConverter.GetBytes(character.Vitality).CopyTo(Data, 0x35);
            BitConverter.GetBytes(character.Dexterity).CopyTo(Data, 0x39);
            BitConverter.GetBytes(character.Intelligence).CopyTo(Data, 0x3D);
            // TODO: Add AttackSpeed, MovSpeed, CastSpeed, PhysicalAttack, PhysicalDefence, MagicalAttack, MagicalDefence properties to Database's character table
            BitConverter.GetBytes(character.StatPoints).CopyTo(Data, 0x69);
            BitConverter.GetBytes(character.SkillPoints).CopyTo(Data, 0x71);
        }

        #region IPacket Members

        public byte Id
        {
            get { return (byte) OutPackets.CharacterStatsPacket; }
        }

        public byte[] Data { get; private set; }

        #endregion
    }
}