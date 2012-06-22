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
    internal class EntitySpawnPacket : IPacket
    {
        private const int Length = 96;

        public EntitySpawnPacket(NPC npc)
        {
            // TODO there are many things missing.
            Data = new byte[Length];
            Data[0] = Id;
            // VID 0x01 + 4
            BitConverter.GetBytes(npc.Vid).CopyTo(Data, 0x1);
            // Position 0x9 + 8
            BitConverter.GetBytes(npc.AbsolutePosition.X).CopyTo(Data, 0x9);
            BitConverter.GetBytes(npc.AbsolutePosition.Y).CopyTo(Data, 0xD);
            // Vnum 0x16 + 2
            BitConverter.GetBytes(npc.Vnum);
            // AtkSpeed, MovSpeed (stub) 0x18 + 2
            Data[0x18] = 100;
            Data[0x19] = 100;
            // TODO There are a lot of empty bytes, check them. I think they are "flags".
            // VID (again...) 0x28 + 2
            BitConverter.GetBytes(npc.Vid).CopyTo(Data, 0x28);
            // Name 0x2D + ????
            Encoding.ASCII.GetBytes(npc.Name).CopyTo(Data, 0x2D);
            // Empire 0x4E + 1
            Data[0x4E] = (byte) EmpireFlag.NPC;
        }

        public EntitySpawnPacket(Character character)
        {
            // TODO there are many things missing.
            Data = new byte[Length];
            Data[0] = Id;
            // VID 0x01 + 4
            BitConverter.GetBytes(character.Vid).CopyTo(Data, 0x1);
            // Position 0x9 + 8
            BitConverter.GetBytes(character.AbsolutePosition.X).CopyTo(Data, 0x9);
            BitConverter.GetBytes(character.AbsolutePosition.Y).CopyTo(Data, 0xD);
            // Vnum 0x16 + 2
            BitConverter.GetBytes(character.Race);
            // AtkSpeed, MovSpeed (stub) 0x18 + 2
            Data[0x18] = 100;
            Data[0x19] = 100;
            // TODO There are a lot of empty bytes, check them. I think they are "flags".
            // VID (again...) 0x28 + 2
            BitConverter.GetBytes(character.Vid).CopyTo(Data, 0x28);
            // Name 0x2D + ????
            Encoding.ASCII.GetBytes(character.Name).CopyTo(Data, 0x2D);
            // Empire 0x4E + 1
            Data[0x4E] = (byte) character.Empire;
            // Part_Main  0x46 +2
            BitConverter.GetBytes(character.Armor).CopyTo(Data, 0x46);
            // Part_Weapon 0x48 + 2

            BitConverter.GetBytes(character.Weapon).CopyTo(Data, 0x48);
            // Part_Unknown 0x4a + 2

            // Part_Hair 0x4C + 2
            BitConverter.GetBytes(character.Hair).CopyTo(Data, 0x4C);
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