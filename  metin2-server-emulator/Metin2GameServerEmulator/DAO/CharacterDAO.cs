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
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using Metin2GameServerEmulator.Network.Packets.In;
using Metin2GameServerEmulator.World.Entities;
using Metin2ServerEmulatorCommon;
using Metin2ServerEmulatorCommon.DAO;
using MySql.Data.MySqlClient;

namespace Metin2GameServerEmulator.DAO
{
    internal class CharacterDAO
    {
        private readonly Database _conn;

        internal CharacterDAO(Database connection)
        {
            _conn = connection;
        }

        internal Character[] GetCharactersByAccount(int accountID)
        {
            List<UInt32> temp = new List<uint>();
            string query = string.Format(
                "SELECT `id` FROM `character` WHERE `account_id`='{0}'", accountID);
            MySqlDataReader reader = _conn.Query(query);

            while (reader.Read())
            {
                temp.Add(reader.GetUInt32("id"));
            }

            reader.Close();

            return temp.Select(GetCharacter).ToArray();
        }

        internal Character GetCharacter(string name)
        {
            string query = string.Format(
                "SELECT `id` FROM `character` WHERE `name`='{0}'", name);
            MySqlDataReader reader = _conn.Query(query);
            uint id = reader.GetUInt32("id");
            return GetCharacter(id);
        }

        internal Character GetCharacter(uint id)
        {
            Character character = null;
            string query = string.Format(
                "SELECT * FROM `character` WHERE `id`='{0}'", id);
            MySqlDataReader reader = _conn.Query(query);
            if (reader.Read())
            {
                character = new Character
                                {
                                    Race = reader.GetByte("race"),
                                    Level = reader.GetByte("level"),
                                    Job = reader.GetByte("job"),
                                    Name = reader.GetString("name"),
                                    Pid = reader.GetUInt32("id"),
                                    MapIndex = reader.GetInt32("map_index"),
                                    PlayTime = reader.GetInt32("playtime"),
                                    Strong = reader.GetUInt32("str"),
                                    Vitality = reader.GetUInt32("vit"),
                                    Dexterity = reader.GetUInt32("dex"),
                                    Intelligence = reader.GetUInt32("iq"),
                                    Exp = reader.GetUInt32("exp"),
                                    Gold = reader.GetUInt32("gold"),
                                    StatPoints = reader.GetUInt16("stat_point"),
                                    SkillPoints = reader.GetUInt16("skill_point"),
                                    Armor = reader.GetUInt16("armor"),
                                    Hair = reader.GetUInt16("hair"),
                                    Alignment = reader.GetInt32("alignment"),
                                    Guild = reader.GetUInt32("guild"),
                                    AbsolutePosition = new Position(reader.GetInt32("x"), reader.GetInt32("y")),
                                };
            }
            reader.Close();

            return character;
        }

        internal Character[] GetCharactersByAccount(string username)
        {
            AccountDAO accMgr = new AccountDAO(Server.ServerInstance.Database);
            int accountID = accMgr.GetAccountID(username);
            return GetCharactersByAccount(accountID);
        }

        internal Character[] GetMembersByGuild(uint id)
        {
            MySqlDataReader reader =
                _conn.Query(string.Format("SELECT `id` FROM `character` WHERE `guild`='{0}'", id));
            List<UInt32> temp = new List<uint>();
            while (reader.Read())
            {
                temp.Add(reader.GetUInt32("id"));
            }

            reader.Close();

            return temp.Select(GetCharacter).ToArray();
        }

        internal Character[] GetMembersByGuild(string name)
        {
            MySqlDataReader reader =
                _conn.Query(string.Format("SELECT `id` FROM `guild` WHERE `name`='{0}'", name));
            uint guildID = reader.GetUInt32("id");
            reader.Close();
            return GetMembersByGuild(guildID);
        }

        internal Character CreateNewCharacter(CharacterCreatePacket characterCreatePacket, string accountUsername)
        {
            AccountDAO accMgr = new AccountDAO(Server.ServerInstance.Database);
            int accountID = accMgr.GetAccountID(accountUsername);
            if (GetCharactersByAccount(accountID).Length >= 4)
                // Se necessario, possiamo creare due classi che ereditano da Exception per queste due eccezioni invece di usare SqlAlreadyFilledException.
                throw new SqlAlreadyFilledException(
                    "This account already have four characters. Is not allowed another character.");
            if (GetCharacter(characterCreatePacket.CharacterName) != null)
                throw new SqlAlreadyFilledException("A character with this name already exists!");
            MySqlDataReader writer = // OK LOL!!!
                _conn.Query(
                    string.Format("INSERT INTO `character` (`name`, `race`, `account_id`) VALUES ('{0}', {1}, {2})",
                                  characterCreatePacket.CharacterName, characterCreatePacket.SexRace, accountID));
            // TODO: Add CharacterPosition (account-relative) & Look to character table.
            // TODO: Set all values for the record and so call GetCharacter(uint) to get a Character's instance and return...
            writer.Close();
            return null;
        }
    }
}