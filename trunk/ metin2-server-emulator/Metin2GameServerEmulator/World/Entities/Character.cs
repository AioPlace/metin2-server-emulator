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
using Metin2GameServerEmulator.DAO;
using Metin2GameServerEmulator.Game.Chat;
using Metin2GameServerEmulator.Network.Packets.In;

// ReSharper disable ConvertToAutoProperty

namespace Metin2GameServerEmulator.World.Entities
{
    internal class Character : IEntity
    {
        #region "Eredited from IEntity"

        public ushort Vid { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }
        public int MaxHP { get; set; }
        public int CurHP { get; set; }
        public uint Strong { get; set; }
        public uint Vitality { get; set; }
        public uint Dexterity { get; set; }
        public uint Intelligence { get; set; }
        public Position RelativePosition { get; set; }
        public Position AbsolutePosition { get; set; }
        public int MapIndex { get; set; }
        public IEntity LastAttacker { get; set; }
        public EntityType Type { get; set; }
        public EmpireFlag Empire { get; set; }

        #endregion

        #region "Proprieties"

        internal byte Job { get; set; }
        internal uint Pid { get; set; }
        internal Int32 PlayTime { get; set; }
        internal Int32 Alignment { get; set; } // karma
        internal UInt32 Guild { get; set; }
        //internal string GuildName { get; set; }
        internal UInt16 Armor { get; set; }
        internal UInt16 Hair { get; set; }
        internal UInt32 Weapon { get; set; }
        internal UInt32 Exp { get; set; }
        internal UInt32 Gold { get; set; }
        internal UInt16 StatPoints { get; set; }
        internal UInt16 SkillPoints { get; set; }
        internal byte Race { get; set; }

        #endregion

        #region "Methods"

        internal void OnDeath(IEntity sender)
        {
            // TODO All
            if (sender != null)
                switch (sender.Type)
                {
                    case EntityType.Monster:
                        // DEAD_BY_MONSTER
                        break;
                    case EntityType.NPC:
                        // DEAD_BY_NPC
                        break;
                    case EntityType.Player:
                        // DEAD_BY_PC
                        break;
                    default:
                        // DEAD
                        break;
                }
        }

        internal void OnCreate() // ???
        {
            // TODO All
        }

        internal static Character Create(string accountName, CharacterCreatePacket characterCreatePacket)
        {
            CharacterDAO chrMgr = new CharacterDAO(Server.ServerInstance.Database);
            return chrMgr.CreateNewCharacter(characterCreatePacket, accountName); // Cosa fare con OnCreate()?
        }

        internal void Purge()
        {
            // TODO All
        }

        internal void Say(ChatType chatType, string text)
        {
            // TODO: Send Chat Packet in correct range.
        }

        internal void UseSkill()
        {
            // TODO All
        }

        internal void Attack()
        {
            // TODO Calc damage, send damage in all entities in range, send damage packet to all
        }

        internal void Animation()
        {
            // TODO Send Animation Packet in range
        }

        #endregion

        #region "Calculated Properties"

        internal UInt32 RequiredExp
            // TODO: This method shouldn't be reside in this class. Should be used a uint GetNextExp(byte level) function.
        {
            get
            {
                if (Level < 77)
                    return Level; // TODO: see http://wiki.metin2.it/index.php/Exp
                if (Level >= 77 && Level <= 90)
                    return (uint) (2*1000000*Math.Pow(Level, 2) + 2*10000000*Level + 2*100000000);
                if (Level > 90 && Level <= 99)
                    return (uint) (7*1000000*Math.Pow(Level, 2) + 8*10000000*Level + 2*1000000000);
                if (Level > 99)
                    return Level; // TODO: Add a function
                return 0;
            }
        }

        internal string GuildName
        {
            get
            {
                GuildDAO guildMgr = new GuildDAO(Server.ServerInstance.Database);
                return guildMgr.GetGuildName(Guild);
            }
        }

        #endregion

        #region "Calc Function"

        internal void IncreaseHP()
        {
            uint hpBase = 0;
            Random rand = new Random();
            if (Race == 0 || Race == 4)
                hpBase = 600;
            if (Race == 2 || Race == 5 || Race == 1 || Race == 6)
                hpBase = 650;
            if (Race == 3 || Race == 7)
                hpBase = 700;
            MaxHP = (int) (hpBase + rand.Next(76, 85)); // TODO: + BonusHP ... see: http://wiki.metin2.it/index.php/HP
        }

        #endregion
    }
}

// ReSharper restore ConvertToAutoProperty